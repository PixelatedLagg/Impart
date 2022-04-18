using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>A nest of Elements.</summary>
    public struct Nest : Element
    {
        private List<Element> _elements;

        /// <value>All of the Elements included in the Nest.</value>
        public Element[] elements
        {
            get
            {
                return _elements.ToArray();
            }
        }
        internal Type elementType = typeof(Nest);
        private List<Nested> _nested;

        /// <summary>Creates an empty Nest instance.</summary>
        public Nest()
        {
            _nested = new List<Nested>();
            _elements = new List<Element>();
        }

        /// <summary>Creates a Nest instance.</summary>
        /// <param name="elements">The Elements to add to the Nest.</param>
        public Nest(params Element[] elements)
        {
            _nested = new List<Nested>();
            _elements = new List<Element>();
            _elements.AddRange(elements);
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
                    _nested.Add(current);
                }
            }
        }

        /// <summary>Adds <paramref name="elements"> to the Nest.</summary>
        /// <param name="elements">The Elements to add to the Nest.</param>
        public Nest Add(params Element[] elements)
        {
            _elements.AddRange(elements);
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
                    _nested.Add(current);
                }
            }
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _nested.Count; i++)
            {
                result.Append(_nested[i].First());
            }
            for (int i = _nested.Count - 1; i >= 0; i--)
            {
                result.Append(_nested[i].Last());
            }
            return result.ToString();
        }
    }
}