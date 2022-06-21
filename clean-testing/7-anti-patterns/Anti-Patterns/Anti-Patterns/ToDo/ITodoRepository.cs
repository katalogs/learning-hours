namespace Anti_Patterns.ToDo
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> Search(string text);
    }
}