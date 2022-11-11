﻿namespace Waves.NET.Transactions
{
    public class ExchangeTransaction : Transaction, IExchangeTransaction
    {
        public const int TYPE = 7;
        public const int LatestVersion = 3;
        public const int MinFee = 300000;

        public Order Order1 { get; set; } = null!;
        public Order Order2 { get; set; } = null!;
        public long Amount { get; set; }
        public long Price { get; set; }
        public long BuyMatcherFee { get; set; }
        public long SellMatcherFee { get; set; }
    }

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