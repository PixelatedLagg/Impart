using System;
using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>A nest of Elements.</summary>
    public struct Nest : Element
    {
        private List<Element> _Elements = new List<Element>();

        /// <value>All of the Elements included in the Nest.</value>
        public Element[] Elements
        {
            get
            {
                return _Elements.ToArray();
            }
        }
        private List<Nested> _Nested = new List<Nested>();
        string Element.ID
        {
            get
            {
                return null;
            }
            set { }
        }
        private int IOIDValue = Ioid.Generate();
        int Element.IOID
        {
            get
            {
                return IOIDValue;
            }
        }

        /// <summary>Creates an empty Nest instance.</summary>
        public Nest() { }

        /// <summary>Creates a Nest instance.</summary>
        /// <param name="elements">The Elements to add to the Nest.</param>
        public Nest(params Element[] elements)
        {
            _Elements.AddRange(elements);
            Nested current;
            foreach (Element e in elements)
            {
                current = e as Nested;
                if (current != null)
                {
                    throw new ImpartError("Cannot add this element to a nest!");
                }
                else
                {
                    _Nested.Add(current);
                }
            }
        }

        /// <summary>Adds <paramref name="elements"> to the Nest.</summary>
        /// <param name="elements">The Elements to add to the Nest.</param>
        public Nest Add(params Element[] elements)
        {
            _Elements.AddRange(elements);
            Nested current;
            foreach (Element e in elements)
            {
                current = e as Nested;
                if (current == null)
                {
                    throw new ImpartError("Cannot add this element to a nest!");
                }
                else
                {
                    _Nested.Add(current);
                }
            }
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _Nested.Count; i++)
            {
                result.Append(_Nested[i].First());
            }
            for (int i = _Nested.Count - 1; i >= 0; i--)
            {
                result.Append(_Nested[i].Last());
            }
            return result.ToString();
        }
    }
}