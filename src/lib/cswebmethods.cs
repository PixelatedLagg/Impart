using System.Threading;
using System.Globalization;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Csweb
{
    static public class CswebMethods
    {
        internal static List<cswebobj> objects = new List<cswebobj>();
        internal static void AddObject(cswebobj _cswebobj) => objects.Add(_cswebobj);
        public static cswebobj FindById(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Object id must not be null or empty!");
            }
            if (objects.Count == 0)
            {
                throw new MissingMemberException("No objects initialized!");
            }
            foreach (cswebobj obj in objects)
            {
                if (obj.id == id)
                {
                    return obj;
                }
            }
            return null;
        }
    }
}