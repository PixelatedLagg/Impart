namespace Impart
{
    /// <summary>IElement interface.</summary>
    public interface IElement 
    {
        /// <summary>IOID value for the IElement.</summary>
        int IOID { get; }

        /// <summary>Clones the IElement (including its IOID).</summary>
        IElement Clone();

        /// <summary>The external attributes of the IElement.</summary>
        ExtAttrList ExtAttrs { get; }

        /// <summary>The attributes of the IElement.</summary>
        AttrList Attrs { get; }

        /// <summary>Create an ElementRef referencing the IElement</summary>
        ElementRef Reference();
    }
}