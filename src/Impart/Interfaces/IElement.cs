namespace Impart
{
    /// <summary>IElement interface.</summary>
    public interface IElement 
    {
        /// <value>IOID value for the IElement.</value>
        int IOID { get; }

        /// <summary>Clones the IElement (including its IOID).</summary>
        IElement Clone();

        /// <summary>The external attributes of the IElement.</summary>
        ExtAttrList ExtAttrs { get; }
    }
}