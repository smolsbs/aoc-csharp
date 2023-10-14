namespace Utils;


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

    public static string Stringify(this IEnumerable<char> value) {
        return new string(value.ToArray());
    }

    public static (T v1, T v2) ToTuple<T>( this T[] values){
        if (values.Length != 2){
            throw new ArgumentException("pls no");
        }
        return (values[0], values[1]); 
    }


    public static string ReverseString(this string value){
        return value.Reverse().Stringify();
    }

    public static string JoinStrings(this IEnumerable<string> value, string separator=""){
        return string.Join(separator, value);
    }

    public static T[] GetColumn<T>(this T[,] matrix, int columnNumber){
        return Enumerable.Range(0, matrix.GetLength(0))
                         .Select(y => matrix[y, columnNumber])
                         .ToArray();
    }


    public static T[] GetRow<T>(this T[,] matrix, int rowNumber){
        return Enumerable.Range(0, matrix.GetLength(1))
                         .Select(x => matrix[rowNumber, x])
                         .ToArray();
    }

    public static char ToChar(this string s){
        return s.ToCharArray()[0];
    }
}
