using System;
using System.Linq;

namespace LSL.AddressFormatting;

/// <summary>
/// Address builder extensions
/// </summary>
public static class AddressBuilderExtensions
{
    /// <summary>
    /// Creates an address builder delegate that accepts an
    /// instance of type <typeparamref name="T"/> and
    /// returns the single line address
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator">An action to configure the address builder</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A delegate to produce a single line address given an instance of <typeparamref name="T"/></returns>
    public static Func<T, string> Create<T>(this IAddressBuilder source, Action<IBuilderContext<T>> configurator) => 
        source.CreateFormatter(configurator).ToSingleLine;

    /// <summary>
    /// Configures an address builder then
    /// returns the built address string
    /// </summary>
    /// <param name="source"></param>
    /// <param name="configurator">An action to configure the address builder</param>
    /// <returns>The address as a single line string</returns>
    public static string Build(this IAddressBuilder source, Action<IBuilderContext> configurator) => 
        InternalBuilder.InternalBuild(
            new BuilderContext().NullSafeConfigure(configurator),
            ctx => ctx.LineDefinitions.Where(ctx.LineFilter),
            l => l.Sections);
}
