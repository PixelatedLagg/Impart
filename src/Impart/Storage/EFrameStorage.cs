using Impart.Internal;

namespace Impart
{
    public class EFrameStorage : IStorage
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

        public EFrameStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public EFrame ToBuilder()
        {
            int index = 1;
            return EFrameParser.Parse(Cache, ref index);
        }
    }
}