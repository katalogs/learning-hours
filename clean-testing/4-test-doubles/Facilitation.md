# Test Doubles
## Learning Goals
- Understand the different kinds of `Test Doubles`
- Clarify the vocabulary related to `Test Doubles`
- Explain their utility depending on the encountered problem

## Connect - Word Association



## Concepts - 50 Shades of Test Doubles
One of the fundamental requirements of making Unit testing work is `isolation`. 
Isolation is hard in real world as there are always dependencies (collaborators) across the system.
![Test Double](img/test-double.jpg)

That’s where concept of something generically called ‘Test Double’ comes into picture.

A `Double` allow us to break the original dependency, helping isolate the unit (or System Under Test (SUT) – as commonly referred). As this Double is used to pass a unit test it’s generally referred to as ‘Test Double’. There are variations in types of Test Doubles depending on their intent.

Test Doubles play a key role in ensuring `repeatability` of our tests as well by replacing external dependencies: 

- Call to an API
- Call to a database
- Non deterministic data : random or temporal for example

### Dummy
Dummy is a placeholder required to pass the unit test :
- Either the SUT (System Under Test) does not exercise this placeholder
- Or its value has no impact on the end result

Dummy can be something as simple as passing ‘null’ or a void implementation with exceptions to ensure it’s never leveraged.

```c#
 [Fact]
public void Should_Throw_An_ArgumentException_When_Divide_By_Zero()
{
    var calculator = new Calculator();
    const int numerator = 9;
    const int zero = 0;

    calculator.Invoking(_ => _.Divide(numerator, zero))
        .Should()
        .Throw<ArgumentException>();
}
```

- Here whatever the value of numerator it has no impact on the sut
- We could even replace it by:
```c#
var numerator = new Random().Next(); 
```

### Fake
Fake is used to simplify a dependency so that unit test can pass easily:
- A real implementation but simplified of a real class
- You can not use libraries to define them because it is a `Test Double` that requires a true implementation

### Stub

### Limits
> Only mock code you own

Substituting an object from an external system may lead to major changes if the external object changes -> create an intermediate layer (Use Adapter pattern for example).

## Concrete Practice - 

## Conclusion

### Resources
- [XUnit Test Patterns - Refacoring Test Code](http://xunitpatterns.com/)
- [Software craft - TDD, Clean Code et autres pratiques essentielles](https://www.dunod.com/sciences-techniques/software-craft-tdd-clean-code-et-autres-pratiques-essentielles)
- [Dummy vs. Stub vs. Spy vs. Fake vs. Mock](https://nirajrules.wordpress.com/2011/08/27/dummy-vs-stub-vs-spy-vs-fake-vs-mock/)