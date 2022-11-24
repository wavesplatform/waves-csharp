using Waves.NET.Transactions;

namespace Waves.NET.ReturnTypes
{
    public record ScriptEvaluationExpression
    {
        public string Expr { get; init; } = "";
        public string Id { get; init; } = "";
        public long Fee { get; init; }
        public string? FeeAssetId { get; init; }
        public string Sender { get; init; } = "";
        public string SenderPublicKey { get; init; } = "";
        public ICollection<Amount> Payment { get; init; } = new List<Amount>();
    }
}