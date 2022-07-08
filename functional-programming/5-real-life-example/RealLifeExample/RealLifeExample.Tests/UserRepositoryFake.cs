using System;
using LanguageExt;
using static LanguageExt.Prelude;
using static RealLifeExample.Tests.UsersForTests;

namespace RealLifeExample.Tests
{
    public class UserRepositoryFake : IUserRepository
    {
        private readonly Seq<User> _inMemoryData = Seq(Morty, Rick);

        public User FindById(Guid id) =>
            _inMemoryData
                .Filter(p => id == p.Id)
                .SingleOrDefault();
    }
}