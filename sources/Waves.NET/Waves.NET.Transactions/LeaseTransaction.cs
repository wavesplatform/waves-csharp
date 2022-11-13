﻿using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public class LeaseTransaction : Transaction, INonGenesisTransaction
    {
        public const int TYPE = 8;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public string Recipient { get; set; } = null!;
        public long Amount { get; set; }

        public LeaseStatus Status { get; set; } //TODO: check if required here
    }

    public interface ILeaseTransaction : INonGenesisTransaction
    {
        IRecipient Recipient { get; set; }
        long Amount { get; set; }
    }
}