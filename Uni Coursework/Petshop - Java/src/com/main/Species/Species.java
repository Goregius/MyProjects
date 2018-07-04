package com.main.Species;

public class Species extends Genus {
    private String speciesValue;
    private int noOfLegs;

    public Species(String nameValue, String classValue, String orderValue, String familyValue, String genusValue, String speciesValue, int noOfLegs) {
        super(nameValue, classValue, orderValue, familyValue, genusValue);
        this.speciesValue = speciesValue;
        this.noOfLegs = noOfLegs;
    }

    public String getSpeciesValue() {
        return speciesValue;
    }

    public int getNoOfLegs() {
        return noOfLegs;
    }
}
