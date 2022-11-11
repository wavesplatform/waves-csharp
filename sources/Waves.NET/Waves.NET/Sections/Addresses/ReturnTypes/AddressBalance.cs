namespace Waves.NET.Addresses.ReturnTypes
{
    public record AddressBalance
    {
        public string Address { get; init; } = null!;
        public int Confirmations { get; init; }
        public long Balance { get; init; }
    }
}
