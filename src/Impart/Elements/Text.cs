using System.Text;
using Impart.Internal;
using Impart.Scripting;

namespace Impart
{
    /// <summary>Text element.</summary>
    public partial class Text : IElement, INested
    {
        /// <summary>The ID value of the instance. Returns null if ID is not set.</summary>
        public string ID
        {
            get
            {
                return ExtAttrs[ExtAttrType.ID]?.Value ?? null;
            }
        }
        private string _TextValue;

        /// <summary>The text value of the Text.</summary>
        public string TextValue
        {
            get 
            {
                return _TextValue;
            }
            set
            {
                Changed = true;
                _TextValue = value;
            }
        }
        private TextType _Type;

        /// <summary>The TextType value of the Text.</summary>
        public TextType Type
        {
            get
            {
                return _Type;
            }
        }

        /// <summary>The Attr values of the instance.</summary>
        public AttrList Attrs = new AttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        public ExtAttrList ExtAttrs = new ExtAttrList();

        /// <summary>The ExtAttr values of the instance.</summary>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return ExtAttrs;
            }
        }

        /// <summary>The internal ID of the instance.</summary>
        int IElement.IOID
        {
            get
            {
                return _IOID;
            }
        }

        internal int _IOID;
        internal EventManager _Events = new EventManager();
        internal bool Changed = true;
        internal string _TextType;
        private string Render = "";

        /// <summary>Creates an empty Text instance.</summary>
        public Text() : this("") {}

        /// <summary>Creates a Text instance.</summary>
        /// <param name="text">The Text text.</param>
        public Text(string text)
        {
            _TextValue = text;
            _Type = TextType.Regular;
            _TextType = "p";
            _IOID = Ioid.Generate();
        }

        /// <summary>Creates a Text instance.</summary>
        /// <param name="text">The Text text.</param>
        /// <param name="type">The Text type.</param>
        public Text(string text, TextType type)
        {
            _TextValue = text;
            _Type = type;
            _TextType = type switch
            {
                TextType.Regular => "p",
                TextType.Bold => "b",
                TextType.Delete => "del",
                TextType.Emphasize => "em",
                TextType.Important => "strong",
                TextType.Insert => "ins",
                TextType.Italic => "i",
                TextType.Mark => "mark",
                TextType.Small => "small",
                TextType.Subscript => "sub",
                TextType.Superscript => "sup",
                _ => ""
            };
            _IOID = Ioid.Generate();
        }

        internal Text(string text, int ioid)
        {
            _TextValue = text;
            _Type = TextType.Regular;
            _TextType = "p";
            _IOID = ioid;
        }

        internal Text(string text, TextType type, int ioid)
        {
            _TextValue = text;
            _Type = type;
            _TextType = type switch
            {
                TextType.Regular => "p",
                TextType.Bold => "b",
                TextType.Delete => "del",
                TextType.Emphasize => "em",
                TextType.Important => "strong",
                TextType.Insert => "ins",
                TextType.Italic => "i",
                TextType.Mark => "mark",
                TextType.Small => "small",
                TextType.Subscript => "sub",
                TextType.Superscript => "sup",
                _ => ""
            };
            _IOID = ioid;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed && !Attrs.Changed && !ExtAttrs.Changed)
            {
                return Render;
            }
            Changed = false;
            Attrs.Changed = false;
            ExtAttrs.Changed = false;
            StringBuilder result = new StringBuilder($"<{_TextType} class=\"{_IOID}\"{_Events}");
            if (Attrs.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attr attribute in Attrs)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in ExtAttrs)
            {
                result.Append(ExtAttr);
            }
            Render = result.Append($">{_TextValue}</{_TextType}>").ToString();
            return Render;
        }

        /// <summary>Convert the String instance to a Text.</summary>
        /// <param name="text">The String to convert.</param>
        public static implicit operator Text(string text)
        {
            return new Text(text);
        }

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Text result = new Text();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._TextValue = _TextValue;
            result._TextType = _TextType;
            result._Type = _Type;
            result.Render = Render;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);
        
        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Text result = new Text();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._TextValue = _TextValue;
            result._TextType = _TextType;
            result._Type = _Type;
            result.Render = Render;
            return result;
        }

        /// <summary>Return the first part of the INested as a string.</summary>
        string INested.First()
        {
            return ToString().Remove(Render.Length - $"</{_TextType}>".Length);
        }
        
        /// <summary>Return the last part of the INested as a string.</summary>
        string INested.Last()
        {
            return $"</{_TextType}>";
        }
    }
}