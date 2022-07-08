using LanguageExt;
using static LanguageExt.Prelude;

namespace RealLifeExample
{
    public class UserService
    {
        private readonly Seq<User> _repository =
            Seq(
                new User(Guid.Parse("376510ae-4e7e-11ea-b77f-2e728ce88125"),
                    "bud.spencer@gmail.com",
                    "Bud Spencer",
                    "OJljaefp0')"),
                new User(Guid.Parse("37651306-4e7e-11ea-b77f-2e728ce88125"),
                    "terrence.hill@gmail.com",
                    "Terrence Hill",
                    "àu__udsv09Ll")
            );

        public User FindById(Guid id) =>
            _repository
                .Filter(p => id == p.Id)
                .Single();

        public void UpdateTwitterAccountId(Guid id, string twitterAccountId)
        {
        }
    }
}