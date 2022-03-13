using System.Text;
using System.Collections.Generic;

namespace Impart.API
{
    public struct JsonArray
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
        private List<JsonSet> sets;
        private List<JsonArray> arrays;
        public JsonArray(object key, params (object, object)[] sets)
        {
            _key = key;
            _length = 0;
            builder = new StringBuilder();
            this.sets = new List<JsonSet>();
            arrays = new List<JsonArray>();
            foreach ((object, object) set in sets)
            {
                this.sets.Add(new JsonSet(set.Item1, set.Item2));
                _length++;
            }
        }
        public JsonArray AddSet(object key, object value)
        {
            sets.Add(new JsonSet(key, value));
            _length++;
            return this;
        }
        public JsonArray AddSets(params (object, object)[] entries)
        {
            foreach ((object, object) entry in entries)
            {
                sets.Add(new JsonSet(entry.Item1, entry.Item2));
                _length++;
            }
            return this;
        }
        public JsonArray AddArray(JsonArray array)
        {
            arrays.Add(array);
            _length++;
            return this;
        }
        public JsonArray AddArrays(params JsonArray[] arrays)
        {
            foreach (JsonArray array in arrays)
            {
                this.arrays.Add(array);
            }
            _length++;
            return this;
        }
        public JsonArray RemoveSet(object key)
        {
            foreach (JsonSet set in sets)
            {
                if (set.key == key)
                {
                    sets.Remove(set);
                }
            }
            return this;
        }
        public JsonArray RemoveSets(params object[] keys)
        {
            foreach (object key in keys)
            {
                foreach (JsonSet set in sets)
                {
                    if (set.key == key)
                    {
                        sets.Remove(set);
                    }
                }
            }
            return this;
        }
        public JsonArray RemoveArray(object key)
        {
            foreach (JsonArray array in arrays)
            {
                if (array.key == key)
                {
                    arrays.Remove(array);
                }
            }
            return this;
        }
        public JsonArray RemoveArrays(params object[] keys)
        {
            foreach (object key in keys)
            {
                foreach (JsonArray array in arrays)
                {
                    if (array.key == key)
                    {
                        arrays.Remove(array);
                    }
                }
            }
            return this;
        }
        public JsonSet? GetSet(object key)
        {
            foreach (JsonSet set in sets)
            {
                if (set.key == key)
                {
                    return set;
                }
            }
            return null;
        }
        public JsonArray? GetArray(object key)
        {
            foreach (JsonArray array in arrays)
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
            foreach (JsonSet set in sets)
            {
                if (_length == 0)
                {
                    builder.Append($"\"{set.key}\" : \"{set.value}\"");
                }
                else
                {
                    builder.Append($", \"{set.key}\" : \"{set.value}\"");
                }
            }
            foreach (JsonArray array in arrays)
            {
                if (_length == 0)
                {
                    builder.Append(array.Render());
                }
                else
                {
                    string str = array.Render();
                    builder.Append($", {str}");
                }
            }
            return $"\"{_key}\" : {{ {builder.ToString()} }}";
        }
    }
}