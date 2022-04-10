using System;

namespace Impart.Format
{
    public class JsonValue : IEquatable<JsonValue>
    {
        private readonly bool boolValue = default!;
		private readonly string stringValue = default!;
		private readonly double numberValue = default!;
		private readonly JsonObject objectValue = default!;
		private readonly JsonArray arrayValue = default!;
        public static readonly JsonValue Null = new JsonValue();
        public bool Bool
        {
            get
            {
				return boolValue;
            }
        }
        public string String
        {
            get
            {
				return stringValue;
            }
        }
        public double Number
        {
            get
            {
				return numberValue;
            }
        }
        public JsonObject JsonObject
        {
            get
            {
				return objectValue;
            }
        }
        public JsonArray JsonArray
        {
            get
            {
				return arrayValue;
            }
        }
        public ValueType Type { get; }
        public JsonValue()
        {
            Type = ValueType.Null;
        }
        public JsonValue(bool b)
		{
			boolValue = b;
			Type = ValueType.Boolean;
		}
        public JsonValue(string s)
		{
			stringValue = s ?? throw new ImpartError("String assigned to JsonValue cannot be null!");
			Type = ValueType.String;
		}
        public JsonValue(double n)
		{
			numberValue = n;
			Type = ValueType.Number;
		}
        public JsonValue(JsonArray a)
		{
			arrayValue = a ?? throw new ImpartError("JsonArray assigned to JsonValue cannot be null!");;
			Type = ValueType.Array;
		}
        public JsonValue(JsonValue other)
        {
            if (other == null)
            {
                throw new ImpartError("JsonValue to copy from must not be null!");
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
            switch (Type)
			{
				case ValueType.Number:
					return numberValue.ToString();
				case ValueType.String:
					return $"\"{stringValue}\"";
				case ValueType.Boolean:
					return boolValue.ToString();
				case ValueType.Object:
					return objectValue.ToString();
				case ValueType.Array:
					return arrayValue.ToString();
			}
            return "\"\"";
        }
        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }
            switch (o)
            {
                case JsonValue jv:
                    return ReferenceEquals(this, jv);
                case JsonArray a:
                    return arrayValue.Equals(a);
                case JsonObject obj:
                    return objectValue.Equals(obj);
                case bool b:
                    return boolValue.Equals(b);
                case string s:
                    return stringValue.Equals(s);
                default:
                    if (IsNumber(o))
                    {
                        return numberValue.Equals(Convert.ToDouble(o));
                    }
                    return false;
            }
        }
        private bool IsNumber(object o)
		{
			return o is double || o is float || o is int || o is uint ||
			o is short || o is ushort || o is byte || o is long ||
			o is ulong;
		}
        public bool Equals(JsonValue other)
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
        public static implicit operator JsonValue(bool b)
		{
			return new JsonValue(b);
		}
        public static implicit operator JsonValue(string s)
		{
			return s is null ? null : new JsonValue(s);
		}
        public static implicit operator JsonValue(double n)
		{
			return new JsonValue(n);
		}
        public static implicit operator JsonValue(JsonObject o)
		{
			return o is null ? null : new JsonValue(o);
		}
        public static implicit operator JsonValue(JsonArray a)
		{
			return a is null ? null : new JsonValue(a);
		}
        public static bool operator ==(JsonValue a, JsonValue b)
		{
			return ReferenceEquals(a, b) || (a != null && a.Equals(b));
		}
        public static bool operator !=(JsonValue a, JsonValue b)
		{
			return !Equals(a, b);
		}
    }
}