﻿using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record BinaryEntry : DataEntry
    {
        public Base64 Value { get; init; } = null!;

        public BinaryEntry()
        {
            Type = "binary";
        }
    }
}
