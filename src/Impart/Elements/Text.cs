using System.Text;
using Impart.Internal;
using System.Collections.Generic;

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
        private string _ID;

        /// <value>The ID value of the Text.</value>
        public string ID 
        {
            get 
            {
                return _ID;
            }
            set
            {
                Changed = true;
                _ID = value;
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
        public List<Attr> Attributes = new List<Attr>();
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
        private List<ExtAttr> _ExtAttrs = new List<ExtAttr>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates an empty Text instance.</summary>
        public Text() : this("") {}

        /// <summary>Creates a Text instance.</summary>
        /// <param name="text">The Text text.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, string id = null)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _Text = text;
            _ID = id;
            _Type = TextType.Regular;
            _TextType = "p";
            if (id != null)
            {
                _ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, id));
            }
        }

        /// <summary>Creates a Text instance.</summary>
        /// <param name="text">The Text text.</param>
        /// <param name="type">The Text type.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, TextType type, string id = null)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _Text = text;
            _ID = id;
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
            if (id != null)
            {
                _ExtAttrs.Add(new ExtAttr(ExtAttrType.ID, id));
            }
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder($"<{_TextType}");
            if (Attributes.Count != 0)
            {
                result.Append(" style=\"");
                foreach (Attribute attribute in Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
            }
            foreach (ExtAttr ExtAttr in _ExtAttrs)
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
            result.Attributes = Attributes;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
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
            result.Attributes = Attributes;
            result._ExtAttrs = _ExtAttrs;
            result._ID = _ID;
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