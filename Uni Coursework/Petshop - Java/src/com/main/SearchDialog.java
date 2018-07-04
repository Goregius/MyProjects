package com.main;

import com.main.Species.ExtendedSpecies;
import com.main.Species.ExtendedSpeciesCreator;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;
import java.util.List;

public class SearchDialog extends JDialog {

    private JPanel contentPane;
    private JButton buttonSearch;
    private JButton buttonCancel;
    private JTextField textFieldName;
    private JTextField textFieldCommonName;
    private JTextField textFieldMainColour;
    private JRadioButton maleRadioButton;
    private JRadioButton femaleRadioButton;
    private JTextField textFieldClass;
    private JTextField textFieldOrder;
    private JTextField textFieldFamily;
    private JTextField textFieldSpecies;
    private JTextField textFieldLegsFrom;
    private JTextField textFieldLegsTo;
    private JCheckBox checkBoxIncludeVenomous;
    private JCheckBox checkBoxVenomous;
    private JCheckBox checkBoxCanTalk;
    private JCheckBox checkBoxIncludeCanTalk;
    private JCheckBox checkBoxIncludeSex;
    private JTextField textFieldGenus;
    private JLabel resultsLabel;

    private AnimalData animalData;
    private AnimalSearch searchQuery;

    private App app;

    private SearchDialog(App app, Component component) {
        this.app = app;
        this.animalData = app.getAnimalData();

        this.setTitle("Search");
        setIconImage(null);
        setContentPane(contentPane);
        setModal(true);

        getRootPane().setDefaultButton(buttonSearch);
        updateResultsLabel();
        setListeners();


        this.pack();
        this.setLocationRelativeTo(component);
        this.setVisible(true);
    }

    static void showDialog(App app, Component component) {
        new SearchDialog(app, component);
    }

    private void updateResultsLabel() {
        createSearchQuery(false);
        if (textValidityCheck() && searchQuery != null) {
            resultsLabel.setForeground(Color.black);
            resultsLabel.setText("Results: " + Integer.toString(animalSearch(animalData, searchQuery).size()));
        } else {
            resultsLabel.setText("Results: Input Error");
            resultsLabel.setForeground(Color.red);
        }
    }


