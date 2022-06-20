namespace Anti_Patterns.Blog
{
    public sealed record Comment(string Text, string Author, DateOnly CreationDate);
}