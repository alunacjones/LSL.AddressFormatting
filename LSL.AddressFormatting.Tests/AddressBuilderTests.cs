using System.Collections;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileProviders;
using NJsonSchema;
using YamlDotNet.Serialization;

namespace LSL.AddressFormatting.Tests;

public class AddressBuilderTests
{
    [TestCaseSource(nameof(TestCases))]
    public void Build_GivenAConfiguration_ItShouldGenerateTheExpectedResult(AddressBuilderTestCase testCase)
    {
        // Arrange
        var builder = new AddressBuilder();

        // Act
        var result = builder.Build(context => context.BuildFromTestCase(testCase));

        // Assert
        using var assertionScope = new AssertionScope();
        
        result.Should().Be(testCase.ExpectedResult);
    }

    [Test]
    [Ignore("just for the readme file")]
    public void ForDocs()
    {
        var builder = new AddressBuilder();
        var result = builder.Build(c => c
            // The string to separate each line.
            // In this case it is not required as ", "
            // is the default separator
            .WithLineSeparator(", ")
            // The string to separate each section within a line.
            // In this case it is not required as " "
            // is the default separator            
            .WithSectionSeparator(" ")
            // An optional transform for each section
            .WithSectionValueTransform(s => s?.Trim())
            .AddLine(ld => ld.AddSections([
                "123",
                null,
                "",
                "High Street"
            ]))
            .AddLine(ld => ld.AddSections([
            ]))
            .AddLine(ld => ld.AddSections([
                "",
                null
            ]))
            .AddLine(ld => ld.AddSections([
                "Chester"
            ]))
            .AddLine(ld => ld.AddSections([
                    "Cheshire",
                    "more info"
                ])
                .WithSectionSeparator(" - "))
            .AddLine(ld => ld.AddSections(["CH1 3DD"]))
        );
    }

    public static IEnumerable TestCases 
    {
        get 
        {
            var provider = new EmbeddedFileProvider(typeof(AddressBuilderTests).Assembly);
            using var stream = provider
                .GetDirectoryContents("/")
                .First(f => f.Name.EndsWith("Tests.yaml"))
                .CreateReadStream();

            using var reader = new StreamReader(stream);

            var tests = new Deserializer().Deserialize<TestCaseContainer>(reader);
            foreach (var test in tests.Tests)
            {
                yield return new TestCaseData(test)
                    .SetName($"[Build] GIVEN {test.Description} THEN it should return '{test.ExpectedResult}'");
            }
        }
    }

    [Test]
    public void Create_GivenADefinition_ItShouldProduceTheExpectedDelegate()
    {
        var builder = new AddressBuilder();
        var fn = builder.Create<MyType>(c => c
            .WithSectionSeparator(" ")
            .WithLineSeparator(", ")
            .WithSectionValueTransform(s => s?.Trim())
            .AddLine(ld => ld
                .AddSectionProviders([
                    i => i.Name,
                    i => i.Street
                ])
                .WithSectionSeparator(" - ")
            )
            .AddLine(ld => ld.AddSectionProviders([i => i.City]))
            .AddLine(ld => ld.AddSectionProviders([i => i.Postcode]))
        );

        using var scope = new AssertionScope();

        fn(new MyType
        {
            Name = "Als",
            Street = "my street ",
            City = "my city ",
            Postcode = "my postcode"
        }).Should().Be("Als - my street, my city, my postcode");

        fn(new MyType
        {
            Name = "Als",
            Postcode = "my postcode"
        }).Should().Be("Als, my postcode");
    }

    internal class MyType
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
    }
}