using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    public struct Nest : Element
    {
        private List<Nested> _nested;
        private List<Element> _elements;
        public Element[] elements
        {
            get
            {
                return _elements.ToArray();
            }
        }
        internal Type elementType = typeof(Nest);
        public Nest()
        {
            _nested = new List<Nested>();
            _elements = new List<Element>();
        }
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
        internal string Render()
        {
            StringBuilder result = new StringBuilder();
            foreach (Nested n in _nested)
            {
                Console.WriteLine(n);
            }
            for (int i = 0; i < _nested.Count; i++)
            {
                result.Append(_nested[i].First());
            }
            for (int i = _nested.Count - 1; i >= 0; i--)
            {
                result.Append(_nested[i].Last());
            }
            Console.WriteLine(result.ToString());
            return result.ToString();
        }
    }
}