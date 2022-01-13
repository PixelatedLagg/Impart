namespace Impart.Scripting
{
    public interface IEvent
    {
        void ClickEvent(ClickEventArgs args);
        void KeyEvent(KeyEventArgs args);
    }
}