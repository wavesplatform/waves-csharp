﻿using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.ReturnTypes
{
    public record TransactionValidationResult
    {
        public bool Valid { get; init; }
        public int ValidationTime { get; init; }
        public ICollection<string> Trace { get; init; } = null!;
        public string? Error { get; init; }
        public Transaction? Transaction { get; init; }
    }
}
