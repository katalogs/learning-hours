# Properties of Good Unit Tests

## Learning Goals

- Remember properties of Good Unit Tests
- Apply strategy on Unit Tests that make them compliant with those properties

## Connect - Characteristics

- Open the solution `Test-Characteristics`
- Then open the test class `FizzBuzzShould`
- Identify which are the problems / improvement possible on those tests

## Concepts

### 3 pillars and 1 foundation

![pillars and foundation](img/pillars-foundation.png)

To create tests that are more resistant to refactoring we invite you to deep dive into `Test Data Builders`
and `Mocking` topics.

### Apply F.I.R.S.T principles

![FIRST principles](img/F.I.R.S.T-principles.png)

Go beyond `F.I.R.S.T principles` with [Test Desiderata](https://kentbeck.github.io/TestDesiderata/)

## Concrete Practice - F.I.R.S.T FizzBuzz tests

In mob, let's fix the tests to make them respect the F.I.R.S.T principles

### Isolation

If you run all the tests each test should be green
![Green tests](img/all-tests-green.png)

What happens if you run the `Return_Buzz_For_Multiples_Of5` test?

![No isolation](img/isolation-failure.png)

Why does it happen?

```csharp
private static FizzBuzz _fizzBuzz;

[Theory]
[InlineData(3)]
[InlineData(12)]
[InlineData(81)]
public void Return_Fizz_For_Multiples_Of3(int value)
{
    _fizzBuzz = FizzBuzz.New();
    _fizzBuzz.Convert(value)
        .Should()
        .Be("Fizz");
}

[Theory]
[InlineData(5)]
[InlineData(25)]
[InlineData(95)]
public void Return_Buzz_For_Multiples_Of5(int value) =>
    _fizzBuzz.Convert(value)
        .Should()
        .Be("Buzz");
```

The `_fizzBuzz` field is instantiated statically by the test `Return_Fizz_For_Multiples_Of3`.
If this test has been ran before the test we want to run, the test is green otherwise not.

As usual, a step-by-step solution is available [here](step-by-step.md)

## Conclusion