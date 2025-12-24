using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace LSL.AddressFormatting.Tests;

public class AddressFormattingServiceCollectionExtensionsTests
{
    [Test]
    public void GivenAServiceProvider_ItShouldProvideTheExpectedAddressFormatter()
    {
        var provider = BuildServiceProvider();
        var formatter = provider.GetRequiredService<IAddressFormatter<MyAddress>>();
        var nameFormatter = provider.GetRequiredService<IAddressFormatter<NameParts>>();
        var builder = provider.GetRequiredService<IAddressBuilder>();
        var nameFormatter2 = builder.CreateFormatter<NameParts>(c => c.AddLine(c => c
            .AddSectionProviders([n => n.FirstName, n => n.MiddleNames, n => n.LastName])
        ));

        formatter.ToSingleLine(new()
        {
            Street = "12 the street",
            City = "the city",
            Postcode = "CH12QQ"
        })
            .Should()
            .Be("12 the street, the city, CH12QQ");

        nameFormatter.ToSingleLine(new()
        {
            FirstName = "Al",
            MiddleNames = "",
            LastName = "Jones"
        })
        .Should()
        .Be("Al Jones");

        nameFormatter2.ToSingleLine(new()
        {
            FirstName = "Al",
            MiddleNames = "",
            LastName = "Jones"
        })
        .Should()
        .Be("Al Jones");
    }

    private static IServiceProvider BuildServiceProvider()
    {
        return new ServiceCollection()
            .AddAddressFormatting()
            .AddFormatter<MyAddress>(c => c.AddLine(c => c
                .AddSectionProviders([a => a.Street, a => a.City, a => a.Postcode])
                .WithSectionSeparator(", ")))
            .AddFormatter<NameParts>(c => c.AddLine(c => c
                .AddSectionProviders([n => n.FirstName, n => n.MiddleNames, n => n.LastName])
            ))
            .Services
            .BuildServiceProvider();
    }

    internal class NameParts
    {
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
    }

    internal class MyAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
    }
}