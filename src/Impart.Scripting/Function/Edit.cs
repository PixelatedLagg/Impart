namespace Impart.Scripting
{
    public class Edit : IFunction
    {
        private string Render;
        public Edit(string render)
        {
            Render = render;
        }
        public override string ToString()
        {
            return Render;
        }
    }
}