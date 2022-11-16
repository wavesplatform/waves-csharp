using Google.Protobuf;
using System.Text;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class InvokeScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IInvokeScriptTransaction)transaction;
            proto.InvokeScript = new InvokeScriptTransactionData();
            proto.InvokeScript.Payments.Add(tx.Payment.Select(x => new AmountProto { Amount_ = x.Amount, AssetId = ByteString.CopyFromUtf8(x.AssetId) }));
            proto.InvokeScript.DApp = tx.DApp.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)tx.DApp).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFromUtf8(((Alias)tx.DApp).Name) };
            proto.InvokeScript.FunctionCall = ByteString.FromStream(CreateFunctionData(tx.Call));
        }

        private Stream CreateFunctionData(Call call)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.WriteByte(1);
            bw.WriteByte(9);
            bw.WriteByte(1);
            bw.Write(call.Function.Length);
            bw.Write(Encoding.UTF8.GetBytes(call.Function));

            WriteArgsWithCount(bw, call.Args);

            return ms;
        }

        private void WriteArgsWithCount(BinaryWriter bw, ICollection<CallArgs> args)
        {
            bw.Write(args.Count);
            foreach (var arg in args)
            {
                if (arg.Type == CallArgType.Boolean)
                {
                    if((bool)arg.Value)
                    {
                        bw.WriteByte(6);
                        bw.WriteByte(1);
                    }
                    else
                    {
                        bw.WriteByte(7);
                        bw.WriteByte(0);
                    }
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
                        WriteArgsWithCount(bw, list);
                        break;
                }
            }
        }
    }
}