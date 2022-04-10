using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Impart.Format
{
    public class JsonObject : Dictionary<string, JsonValue>
    {
        public new JsonValue this[string key]
		{
			get
            {
                return base[key];
            }
			set
            {
                base[key] = value ?? JsonValue.Null;
            }
		}
        public JsonValue this[int index]
        {
            get
            {
                return base.Values.ElementAt(index);
            }
            set
            {
                base[base.Keys.ElementAt(index)] = value ?? JsonValue.Null;
            }
        }
        public DuplicateOptions DuplicateOptions = DuplicateOptions.Allow;
        public JsonObject() {}
        public JsonObject(IDictionary<string, JsonValue> collection) : base(collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value ?? JsonValue.Null)) {}
        public new void Add(string key, JsonValue value)
		{
            if (base.ContainsKey(key))
            {
                switch (DuplicateOptions)
                {
                    case DuplicateOptions.Allow:
                        base.Add(key, value ?? JsonValue.Null);
                        break;
                    case DuplicateOptions.Overwrite:
                        this[key] = value ?? JsonValue.Null;
                        break;
                }
            }
            else
            {
                base.Add(key, value ?? JsonValue.Null);
            }
		}
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("{");
            foreach (KeyValuePair<string, JsonValue> kvp in this)
            {
                result.Append($"{kvp.Key} : {kvp.Value},");
            }
            result.Remove(result.Length - 1, 1);
            return result.Append("}").ToString();
        }
        public new bool Equals(object o)
        {
            if (!(o is JsonObject obj))
            {
                return false;
            }
			return this.All(pair => obj[pair.Key].Equals(pair.Value));
        }
        public new int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}