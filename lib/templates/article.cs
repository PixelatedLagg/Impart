using System;
using Csweb;

namespace Csweb.Templates
{
    internal static class Article
    {
        internal static string Render(object[] args = null)
        {
            string textCache = "";
            foreach (object obj in args)
            {
                textCache = $"{textCache}";
            }
            return null;
        }
    }
}