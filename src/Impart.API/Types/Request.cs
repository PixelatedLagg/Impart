namespace Impart.Api
{
    /// <summary>All of the request types.</summary>
    public enum RequestType
    {
        /// <value>GET HTTP request.</value>
        Get,

        /// <value>PUSH HTTP request.</value>
        Push,

        /// <value>PUT HTTP request.</value>
        Put,

        /// <value>DELETE HTTP request.</value>
        Delete,

        /// <value>HEAD HTTP request.</value>
        Head,

        /// <value>CONNECT HTTP request.</value>
        Connect,
        
        /// <value>OPTIONS HTTP request.</value>
        Options,

        /// <value>TRACE HTTP request.</value>
        Trace,

        /// <value>PATCH HTTP request.</value>
        Patch
    }
}