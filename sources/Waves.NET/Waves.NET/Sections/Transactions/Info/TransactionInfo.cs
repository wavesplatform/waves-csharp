using Newtonsoft.Json;
using Waves.NET.Json;

namespace Waves.NET.Transactions.Info
{
    [JsonConverter(typeof(TransactionInfoJsonConverter))]
    public abstract class TransactionInfo : TransactionWithStatus
    {
        public int Height { get; init; }

        public TransactionInfo(Transaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus)
        {
            Height = height == 0 && Transaction.Type == GenesisTransaction.TYPE ? 1 : height;
        }
    }
}
