﻿using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class EthereumTransactionInfo : TransactionInfo
    {
        public EthereumTransactionInfo(EthereumTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override EthereumTransaction Transaction => (EthereumTransaction)base.Transaction;

        public bool IsTransferTransaction => Transaction.Payload is EthTransactionTransferPayload;
        public bool IsInvokeTransaction => Transaction.Payload is EthTransactionInvokePayload;

        public T? GetPayload<T>() where T : EthTransactionPayload
        {
            return Transaction.Payload as T;
        }
    }
}