using System.Collections.Generic;
using System.Linq;
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
            var firstNames = Data.People.Map(person => person.FirstName);
            var expectedFirstNames = new[] {"Mary", "Bob", "Ted", "Jake", "Barry", "Terry", "Harry", "John"};

            firstNames.Should().BeEquivalentTo(expectedFirstNames);
        }

        [Fact]
        public void GetNamesOfMarySmithsPets()
        {
            // Replace it, with a transformation method on people.
            var names =
                Data.People
                    .Find(x => x.Named("Mary Smith"))
                    .Bind(mary => mary.Pets)
                    .Map(pet => pet.Name);

            names.Single()
                .Should()
                .Be("Tabby");
        }


        private void GetPetsWithLinQ()
        {
            var withLinQ = Data.People
                .FirstOrDefault(x => x.Named("Mary"))
                .Pets
                .Select(pet => pet.Name);

            var persons = new List<Person>()
            {
                new Person("Mary", "Smith").AddPet(PetType.Cat, "Tabby", 2),
                new Person("Bob", "Smith")
                    .AddPet(PetType.Cat, "Dolly", 3)
                    .AddPet(PetType.Dog, "Spot", 2)
            };

            persons
                .Find(x => x.Named("Mary"))?
                .Pets
                .Map(pet => pet.Name);
        }

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