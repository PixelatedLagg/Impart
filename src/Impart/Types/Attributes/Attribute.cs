namespace Impart
{
    public struct Attribute
    {
        private AttributeType _type;
        public AttributeType type
        {
            get
            {
                return _type;
            }
        }
        public Attribute(AttributeType type)
        {
            _type = type;
        }
    }
}