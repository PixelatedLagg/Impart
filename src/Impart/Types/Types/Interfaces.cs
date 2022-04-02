using System;

namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element {}

    /// <summary>Color interface.</summary>
    public interface Color {}

    internal interface Nested 
    {
        internal string First();
        internal string Last();
    }
}