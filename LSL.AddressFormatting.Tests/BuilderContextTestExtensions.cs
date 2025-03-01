namespace LSL.AddressFormatting.Tests;

public static class BuilderContextTestExtensions
{
    public static void BuildFromTestCase(this IBuilderContext source, AddressBuilderTestCase testCase)
    {
        source
            .WithSectionSeparator(testCase.SectionSeparator)
            .WithLineSeparator(testCase.LineSeparator);
        
        foreach (var line in testCase.Lines)
        {
            source.AddLine(d => d.AddSections(line.Sections).WithSectionSeparator(line.SectionSeparator));
        }
    }
}
