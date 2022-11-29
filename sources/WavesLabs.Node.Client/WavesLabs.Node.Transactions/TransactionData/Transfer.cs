using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public record Transfer
    {
        public IRecipient Recipient { get; init; } = null!;
        public long Amount { get; init; }

        public static Transfer To(IRecipient recipient, long amount)
        {
            return new Transfer { Recipient = recipient, Amount = amount };
        }
    }
}