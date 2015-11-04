using System;
using System.IO;
using System.Collections.Generic;

namespace Voxel
{
    /// <summary>
    /// Container for a value that can be stored in a KeyValueStorage
    /// 
    /// Supports conversions between types via the IConvertable interface
    /// and also has implicit casting to allow for an easier usage
    /// </summary>
    public class Value : IConvertible, IEquatable<Value>
    {
        private TypeCode m_typeCode;
        private object m_value;

        public object Data
        {
            get { return m_value; }
            private set { m_value = value; }
        }

        public static Value Create<T>(T value) where T : IConvertible
        {
            return Activator.CreateInstance(typeof(Value), value) as Value;
        }

        public Value()
        {
            m_typeCode = TypeCode.Empty;
            Data = null;
        }

        public Value(bool value)
        {
            m_typeCode = TypeCode.Boolean;
            Data = value;
        }
        public Value(char value)
        {
            m_typeCode = TypeCode.Char;
            Data = value;
        }
        public Value(DateTime value)
        {
            m_typeCode = TypeCode.DateTime;
            Data = value;
        }
        public Value(decimal value)
        {
            m_typeCode = TypeCode.Decimal;
            Data = value;
        }
        public Value(double value)
        {
            m_typeCode = TypeCode.Double;
            Data = value;
        }
        public Value(short value)
        {
            m_typeCode = TypeCode.Int16;
            Data = value;
        }

        public Value(int value)
        {
            m_typeCode = TypeCode.Int32;
            Data = value;
        }
        public Value(long value)
        {
            m_typeCode = TypeCode.Int64;
            Data = value;
        }

        public Value(sbyte value)
        {
            m_typeCode = TypeCode.SByte;
            Data = value;
        }

        public Value(float value)
        {
            m_typeCode = TypeCode.Single;
            Data = value;
        }

        public Value(string value)
        {
            m_typeCode = TypeCode.String;
            Data = value;
        }

        public Value(UInt16 value)
        {
            m_typeCode = TypeCode.UInt16;
            Data = value;
        }

        public Value(UInt32 value)
        {
            m_typeCode = TypeCode.UInt32;
            Data = value;
        }

        public Value(UInt64 value)
        {
            m_typeCode = TypeCode.UInt64;
            Data = value;
        }

        public static implicit operator bool(Value v)
        {
            if (v.m_typeCode == TypeCode.Boolean)
                return (bool)v.m_value;
            return v.ToBoolean(null);
        }
        public static implicit operator byte(Value v)
        {
            if (v.m_typeCode == TypeCode.Byte)
                return (byte)v.m_value;
            return v.ToByte(null);
        }
        public static implicit operator char(Value v)
        {
            if (v.m_typeCode == TypeCode.Char)
                return (char)v.m_value;
            return v.ToChar(null);
        }
        public static implicit operator DateTime(Value v)
        {
            if (v.m_typeCode == TypeCode.DateTime)
                return (DateTime)v.m_value;
            return v.ToDateTime(null);
        }
        public static implicit operator decimal(Value v)
        {
            if (v.m_typeCode == TypeCode.Decimal)
                return (decimal)v.m_value;
            return v.ToDecimal(null);
        }
        public static implicit operator double(Value v)
        {
            if (v.m_typeCode == TypeCode.Double)
                return (double)v.m_value;
            return v.ToDouble(null);
        }
        public static implicit operator short(Value v)
        {
            if (v.m_typeCode == TypeCode.Int16)
                return (short)v.m_value;
            return v.ToInt16(null);
        }
        public static implicit operator int(Value v)
        {
            if (v.m_typeCode == TypeCode.Int32)
                return (int)v.m_value;
            return v.ToInt32(null);
        }
        public static implicit operator long(Value v)
        {
            if (v.m_typeCode == TypeCode.Int64)
                return (long)v.m_value;
            return v.ToInt64(null);
        }
        public static implicit operator sbyte(Value v)
        {
            if (v.m_typeCode == TypeCode.SByte)
                return (sbyte)v.m_value;
            return v.ToSByte(null);
        }
        public static implicit operator float(Value v)
        {
            if (v.m_typeCode == TypeCode.Single)
                return (float)v.m_value;
            return v.ToSingle(null);
        }
        public static implicit operator string(Value v)
        {
            if (v.m_typeCode == TypeCode.String)
                return (string)v.m_value;
            return v.ToString(null);
        }
        public static implicit operator UInt16(Value v)
        {
            if (v.m_typeCode == TypeCode.UInt16)
                return (UInt16)v.m_value;
            return v.ToUInt16(null);
        }
        public static implicit operator UInt32(Value v)
        {
            if (v.m_typeCode == TypeCode.UInt32)
                return (UInt32)v.m_value;
            return v.ToUInt32(null);
        }
        public static implicit operator UInt64(Value v)
        {
            if (v.m_typeCode == TypeCode.UInt64)
                return (UInt64)v.m_value;
            return v.ToUInt64(null);
        }

        public static implicit operator Value(bool v)
        {
            return new Value(v);
        }
        public static implicit operator Value(char v)
        {
            return new Value(v);
        }
        public static implicit operator Value(DateTime v)
        {
            return new Value(v);
        }
        public static implicit operator Value(decimal v)
        {
            return new Value(v);
        }
        public static implicit operator Value(double v)
        {
            return new Value(v);
        }
        public static implicit operator Value(short v)
        {
            return new Value(v);
        }
        public static implicit operator Value(int v)
        {
            return new Value(v);
        }
        public static implicit operator Value(long v)
        {
            return new Value(v);
        }
        public static implicit operator Value(sbyte v)
        {
            return new Value(v);
        }
        public static implicit operator Value(float v)
        {
            return new Value(v);
        }
        public static implicit operator Value(string v)
        {
            return new Value(v);
        }
        public static implicit operator Value(UInt16 v)
        {
            return new Value(v);
        }
        public static implicit operator Value(UInt32 v)
        {
            return new Value(v);
        }
        public static implicit operator Value(UInt64 v)
        {
            return new Value(v);
        }

        public TypeCode GetTypeCode()
        {
            return m_typeCode;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(m_value, provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(m_value, provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(m_value, provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(m_value, provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(m_value, provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(m_value, provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(m_value, provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(m_value, provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(m_value, provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(m_value, provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(m_value, provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return Convert.ToString(m_value, provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            if (Type.GetTypeCode(conversionType) == GetTypeCode())
            {
                return m_value;
            }
            else
            {
                return null;
            }
        }

        public T ToType<T>() where T : IConvertible
        {
            return (T)ToType(typeof(T), null);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(m_value, provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(m_value, provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(m_value, provider);
        }


        public bool Equals(Value other)
        {
            return m_value.Equals(other.m_value);
        }

        public override bool Equals(object other)
        {
            if (other is Value)
                return Equals(other as Value);
            return m_value.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return m_value.ToString();
        }
    }
}
