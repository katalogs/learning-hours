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
simply add the verifyAsJson:
```java
Approvals.verifyAsJson(order);
```
The first launch of the test create the file 
`OrderTest.new_order_should_have_correct_values.received.json`,
review it and move it to its approved counterpart: 
`OrderTest.new_order_should_have_correct_values.received.json`

### Recommended
`verifyAsJson` is deprecated, ApprovalTesting prefer that you use any json parser that you want,
to give back control of the parser.
You can use the gson serializer, which is installed in the project:
```java
Approvals.verify(new Gson().toJson(order));
```
Problem, the approved file extension is txt. You may juste add the option of the extension in verify:
```java
Approvals.verify(new Gson().toJson(order));
```
Finally, you may add useful options to gson, like pretty json to ease reading, 
or null serialization which might be important (but not in this example)
```
Approvals.verify(
    new GsonBuilder()
        .setPrettyPrinting()
        .serializeNulls()
        .create()
    .toJson(order),"json");
```

### Combine for more test data
You may introduce the combine functionality, and/or the golden master technique and/or fuzz testing.
Example of combine to test multiples products:
```java 
    @Test
    void products_should_have_correct_values() {
        // Arrange
        String[] names = {"salad","tomato","race kart"};
        BigDecimal[] prices = { new BigDecimal("3.4"), new BigDecimal("5.5") };
        Category[] categories = {
            new Category("food", new BigDecimal("10")),
            new Category("toy", new BigDecimal("20"))
        };

        // Act

        // Assert
        Gson gson = new GsonBuilder()
                .setPrettyPrinting()
                .serializeNulls()
                .create();
        CombinationApprovals.verifyAllCombinations(
                (name, price, category) -> {
                    Product product = new Product(name, price, category);
                    return "Tax: " + product.getTax() + " - Price: " + product.getTaxedAmount();
                }, names, prices, categories );
    }
```
