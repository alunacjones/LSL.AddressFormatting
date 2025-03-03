using System;

namespace LSL.AddressFormatting;
/// <summary>
/// Address builder context
/// </summary>
public interface IBuilderContext : IBaseContext<IBuilderContext>
{
    /// <summary>
    /// Adds an address line
    /// </summary>
    /// <param name="definer">An action to configure the <see cref="LineDefinition"/></param>
    /// <returns></returns>
    IBuilderContext AddLine(Action<LineDefinition> definer);
}
