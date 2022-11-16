namespace Waves.NET.Transactions
{
    public static class BinaryWriterExtensions
    {
        public static void WriteByte(this BinaryWriter bw, byte val)
        {
            bw.Write(val);
        }
    }
}