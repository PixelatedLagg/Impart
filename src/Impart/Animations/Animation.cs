using System.Collections.Generic;

namespace Impart
{
    public struct Animation
    {
        private List<Frame> _Frames;
        public List<Frame> Frames
        {
            get
            {
                return _Frames;
            }
        }
        public Animation()
        {
            _Frames = new List<Frame>();
        }
        public Animation AddFrame(Frame frame)
        {
            _Frames.Add(frame);
            return this;
        }
        public Animation AddFrame(Percent position, ChangeType changeType, object change)
        {
            _Frames.Add(new Frame(position, changeType, change));
            return this;
        }
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
    }
}