﻿namespace Waves.NET.Transactions.Actions
{
    public record SponsorFeeAction
    {
        public string AssetId { get; init; } = null!;
        public int MinSponsoredAssetFee { get; init; }
    }
}