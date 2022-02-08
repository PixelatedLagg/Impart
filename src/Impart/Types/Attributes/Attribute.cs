namespace Impart
{
    /// <summary>An element attribute.</summary>
    public struct Attribute
    {
        private AttributeType _type;
        private object[] _value;

        /// <value>The attribute type.</value>
        public AttributeType type
        {
            get
            {
                return _type;
            }
        }

        /// <value>The attribute value(s).</value>
        public object[] value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>Creates the attribute with the type and value(s).</summary>
        /// <returns>An Attribute instance.</returns>
        /// <param name="type">The attribute type.</param>
        /// <param name="value">The attribute value.</param>
        public Attribute(AttributeType type, params object[] value)
        {
            _type = type;
            _value = value;
        }
    }
}