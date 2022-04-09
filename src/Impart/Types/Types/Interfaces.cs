using System;

namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element {}
    internal interface Nested 
    {
        internal string First();
        internal string Last();
    }
}