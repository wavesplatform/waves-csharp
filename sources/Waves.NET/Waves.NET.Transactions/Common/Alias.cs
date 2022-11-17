using System.Text;
using System.Text.RegularExpressions;

namespace Waves.NET.Transactions.Common
{
    public class Alias : IRecipient
    {
        public const byte TYPE = 2;
        private const string Alphabet = "-.0-9@_a-z";
        public const string Prefix = "alias:";
        public const int MinLength = 4;
        public const int MaxLength = 30;
        public const int BytesLength = 2 + MaxLength;

        public byte Type => TYPE;
        public byte ChainId => Bytes[1];

        public string Name { get; init; }
        public byte[] Bytes { get; init; }

        public string ToStringWithPrefix() => $"{Prefix}{(char)Bytes[1]}:{Name}";
        public override string ToString() => $"{Name}";

        public Alias(string name) : this(WavesConfig.ChainId, name) { }

        public Alias(byte chainId, string name)
        {
            if(!IsValid(name))
                throw new ArgumentException($"Invalid alias. Must be {MinLength}-{MaxLength} characters long and contains [{Alphabet}] only");

            Name = name.Replace($"{Prefix}{(char)chainId}:", "");
            Bytes = new byte[] { Type, chainId }
                .Concat(BitConverter.GetBytes((short)Name.Length))
                .Concat(Encoding.UTF8.GetBytes(name)).ToArray();
        }

        public static bool IsValid(string alias) => IsValid(WavesConfig.ChainId, alias);

        public static bool IsValid(byte chainId, string alias) =>
            Regex.IsMatch(alias.Replace($"{Prefix}{(char)chainId}:", ""), $"^[{Alphabet}]{{{MinLength},{MaxLength}}}$");

        public static Alias As(string alias) => new Alias(alias);
        public static Alias As(byte chainId, string name) => new Alias(chainId, name);

        public static bool IsAlias(string str) =>
            str.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase) && IsValid(str);

        public static implicit operator string(Alias x) => x.ToString();
        public static explicit operator Alias(string x) => new(x);

        public override int GetHashCode() => ToStringWithPrefix().GetHashCode();
        public override bool Equals(object? obj) => Equals(obj as Alias);
        public bool Equals(Alias? alias) => alias is not null && ToStringWithPrefix().Equals(alias.ToStringWithPrefix(), StringComparison.Ordinal);
    }
}
