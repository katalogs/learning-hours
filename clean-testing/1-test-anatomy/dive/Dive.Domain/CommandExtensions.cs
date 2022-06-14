namespace Dive.Domain
{

    public static class CommandExtensions
    {
        public static Command FromText(this string text)
        {
            var parameters = text.Split(' ');

            return new Command(parameters[0], int.Parse(parameters[1]));
        }

        public static Command Forward(this int value)
            => new(Commands.Forward, value);

        public static Command Down(this int value)
            => new(Commands.Down, value);

        public static Command Up(this int value)
            => new(Commands.Up, value);
    }

}
