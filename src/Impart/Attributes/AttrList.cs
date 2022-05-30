using System;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Store a list of Attrs.</summary>
    public class AttrList : List<Attr>
    {
        internal bool Changed = true;

        /// <summary>Creates an AttrList instance.</summary>
        public AttrList() : base() { }

        /// <summary>Return an Attr from the AttrList based on the AttrType.</summary>
        public Attr this[AttrType type]
        {
            get
            {
                foreach (Attr attr in this)
                {
                    if (attr.Type == type)
                    {
                        return attr;
                    }
                }
                return default;
            }
        }

        /// <summary>Add an Attr to the AttrList.</summary>
        /// <param name="attribute">The Attr to add.</param>
        new public void Add(Attr attribute)
        {
            Changed = true;
            base.Add(attribute);
        }

        /// <summary>Add an Attr to the AttrList.</summary>
        /// <param name="type">The AttrType to add.</param>
        /// <param name="args">The Attr value(s).</param>
        public void Add(AttrType type, params object[] args)
        {
            Changed = true;
            base.Add(new Attr(type, args));
        }

        /// <summary>Remove all Attrs from the AttrList.</summary>
        new public void Clear()
        {
            Changed = true;
            base.Clear();
        }

        /// <summary>Add multiple Attrs to the AttrList.</summary>
        /// <param name="attributes">The Attrs to add.</param>
        new public void AddRange(IEnumerable<Attr> attributes)
        {
            Changed = true;
            base.AddRange(attributes);
        }

        /// <summary>Add an Attr to the AttrList at the specified index.</summary>
        /// <param name="index">The index to add the Attr at.</param>
        /// <param name="attribute">The Attr to add.</param>
        new public void Insert(int index, Attr attribute)
        {
            Changed = true;
            base.Insert(index, attribute);
        }

        /// <summary>Add multiple Attrs to the AttrList at the specified index.</summary>
        /// <param name="index">The index to add the Attrs at.</param>
        /// <param name="attributes">The Attrs to add.</param>
        new public void InsertRange(int index, IEnumerable<Attr> attributes)
        {
            Changed = true;
            base.InsertRange(index, attributes);
        }

        /// <summary>Remove an Attr from the AttrList.</summary>
        /// <param name="attribute">The Attr to remove.</param>
        new public void Remove(Attr attribute)
        {
            Changed = true;
            base.Remove(attribute);
        }

        /// <summary>Remove all Attrs that meet specific conditions from the AttrList.</summary>
        /// <param name="conditions">The Attr conditions.</param>
        new public void RemoveAll(Predicate<Attr> conditions)
        {
            Changed = true;
            base.RemoveAll(conditions);
        }

        /// <summary>Remove an Attr from the AttrList at the specified index.</summary>
        /// <param name="index">The index to remove the Attr at.</param>
        new public void RemoveAt(int index)
        {
            Changed = true;
            base.RemoveAt(index);
        }

        /// <summary>Remove multiple Attrs from the AttrList at the specified index.</summary>
        /// <param name="index">The index to remove the Attrs at.</param>
        /// <param name="count">The number of Attrs to remove after the index.</param>
        new public void RemoveRange(int index, int count)
        {
            Changed = true;
            base.RemoveRange(index, count);
        }
    }
}