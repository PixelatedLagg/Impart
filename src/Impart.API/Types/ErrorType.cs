namespace Impart.Api
{
    /// <summary>All of the API server-side error types.</summary>
    public enum ErrorType
    {
        /// <value>BadRequest error.</value>
        BadRequest = 400,

        /// <value>Unauthorized error.</value>
        Unauthorized = 401,

        /// <value>PaymentRequired error.</value>
        PaymentRequired = 402,
        
        /// <value>Forbidden error.</value>
        Forbidden = 403,

        /// <value>NotFound error.</value>
        NotFound = 404,

        /// <value>MethodNotAllowed error.</value>
        MethodNotAllowed = 405,

        /// <value>NotAcceptable error.</value>
        NotAcceptable = 406,

        /// <value>ProxyAuthenticationRequired error.</value>
        ProxyAuthenticationRequired = 407,

        /// <value>RequestTimeout error.</value>
        RequestTimeout = 408,

        /// <value>Conflict error.</value>
        Conflict = 409,

        /// <value>Gone error.</value>
        Gone = 410,

        /// <value>LengthRequest error.</value>
        LengthRequest = 411,

        /// <value>PreconditionFailed error.</value>
        PreconditionFailed = 412,

        /// <value>PayloadTooLarge error.</value>
        PayloadTooLarge = 413,

        /// <value>URITooLong error.</value>
        URITooLong = 414,

        /// <value>UnsupportedMediaType error.</value>
        UnsupportedMediaType = 415,

        /// <value>RangeNotSatisfiable error.</value>
        RangeNotSatisfiable = 416,

        /// <value>ExpectationFailed error.</value>
        ExpectationFailed = 417,

        /// <value>MisdirectedRequest error.</value>
        MisdirectedRequest = 421,

        /// <value>UnprocessableEntity error.</value>
        UnprocessableEntity = 422,

        /// <value>Locked error.</value>
        Locked = 423,

        /// <value>FailedDependency error.</value>
        FailedDependency = 424,

        /// <value>TooEarly error.</value>
        TooEarly = 425,

        /// <value>UpgradeRequired error.</value>
        UpgradeRequired = 426,

        /// <value>PreconditionRequired error.</value>
        PreconditionRequired = 428,

        /// <value>TooManyRequests error.</value>
        TooManyRequests = 429,

        /// <value>RequestHeaderFieldsTooLarge error.</value>
        RequestHeaderFieldsTooLarge = 431,

        /// <value>UnavailableForLegalReasons error.</value>
        UnavailableForLegalReasons = 451,

        /// <value>InternalServerError error.</value>
        InternalServerError = 500,

        /// <value>NotImplemented error.</value>
        NotImplemented = 501,

        /// <value>BadGateway error.</value>
        BadGateway = 502,

        /// <value>ServiceUnavailable error.</value>
        ServiceUnavailable = 503,

        /// <value>GatewayTimeout error.</value>
        GatewayTimeout = 504,

        /// <value>HTTPVersionNotSupported error.</value>
        HTTPVersionNotSupported = 505,

        /// <value>VariantAlsoNegotiates error.</value>
        VariantAlsoNegotiates = 506,

        /// <value>InsufficientStorage error.</value>
        InsufficientStorage = 507,

        /// <value>LoopDetected error.</value>
        LoopDetected = 508,

        /// <value>NotExtended error.</value>
        NotExtended = 510,

        /// <value>NetworkAuthenticationRequired error.</value>
        NetworkAuthenticationRequired = 511
    }
}