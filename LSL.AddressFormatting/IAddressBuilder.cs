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
}
