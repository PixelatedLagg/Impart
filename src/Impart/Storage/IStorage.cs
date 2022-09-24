namespace Impart
{
    public interface IStorage
    {
        IElement ToBuilder();
        int IOID { get; }
    }
}