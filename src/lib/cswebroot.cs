using System;

namespace Csweb
{
    public class cswebroot
    {
        public string source;
        public cswebroot(string page)
        {
            if (String.IsNullOrEmpty(page))
            {
                throw new ArgumentException("ID or Page source missing/invalid!");
            }
            source = page;
        }
    }
}