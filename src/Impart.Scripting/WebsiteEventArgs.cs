using System.Collections.Generic;

namespace Impart.Scripting
{
    /// <summary>The event arguments passed for all main website events.</summary>
    public struct WebsiteEventArgs
    {
        /// <summary>The platform type.</summary>
        public Platform platform;

        /// <summary>Bool representing if the user is on a mobile device.</summary>
        public bool mobile;

        /// <summary>List of all browsers the server is sent. (Out of the three sent, one usually the main, and another what the main is based on.)</summary>
        public List<(Browser browser, int version)> browsers;

        /// <summary>Create a WebsiteEventArgs instance.</summary>
        /// <returns>A WebsiteEventArgs instance.</returns>
        /// <param name="platform">The platform type.</param>
        /// <param name="browsers">List of all browsers.</param>
        /// <param name="mobile">Bool representing if the user is on a mobile device.</param>
        public WebsiteEventArgs(Platform platform, List<(Browser browser, int version)> browsers, bool mobile)
        {
            this.platform = platform;
            this.mobile = mobile;
            this.browsers = browsers;
        }
    }
}