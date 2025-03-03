using System;

namespace LSL.AddressFormatting;

/// <summary>
/// The builder context used for the <see cref="AddressBuilder.Create{T}(Action{IBuilderContext{T}})"/> method
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBuilderContext<T> : IBaseContext<IBuilderContext<T>>
{
    /// <summary>
    /// Adds an address line
    /// </summary>
    /// <param name="definer">An action to configure the <see cref="LineDefinition{T}"/></param>
    /// <returns></returns>
    IBuilderContext<T> AddLine(Action<LineDefinition<T>> definer);
}