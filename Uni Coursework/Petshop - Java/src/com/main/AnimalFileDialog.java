package com.main;

import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import java.awt.*;
import java.io.File;
import java.util.List;

class AnimalFileDialog extends JFileChooser {

    private List<String[]> fileAnimals;

    private AnimalFileDialog(Component parent) {

        var filter = new FileNameExtensionFilter("CSV (*csv)", "csv");
        this.setFileFilter(filter);
        this.setCurrentDirectory(new File(".\\Files"));
        int returnVal = this.showOpenDialog(parent);
        if (returnVal != JFileChooser.APPROVE_OPTION) {
            return;
        }

        fileAnimals = AnimalCsvManager.readAnimalsFromCsvAbsolute(this.getSelectedFile().getAbsolutePath());

    }

    static List<String[]> showDialog(App app) {
        return (new AnimalFileDialog(app.getMainPanel())).fileAnimals;
    }

}
