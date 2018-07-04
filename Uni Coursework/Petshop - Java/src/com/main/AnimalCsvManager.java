package com.main;

import javax.swing.*;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

final class AnimalCsvManager {

    private AnimalCsvManager() {
    } //So you can't initialize this class.

    static List<String[]> readAnimalsFromCsvRelative(String relativeFilePath) {
        return readAnimalsFromCsvAbsolute("Files\\" + relativeFilePath);
    }

    static List<String[]> readAnimalsFromCsvAbsolute(String absoluteFilePath) {
        String line;
        var cvsSplitBy = ", ";

        var animals = new ArrayList<String[]>();
        var failCount = 0;

        try (var br = new BufferedReader(new FileReader(absoluteFilePath))) {
            while ((line = br.readLine()) != null) {

                //Saves the line in the file to a String array.
                var separatedLine = line.split(cvsSplitBy, 7);
                if (TableTextValidator.isInvalidLine(separatedLine)) {
                    failCount++;
                    continue;
                }

                var animal = new String[7];

                //Copies separatedLine into the animal array so that all the rows have the same length of 7.
                System.arraycopy(separatedLine, 0, animal, 0, separatedLine.length);

                if (animal[5] == null) {
                    animal[5] = DateHelper.getDateString();
                }

                animals.add(animal);
            }

        } catch (IOException e) {
            e.printStackTrace();
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(null, e.getMessage(), "IOException", JOptionPane.ERROR_MESSAGE);
            return new ArrayList<>();
        }

        if (animals.size() == 0) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(null, "None of the lines could be parsed.\nPlease check the format of the file.", "CSV Parse Error", JOptionPane.ERROR_MESSAGE);
        } else if (failCount > 0) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(null, failCount + " lines failed to load.\nPlease check the format of the file.", "CSV Parse Error", JOptionPane.ERROR_MESSAGE);
        }
        return animals;
    }


}
