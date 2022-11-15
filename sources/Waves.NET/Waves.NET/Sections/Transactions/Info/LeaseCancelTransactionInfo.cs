﻿using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class LeaseCancelTransactionInfo : TransactionInfo
    {
        public LeaseCancelTransactionInfo(LeaseCancelTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override LeaseCancelTransaction Transaction => (LeaseCancelTransaction)base.Transaction;
    }
}