using System;
using FluentAssertions;
using LanguageExt;
using PlayWithFunctors.Persons;
using Xunit;
using static LanguageExt.Prelude;

namespace PlayWithFunctors
{
    public class ListsAreFunctors
    {
        [Fact]
        public void GetFirstNamesOfAllPeople()
        {
            // Replace it, with a transformation method on people.
            var firstNames = Seq<string>();
            var expectedFirstNames = new[] {"Mary", "Bob", "Ted", "Jake", "Barry", "Terry", "Harry", "John"};

            firstNames.Should().BeEquivalentTo(expectedFirstNames);
        }

        [Fact]
        public void GetNamesOfMarySmithsPets()
        {
            var person = GetPersonNamed("Mary Smith");

            // Replace it, with a transformation method on people.
            Seq<string> names = Seq<string>();

            names.Single()
                .Should()
                .Be("Tabby");
        }

        private Person GetPersonNamed(string fullName) =>
            throw new NotImplementedException();

        [Fact]
        public void GetPeopleWithCats()
        {
            // Replace it, with a positive filtering method on people.
            Seq<string> peopleWithCats = Seq<string>();

            peopleWithCats.Should().HaveCount(2);
        }

        [Fact]
        public void TotalPetAge()
        {
            var totalAge = 0L;
            totalAge.Should().Be(17L);
        }

        [Fact]
        public void PetsNameSorted()
        {
            string sortedPetNames = null;

            sortedPetNames.Should()
                .Be("Dolly, Fuzzy, Serpy, Speedy, Spike, Spot, Tabby, Tweety, Wuzzy");
        }

        [Fact]
        public void SortByAge()
        {
            // Create a Seq<int> with ascending ordered age values.
            Seq<int> sortedAgeList = Seq<int>();

            sortedAgeList.Should()
                .HaveCount(4)
                .And
                .BeEquivalentTo(Seq(1, 2, 3, 4));
        }

        [Fact]
        public void Top3OlderPets()
        {
            // Create a Seq<string> with the 3 older pets.
            Seq<string> top3OlderPets = Seq<string>();

            top3OlderPets.Should()
                .HaveCount(3)
                .And
                .BeEquivalentTo(Seq("Spike", "Dolly", "Tabby"));
        }
    }
}