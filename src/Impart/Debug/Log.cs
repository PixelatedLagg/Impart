namespace Impart
{
    /// <summary>Store a log.</summary>
    public struct Log
    {
        /// <summary>The duration of the log.</summary>
        public readonly double Milliseconds;

        /// <summary>The Log message.</summary>
        public readonly string Message;

        /// <summary>Creates a Log instance.</summary>
        /// <param name="message">The Log message</param>
        /// <param name="milliseconds">The duration of the action in milliseconds.</param>
        public Log(string message, double milliseconds)
        {
            Message = message;
            Milliseconds = milliseconds;
        }
    }
}