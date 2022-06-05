using System.Linq.Expressions;
using Specification;

namespace Blog.Blogs
{
    public class ActivatedBlogSpecification : Specification<Blog>
    {
        public override Expression<Func<Blog, bool>> ToExpression() 
            => blog => blog.Activated.HasValue && IsAccessible(blog);

        private bool IsAccessible(Blog blog) 
            => !blog.Banned.HasValue 
               && !blog.Removed.HasValue 
               && !blog.Banned.HasValue;
    }
}