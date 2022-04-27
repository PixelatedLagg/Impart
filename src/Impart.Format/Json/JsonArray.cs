using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Impart.Format
{
    /// <summary>Store an array of JsonValues.</summary>
    public class JsonArray : List<JsonValue>
    {
        /// <summary>Creates a JsonArray instance.</summary>      
        public JsonArray() { }

        /// <summary>Creates a JsonArray instance containing all the entries of a IEnumerable.</summary>
        /// <param name="collection">The IEnumerable to copy the entries from.</param>
        public JsonArray(IEnumerable<JsonValue> collection) : base(collection.Select(v => v ?? JsonValue.Null)) { }
        
        /// <summary>Adds a JsonValue to the JsonArray.</summary>
        /// <param name="value">The JsonValue to add.</param>
        public new void Add(JsonValue value)
		{
			base.Add(value ?? JsonValue.Null);
		}

        /// <summary>Adds all of the entries of an IEnumerable.</summary>
        /// <param name="collection">The IEnumerable to copy the entries from.</param>
        public new void AddRange(IEnumerable<JsonValue> collection)
		{
			base.AddRange(collection.Select(v => v ?? JsonValue.Null));
		}

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("[");
            foreach (JsonValue v in this)
            {
                result.Append($"{v},");
            }
            result.Remove(result.Length - 1, 1);
            return result.Append("]").ToString();
        }

        /// <summary>Compares the equality of this instance and an Object.</summary>
        /// <param name="o">The Object to compare.</param>
        public override bool Equals(object o)
        {
            if (o is JsonArray a)
            {
                return a.ToArray().Equals(base.ToArray());
            }
            return false;
        }

        /// <summary>Get the hashcode of the current instance.</summary>
        public override int GetHashCode() => base.GetHashCode();
    }
}