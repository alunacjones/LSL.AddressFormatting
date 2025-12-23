using System;

namespace LSL.AddressFormatting;

/// <summary>
/// The address builder
/// </summary>
public interface IAddressBuilder
{
    /// <summary>
    /// Creates an <see cref="IAddressFormatter{T}"/> instance that accepts an
    /// instance of type <typeparamref name="T"/> and
    /// returns the single line address
    /// </summary>
    /// <param name="configurator">An action to configure the address builder</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A delegate to produce a single line address given an instance of <typeparamref name="T"/></returns>
    public IAddressFormatter<T> CreateFormatter<T>(Action<IBuilderContext<T>> configurator);
}