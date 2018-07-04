/*  Written using java 10.
    This possibly means that you need java 10 to compile this.
    The use of 'var' is exclusive to java 10+.
 */

package com.main;

import javax.swing.*;
import javax.swing.filechooser.FileNameExtensionFilter;
import javax.swing.table.DefaultTableCellRenderer;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableRowSorter;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.*;
import java.util.List;

public class App {

    private AnimalData animalData = new AnimalData();
    private AnimalUndoManager animalUndoManager = new AnimalUndoManager();
    private JPanel mainPanel;
    private JTabbedPane tabbedPane1;
    private JTable animalsTable;
    private JTable animalsSearchTable;
    private JButton openSearchMenuButton;
    private JButton addAnimalButton;
    private JButton addAnimalsFromFileButton;
    private JButton updateButton;
    private JButton writeButton;
    private JButton addLastDeletedButton;
    private JButton sellAnimalsButton;
    private JButton undoDeleteButton;

    private App() {
        setupAnimalListTable();
        setupSearchListTable();

        animalsTable.setAutoCreateRowSorter(true);
        animalsSearchTable.setAutoCreateRowSorter(true);


        createActionListeners();
        createKeyInputListeners();

        animalsTable.addMouseListener(new MouseAdapter() {
            @Override
            public void mousePressed(MouseEvent e) {
                if (e.isPopupTrigger())
                    doPop(e);
            }

            @Override
            public void mouseReleased(MouseEvent e) {

                if (e.isPopupTrigger())
                {
                    var source = (JTable)e.getSource();
                    var row = source.rowAtPoint( e.getPoint() );
                    var column = source.columnAtPoint( e.getPoint() );

                    if (! source.isRowSelected(row))
                        source.changeSelection(row, column, false, false);
                    doPop(e);
                }

            }


        });
        addAnimalsFromFileButton.addActionListener(e -> createAddAnimalsFileDialog());
        sellAnimalsButton.addActionListener(e -> sellSelectedAnimal(animalsTable));
        animalsTable.addMouseListener(new java.awt.event.MouseAdapter() {
            @Override
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                if (evt.getClickCount() == 2) {
                    int row = animalsTable.rowAtPoint(evt.getPoint());
                    int col = animalsTable.columnAtPoint(evt.getPoint());
                    if (row >= 0 && col == 6) {
                        sellSelectedAnimal(animalsTable);
                    }
                }
            }
        });

    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(() -> {
            if (System.getProperty("os.name").indexOf("Windows") == 0) {
                try {
                    UIManager.setLookAndFeel("com.sun.java.swing.plaf.windows.WindowsLookAndFeel");
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
            setupFrame();
        });
    }

    JPanel getMainPanel() {
        return mainPanel;
    }

    static void playWindowsExclamation() {
        final var runnable = (Runnable) Toolkit.getDefaultToolkit().getDesktopProperty("win.sound.exclamation");
        if (runnable != null) runnable.run();
    }

    private static void setupFrame() {
        var frame = new JFrame("Petshop");
        frame.setContentPane(new App().mainPanel);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(1500, 1000);
        frame.setLocationRelativeTo(null);
        frame.setVisible(true);
    }

    AnimalData getAnimalData() {
        return animalData;
    }

    private void createActionListeners() {
        openSearchMenuButton.addActionListener(e -> SearchDialog.showDialog(this, mainPanel));
        addAnimalButton.addActionListener(createAddAnimalDialog());
        updateButton.addActionListener(e -> updateAnimalsTable());
        writeButton.addActionListener(e -> writeTableToFile());
        addLastDeletedButton.addActionListener(e -> computeRevenue());
    }

    private void computeRevenue() {
        RevenueDialog.showDialog(this, animalsTable, animalData);
    }

    private void createKeyInputListeners() {
        addMainUndo(animalsTable);
        addMainUndo(mainPanel);
        addMainUndo(openSearchMenuButton);
        addMainUndo(addAnimalsFromFileButton);
        addMainUndo(updateButton);
        addMainUndo(writeButton);
        addMainUndo(addLastDeletedButton);

        addMainRedo(animalsTable);
        addMainRedo(mainPanel);
        addMainRedo(openSearchMenuButton);
        addMainRedo(addAnimalsFromFileButton);
        addMainRedo(updateButton);
        addMainRedo(writeButton);
        addMainRedo(addLastDeletedButton);

        createAnimalDialogFromKeys(animalsTable);
        createAnimalDialogFromKeys(mainPanel);
        createAnimalDialogFromKeys(openSearchMenuButton);
        createAnimalDialogFromKeys(addAnimalsFromFileButton);
        createAnimalDialogFromKeys(updateButton);
        createAnimalDialogFromKeys(writeButton);
        createAnimalDialogFromKeys(addLastDeletedButton);

        createAnimalsDialogFromKeys(animalsTable);
        createAnimalsDialogFromKeys(mainPanel);
        createAnimalsDialogFromKeys(openSearchMenuButton);
        createAnimalsDialogFromKeys(addAnimalsFromFileButton);
        createAnimalsDialogFromKeys(updateButton);
        createAnimalsDialogFromKeys(writeButton);
        createAnimalsDialogFromKeys(addLastDeletedButton);

        writeToFileFromKeys(animalsTable);
        writeToFileFromKeys(mainPanel);
        writeToFileFromKeys(openSearchMenuButton);
        writeToFileFromKeys(addAnimalsFromFileButton);
        writeToFileFromKeys(updateButton);
        writeToFileFromKeys(writeButton);
        writeToFileFromKeys(addLastDeletedButton);

        computeRevenueFromKeys(animalsTable);
        computeRevenueFromKeys(mainPanel);
        computeRevenueFromKeys(openSearchMenuButton);
        computeRevenueFromKeys(addAnimalsFromFileButton);
        computeRevenueFromKeys(updateButton);
        computeRevenueFromKeys(writeButton);
        computeRevenueFromKeys(addLastDeletedButton);

        sellFromKeys(animalsTable);
        sellFromKeys(mainPanel);
        sellFromKeys(openSearchMenuButton);
        sellFromKeys(addAnimalsFromFileButton);
        sellFromKeys(updateButton);
        sellFromKeys(writeButton);
        sellFromKeys(addLastDeletedButton);

        animalsTable.getActionMap().put("Delete", new AbstractAction("Delete") {
            public void actionPerformed(ActionEvent evt) {
                deleteRows();
            }
        });
        animalsTable.getInputMap().put(KeyStroke.getKeyStroke("DELETE"), "Delete");
    }

    private void createAnimalDialogFromKeys(JComponent component) {
        component.getActionMap().put("Add", new AbstractAction("Add") {
            public void actionPerformed(ActionEvent evt) {
                AddAnimalDialog.showDialog(App.this);
            }
        });
        component.getInputMap().put(KeyStroke.getKeyStroke("control E"), "Add");
    }

    private void createAnimalsDialogFromKeys(JComponent component) {
        component.getActionMap().put("Add File", new AbstractAction("Add File") {
            public void actionPerformed(ActionEvent evt) {
                 createAddAnimalsFileDialog();
            }
        });
        component.getInputMap().put(KeyStroke.getKeyStroke("control O"), "Add File");
    }

    private void writeToFileFromKeys(JComponent component) {
        component.getActionMap().put("Write", new AbstractAction("Write") {
            public void actionPerformed(ActionEvent evt) {
                writeTableToFile();
            }
        });
        component.getInputMap().put(KeyStroke.getKeyStroke("control S"), "Write");
    }

    private void computeRevenueFromKeys(JComponent component) {
        component.getActionMap().put("Revenue", new AbstractAction("Revenue") {
            public void actionPerformed(ActionEvent evt) {
                computeRevenue();
            }
        });
        component.getInputMap().put(KeyStroke.getKeyStroke("control R"), "Revenue");
    }

    private void sellFromKeys(JComponent component) {
        component.getActionMap().put("Sell", new AbstractAction("Sell") {
            public void actionPerformed(ActionEvent evt) {
                sellSelectedAnimal(animalsTable);
            }
        });
        component.getInputMap().put(KeyStroke.getKeyStroke("control D"), "Sell");
    }

    void updateAnimalsTable() {

        //Clears the table.
        var model = (DefaultTableModel) animalsTable.getModel();
        model.setRowCount(0);

        for (var animal : animalData.getAnimalsList()) {
            model.addRow(animal);
        }

    }

    private void updateSearchTable() {
        if (animalData.getSearchData() == null) return;

        var model = (DefaultTableModel) animalsSearchTable.getModel();
        //Clears the table.
        model.setRowCount(0);
        for (var animal : animalData.getSearchData()) {
            model.addRow(animal);
        }
    }

    void updateSearchTable(AnimalData animals) {
        animalData.setSearchData(animals);

        updateSearchTable();
    }

    private void sellSelectedAnimal(JTable table) {
        var selectedRowIndex = table.getSelectedRow();
        if (selectedRowIndex == -1) {
            playWindowsExclamation();
            return;
        }

        var model = (DefaultTableModel) table.getModel();
        var modelIndex = animalsTable.convertRowIndexToModel(selectedRowIndex);
        var dateCellObject = model.getValueAt(modelIndex, 6);
        var dateCellString = dateCellObject == null ? "" : dateCellObject.toString();
        var nameCellObject = model.getValueAt(modelIndex, 0);
        var nameCellString = nameCellObject == null ? "" : nameCellObject.toString();

        String date;
        if (dateCellString.isEmpty()) {
            date = SellAnimalDialog.showDialog(this, nameCellString);
            if (Objects.equals(date, "remove")) {
                searchAnimalFromRow(modelIndex)[6] = "";
                model.setValueAt("", modelIndex, 6);
                return;
            }
            if (!date.isEmpty())
                searchAnimalFromRow(modelIndex)[6] = date;
            model.setValueAt(date, modelIndex, 6);
        } else {
            var result = JOptionPane.showConfirmDialog(this.mainPanel, "Edit sell date of animal previously listed as sold?", "Edit", JOptionPane.OK_CANCEL_OPTION);
            if (result == 0) {
                date = SellAnimalDialog.showDialog(this, nameCellString, dateCellString);
                if (Objects.equals(date, "remove")) {
                    searchAnimalFromRow(modelIndex)[6] = "";
                    model.setValueAt("", modelIndex, 6);
                    return;
                }
                if (!date.isEmpty()) {
                    searchAnimalFromRow(modelIndex)[6] = date;
                    model.setValueAt(date, modelIndex, 6);
                }

            }
        }

    }

    private String[] searchAnimalFromRow(int row) {

        var rowName = animalsTable.getModel().getValueAt(row, 0);
        var rowCommonName = animalsTable.getModel().getValueAt(row, 1);
        var rowPrice = animalsTable.getModel().getValueAt(row, 2);
        var rowSex = animalsTable.getModel().getValueAt(row, 3);
        var rowColour = animalsTable.getModel().getValueAt(row, 4);
        var rowArrivalDate = animalsTable.getModel().getValueAt(row, 5);

        var rowAnimal = new String[]{String.valueOf(rowName), String.valueOf(rowCommonName), String.valueOf(rowPrice), String.valueOf(rowSex), String.valueOf(rowColour), String.valueOf(rowArrivalDate), ""};

        for (var animal : animalData.getAnimalsList()) {
            rowAnimal[6] = animal[6];
            if (Arrays.equals(animal, rowAnimal)) {
                return animal;
            }
        }
        return new String[]{};
    }

    private void createAddAnimalsFileDialog() {
        var animals = AnimalFileDialog.showDialog(this);
        if (animals != null) {
            animalData.getAnimalsList().addAll(animals);
            updateAnimalsTable();
        }
    }

    private void addMainUndo(JComponent component) {
        component.getActionMap().put("Undo", new AbstractAction("Undo") {
            public void actionPerformed(ActionEvent evt) {
                animalUndoManager.undoDelete(App.this.animalsTable, App.this.animalData);
            }
        });
        component.getInputMap().put(KeyStroke.getKeyStroke("control Z"), "Undo");
    }

    private void addMainRedo(JComponent component) {
        component.getActionMap().put("Redo", new AbstractAction("Redo") {
            public void actionPerformed(ActionEvent evt) {
                animalUndoManager.redoDelete(App.this.animalsTable, App.this);
            }
        });
        component.getInputMap().put(KeyStroke.getKeyStroke("control shift Z"), "Redo");
    }

    private ActionListener createAddAnimalDialog() {
        return e -> AddAnimalDialog.showDialog(this);
    }

    private void deleteRows() {
        var selectedRowIndexes = animalsTable.getSelectedRows();
        if (selectedRowIndexes.length == 0) {
            playWindowsExclamation();
            return;
        }
        if (!showDeleteConfirmDialog()) return;
        var model = (DefaultTableModel) animalsTable.getModel();
        var deletedData = new ArrayList<String[]>();

        if (animalsTable.getSelectedRows().length == animalData.getAnimalsList().size()) {
            //Faster for deleting all rows.
            deletedData.addAll(animalData.getAnimalsList());
            animalData.getAnimalsList().clear();
            updateAnimalsTable();
        } else {
            while (animalsTable.getSelectedRows().length > 0) {
                var modelIndex = animalsTable.convertRowIndexToModel(animalsTable.getSelectedRows()[0]);
                model.removeRow(modelIndex);

                deletedData.add(animalData.getAnimalsList().get(modelIndex));
                animalData.getAnimalsList().remove(modelIndex);
            }
        }


        animalUndoManager.pushDelete(deletedData);
        animalsTable.clearSelection();
    }

    private void writeTableToFile() {
        var list = animalData.getAnimalsList(true);
        var unsold = new ArrayList<String[]>();
        var sold = new ArrayList<String[]>();

        for (var animal : list) {
            if (animal[6] == null) {
                unsold.add(animal);
            }
            else {
                sold.add(animal);
            }
        }

        sold.sort(Comparator.comparing(o -> {
            try {
                if (o[6] == null) {
                    return (new Date((int) 10e15));
                }
                return (new SimpleDateFormat("yyyy-MM-dd").parse(o[6]));
            } catch (Exception e) {
                return (new Date((int) 10e15));
            }
        }));
        unsold.sort(Comparator.comparing(o -> {
            try {
                if (o[5] == null) {
                    return (new Date((int) 10e15));
                }
                return (new SimpleDateFormat("yyyy-MM-dd").parse(o[5]));
            } catch (Exception e) {
                return (new Date((int) 10e15));
            }
        }));
        list = new ArrayList<>(sold);
        list.addAll(unsold);

        PrintWriter pw;
        try {
            var fileChooser = new JFileChooser();
            fileChooser.setDialogType(JFileChooser.SAVE_DIALOG);
            //Removes "all files" option.
            fileChooser.removeChoosableFileFilter(fileChooser.getFileFilter());
            var filter = new FileNameExtensionFilter("CSV (*.csv)", "csv");
            fileChooser.setFileFilter(filter);
            fileChooser.setCurrentDirectory(new File("Files"));
            var opt = fileChooser.showSaveDialog(this.mainPanel);

            if (opt == 0) {
                if (!fileChooser.getSelectedFile().toString().endsWith(".csv")) {
                    fileChooser.setSelectedFile(new File(fileChooser.getSelectedFile() + ".csv"));
                }
                pw = new PrintWriter(fileChooser.getSelectedFile());
            } else {
                return;
            }

        } catch (FileNotFoundException e) {
            e.printStackTrace();
            App.playWindowsExclamation();
            JOptionPane.showMessageDialog(null, e.getMessage(), "FileNotFoundException", JOptionPane.ERROR_MESSAGE);
            return;
        }

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < list.size(); i++) {
            var row = list.get(i);
            for (int j = 0; j < row.length; j++) {
                if (row[j] != null)
                    builder.append(row[j]);
                if (j < row.length - 1) {
                    if (row[j + 1] != null) {
                        builder.append(", ");
                    }
                }

            }
            if (i < list.size() - 1) {
                builder.append("\n");
            }
        }

        pw.write(builder.toString());
        pw.close();
    }

