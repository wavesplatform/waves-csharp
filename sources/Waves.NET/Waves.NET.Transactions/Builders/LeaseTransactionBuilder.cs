using Google.Protobuf;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions.Builders
{
    public class LeaseTransactionBuilder : TransactionBuilder<LeaseTransactionBuilder, LeaseTransaction>
    {
        public LeaseTransactionBuilder() : base(LeaseTransaction.LatestVersion, LeaseTransaction.MinFee, LeaseTransaction.TYPE) { }

        public LeaseTransactionBuilder(string recipient, long amount) : this()
        {
            Transaction.Recipient = recipient;
            Transaction.Amount = amount;
        }

        public static LeaseTransactionBuilder Data(string recipient, long amount)
        {
            return new LeaseTransactionBuilder(recipient, amount);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (ILeaseTransaction)Transaction;
            proto.Lease = new LeaseTransactionData();
            proto.Lease.Amount = tx.Amount;
            proto.Lease.Recipient = proto.Transfer.Recipient = tx.Recipient.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)tx.Recipient).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFromUtf8(((Alias)tx.Recipient).Name) };
        }
    }
}