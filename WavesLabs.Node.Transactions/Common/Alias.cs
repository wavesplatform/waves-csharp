﻿using System.Text;
using System.Text.RegularExpressions;

namespace WavesLabs.Node.Transactions.Common
{
    public class Alias : IRecipient, IEquatable<Alias?>
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

        public string ToFullString() => $"{Prefix}{(char)Bytes[1]}:{Name}";
        public override string ToString() => $"{Name}";

        public Alias(string name) : this(WavesConfig.ChainId, name) { }

        public Alias(byte chainId, string name)
        {
            if(!IsValidName(name))
                throw new ArgumentException($"Invalid alias. Must be {MinLength}-{MaxLength} characters long and contains [{Alphabet}] only");

            Name = name.Replace($"{Prefix}{(char)chainId}:", "");
            Bytes = new byte[] { Type, chainId }
                .Concat(BitConverter.GetBytes((short)Name.Length))
                .Concat(Encoding.UTF8.GetBytes(name)).ToArray();
        }

        public static bool IsValidName(string alias) => IsValidName(WavesConfig.ChainId, alias);

        public static bool IsValidName(byte chainId, string alias) =>
            Regex.IsMatch(alias.Replace($"{Prefix}{(char)chainId}:", ""), $"^[{Alphabet}]{{{MinLength},{MaxLength}}}$");

        public static Alias As(string name) => new Alias(name);
        public static Alias As(byte chainId, string name) => new Alias(chainId, name);

        public static bool IsAlias(string alias) => Regex.Match(alias, $@"^{Prefix}\S:").Success && IsValidName(alias);

        public static implicit operator string(Alias x) => x.ToString();
        public static explicit operator Alias(string x) => new(x);

        public override int GetHashCode() => ToString().GetHashCode();
        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Alias is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as Alias);
        }

        public bool Equals(Alias? alias) =>
            alias is not null && (ReferenceEquals(this, alias) || Name.Equals(alias.Name, StringComparison.Ordinal));

        public static bool operator ==(Alias a, Alias b) => a.Equals(b);
        public static bool operator !=(Alias a, Alias b) => !a.Equals(b);
    }
}
