using Waves.NET.Transactions.Info;

namespace Waves.NET.Blocks.ReturnTypes
{
    public record Block : BlockHeaders
    {
        public ICollection<TransactionInfo> Transactions { get; init; } = null!;
    }
}
