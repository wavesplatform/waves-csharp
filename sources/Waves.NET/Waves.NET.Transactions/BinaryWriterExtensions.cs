namespace Waves.NET.Transactions
{
    public static class BinaryWriterExtensions
    {
        public static void WriteByte(this BinaryWriter bw, byte val)
        {
            bw.Write(val);
        }

        public static void WriteInt(this BinaryWriter bw, int val)
        {
            var bytes = BitConverter.GetBytes(val);
            bw.Write(BitConverter.IsLittleEndian ? bytes.Reverse().ToArray() : bytes);
        }

        public static void WriteLong(this BinaryWriter bw, long val)
        {
            var bytes = BitConverter.GetBytes(val);
            bw.Write(BitConverter.IsLittleEndian ? bytes.Reverse().ToArray() : bytes);
        }
    }
}