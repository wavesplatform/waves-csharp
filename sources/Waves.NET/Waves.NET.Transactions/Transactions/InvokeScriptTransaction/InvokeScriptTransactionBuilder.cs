﻿using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class InvokeScriptTransactionBuilder : TransactionBuilder<InvokeScriptTransactionBuilder, InvokeScriptTransaction>
    {
        public InvokeScriptTransactionBuilder() : base(InvokeScriptTransaction.LatestVersion, InvokeScriptTransaction.MinFee, InvokeScriptTransaction.TYPE) { }

        public InvokeScriptTransactionBuilder(IRecipient dApp, ICollection<Payment> payment, Call call) : this()
        {
            Transaction.DApp = dApp;
            Transaction.Call = call;
            Transaction.Payment = payment ?? new List<Payment>();
        }

        public static InvokeScriptTransactionBuilder Params(IRecipient dApp, ICollection<Payment> payment, Call call)
        {
            return new InvokeScriptTransactionBuilder(dApp, payment, call);
        }

        public static InvokeScriptTransactionBuilder Params(IRecipient dApp, Call call)
        {
            return new InvokeScriptTransactionBuilder(dApp, new List<Payment>(), call);
        }
    }
}