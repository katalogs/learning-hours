using System;
using FluentAssertions;
using Xunit;

namespace RealLifeExample.Tests
{
    public class RealLifeExample
    {
        private static readonly Guid BudSpencer = Guid.Parse("376510ae-4e7e-11ea-b77f-2e728ce88125");
        private static readonly Guid UnknownUser = Guid.Parse("376510ae-4e7e-11ea-b77f-2e728ce88121");
        private readonly AccountService _accountService;

        public RealLifeExample()
            => _accountService = new AccountService(
                new UserService(),
                new TwitterService(),
                new BusinessLogger()
            );

        [Fact]
        public void Register_BudSpencer_should_return_a_new_tweet_url() =>
            _accountService
                .Register(BudSpencer)
                .Should()
                .Be("TweetUrl");

        [Fact]
        public void Register_an_unknown_user_should_return_an_error_message() =>
            _accountService
                .Register(UnknownUser)
                .Should().BeNull();
    }
}