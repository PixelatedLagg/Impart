namespace Impart
{
    /// <summary>The class for a single Frame in an Animation.</summary>
    public struct Frame
    {
        /// <summary>The position of the Frame.</summary>
        public readonly Percent Position;

        /// <summary>The type of Change.</summary>
        public readonly ChangeType ChangeType;

        /// <summary>The change to take place in the Frame.</summary>
        public readonly object Change;
        private string Render;

        /// <summary>Creates a Frame instance.</summary>
        /// <param name="position">The position value.</param>
        /// <param name="changeType">The type of Change.</param>
        /// <param name="change">The change to take place.</param>
        public Frame(Percent position, ChangeType changeType, object change)
        {
            switch (changeType)
            {
                case ChangeType.BackgroundColor:
                    Change = Color.Convert(change);
                    Render = $"{position} {{ background-color: {Change}; }}";
                    break;
                case ChangeType.ForegroundColor:
                    Change = Color.Convert(change);
                    Render = $"{position} {{ foreground-color: {Change}; }}";
                    break;
                case ChangeType.Width:
                    Change = Length.Convert(change);
                    Render = $"{position} {{ width: {Change}; }}";
                    break;
                case ChangeType.Height:
                    Change = Length.Convert(change);
                    Render = $"{position} {{ height: {Change}; }}";
                    break;
                case ChangeType.Position:
                    (object x, object y) posChange = ((object, object))change;
                    Length x = Length.Convert(posChange.x), y = Length.Convert(posChange.y);
                    Change = (x, y);
                    Render = $"{position} {{ left: {x}; top: {y}; }}";
                    break;
                case ChangeType.Size:
                    (object x, object y) sizeChange = ((object, object))change;
                    Length sx = Length.Convert(sizeChange.x), sy = Length.Convert(sizeChange.y);
                    Change = (sx, sy);
                    Render = $"{position} {{ width: {sx}; height: {sy}; }}";
                    break;
                default:
                    Change = "";
                    Render = "";
                    break;
            }
            Position = position;
            ChangeType = changeType;
            Change = change;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString() => Render;
    }
}