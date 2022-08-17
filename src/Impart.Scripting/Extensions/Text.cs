namespace Impart.Scripting
{
    public static class TextExtensions
    {
        public static Trigger ClickTrigger(this Text text)
        {
            return new Trigger($"if (event.target.matches('#{text.ID}'))", Trigger.Type.Click);
        }
        public static Trigger HoverTrigger(this Text text)
        {
            return new Trigger($"if (event.target.matches('#{text.ID}'))", Trigger.Type.Hover);
        }
    }
}