using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>A nest of IElements.</summary>
    public class Nest : IElement
    {
        private List<IElement> _Elements = new List<IElement>();

        /// <summary>All of the IElements included in the Nest.</summary>
        public IElement[] IElement
        {
            get
            {
                return _Elements.ToArray();
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
        
        private List<INested> _Nested = new List<INested>();

        /// <summary>The ExtAttr values of the instance (will always return null, as this is a group of IElements).</summary>
        ExtAttrList IElement.ExtAttrs
        {
            get
            {
                return null;
            }
        }

        internal int _IOID = Ioid.Generate();

        /// <summary>Creates an empty Nest instance.</summary>
        public Nest() { }

        /// <summary>Creates a Nest instance.</summary>
        /// <param name="elements">The IElements to add to the Nest.</param>
        public Nest(params IElement[] elements)
        {
            _Elements.AddRange(elements);
            INested current;
            foreach (IElement e in elements)
            {
                current = e as INested;
                if (current == null)
                {
                    throw new ImpartError("Cannot add this element to a nest!");
                }
                else
                {
                    _Nested.Add(current);
                }
            }
        }

        /// <summary>Adds IElement(s) to the Nest.</summary>
        /// <param name="elements">The IElements to add to the Nest.</param>
        public Nest Add(params IElement[] elements)
        {
            _Elements.AddRange(elements);
            INested current;
            foreach (IElement e in elements)
            {
                current = e as INested;
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

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        public IElement Clone()
        {
            Nest result = new Nest();
            result._Elements = _Elements;
            result._IOID = _IOID;
            result._Nested = _Nested;
            return result;
        }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        public ElementRef Reference() => new ElementRef(_IOID);

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef IElement.Reference() => new ElementRef(_IOID);

        /// <summary>Clones the IElement instance (including the internal ID).</summary>
        IElement IElement.Clone()
        {
            Nest result = new Nest();
            result._Elements = _Elements;
            result._IOID = _IOID;
            result._Nested = _Nested;
            return result;
        }
    }
}