﻿using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IMassTransferTransaction : INonGenesisTransaction
    {
        AssetId? AssetId { get; set; }
        Base58s Attachment { get; set; }
        int TransferCount { get; set; }
        long TotalAmount { get; set; }
        ICollection<Transfer> Transfers { get; set; }
    }
}