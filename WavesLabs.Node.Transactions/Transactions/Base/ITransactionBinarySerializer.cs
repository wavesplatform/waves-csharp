namespace WavesLabs.Node.Transactions
{
    public interface ITransactionBinarySerializer
    {
        byte[] Serialize(Transaction transaction);
    }
}
