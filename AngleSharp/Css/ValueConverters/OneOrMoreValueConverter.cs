﻿namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

    sealed class OneOrMoreValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;
        readonly Int32 _minimum;
        readonly Int32 _maximum;

        public OneOrMoreValueConverter(IValueConverter converter, Int32 minimum, Int32 maximum)
        {
            _converter = converter;
            _minimum = minimum;
            _maximum = maximum;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var items = value.ToItems();
            var n = items.Count;

            if (n >= _minimum && n <= _maximum)
            {
                var values = new IPropertyValue[items.Count];

                for (int i = 0; i < n; i++)
                {
                    values[i] = _converter.Convert(items[i]);

                    if (values[i] == null)
                        return null;
                }

                return new MultipleValue(values, value);
            }

            return null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            var values = new IPropertyValue[properties.Length];

            for (var i = 0; i < properties.Length; i++)
            {
                var result = _converter.Construct(new[] { properties[i] });

                if (result == null)
                    return null;

                values[i] = result;
            }

            return new MultipleValue(values, Enumerable.Empty<CssToken>());
        }

        sealed class MultipleValue : IPropertyValue
        {
            readonly IPropertyValue[] _values;
            readonly CssValue _value;

            public MultipleValue(IPropertyValue[] values, IEnumerable<CssToken> tokens)
            {
                _values = values;
                _value = new CssValue(tokens);
            }

            public String CssText
            {
                get { return String.Join(" ", _values.Where(m => !String.IsNullOrEmpty(m.CssText)).Select(m => m.CssText)); }
            }

            public CssValue Original
            {
                get { return _value; }
            }

            public CssValue ExtractFor(String name)
            {
                var tokens = new List<CssToken>();

                foreach (var value in _values)
                {
                    var extracted = value.ExtractFor(name);

                    if (extracted != null)
                    {
                        if (tokens.Count > 0)
                            tokens.Add(CssToken.Whitespace);

                        tokens.AddRange(extracted);
                    }
                }

                return new CssValue(tokens);
            }
        }
    }
}
