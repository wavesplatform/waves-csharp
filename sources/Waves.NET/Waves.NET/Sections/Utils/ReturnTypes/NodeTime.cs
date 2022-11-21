namespace Waves.NET.Utils
{
    public record NodeTime
    {
        public long System { get; init; }
        public long Ntp { get; init; }
    }
}
