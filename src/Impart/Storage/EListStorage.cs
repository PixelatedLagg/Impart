using Impart.Internal;

namespace Impart
{
    public class EListStorage : IStorage
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

        public EListStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public EList ToBuilder()
        {
            int index = 1;
            return EListParser.Parse(Cache, ref index);
        }
    }
}