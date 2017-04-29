using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    /*
        Multiplatform Binary Class Serialization Format
        Piotr Szuster
        <retsuz@gmail.com>
        www.retsuz.pl
    */
    public abstract class CustomSerialization : ICustomSerialization
    {

        static Encoding enc = Encoding.UTF8;

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        private static byte[] serializeString(String src)
        {
            byte[] strArr = enc.GetBytes(src);
            int length = strArr.Length;
            byte[] lenArr = Converter.toByteArray(length);

            byte[] result = Combine(lenArr, strArr);
            return result;
        }

        private static byte[] serializeObject(Object o)
        {

            byte[] result = null;
            byte[] serialized = null;

            if (o is ICustomSerialization)
            {
                ICustomSerialization serializable = o as ICustomSerialization;
                serialized = serializable.toByteArray();

            }
            else
            {
                serialized = Converter.ObjectToByteArray(o);
            }
            

            int length = serialized.Length;
            byte[] lenArr = Converter.toByteArray(length);
            result = Combine(lenArr, serialized);

            return result;
        }

        private static int getIntLength()
        {
            int tmp = 4;
            return Converter.toByteArray(tmp).Length;
        }

        private static byte[] getValuebySequence(byte[] arr, int startoffset, ref int endoffset)
        {
            int intLength = getIntLength();
            byte[] Len = new byte[intLength];
            byte[] result;
            Buffer.BlockCopy(arr, startoffset, Len, 0, Len.Length);
            int length = Converter.ToInt32(Len);
            result = new byte[length];
            Buffer.BlockCopy(arr, startoffset + Len.Length, result, 0, result.Length);
            endoffset = startoffset + intLength + length;
            return result;
        }

        public static int getSerializedLength(byte [] arr)
        {
            byte[] lengthB = new byte[getIntLength()];
            Buffer.BlockCopy(arr,0,lengthB,0,getIntLength());
            return Converter.ToInt32(lengthB);
        }

        private static Object objectFromByteArray(Type t, byte[] resultarr)
        {
            Object o = Converter.ByteArrayToObject(t, resultarr);

            if(o==null)
            {
                o = Activator.CreateInstance(t);
                if (o is ICustomSerialization)
                {
                    (o as ICustomSerialization).fromByteArray(resultarr);

                }
            }

            return o;
        }

        private static String toCanonicalTypeName(String localTypeName)
        {
            if (localTypeName.Equals(typeof(string).Name)) return "string";
            else if (localTypeName.Equals(typeof(bool).Name)) return "bool";
            else if(localTypeName.Equals(typeof(char).Name)) return "char";
            else if(localTypeName.Equals(typeof(double).Name)) return "double";
            else if (localTypeName.Equals(typeof(short).Name)) return "short";
            else if (localTypeName.Equals(typeof(int).Name)) return "int";
            else if (localTypeName.Equals(typeof(long).Name)) return "long";
            else if (localTypeName.Equals(typeof(float).Name)) return "float";
            else if (localTypeName.Equals(typeof(UInt16).Name)) return "ushort";
            else if (localTypeName.Equals(typeof(UInt32).Name)) return "uint";
            else if (localTypeName.Equals(typeof(UInt64).Name)) return "ulong";
            else if (localTypeName.Equals(typeof(byte[]).Name)) return "byte[]";
            else return localTypeName;
        }

        private static String fromCanonicalTypeName(String canonicalTypeName)
        {
            if (canonicalTypeName.Equals("string")) return typeof(string).Name;
            else if (canonicalTypeName.Equals("bool")) return typeof(bool).Name;
            else if (canonicalTypeName.Equals("char")) return typeof(char).Name;
            else if (canonicalTypeName.Equals("double")) return typeof(double).Name;
            else if (canonicalTypeName.Equals("short")) return typeof(short).Name;
            else if (canonicalTypeName.Equals("int")) return typeof(int).Name;
            else if (canonicalTypeName.Equals("long")) return typeof(long).Name;
            else if (canonicalTypeName.Equals("float")) return typeof(float).Name;
            else if (canonicalTypeName.Equals("ushort")) return typeof(UInt16).Name;
            else if (canonicalTypeName.Equals("uint")) return typeof(UInt32).Name;
            else if (canonicalTypeName.Equals("ulong")) return typeof(UInt64).Name;
            else if (canonicalTypeName.Equals("byte[]")) return typeof(byte[]).Name;
            else return canonicalTypeName;
        }

        public byte[] toByteArray()
        {
            Type currentType = this.GetType();
            String typeName = currentType.Name;
            Byte[] serialized = serializeString(typeName);

            FieldInfo[] fields = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            String fType;
            String fName;

            Byte[] fieldType;
            Byte[] fieldName;
            Byte[] fieldInfo;
            Byte[] fieldValue;
            Byte[] field;
            Byte[] temporary;
            Object Value;

            foreach (FieldInfo f in fields)
            {
                fType = toCanonicalTypeName(f.FieldType.Name);
                fName = f.Name;

                fieldType = serializeString(fType);
                fieldName = serializeString(fName);
                fieldInfo = Combine(fieldType, fieldName);

                Value = f.GetValue(this);
                fieldValue = serializeObject(Value);

                field = Combine(fieldInfo, fieldValue);
                temporary = Combine(serialized, field);
                serialized = new Byte[temporary.Length];
                Buffer.BlockCopy(temporary, 0, serialized, 0, temporary.Length);
                fieldType = null;
                fieldName = null;
                fieldInfo = null;
                fieldValue = null;
                field = null;
                temporary = null;
            }
            int slength = serialized.Length + getIntLength();
            byte[] slengthB = Converter.toByteArray(slength);
            temporary = Combine(slengthB, serialized);
            serialized = null;
            serialized = new Byte[temporary.Length];
            Buffer.BlockCopy(temporary, 0, serialized, 0, temporary.Length);
            temporary = null;
            return serialized;
        }

        public bool fromByteArray(byte[] arr)
        {
            Type currentType = this.GetType();
            FieldInfo[] fields = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            byte[] thisSerialized = arr;
            int startoffset = getIntLength();
            int allen = getSerializedLength(arr);
            int endoffset = 0;
            byte[] resultarr;
            resultarr = getValuebySequence(thisSerialized, startoffset, ref endoffset);
            String serializedClassName = enc.GetString(resultarr);

            if (!serializedClassName.Equals(currentType.Name)) return false;

            startoffset = endoffset;
            String fieldType;
            String fieldName;
            while (startoffset <allen)
            {
                resultarr = getValuebySequence(thisSerialized, startoffset, ref endoffset);
                fieldType = enc.GetString(resultarr);
                startoffset = endoffset;
                resultarr = getValuebySequence(thisSerialized, startoffset, ref endoffset);
                fieldName = enc.GetString(resultarr);
                startoffset = endoffset;
                resultarr = getValuebySequence(thisSerialized, startoffset, ref endoffset);

                FieldInfo f = currentType.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

                if(f!=null)
                {
                   
                    if(fromCanonicalTypeName(fieldType).Equals(f.FieldType.Name))
                    {
                        if (enc.GetString(resultarr).Equals("null")) f.SetValue(this, null);
                        else
                        {
                            Type t = f.FieldType;
                            Object o = null;
                            o = objectFromByteArray(t, resultarr);
                            f.SetValue(this, o);
                        }
                    }

                }
                startoffset = endoffset;
            }
            return true;
        }

        public override string ToString()
        {
            string result = "";
            Type currentType = this.GetType();
            String typeName = currentType.Name;

            result += typeName + " { ";
            FieldInfo[] fields = currentType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo f in fields)
            {
                String val;
                if (f.GetValue(this) != null)
                {
                    val = f.GetValue(this).ToString();
                }
                else
                {
                    val = "null";
                }
                result += f.FieldType.Name + " " + f.Name + " = '" + val + "';\n";
            }
            result += " };\n";
            return result;
        }
    }
}
