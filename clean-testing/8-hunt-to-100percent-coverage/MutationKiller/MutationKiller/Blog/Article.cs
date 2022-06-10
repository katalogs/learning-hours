using LanguageExt;
using static LanguageExt.Prelude;

namespace Mutation.Blog
{
    public sealed record Article(string Name, string Content, Seq<Comment> Comments)
    {
        public Article(string name, string content)
            : this(name, content, new List<Comment>().ToSeq())
        {
        }

        public Either<Seq<ValidationError>, Article> AddComment(
            string text,
            string author)
            => AddComment(text, author, DateOnly.FromDateTime(DateTime.Now));

        private Either<Seq<ValidationError>, Article> AddComment(
            string text,
            string author,
            DateOnly creationDate)
        {
            var comment = new Comment(text, author, creationDate);

            return Comments.Contains(comment)
                ? Left(
                    List(new ValidationError("Comment already in the article")).ToSeq()
                )
                : Right(this with {Comments = Comments.Add(comment)});
        }
    }
}