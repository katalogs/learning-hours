namespace ArchUnit.Kata.Examples.Domain
{
    public class AnAggregate
    {
        private readonly Person person;

        public AnAggregate(Person person)
        {
            this.person = person;
        }
    }
}