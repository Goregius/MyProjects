package com.main.Species;

public class Family extends Order {
    private String familyValue;

    public Family(String nameValue, String classValue, String orderValue, String familyValue) {
        super(nameValue, classValue, orderValue);
        this.familyValue = familyValue;
    }

    public String getFamilyValue() {
        return familyValue;
    }
}
