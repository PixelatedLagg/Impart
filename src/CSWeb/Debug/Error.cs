using System.IO;
using System;

namespace CSWeb
{
    internal class CSWebObjError : Exception
    {
        internal CSWebObjError(string error, cswebobj obj) : base($"{{ Error: ({error}) Trace: ({obj.path}, {obj.cssPath}) }}") {}
    }
    internal class StyleError : Exception
    {
        internal StyleError(string error, style style) : base($"{{ Error: ({error}) Trace: ({style}) }}") {}
    }
    internal class TemplateError : Exception
    {
        internal TemplateError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ConfigError : Exception
    {
        internal ConfigError(string error) : base($"{{ Error: ({error}) Trace: (config.csweb) }}") {}
    }
    internal class TextError : Exception
    {
        internal TextError(string error, Text text) : base($"{{ Error: ({error}) Trace: ({text}) }}") {}
    }
    internal class ImageError : Exception
    {
        internal ImageError(string error, Image image) : base($"{{ Error: ({error}) Trace: ({image}) }}") {}
    }
    internal class LinkError : Exception
    {
        internal LinkError(string error, Link link) : base($"{{ Error: ({error}) Trace: ({link}) }}") {}
    }
    internal class HeaderError : Exception
    {
        internal HeaderError(string error, Header header) : base($"{{ Error: ({error}) Trace: ({header}) }}") {}
    }
    internal class DivisionError : Exception
    {
        internal DivisionError(string error, Division division) : base($"{{ Error: ({error}) Trace: ({division}) }}") {}
    }
    internal class ButtonError : Exception
    {
        internal ButtonError(string error, Button button) : base($"{{ Error: ({error}) Trace: ({button}) }}") {}
    }
    internal class ColorError : Exception
    {
        internal ColorError(string error, Rgb color) : base($"{{ Error: ({error}) Trace: ({color}) }}") {}
        internal ColorError(string error, Hsl color) : base($"{{ Error: ({error}) Trace: ({color}) }}") {}
    }
    internal class ConversionError : Exception
    {
        internal ConversionError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ListError : Exception
    {
        internal ListError(string error, List list) : base($"{{ Error: ({error}) Trace: ({list}) }}") {}
    }
}