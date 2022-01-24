namespace Impart
{
    public struct Attribute
    {
        private AttributeType _type;
        private object[] _value;
        public AttributeType type
        {
            get
            {
                return _type;
            }
        }
        public object[] value
        {
            get
            {
                return _value;
            }
        }
        public Attribute(AttributeType type, params object[] value)
        {
            _type = type;
            _value = value;
        }
    }
}