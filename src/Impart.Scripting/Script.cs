using System.Text;

namespace Impart.Scripting
{
    internal class Script
    {
        internal StringBuilder Cache = new StringBuilder();
        internal Script(string text)
        {
            Cache.Append(text);
        }
        internal void Append(string text) => Cache.Append(text);
        public override string ToString()
        {
            if (Cache.ToString() == "")
            {
                return "";
            }
            return $"<style>{Cache.ToString()}</style>";
        }
    }
}