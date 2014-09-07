﻿namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS list-style-type property.
    /// </summary>
    public interface ICssListStyleTypeProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected style for the list.
        /// </summary>
        ListStyle Style { get; }
    }

    /// <summary>
    /// Represents the CSS list-style-position property.
    /// </summary>
    public interface ICssListStylePositionProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected position.
        /// </summary>
        ListPosition Position { get; }
    }

    /// <summary>
    /// Represents the CSS list-style-image property.
    /// </summary>
    public interface ICssListStyleImageProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS list-style shorthand property.
    /// </summary>
    public interface ICssListStyleProperty : ICssProperty, ICssListStyleImageProperty, ICssListStylePositionProperty, ICssListStyleTypeProperty
    {
    }
}