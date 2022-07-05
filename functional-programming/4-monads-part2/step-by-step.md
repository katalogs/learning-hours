# Try

## GetTheResultOfDivide

Easy peace here, we just have to call the `Divide` method

```c#
[Fact]
public void GetTheResultOfDivide() =>
    // Divide x = 9 by y = 2
    Divide(9, 2)
        .Should()
        .BeSuccess(result => result.Should().Be(4))
        .And
        .IsDefault()
        .Should()
        .BeFalse();
```

## MapTheResultOfDivide

We use the fact that `Try` is a functor so we can apply easily a function to its content safely by using the `Map`
function.
Then we use `Match` to retrieve the `Success` value and 0 in case of `Failure`

```c#
[Fact]
public void MapTheResultOfDivide()
{
    // Divide x = 9 by y = 2 and add z to the result
    var z = 3;
    var result = Divide(9, 2)
        .Map(a => a + z)
        .Match(x => x, 0);

    result.Should().Be(7);
}
```

## DivideByZeroIsAlwaysAGoodIdea

To have the expected result we need to throw exception by ourselves.
We can do in the failure branch of the `Match`

```c#
[Fact]
public void DivideByZeroIsAlwaysAGoodIdea()
{
    // Divide x by 0 and get the result
    var x = 1;

    var divideByZero = () =>
        Divide(x, 0)
            .Match(success => success, ex => throw ex);

    divideByZero
        .Should()
        .Throw<DivideByZeroException>();
}
```

## DivideByZeroOrElse

We can use the `IfFail` method to return a specified value in case of failure

```c#
[Fact]
public void DivideByZeroOrElse()
{
    // Divide x by 0, on exception returns 0
    var x = 1;
    var result = Divide(x, 0)
        .IfFail(0);

    result.Should().Be(0);
}
```

## MapTheFailure

```c#
[Fact]
public void MapTheFailure()
{
    // Divide x by 0, log the failure message to the console
    // And get 0
    var x = 1;
    var result = Divide(x, 0)
        .IfFail(failure =>
        {
            _testOutputHelper.WriteLine(failure.Message);
            return 0;
        });

    result.Should().Be(0);
}
```

## MapTheSuccess

We use once again the `Match` method and write into the console from the lambdas

```c#
[Fact]
public void MapTheSuccess()
{
    // Divide x by y
    // log the failure message to the console
    // Log your success to the console
    // Get the result or 0 if exception
    var x = 8;
    var y = 4;

    var result = Divide(x, y)
        .Match(success =>
            {
                _testOutputHelper.WriteLine($"Success {success}");
                return success;
            },
            failure =>
            {
                _testOutputHelper.WriteLine(failure.Message);
                return 0;
            });

    result.Should().Be(2);
}
```

## MapTheSuccessWithoutMatch

- We can log / write into the console through the `Do` method
- Then get the result with the `IfFail` method

It is an alternative to the previous example

```c#
[Fact]
public void MapTheSuccessWithoutMatch()
{
    // Divide x by y
    // log the failure message to the console
    // Log your success to the console
    // Get the result or 0 if exception
    // You are not authorized to use the Match method
    var x = 8;
    var y = 4;

    var result =
        Divide(x, y)
            .Do(r => _testOutputHelper.WriteLine($"Success {r}"))
            .IfFail(failure =>
            {
                _testOutputHelper.WriteLine(failure.Message);
                return 0;
            });

    Assert.Equal(2, result);
}
```

## ChainTheTry

To chain we need to use the monadic binding -> `Bind`

```c#
[Fact]
public void ChainTheTry()
{
    // Divide x by y
    // Chain 2 other calls to divide with x = previous Divide result
    // log the failure message to the console
    // Log your success to the console
    // Get the result or 0 if exception
    var x = 27;
    var y = 3;

    var result = Divide(x, y)
        .Bind(previous => Divide(previous, y))
        .Bind(previous => Divide(previous, y))
        .Do(success => _testOutputHelper.WriteLine($"Success {success}"))
        .IfFail(failure =>
        {
            _testOutputHelper.WriteLine(failure.Message);
            return 0;
        });

    result.Should().Be(1);
}
```

## TryAndReturnOption

We have an extension methods on `Try` that we can use here to convert a `Try` to an `Option`

```c#
[Fact]
public void TryAndReturnOption()
{
    // Create a Divide function that return an Option on Divide
    // If something fails -> return None
    // Divide 9 by 3 again
    // Can be useful sometimes
    var result = DivideOptional(9, 3);

    result.Should()
        .BeSome(_ => _.Should().Be(3));
}

private static Option<int> DivideOptional(int x, int y)
    => Divide(x, y).ToOption();
```

## TryOnAsync

Same as in the previous exercise, we can convert a `Try` into `TryAsync` monad

```c#
[Fact]
public async Task TryOnAsync()
{
    // Create a Divide function that returns a TryAsync
    var result = await DivideAsync(90, 10);

    result.Should()
        .BeSuccess(success => success.Should().Be(9));
}

private static TryAsync<int> DivideAsync(int x, int y)
    => Divide(x, y).ToAsync();
```

# Either

## MapTheResultOfDivide

Like with `Try` we can use a function `If<something>`. By convention `Left` is the failure case:

```c#
[Fact]
public void MapTheResultOfDivide()
{
    // Divide x = 9 by y = 2 and add z to the result
    var z = 3;
    var result = Divide(9, 2)
        .Map(a => a + z)
        .IfLeft(0);

    result.Should().Be(7);
}
```

## ChainTheEither

We use the same methods than on `Try` with `Either`:

```c#
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

    var result = Divide(x, y)
        .Bind(previous => Divide(previous, y))
        .Bind(previous => Divide(previous, y))
        .Do(success => _testOutputHelper.WriteLine($"Result is {success}"))
        .IfLeft(left =>
        {
            _testOutputHelper.WriteLine(left.Message);
            return 0;
        });

    result.Should().Be(1);
}
```