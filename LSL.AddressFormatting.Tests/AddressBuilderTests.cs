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
    public void GivenAConfiguration_ItShouldGenerateTheExpectedResult(AddressBuilderTestCase testCase)
    {
        // Arrange
        var builder = new AddressBuilder();

        // Act
        var result = builder.Build(context => context.BuildFromTestCase(testCase));

        // Assert
        using var assertionScope = new AssertionScope();
        
        result.Should().Be(testCase.ExpectedResult);
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


}