using System;

namespace LSL.AddressFormatting;

internal interface ICommonPropertiesAccessor
{
    string SectionSeparator { get; }
    Func<string, bool> SectionFilter { get; }
    string LineSeparator { get; }
    Func<string, string> SectionTransformer { get; }
}
