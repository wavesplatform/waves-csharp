﻿using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class CreateAliasTransactionInfo : TransactionInfo
    {
        public CreateAliasTransactionInfo(CreateAliasTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) { }

        public override CreateAliasTransaction Transaction => (CreateAliasTransaction)base.Transaction;
    }
}