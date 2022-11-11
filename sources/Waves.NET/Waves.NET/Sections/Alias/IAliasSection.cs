namespace Waves.NET.Alias
{
    public interface IAliasSection
    {
        string GetAddressByAlias(string alias);
        ICollection<string> GetAliasesByAddress(string address);
    }
}