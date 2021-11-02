using System;
using CSWeb;

namespace CSWeb.Templates
{
    internal static class Article
    {
        internal static string Render(string[] args = null)
        {
            string textCache = "";
            bool header = true;
            foreach (string obj in args)
            {
                if (header)
                {
                    textCache = $"{textCache}%^    <h1>{obj}</h1>";
                    header = false;
                }
                else
                {
                    textCache = $"{textCache}%^    <p>{obj}</p>";
                    header = true;
                }
            }
            return textCache;
        }
    }
}