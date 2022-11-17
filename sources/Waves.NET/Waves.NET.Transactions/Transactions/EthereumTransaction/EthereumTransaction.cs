﻿namespace Waves.NET.Transactions
{
    public class EthereumTransaction : Transaction, IEthereumTransaction
    {
        public const int TYPE = 18;

        public byte[] Bytes { get; set; } = null!;
        public EthTransactionPayload Payload { get; set; } = null!;
    }
}