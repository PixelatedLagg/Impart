namespace Impart
{
    public class AnimationArgs
    {
        public readonly string Name;
        public readonly Time Duration;
        public readonly int Count;
        public readonly Time Delay;
        public AnimationArgs(string name, Time duration, int count = 1, Time delay = 0)
        {
            Name = name;
            Duration = duration;
            Count = count;
            Delay = delay;
        }
        public override string ToString()
        {
            return $"animation-name: {Name};animation-duration: {Duration};animation-iteration-count: {Count};animation-delay: {Delay};";
        }
    }
}