using System;

namespace LSL.AddressFormatting;

/// <summary>
/// Common context actions
/// </summary>
/// <typeparam name="TFluentReturn"></typeparam>
public interface IBaseContext<TFluentReturn>
{
    /// <summary>
    /// The string used to separate each section of a line
    /// </summary>
    /// <param name="sectionSeparator"></param>
    /// <returns></returns>
    TFluentReturn WithSectionSeparator(string sectionSeparator);

    /// <summary>
    /// The string used to separate each line
    /// </summary>
    /// <param name="lineSeparator"></param>
    /// <returns></returns>
    TFluentReturn WithLineSeparator(string lineSeparator);    

    /// <summary>
    /// Allows for the transforming of each section value.
    /// </summary>
    /// <remarks>
    /// Can be used to trim values for example
    /// </remarks>
    /// <param name="sectionTransformer"></param>
    /// <returns></returns>
    TFluentReturn WithSectionValueTransform(Func<string, string> sectionTransformer);

    /// <summary>
    /// Allows for custom section filtering.
    /// </summary>
    /// <remarks>
    /// The default behaviour is to return <c>true</c> if the 
    /// section value is not null and not empty.
    ///</remarks>
    /// <param name="sectionFilter"></param>
    /// <returns></returns>
    TFluentReturn WithSectionFilter(Func<string, bool> sectionFilter);

    /// <summary>
    /// Returns <see langword="null"/> if the result is an empty string
    /// and <paramref name="shouldReturnNullIfEmpty"/> is <see langword="true" />
    /// </summary>
    /// <param name="shouldReturnNullIfEmpty"></param>
    /// <returns></returns>
    TFluentReturn WithNullBeingReturnedIfEmpty(bool shouldReturnNullIfEmpty = true);
}