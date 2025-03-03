namespace LSL.AddressFormatting.Tests;

public static class BuilderContextTestExtensions
{
    public static void BuildFromTestCase(this IBuilderContext source, AddressBuilderTestCase testCase)
    {
        source
            .WithSectionSeparator(testCase.SectionSeparator)
            .WithLineSeparator(testCase.LineSeparator);
        
        if (testCase.UseTrim != null)
        {
            source.WithSectionValueTransform(s => s?.Trim(testCase.UseTrim.Value));
        }
        
        if (testCase.UseNoWhitespaceSectionFilter.GetValueOrDefault(false))
        {
            source.WithSectionFilter(s => !string.IsNullOrWhiteSpace(s));
        }
        
        foreach (var line in testCase.Lines)
        {
            source.AddLine(d => d.AddSections(line.Sections).WithSectionSeparator(line.SectionSeparator));
        }
    }
}
