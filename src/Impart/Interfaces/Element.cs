namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element 
    {
        /// <value>IOID value for the Element.</value>
        int IOID { get; }

        /// <summary>Clones the Element (including its IOID).</summary>
        Element Clone();

        /// <summary>The external attributes of the Element.</summary>
        ExtAttrList ExtAttrs { get; }
    }
}