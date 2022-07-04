using System.Collections.Immutable;
using System.Linq;
using LanguageExt;

namespace PlayWithFunctors.Persons
{
    public record Person(string FirstName, string LastName, Seq<Pet> Pets)
    {
        public Person(string firstName, string lastName)
            : this(firstName, lastName, Seq<Pet>.Empty)
        {
        }

        public bool Named(string fullName) => fullName.Equals(FirstName + " " + LastName);

        public Person AddPet(PetType petType, string name, int age) =>
            this with {Pets = Pets.Add(new Pet(petType, name, age))};

        public ImmutableDictionary<PetType, int> GetPetTypes() =>
            Pets.GroupBy(p => p.Type)
                .ToDictionary(g => g.Key, g => g.Count())
                .ToImmutableDictionary();

        public bool HasPetType(PetType type) =>
            GetPetTypes()
                .ContainsKey(type);
    }
}