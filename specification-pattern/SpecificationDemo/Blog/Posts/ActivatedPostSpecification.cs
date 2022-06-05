using System.Linq.Expressions;
using Specification;

namespace Blog.Posts
{
    public class ActivatedSpecification : Specification<Post>
    {
        public override Expression<Func<Post, bool>> ToExpression() => post => post.Activated.HasValue;
    }
}