using System;
using System.IO;

namespace Impart.Format
{
    internal static class JsonObjectParser
    {
        internal static bool Parse(string source, ref int index, out JsonValue value)
        {
            //bool complete = false;
			JsonObject obj = new JsonObject();
			value = obj;
			var length = source.Length;
			index++;
			while (index < length)
			{

			}
			return false;
        }
    }
}