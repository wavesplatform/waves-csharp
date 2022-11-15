﻿using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public interface INonGenesisTransaction : ITransaction
    {
        IRecipient Sender { get; set; }
        PublicKey SenderPublicKey { get; set; }
        string ApplicationStatus { get; set; }
        int Version { get; set; }
        ICollection<Base58s> Proofs { get; set; }
        Base58s? FeeAssetId { get; set; }
    }
}