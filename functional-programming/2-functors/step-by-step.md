# Functions are Functors

## Without Language-Ext

We start with a failing test and implement the method as simplest as possible:

```c#
private static int Double(int y) => Multiply(y, 2);
private static int Add1(int x) => Add(x, 1);

private int Add1AndDouble(int x)
{
    var y = Add1(x);
    return Double(y);
}

[Fact]
public void Add1AndDoubleIt()
{
    // Create an Add1 function based on Add function
    // Create a Double function based on Multiply
    // Compose the 2 functions together to implement the Add1AndDouble function
    Add1AndDouble(2)
        .Should()
        .Be(6);
}
```

We want to use `functors` here so let's define a `Map` function:

```c#
private int Add1AndDouble(int x)
    => Add1(x).Map(Double);
    
// We create an extension on int for this purpose
static class IntExtensions
{
    public static int Map(this int x, Func<int, int> function) => function(x);
}
```

For the second implementation let's just use function chaining

```c#
[Fact]
public void AddXAndDoubleItInDouble()
{
    // Use the Double and the Add function to implement it
    AddXtoYAndDouble(2, 7)
        .Should()
        .Be(18);
}

private int AddXtoYAndDouble(int x, int y) => Double(Add(x, y));
```

## With Language-Ext

We start by defining the `functions`

```c#
private static readonly Func<int, int> Add1 = x => Add(x, 1);
private static readonly Func<int, int> Double = x => Multiply(x, 2);
```

Then we compose them through the `Compose` method:

```c#
[Fact]
public void Add1AndDoubleIt()
{
    // Create an Add1 function based on Add function
    // Create a Double function based on Multiply
    // Compose the 2 functions together to implement the Add1AndDouble function

    int Add1AndDouble(int x) => Add1.Compose(Double)(x);
    Add1AndDouble(2).Should().Be(6);
}
```

For the other test it is the same implementation:

```c#
[Fact]
public void AddXtoYAndDoubleIt()
{
    // Use the Double and the Add function to implement it
    int AddXtoYAndDouble(int x, int y) => Double(Add(x, y));
    AddXtoYAndDouble(2, 5).Should().Be(14);
}
```

## Lists are functors