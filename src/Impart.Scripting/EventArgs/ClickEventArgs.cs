namespace Impart.Scripting
{
    public struct ClickEventArgs
    {
        public Click click;
        public ClickEventArgs(Click click)
        {
            this.click = click;
        }
    }
}