using Google.Protobuf;
using System.Text;
using Waves;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class InvokeScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IInvokeScriptTransaction)transaction;
            proto.InvokeScript = new InvokeScriptTransactionData();
            proto.InvokeScript.DApp = tx.DApp.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)tx.DApp).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFromUtf8(((Alias)tx.DApp).Name) };
            proto.InvokeScript.FunctionCall = ByteString.CopyFrom(CreateFunctionData(tx.Call));

            foreach(var p in tx.Payment ?? new List<Amount>())
            {
                proto.InvokeScript.Payments.Add(new AmountProto {
                    Amount_ = p.Value,
                    AssetId = p.AssetId is null ? ByteString.Empty : ByteString.CopyFrom(p.AssetId)
                });
            }
        }

        private byte[] CreateFunctionData(Call call)
        {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            var functionNameBytes = Encoding.UTF8.GetBytes(call.Function);

            bw.WriteByte(1);
            bw.WriteByte(9);
            bw.WriteByte(1);
            bw.WriteInt(functionNameBytes.Length);
            bw.Write(functionNameBytes);

            WriteArgsWithCount(bw, call.Args);

            return ms.ToArray();
        }

        private void WriteArgsWithCount(BinaryWriter bw, ICollection<CallArg> args)
        {
            bw.WriteInt(args.Count);
            foreach (var arg in args)
            {
                if (arg.Type == CallArgType.Boolean)
                {
                    bw.WriteByte((bool)arg.Value ? (byte)6 : (byte)7);
                    continue;
                }

                bw.WriteByte((byte)arg.Type);

                switch (arg.Type)
                {
                    case CallArgType.Integer:
                        bw.WriteLong((long)arg.Value);
                        break;
                    case CallArgType.String:
                        var str = Encoding.UTF8.GetBytes((string)arg.Value);
                        bw.WriteInt(str.Length);
                        bw.Write(str);
                        break;
                    case CallArgType.ByteArray:
                        var bytes = ((Base64s)arg.Value).Bytes;
                        bw.WriteInt(bytes.Length);
                        bw.Write(bytes);
                        break;
                    case CallArgType.List:
                        var list = (List<CallArg>)arg.Value;
                        WriteArgsWithCount(bw, list);
                        break;
                }
            }
        }
    }
}