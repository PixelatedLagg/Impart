namespace Impart
{
    /// <summary>IStyleElement interface.</summary>
    public interface IStyleElement
    {
        /// <summary>The Impart Object ID for the IStyleElement.</summary>
        int IOID { get; }

        /// <summary>The ID for the IStyleElement.</summary>
        string ID { get; set; }
    }
}