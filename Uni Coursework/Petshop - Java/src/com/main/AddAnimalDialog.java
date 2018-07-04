package com.main;

import javax.swing.*;
import java.text.SimpleDateFormat;

public class AddAnimalDialog extends JDialog {
    private JPanel contentPane;
    private JTextField textFieldName;
    private JTextField textFieldCommonName;
    private JTextField textFieldPrice;
    private JTextField textFieldMainColour;
    private JButton addAnimalButton;
    private JButton cancelButton;
    private JRadioButton maleRadioButton;
    private JRadioButton femaleRadioButton;
    private JTextField textFieldArrivalYear;
    private JTextField textFieldArrivalMonth;
    private JTextField textFieldArrivalDay;
    private JLabel nameLabel;
    private JTextField textFieldSellingYear;
    private JTextField textFieldSellingMonth;
    private JTextField textFieldSellingDay;
    private JButton buttonOK;
    private JButton buttonCancel;

    private App app;

    private AddAnimalDialog(App app) {
        this.app = app;

        this.setTitle("Add Animal");
        setContentPane(contentPane);
        setModal(true);
        getRootPane().setDefaultButton(addAnimalButton);

        setListeners();

        nameLabel.setAlignmentX(SwingConstants.RIGHT);
        this.pack();
        this.setLocationRelativeTo(app.getMainPanel());
        this.setVisible(true);

        addAnimalButton.addActionListener(e -> onAddAnimalButtonPress());
        cancelButton.addActionListener(e -> onCancelButtonPress());
    }

    static void showDialog(App app) {
        new AddAnimalDialog(app);
    }

    private void setListeners() {
        addAnimalButton.addActionListener(e -> onAddAnimalButtonPress());
        cancelButton.addActionListener(e -> onCancelButtonPress());

    }

    private void onAddAnimalButtonPress() {
        if (!addAnimal()) {
            return;
        }

        setVisible(false);
        dispose();
    }

    private boolean addAnimal() {
        if (textFieldName.getText().isEmpty()) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(this, "Name is empty.", "Input Error!", JOptionPane.ERROR_MESSAGE);
            return false;
        }
        if (textFieldCommonName.getText().isEmpty()) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(this, "Common name is empty.", "Input Error!", JOptionPane.ERROR_MESSAGE);
            return false;
        }
        if (textFieldMainColour.getText().isEmpty()) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(this, "Colour is empty.", "Input Error!", JOptionPane.ERROR_MESSAGE);
            return false;
        }
        var name = textFieldName.getText().substring(0, 1).toUpperCase() + textFieldName.getText().substring(1);
        var commonName = textFieldCommonName.getText().substring(0, 1).toUpperCase() + textFieldCommonName.getText().substring(1);
        var price = textFieldPrice.getText();

        String sex;

        if (maleRadioButton.isSelected()) {
            sex = "male";
        } else if (femaleRadioButton.isSelected()) {
            sex = "female";
        } else {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(this, "Select a sex (Male/Female).", "Sex not selected.", JOptionPane.ERROR_MESSAGE);
            return false;
        }

        var mainColour = textFieldMainColour.getText().toLowerCase();

        String arrivalDate;


        String sellingDate;

        if (textFieldSellingYear.getText().isEmpty() || textFieldSellingMonth.getText().isEmpty() || textFieldSellingDay.getText().isEmpty()) {
            sellingDate = "";
        } else {
            var year = textFieldSellingYear.getText();
            var month = textFieldSellingMonth.getText();
            var day = textFieldSellingDay.getText();
            if (DateHelper.isDateValidOrFuture(year, month, day)) {
                sellingDate = year + "-" + DateHelper.formatMonth(month) + "-" + DateHelper.formatDay(day);
            } else {
                App.playWindowsExclamation();
                JOptionPane.showMessageDialog(this, "Invalid Selling Date.", "Date Input Error!", JOptionPane.ERROR_MESSAGE);
                return false;
            }
        }

        if (textFieldArrivalYear.getText().isEmpty() || textFieldArrivalMonth.getText().isEmpty() || textFieldArrivalDay.getText().isEmpty()) {
            arrivalDate = DateHelper.getDateString();
        } else {
            var year = textFieldArrivalYear.getText();
            var month = textFieldArrivalMonth.getText();
            var day = textFieldArrivalDay.getText();
            if (DateHelper.isDateValidOrFuture(year, month, day)) {
                arrivalDate = year + "-" + DateHelper.formatMonth(month) + "-" + DateHelper.formatDay(day);
            } else {
                App.playWindowsExclamation();
                JOptionPane.showMessageDialog(this, "Invalid Arrival Date.", "Date Input Error!", JOptionPane.ERROR_MESSAGE);
                return false;
            }
        }

        if (!arrivalDate.isEmpty() && !sellingDate.isEmpty()) {
            try {
                var arrival = new SimpleDateFormat("yyyy-MM-dd").parse(arrivalDate);
                var selling = new SimpleDateFormat("yyyy-MM-dd").parse(sellingDate);
                if (arrival.after(selling)) {
                    App.playWindowsExclamation();
                    JOptionPane.showMessageDialog(this, "Arrival date can't be after the selling date.", "Date Input Error!", JOptionPane.ERROR_MESSAGE);
                    return false;
                }
            } catch (Exception e) {
                return false;
            }
        }


        String[] animal = {name, commonName, price, sex, mainColour, arrivalDate, sellingDate};

        if (TableTextValidator.isInvalidLine(animal)) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(this, "Price is not the correct format.", "Input Error!", JOptionPane.ERROR_MESSAGE);
            return false;
        }

        app.getAnimalData().getAnimalsList().add(animal);
        app.updateAnimalsTable();
        return true;
    }

    private void onCancelButtonPress() {
        setVisible(false);
        dispose();
    }
}
