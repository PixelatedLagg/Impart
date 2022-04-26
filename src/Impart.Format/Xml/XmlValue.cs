using System;

namespace Impart.Format
{
    /// <summary>Store the value of an Xml node.</summary>
    public class XmlValue : IEquatable<XmlValue>
    {
        private readonly bool _Bool = default!;

        /// <value>The Bool value of the XmlValue. (If any)</value>
        public bool Bool
        {
            get
            {
				return _Bool;
            }
        }

		private readonly string _String = default!;

        /// <value>The String value of the XmlValue. (If any)</value>
        public string String
        {
            get
            {
				return _String;
            }
        }
		private readonly double _Double = default!;

        /// <value>The Double value of the XmlValue. (If any)</value>
        public double Double
        {
            get
            {
				return _Double;
            }
        }
		private readonly XmlObject _XmlObject = default!;

        /// <value>The XmlObject value of the XmlValue. (If any)</value>
        public XmlObject XmlObject
        {
            get
            {
				return _XmlObject;
            }
        }
		private readonly XmlArray _XmlArray = default!;

        /// <value>The XmlArray value of the XmlValue. (If any)</value>
        public XmlArray XmlArray
        {
            get
            {
				return _XmlArray;
            }
        }

        /// <value>A null instance of XmlValue.</value>
        public static readonly XmlValue Null = new XmlValue();

        /// <value>The ValueType of the value the XmlValue instance is holding.</value>
        public readonly ValueType Type;

        /// <summary>Creates a null-based XmlValue instance.</summary>
        public XmlValue()
        {
            Type = ValueType.Null;
        }

        /// <summary>Creates a Bool-based XmlValue instance.</summary>
        /// <param name="value">The Bool value to use.</param>
        public XmlValue(bool value)
		{
			_Bool = value;
			Type = ValueType.Boolean;
		}

        /// <summary>Creates a String-based XmlValue instance.</summary>
        /// <param name="value">The String value to use.</param>
        public XmlValue(String value)
		{
			_String = value ?? throw new ImpartError("String assigned to JsonValue cannot be null!");
			Type = ValueType.String;
		}

        /// <summary>Creates a Double-based XmlValue instance.</summary>
        /// <param name="value">The Double value to use.</param>
        public XmlValue(Double value)
		{
			_Double = value;
			Type = ValueType.Double;
		}

        /// <summary>Creates a XmlArray-based XmlValue instance.</summary>
        /// <param name="value">The XmlArray value to use.</param>
        public XmlValue(XmlArray value)
		{
			_XmlArray = value ?? throw new ImpartError("XmlArray assigned to XmlValue cannot be null!");;
			Type = ValueType.Array;
		}

        /// <summary>Creates a XmlValue-based XmlValue instance.</summary>
        /// <param name="value">The XmlValue value to use.</param>
        public XmlValue(XmlValue value)
        {
            if (value == null)
            {
                throw new ImpartError("XmlValue to copy from must not be null!");
            }
            _XmlArray = value._XmlArray;
			_XmlObject = value._XmlObject;
			_Double = value._Double;
			_String = value._String;
			_Bool = value._Bool;
            Type = value.Type;
        }

        /// <summary>Returns the instance as a String.</summary>
        public override string ToString()
        {
            return Type switch
            {
                ValueType.Array => _XmlArray.ToString(),
                ValueType.Boolean => _Bool.ToString(),
                ValueType.Double => _Double.ToString(),
                ValueType.Object => _XmlObject.ToString(),
                ValueType.String => _String,
                _ => ""
            };
        }

        /// <summary>Compares the equality of this instance and an Object.</summary>
        /// <param name="o">The Object to compare.</param>
        public override bool Equals(object o)
        {
            return o switch
            {
                null => false,
                XmlValue v => ReferenceEquals(this, v),
                XmlArray a => _XmlArray.Equals(a),
                XmlObject obj => _XmlArray.Equals(obj),
                bool b => _XmlArray.Equals(b),
                string s => _XmlArray.Equals(s),
                _ => EqualsExtension(o)
            };
        }

        /// <summary>Compares the equality of this instance and a XmlValue.</summary>
        /// <param name="value">The XmlValue to compare.</param>
        public bool Equals(XmlValue value)
        {
			if (value.Type != Type || ReferenceEquals(value, null))
            {
                return false;
            }
			switch (Type)
			{
				case ValueType.Double:
					return _Double.Equals(value._Double);
				case ValueType.String:
					return _String.Equals(value.String);
				case ValueType.Boolean:
					return _Bool.Equals(value.Bool);
				case ValueType.Object:
					return _XmlObject.Equals(value._XmlObject);
				case ValueType.Array:
					return _XmlArray.Equals(value._XmlArray);
				case ValueType.Null:
					return true;
			}
			return false;
        }

        /// <summary>Get the hashcode of the current instance.</summary>
        public override int GetHashCode()
		{
			switch (Type)
			{
				case ValueType.Double:
					return _Double.GetHashCode();
				case ValueType.String:
					return _String.GetHashCode();
				case ValueType.Boolean:
					return _Bool.GetHashCode();
				case ValueType.Object:
					return _XmlObject.GetHashCode();
				case ValueType.Array:
					return _XmlArray.GetHashCode();
				case ValueType.Null:
					return ValueType.Null.GetHashCode();
			}
			return base.GetHashCode();
		}

        /// <summary>Creates a Bool-based XmlValue instance.</summary>
        /// <param name="value">The Bool value to use.</param>
        public static implicit operator XmlValue(bool value)
		{
			return new XmlValue(value);
		}

        /// <summary>Creates a String-based XmlValue instance.</summary>
        /// <param name="value">The String value to use.</param>
        public static implicit operator XmlValue(string value)
		{
			return value is null ? null : new XmlValue(value);
		}

        /// <summary>Creates a Double-based XmlValue instance.</summary>
        /// <param name="value">The Double value to use.</param>
        public static implicit operator XmlValue(double value)
		{
			return new XmlValue(value);
		}
        
        /// <summary>Creates a XmlObject-based XmlValue instance.</summary>
        /// <param name="value">The XmlObject value to use.</param>
        public static implicit operator XmlValue(XmlObject value)
		{
			return value is null ? null : new XmlValue(value);
		}

        /// <summary>Creates a XmlValue-based XmlValue instance.</summary>
        /// <param name="value">The XmlValue value to use.</param>
        public static implicit operator XmlValue(XmlArray value)
		{
			return value is null ? null : new XmlValue(value);
		}

        /// <summary>Compares the equality of two XmlValues.</summary>
        /// <param name="a">The first XmlValue to compare.</param>
        /// <param name="b">The second XmlValue to compare.</param>
        public static bool operator ==(XmlValue a, XmlValue b)
		{
			return ReferenceEquals(a, b) || (a != null && a.Equals(b));
		}

        /// <summary>Compares the inequality of two XmlValues.</summary>
        /// <param name="a">The first XmlValue to compare.</param>
        /// <param name="b">The second XmlValue to compare.</param>
        public static bool operator !=(XmlValue a, XmlValue b)
		{
			return !Equals(a, b);
		}
        private bool EqualsExtension(object o)
        {
            if (Impart.Internal.Number.IsNumber(o))
            {
                return _Double.Equals(Convert.ToDouble(o));
            }
            return false;
        }
    }
}