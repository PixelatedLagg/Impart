using System;
using Impart.Internal;

namespace Impart.Format
{
    public class XmlValue : IEquatable<XmlValue>
    {
        private readonly bool boolValue = default!;

        public bool Bool
        {
            get
            {
				return boolValue;
            }
        }

		private readonly string stringValue = default!;

        public string String
        {
            get
            {
				return stringValue;
            }
        }
		private readonly double numberValue = default!;

        public double Number
        {
            get
            {
				return numberValue;
            }
        }
		private readonly XmlObject objectValue = default!;

        public XmlObject XmlObject
        {
            get
            {
				return objectValue;
            }
        }
		private readonly XmlArray arrayValue = default!;

        public XmlArray XmlArray
        {
            get
            {
				return arrayValue;
            }
        }

        public static readonly XmlValue Null = new XmlValue();

        public ValueType Type { get; }

        public XmlValue()
        {
            Type = ValueType.Null;
        }

        public XmlValue(bool b)
		{
			boolValue = b;
			Type = ValueType.Boolean;
		}

        public XmlValue(string s)
		{
			stringValue = s ?? throw new ImpartError("String assigned to JsonValue cannot be null!");
			Type = ValueType.String;
		}

        public XmlValue(double n)
		{
			numberValue = n;
			Type = ValueType.Number;
		}

        public XmlValue(XmlArray a)
		{
			arrayValue = a ?? throw new ImpartError("XmlArray assigned to XmlValue cannot be null!");;
			Type = ValueType.Array;
		}

        public XmlValue(XmlValue other)
        {
            if (other == null)
            {
                throw new ImpartError("XmlValue to copy from must not be null!");
            }
            arrayValue = other.arrayValue;
			objectValue = other.objectValue;
			numberValue = other.numberValue;
			stringValue = other.stringValue;
			boolValue = other.boolValue;
            Type = other.Type;
        }

        public override string ToString()
        {
            return "";
        }

        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            switch (o)
            {
                case XmlValue xv:
                    return ReferenceEquals(this, xv);
                case XmlArray a:
                    return arrayValue.Equals(a);
                case XmlObject obj:
                    return objectValue.Equals(obj);
                case bool b:
                    return boolValue.Equals(b);
                case string s:
                    return stringValue.Equals(s);
                default:
                    if (Impart.Internal.Number.IsNumber(o))
                    {
                        return numberValue.Equals(Convert.ToDouble(o));
                    }
                    return false;
            }
        }

        public bool Equals(XmlValue other)
        {
			if (other.Type != Type || ReferenceEquals(other, null))
            {
                return false;
            }
			switch (Type)
			{
				case ValueType.Number:
					return numberValue.Equals(other.Number);
				case ValueType.String:
					return stringValue.Equals(other.String);
				case ValueType.Boolean:
					return boolValue.Equals(other.Bool);
				case ValueType.Object:
					return objectValue.Equals(other.objectValue);
				case ValueType.Array:
					return arrayValue.Equals(other.arrayValue);
				case ValueType.Null:
					return true;
			}
			return false;
        }

        public override int GetHashCode()
		{
			switch (Type)
			{
				case ValueType.Number:
					return numberValue.GetHashCode();
				case ValueType.String:
					return stringValue.GetHashCode();
				case ValueType.Boolean:
					return boolValue.GetHashCode();
				case ValueType.Object:
					return objectValue.GetHashCode();
				case ValueType.Array:
					return arrayValue.GetHashCode();
				case ValueType.Null:
					return ValueType.Null.GetHashCode();
			}
			return base.GetHashCode();
		}

        public static implicit operator XmlValue(bool b)
		{
			return new XmlValue(b);
		}

        public static implicit operator XmlValue(string s)
		{
			return s is null ? null : new XmlValue(s);
		}

        public static implicit operator XmlValue(double n)
		{
			return new XmlValue(n);
		}
        
        public static implicit operator XmlValue(XmlObject o)
		{
			return o is null ? null : new XmlValue(o);
		}

        public static implicit operator XmlValue(XmlArray a)
		{
			return a is null ? null : new XmlValue(a);
		}

        public static bool operator ==(XmlValue a, XmlValue b)
		{
			return ReferenceEquals(a, b) || (a != null && a.Equals(b));
		}

        public static bool operator !=(XmlValue a, XmlValue b)
		{
			return !Equals(a, b);
		}
    }
}