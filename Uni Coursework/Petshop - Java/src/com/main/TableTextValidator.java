package com.main;

public class TableTextValidator {

    static boolean isInvalidLine(String[] line) {
        boolean valid = true;
        switch (line.length) {
            case 7:
                valid = isValidDateOrEmpty(line[6]);
            case 6:
                valid &= isValidDateOrEmpty(line[5]);
            case 5:
                valid &= !line[0].isEmpty() && !line[1].isEmpty() && isValidCurrency(line[2]) && isValidSex(line[3]) && !line[4].isEmpty();
                return !valid;
            default:
                return true;
        }

    }


    private static boolean isValidCurrency(String text) {
        return text.matches("\\d+(.\\d\\d)?");
    }

    private static boolean isValidSex(String text) {
        return text.equals("male") || text.equals("female");
    }

    private static boolean isValidDateOrEmpty(String text) {
        if (text.isEmpty()) {
            return true;
        }
        if (text.length() == 10) {
            return DateHelper.isDateValidOrFuture(text.substring(0, 4), text.substring(5, 7), text.substring(8, 10)) && text.charAt(4) == '-' && text.charAt(7) == '-';
        }

        return false;


    }
}
