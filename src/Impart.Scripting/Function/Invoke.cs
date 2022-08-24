namespace Impart.Scripting
{
    /// <summary>Stores a function invocation.</summary>
    public class Invoke : IFunction
    {
        private string Render;

        /// <summary>Creates an Invoke instance.</summary>
        /// <param name="render">The invocation render to store.</param>
        public Invoke(string render)
        {
            Render = render;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return Render;
        }
    }
}