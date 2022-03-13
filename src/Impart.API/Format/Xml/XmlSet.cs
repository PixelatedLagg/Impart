namespace Impart.API
{
    public struct XmlSet
    {
        private object _key;
        public object key
        {
            get
            {
                return _key;
            }
        }

        private object _value;
        public object value
        {
            get
            {
                return _value;
            }
        }
        public XmlSet(object key, object value)
        {
            _key = key;
            _value = value;
        }
    }
}