using System;

namespace LSL.AddressFormatting;

/// <summary>
/// Address builder context
/// </summary>
public interface IBuilderContext
{
    /// <summary>
    /// Adds an address line
    /// </summary>
    /// <param name="definer">An action to configure the <see cref="LineDefinition"/></param>
    /// <returns></returns>
    IBuilderContext AddLine(Action<LineDefinition> definer);

    /// <summary>
    /// The string used to separate each section of a line
    /// </summary>
    /// <param name="sectionSeparator"></param>
    /// <returns></returns>
    IBuilderContext WithSectionSeparator(string sectionSeparator);

    /// <summary>
    /// The string used to separate each line
    /// </summary>
    /// <param name="lineSeparator"></param>
    /// <returns></returns>
    IBuilderContext WithLineSeparator(string lineSeparator);
}