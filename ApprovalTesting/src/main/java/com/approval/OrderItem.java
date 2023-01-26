package com.approval;

import java.math.BigDecimal;

import static java.math.RoundingMode.HALF_UP;

public record OrderItem(Product product, int quantity) {
    public BigDecimal getTaxedAmount() {
        return product.getTaxedAmount().multiply(BigDecimal.valueOf(quantity)).setScale(2, HALF_UP);
    }

    public BigDecimal getTax() {
        return product.getTax().multiply(BigDecimal.valueOf(quantity)).setScale(2, HALF_UP);
    }
}
