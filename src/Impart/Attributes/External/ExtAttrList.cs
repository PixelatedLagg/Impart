using System;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Store a list of ExtAttrs.</summary>
    public class ExtAttrList : List<ExtAttr>
    {
        internal bool Changed = true;

        /// <summary>Creates an ExtAttrList instance.</summary>
        public ExtAttrList() : base() { }

        /// <summary>Return an ExtAttr from the ExtAttrList based on the ExtAttrType.</summary>
        public ExtAttr this[ExtAttrType type]
        {
            get
            {
                foreach (ExtAttr extattr in this)
                {
                    if (extattr.Type == type)
                    {
                        return extattr;
                    }
                }
                return default;
            }
        }
        /// <summary>Add an ExtAttr to the ExtAttrList.</summary>
        /// <param name="attribute">The ExtAttr to add.</param>
        new public void Add(ExtAttr attribute)
        {
            Changed = true;
            base.Add(attribute);
        }

        /// <summary>Add an ExtAttr to the ExtAttrList.</summary>
        /// <param name="type">The ExtAttrType to add.</param>
        /// <param name="value">The ExtAttr value.</param>
        public void Add(ExtAttrType type, string value)
        {
            Changed = true;
            base.Add(new ExtAttr(type, value));
        }

        /// <summary>Remove all ExtAttrs from the ExtAttrList.</summary>
        new public void Clear()
        {
            Changed = true;
            base.Clear();
        }

        /// <summary>Add multiple ExtAttrs to the ExtAttrList.</summary>
        /// <param name="attributes">The ExtAttrs to add.</param>
        new public void AddRange(IEnumerable<ExtAttr> attributes)
        {
            Changed = true;
            base.AddRange(attributes);
        }

        /// <summary>Add an ExtAttr to the ExtAttrList at the specified index.</summary>
        /// <param name="index">The index to add the ExtAttr at.</param>
        /// <param name="attribute">The ExtAttr to add.</param>
        new public void Insert(int index, ExtAttr attribute)
        {
            Changed = true;
            base.Insert(index, attribute);
        }

        /// <summary>Add multiple ExtAttrs to the ExtAttrList at the specified index.</summary>
        /// <param name="index">The index to add the ExtAttrs at.</param>
        /// <param name="attributes">The ExtAttrs to add.</param>
        new public void InsertRange(int index, IEnumerable<ExtAttr> attributes)
        {
            Changed = true;
            base.InsertRange(index, attributes);
        }

        /// <summary>Remove an ExtAttr from the ExtAttrList.</summary>
        /// <param name="attribute">The ExtAttr to remove.</param>
        new public void Remove(ExtAttr attribute)
        {
            Changed = true;
            base.Remove(attribute);
        }

        /// <summary>Remove all ExtAttrs that meet specific conditions from the ExtAttrList.</summary>
        /// <param name="conditions">The ExtAttr conditions.</param>
        new public void RemoveAll(Predicate<ExtAttr> conditions)
        {
            Changed = true;
            base.RemoveAll(conditions);
        }

        /// <summary>Remove an ExtAttr from the ExtAttrList at the specified index.</summary>
        /// <param name="index">The index to remove the ExtAttr at.</param>
        new public void RemoveAt(int index)
        {
            Changed = true;
            base.RemoveAt(index);
        }

        /// <summary>Remove multiple ExtAttrs from the ExtAttrList at the specified index.</summary>
        /// <param name="index">The index to remove the ExtAttrs at.</param>
        /// <param name="count">The number of ExtAttrs to remove after the index.</param>
        new public void RemoveRange(int index, int count)
        {
            Changed = true;
            base.RemoveRange(index, count);
        }
    }
}