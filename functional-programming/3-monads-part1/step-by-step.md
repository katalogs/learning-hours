# Option

## WorkingWithNull

Use `Map` and `IfNone` methods here:

```c#
[Fact]
public void WorkingWithNull()
{
    // Instantiate a None Option of string
    // Map it to an Upper case function
    // Then it must return the string "Ich bin empty" if empty
    var iamAnOption = Option<string>.None;
    var optionValue = iamAnOption
        .Map(p => p.ToUpper())
        .IfNone("Ich bin empty");

    iamAnOption
        .Should()
        .BeNone();

    optionValue
        .Should()
        .Be("Ich bin empty");
}
```

## FilterAListOfPerson

- We can start by doing something natural
    - `Filter` the list with only the `Some` method
    - Then we `Map` each value

```c#
[Fact]
public void FilterAListOfPerson()
{
    // Filter the persons list with only defined persons
    var definedPersons =
        _persons
            .Filter(p => p.IsSome)
            .Map(person => person.IfNone(() => throw new InvalidOperationException("WTF ?")));

    definedPersons
        .Should()
        .HaveCount(2);
}
```

- We can simplify it by using `monadic` binding
    - It is the equivalent of doing a filter then select the value in the `Option`

```c#
[Fact]
public void FilterAListOfPerson() =>
    _persons
        .Bind(p => p)
        .Should()
        .HaveCount(2);
```

## FindKaradoc

Here we need to mix functions usage:

```c#
[Fact]
public void FindKaradoc()
{
    // Find Karadoc in the people List or return Perceval
    var foundPersonLastName = 
        _persons
            // Work only on defined `Option` -> Seq<Person>
            .Bind(p => p)
            // Call the Find on the result Seq<Person>
            .Find(p => p.LastName == "Karadoc")
            // Map found person to only get his/her last name
            .Map(p => p.LastName)
            // If none return -> Perceval
            .IfNone("Perceval");

    foundPersonLastName
        .Should()
        .Be("Perceval");
}
```

### FindPersonOrDieTryin

```c#
[Fact]
public void FindPersonOrDieTryin()
{
    // Find a person matching firstName and lastName, throws an ArgumentException if not found
    var firstName = "Rick";
    var lastName = "Sanchez";

    var findPersonOrDieTryin = () =>
        _persons
            .Bind(p => p)
            .Find(person => person.LastName == lastName && person.FirstName == firstName)
            .IfNone(() => throw new ArgumentException("No matching person"));

    findPersonOrDieTryin
        .Should()
        .Throw<ArgumentException>();
}
```

### ChainCall

The `Half` method takes a primitive type as input but returns a `Monad` result to express that its call can potentially
fail

- Express all the possible output of the function

```c#
private Option<double> Half(double x)
    => x % 2 == 0
        ? Some(x / 2)
        : None;
```

We can us `Monadic` binding once again to chain calls and use the `Do` function to apply side effects free functions

```c#
[Fact]
public void ChainCall()
{
    // Chain calls to the half method 4 times with start as argument
    // For each half append the value to the resultBuilder (side effect)
    var start = 500d;
    var resultBuilder = new StringBuilder();

    var result =
        Half(start)
            .Do(r => resultBuilder.Append(r))
            .Bind(Half)
            .Do(r => resultBuilder.Append(r))
            .Bind(Half)
            .Do(r => resultBuilder.Append(r))
            .Bind(Half)
            .Do(r => resultBuilder.Append(r));

    result
        .Should()
        .BeNone();

    resultBuilder
        .ToString()
        .Should()
        .Be("250125");
}
```