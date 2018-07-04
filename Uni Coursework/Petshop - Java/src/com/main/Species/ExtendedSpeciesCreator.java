package com.main.Species;


import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class ExtendedSpeciesCreator {
    private ExtendedSpecies[] speciesList;

    private ExtendedSpeciesCreator() {
        speciesList = readSpeciesFromCsv();
    }

    public static ExtendedSpecies[] getAllSpecies() {
        return new ExtendedSpeciesCreator().speciesList;
    }

    private ExtendedSpecies[] readSpeciesFromCsv() {
        String line;
        final var cvsSplitBy = ", ";

        var speciesList = new ArrayList<ExtendedSpecies>();

        try (var br = new BufferedReader(new FileReader("Files\\Species\\Species.csv"))) {
            while ((line = br.readLine()) != null) {
                //Saves the line in the file to a String array.
                var separatedLine = line.split(cvsSplitBy, 8);
                var speciesArray = new String[8];

                //Copies separatedLine into the animal array so that all the rows have the same length of 8.
                System.arraycopy(separatedLine, 0, speciesArray, 0, separatedLine.length);
                var name = speciesArray[0];
                var animalClass = speciesArray[1];
                var order = speciesArray[2];
                var family = speciesArray[3];
                var genus = speciesArray[4];
                var species = speciesArray[5];
                int noOfLegs;
                try {
                    noOfLegs = Integer.parseInt(speciesArray[6]);
                } catch (NumberFormatException e) {
                    continue;
                }

                if (speciesArray[1].equals("Reptilia")) { //Reptile
                    var isVenomous = speciesArray[7].equals("venomous");
                    speciesList.add(new ExtendedSpecies(new Reptile(name, animalClass, order, family, genus, species, noOfLegs, isVenomous)));
                } else if (speciesArray[2].equals("Psittaciformes")) { //Parrot
                    var canTalk = speciesArray[7].equals("talking");
                    speciesList.add(new ExtendedSpecies(new Parrot(name, animalClass, order, family, genus, species, noOfLegs, canTalk)));
                } else { //Normal species
                    speciesList.add(new ExtendedSpecies(new Species(name, animalClass, order, family, genus, species, noOfLegs)));
                }
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        var array = new ExtendedSpecies[speciesList.size()];
        return speciesList.toArray(array);
    }
}
