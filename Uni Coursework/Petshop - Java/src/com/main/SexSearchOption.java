package com.main;

public enum SexSearchOption {
    MALE("male"), FEMALE("female"), NO_SEARCH("");

    private final String string;

    SexSearchOption(String string) {
        this.string = string;
    }

    public String getString() {
        return string;
    }
}