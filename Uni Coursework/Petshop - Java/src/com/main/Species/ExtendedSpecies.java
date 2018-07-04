package com.main.Species;

public class ExtendedSpecies extends Species {
    private Boolean canTalk = null;
    private Boolean isVenomous = null;

    public ExtendedSpecies(Species species) {
        super(species.getNameValue(), species.getClassValue(), species.getOrderValue(), species.getFamilyValue(), species.getGenusValue(), species.getSpeciesValue(), species.getNoOfLegs());
    }

    public ExtendedSpecies(Reptile reptile) {
        super(reptile.getNameValue(), reptile.getClassValue(), reptile.getOrderValue(), reptile.getFamilyValue(), reptile.getGenusValue(), reptile.getSpeciesValue(), reptile.getNoOfLegs());
        this.isVenomous = reptile.getIsVenomous();
    }

    public ExtendedSpecies(Parrot parrot) {
        super(parrot.getNameValue(), parrot.getClassValue(), parrot.getOrderValue(), parrot.getFamilyValue(), parrot.getGenusValue(), parrot.getSpeciesValue(), parrot.getNoOfLegs());
        this.canTalk = parrot.getCanTalk();
    }

    public Boolean getCanTalk() {
        return canTalk;
    }

    public Boolean getVenomous() {
        return isVenomous;
    }

    @Override
    public String toString() {
        return getNameValue() + ", " + getClassValue() + ", " + getOrderValue() + ", " + getFamilyValue() + ", " + getGenusValue() + ", " + getSpeciesValue() + ", " + getNoOfLegs() + ", " + getVenomous() + ", " + getCanTalk();
    }
}
