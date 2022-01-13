namespace Impart.Scripting
{
    public struct ClickEventArgs
    {
        public Type type;
        public ClickEventArgs(Type type)
        {
            this.type = type;
        }
        public enum Type
        {
            OnClick,
            OffClick,
            Click
        }
    }
}