using System;
using Impart.Internal;

namespace Impart.Format
{
	/// <summary>Store the value of a Json node.</summary>
    public class JsonValue : IEquatable<JsonValue>
    {
        private readonly bool _Bool = default!;

        /// <value>The Bool value of the JsonValue. (If any)</value>
        public bool Bool
        {
            get
            {
				return _Bool;
            }
        }

		private readonly string _String = default!;

        /// <value>The String value of the JsonValue. (If any)</value>
        public string String
        {
            get
            {
				return _String;
            }
        }
		private readonly double _Double = default!;

        /// <value>The Double value of the JsonValue. (If any)</value>
        public double Double
        {
            get
            {
				return _Double;
            }
        }
		private readonly JsonObject _JsonObject = default!;

        /// <value>The JsonObject value of the JsonValue. (If any)</value>
        public JsonObject JsonObject
        {
            get
            {
				return _JsonObject;
            }
        }
		private readonly JsonArray _JsonArray = default!;

        /// <value>The JsonArray value of the JsonValue. (If any)</value>
        public JsonArray JsonArray
        {
            get
            {
				return _JsonArray;
            }
        }

        /// <value>A null instance of JsonValue.</value>
        public static readonly JsonValue Null = new JsonValue();

        /// <value>The ValueType of the value the JsonValue instance is holding.</value>
        public readonly ValueType Type;

		/// <summary>Creates a null-based JsonValue instance.</summary>
        public JsonValue()
        {
            Type = ValueType.Null;
        }

        /// <summary>Creates a Bool-based JsonValue instance.</summary>
        /// <param name="value">The Bool value to use.</param>
        public JsonValue(bool value)
		{
			_Bool = value;
			Type = ValueType.Boolean;
		}

        /// <summary>Creates a String-based JsonValue instance.</summary>
        /// <param name="value">The String value to use.</param>
        public JsonValue(String value)
		{
			_String = value ?? throw new ImpartError("String assigned to JsonValue cannot be null!");
			Type = ValueType.String;
		}

        /// <summary>Creates a Double-based JsonValue instance.</summary>
        /// <param name="value">The Double value to use.</param>
        public JsonValue(Double value)
		{
			_Double = value;
			Type = ValueType.Double;
		}

        /// <summary>Creates a JsonArray-based JsonValue instance.</summary>
        /// <param name="value">The JsonArray value to use.</param>
        public JsonValue(JsonArray value)
		{
			_JsonArray = value ?? throw new ImpartError("JsonArray assigned to JsonValue cannot be null!");;
			Type = ValueType.Array;
		}

        /// <summary>Creates a JsonValue-based JsonValue instance.</summary>
        /// <param name="value">The JsonValue value to use.</param>
        public JsonValue(JsonValue value)
        {
            if (value == null)
            {
                throw new ImpartError("JsonValue to copy from must not be null!");
            }
            _JsonArray = value._JsonArray;
			_JsonObject = value._JsonObject;
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
                ValueType.Array => _JsonArray.ToString(),
                ValueType.Boolean => _Bool.ToString(),
                ValueType.Double => _Double.ToString(),
                ValueType.Object => _JsonObject.ToString(),
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
                JsonValue v => ReferenceEquals(this, v),
                JsonArray a => _JsonArray.Equals(a),
                JsonObject obj => _JsonArray.Equals(obj),
                bool b => _JsonArray.Equals(b),
                string s => _JsonArray.Equals(s),
                _ => EqualsExtension(o)
            };
        }
		private bool EqualsExtension(object o)
        {
            if (o.IsNumber())
            {
                return _Double.Equals(Convert.ToDouble(o));
            }
            return false;
        }

        /// <summary>Compares the equality of this instance and a JsonValue.</summary>
        /// <param name="value">The JsonValue to compare.</param>
        public bool Equals(JsonValue value)
        {
			if (value.Type != Type || ReferenceEquals(value, null))
            {
                return false;
            }
			return Type switch
			{
				ValueType.Array => _JsonArray.Equals(value._JsonArray),
				ValueType.Boolean => _Bool.Equals(value._Bool),
				ValueType.Double => _Double.Equals(value._Double),
				ValueType.Object => _JsonObject.Equals(value._JsonObject),
				ValueType.String => _String.Equals(value._String),
				_ => false
			};
        }

        /// <summary>Get the hashcode of the current instance.</summary>
        public override int GetHashCode()
		{
			return Type switch
            {
                ValueType.Array => _JsonArray.GetHashCode(),
                ValueType.Boolean => _Bool.GetHashCode(),
                ValueType.Double => _Double.GetHashCode(),
                ValueType.Null => Null.GetHashCode(),
                ValueType.Object => _JsonObject.GetHashCode(),
                ValueType.String => _String.GetHashCode(),
                _ => base.GetHashCode()
            };
		}
        /// <summary>Creates a Bool-based JsonValue instance.</summary>
        /// <param name="value">The Bool value to use.</param>
        public static implicit operator JsonValue(bool value)
		{
			return new JsonValue(value);
		}

        /// <summary>Creates a String-based JsonValue instance.</summary>
        /// <param name="value">The String value to use.</param>
        public static implicit operator JsonValue(string value)
		{
			return value is null ? null : new JsonValue(value);
		}

        /// <summary>Creates a Double-based JsonValue instance.</summary>
        /// <param name="value">The Double value to use.</param>
        public static implicit operator JsonValue(double value)
		{
			return new JsonValue(value);
		}
        
        /// <summary>Creates a JsonObject-based JsonValue instance.</summary>
        /// <param name="value">The JsonObject value to use.</param>
        public static implicit operator JsonValue(JsonObject value)
		{
			return value is null ? null : new JsonValue(value);
		}

        /// <summary>Creates a JsonArray-based JsonValue instance.</summary>
        /// <param name="value">The JsonArray value to use.</param>
        public static implicit operator JsonValue(JsonArray value)
		{
			return value is null ? null : new JsonValue(value);
		}

        /// <summary>Compares the equality of two JsonValues.</summary>
        /// <param name="a">The first JsonValue to compare.</param>
        /// <param name="b">The second JsonValue to compare.</param>
        public static bool operator ==(JsonValue a, JsonValue b)
		{
			return ReferenceEquals(a, b) || (a != null && a.Equals(b));
		}

        /// <summary>Compares the inequality of two JsonValues.</summary>
        /// <param name="a">The first JsonValue to compare.</param>
        /// <param name="b">The second JsonValue to compare.</param>
        public static bool operator !=(JsonValue a, JsonValue b)
		{
			return !Equals(a, b);
		}
    }
}