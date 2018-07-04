package com.main;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;
import java.util.TimeZone;

public class SellAnimalDialog extends JDialog {
    private JPanel contentPane;
    private JButton sellButton;
    private JButton cancelButton;
    private JTextField textFieldYear;
    private JTextField textFieldMonth;
    private JTextField textFieldDay;
    private JLabel yearLabel;
    private JLabel monthLabel;
    private JLabel dayLabel;
    private JLabel animalLabel;
    private JButton removeDateButton;

    private String date;

    private SellAnimalDialog(App app, String animalName, String initialDate) {
        animalLabel.setText(animalName);
        if (initialDate.isEmpty()) {
            var formatter = new SimpleDateFormat("yyyy-MM-dd");
            var now = Calendar.getInstance(TimeZone.getTimeZone("UK/London"), Locale.UK).getTime();
            this.date = formatter.format(now);
        } else {
            this.date = initialDate;
        }

        setTextFields();
        animalLabel.setText(animalName);
        this.setTitle("Set Sell Date");
        setContentPane(contentPane);
        setModal(true);
        getRootPane().setDefaultButton(sellButton);

        sellButton.addActionListener(e -> onSell());
        cancelButton.addActionListener(e -> onCancel());
        removeDateButton.addActionListener(e -> onRemove());
        contentPane.registerKeyboardAction(e -> onCancel(), KeyStroke.getKeyStroke(KeyEvent.VK_ESCAPE, 0), JComponent.WHEN_ANCESTOR_OF_FOCUSED_COMPONENT);
        addWindowListener(new WindowAdapter() {
            public void windowClosing(WindowEvent e) {
                onCancel();
            }
        });
        //textFieldListeners();
        if (initialDate.isEmpty()) {
            removeDateButton.setVisible(false);
        }
        this.pack();
        this.setLocationRelativeTo(app.getMainPanel());
        this.setVisible(true);

    }

    public static void main(String[] args) {

    }

    static String showDialog(App app, String animalName, String initialDate) {
        return (new SellAnimalDialog(app, animalName, initialDate).date);
    }

    static String showDialog(App app, String animalName) {
        return showDialog(app, animalName, "");
    }

    private void onRemove() {
        date = "remove";
        setVisible(false);
        dispose();
    }

    //private void textFieldListeners() {
    //    textFieldYear.addFocusListener(new FocusAdapter() {
    //        @Override
    //        public void focusGained(FocusEvent e) {
    //            textFieldYear.selectAll();
    //        }
    //
    //        @Override
    //        public void focusLost(FocusEvent e) {
    //            if (!DateHelper.isYearValid(SellAnimalDialog.this.textFieldYear.getText())) {
    //                textErrorTip(textFieldYear);
    //            }
    //        }
    //    });
    //    textFieldMonth.addFocusListener(new FocusAdapter() {
    //        @Override
    //        public void focusGained(FocusEvent e) {
    //            textFieldMonth.selectAll();
    //        }
    //
    //        @Override
    //        public void focusLost(FocusEvent e) {
    //            if (!DateHelper.isMonthValid(SellAnimalDialog.this.textFieldMonth.getText())) {
    //                textErrorTip(textFieldMonth);
    //            }
    //        }
    //    });
    //    textFieldDay.addFocusListener(new FocusAdapter() {
    //        @Override
    //        public void focusGained(FocusEvent e) {
    //            textFieldDay.selectAll();
    //        }
    //
    //        @Override
    //        public void focusLost(FocusEvent e) {
    //            if (!DateHelper.isDayValid(SellAnimalDialog.this.textFieldDay.getText(), SellAnimalDialog.this.textFieldMonth.getText(), SellAnimalDialog.this.textFieldYear.getText())) {
    //                textErrorTip(textFieldDay);
    //            }
    //        }
    //    });
    //    textFieldYear.addKeyListener(new KeyAdapter() {
    //        @Override
    //        public void keyReleased(KeyEvent e) {
    //            if (DateHelper.isYearValid(SellAnimalDialog.this.textFieldYear.getText())) {
    //                textFieldYear.setForeground(Color.black);
    //            }
    //        }
    //    });
    //    textFieldMonth.addKeyListener(new KeyAdapter() {
    //        @Override
    //        public void keyReleased(KeyEvent e) {
    //            if (DateHelper.isMonthValid(SellAnimalDialog.this.textFieldMonth.getText())) {
    //                textFieldMonth.setForeground(Color.black);
    //
    //                if (DateHelper.isDayValid(SellAnimalDialog.this.textFieldDay.getText(), SellAnimalDialog.this.textFieldMonth.getText(), SellAnimalDialog.this.textFieldYear.getText())) {
    //                    textFieldDay.setForeground(Color.black);
    //                } else {
    //                    textErrorTip(textFieldDay);
    //                }
    //            }
    //        }
    //    });
    //    textFieldDay.addKeyListener(new KeyAdapter() {
    //        @Override
    //        public void keyReleased(KeyEvent e) {
    //            if (DateHelper.isDayValid(SellAnimalDialog.this.textFieldDay.getText(), SellAnimalDialog.this.textFieldMonth.getText(), SellAnimalDialog.this.textFieldYear.getText())) {
    //                textFieldDay.setForeground(Color.black);
    //            }
    //        }
    //    });
    //}

    private void setTextFields() {
        textFieldYear.setDocument(new JTextFieldLimit(4));
        textFieldMonth.setDocument(new JTextFieldLimit(2));
        textFieldDay.setDocument(new JTextFieldLimit(2));

        var dateSplit = date.split("-");
        if (dateSplit.length <= 2) return;

        textFieldYear.setText(dateSplit[0]);
        textFieldMonth.setText(dateSplit[1]);
        textFieldDay.setText(dateSplit[2]);
    }

    private void textErrorTip(JTextField textField) {
        textField.setForeground(Color.red);
        App.playWindowsExclamation();
    }

    private void onSell() {
        var yearText = textFieldYear.getText();
        var monthText = textFieldMonth.getText();
        var dayText = textFieldDay.getText();

        var now = Calendar.getInstance(TimeZone.getTimeZone("UK/London"), Locale.UK);
        if (yearText.isEmpty()) {
            yearText = String.valueOf(now.get(Calendar.YEAR));
            textFieldYear.setText(yearText);
        }
        if (monthText.isEmpty()) {
            monthText = String.valueOf(now.get(Calendar.MONTH) + 1);
            textFieldMonth.setText(monthText);
        }
        if (dayText.isEmpty()) {
            dayText = String.valueOf(now.get(Calendar.DAY_OF_MONTH));
            textFieldDay.setText(dayText);
        }

        var year = yearText;
        var month = monthText.matches("[1-9]") ? "0" + monthText.charAt(monthText.length() - 1) : monthText;
        var day = dayText.matches("[1-9]") ? "0" + dayText.charAt(dayText.length() - 1) : dayText;

        if (DateHelper.isDateFuture(year, month, day)) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(this.contentPane, "Can't set a date in the future!", "Input Error", JOptionPane.ERROR_MESSAGE);
            return;
        }
        else if (!DateHelper.isDateValidOrFuture(yearText, monthText, dayText)) {
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(this.contentPane, "Date invalid!", "Input Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        date = year + "-" + month + "-" + day;
        setVisible(false);
        dispose();
    }

    private void onCancel() {
        date = "";
        setVisible(false);
        dispose();
    }
}
