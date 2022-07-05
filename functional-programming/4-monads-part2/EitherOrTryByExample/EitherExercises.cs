using FluentAssertions;
using LanguageExt;
using LanguageExt.Common;
using Xunit;
using Xunit.Abstractions;
using static LanguageExt.Prelude;

namespace EitherOrTryByExample;

public class EitherExercises
{
    private readonly ITestOutputHelper _testOutputHelper;

    private static Either<Error, int> Divide(int x, int y)
        => y == 0 ? Left(Error.New("Dude, can't divide by 0")) : Right(x / y);

    public EitherExercises(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Fact]
    public void MapTheResultOfDivide()
    {
        // Divide x = 9 by y = 2 and add z to the result
        var z = 3;
        var result = 0;

        result.Should().Be(7);
    }

    [Fact]
    public void ChainTheEither()
    {
        // Divide x by y
        // Chain 2 other calls to divide with x = previous Divide result
        // log the failure message to the console
        // Log your success to the console
        // Get the result or 0 if exception
        var x = 27;
        var y = 3;

        var result = -1;

        result.Should().Be(1);
    }
}