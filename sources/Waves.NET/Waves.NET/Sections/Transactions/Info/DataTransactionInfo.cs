﻿using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class DataTransactionInfo : TransactionInfo
    {
        public DataTransactionInfo(DataTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) { }

        public override DataTransaction Transaction => (DataTransaction)base.Transaction;
    }
}