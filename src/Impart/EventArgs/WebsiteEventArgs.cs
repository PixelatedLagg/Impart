using System.Net;

namespace Impart
{
    /// <summary>The event arguments passed when a client requests a WebPage from the Website.</summary>
    public struct WebsiteRequestArgs
    {
        /// <summary>The full request from the client.</summary>
        public readonly string Request;

        /// <summary>The IP address of the client.</summary>
        public readonly IPAddress Address;

        /// <summary>Create a WebsiteRequestArgs instance.</summary>
        /// <param name="request">The full request from the client.</param>
        /// <param name="address">The IP address of the client.</param>
        public WebsiteRequestArgs(string request, IPAddress address)
        {
            Request = request;
            Address = address;
        }
    }
}