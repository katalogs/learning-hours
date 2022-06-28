## Split Formatting and Calculation

We can do it directly from our tests by writing a new test for `FormatStatement`

```c#
[Fact]
public void CalculateRentals()
{
    var calculator = new RentalCalculator(SomeRentals());
    calculator.Calculate();

    calculator.IsCalculated
        .Should()
        .BeTrue();

    calculator.Amount
        .Should()
        .BeApproximately(3037.24, 0.001);
}

[Fact]
public void FormatStatement()
{
    var calculator = new RentalCalculator(SomeRentals());
    var statement = calculator.FormatStatement();

    statement.Should()
        .Be(
            "09-10-2020 : Le Refuge des Loups (LA BRESSE) | 1089.9" + NewLine +
            "12-10-2020 : Au pied de la Tour (NOUILLORC) | 1276.45" + NewLine +
            "24-10-2020 : Le moulin du bonheur (GLANDAGE) | 670.89" + NewLine +
            "Total amount | 3037.2400000000002"
        );
}
```

- Then we can generate our new method from here
- We duplicate the code to make it `green` as fast as possible

```c#
public string Calculate()
{
    if (!_rentals.Any())
    {
        throw new InvalidOperationException("No rentals on which perform calculation");
    }

    var result = new StringBuilder();

    foreach (var rental in _rentals)
    {
        if (!IsCalculated)
        {
            Amount += rental.Amount;
        }

        result.Append(FormatLine(rental, rental.Amount));
    }

    result.Append($"Total amount | {Amount}");
    IsCalculated = true;

    return result.ToString();
}

private string FormatLine(Rental rental, double amount)
    => $"{rental.Date.ToString("dd-MM-yyyy")} : {rental.Label} | {rental.Amount}{Environment.NewLine}";

public string FormatStatement()
{
    if (!_rentals.Any())
    {
        throw new InvalidOperationException("No rentals on which perform calculation");
    }

    var result = new StringBuilder();

    foreach (var rental in _rentals)
    {
        if (!IsCalculated)
        {
            Amount += rental.Amount;
        }

        result.Append(FormatLine(rental, rental.Amount));
    }

    result.Append($"Total amount | {Amount}");
    IsCalculated = true;

    return result.ToString();
}
```

- We refactor and eliminate some duplication
    - `CheckRentals()` method
    - Remove dead parameter

```c#
public class RentalCalculator
{
    private readonly IEnumerable<Rental> _rentals;
    public bool IsCalculated { get; private set; }
    public double Amount { get; private set; }

    public RentalCalculator(IEnumerable<Rental> rentals) => _rentals = rentals;

    public void Calculate()
    {
        CheckRental();

        foreach (var rental in _rentals)
        {
            if (!IsCalculated)
            {
                Amount += rental.Amount;
            }
        }

        IsCalculated = true;
    }

    private void CheckRental()
    {
        if (!_rentals.Any())
        {
            throw new InvalidOperationException("No rentals on which perform calculation");
        }
    }

    public string FormatStatement()
    {
        CheckRental();

        var result = new StringBuilder();

        foreach (var rental in _rentals)
        {
            result.Append(FormatLine(rental));
        }

        if (!IsCalculated)
        {
            Calculate();
        }

        result.Append($"Total amount | {Amount}");

        return result.ToString();
    }

    private string FormatLine(Rental rental)
        => $"{rental.Date.ToString("dd-MM-yyyy")} : {rental.Label} | {rental.Amount}{Environment.NewLine}";
}
```

## Remove States

We have 2 states for computation here: `IsCalculated` and `Amount`.

Let's adapt the `Calculate` method, it should now return a `double`

```c#
public double Calculate()
{
    CheckRental();
    var amount = 0d;

    foreach (var rental in _rentals)
    {
        amount += rental.Amount;
    }

    return amount;
}

// The corresponding test
[Fact]
public void CalculateRentals() =>
    new RentalCalculator(SomeRentals())
        .Calculate()
        .Should()
        .BeApproximately(3037.24, 0.001);
```

We do the same for the `FormatStatement` method

```c#
public string FormatStatement()
{
    CheckRental();

    var result = new StringBuilder();

    foreach (var rental in _rentals)
    {
        result.Append(FormatLine(rental));
    }

    result.Append($"Total amount | {Calculate()}");

    return result.ToString();
}
```

Where are we regarding `Pure Function` properties?


