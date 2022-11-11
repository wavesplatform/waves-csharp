namespace Waves.NET.Exceptions
{
    public record ApiError
    {
        public int Error { get; init; }
        public string Message { get; init; } = null!;
    }
}
