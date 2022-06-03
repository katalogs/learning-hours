using System.Linq.Expressions;
using Specifications;

namespace Blog.Posts
{
    public class NotRemovedSpecification : Specification<Post>
    {
        public override Expression<Func<Post, bool>> ToExpression() => post => !post.Removed.HasValue;
    }
}