public static class Utils{
    public static int mod(this int a, int n)
    {
        return ((a % n) + n) % n;
    }
    public static bool IsSubsetOf<T>(this IEnumerable<T> value, IEnumerable<T> source)
    {
        return value.All(m => source.Contains(m));
    }

    public static bool Match(this string value1, string value2)
    {
        if (value1.Length != value2.Length)
        {
            return false;
        }

        return value1.All(c => value2.Contains(c));
    }

    public static IEnumerable<List<T>> Permute<T>(this IEnumerable<T> cities)
    {
        IEnumerable<List<T>> permutate(IEnumerable<T> reminder, IEnumerable<T> prefix)
        {
            return !reminder.Any()
                ? new List<List<T>> { prefix.ToList() }
                : reminder.SelectMany((c, i) => permutate(
                    reminder.Take(i).Concat(reminder.Skip(i + 1)).ToList(),
                    prefix.Append(c)));
        }
        return permutate(cities, Enumerable.Empty<T>());
    }
}