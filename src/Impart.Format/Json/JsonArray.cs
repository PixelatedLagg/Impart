using System;
using System.Linq;
using System.Collections.Generic;

namespace Impart.Format
{
    public class JsonArray : List<JsonValue>
    {
        public JsonArray() {}
        public JsonArray(IEnumerable<JsonValue> collection)
			: base(collection.Select(jv => jv ?? JsonValue.Null)) {}
        public new void Add(JsonValue item)
		{
			base.Add(item ?? JsonValue.Null);
		}
        public new void AddRange(IEnumerable<JsonValue> collection)
		{
			base.AddRange(collection.Select(v => v ?? JsonValue.Null));
		}
        public override string ToString()
        {
            return "";
        }
        public override bool Equals(object o)
        {
            if (!(o is JsonArray a))
            {
                return false;
            }
            if (!a.ToArray().Equals(base.ToArray()))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}