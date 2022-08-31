using System.Net;

namespace Impart
{
    /// <summary>The event arguments passed when a client requests a WebPage from the WebSite.</summary>
    public struct WebSiteRequestArgs
    {
        /// <summary>The full request from the client.</summary>
        public readonly string Request;

        /// <summary>The IP address of the client.</summary>
        public readonly IPAddress Address;

        /// <summary>Create a WebSiteRequestArgs instance.</summary>
        /// <param name="request">The full request from the client.</param>
        /// <param name="address">The IP address of the client.</param>
        public WebSiteRequestArgs(string request, IPAddress address)
        {
            Request = request;
            Address = address;
        }
    }
}