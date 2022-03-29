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
        public JsonValueType Type { get; }
        public JsonValue()
        {
            Type = JsonValueType.Null;
        }
        public JsonValue(bool b)
		{
			boolValue = b;
			Type = JsonValueType.Boolean;
		}
        public JsonValue(string s)
		{
			stringValue = s ?? throw new ImpartError("String assigned to JsonValue cannot be null!");
			Type = JsonValueType.String;
		}
        public JsonValue(double n)
		{
			numberValue = n;
			Type = JsonValueType.Number;
		}
        public JsonValue(JsonArray a)
		{
			arrayValue = a ?? throw new ImpartError("JsonArray assigned to JsonValue cannot be null!");;
			Type = JsonValueType.Array;
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
				case JsonValueType.Number:
					return numberValue.Equals(other.Number);
				case JsonValueType.String:
					return stringValue.Equals(other.String);
				case JsonValueType.Boolean:
					return boolValue.Equals(other.Bool);
				case JsonValueType.Object:
					return objectValue.Equals(other.objectValue);
				case JsonValueType.Array:
					return arrayValue.Equals(other.arrayValue);
				case JsonValueType.Null:
					return true;
			}
			return false;
        }
        public override int GetHashCode()
		{
			switch (Type)
			{
				case JsonValueType.Number:
					return numberValue.GetHashCode();
				case JsonValueType.String:
					return stringValue.GetHashCode();
				case JsonValueType.Boolean:
					return boolValue.GetHashCode();
				case JsonValueType.Object:
					return objectValue.GetHashCode();
				case JsonValueType.Array:
					return arrayValue.GetHashCode();
				case JsonValueType.Null:
					return JsonValueType.Null.GetHashCode();
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