using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class VideoStorage : IStorage
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
        public VideoStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }
        IElement IStorage.ToBuilder() => ToBuilder();
        public Video ToBuilder()
        {
            int index = 1;
            return VideoParser.Parse(Cache, ref index);
        }
    }
}