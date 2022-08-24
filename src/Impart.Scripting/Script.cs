using System.Text;
using System.Collections.Generic;

namespace Impart.Scripting
{
    /// <summary>A collection of IFunctions, all called when the WebPage first loads.</summary>
    public class Script
    {
        /// <summary>All of the IFunctions currently registered in the Script.</summary>
        public List<IFunction> Functions = new List<IFunction>();

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            if (Functions.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder($"<script>");
            foreach (IFunction function in Functions)
            {
                builder.Append(function);
            }
            return builder.Append("</script>").ToString();
        }
    }
}