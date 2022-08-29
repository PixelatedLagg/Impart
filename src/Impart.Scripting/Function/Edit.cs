namespace Impart.Scripting
{
    /// <summary>Stores an edit made to an IElement.</summary>
    public class Edit : IFunction
    {
        private string Render;

        /// <summary>Creates an Edit instance.</summary>
        /// <param name="render">The edit render to store.</param>
        public Edit(string render)
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