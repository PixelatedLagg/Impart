using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Impart.Format
{
    /// <summary>Store many Xml nodes.</summary>
    public class XmlObject : Dictionary<string, XmlValue>
    {
        /// <value>Controls how the XmlObject handles duplicate entries.</value>
        public DuplicateOptions DuplicateOptions = DuplicateOptions.Allow;

        /// <summary>Creates a XmlObject instance.</summary>
        public XmlObject() { }

        /// <summary>Creates a XmlObject instance containing all the entries of a Dictionary.</summary>
        /// <param name="collection">The Dictionary to copy the entries from.</param>
        public XmlObject(IDictionary<string, XmlValue> collection) : base(collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value ?? XmlValue.Null)) { }
        
        /// <summary>Gets a XmlValue by key.</summary>
        /// <param name="key">The key to use.</param>
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

        /// <summary>Gets a XmlValue by index.</summary>
        /// <param name="index">The index to use.</param>
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

        /// <summary>Adds a XmlValue and key to the XmlObject.</summary>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The XmlValue to add.</param>
        public new void Add(string key, XmlValue value)
		{
            if (base.ContainsKey(key))
            {
                switch (DuplicateOptions)
                {
                    case DuplicateOptions.Allow:
                        base.Add(key, value ?? XmlValue.Null);
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

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, XmlValue> kvp in this)
            {
                result.Append($"<{kvp.Key}>{kvp.Value}</{kvp.Key}>");
            }
            return result.ToString();
        }

        /// <summary>Compares the equality of this instance and an Object.</summary>
        /// <param name="o">The Object to compare.</param>
        public new bool Equals(object o)
        {
            if (o is XmlObject obj)
            {
                return this.All(pair => obj[pair.Key].Equals(pair.Value));
            }
            return false;
        }

        /// <summary>Get the hashcode of the current instance.</summary>
        public new int GetHashCode() => base.GetHashCode();
    }
}