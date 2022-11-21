namespace Waves.NET.Addresses
{
    public record ScriptMetaResponse
    {
        public string Address { get; set; }
        public ScriptMeta Meta { get; set; }
    }
}