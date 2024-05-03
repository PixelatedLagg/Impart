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
        private readonly object[] Variables;
        private readonly int[] Indexes;

        public Html(int length, int count)
        {
            Message = new StringBuilder(length);
            Variables = new object[count];
            Indexes = new int[count];
        }

        public void AppendLiteral(string s)
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (Variables[i] == null)
                {
                    Variables[i] = s;
                    Indexes[i] = Message.Length - 1;
                }
            }
        }

        public void AppendFormatted<T>(T t)
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (Variables[i] == null)
                {
                    Variables[i] = t;
                    Indexes[i] = Message.Length - 1;
                }
            }
        }

        public string BuildMessage()
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                Message.Insert(Indexes[i], Variables[i]);
            }
            return Message.ToString();
        }
    }
}