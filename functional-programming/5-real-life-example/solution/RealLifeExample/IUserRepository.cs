using LanguageExt;
using LanguageExt.Common;

namespace RealLifeExample
{
    public interface IUserRepository
    {
        EitherAsync<Error, User> FindByIdAsync(Guid id);
    }
}