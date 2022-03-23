using System;
using System.Text;
using System.Collections.Generic;

namespace Impart.API
{
    public struct Json : Format
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
        private List<JsonSet> sets;
        private List<JsonArray> arrays;
        private string title;
        public Json(string title, params (object, object)[] entries)
        {
            this.title = title;
            builder = new StringBuilder();
            _length = 0;
            sets = new List<JsonSet>();
            arrays = new List<JsonArray>();
            foreach ((object, object) entry in entries)
            {
                sets.Add(new JsonSet(entry.Item1, entry.Item2));
                _length++;
            }
        }
        public Json AddSet(object key, object value)
        {
            sets.Add(new JsonSet(key, value));
            _length++;
            return this;
        }
        public Json AddSets(params (object, object)[] entries)
        {
            foreach ((object, object) entry in entries)
            {
                sets.Add(new JsonSet(entry.Item1, entry.Item2));
                _length++;
            }
            return this;
        }
        public Json AddArray(JsonArray array)
        {
            arrays.Add(array);
            _length++;
            return this;
        }
        public Json AddArrays(params JsonArray[] arrays)
        {
            foreach (JsonArray array in arrays)
            {
                this.arrays.Add(array);
            }
            _length++;
            return this;
        }
        public Json RemoveSet(object key)
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
        public Json RemoveSets(params object[] keys)
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
        public Json RemoveArray(object key)
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
        public Json RemoveArrays(params object[] keys)
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
                if (builder.Length == 0)
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
                if (builder.Length == 0)
                {
                    builder.Append(array.Render());
                }
                else
                {
                    builder.Append($", {array.Render()}");
                }
            }
            return $"{{ \"{title}\" : {{ {builder.ToString()} }} }}";
        }
    }
}