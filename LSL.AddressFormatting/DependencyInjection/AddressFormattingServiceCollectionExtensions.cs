using LSL.AddressFormatting;
using LSL.AddressFormatting.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Address formatting service collection extensions
/// </summary>
public static class AddressFormattingServiceCollectionExtensions
{
    /// <summary>
    /// Adds the address formatting services.
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IAddressFormattingBuilder AddAddressFormatting(this IServiceCollection source)
    {
        source.TryAddSingleton<IAddressBuilder, AddressBuilder>();

        return new AddressFormattingBuilder(source);
    }
}
