using System;

namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element 
    {
        int IOID { get; set; }
    }

    internal interface Nested 
    {
        internal string First();
        internal string Last();
    }
}