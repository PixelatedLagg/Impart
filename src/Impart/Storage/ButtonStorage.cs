using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class ButtonStorage : IStorage
    {
        private string Cache;
        private int _IOID;
        int IStorage.IOID
        {
            get
            {
                return _IOID;
            }
        }

        public ButtonStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public Button ToBuilder()
        {
            int index = 1;
            return ButtonParser.Parse(Cache, ref index);
        }
    }
}
//</button>