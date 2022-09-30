using System;
using System.Text;
using Impart.Internal;

namespace Impart
{
    public class TableStorage : IStorage
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

        public TableStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }

        IElement IStorage.ToBuilder() => ToBuilder();

        public Table ToBuilder()
        {
            int index = 1;
            return TableParser.Parse(Cache, ref index);
        }
    }
}