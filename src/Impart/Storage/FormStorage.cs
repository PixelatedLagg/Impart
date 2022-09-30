using System;
using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    public class FormStorage : IStorage 
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
        public FormStorage(string cache, int ioid)
        {
            Cache = cache;
            _IOID = ioid;
        }
        IElement IStorage.ToBuilder() => ToBuilder();
        public Form ToBuilder()
        {
            int index = 1;
            return FormParser.Parse(Cache, ref index);
        }
    }
}