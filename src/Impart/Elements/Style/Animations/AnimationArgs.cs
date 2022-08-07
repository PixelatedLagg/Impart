namespace Impart
{
    /// <summary>The arguments for calling an Animation.</summary>
    public class AnimationArgs
    {
        /// <summary>The Animation name.</summary>
        public readonly string Name;

        /// <summary>The Animation duration.</summary>
        public readonly Time Duration;

        /// <summary>The Animation delay.</summary>
        public readonly Time Delay;

        /// <summary>The number of times to repeat the Animation.</summary>
        public readonly int Count;

        /// <summary>Creates an AnimationArgs instance.</summary>
        /// <param name="name">The Animation name.</param>
        /// <param name="duration">The Animation duration.</param>
        public AnimationArgs(string name, Time duration) : this(name, duration, 0, 1) { }

        /// <summary>Creates an AnimationArgs instance.</summary>
        /// <param name="name">The Animation name.</param>
        /// <param name="duration">The Animation duration.</param>
        /// <param name="delay">The Animation delay.</param>
        public AnimationArgs(string name, Time duration, Time delay) : this(name, duration, delay, 1) { }

        /// <summary>Creates an AnimationArgs instance.</summary>
        /// <param name="name">The Animation name.</param>
        /// <param name="duration">The Animation duration.</param>
        /// <param name="delay">The Animation delay.</param>
        /// <param name="count">The number of times to repeat the Animation.</param>
        public AnimationArgs(string name, Time duration, Time delay, int count)
        {
            Name = name;
            Duration = duration;
            Delay = delay;
            Count = count;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return $"animation-name: {Name};animation-duration: {Duration};animation-iteration-count: {Count};animation-delay: {Delay};";
        }
    }
}