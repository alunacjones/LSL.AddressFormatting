using System;

namespace LSL.AddressFormatting;

internal static class SafeConfigureExtensions
{
    public static TContext NullSafeConfigure<TContext, TAbstractContext>(this TContext source, Action<TAbstractContext> configurator)
        where TContext : TAbstractContext
    {
        configurator?.Invoke(source);
        return source;
    }
}