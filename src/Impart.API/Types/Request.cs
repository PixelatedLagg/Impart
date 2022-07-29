namespace Impart.Api
{
    /// <summary>All of the request types.</summary>
    public enum RequestType
    {
        /// <summary>GET HTTP request.</summary>
        Get,

        /// <summary>PUSH HTTP request.</summary>
        Push,

        /// <summary>PUT HTTP request.</summary>
        Put,

        /// <summary>DELETE HTTP request.</summary>
        Delete,

        /// <summary>HEAD HTTP request.</summary>
        Head,

        /// <summary>CONNECT HTTP request.</summary>
        Connect,
        
        /// <summary>OPTIONS HTTP request.</summary>
        Options,

        /// <summary>TRACE HTTP request.</summary>
        Trace,

        /// <summary>PATCH HTTP request.</summary>
        Patch
    }
}