namespace WavesLabs.Node.Transactions
{
    public interface IExchangeTransaction : INonGenesisTransaction
    {
        Order Order1 { get; set; }
        Order Order2 { get; set; }
        long Amount { get; set; }
        long Price { get; set; }
        long BuyMatcherFee { get; set; }
        long SellMatcherFee { get; set; }
    }
}