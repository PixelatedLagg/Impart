using System.Text;
using System.Collections.Generic;

namespace Impart.API
{
    public struct XmlArray
    {
        private int _length;
        public int length
        {
            get
            {
                return _length;
            }
        }

        private object _key;
        public object key
        {
            get
            {
                return _key;
            }
        }
        private StringBuilder builder;
        private List<XmlSet> sets;
        private List<XmlArray> arrays;
        public XmlArray(object key, params (object, object)[] sets)
        {
            _key = key;
            builder = new StringBuilder();
            _length = 0;
            this.sets = new List<XmlSet>();
            arrays = new List<XmlArray>();
            foreach ((object, object) set in sets)
            {
                builder.Append($"<{set.Item1}>{set.Item2}</{set.Item1}>");
                _length++;
            }
        }
        public XmlArray AddSet(object key, object value)
        {
            sets.Add(new XmlSet(key, value));
            _length++;
            return this;
        }
        public XmlArray AddSets(params (object, object)[] entries)
        {
            foreach ((object, object) entry in entries)
            {
                sets.Add(new XmlSet(entry.Item1, entry.Item2));
                _length++;
            }
            return this;
        }
        public XmlArray AddArray(XmlArray array)
        {
            arrays.Add(array);
            _length++;
            return this;
        }
        public XmlArray AddArrays(params XmlArray[] arrays)
        {
            foreach (XmlArray array in arrays)
            {
                this.arrays.Add(array);
            }
            _length++;
            return this;
        }
        public XmlArray RemoveSet(object key)
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
        public XmlArray RemoveSets(params object[] keys)
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
        public XmlArray RemoveArray(object key)
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
        public XmlArray RemoveArrays(params object[] keys)
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
            return $"<{_key}>{builder.ToString()}</{_key}>";
        }
    }
}