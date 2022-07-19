# Test your Architecture with ArchUnit

## Learning Goals

- Understand the advantages of using a library like `ArchUnit`
- Clarify how we can use it on a daily basis

## Connect - Architecture?

In group, discuss:

- What are your current `architecture rules` inside your team
- How you ensure that your code is always matching those rules

Examples :

- All our events must reside in the events package
- Our repository layer can only be accessed through our application layer
- …

[![Im an expert](img/expert.webp)](https://youtu.be/BKorP55Aqvg)

## Concepts - What is it?

You can use the slides available [here](https://speakerdeck.com/thirion/test-your-architecture-with-archunit) for this
part

## Concrete Practice - Create some rules

To simplify the usage of the library during this kata some preparation work has already been made:

- Dependencies are already there: `TngTech.ArchUnitNet` and `TngTech.ArchUnitNet.xUnit`
- `ArchUnitExtensions` contains some extensions that could help you to `check` rules
- Test classes have been provided but implementations are missing
    - You have to fill the gap to implement the rule expressed in plain text english correctly
        - Do it for each rule using the `EmptyRule`
    - If you want, you can `fix` the production code based on the `ArchRule` discoveries - *Optional*

### Example

```c#
public class Guidelines
{
    [Fact]
    public void ClassShouldNotDependOnAnother() =>
        EmptyRule("Class SomeExample should not depend on Other")
            .Check();
}
```

- We use the `ArchUnit` dsl to write it
    - Let the public `api` guide you
    - Think `dot-driven development`

![Dot-driven development in ArchUnit](img/dot-driven-development.gif)

```c#
[Fact]
public void ClassShouldNotDependOnAnother() =>
    Classes().That()
        .Are(typeof(SomeExample))
        .Should()
        .NotDependOnAny(typeof(Other))
        .Check();
```

### Exercises

Recommended order (`do at least 1 per category / test class`):

- `LayeredArchitecture`
- `NamingConvention`
- `Guidelines`
- `LinguisticAntiPatterns`
- `MethodsReturnTypes`
- `Annotations`

Which other `rules` could be added?

Solution is available in the `solution` folder

## Conclusion - Reflect

- What can we do with it ?
- Which rules could be useful ?

## Resources

- [ArchUnit](https://www.archunit.org/)
- [ArchUnit for .Net](https://archunitnet.readthedocs.io/en/latest/)