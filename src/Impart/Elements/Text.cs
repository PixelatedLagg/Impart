using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Text element.</summary>
    public struct Text : Element, Nested
    {
        private string _Text;

        /// <value>The text value of the Text.</value>
        public string TextValue
        {
            get 
            {
                return _Text;
            }
            set
            {
                Changed = true;
                _Text = value;
            }
        }
        private TextType _Type;

        /// <value>The type value of the Text.</value>
        public TextType Type
        {
            get
            {
                return _Type;
            }
        }

        /// <value>The Attribute values of the Text.</value>
        public AttrList Attrs = new AttrList();
        public ExtAttrList ExtAttrs = new ExtAttrList();
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int Element.IOID
        {
            get
            {
                return _IOID;
            }
        }
        private string _TextType;
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates an empty Text instance.</summary>
        public Text() : this("") {}

        /// <summary>Creates a Text instance.</summary>
        /// <param name="text">The Text text.</param>
        public Text(string text)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _Text = text;
            _Type = TextType.Regular;
            _TextType = "p";
        }

        /// <summary>Creates a Text instance.</summary>
        /// <param name="text">The Text text.</param>
        /// <param name="type">The Text type.</param>
        public Text(string text, TextType type)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _Text = text;
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
            StringBuilder result = new StringBuilder($"<{_TextType}");
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
            Render = result.Append($">{_Text}</{_TextType}>").ToString();
            return Render;
        }

        /// <summary>Convert the String instance to a Text.</summary>
        /// <param name="text">The String to convert.</param>
        public static implicit operator Text(string text)
        {
            return new Text(text);
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        Element Element.Clone()
        {
            Text result = new Text();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Text = _Text;
            result._TextType = _TextType;
            result._Type = _Type;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        /// <summary>Clones the Element instance (including the internal ID).</summary>
        public Element Clone()
        {
            Text result = new Text();
            result.Attrs = Attrs;
            result.ExtAttrs = ExtAttrs;
            result._IOID = _IOID;
            result._Text = _Text;
            result._TextType = _TextType;
            result._Type = _Type;
            result.Changed = Changed;
            result.Render = Render;
            return result;
        }

        string Nested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - ($"</{_TextType}>".Length));
        }
        
        string Nested.Last()
        {
            return $"</{_TextType}>";
        }
    }
}