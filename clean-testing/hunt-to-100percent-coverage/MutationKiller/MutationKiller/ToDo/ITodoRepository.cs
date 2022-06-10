using LanguageExt;

namespace Mutation.ToDo
{
    public interface ITodoRepository
    {
        Seq<Todo> Search(string text);
    }
}