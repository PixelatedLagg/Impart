using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class LinkStorage : IStorage
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

        public LinkStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public Link ToBuilder()
        {
            int index = 1;
            return LinkParser.Parse(Cache, ref index);
        }
    }
}