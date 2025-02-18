using System;

namespace LSL.AddressFormatting;

/// <summary>
/// The address builder
/// </summary>
public interface IAddressBuilder
{
    /// <summary>
    /// Configures an address builder then
    /// returns the built address string
    /// </summary>
    /// <param name="configurator">An action to configure the address builder</param>
    /// <returns>The address as a single line string</returns>
    string Build(Action<IBuilderContext> configurator);

    /// <summary>
    /// Creates an address builder delegate that accepts an
    /// instance of type <typeparamref name="T"/> and
    /// returns the single line address
    /// </summary>
    /// <param name="configurator">An action to configure the address builder</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A delegate to produce a single line address given an instance of <typeparamref name="T"/></returns>
    Func<T, string> Create<T>(Action<IBuilderContext<T>> configurator);
}
