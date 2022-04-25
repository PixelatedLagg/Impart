using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>The class for an animation.</summary>
    public struct Animation
    {
        /// <value>The name of the Animation.</value>
        public readonly string Name;

        private List<Frame> _Frames;

        /// <value>A list of Frames included in the Animation.</value>
        public List<Frame> Frames
        {
            get
            {
                return _Frames;
            }
        }

        /// <summary>Creates an Animation instance with <paramref name="name"/> as the name.</summary>
        /// <param name="name">The name of the Animation.</param>
        public Animation(string name)
        {
            Name = name;
            _Frames = new List<Frame>();
        }

        /// <summary>Add a Frame to the Animation.</summary>
        /// <param name="frames">The Frame(s) to add.</param>
        public Animation AddFrame(params Frame[] frames)
        {
            foreach (Frame frame in frames)
            {
                _Frames.Add(frame);
            }
            return this;
        }

        /// <summary>Add a Frame to the Animation.</summary>
        /// <param name="position">The position of the Frame to add.</param>
        /// <param name="changeType">The ChangeType of the Frame to add.</param>
        /// <param name="change">The change of the Frame to add.</param>
        public Animation AddFrame(Percent position, ChangeType changeType, object change)
        {
            _Frames.Add(new Frame(position, changeType, change));
            return this;
        }

        /// <summary>Remove a Frame from the Animation.</summary>
        /// <param name="position">The position of the Frame to remove.</param>
        public Animation RemoveFrame(Percent position)
        {
            foreach (Frame frame in _Frames)
            {
                if (frame.Position == position)
                {
                    _Frames.Remove(frame);
                    return this;
                }
            }
            throw new ImpartError("Animation does not contain a Frame at this position!");
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder($"@keyframes {Name} {{");
            foreach (Frame f in _Frames)
            {
                result.Append(f);
            }
            result.Append("}");
            return result.ToString();
        }
    }
}