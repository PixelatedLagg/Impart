using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Impart.Format
{
    /// <summary>Store many Json nodes.</summary>
    public class JsonObject : Dictionary<string, JsonValue>
    {
        /// <value>Controls how the JsonObject handles duplicate entries.</value>
        public DuplicateOptions DuplicateOptions = DuplicateOptions.Allow;

        /// <summary>Creates a JsonObject instance.</summary>
        public JsonObject() { }

        /// <summary>Creates a JsonObject instance containing all the entries of a Dictionary.</summary>
        /// <param name="collection">The Dictionary to copy the entries from.</param>
        public JsonObject(IDictionary<string, JsonValue> collection) : base(collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value ?? JsonValue.Null)) { }
        
        /// <summary>Gets a JsonValue by key.</summary>
        /// <param name="key">The key to use.</param>
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

        /// <summary>Gets a JsonValue by index.</summary>
        /// <param name="index">The index to use.</param>
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

        /// <summary>Adds a JsonValue and key to the JsonObject.</summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The JsonValue to add.</param>
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

        /// <summary>Returns the instance as a String.</summary>
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

        /// <summary>Compares the equality of this instance and an Object.</summary>
        /// <param name="o">The Object to compare.</param>
        public new bool Equals(object o)
        {
            if (o is JsonObject obj)
            {
                return this.All(pair => obj[pair.Key].Equals(pair.Value));
            }
            return false;
        }

        /// <summary>Get the hashcode of the current instance.</summary>
        public new int GetHashCode() => base.GetHashCode();
    }
}