using System;
using System.Linq;
using System.Collections.Generic;

namespace Impart.Format
{
    public class XmlArray : List<XmlValue>
    {
        public XmlArray() {}
        public XmlArray(IEnumerable<XmlValue> collection)
			: base(collection.Select(jv => jv ?? XmlValue.Null)) {}
        public new void Add(XmlValue item)
		{
			base.Add(item ?? XmlValue.Null);
		}
        public new void AddRange(IEnumerable<XmlValue> collection)
		{
			base.AddRange(collection.Select(v => v ?? XmlValue.Null));
		}
        public override string ToString()
        {
            return "";
        }
        public override bool Equals(object o)
        {
            if (!(o is XmlArray a))
            {
                return false;
            }
            if (!a.ToArray().Equals(base.ToArray()))
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}