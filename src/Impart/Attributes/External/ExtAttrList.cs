using System;
using System.Collections.Generic;

namespace Impart
{
    public class ExtAttrList : List<ExtAttr>
    {
        internal bool Changed = true;
        public ExtAttrList() : base() { }
        public object this[ExtAttrType type]
        {
            get
            {
                foreach (ExtAttr extattr in this)
                {
                    if (extattr.Type == type)
                    {
                        return extattr.Value;
                    }
                }
                return null;
            }
        }
        new public void Add(ExtAttr attribute)
        {
            Changed = true;
            base.Add(attribute);
        }
        public void Add(ExtAttrType type, params object[] args)
        {
            Changed = true;
            base.Add(new ExtAttr(type, args));
        }
        new public void Clear()
        {
            Changed = true;
            base.Clear();
        }
        new public void AddRange(IEnumerable<ExtAttr> attributes)
        {
            Changed = true;
            base.AddRange(attributes);
        }
        new public void Insert(int index, ExtAttr attribute)
        {
            Changed = true;
            base.Insert(index, attribute);
        }
        new public void InsertRange(int index, IEnumerable<ExtAttr> attributes)
        {
            Changed = true;
            base.InsertRange(index, attributes);
        }
        new public void Remove(ExtAttr attribute)
        {
            Changed = true;
            base.Remove(attribute);
        }
        new public void RemoveAll(Predicate<ExtAttr> attributes)
        {
            Changed = true;
            base.RemoveAll(attributes);
        }
        new public void RemoveAt(int index)
        {
            Changed = true;
            base.RemoveAt(index);
        }
        new public void RemoveRange(int index, int count)
        {
            Changed = true;
            base.RemoveRange(index, count);
        }
        new public void Reverse()
        {
            Changed = true;
            base.Reverse();
        }
        new public void Reverse(int index, int count)
        {
            Changed = true;
            base.Reverse(index, count);
        }
        new public void Sort()
        {
            Changed = true;
            base.Sort();
        }
        new public void Sort(Comparison<ExtAttr> comparison)
        {
            Changed = true;
            base.Sort(comparison);
        }
        new public void Sort(IComparer<ExtAttr> comparer)
        {
            Changed = true;
            base.Sort(comparer);
        }
        new public void Sort(int index, int count, IComparer<ExtAttr> comparer)
        {
            Changed = true;
            base.Sort(index, count, comparer);
        }
    }
}