    private boolean showDeleteConfirmDialog() {
        var result = JOptionPane.showConfirmDialog(this.mainPanel, "Delete selected row(s)?", "Delete", JOptionPane.OK_CANCEL_OPTION);
        //OK = 0, CANCEL = 2.
        return result == 0;
    }

    private void setupSearchListTable() {
        var m = new DefaultTableModel(new String[]{
                "Name",
                "Common Name",
                "price",
                "sex",
                "main colour",
                "arrival date",
                "selling date"
        }, 0) {
            //Readonly.
            @Override
            public boolean isCellEditable(int row, int column) {
                return false;
            }
        };
        animalsSearchTable.setModel(m);

        //Adds a £ to the start of each price cell without changing the data in the cell.
        animalsSearchTable.getColumnModel().getColumn(2).setCellRenderer(new DefaultTableCellRenderer() {
            @Override
            protected void setValue(Object value) {
                super.setValue("£" + value);
            }
        });

        updateSearchTable();
    }

    private void setupAnimalListTable() {
        String[] columns = {
                "Name",
                "Common Name",
                "price",
                "sex",
                "main colour",
                "arrival date",
                "selling date"
        };

        DefaultTableModel model = new DefaultTableModel(columns, 0) {
            //Readonly.
            @Override
            public boolean isCellEditable(int row, int column) {
                return false;
            }
        };

        animalsTable.setModel(model);

        updateAnimalsTable();
        updateSearchTable();

        //Adds a £ to the start of each price cell without changing the data in the cell.
        animalsTable.getColumnModel().getColumn(2).setCellRenderer(new DefaultTableCellRenderer() {
            @Override
            protected void setValue(Object value) {
                super.setValue("£" + value);
            }
        });


    }

    private void doPop(MouseEvent e) {
        var menu = new JPopupMenu();
        menu.add("Sell Animal (Ctrl+D)").addActionListener(e1 -> sellSelectedAnimal(animalsTable));
        menu.add(new JSeparator());
        menu.add("Compute Revenue (Ctrl+R)").addActionListener(e1 -> computeRevenue());
        menu.add(new JSeparator());
        menu.add("Delete (Delete)").addActionListener(e1 -> deleteRows());
        menu.show(e.getComponent(), e.getX(), e.getY());
    }
}
