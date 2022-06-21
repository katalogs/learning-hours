using LanguageExt;
using static System.Array;

namespace Anti_Patterns.Blog
{
    public sealed record Article(string Name, string Content, Comment[] Comments)
    {
        public Article(string name, string content)
            : this(name, content, Empty<Comment>())
        {
        }

        public Either<IEnumerable<ValidationError>, Article> AddComment(
            string text,
            string author)
            => AddComment(text, author, DateOnly.FromDateTime(DateTime.Now));

        private Either<IEnumerable<ValidationError>, Article> AddComment(
            string text,
            string author,
            DateOnly creationDate)
        {
            var comment = new Comment(text, author, creationDate);

            return Comments.Contains(comment)
                ? new List<ValidationError> {new("Comment already in the article")}
                : this with {Comments = Comments.Append(comment).ToArray()};
        }
    }
}