using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Impart.Format
{
    public class XmlObject : Dictionary<string, XmlValue>
    {
        public new XmlValue this[string key]
		{
			get
            {
                return base[key];
            }
			set
            {
                base[key] = value ?? XmlValue.Null;
            }
		}
        public XmlValue this[int index]
        {
            get
            {
                return base.Values.ElementAt(index);
            }
            set
            {
                base[base.Keys.ElementAt(index)] = value ?? XmlValue.Null;
            }
        }
        public DuplicateOptions DuplicateOptions = DuplicateOptions.Allow;
        public XmlObject() {}
        public XmlObject(IDictionary<string, XmlValue> collection) : base(collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value ?? XmlValue.Null)) {}
        public new void Add(string key, XmlValue value)
		{
            if (base.ContainsKey(key))
            {
                switch (DuplicateOptions)
                {
                    case DuplicateOptions.Allow:
                        base.Add(key, value ?? XmlValue.Null);
                        break;
                    case DuplicateOptions.Ignore:
                        break;
                    case DuplicateOptions.Overwrite:
                        this[key] = value ?? XmlValue.Null;
                        break;
                }
            }
            else
            {
                base.Add(key, value ?? XmlValue.Null);
            }
		}
        public override string ToString()
        {
            return "";
        }
        public new bool Equals(object o)
        {
            if (!(o is XmlObject obj))
            {
                return false;
            }
			return this.All(pair => obj[pair.Key].Equals(pair.Value));
        }
        public new int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}