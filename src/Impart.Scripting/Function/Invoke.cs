namespace Impart.Scripting
{
    public class Invoke : IFunction
    {
        private string Render;
        public Invoke(string render)
        {
            Render = render;
        }
        public override string ToString()
        {
            return Render;
        }
    }
}