﻿namespace WavesLabs.Node.Transactions.Common
{
    public record TypeValuePair<TType, TVaue>
    {
        public TType Type { get; set; }
        public TVaue? Value { get; set; }
    }
}