using System;

namespace LSL.AddressFormatting.Tests;

public class GuardTests
{
    [Test]
    public void AssertNotNull_GivenANonNullValue_ItShouldNotThrow()
    {
        new Action(() => Guard.AssertNotNull("no-matter", ""))
            .Should()
            .NotThrow();
    }

    [Test]
    public void AssertNotNull_GivenANullValue_ItShouldNotThrow()
    {
        new Action(() => Guard.AssertNotNull("bad-param", null))
            .Should()
            .Throw<ArgumentException>()
            .WithParameterName("bad-param");
    }    
}