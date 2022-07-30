namespace Impart
{
    /// <summary> Hold a reference to an IElement.</summary>
    public struct ElementRef
    {
        internal readonly int IOID;

        /// <summary>Creates an ElementRef instance.</summary>
        /// <param name="element">The IElement instance to create the reference to.</param>
        public ElementRef(IElement element)
        {
            new ElementRef().
            IOID = element.IOID;
        }

        /// <summary>Compares an IElement instance and an ElementRef.</summary>
        /// <param name="element">The IElement instance to compare.</param>
        /// <param name="reference">The ElementRef to compare.</param>
        public static bool operator ==(IElement element, ElementRef reference) => reference.IOID == element.IOID;

        /// <summary>Creates an ElementRef instance.</summary>
        /// <param name="element">The IElement instance to create the reference to.</param>
        public static explicit operator ElementRef(IElement element) => new ElementRef(element);

        /// <summary>Creates an ElementRef instance.</summary>
        /// <param name="element">The IElement instance to create the reference to.</param>
        public static implicit operator ElementRef(IElement element) => new ElementRef(element);
    }
}