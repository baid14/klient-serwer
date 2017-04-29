using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class ByteParser
    {
        public static string byteToType(byte[] source)
        {
            //bit 4-7 - długość stringa
            //bit 8-n - string
            //byte[] array = { 0x01, 0x0A, 0x10, 0x0B, 0x06, 0x00, 0x00, 0x00, 0x64, 0x6F, 0x75, 0x62, 0x6C, 0x65 };
            byte[] tmpInt = new byte[4];
            Buffer.BlockCopy(source, 4, tmpInt, 0, 4);
            int size = BitConverter.ToInt32(tmpInt, 0);
            byte[] tmpString = new byte[size];
            Buffer.BlockCopy(source, 8, tmpString, 0, size);
            string type;
            type = Encoding.ASCII.GetString(tmpString);
            //type = BitConverter.ToString(tmpString);
            return type;
        }

    }
}
