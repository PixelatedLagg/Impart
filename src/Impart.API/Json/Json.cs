using Impart;
using System;
using System.Text;

namespace Impart.API
{
    public class Json
    {
        private StringBuilder builder;
        public Json()
        {
            builder = new StringBuilder("{");
        }
        public void AddSet(object name, object value)
        {
            if (builder.Length == 1)
            {
                builder.Append($"\"{name}\" : \"{value}\"");
            }
            else
            {
                builder.Append($"\", {name}\" : \"{value}\"");
            }
        }
        public void AddArray(object name, params (object, object)[] entries)
        {
            builder.Append($"\"{name}\" : {{");
            for (int i = 0; i < entries.Length; i++)
            {
                if (i == 0)
                {
                    builder.Append($"\"{entries[i].Item1} : {entries[i].Item2}\"");
                }
                else
                {
                    builder.Append($"\", {entries[i].Item1} : {entries[i].Item2}\"");
                }
            }
            builder.Append("}");
            Console.WriteLine(builder.ToString());
        }
    }
}