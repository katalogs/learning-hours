package com.approval;

import java.math.BigDecimal;

import static java.math.BigDecimal.valueOf;
import static java.math.RoundingMode.HALF_UP;

public record Product(String name, BigDecimal price, Category category) {
    public BigDecimal getTaxedAmount() {
        return price.add(this.getTax()).setScale(2, HALF_UP);
    }


    public BigDecimal getTax() {
        return price().divide(valueOf(100)).multiply(category.taxPercentage()).setScale(2, HALF_UP);
    }
}
