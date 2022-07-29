using System;

namespace Impart.Api
{
    /// <summary>Information about current API event.</summary>
    public struct APIEventArgs
    {
        /// <summary>The type of request that was sent.</summary>
        public readonly RequestType Request;

        /// <summary>The time the request was sent.</summary>
        public readonly DateTime Time;

        /// <summary>Creates an APIEventArgs instance with <paramref name="request"/> as the request type.</summary>
        /// <param name="request">The request type.</param>
        public APIEventArgs(RequestType request)
        {
            Request = request;
            Time = DateTime.Now;
        }
    }
}