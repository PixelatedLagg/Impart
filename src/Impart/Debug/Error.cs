using System;

namespace Impart
{
    internal class WebPageError : Exception
    {
        internal WebPageError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class StyleError : Exception
    {
        internal StyleError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class TemplateError : Exception
    {
        internal TemplateError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ConfigError : Exception
    {
        internal ConfigError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class TextError : Exception
    {
        internal TextError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ImageError : Exception
    {
        internal ImageError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class LinkError : Exception
    {
        internal LinkError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class HeaderError : Exception
    {
        internal HeaderError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class DivisionError : Exception
    {
        internal DivisionError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ButtonError : Exception
    {
        internal ButtonError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ColorError : Exception
    {
        internal ColorError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ConversionError : Exception
    {
        internal ConversionError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ListError : Exception
    {
        internal ListError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ScrollbarError : Exception
    {
        internal ScrollbarError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class FormError : Exception
    {
        internal FormError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class SizeError : Exception
    {
        internal SizeError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class ScriptingError : Exception
    {
        internal ScriptingError(string error) : base($"{{ Error: ({error}) }}") {}
    }
}