using System;

namespace Csweb
{
    public class cswebroot
    {
        public string path;
        public cswebroot(string path)
        {
            if (String.IsNullOrEmpty(path) || !Common.ValidPath(path, null))
            {
                throw new ArgumentException("Not a valid file path!");
            }
            this.path = path;
        }
    }
}