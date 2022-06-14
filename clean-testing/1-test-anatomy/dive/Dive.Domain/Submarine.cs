using static Dive.Domain.Commands;

namespace Dive.Domain
{
    public record Submarine(int Aim = 0, int Position = 0, int Depth = 0)
    {
        public Submarine Execute(Command command) =>
            command.Text switch
            {
                Up => this with {Aim = Aim - command.Value},
                Down => this with {Aim = Aim + command.Value},
                Forward => this with {Position = Position + command.Value, Depth = Depth + (command.Value * Aim)},
                _ => throw new ArgumentException($"Unknown command {command.Text}")
            };

        public Submarine Execute(params string[] commands) 
            => commands.Aggregate(this, (submarine, command) => submarine.Execute(command.FromText()));
    }
}
