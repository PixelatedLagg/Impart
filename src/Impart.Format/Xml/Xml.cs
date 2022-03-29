using System.Text;
using System.Collections.Generic;

namespace Impart.Format
{
    public struct Xml
    {
        private int _length;
        public int length
        {
            get
            {
                return _length;
            }
        }
        private StringBuilder builder;
        private List<XmlSet> sets;
        private List<XmlArray> arrays;
        private string title;
        public Xml(string title, params (object, object)[] entries)
        {
            this.title = title;
            builder = new StringBuilder();
            _length = 0;
            sets = new List<XmlSet>();
            arrays = new List<XmlArray>();
            foreach ((object, object) entry in entries)
            {
                sets.Add(new XmlSet(entry.Item1, entry.Item2));
                _length++;
            }
        }
        public Xml AddSet(object key, object value)
        {
            sets.Add(new XmlSet(key, value));
            _length++;
            return this;
        }
        public Xml AddSets(params (object, object)[] entries)
        {
            foreach ((object, object) entry in entries)
            {
                sets.Add(new XmlSet(entry.Item1, entry.Item2));
                _length++;
            }
            return this;
        }
        public Xml AddArray(XmlArray array)
        {
            arrays.Add(array);
            _length++;
            return this;
        }
        public Xml AddArrays(params XmlArray[] arrays)
        {
            foreach (XmlArray array in arrays)
            {
                this.arrays.Add(array);
            }
            _length++;
            return this;
        }
        public Xml RemoveSet(object key)
        {
            foreach (XmlSet set in sets)
            {
                if (set.key == key)
                {
                    sets.Remove(set);
                }
            }
            return this;
        }
        public Xml RemoveSets(params object[] keys)
        {
            foreach (object key in keys)
            {
                foreach (XmlSet set in sets)
                {
                    if (set.key == key)
                    {
                        sets.Remove(set);
                    }
                }
            }
            return this;
        }
        public Xml RemoveArray(object key)
        {
            foreach (XmlArray array in arrays)
            {
                if (array.key == key)
                {
                    arrays.Remove(array);
                }
            }
            return this;
        }
        public Xml RemoveArrays(params object[] keys)
        {
            foreach (object key in keys)
            {
                foreach (XmlArray array in arrays)
                {
                    if (array.key == key)
                    {
                        arrays.Remove(array);
                    }
                }
            }
            return this;
        }
        public XmlSet? GetSet(object key)
        {
            foreach (XmlSet set in sets)
            {
                if (set.key == key)
                {
                    return set;
                }
            }
            return null;
        }
        public XmlArray? GetArray(object key)
        {
            foreach (XmlArray array in arrays)
            {
                if (array.key == key)
                {
                    return array;
                }
            }
            return null;
        }
        internal string Render()
        {
            foreach (XmlSet set in sets)
            {
                builder.Append($"<{set.key}>{set.value}</{set.key}>");
            }
            foreach (XmlArray array in arrays)
            {
                builder.Append(array.Render());
            }
            return $"<{title}>{builder.ToString()}</{title}>";
        }
    }
}