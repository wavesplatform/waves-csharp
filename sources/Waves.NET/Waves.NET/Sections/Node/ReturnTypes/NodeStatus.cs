﻿namespace Waves.NET.Node
{
    public record NodeStatus
    {
        public int BlockchainHeight { get; init; }
        public int StateHeight { get; init; }
        public long UpdatedTimestamp { get; init; }
        public string UpdatedDate { get; init; } = null!;
    }
}
