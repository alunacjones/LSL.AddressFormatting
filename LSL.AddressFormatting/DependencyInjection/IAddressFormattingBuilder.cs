#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Microsoft.Extensions.DependencyInjection;
#pragma warning restore IDE0130 // Namespace does not match folder structure

/// <summary>
/// Address formatting builder
/// </summary>
public interface IAddressFormattingBuilder
{
    /// <summary>
    /// The original <see cref="IServiceCollection"/>
    /// </summary>
    public IServiceCollection Services { get; }
}
