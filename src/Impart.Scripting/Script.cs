using System;
using System.Net;
using System.Text;

namespace Impart.Scripting
{
    internal class Script
    {
        internal StringBuilder cache = new StringBuilder();
        internal Script(string text)
        {
            cache.Append(text);
        }
        internal void AddEvents()
        {
        }
        internal void Send()
        {
            
        }
    }
}