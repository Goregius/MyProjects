package com.main;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Stack;

class AnimalUndoManager {
    private Stack<List<String[]>> deletedAnimalData = new Stack<>();
    private Stack<List<String[]>> undeletedAnimalData = new Stack<>();

    void undoDelete(JTable table, AnimalData animalData) {
        var model = (DefaultTableModel) table.getModel();
        if (deletedAnimalData.size() == 0) {
            App.playWindowsExclamation();
            return;
        }

        var lastDeleted = deletedAnimalData.pop();
        undeletedAnimalData.push(lastDeleted);

        for (var animal : lastDeleted) {
            animalData.getAnimalsList().add(animal);
            model.addRow(animal);
        }

    }

    void redoDelete(JTable animalsTable, App app) {
        if (undeletedAnimalData.size() == 0) {
            App.playWindowsExclamation();
            return;
        }

        var lastUnDeleted = undeletedAnimalData.pop();
        try {
            deletedAnimalData.push(new ArrayList<>(lastUnDeleted));
        } catch (Exception e) {
            JOptionPane.showMessageDialog(animalsTable, e.getMessage(), "Casting Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        for (int i = 0; i < app.getAnimalData().getAnimalsList().size(); i++) {
            for (int stackIndex = 0; stackIndex < lastUnDeleted.size(); stackIndex++) {
                if (i < 0) continue;
                var animal = app.getAnimalData().getAnimalsList().get(i);

                if (Arrays.equals(animal, lastUnDeleted.get(stackIndex))) {
                    lastUnDeleted.remove(stackIndex);
                    app.getAnimalData().getAnimalsList().remove(i);
                    i--;

                }
            }
        }

        app.updateAnimalsTable();
    }

    void pushDelete(List<String[]> deletedData) {
        deletedAnimalData.push(deletedData);
    }
}
