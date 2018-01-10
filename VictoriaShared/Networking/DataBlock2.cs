using System;
using System.Text;

namespace VictoriaShared.Networking
{
    public enum DataBlockFunction : ushort
    {
        Admin_1001_Log = 1001,
        ADMIN = 2000,
        REQUEST = 3000,
        Action_3001_Move = 3001,
        ACTION = 4000,
        Event_4001_Spawn = 4001
    }

    public class DataBlock
    {
        // Static
        private const ushort HEADER_SIZE = 14;

        // Instance
        public readonly ushort version;
        public readonly long time;
        public readonly DataBlockFunction function;
        public readonly string body;

        /**
            Builds initial new block from raw data.
        */
        public DataBlock(ushort version, long time, DataBlockFunction function, string body)
        {
            this.version = version;
            this.time = time;
            this.function = function;
            this.body = body;
        }

        /**
            Builds a new data block from an array of bytes gotten from GetBytes().
        */
        public DataBlock(byte[] datablockBytes)
        {
            /*
                2 bytes - Version
                8 bytes - Time (Timeline)
                2 bytes - Function
                2 bytes - Size (Body)
                size bytes - Body
            */

            version = BitConverter.ToUInt16(datablockBytes, 0);
            time = BitConverter.ToInt64(datablockBytes, 2);
            function = (DataBlockFunction) BitConverter.ToUInt16(datablockBytes, 10);

            ushort size = BitConverter.ToUInt16(datablockBytes, 12);

            char[] chars = new char[size];

            int bytesUsed;
            int charsUsed;
            bool completed;

            Decoder decoder = Encoding.ASCII.GetDecoder();
            decoder.Convert(datablockBytes, HEADER_SIZE, size, chars, 0, chars.Length, true, out bytesUsed, out charsUsed, out completed);

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
                8 bytes - Time (Timeline)
                2 bytes - Function
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
            Array.Copy(BitConverter.GetBytes(time), 0, headerBytes, 2, 8);
            Array.Copy(BitConverter.GetBytes((ushort)function), 0, headerBytes, 10, 2);
            Array.Copy(BitConverter.GetBytes((ushort)bodyBytes.Length), 0, headerBytes, 12, 2);

            // -- Copy header to datablockBytes
            Array.Copy(headerBytes, 0, datablockBytes, 0, HEADER_SIZE);

            return datablockBytes;
        }

        public override string ToString()
        {
            return "Version: " + version + " Time: " + time + " Function: " + function.ToString() + " Body: " + body;
        }
    }
}