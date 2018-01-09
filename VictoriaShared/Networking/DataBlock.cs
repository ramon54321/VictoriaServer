using System;
using System.Text;

namespace VictoriaShared.Networking
{
    public class DataBlock
    {
        // Static
        private const ushort HEADER_SIZE = 10;


        // Instance
        public readonly ushort version;
        public readonly uint time;
        public readonly ushort type;
        public readonly string body;

        /**
            Builds initial new block from raw data.
        */
        public DataBlock(ushort version, uint time, ushort type, string body)
        {
            this.version = version;
            this.time = time;
            this.type = type;
            this.body = body;
        }

        /**
            Builds a new data block from an array of bytes gotten from GetBytes().
        */
        public DataBlock(byte[] datablockBytes)
        {
            /*
                2 bytes - Version
                4 bytes - Time (Timeline)
                2 bytes - Type
                2 bytes - Size (Body)
                size bytes - Body
            */

            version = BitConverter.ToUInt16(datablockBytes, 0);
            time = BitConverter.ToUInt32(datablockBytes, 2);
            type = BitConverter.ToUInt16(datablockBytes, 6);

            ushort size = BitConverter.ToUInt16(datablockBytes, 8);

            char[] chars = new char[size];

            int bytesUsed;
            int charsUsed;
            bool completed;

            Decoder decoder = Encoding.ASCII.GetDecoder();
            decoder.Convert(datablockBytes, 10, size, chars, 0, chars.Length, true, out bytesUsed, out charsUsed, out completed);

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                stringBuilder.Append(chars[i]);
            }
            string charString = stringBuilder.ToString();

            body = charString;
        }

        /**
            Gets bytes in the correct form to be sent over network.
        */
        public byte[] GetBytes()
        {
            /*
                2 bytes - Version
                4 bytes - Time (Timeline)
                2 bytes - Type
                2 bytes - Size (Body)
                size bytes - Body
            */

            char[] bodyChars = body.ToCharArray();
            byte[] bodyBytes = new byte[bodyChars.Length];

            int charsUsed;
            int bytesUsed;
            bool completed;

            // -- Encode body string to bytes
            Encoder encoder = Encoding.ASCII.GetEncoder();
            encoder.Convert(bodyChars, 0, bodyChars.Length, bodyBytes, 0, bodyBytes.Length, true, out charsUsed, out bytesUsed, out completed);

            byte[] datablockBytes = new byte[HEADER_SIZE + bodyBytes.Length];
            Array.Copy(bodyBytes, 0, datablockBytes, HEADER_SIZE, bodyBytes.Length);

            // -- Build Header
            byte[] headerBytes = new byte[HEADER_SIZE];
            Array.Copy(BitConverter.GetBytes(version), 0, headerBytes, 0, 2);
            Array.Copy(BitConverter.GetBytes(time), 0, headerBytes, 2, 4);
            Array.Copy(BitConverter.GetBytes(type), 0, headerBytes, 6, 2);
            Array.Copy(BitConverter.GetBytes((ushort)bodyBytes.Length), 0, headerBytes, 8, 2);

            // -- Copy header to datablockBytes
            Array.Copy(headerBytes, 0, datablockBytes, 0, HEADER_SIZE);

            return datablockBytes;
        }

        public override string ToString()
        {
            return "Version: " + version + " Time: " + time + " Type: " + type + " Body: " + body;
        }
    }
}