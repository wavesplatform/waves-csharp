namespace Waves.NET.Transactions
{
    public record Call
    {
        public string Function { get; init; } = "";
        public ICollection<CallArgs> Args { get; init; } = new List<CallArgs>();
    }
}