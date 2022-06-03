using System.Linq.Expressions;
using Specifications;

namespace Blog.Posts
{
    public class CreatedAfterSpecification : Specification<Post>
    {
        private readonly DateTime _date;

        public CreatedAfterSpecification(DateTime date) => _date = date;

        public override Expression<Func<Post, bool>> ToExpression() => post => post.Created >= _date;
    }
}