namespace Impart
{
    /// <summary>An element external attribute.</summary>
    public struct ExtAttr
    {
        private ExtAttrType _Type;

        /// <Value>The ExtAttr Type.</Value>
        public ExtAttrType Type
        {
            get
            {
                return _Type;
            }
        }
        private object[] _Value;

        /// <Value>The ExtAttr Value(s).</Value>
        public object[] Value
        {
            get
            {
                return _Value;
            }
        }

        /// <summary>Creates an ExtAttr instance.</summary>
        /// <param name="type">The ExtAttrType.</param>
        /// <param name="value">The ExtAttr Value.</param>
        public ExtAttr(ExtAttrType type, params object[] value)
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