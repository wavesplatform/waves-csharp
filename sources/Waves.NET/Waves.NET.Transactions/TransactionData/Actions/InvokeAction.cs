namespace Waves.NET.Transactions.Actions
{
    public record InvokeAction
    {
        public string DApp { get; init; } = "";
        public string Function { get; init; } = "";
        public ICollection<Amount> Payments { get; init; } = new List<Amount>();
        public object Error { get; init; }
        public StateChanges StateChanges { get; init; }
    }
}