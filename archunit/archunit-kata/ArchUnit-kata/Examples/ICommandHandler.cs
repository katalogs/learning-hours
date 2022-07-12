namespace ArchUnit.Kata.Examples
{
    public interface ICommandHandler<TCommand>
        where TCommand : Command
    {
        int Handle(TCommand command);
    }
}