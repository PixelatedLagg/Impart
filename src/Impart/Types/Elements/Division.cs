using System.Collections.Generic;
using System.Text;
using System;

namespace Impart
{
    /// <summary>Division element.</summary>
    public struct Division : Element, IDisposable
    {
        private string _id;

        /// <value>The ID value of the List.</value>
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        private List<Attribute> _attributes;

        /// <value>The attribute values of the List.</value>
        public List<Attribute> attributes
        {
            get 
            {
                return _attributes;
            }
        }
        private StringBuilder _style;
        internal string style 
        {
            get
            {
                if (_style.Length == 0)
                {
                    return "";
                }
                return $" style=\"{_style.ToString()}\"";
            }
        }
        internal StringBuilder attributeBuilder;
        internal StringBuilder textBuilder;
        internal StringBuilder webPageStyleBuilder;
        internal Type elementType = typeof(Division);

        /// <summary>Creates a Division instance.</summary>
        /// <returns>A Division instance.</returns>
        public Division(string id = null)
        {
            if (id == null)
            {
                attributeBuilder = new StringBuilder();
            }
            else
            {
                attributeBuilder = new StringBuilder($" id=\"{id}\"");
            }
            _id = id;
            _attributes = new List<Attribute>();
            _style = new StringBuilder("overflow-x: auto; overflow-y: auto;");
            textBuilder = new StringBuilder();
            webPageStyleBuilder = new StringBuilder();
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Division instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Division SetAttribute(AttributeType type, params object[] value)
        {
            switch (type)
            {
                case AttributeType.BackgroundColor:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Rgb rgb:
                            _style.Append($" background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                            break;
                        case Hsl hsl:
                            _style.Append($" background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                            break;
                        case Hex hex:
                            _style.Append($" background-color: #{hex.hex};");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    _attributes.Add(new Attribute(AttributeType.BackgroundColor, value[0]));
                    break;
                case AttributeType.ForegroundColor:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Rgb rgb:
                            _style.Append($" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                            break;
                        case Hsl hsl:
                            _style.Append($" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                            break;
                        case Hex hex:
                            _style.Append($" color: #{hex.hex};");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    _attributes.Add(new Attribute(AttributeType.ForegroundColor, value[0]));
                    break;
                case AttributeType.Alignment:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Alignment.Center:
                            _style.Append(" align: center;");
                            break;
                        case Alignment.Justify:
                            _style.Append(" align: justify;");
                            break;
                        case Alignment.Left:
                            _style.Append(" align: left;");
                            break;
                        case Alignment.Right:
                            _style.Append(" align: right;");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    break;
                case AttributeType.FontFamily:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case FontFamily.AndaleMono:
                            _style.Append(" font-family: Andale Mono;");
                            break;
                        case FontFamily.AppleChancery:
                            _style.Append(" font-family: Apple Chancery;");
                            break;
                        case FontFamily.Arial:
                            _style.Append(" font-family: Arial;");
                            break;
                        case FontFamily.AvantaGarde:
                            _style.Append(" font-family: Avanta Garde;");
                            break;
                        case FontFamily.Baskerville:
                            _style.Append(" font-family: Baskerville;");
                            break;
                        case FontFamily.BigCaslon:
                            _style.Append(" font-family: Big Caslon;");
                            break;
                        case FontFamily.BodoniMT:
                            _style.Append(" font-family: Bodoni MT;");
                            break;
                        case FontFamily.BookAntiqua:
                            _style.Append(" font-family: Book Antiqua;");
                            break;
                        case FontFamily.Bookman:
                            _style.Append(" font-family: Bookman;");
                            break;
                        case FontFamily.BradleyHand:
                            _style.Append(" font-family: Bradley Hand;");
                            break;
                        case FontFamily.BrushScriptMT:
                            _style.Append(" font-family: Brush Script MT;");
                            break;
                        case FontFamily.BrushScriptStd:
                            _style.Append(" font-family: Brush Script Std;");
                            break;
                        case FontFamily.Calibri:
                            _style.Append(" font-family: Calibri;");
                            break;
                        case FontFamily.CalistoMT:
                            _style.Append(" font-family: Calisto MT;");
                            break;
                        case FontFamily.Cambria:
                            _style.Append(" font-family: Cambria;");
                            break;
                        case FontFamily.Candara:
                            _style.Append(" font-family: Candara;");
                            break;
                        case FontFamily.CenturyGothic:
                            _style.Append(" font-family: Century Gothic;");
                            break;
                        case FontFamily.ComicSans:
                            _style.Append(" font-family: Comic Sans;");
                            break;
                        case FontFamily.ComicSansMS:
                            _style.Append(" font-family: Comic Sans MS;");
                            break;
                        case FontFamily.Consolas:
                            _style.Append(" font-family: Consolas;");
                            break;
                        case FontFamily.Coronetscript:
                            _style.Append(" font-family: Coronet script;");
                            break;
                        case FontFamily.Courier:
                            _style.Append(" font-family: Courier;");
                            break;
                        case FontFamily.CourierNew:
                            _style.Append(" font-family: Courier New;");
                            break;
                        case FontFamily.Didot:
                            _style.Append(" font-family: Didot;");
                            break;
                        case FontFamily.Florence:
                            _style.Append(" font-family: Florence;");
                            break;
                        case FontFamily.FranklinGothicMedium:
                            _style.Append(" font-family: Franklin Gothic Medium;");
                            break;
                        case FontFamily.Futara:
                            _style.Append(" font-family: Futara;");
                            break;
                        case FontFamily.Garamond:
                            _style.Append(" font-family: Garamond;");
                            break;
                        case FontFamily.Geneva:
                            _style.Append(" font-family: Geneva;");
                            break;
                        case FontFamily.Georgia:
                            _style.Append(" font-family: Georgia;");
                            break;
                        case FontFamily.GillSans:
                            _style.Append(" font-family: Gill Sans;");
                            break;
                        case FontFamily.GoudyOldStyle:
                            _style.Append(" font-family: Goudy Old Style;");
                            break;
                        case FontFamily.Helvetica:
                            _style.Append(" font-family: Helvetica;");
                            break;
                        case FontFamily.HoeflerText:
                            _style.Append(" font-family: Hoefler Text;");
                            break;
                        case FontFamily.LucidaBright:
                            _style.Append(" font-family: Lucida Bright;");
                            break;
                        case FontFamily.LucidaConsole:
                            _style.Append(" font-family: Lucida Console;");
                            break;
                        case FontFamily.LucidaSans:
                            _style.Append(" font-family: Lucida Sans;");
                            break;
                        case FontFamily.LucidaSansTypewriter:
                            _style.Append(" font-family: Lucida Sans Typewriter;");
                            break;
                        case FontFamily.Monaco:
                            _style.Append(" font-family: Monaco;");
                            break;
                        case FontFamily.NewCenturySchoolbook:
                            _style.Append(" font-family: New Century Schoolbook;");
                            break;
                        case FontFamily.Noto:
                            _style.Append(" font-family: Noto;");
                            break;
                        case FontFamily.Optima:
                            _style.Append(" font-family: Optima;");
                            break;
                        case FontFamily.Palatino:
                            _style.Append(" font-family: Palatino;");
                            break;
                        case FontFamily.Parkavenue:
                            _style.Append(" font-family: Parkavenue;");
                            break;
                        case FontFamily.Perpetua:
                            _style.Append(" font-family: Perpetua;");
                            break;
                        case FontFamily.Rockwell:
                            _style.Append(" font-family: Rockwell;");
                            break;
                        case FontFamily.RockwellExtraBold:
                            _style.Append(" font-family: Rockwell Extra Bold;");
                            break;
                        case FontFamily.SegoeUI:
                            _style.Append(" font-family: Segoe UI;");
                            break;
                        case FontFamily.SnellRoundhan:
                            _style.Append(" font-family: Snell Roundhan;");
                            break;
                        case FontFamily.TimesNewRoman:
                            _style.Append(" font-family: Times New Roman;");
                            break;
                        case FontFamily.TrebuchetMS:
                            _style.Append(" font-family: Trebuchet MS;");
                            break;
                        case FontFamily.URWChancery:
                            _style.Append(" font-family: URW Chancery;");
                            break;
                        case FontFamily.Verdana:
                            _style.Append(" font-family: Verdana;");
                            break;
                        case FontFamily.ZapfChancery:
                            _style.Append(" font-family: Zapf Chancery;");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    _attributes.Add(new Attribute(AttributeType.FontFamily, value[0]));
                    break;
                case AttributeType.FontSize:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            _style.Append($" font-size: {pct}%;");
                            break;
                        case Pixels pxls:
                            _style.Append($" font-size: {pxls}px;");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    _attributes.Add(new Attribute(AttributeType.FontFamily, value[0]));
                    break;
                case AttributeType.Margin:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            _style.Append($" margin: {pct}%;");
                            break;
                        case Pixels pxls:
                            _style.Append($" margin: {pxls}px;");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    _attributes.Add(new Attribute(AttributeType.Margin, value[0]));
                    break;
                case AttributeType.Padding:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            _style.Append($" padding: {pct}%;");
                            break;
                        case Pixels pxls:
                            _style.Append($" padding: {pxls}px;");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    _attributes.Add(new Attribute(AttributeType.Padding, value[0]));
                    break;
                case AttributeType.HoverMessage:
                    if (value.Length != 1)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    if (value[0] as string != null)
                    {
                        attributeBuilder.Append($" title=\"{value[0]}\"");
                        _attributes.Add(new Attribute(AttributeType.HoverMessage, value[0]));
                    }
                    else
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    break;
                case AttributeType.Size:
                    if (value.Length != 2)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            _style.Append($" width: {pct}%;");
                            break;
                        case Pixels pxls:
                            _style.Append($" width: {pxls}px;");
                            break;
                        case null:
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[1])
                    {
                        case Percent pct:
                            _style.Append($" height: {pct}%;");
                            break;
                        case Pixels pxls:
                            _style.Append($" height: {pxls}px;");
                            break;
                        case null:
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    _attributes.Add(new Attribute(AttributeType.Size, value[0], value[1]));
                    break;
                case AttributeType.Border:
                    if (value.Length != 3)
                    {
                        throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[0])
                    {
                        case Percent pct:
                            _style.Append($" border: {pct}%");
                            break;
                        case Pixels pxls:
                            _style.Append($" border: {pxls}px");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[1])
                    {
                        case Border.Dashed:
                            _style.Append($" dashed");
                            break;
                        case Border.Dotted:
                            _style.Append($" dotted");
                            break;
                        case Border.Double:
                            _style.Append($" double");
                            break;
                        case Border.In3D:
                            _style.Append($" inset");
                            break;
                        case Border.Normal:
                            _style.Append($" solid");
                            break;
                        case Border.Out3D:
                            _style.Append($" outset");
                            break;
                        default:
                            throw new ImpartError("Invalid attribute parameters.");
                    }
                    switch (value[2])
                    {
                        case Rgb rgb:
                            _style.Append($" rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                            break;
                        case Hsl hsl:
                            _style.Append($" hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                            break;
                        case Hex hex:
                            _style.Append($" #{hex.hex};");
                            break;
                    }
                    _attributes.Add(new Attribute(AttributeType.Border, value[0], value[1], value[2]));
                    break;
                default:
                    throw new ImpartError("Invalid attribute parameters.");
            }
            return this;
        }

        /// <summary>Add <paramref name="scrollbar"> to the Division.</summary>
        /// <returns>A Division instance.</returns>
        /// <param name="scrollbar">The scrollbar to add.</param>
        public Division AddScrollbar(Scrollbar scrollbar)
        {
            if (id == null)
            {
                throw new ImpartError("To add scrollbar, division must have an ID set.");
            }
            switch (scrollbar.axis)
            {
                case Axis.X:
                    _style.Append("overflow-x: auto;");
                    break;
                case Axis.Y:
                    _style.Append("overflow-y: auto;");
                    break;
                default:
                    throw new ImpartError("Invalid axis!");
            }
            switch (scrollbar.width)
            {
                case Percent pct:
                    webPageStyleBuilder.Append($"{this.id}::-webkit-scrollbar {{width: {scrollbar.width}%;background-color: #808080; }}{this.id}::-webkit-scrollbar-track {{");
                    break;
                case Pixels pxls:
                    webPageStyleBuilder.Append($"{this.id}::-webkit-scrollbar {{width: {scrollbar.width}px;background-color: #808080; }}{this.id}::-webkit-scrollbar-track {{");
                    break;
            }
            switch (scrollbar.bgColor)
            {
                case Rgb rgb:
                    webPageStyleBuilder.Append($"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});}}");
                    break;
                case Hsl hsl:
                    webPageStyleBuilder.Append($"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);}}");
                    break;
                case Hex hex:
                    webPageStyleBuilder.Append($"background-color: #{hex.hex};}}");
                    break;
            }
            webPageStyleBuilder.Append($"{this.id}::-webkit-scrollbar-thumb {{");
            switch (scrollbar.fgColor)
            {
                case Rgb rgb:
                    webPageStyleBuilder.Append($"background-color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});");
                    break;
                case Hsl hsl:
                    webPageStyleBuilder.Append($"background-color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);");
                    break;
                case Hex hex:
                    webPageStyleBuilder.Append($"background-color: #{hex.hex};");
                    break;
            }
            if (scrollbar.radius != null)
            {
                switch (scrollbar.radius)
                {
                    case Percent pct:
                        webPageStyleBuilder.Append($"border-radius: {scrollbar.radius}%;}}");
                        break;
                    case Pixels pxls:
                        webPageStyleBuilder.Append($"border-radius: {scrollbar.radius}px;}}");
                        break;
                }
            }
            else
            {
                webPageStyleBuilder.Append("}");
            }
            return this;
        }

        /// <summary>Dispose of the Division instance.</summary>
        public void Dispose()
        {
            //only implemented to allow easy scope management for the dev
        }
        internal string Render()
        {
            return $"<div{attributeBuilder.ToString()}{style}>{textBuilder.ToString()}</div>";
        }
    } 
}