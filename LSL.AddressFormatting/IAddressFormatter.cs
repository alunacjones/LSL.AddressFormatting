namespace LSL.AddressFormatting;

/// <summary>
/// An address formatter
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAddressFormatter<T>
{
    /// <summary>
    /// Converts an instance of <typeparamref name="T"/> to a single line address.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    string ToSingleLine(T source);
}
