namespace Impart.Scripting
{
    public struct Trigger
    {
        private string Render;
        public Trigger(string render)
        {
            Render = render;
        }
        public override string ToString()
        {
            return Render;
        }
    }
}