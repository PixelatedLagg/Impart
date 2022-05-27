using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    /// <summary>Style Elements with a certain ID.</summary>
    public sealed class Style : IDisposable, StyleElement
    {
        private string _ID;

        /// <value>The ID value of the Style.</value>
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

        /// <value>The attribute values of the Style.</value>
        public AttrList Attrs = new AttrList();
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int StyleElement.IOID
        {
            get
            {
                return _IOID;
            }
        }
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates a Style instance.</summary>
        /// <param name="id">The ID.</param>
        public Style(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ImpartError("ID cannot be null!");
            }
            _ID = id;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            StringBuilder result = new StringBuilder($"{_ID} {{");
            foreach (Attr attribute in Attrs)
            {
                result.Append(attribute);
            }
            Render = result.Append('}').ToString();
            return Render;
        }

        /// <summary>Dispose of the Style instance.</summary>
        public void Dispose() { }
    }
}