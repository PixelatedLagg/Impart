namespace Impart
{
    /// <summary>An element external attribute.</summary>
    public class ExtAttr
    {

        /// <summary>The ExtAttrType.</summary>
        readonly public ExtAttrType Type;

        /// <summary>The ExtAttr value.</summary>
        readonly public string Value;

        /// <summary>Creates an ExtAttr instance.</summary>
        /// <param name="type">The ExtAttrType.</param>
        /// <param name="value">The ExtAttr value.</param>
        public ExtAttr(ExtAttrType type, string value)
        {
            Type = type;
            Value = value;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            switch (Type)
            {
                case ExtAttrType.ID:
                    return $" id=\"{Value}\"";
                case ExtAttrType.HoverMessage:
                    return $" title=\"{Value}\"";
                default:
                    throw new ImpartError("Invalid external attribute parameters.");
            }
        }
    }
}