## Connect - Pros & cons

### Snippet 1 - not null

**Pros**

* Sonar quality gate is happy with code coverage
* Low maintenance

**Cons**

* Does not guaranty anything, useless as a test

### Snippet 2

**Pros**

* Validate the behaviour of the tested class

**Cons**

* Verbose
* Require maintenance on behavior change or attribute change

Other solutions (create a reference object and compare, load an existing object from json an compare, ...)
have nearly the same pros and cons as snippet 2

## Concrete practice

### Basic

simply add the UsesVerify attribute to the test class, make the test method return a `Task` and remove the assertions to
return `Verifier.Verify(order)`:

```csharp
[UsesVerify]
public class OrderTests
{ 
    [Fact]
    public Task New_order_should_have_correct_values()
    {
        // ...
        return Verifier.Verify(order);
    } 
}
```

The first launch of the test create the file
`OrderTests.New_order_should_have_correct_values.received.txt`, review it and move it to its approved counterpart:
`OrderTests.New_order_should_have_correct_values.verified.txt`

### Combine for more test data

You may introduce the combine functionality, and/or the golden master technique and/or fuzz testing.

Combinatorial functionality is provided by a mix
between [Xunit.Combinatorial](https://github.com/AArnott/Xunit.Combinatorial) and
the [Verify's ability to use parameters as part of file names](https://github.com/VerifyTests/Verify/blob/main/docs/parameterised.md)

Example of combine to test multiples products:

```csharp 
    [Theory, CombinatorialData]
    public async Task products_should_have_correct_values(
        [CombinatorialValues("salad","tomato","race kart")] string name,
        [CombinatorialValues(3.4, 5.5)] decimal price,
        [CombinatorialValues(10, 20)] decimal taxPercentage)
    {
        var category = new Category(Name: "category", TaxPercentage: taxPercentage);
        var product = new Product(name, price, category);

        await Verify($"Tax: {product.Tax} - Price: {product.TaxedAmount}")
            .UseParameters(name, price, taxPercentage);
    }
```
