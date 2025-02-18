[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-addressformatting.svg)](https://ci.appveyor.com/project/alunacjones/lsl-addressformatting)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.AddressFormatting)](https://coveralls.io/github/alunacjones/LSL.AddressFormatting)
[![NuGet](https://img.shields.io/nuget/v/LSL.AddressFormatting.svg)](https://www.nuget.org/packages/LSL.AddressFormatting/)

# LSL.AddressFormatting

A package for building a single address line using simple logic to filter out empty lines and parts of a line.

## Example

```csharp
// Required usings
using LSL.AddressFormatting;

...

var builder = new AddressBuilder();

var singleLineAddress = builder.Build(c => c
    // The string to separate each line.
    // In this case it is not required as ", "
    // is the default separator
    .WithLineSeparator(", ")
    // The string to separate each section within a line.
    // In this case it is not required as " "
    // is the default separator            
    .WithSectionSeparator(" ")
    // The following line will have the two middle items removed
    // since they are null or empty strings
    .AddLine(ld => ld.AddSections([
        "123",
        null,
        "",
        "High Street"
    ]))
    // This line will not be used as it has no sections
    .AddLine(ld => ld.AddSections([
    ]))
    // This line will not be used as all its sections are either 
    // null or empty strings
    .AddLine(ld => ld.AddSections([
        "",
        null
    ]))
    .AddLine(ld => ld.AddSections([
        "Chester"
    ]))
    .AddLine(ld => ld.AddSections([
            "Cheshire",
            "more info"
        ])
        // This is a section separator that
        // is only applied to this ilne
        .WithSectionSeparator(" - "))
    .AddLine(ld => ld.AddSections(["CH1 3DD"]))
);

// singleLineAddress will be "123 High Street, Chester, Cheshire - more info, CH1 3DD"
```

## Example using the `Create<T>` method

The `MyType` class used in the following example is defined as:

```csharp
public class MyType
{
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
}
```

```csharp
// Required usings
using LSL.AddressFormatting;

...

var builder = new AddressBuilder();
var fn = builder.Create<MyType>(c => c
    .WithSectionSeparator(" ")
    .WithLineSeparator(", ")
    .AddLine(ld => ld
        .AddSectionProviders([
            i => i.Name,
            i => i.Street
        ])
        .WithSectionSeparator(" - ")
    )
    .AddLine(ld => ld.AddSectionProviders([i => i.City]))
    .AddLine(ld => ld.AddSectionProviders([i => i.Postcode]))
);

var address1 = fn(new MyType
{
    Name = "Als",
    Street = "my street",
    City = "my city",
    Postcode = "my postcode"
})

// address1 will be "Als - my street, my city, my postcode"

var address2 = fn(new MyType
{
    Name = "Als",
    Postcode = "my postcode"
});

// address2 will be "Als, my postcode"

```