namespace Impart
{
    /// <summary> Hold a reference to an IElement.</summary>
    public class ElementRef
    {
        internal readonly int IOID;

        /// <summary>Creates an ElementRef instance.</summary>
        /// <param name="element">The IElement instance to create the reference to.</param>
        public ElementRef(IElement element)
        {
            IOID = element.IOID;
        }

        internal ElementRef(int ioid)
        {
            IOID = ioid;
        }

        /// <summary>Compares the equality of an IElement instance and an ElementRef instance.</summary>
        /// <param name="element">The IElement instance to compare.</param>
        /// <param name="reference">The ElementRef instance to compare.</param>
        public static bool operator ==(IElement element, ElementRef reference) => reference.IOID == element.IOID;

        /// <summary>Compares the inequality of an IElement instance and an ElementRef instance.</summary>
        /// <param name="element">The IElement instance to compare.</param>
        /// <param name="reference">The ElementRef instance to compare.</param>
        public static bool operator !=(IElement element, ElementRef reference) => reference.IOID != element.IOID;

        /// <summary>Compares the equality of an Object instance and this instance.</summary>
        /// <param name="obj">The Object instance to compare.</param>
        public override bool Equals(object obj)
        {
            if (obj as ElementRef != null)
            {
                return (ElementRef)obj == this;
            }
            return false;
        }

        /// <summary>Get the hash code of this instance.</summary>
        public override int GetHashCode()
        {
            return IOID.GetHashCode();
        }
    }
}