namespace Impart.Internal
{
    /// <summary>Elements that can be nested.</summary>
    public interface INested
    {
        /// <summary>The first tag of an Element with the contents.</summary>
        string First();
        
        /// <summary>The last tag of an Element.</summary>
        string Last();
    }
}