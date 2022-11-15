using Google.Protobuf;
using System.Text;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public class InvokeScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (IInvokeScriptTransaction)transaction;
            proto.InvokeScript = new InvokeScriptTransactionData();
            proto.InvokeScript.Payments.Add(tx.Payment.Select(x => new AmountProto { Amount_ = x.Amount, AssetId = ByteString.CopyFromUtf8(x.AssetId) }));
            proto.InvokeScript.DApp = tx.DApp.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)tx.DApp).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFromUtf8(((Alias)tx.DApp).Name) };
            proto.InvokeScript.FunctionCall = ByteString.FromStream(CreateFunctionData(tx.Call));
        }

        protected override void SerializeToProofsSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeToSignatureSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        private Stream CreateFunctionData(Call call)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.Write((byte)1);
            bw.Write((byte)9);
            bw.Write((byte)1);
            bw.Write(call.Function.Length);
            bw.Write(Encoding.UTF8.GetBytes(call.Function));

            WriteArgs(bw, call.Args);

            return ms;
        }

        private void WriteArgs(BinaryWriter bw, ICollection<CallArgs> args)
        {
            foreach (var arg in args)
            {
                if (arg.Type == CallArgType.Boolean)
                {
                    var boolVal = (bool)arg.Value;
                    bw.Write(boolVal ? 6 : 7);
                    bw.Write(boolVal ? 1 : 0);
                    continue;
                }

                bw.Write((byte)arg.Type);

                switch (arg.Type)
                {
                    case CallArgType.Integer: bw.Write((long)arg.Value); break;
                    case CallArgType.String:
                        var str = (string)arg.Value;
                        bw.Write(str.Length);
                        bw.Write(str);
                        break;
                    case CallArgType.ByteArray:
                        var bytes = Base64s.Decode((string)arg.Value);
                        bw.Write(bytes.Length);
                        bw.Write(bytes);
                        break;
                    case CallArgType.List:
                        var list = (List<CallArgs>)arg.Value;
                        bw.Write(list.Count);
                        WriteArgs(bw, list);
                        break;
                }
            }
        }
    }
}