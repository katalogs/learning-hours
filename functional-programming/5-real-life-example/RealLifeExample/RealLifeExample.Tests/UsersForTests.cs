using System;

namespace RealLifeExample.Tests
{
    public static class UsersForTests
    {
        public static readonly User Rick = new(Guid.Parse("376510ae-4e7e-11ea-b77f-2e728ce88125"),
            "rick@green.com",
            "Rick",
            "OJljaefp0')");

        public static readonly User Morty = new(Guid.Parse("37651306-4e7e-11ea-b77f-2e728ce88125"),
            "morty@green.com",
            "Morty",
            "àu__udsv09Ll");
    }
}