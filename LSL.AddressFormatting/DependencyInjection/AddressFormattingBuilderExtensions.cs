using System;
using System.Linq;
using LSL.AddressFormatting;
using Microsoft.Extensions.DependencyInjection.Extensions;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Address formatting builder extensions
/// </summary>
public static class AddressFormattingBuilderExtensions
{
    /// <summary>
    /// Adds an <see cref="IAddressFormatter{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="configurator"></param>
    /// <returns></returns>
    public static IAddressFormattingBuilder AddFormatter<T>(this IAddressFormattingBuilder source, Action<IBuilderContext<T>> configurator)
    {
        source.Services.TryAddSingleton(sp => sp.GetRequiredService<IAddressBuilder>().CreateFormatter(configurator));
        return source;
    }
}