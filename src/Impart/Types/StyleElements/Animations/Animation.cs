using System.Text;
using Impart.Internal;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Animation style element.</summary>
    public class Animation : StyleElement
    {
        private List<Frame> _Frames = new List<Frame>();

        /// <value>A list of Frames included in the Animation.</value>
        public List<Frame> Frames
        {
            get
            {
                return _Frames;
            }
        }
        
        /// <value>The ID value of the Animation. (acts as the name of the Animation)</value>
        string StyleElement.ID
        {
            get
            {
                return _Name;
            }
            set 
            {
                Changed = true;
                _Name = value;
            }
        }
        private int _IOID = Ioid.Generate();

        /// <value>The internal ID of the instance.</value>
        int StyleElement.IOID
        {
            get
            {
                return _IOID;
            }
        }
        private string _Name;
        private bool Changed = true;
        private string Render;

        /// <summary>Creates an Animation instance.</summary>
        /// <param name="name">The name of the Animation.</param>
        public Animation(string name)
        {
            _Name = name;
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
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder($"@keyframes {_Name} {{");
            foreach (Frame frame in _Frames)
            {
                result.Append(frame);
            }
            Render = result.Append("}").ToString();
            return Render;
        }
    }
}