using System.Linq.Expressions;
using Specification;

namespace Blog.Posts
{
    public class NotBannedSpecification : Specification<Post>
    {
        public override Expression<Func<Post, bool>> ToExpression() => post => !post.Banned.HasValue;
    }
}