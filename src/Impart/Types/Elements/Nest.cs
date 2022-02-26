using System.Collections.Generic;

namespace Impart
{
    public struct Nest : Element
    {
        private List<Element> _elements = new List<Element>();
        public Element[] elements
        {
            get
            {
                return _elements.ToArray();
            }
        }
        public Nest(params Element[] elements)
        {
            _elements.AddRange(elements);
        }
        public Nest Add(params Element[] elements)
        {
            _elements.AddRange(elements);
            return this;
        }
        public Nest Add(List<Element> elements)
        {
            _elements.AddRange(elements);
            return this;
        }
        internal string Render()
        {
            return "";
        }
    }
}