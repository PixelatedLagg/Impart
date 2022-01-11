namespace Impart.Scripting
{
    public struct Inputargs
    {
        public Event evt;
        public Input input;
        public Inputargs(Event evt, Input input)
        {
            this.evt = evt;
            this.input = input;
        }
    }
}