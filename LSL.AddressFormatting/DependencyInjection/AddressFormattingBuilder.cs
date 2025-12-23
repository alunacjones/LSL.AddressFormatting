using Microsoft.Extensions.DependencyInjection;

namespace LSL.AddressFormatting.DependencyInjection;

internal class AddressFormattingBuilder(IServiceCollection services) : IAddressFormattingBuilder
{
    public IServiceCollection Services => services;
}