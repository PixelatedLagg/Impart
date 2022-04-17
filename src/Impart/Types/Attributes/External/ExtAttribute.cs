namespace Impart
{
    public struct ExtAttribute
    {
        private ExtAttributeType _Type;

        public ExtAttributeType Type
        {
            get
            {
                return _Type;
            }
        }
        private object[] _Value;

        public object[] Value
        {
            get
            {
                return _Value;
            }
        }

        public ExtAttribute(ExtAttributeType type, params object[] value)
        {
            _Type = type;
            _Value = value;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case ExtAttributeType.ID:
                    return $" id=\"{_Value[0]}\"";
                case ExtAttributeType.HoverMessage:
                    return $" title=\"{_Value[0]}\"";
                default:
                    throw new ImpartError("Invalid external attribute parameters.");
            }
        }
    }
}