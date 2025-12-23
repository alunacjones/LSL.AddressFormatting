using System;
using Microsoft.Extensions.DependencyInjection;

namespace LSL.AddressFormatting.Tests;

public class AddressFormattingServiceCollectionExtensionsTests
{
    [Test]
    public void GivenAServiceProvider_ItShouldProvideTheExpectedAddressFormatter()
    {
        var provider = BuildServiceProvider();
        var formatter = provider.GetRequiredService<IAddressFormatter<MyAddress>>();
        formatter.ToSingleLine(new()
            {
                Street = "12 the street",
                City = "the city",
                Postcode = "CH12QQ"
            })
            .Should()
            .Be("12 the street, the city, CH12QQ");
    }

    private static IServiceProvider BuildServiceProvider()
    {
        return new ServiceCollection()
            .AddAddressFormatting()
            .AddFormatter<MyAddress>(c => c.AddLine(c => c
                .AddSectionProviders([a => a.Street, a => a.City, a => a.Postcode])
                .WithSectionSeparator(", ")))
            .Services
            .BuildServiceProvider();
    }

    internal class MyAddress
    {
        public string Street { get ;set; }
        public string City { get; set; }
        public string Postcode { get; set; }
    }
}