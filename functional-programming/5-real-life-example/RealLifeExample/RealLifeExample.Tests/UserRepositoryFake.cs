using System;
using System.Threading.Tasks;
using LanguageExt;
using static System.Threading.Tasks.Task;
using static LanguageExt.Prelude;
using static RealLifeExample.Tests.UsersForTests;

namespace RealLifeExample.Tests
{
    public class UserRepositoryFake : IUserRepository
    {
        private readonly Seq<User> _inMemoryData = Seq(Morty, Rick);

        public Task<User> FindByIdAsync(Guid id) =>
            FromResult(
                _inMemoryData
                    .Filter(p => id == p.Id)
                    .SingleOrDefault()
            );
    }
}