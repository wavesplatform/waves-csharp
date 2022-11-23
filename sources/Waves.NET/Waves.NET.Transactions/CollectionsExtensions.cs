namespace Waves.NET.Transactions
{
    public static class CollectionsExtensions
    {
        public static bool ContentEquals<TK, TV>(this IDictionary<TK, TV> o, IDictionary<TK, TV> other)
        {
            if (other == null || o.Count != other.Count) return false;
            if (o.Count == 0 && other.Count == 0) return true;

            if (o.First().Value is not IEnumerable<object>)
                return o.OrderBy(x => x.Key).SequenceEqual(other.OrderBy(x => x.Key));

            if (!o.Keys.SequenceEqual(other.Keys)) return false;

            foreach(var item in o)
                if (!((IEnumerable<object>)item.Value!).SequenceEqual((IEnumerable<object>)other[item.Key]!))
                    return false;

            return true;
        }

        public static bool ContentEquals<T>(this ICollection<T> o, ICollection<T> other)
        {
            if (other == null || o.Count != other.Count) return false;
            return o.SequenceEqual(other);
        }

        public static int CalcHashCode<T>(this ICollection<T> collection)
        {
            var hash = new HashCode();
            foreach(T item in collection)
            {
                if(item is null) continue;
                hash.Add(item.GetHashCode());
            }
            return hash.ToHashCode();
        }
    }
}
