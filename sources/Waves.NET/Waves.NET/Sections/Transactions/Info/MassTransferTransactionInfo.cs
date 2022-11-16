﻿using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class MassTransferTransactionInfo : TransactionInfo
    {
        public MassTransferTransactionInfo(MassTransferTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override MassTransferTransaction Transaction => (MassTransferTransaction)base.Transaction;
    }
}