namespace Waves.NET.Utils.ReturnTypes
{
    public record NodeTime
    {
        public long System { get; init; }
        public long Ntp { get; init; }
    }
}
