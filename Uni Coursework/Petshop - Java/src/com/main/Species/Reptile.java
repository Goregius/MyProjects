package com.main.Species;

public class Reptile extends Species {
    private boolean isVenomous;

    public Reptile(Species species, boolean isVenomous) {
        super(species.getNameValue(), species.getClassValue(), species.getOrderValue(), species.getFamilyValue(), species.getGenusValue(), species.getSpeciesValue(), species.getNoOfLegs());
        this.isVenomous = isVenomous;
    }

    public Reptile(String nameValue, String classValue, String orderValue, String familyValue, String genusValue, String speciesValue, int noOfLegs, boolean isVenomous) {
        super(nameValue, classValue, orderValue, familyValue, genusValue, speciesValue, noOfLegs);
        this.isVenomous = isVenomous;
    }

    public boolean getIsVenomous() {
        return isVenomous;
    }
}
