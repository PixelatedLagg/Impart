using System;

namespace Impart.Api
{
    /// <summary>Information about current API event.</summary>
    public struct APIEventArgs
    {
        /// <value>The type of request that was sent.</value>
        public readonly RequestType Request;

        /// <value>The time the request was sent.</value>
        public readonly DateTime Time;

        /// <summary>Creates an APIEventArgs instance with <paramref name="request"/> as the request type.</summary>
        /// <returns>An APIEventArgs instance.</returns>
        /// <param name="request">The request type.</param>
        public APIEventArgs(RequestType request)
        {
            Request = request;
            Time = DateTime.Now;
        }
    }
}