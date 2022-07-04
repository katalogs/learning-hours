# Functions are Functors

## Without Language-Ext

We start with a failing test and implement the method as simplest as possible:

```c#
private static int Double(int y) => Multiply(y, 2);
private static int Add1(int x) => Add(x, 1);

private int Add1AndDouble(int x)
{
    var y = Add1(x);
    return Double(y);
}

[Fact]
public void Add1AndDoubleIt()
{
    // Create an Add1 function based on Add function
    // Create a Double function based on Multiply
    // Compose the 2 functions together to implement the Add1AndDouble function
    Add1AndDouble(2)
        .Should()
        .Be(6);
}
```

We want to use `functors` here so let's define a `Map` function:

```c#
private int Add1AndDouble(int x)
    => Add1(x).Map(Double);
    
// We create an extension on int for this purpose
static class IntExtensions
{
    public static int Map(this int x, Func<int, int> function) => function(x);
}
```

For the second implementation let's just use function chaining

```c#
[Fact]
public void AddXAndDoubleItInDouble()
{
    // Use the Double and the Add function to implement it
    AddXtoYAndDouble(2, 7)
        .Should()
        .Be(18);
}

private int AddXtoYAndDouble(int x, int y) => Double(Add(x, y));
```

## With Language-Ext

We start by defining the `functions`

```c#
private static readonly Func<int, int> Add1 = x => Add(x, 1);
private static readonly Func<int, int> Double = x => Multiply(x, 2);
```

Then we compose them through the `Compose` method:

```c#
[Fact]
public void Add1AndDoubleIt()
{
    // Create an Add1 function based on Add function
    // Create a Double function based on Multiply
    // Compose the 2 functions together to implement the Add1AndDouble function

    int Add1AndDouble(int x) => Add1.Compose(Double)(x);
    Add1AndDouble(2).Should().Be(6);
}
```

For the other test it is the same implementation:

```c#
[Fact]
public void AddXtoYAndDoubleIt()
{
    // Use the Double and the Add function to implement it
    int AddXtoYAndDouble(int x, int y) => Double(Add(x, y));
    AddXtoYAndDouble(2, 5).Should().Be(14);
}
```

## Lists are functors

Here is the result, observe the `Bind` function -> monadic binding (`flatMap` in other languages)

```c#
using System;
using FluentAssertions;
using PlayWithFunctors.Persons;
using Xunit;
using static LanguageExt.Prelude;
using static PlayWithFunctors.Persons.Data;
using static PlayWithFunctors.Persons.PetType;

namespace PlayWithFunctors
{
    public class ListsAreFunctors
    {
        [Fact]
        public void GetFirstNamesOfAllPeople()
        {
            // Replace it, with a transformation method on people.
            var firstNames = People.Map(p => p.FirstName);
            var expectedFirstNames = Seq("Mary", "Bob", "Ted", "Jake", "Barry", "Terry", "Harry", "John");

            firstNames.Should().BeEquivalentTo(expectedFirstNames);
        }

        [Fact]
        public void GetNamesOfMarySmithsPets()
        {
            var person = GetPersonNamed("Mary Smith");

            // Replace it, with a transformation method on people.
            var names = person.Pets.Map(p => p.Name);

            names.Single()
                .Should()
                .Be("Tabby");
        }

        private Person GetPersonNamed(string fullName) =>
            People.Find(p => p.Named(fullName))
                .IfNone(() => throw new ArgumentException("Can't find person named: " + fullName));

        [Fact]
        public void GetPeopleWithCats()
        {
            // Replace it, with a positive filtering method on people.
            var peopleWithCats = People.Filter(p => p.HasPetType(Cat));

            peopleWithCats.Should().HaveCount(2);
        }

        [Fact]
        public void TotalPetAge()
        {
            var totalAge = People.Bind(p => p.Pets)
                .Map(pet => pet.Age)
                .Sum();

            totalAge.Should().Be(17);
        }

        [Fact]
        public void PetsNameSorted()
        {
            var sortedPetNames =
                People.Bind(p => p.Pets)
                    .Map(pet => pet.Name)
                    .OrderBy(s => s)
                    .ToSeq()
                    .ToFullString();

            sortedPetNames.Should()
                .Be("Dolly, Fuzzy, Serpy, Speedy, Spike, Spot, Tabby, Tweety, Wuzzy");
        }

        [Fact]
        public void SortByAge()
        {
            // Create a Seq<int> with ascending ordered age values.
            var sortedAgeList = People.Bind(p => p.Pets)
                .Map(pet => pet.Age)
                .Distinct()
                .OrderBy(a => a)
                .ToSeq();

            sortedAgeList.Should()
                .HaveCount(4)
                .And
                .BeEquivalentTo(Seq(1, 2, 3, 4));
        }

        [Fact]
        public void Top3OlderPets()
        {
            // Create a Seq<string> with the 3 older pets.
            var top3OlderPets =
                People.Bind(p => p.Pets)
                    .OrderByDescending(pet => pet.Age)
                    .Map(pet => pet.Name).ToSeq()
                    .Take(3);

            top3OlderPets.Should()
                .HaveCount(3)
                .And
                .BeEquivalentTo(Seq("Spike", "Dolly", "Tabby"));
        }
    }
}
```