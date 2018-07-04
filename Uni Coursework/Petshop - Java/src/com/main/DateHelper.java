package com.main;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;
import java.util.TimeZone;

class DateHelper {

    static String getDateString() {
        return getYearString() + "-" + getMonthString() + "-" + getDayString();
    }

    static Date dateStringToDate(String dateString) {
        try {
            if (isDateValidOrFuture(dateString))
                return new SimpleDateFormat("yyyy-MM-dd").parse(dateString);
        } catch (Exception ignored) {
        }

        return null;
    }

    static String formatYear(String year) {
        StringBuilder yearBuilder = new StringBuilder(year);
        for (int i = yearBuilder.length(); i <= 4; i++) {
            yearBuilder.append("0");
        }
        year = yearBuilder.toString();
        return year.substring(0, 4);
    }

    static String formatMonth(String month) {
        if (month.isEmpty()) return "00";
        return (month.matches("[1-9]") ? "0" + month.charAt(month.length() - 1) : month).substring(0, 2);
    }

    static String formatDay(String day) {
        if (day.isEmpty()) return "00";
        return (day.matches("[1-9]") ? "0" + day.charAt(day.length() - 1) : day).substring(0, 2);
    }

    private static String getYearString() {
        var now = Calendar.getInstance(TimeZone.getTimeZone("UK/London"), Locale.UK);
        return now.get(Calendar.YEAR) <= 9 ? "0" + String.valueOf((now.get(Calendar.YEAR))) : String.valueOf((now.get(Calendar.YEAR)));
    }

    private static String getMonthString() {
        var now = Calendar.getInstance(TimeZone.getTimeZone("UK/London"), Locale.UK);
        var month = now.get(Calendar.MONTH) + 1;
        return month <= 9 ? "0" + String.valueOf(month) : String.valueOf(month);
    }

    private static String getDayString() {
        var now = Calendar.getInstance(TimeZone.getTimeZone("UK/London"), Locale.UK);
        return now.get(Calendar.DAY_OF_MONTH) <= 9 ? "0" + String.valueOf((now.get(Calendar.DAY_OF_MONTH))) : String.valueOf((now.get(Calendar.DAY_OF_MONTH)));
    }

    private static boolean isDateValid(String year, String month, String day) {
        if ((day+month+year).trim().equals(""))
        {
            return false;
        }
        else {
            SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd");
            dateFormat.setLenient(false);
            try
            {
                dateFormat.parse(year + "-" + month + "-" + day);
            }
            // Date format is invalid
            catch (ParseException e)
            {
                return false;
            }
            //Return true if date format is valid
            return true;
        }

    }
    private static boolean isDateValidOrFuture(String date) {
        if (date.length() < 10) return false;
        return isDateValidOrFuture(date.substring(0, 4), date.substring(5, 7), date.substring(8, 10));
    }

    static boolean isDateValidOrFuture(String year, String month, String day) {
        if (day.length() < 2 || month.length() < 2 || year.length() < 4) return false;
        return isDateValid(year, month, day) && !isDateFuture(year, month, day);
    }

    static boolean isDateFuture(String year, String month, String day) {
        try {
            var date1 = new SimpleDateFormat("yyyyMMdd").parse(formatYear(year) + formatMonth(month) + formatDay(day));
            return date1.after(new Date());
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return true;
    }
}
