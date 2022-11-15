namespace Waves.NET.Transactions
{
    public interface ITransactionBinarySerializer
    {
        byte[] Serialize(Transaction transaction);
    }
}
