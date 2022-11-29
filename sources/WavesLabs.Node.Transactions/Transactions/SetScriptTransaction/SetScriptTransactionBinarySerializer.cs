using Google.Protobuf;
using Waves;

namespace WavesLabs.Node.Transactions
{
    public class SetScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ISetScriptTransaction)transaction;
            proto.SetScript = new SetScriptTransactionData();
            proto.SetScript.Script = ByteString.FromBase64(tx.Script);
        }
    }
}