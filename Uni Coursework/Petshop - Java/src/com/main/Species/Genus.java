package com.main.Species;

public class Genus extends Family {
    private String genusValue;

    public Genus(String nameValue, String classValue, String orderValue, String familyValue, String genusValue) {
        super(nameValue, classValue, orderValue, familyValue);
        this.genusValue = genusValue;
    }

    public String getGenusValue() {
        return genusValue;
    }
}
