namespace Impart
{
    /// <summary>An element external attribute.</summary>
    public struct ExtAttribute
    {
        private ExtAttrType _Type;

        /// <Value>The ExtAttribute Type.</Value>
        public ExtAttrType Type
        {
            get
            {
                return _Type;
            }
        }
        private object[] _Value;

        /// <Value>The ExtAttribute Value(s).</Value>
        public object[] Value
        {
            get
            {
                return _Value;
            }
        }

        /// <summary>Creates an ExtAttribute instance.</summary>
        /// <param name="Type">The attribute Type.</param>
        /// <param name="Value">The attribute Value.</param>
        public ExtAttribute(ExtAttrType type, params object[] value)
        {
            _Type = type;
            _Value = value;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            switch (Type)
            {
                case ExtAttrType.ID:
                    return $" id=\"{_Value[0]}\"";
                case ExtAttrType.HoverMessage:
                    return $" title=\"{_Value[0]}\"";
                default:
                    throw new ImpartError("Invalid external attribute parameters.");
            }
        }
    }
}