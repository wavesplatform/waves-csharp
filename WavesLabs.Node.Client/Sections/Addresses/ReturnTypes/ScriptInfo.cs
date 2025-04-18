﻿using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Client.ReturnTypes
{
    public class ScriptInfo : IEquatable<ScriptInfo?>
    {
        public Base64s? Script { get; init; }
        public int Complexity { get; init; }
        public int VerifierComplexity { get; init; }
        public IDictionary<string, int> CallableComplexities { get; init; } = new Dictionary<string, int>();
        public long ExtraFee { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as ScriptInfo is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ScriptInfo);
        }

        public bool Equals(ScriptInfo? other)
        {
            if (other is null) return false;

            var ccNulls = CallableComplexities == null && other.CallableComplexities == null;

            return other is not null &&
                Script == other.Script &&
                Complexity == other.Complexity &&
                VerifierComplexity == other.VerifierComplexity &&
                ExtraFee == other.ExtraFee &&
                (ccNulls || CallableComplexities!.ContentEquals(other.CallableComplexities!));
        }

        public override int GetHashCode() => HashCode.Combine(Script, Complexity, VerifierComplexity, ExtraFee, CallableComplexities);
    }
}
