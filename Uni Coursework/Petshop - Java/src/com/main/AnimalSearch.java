package com.main;

class AnimalSearch {
    private String name;
    private String commonName;
    private SexSearchOption sex;
    private String mainColour;
    private String className;
    private String order;
    private String Family;
    private String genus;
    private String species;
    private Integer[] noOfLegsRange;
    private CheckBoxSearchOption venomous;
    private CheckBoxSearchOption canTalk;

    AnimalSearch(String name, String commonName, SexSearchOption sex, String mainColour, String className, String order, String family, String genus, String species, Integer[] noOfLegsRange, CheckBoxSearchOption venomous, CheckBoxSearchOption canTalk) {
        this.name = name;
        this.commonName = commonName;
        this.sex = sex;
        this.mainColour = mainColour;
        this.className = className;
        this.order = order;
        Family = family;
        this.genus = genus;
        this.species = species;
        this.noOfLegsRange = noOfLegsRange;
        this.venomous = venomous;
        this.canTalk = canTalk;
    }

    public String getName() {
        return name;
    }

    public String getCommonName() {
        return commonName;
    }

    public SexSearchOption getSex() {
        return sex;
    }

    public String getMainColour() {
        return mainColour;
    }

    public String getClassName() {
        return className;
    }

    public String getOrder() {
        return order;
    }

    public String getFamily() {
        return Family;
    }

    public String getGenus() {
        return genus;
    }

    public String getSpecies() {
        return species;
    }

    public Integer[] getNoOfLegsRange() {
        return noOfLegsRange;
    }

    public CheckBoxSearchOption getVenomous() {
        return venomous;
    }

    public CheckBoxSearchOption getCanTalk() {
        return canTalk;
    }


}