- You can see **all the input in the argument list**
- The execution is **deterministic** : the same input will always get the same output
- ✅ You can see **all the output in the return value**


We need to improve our method definitions to have something more explicit:

- `IEnumerable<Rental>` -> double
- `IEnumerable<Rental>` -> string

## Pass the rentals as Argument

Let's start with a failing test. We adapt the test for the calculation for example:

```c#
[Fact]
public void CalculateRentals() =>
    RentalCalculator
        .Calculate(SomeRentals())
        .Should()
        .BeApproximately(3037.24, 0.001);
```

Let's adapt the code:

```c#
public static double Calculate(IEnumerable<Rental> rentals)
{
    CheckRental(rentals);
    var amount = 0d;

    foreach (var rental in rentals)
    {
        amount += rental.Amount;
    }

    return amount;
}

private static void CheckRental(IEnumerable<Rental> rentals)
{
    if (!rentals.Any())
    {
        throw new InvalidOperationException("No rentals on which perform calculation");
    }
}
```

We do the same for the `FormatStatement`:

```c#       
 [Fact]
public void FormatStatement()
{
    var statement = RentalCalculator.FormatStatement(SomeRentals());

    statement.Should()
        .Be(
            "09-10-2020 : Le Refuge des Loups (LA BRESSE) | 1089.9" + NewLine +
            "12-10-2020 : Au pied de la Tour (NOUILLORC) | 1276.45" + NewLine +
            "24-10-2020 : Le moulin du bonheur (GLANDAGE) | 670.89" + NewLine +
            "Total amount | 3037.2400000000002"
        );
}

public static string FormatStatement(IEnumerable<Rental> rentals)
{
    CheckRental(rentals);

    var result = new StringBuilder();

    foreach (var rental in rentals)
    {
        result.Append(FormatLine(rental));
    }

    result.Append($"Total amount | {Calculate(rentals)}");

    return result.ToString();
}
```

## Refactoring

We can simplify our code by using LinQ

```c#
public static class RentalCalculator
{
    public static double Calculate(IEnumerable<Rental> rentals)
    {
        CheckRental(rentals);
        return rentals.Sum(rental => rental.Amount);
    }

    public static string FormatStatement(IEnumerable<Rental> rentals)
    {
        CheckRental(rentals);

        return rentals
                   .Select(FormatLine)
                   .Aggregate(string.Empty, (acc, line) => acc + line)
               + TotalLine(rentals);
    }

    private static void CheckRental(IEnumerable<Rental> rentals)
    {
        if (!rentals.Any())
        {
            throw new InvalidOperationException("No rentals on which perform calculation");
        }
    }

    private static string FormatLine(Rental rental)
        => $"{rental.Date.ToString("dd-MM-yyyy")} : {rental.Label} | {rental.Amount}{Environment.NewLine}";

    private static string TotalLine(IEnumerable<Rental> rentals) => $"Total amount | {Calculate(rentals)}";
}
```

What can be simplified?

## Continuation / Callbacks

We can refactor our code to use continuation on the `CheckRental` method

```c#
public static class RentalCalculator
{
    public static double Calculate(IEnumerable<Rental> rentals) =>
        CheckRental(rentals.ToArray(),
            _ => _.Sum(rental => rental.Amount));

    public static string FormatStatement(IEnumerable<Rental> rentals) =>
        CheckRental(rentals.ToArray(), ToStatement);

    private static T CheckRental<T>(
        Rental[] rentals,
        Func<Rental[], T> onSuccess)
    {
        if (!rentals.Any())
        {
            throw new InvalidOperationException("No rentals on which perform calculation");
        }

        return onSuccess(rentals);
    }

    private static string ToStatement(Rental[] rentals) =>
        rentals.Select(FormatLine)
            .Aggregate(string.Empty, (acc, line) => acc + line)
        + TotalLine(rentals);

    private static string FormatLine(Rental rental)
        => $"{rental.Date.ToString("dd-MM-yyyy")} : {rental.Label} | {rental.Amount}{Environment.NewLine}";

    private static string TotalLine(IEnumerable<Rental> rentals) => $"Total amount | {Calculate(rentals)}";
}
```

Where are we regarding `Pure Function` properties?


- ✅ You can see **all the input in the argument list**
- ✅ The execution is **deterministic** : the same input will always get the same output
- ✅ You can see **all the output in the return value**

> What can still be improved in term of transparency?

Let's keep it for another hour 😉