using System;
using System.Collections.Generic;

namespace Impart.Scripting
{
    public struct WebsiteEventArgs
    {
        public Platform platform;
        public bool mobile;
        public List<(Browser browser, int version)> browsers;

        public WebsiteEventArgs(Platform platform, List<(Browser browser, int version)> browsers, bool mobile)
        {
            this.platform = platform;
            this.mobile = mobile;
            this.browsers = browsers;
        }
    }
}