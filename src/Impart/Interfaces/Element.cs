namespace Impart
{
    /// <summary>Element interface.</summary>
    public interface Element 
    {
        /// <value>IOID value for the Element.</value>
        int IOID { get; }

        /// <summary>Clones the Element (including its IOID).</summary>
        Element Clone();

        ExtAttrList ExtAttrs { get; }
    }
}