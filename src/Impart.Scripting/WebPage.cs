namespace Impart.Scripting
{
    /// <summary>Interactive scripting for WebPage - NOT FINISHED YET.</summary>
    public static class WebPageScripting
    {
        /// <summary>Add a Key event listener to the WebPage instance.</summary>
        /// <param name="webPage">The WebPage to add the event listener to.</param>
        /// <param name="key">The Key to add an event listener for.</param>
        public static WebPage AddKeyEvent(this WebPage webPage, Key key)
        {
            //add event
            return webPage;
        }

        /// <summary>Add a Click event listener to the WebPage instance.</summary>
        /// <param name="webPage">The WebPage to add the event listener to.</param>
        /// <param name="click">The Click to add an event listener for.</param>
        public static WebPage AddClickEvent(this WebPage webPage, Click click)
        {
            //add event
            return webPage;
        }
    }
}