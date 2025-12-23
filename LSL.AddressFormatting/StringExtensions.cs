namespace LSL.AddressFormatting;

internal static class StringExtensions
{
    public static string ToNullIfEmpty(this string source, bool shouldReturnNullIfEmpty) => source.Length == 0 && shouldReturnNullIfEmpty
        ? null
        : source;
}