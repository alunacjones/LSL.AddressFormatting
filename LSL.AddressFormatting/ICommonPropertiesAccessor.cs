using System;

namespace LSL.AddressFormatting;

internal interface ICommonPropertiesAccessor
{
    Func<string, bool> SectionFilter { get; }
    string LineSeparator { get; }
    Func<string, string> SectionTransformer { get; }
}
