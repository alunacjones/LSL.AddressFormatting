using System;

namespace LSL.AddressFormatting;

internal interface ILineDefinition
{
    string SectionSeparator { get; }
    Func<string, string> SectionValueTransformer { get;}
}
