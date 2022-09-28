using System.Collections.Generic;

namespace Impart
{
    /// <summary>Form field interface.</summary>
    public interface IFormField 
    {
        /// <summary>The external attributes of the IFormField.</summary>
        List<ExtAttr> ExtAttrs { get; set; }

        /// <summary>The attributes of the IFormField.</summary>
        List<Attr> Attrs { get; set; }
    }
}