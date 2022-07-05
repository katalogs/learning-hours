using System;
using FluentAssertions;
using LanguageExt;
using LanguageExt.Common;
using Xunit;
using static LanguageExt.Prelude;

namespace EitherOrTryByExample;

public class EitherExercises
{
    [Fact]
    public void GetTheResultOfDivide()
    {
        // Divide x = 9 by y = 2
        var eitherResult = Divide(9, 2);
        int result = 0;

        result.Should().Be(4);
        eitherResult.IsRight.Should().BeTrue();
        eitherResult.IsLeft.Should().BeFalse();
    }

    [Fact]
    public void MapTheResultOfDivide()
    {
        // Divide x = 9 by y = 2 and add z to the result
        int z = 3;
        int result = 0;

        result.Should().Be(7);
    }

    [Fact]
    public void DivideByZeroIsAlwaysAGoodIdea()
    {
        // Divide x by 0 and get the result
        Func<Either<Error, int>> call = null;
        var result = call.Invoke();

        result.IsLeft.Should().BeTrue();
        //result.LeftUnsafe().Message.Should().Be("Dude, can't divide by 0");
    }

    [Fact]
    public void DivideByZeroOrElse()
    {
        // Divide x by 0, on exception returns 0
        int x = 1;
        int result = -1;

        result.Should().Be(0);
    }

    [Fact]
    public void MapTheFailure()
    {
        // Divide x by 0, log the failure message to the console and get 0
        int x = 1;

        int result = -1;

        result.Should().Be(0);
    }

    [Fact]
    public void ChainTheEither()
    {
        // Divide x by y
        // Chain 2 other calls to divide with x = previous Divide result
        // log the failure message to the console
        // Log your success to the console
        // Get the result or 0 if exception
        int x = 27;
        int y = 3;

        int result = -1;

        result.Should().Be(1);
    }

    private static Either<Error, int> Divide(int x, int y)
        => y == 0 ? Left(Error.New("Dude, can't divide by 0")) : Right(x / y);
}