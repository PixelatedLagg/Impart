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
}