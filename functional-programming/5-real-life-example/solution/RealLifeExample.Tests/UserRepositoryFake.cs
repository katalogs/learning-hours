using System;
using LanguageExt;
using LanguageExt.Common;
using static LanguageExt.Prelude;
using static RealLifeExample.Tests.UsersForTests;

namespace RealLifeExample.Tests
{
    public class UserRepositoryFake : IUserRepository
    {
        private readonly Seq<User> _inMemoryData = Seq(Morty, Rick);

        private Either<Error, User> FindById(Guid id)
            => (_inMemoryData.Any(u => id == u.Id))
                ? Right(_inMemoryData.Single(u => id == u.Id))
                : Left(Error.New($"Unknown user {id}"));

        public EitherAsync<Error, User> FindByIdAsync(Guid id)
            => FindById(id).ToAsync();
    }
}