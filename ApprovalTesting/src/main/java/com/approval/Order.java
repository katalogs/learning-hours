package com.approval;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import static java.math.RoundingMode.HALF_UP;

public class Order {
    private final String currency;
    private final List<OrderItem> items;
    private final OrderStatus status;

    public Order(String currency) {
        this.items = new ArrayList<>();
        this.status = OrderStatus.CREATED;
        this.currency = currency;
    }

    public BigDecimal getTotal() {
        return items.stream()
                .map(OrderItem::getTaxedAmount)
                .reduce(BigDecimal.ZERO, BigDecimal::add).setScale(2, HALF_UP);
    }

    public String getCurrency() {
        return currency;
    }

    public List<OrderItem> getItems() {
        return new ArrayList<>(items);
    }

    public BigDecimal getTax() {
        return items.stream()
                .map(OrderItem::getTax)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }

    public OrderStatus getStatus() {
        return status;
    }

    public void addProduct(Product salad, int quantity) {
        this.items.add(new OrderItem(salad, quantity));
    }
}
