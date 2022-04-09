namespace Impart
{
    /// <summary>The class for a single Frame part of Animation.</summary>
    public struct Frame
    {
        /// <value>The position of the Frame.</value>
        public readonly Percent Position;

        /// <value>The type of Change.</value>
        public readonly ChangeType ChangeType;

        /// <value>The change to take place in the Frame.</value>
        public readonly object Change;
        private string Render;

        /// <summary>Creates a Frame instance.</summary>
        /// <returns>A Frame instance.</returns>
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
                    Change = Measurement.Convert(change);
                    Render = $"{position} {{ width: {Change}; }}";
                    break;
                case ChangeType.Height:
                    Change = Measurement.Convert(change);
                    Render = $"{position} {{ height: {Change}; }}";
                    break;
                case ChangeType.Position:
                    (object x, object y) posChange = ((object, object))change;
                    Measurement x = Measurement.Convert(posChange.x), y = Measurement.Convert(posChange.y);
                    Change = (x, y);
                    Render = $"{position} {{ left: {x}; top: {y}; }}";
                    break;
                case ChangeType.Size:
                    (object x, object y) sizeChange = ((object, object))change;
                    Measurement sx = Measurement.Convert(sizeChange.x), sy = Measurement.Convert(sizeChange.y);
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
        /// <returns>A String instance.</returns>
        public override string ToString() => Render;
    }
}