# Refactoring test inputs with Test Data Builders

## Learning Goals

- Putting an even greater business focus on our Builders
- Combine Builders to make our tests even more readable

## Connect - all test data builders are not created equal (4 + 3 min)
**Map code examples with a pattern**

- Ask attendees to map code snippets between `good` and `bad` `Test Data Builder` (4 min)
  - Indicate what could be improved on each piece of code
- Discuss each code snippets with attendees. (3 min)

### Examples of snippets

```c#
// Snippet 1
var address = new AddressBuilder()
    .WithoutStreet()
    .WithoutPostalCode()
    .WithCity("Paris")
    .Build();
    
// Snippet 2
var address = new AddressBuilder()
    .WithStreet("Rue Sainte Catherine")
    .WithPostalCode(new PostalCodeBuilder()
        .WithNumber("1 Bis")
        .Build())
    .WithCity("Bordeaux")
    .Build();
    
// Snippet 3
var address = AnAddress()
    .WithoutPostalCode()
    .WithCity("Bordeaux")
    .Build();

// Snippet 4
var address = ANewAddress()
    .At("Bordeaux")
    .InStreet("Rue Sainte Catherine")
    .WithPostalCode(new PostalCodeBuilder()
        .WithNumber("1 Bis")
        .Build())
    .Build();
    
// Snippet 5
var address = new AddressBuilder()
    .WithStreet(null)
    .WithPostalCode(null)
    .WithCity("Bordeaux")
    .Build();

// Snippet 6
var address = ANewAddress()
    .At("Bordeaux")
    .InStreet("Rue Sainte Catherine")
    .WithPostalCode(_ => _.WithNumber("1 Bis"))
    .Build();
```

## Concepts

### Presentation
> "eliminates the irrelevant, and amplifies the essentials of the test."

Insist on the fact that Builders should amplify what is essential in terms of data for the behavior under tests.

For example : if you run a business process based on a `Person`, it would be really surprising that the name of the `Person` could have any impact to take a business decision (unless you design a fraud system maybe 😊)
What could matter in terms of data would may be : his/her current address, marital situation, ...

## Concrete Practice (35 min)

Ask attendees to: 
- Improve the `Test Data Builders` from previous session to be more business oriented
  - Avoid `Withxxx`
  - Use business terms
  - Amplify the essential -> don't waste time setup irrelevant data for the business under tests

## Conclusion (5min) - Say it in a tweet
Summarize the session in a tweet
- What did you learn?
- What do you want to apply?
- How do you feel?
- ...

![Say it in a Tweet](./img/twitter.png)