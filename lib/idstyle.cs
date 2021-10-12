using System.Threading;
using System.Globalization;
using System;
using System.IO;
using System.Linq;
using System.Drawing;

namespace Csweb
{
    public class idstyle
    {
        private string textCache;
        public idstyle(string id)
        {
            textCache = $"#{id} {{{Environment.NewLine}";
        }
        public void AddColor(Color color)
        {
            textCache = $"{textCache}    color: {color.ToKnownColor()};{Environment.NewLine}";
        }
        internal string Render()
        {
            return $"{textCache}{Environment.NewLine}}}";
        }
    }
}