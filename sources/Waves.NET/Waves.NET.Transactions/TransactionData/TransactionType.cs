namespace Waves.NET.Transactions.TransactionData
{
    public enum TransactionType : int
    {
        Genesis = 1,
        Issue = 3,
        Transfer = 4,
        Reissue = 5,
        Burn = 6,
        Exchange = 7,
        Lease = 8,
        LeaseCancel = 9,
        CreateAlias = 10,
        MassTransfer = 11,
        Data = 12,
        SetScript = 13,
        SponsorFee = 14,
        SetAssetScript = 15,
        InvokeScript = 16,
        UpdateAssetInfo = 17,
    }
}
