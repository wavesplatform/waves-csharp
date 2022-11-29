namespace WavesLabs.Node.Client.ReturnTypes
{
    public record ScriptMetaResponse
    {
        public string Address { get; set; }
        public ScriptMeta Meta { get; set; }
    }
}