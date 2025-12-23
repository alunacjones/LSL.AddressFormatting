using System.Collections.Generic;

namespace LSL.AddressFormatting.Tests;

public class AddressBuilderTestCase
{
    public string Description { get; set; }
    public string SectionSeparator { get; set; }
    public string LineSeparator { get; set; }
    public IEnumerable<LineTestCase> Lines { get; set; } = [];
    public string ExpectedResult { get; set; }
    public char? UseTrim { get; set; }
    public bool? UseNoWhitespaceSectionFilter { get; set; }
    public bool UsePerLineSectionTrim { get; set; }
    public bool UseSectionFilterForCheshire { get; set; }
}
