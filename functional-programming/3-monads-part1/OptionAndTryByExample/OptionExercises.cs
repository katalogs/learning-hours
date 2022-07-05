using System;
using System.Text;
using FluentAssertions;
using FluentAssertions.LanguageExt;
using LanguageExt;
using Xunit;
using static LanguageExt.Prelude;

namespace OptionAndTryByExample
{
    public class OptionExercises
    {
        private Seq<Option<Person>> _persons =
            Seq(
                None,
                Some(new Person("John", "Doe")),
                Some(new Person("Mary", "Smith")),
                None
            );

        [Fact]
        public void WorkingWithNull()
        {
            // Instantiate a None Option of string
            // Map it to an Upper case function
            // Then it must return the string "Ich bin empty" if empty
            var iamAnOption = Option<string>.None;
            string optionValue = null;


            iamAnOption
                .Should()
                .BeNone();

            optionValue
                .Should()
                .Be("Ich bin empty");
        }

        [Fact]
        public void FindKaradoc()
        {
            // Find Karadoc in the people List or returns Perceval
            var foundPersonLastName = "found";

            foundPersonLastName
                .Should()
                .Be("Perceval");
        }

        [Fact]
        public void FindPersonOrDieTryin()
        {
            // Find a person matching firstName and lastName, throws an ArgumentException if not found
            var firstName = "Rick";
            var lastName = "Sanchez";

            Func<Person> findPersonOrDieTryin = () => null;

            findPersonOrDieTryin
                .Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void ChainCall()
        {
            // Chain calls to the half method 4 times with start as argument
            // For each half append the value to the resultBuilder (side effect)
            var start = 500d;
            var resultBuilder = new StringBuilder();

            var result = Option<double>.Some(0);

            result
                .Should()
                .BeNone();

            resultBuilder
                .Should()
                .Be("250125");
        }

        [Fact]
        public void FilterAListOfPerson()
        {
            // Filter the persons list with only defined persons
            var definedPersons = Seq<Person>();

            definedPersons
                .Should()
                .HaveCount(2);
        }

        private Option<double> Half(double x)
            => x % 2 == 0
                ? Some(x / 2)
                : None;
    }

    public record Person(string FirstName, string LastName);
}