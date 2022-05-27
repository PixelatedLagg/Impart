using System;
using System.Collections.Generic;

namespace Impart
{
    public class AttrList : List<Attr>
    {
        internal bool Changed = true;
        public AttrList() : base() { }
        new public void Add(Attr attribute)
        {
            Changed = true;
            base.Add(attribute);
        }
        public void Add(AttrType type, params object[] args)
        {
            Changed = true;
            base.Add(new Attr(type, args));
        }
        new public void Clear()
        {
            Changed = true;
            base.Clear();
        }
        new public void AddRange(IEnumerable<Attr> attributes)
        {
            Changed = true;
            base.AddRange(attributes);
        }
        new public void Insert(int index, Attr attribute)
        {
            Changed = true;
            base.Insert(index, attribute);
        }
        new public void InsertRange(int index, IEnumerable<Attr> attributes)
        {
            Changed = true;
            base.InsertRange(index, attributes);
        }
        new public void Remove(Attr attribute)
        {
            Changed = true;
            base.Remove(attribute);
        }
        new public void RemoveAll(Predicate<Attr> attributes)
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
        new public void Sort(Comparison<Attr> comparison)
        {
            Changed = true;
            base.Sort(comparison);
        }
        new public void Sort(IComparer<Attr> comparer)
        {
            Changed = true;
            base.Sort(comparer);
        }
        new public void Sort(int index, int count, IComparer<Attr> comparer)
        {
            Changed = true;
            base.Sort(index, count, comparer);
        }
    }
}