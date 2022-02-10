namespace Impart.Scripting
{
    /// <summary>The event arguments passed when the user clicks.</summary>
    public struct ClickEventArgs
    {
        /// <summary>The click type.</summary>
        public Click click;

        /// <summary>Create a ClickEventArgs instance.</summary>
        /// <returns>A ClickEventArgs instance.</returns>
        /// <param name="click">The click type.</param>
        public ClickEventArgs(Click click)
        {
            this.click = click;
        }
    }
}