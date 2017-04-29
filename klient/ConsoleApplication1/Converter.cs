using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public abstract class Converter
    {
        static Encoding enc = Encoding.UTF8;

        public static byte[] toSignedByteArray()
        {
            return null;
        }

        public static byte[] toByteArray(bool var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(short var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(int var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(long var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(double var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(float var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(char var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(UInt16 var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(UInt32 var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(UInt64 var)
        {
            return BitConverter.GetBytes(var);
        }

        public static byte[] toByteArray(byte[] var)
        {
            byte[] result = new byte[var.Length];
            Buffer.BlockCopy(var, 0, result, 0, var.Length);
            return result;
        }

        public static byte[] toByteArray(string var)
        {
            return enc.GetBytes(var);
        }

        public static Boolean ToBoolean(byte[] arr)
        {
            return BitConverter.ToBoolean(arr, 0);
        }

        public static short ToInt16(byte[] arr)
        {
            return BitConverter.ToInt16(arr, 0);
        }

        public static int ToInt32(byte[] arr)
        {
            return BitConverter.ToInt32(arr, 0);
        }

        public static long ToInt64(byte[] arr)
        {
            return BitConverter.ToInt64(arr, 0);
        }

        public static double ToDouble(byte[] arr)
        {
            return BitConverter.ToDouble(arr, 0);
        }

        public static float ToSingle(byte[] arr)
        {
            return BitConverter.ToSingle(arr, 0);
        }

        public static char ToChar(byte[] arr)
        {
            return BitConverter.ToChar(arr, 0);
        }

        public static UInt16 ToUInt16(byte[] arr)
        {
            return BitConverter.ToUInt16(arr, 0);
        }

        public static UInt32 ToUInt32(byte[] arr)
        {
            return BitConverter.ToUInt32(arr, 0);
        }

        public static UInt64 ToUInt64(byte[] arr)
        {
            return BitConverter.ToUInt64(arr, 0);
        }

        public static byte[] ToByte(byte[] arr)
        {
            byte[] result = new byte[arr.Length];
            Buffer.BlockCopy(arr, 0, result, 0, arr.Length);
            return result;
        }

        public static string ToString(byte[] arr)
        {
            return enc.GetString(arr);
        }

        public static Object ByteArrayToObject(Type t, byte[] arr)
        {
            Type argumentType = arr.GetType();
            Type resultType = t;
            MethodInfo method = typeof(Converter).GetMethod("To" + t.Name.Replace("[]", String.Empty), new Type[] { argumentType });
            if (method == null) return null;

            Object[] tab = new Object[1];
            tab[0] = arr;
            
            Object result = method.Invoke(null, tab);
            return result;
        }

        public static byte[] ObjectToByteArray(Object o)
        {
            if (o != null)
            {
                Type t = o.GetType();
                MethodInfo method = typeof(Converter).GetMethod("toByteArray", new Type[] { t });
                if (method == null) return toByteArray("null");
                Object[] tab = new Object[1];
                tab[0] = o;

                Object result = method.Invoke(null, tab);

                return result as byte[];
            }
            else
            {
                return toByteArray("null");
            }
        }


    }
}
