using System.Text;

namespace Impart.API
{
    public struct Xml : Format
    {
        private StringBuilder builder;
        private string title;
        private int _length;
        public int length
        {
            get
            {
                return _length;
            }
        }
    }
}