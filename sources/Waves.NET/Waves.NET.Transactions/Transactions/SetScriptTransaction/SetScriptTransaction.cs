namespace Waves.NET.Transactions
{
    public class SetScriptTransaction : Transaction, ISetScriptTransaction
    {
        public const int TYPE = 13;
        public const int LatestVersion = 2;
        public const int MinFee = 1000000;

        public string Script { get; set; } = null!;
    }
}