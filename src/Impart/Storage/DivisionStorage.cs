using Impart.Internal;

namespace Impart
{
    public class DivisionStorage : IStorage
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

        public DivisionStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();
        public Division ToBuilder()
        {
            int index = 1;
            return DivisionParser.Parse(Cache, ref index);
        }
    }
}