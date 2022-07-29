namespace Impart.Api
{
    /// <summary>All of the API server-side error types.</summary>
    public enum ErrorType
    {
        /// <summary>BadRequest error.</summary>
        BadRequest = 400,

        /// <summary>Unauthorized error.</summary>
        Unauthorized = 401,

        /// <summary>PaymentRequired error.</summary>
        PaymentRequired = 402,
        
        /// <summary>Forbidden error.</summary>
        Forbidden = 403,

        /// <summary>NotFound error.</summary>
        NotFound = 404,

        /// <summary>MethodNotAllowed error.</summary>
        MethodNotAllowed = 405,

        /// <summary>NotAcceptable error.</summary>
        NotAcceptable = 406,

        /// <summary>ProxyAuthenticationRequired error.</summary>
        ProxyAuthenticationRequired = 407,

        /// <summary>RequestTimeout error.</summary>
        RequestTimeout = 408,

        /// <summary>Conflict error.</summary>
        Conflict = 409,

        /// <summary>Gone error.</summary>
        Gone = 410,

        /// <summary>LengthRequest error.</summary>
        LengthRequest = 411,

        /// <summary>PreconditionFailed error.</summary>
        PreconditionFailed = 412,

        /// <summary>PayloadTooLarge error.</summary>
        PayloadTooLarge = 413,

        /// <summary>URITooLong error.</summary>
        URITooLong = 414,

        /// <summary>UnsupportedMediaType error.</summary>
        UnsupportedMediaType = 415,

        /// <summary>RangeNotSatisfiable error.</summary>
        RangeNotSatisfiable = 416,

        /// <summary>ExpectationFailed error.</summary>
        ExpectationFailed = 417,

        /// <summary>MisdirectedRequest error.</summary>
        MisdirectedRequest = 421,

        /// <summary>UnprocessableEntity error.</summary>
        UnprocessableEntity = 422,

        /// <summary>Locked error.</summary>
        Locked = 423,

        /// <summary>FailedDependency error.</summary>
        FailedDependency = 424,

        /// <summary>TooEarly error.</summary>
        TooEarly = 425,

        /// <summary>UpgradeRequired error.</summary>
        UpgradeRequired = 426,

        /// <summary>PreconditionRequired error.</summary>
        PreconditionRequired = 428,

        /// <summary>TooManyRequests error.</summary>
        TooManyRequests = 429,

        /// <summary>RequestHeaderFieldsTooLarge error.</summary>
        RequestHeaderFieldsTooLarge = 431,

        /// <summary>UnavailableForLegalReasons error.</summary>
        UnavailableForLegalReasons = 451,

        /// <summary>InternalServerError error.</summary>
        InternalServerError = 500,

        /// <summary>NotImplemented error.</summary>
        NotImplemented = 501,

        /// <summary>BadGateway error.</summary>
        BadGateway = 502,

        /// <summary>ServiceUnavailable error.</summary>
        ServiceUnavailable = 503,

        /// <summary>GatewayTimeout error.</summary>
        GatewayTimeout = 504,

        /// <summary>HTTPVersionNotSupported error.</summary>
        HTTPVersionNotSupported = 505,

        /// <summary>VariantAlsoNegotiates error.</summary>
        VariantAlsoNegotiates = 506,

        /// <summary>InsufficientStorage error.</summary>
        InsufficientStorage = 507,

        /// <summary>LoopDetected error.</summary>
        LoopDetected = 508,

        /// <summary>NotExtended error.</summary>
        NotExtended = 510,

        /// <summary>NetworkAuthenticationRequired error.</summary>
        NetworkAuthenticationRequired = 511
    }
}