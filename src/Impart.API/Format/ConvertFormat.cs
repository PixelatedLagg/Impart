using System;
using System.IO;

namespace Impart.API
{
    public static class ConvertFormat //TODO finish json-file converter
    {
        public static Json Json(string file)
        {
            if (!Path.GetExtension(file).Contains(".json"))
            {
                throw new ImpartError("File must be a JSON file!");
            }
            string contents = File.ReadAllText(file).Trim();
            string title = contents.Split("\"")[1];
            Json json = new Json(title);
            return json;
        }
        public static Json Json(Xml xml)
        {
            return new Json();
        }
        public static Xml Xml(string file)
        {
            return new Xml();
        }
        public static Xml Xml(Json json)
        {
            return new Xml();
        }
    }
}