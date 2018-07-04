package com.main;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.event.KeyEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.text.NumberFormat;
import java.util.Locale;

public class RevenueDialog extends JDialog {
    private JPanel contentPane;
    private JButton buttonCancel;
    private JRadioButton dayRadioButton;
    private JRadioButton monthRadioButton;
    private JLabel yearLabel;
    private JTextField textFieldYear;
    private JLabel monthLabel;
    private JTextField textFieldMonth;
    private JLabel dayLabel;
    private JTextField textFieldDay;
    private JButton computeRevenueButton;

    private AnimalData animals;

    private RevenueDialog(App app, AnimalData animals, String date) {
        this.animals = animals;

        setContentPane(contentPane);
        setModal(true);
        setTitle("Compute Revenue");
        buttonCancel.addActionListener(e -> onCancel());

        // call onCancel() when cross is clicked
        setDefaultCloseOperation(DO_NOTHING_ON_CLOSE);
        addWindowListener(new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                onCancel();
            }
        });
        getRootPane().setDefaultButton(computeRevenueButton);
        // call onCancel() on ESCAPE
        contentPane.registerKeyboardAction(e -> onCancel(), KeyStroke.getKeyStroke(KeyEvent.VK_ESCAPE, 0), JComponent.WHEN_ANCESTOR_OF_FOCUSED_COMPONENT);

        dayRadioButton.addActionListener(e -> textFieldDay.setEnabled(true));
        monthRadioButton.addActionListener(e -> textFieldDay.setEnabled(false));
        computeRevenueButton.addActionListener(e -> onCompute());

        setTextFields(date);
        this.pack();
        this.setLocationRelativeTo(app.getMainPanel());
        this.setVisible(true);
    }
    private RevenueDialog(App app, AnimalData animals) {
        this(app, animals, "");
    }

    static void showDialog(App app, JTable table, AnimalData animals) {
        var selectedRowIndex = table.getSelectedRow();
        var model = (DefaultTableModel) table.getModel();
        if (model == null || selectedRowIndex == -1) {
            new RevenueDialog(app, animals);
        }
        else {
            var modelIndex = table.convertRowIndexToModel(selectedRowIndex);

            var dateCellObject = model.getValueAt(modelIndex, 6);
            var dateCellString = dateCellObject == null ? "" : dateCellObject.toString();

            new RevenueDialog(app, animals, dateCellString);
        }

    }
    private void setTextFields(String date) {
        textFieldYear.setDocument(new JTextFieldLimit(4));
        textFieldMonth.setDocument(new JTextFieldLimit(2));
        textFieldDay.setDocument(new JTextFieldLimit(2));

        var dateSplit = date.split("-");
        if (dateSplit.length <= 2) return;

        textFieldYear.setText(dateSplit[0]);
        textFieldMonth.setText(dateSplit[1]);
        textFieldDay.setText(dateSplit[2]);
    }

    private void onCompute() {
        var year = DateHelper.formatYear(textFieldYear.getText());
        var month = DateHelper.formatMonth(textFieldMonth.getText());
        var day = DateHelper.formatDay(textFieldDay.getText());
        var sum = 0.0;
        if (dayRadioButton.isSelected() && DateHelper.isDateValidOrFuture(year, month, day)) {
            sum = animals.computeRevenueDay(year, month, day);
        } else if (monthRadioButton.isSelected() && DateHelper.isDateValidOrFuture(year, month, "01")) {
            sum = animals.computeRevenueMonth(year, month);
        }
        NumberFormat formatter = NumberFormat.getCurrencyInstance(Locale.UK);

        JOptionPane.showMessageDialog(this, "Computed revenue: " + formatter.format(sum) + ".", "Revenue Output", JOptionPane.INFORMATION_MESSAGE);
    }

    private void onCancel() {
        // add your code here if necessary
        dispose();
    }


}
