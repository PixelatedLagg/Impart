using System;
using Csweb;
using System.Linq;
namespace Csweb
{
    static internal class StyleParser
    {
        //parses string into css
        //note: maybe use another dependency for better and easier parsing
        static internal string Parse(string style)
        {
            string temp = "";
            switch (style)
            {
                case string a when a.Contains(Colors.Any().ToString()):
                    Console.WriteLine("aids");
                    break;
            }
            return temp;
        }
        private static string[] Colors = 
        {
            "red",
            "orange",
            "yellow",
            "green",
            "blue",
            "purple",
            "pink",
            "white",
            "black"
        };
    }
}