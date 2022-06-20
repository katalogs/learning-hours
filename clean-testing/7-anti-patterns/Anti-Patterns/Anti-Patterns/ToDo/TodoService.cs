namespace Anti_Patterns.ToDo
{
    public class TodoService
    {
        private readonly ITodoRepository _todoRepository;
        public TodoService(ITodoRepository todoRepository) => _todoRepository = todoRepository;

        public IEnumerable<Todo> Search(string text)
            => _todoRepository.Search(text);
    }
}