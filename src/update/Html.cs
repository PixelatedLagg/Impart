using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Impart
{

    [InterpolatedStringHandler]
    public ref struct Html
    {
        private readonly StringBuilder Message;
        private readonly (int index, IElement)[] Elements;

        public Html(int length, int count)
        {
            Message = new StringBuilder(length);
            Elements = new (int index, IElement)[count];
        }

        public void AppendLiteral(string s)
        {
            Message.Append(s);
        }

        public void AppendFormatted(IElement e)
        {
            Elements[Array.FindIndex(Elements, item => item == null)]
            Elements.Add((Message.Length - 1, e));
        }

        public void AppendFormatted<T>(T t)
        {
            Message.Append(t);
        }

        public string BuildMessage()
        {
            foreach ((int index, IElement e) in Elements)
            {
                Message.Insert(index, e);
            }
            return Message.ToString();
        }
    }
}