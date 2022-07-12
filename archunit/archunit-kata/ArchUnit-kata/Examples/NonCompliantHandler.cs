using ArchUnit.Kata.Examples.Commands;

namespace ArchUnit.Kata.Examples
{
    public class NonCompliantHandler : ICommandHandler<Order>
    {
        public int Handle(Order command) => 42;
    }
}