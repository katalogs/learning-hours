using LanguageExt;
using static LanguageExt.Prelude;

namespace PlayWithFunctors.Persons
{
    public static class Data
    {
        public static Seq<Person> People =
            Seq(
                new Person("Mary", "Smith").AddPet(PetType.Cat, "Tabby", 2),
                new Person("Bob", "Smith")
                    .AddPet(PetType.Cat, "Dolly", 3)
                    .AddPet(PetType.Dog, "Spot", 2),
                new Person("Ted", "Smith").AddPet(PetType.Dog, "Spike", 4),
                new Person("Jake", "Snake").AddPet(PetType.Snake, "Serpy", 1),
                new Person("Barry", "Bird").AddPet(PetType.Bird, "Tweety", 2),
                new Person("Terry", "Turtle").AddPet(PetType.Turtle, "Speedy", 1),
                new Person("Harry", "Hamster")
                    .AddPet(PetType.Hamster, "Fuzzy", 1)
                    .AddPet(PetType.Hamster, "Wuzzy", 1),
                new Person("John", "Doe")
            );
    }
}