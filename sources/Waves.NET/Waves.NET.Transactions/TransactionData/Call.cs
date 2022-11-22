namespace Waves.NET.Transactions
{
    public record Call
    {
        public string Function { get; init; } = "";
        public ICollection<CallArg> Args { get; init; } = new List<CallArg>();
    }
}