    private void setListeners() {
        buttonSearch.addActionListener(e -> onSearch());

        buttonCancel.addActionListener(e -> onCancel());

        // call onCancel() when cross is clicked
        setDefaultCloseOperation(DO_NOTHING_ON_CLOSE);
        addWindowListener(new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                onCancel();
            }
        });

        // call onCancel() on ESCAPE
        contentPane.registerKeyboardAction(e -> onCancel(), KeyStroke.getKeyStroke(KeyEvent.VK_ESCAPE, 0), JComponent.WHEN_ANCESTOR_OF_FOCUSED_COMPONENT);

        checkBoxVenomous.addActionListener(e -> {
            checkBoxIncludeVenomous.setSelected(true);
            updateResultsLabel();
        });

        checkBoxCanTalk.addActionListener(e -> {
            checkBoxIncludeCanTalk.setSelected(true);
            updateResultsLabel();
        });

        ActionListener sexRadioListener = e -> {
            checkBoxIncludeSex.setSelected(true);
            updateResultsLabel();
        };
        maleRadioButton.addActionListener(sexRadioListener);
        femaleRadioButton.addActionListener(sexRadioListener);

        KeyAdapter keyListener = new KeyAdapter() {
            @Override
            public void keyReleased(KeyEvent e) {
                updateResultsLabel();
            }
        };
        textFieldName.addKeyListener(keyListener);
        textFieldCommonName.addKeyListener(keyListener);
        textFieldMainColour.addKeyListener(keyListener);
        textFieldClass.addKeyListener(keyListener);
        textFieldOrder.addKeyListener(keyListener);
        textFieldFamily.addKeyListener(keyListener);
        textFieldSpecies.addKeyListener(keyListener);
        textFieldLegsFrom.addKeyListener(keyListener);
        textFieldLegsTo.addKeyListener(keyListener);
        textFieldGenus.addKeyListener(keyListener);

        ActionListener checkBoxListener = e -> updateResultsLabel();
        checkBoxIncludeSex.addActionListener(checkBoxListener);
        checkBoxIncludeVenomous.addActionListener(checkBoxListener);
        checkBoxIncludeCanTalk.addActionListener(checkBoxListener);
        checkBoxVenomous.addActionListener(checkBoxListener);
        checkBoxCanTalk.addActionListener(checkBoxListener);
    }

    private void onSearch() {
        var success = createSearchQuery();
        if (success) {
            if (searchQuery == null) return;
            setSearchTable(animalData, searchQuery);
            setVisible(false);
            dispose();
        }
    }

    private boolean createSearchQuery() {
        return createSearchQuery(true);
    }

    private boolean createSearchQuery(boolean showMessage) {
        if (!legRangeValidIntegers()) {
            if (showMessage)
                JOptionPane.showMessageDialog(this, "No. Of Legs entries need to be valid integers.", "Input error", JOptionPane.ERROR_MESSAGE);
            searchQuery = null;
            return false;
        }

        Integer legsFrom = -1;
        if (!textFieldLegsFrom.getText().isEmpty()) {
            legsFrom = Integer.parseInt(textFieldLegsFrom.getText());
        }

        Integer legsTo = Integer.MAX_VALUE;
        if (!textFieldLegsTo.getText().isEmpty()) {
            legsTo = Integer.parseInt(textFieldLegsTo.getText());
        }
        if (legsFrom > legsTo) {
            if (showMessage)
                JOptionPane.showMessageDialog(this, "The second leg range input must be less than the first.", "Input error", JOptionPane.ERROR_MESSAGE);
            searchQuery = null;
            return false;
        }
        var legsRange = new Integer[]{legsFrom, legsTo};
        searchQuery = new AnimalSearch(textFieldName.getText(), textFieldCommonName.getText(), getSexSearchOption(), textFieldMainColour.getText(), textFieldClass.getText(), textFieldOrder.getText(), textFieldFamily.getText(), textFieldGenus.getText(), textFieldSpecies.getText(), legsRange, getCheckBoxSearchOption(checkBoxVenomous, checkBoxIncludeVenomous), getCheckBoxSearchOption(checkBoxCanTalk, checkBoxIncludeCanTalk));
        return true;
    }

    private Boolean textValidityCheck() {
        return legRangeValidIntegers();
    }

    private void setSearchTable(AnimalData animalData, AnimalSearch searchQuery) {
        var animals = animalSearch(animalData, searchQuery);

        animalData.getSearchData().clear();
        animalData.getSearchData().addAll(animals);
        app.updateSearchTable(animalData);
    }

    private void onCancel() {
        setVisible(false);
        dispose();
    }


    private List<String[]> animalSearch(AnimalData searchData, AnimalSearch searchCriteria) {
        var searchResult = new ArrayList<String[]>();

        var sexString = "";
        switch (searchCriteria.getSex()) {
            case MALE:
                sexString = "male";
                break;
            case FEMALE:
                sexString = "female";
                break;
        }

        var speciesList = ExtendedSpeciesCreator.getAllSpecies();

        for (var animal : searchData.getAnimalsList()) {
            ExtendedSpecies species = null;
            for (var speciesEl : speciesList) {
                if (speciesEl.getNameValue().toLowerCase().equals(animal[1].toLowerCase())) {
                    species = speciesEl;
                    break;
                }
            }
            if (species == null) { //No species found for animal name.
                if ((animal[0].toLowerCase().contains(searchCriteria.getName().toLowerCase()) || searchCriteria.getName().isEmpty()) &&
                        (animal[1].toLowerCase().contains(searchCriteria.getCommonName().toLowerCase()) || searchCriteria.getCommonName().isEmpty()) &&
                        (animal[3].equals(sexString) || sexString.isEmpty()) &&
                        (animal[4].toLowerCase().contains(searchCriteria.getMainColour().toLowerCase()) || searchCriteria.getMainColour().isEmpty()) &&
                        (searchCriteria.getCommonName().isEmpty()) &&
                        speciesInputsEmpty(searchCriteria)) {
                    searchResult.add(animal);
                }
            } else if ((animal[0].toLowerCase().contains(searchCriteria.getName().toLowerCase()) || searchCriteria.getName().isEmpty()) &&
                    (animal[1].toLowerCase().contains(searchCriteria.getCommonName().toLowerCase()) || searchCriteria.getCommonName().isEmpty()) &&
                    (animal[3].equals(sexString) || sexString.isEmpty()) &&
                    (animal[4].toLowerCase().contains(searchCriteria.getMainColour().toLowerCase()) || searchCriteria.getMainColour().isEmpty()) &&
                    (species.getClassValue().toLowerCase().contains(searchCriteria.getClassName().toLowerCase()) || searchCriteria.getClassName().isEmpty()) &&
                    (species.getOrderValue().toLowerCase().contains(searchCriteria.getOrder().toLowerCase()) || searchCriteria.getOrder().isEmpty()) &&
                    (species.getFamilyValue().toLowerCase().contains(searchCriteria.getFamily().toLowerCase()) || searchCriteria.getFamily().isEmpty()) &&
                    (species.getGenusValue().toLowerCase().contains(searchCriteria.getGenus().toLowerCase()) || searchCriteria.getGenus().isEmpty()) &&
                    (species.getSpeciesValue().toLowerCase().contains(searchCriteria.getSpecies().toLowerCase()) || searchCriteria.getSpecies().isEmpty()) &&
                    (species.getNoOfLegs() >= searchCriteria.getNoOfLegsRange()[0] && species.getNoOfLegs() <= searchCriteria.getNoOfLegsRange()[1])) {

                boolean searchMatches = true;

                if (searchCriteria.getVenomous() != CheckBoxSearchOption.NO_SEARCH) {
                    if (species.getVenomous() == null) continue;
                    searchMatches = species.getVenomous() == (searchCriteria.getVenomous() == CheckBoxSearchOption.YES);
                }
                if (searchCriteria.getCanTalk() != CheckBoxSearchOption.NO_SEARCH) {
                    if (species.getCanTalk() == null) continue;
                    searchMatches &= species.getCanTalk() == (searchCriteria.getCanTalk() == CheckBoxSearchOption.YES);
                }

                if (searchMatches) {
                    searchResult.add(animal);
                }
            }
        }
        return searchResult;
    }

    private boolean speciesInputsEmpty(AnimalSearch searchCriteria) {
        return searchCriteria.getClassName().isEmpty() &&
                searchCriteria.getOrder().isEmpty() &&
                searchCriteria.getFamily().isEmpty() &&
                searchCriteria.getGenus().isEmpty() &&
                searchCriteria.getSpecies().isEmpty() &&
                searchCriteria.getNoOfLegsRange()[0] == -1 &&
                searchCriteria.getNoOfLegsRange()[1] == Integer.MAX_VALUE &&
                searchCriteria.getVenomous() == CheckBoxSearchOption.NO_SEARCH &&
                searchCriteria.getCanTalk() == CheckBoxSearchOption.NO_SEARCH;
    }

    private boolean legRangeValidIntegers() {
        return textFieldLegsFrom.getText().matches("\\d+|^$") && textFieldLegsTo.getText().matches("\\d+|^$");
    }

    private SexSearchOption getSexSearchOption() {
        if (checkBoxIncludeSex.isSelected()) {
            if (maleRadioButton.isSelected()) {
                return SexSearchOption.MALE;
            } else {
                return SexSearchOption.FEMALE;
            }
        } else {
            return SexSearchOption.NO_SEARCH;
        }
    }

    private CheckBoxSearchOption getCheckBoxSearchOption(JCheckBox checkBox, JCheckBox include) {
        if (include.isSelected()) {
            if (checkBox.isSelected()) {
                return CheckBoxSearchOption.YES;
            } else {
                return CheckBoxSearchOption.NO;
            }
        } else {
            return CheckBoxSearchOption.NO_SEARCH;
        }
    }


}



