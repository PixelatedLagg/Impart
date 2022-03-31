using System;

namespace Impart
{
    /// <summary>The error class used by Impart.</summary>
    public sealed class ImpartError : Exception
    {
        /// <summary>Throws an ImpartError with <paramref name="error"/> as the message.</summary>
        /// <returns>An ImpartError instance.</returns>
        /// <param name="error">The error message.</param>
        public ImpartError(string error) : base(error) {}
    }
}