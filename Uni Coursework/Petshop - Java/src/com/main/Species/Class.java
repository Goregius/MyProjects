package com.main.Species;

public class Class extends Name {
    private String classValue;

    public Class(String nameValue, String classValue) {
        super(nameValue);
        this.classValue = classValue;
    }

    public String getClassValue() {
        return classValue;
    }
}
