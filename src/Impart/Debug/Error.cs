using System;

namespace Impart
{
    public class ImpartError : Exception
    {
        public ImpartError(string error) : base (error) {}
    }
}