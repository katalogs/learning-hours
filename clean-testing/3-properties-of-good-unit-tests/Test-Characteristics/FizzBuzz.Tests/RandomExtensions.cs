using System;

namespace FizzBuzz.Tests;

internal static class RandomExtensions
{
    public static int ValidInt(Random random)
    {
        while (true)
        {
            var randomInt = random.Next(1, 100);
            if (randomInt % 3 == 0 || randomInt % 5 == 0)
            {
                return randomInt;
            }
        }
    }
}