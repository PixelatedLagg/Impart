using System;
using System.Collections.Generic;

namespace Impart.Scripting
{
    public struct WebsiteEventArgs
    {
        public DateTime time;
        public string IP;
        public List<(Browser browser, int version)> browsers;
    }
}