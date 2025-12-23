using System;

namespace LSL.AddressFormatting;

internal class DelegatingAddressFormatter<T>(Func<T, string> formatter) : IAddressFormatter<T>
{
    public string ToSingleLine(T source) => formatter(source);
}