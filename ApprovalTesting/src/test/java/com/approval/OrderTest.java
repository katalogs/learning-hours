package com.approval;

import org.junit.jupiter.api.Test;

import java.math.BigDecimal;

import static org.assertj.core.api.Assertions.assertThat;

public class OrderTest {
    @Test
    void simple_order_should_be_correct() {
        // Arrange
        Order order = new Order("EUR");

        Category foodCategory = new Category("food", new BigDecimal("10"));
        Product salad = new Product("salad", new BigDecimal("3.56"), foodCategory);

        order.addProduct(salad, 1);
        // Act

        // Assert
        assertThat(order).isNotNull();
    }

    @Test
    void new_order_should_have_correct_values() {
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
    }
}
