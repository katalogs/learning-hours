namespace Mutation.Blog
{
    public sealed record Comment(string Text, string Author, DateOnly CreationDate);
}