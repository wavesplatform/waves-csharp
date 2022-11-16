﻿using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class SetScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ISetScriptTransaction)transaction;
            proto.SetScript = new SetScriptTransactionData();
            proto.SetScript.Script = ByteString.CopyFromUtf8(tx.Script);
        }
    }
}