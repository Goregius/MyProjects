package com.main.Species;

public class Parrot extends Species {
    private boolean canTalk;

    public Parrot(Species species, boolean canTalk) {
        super(species.getNameValue(), species.getClassValue(), species.getOrderValue(), species.getFamilyValue(), species.getGenusValue(), species.getSpeciesValue(), species.getNoOfLegs());
        this.canTalk = canTalk;
    }

    public Parrot(String nameValue, String classValue, String orderValue, String familyValue, String genusValue, String speciesValue, int noOfLegs, boolean canTalk) {
        super(nameValue, classValue, orderValue, familyValue, genusValue, speciesValue, noOfLegs);
        this.canTalk = canTalk;
    }

    public boolean getCanTalk() {
        return canTalk;
    }
}
