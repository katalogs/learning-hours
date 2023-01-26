# Approval testing
A step by step file (for the animator) exit [here](step-by-step.md)
## Learning Goals

- Understand the approval testing concept
- Try the ApprovalTests java lib 
- (Optionnal) Introduce Golden master and [Combination approval](https://github.com/approvals/ApprovalTests.Java/blob/master/approvaltests/docs/how_to/TestCombinations.md)

## Connect - Pros & cons
List pros and cons of those two tests:

```java
        // Arrange
        Order order = new Order("EUR");

        Category foodCategory = new Category("food", new BigDecimal("10"));
        Product salad = new Product("salad", new BigDecimal("3.56"), foodCategory);
        Product tomato = new Product("tomato", new BigDecimal("4.65"), foodCategory);

        order.addProduct(salad, 2);
        order.addProduct(tomato, 3);
        // Act

        // Assert
        assertThat(order.getStatus()).isNotNull();
```

```java
        // Arrange
        Order order = new Order("EUR");

        Category foodCategory = new Category("food", new BigDecimal("10"));
        Product salad = new Product("salad", new BigDecimal("3.56"), foodCategory);
        Product tomato = new Product("tomato", new BigDecimal("4.65"), foodCategory);

        order.addProduct(salad, 2);
        order.addProduct(tomato, 3);
        // Act

        // Assert
        assertThat(order.getStatus()).isEqualTo(OrderStatus.CREATED);
        assertThat(order.getTotal()).isEqualTo(new BigDecimal("23.20"));
        assertThat(order.getTax()).isEqualTo(new BigDecimal("2.13"));
        assertThat(order.getCurrency()).isEqualTo("EUR");
        assertThat(order.getItems()).hasSize(2);
        assertThat(order.getItems().get(0).product().name()).isEqualTo("salad");
        assertThat(order.getItems().get(0).product().price()).isEqualTo(new BigDecimal("3.56"));
        assertThat(order.getItems().get(0).quantity()).isEqualTo(2);
        assertThat(order.getItems().get(0).getTaxedAmount()).isEqualTo(new BigDecimal("7.84"));
        assertThat(order.getItems().get(0).getTax()).isEqualTo(new BigDecimal("0.72"));
        assertThat(order.getItems().get(1).product().name()).isEqualTo("tomato");
        assertThat(order.getItems().get(1).product().price()).isEqualTo(new BigDecimal("4.65"));
        assertThat(order.getItems().get(1).quantity()).isEqualTo(3);
        assertThat(order.getItems().get(1).getTaxedAmount()).isEqualTo(new BigDecimal("15.36"));
        assertThat(order.getItems().get(1).getTax()).isEqualTo(new BigDecimal("1.41"));
```

## Concept
Approval test take a "picture" of an entry (object, String, array, ...) and compares it with a reference version of that picture - the approved version, thus approval testing -.

## Concrete practice
Refactor tests from OrderTest to use Approval

(Optional) Use [Combination approval](https://github.com/approvals/ApprovalTests.Java/blob/master/approvaltests/docs/how_to/TestCombinations.md) to create test for product tax calculation

