namespace Impart.Scripting
{
    public struct KeyEventArgs
    {
        public Key key;
        public KeyEventArgs(Key key)
        {
            this.key = key;
        }
    }
}