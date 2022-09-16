namespace Impart.Internal
{
    public static class StorageExtensions
    {
        public static Attr GetAttr(string attrType, string attrValue)
        {
            string[] args = attrValue.Split(' ');
            switch (attrType)
            {
                case "background-color":
                    return new Attr(AttrType.BackgroundColor);
            }
        }
        public static ExtAttr GetExtAttr(string extAttrType, string extAttrValue)
        {

        }
    }
}