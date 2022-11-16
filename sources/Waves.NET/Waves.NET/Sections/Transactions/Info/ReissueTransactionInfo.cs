﻿using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class ReissueTransactionInfo : TransactionInfo
    {
        public ReissueTransactionInfo(ReissueTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override ReissueTransaction Transaction => (ReissueTransaction)base.Transaction;
    }
}