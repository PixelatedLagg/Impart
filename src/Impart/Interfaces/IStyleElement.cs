namespace Impart
{
    /// <summary>IStyleElement interface.</summary>
    public interface IStyleElement
    {
        /// <value>The Impart Object ID for the IStyleElement.</value>
        int IOID { get; }

        /// <value>The ID for the IStyleElement.</value>
        string ID { get; set; }
    }
}