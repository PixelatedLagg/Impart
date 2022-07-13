using System.Text;

namespace Impart
{
    public struct FontArgs
    {
        private StringBuilder builder = new StringBuilder();
        public FontArgs(string url, FontType type)
        {
            
        }
        public override string ToString()
        {
            return builder.ToString();
        }
    }
}