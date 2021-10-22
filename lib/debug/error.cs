using System.IO;
using System;

namespace Csweb
{
    internal class CSWebObjError : Exception
    {
        internal CSWebObjError(string error, cswebobj obj) : base($"{{ Error: ({error}) Trace: ({obj.path}, {obj.cssPath}) }}") {}
    }
    internal class ClassStyleError : Exception
    {
        internal ClassStyleError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class EStyleError : Exception
    {
        internal EStyleError(string error) : base($"{{ Error: ({error}) }}") {}
    }
    internal class IDStyleError : Exception
    {
        internal IDStyleError(string error) : base($"{{ Error: ({error}) }}") {}
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
}