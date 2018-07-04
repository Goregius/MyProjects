package com.main.Species;

public class Order extends Class {
    private String orderValue;

    public Order(String nameValue, String classValue, String orderValue) {
        super(nameValue, classValue);
        this.orderValue = orderValue;
    }

    public String getOrderValue() {
        return orderValue;
    }
}
