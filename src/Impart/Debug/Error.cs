using System;

namespace Impart
{
    /// <summary>The error class used by Impart.</summary>
    public sealed class ImpartError : Exception
    {
        /// <summary>Throws an ImpartError.</summary>
        /// <param name="error">The error message.</param>
        public ImpartError(string error) : base(error) {}
    }
}