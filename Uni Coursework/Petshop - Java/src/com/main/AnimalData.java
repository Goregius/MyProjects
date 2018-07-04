package com.main;

import java.util.*;

class AnimalData {
    private List<String[]> animalsList = new ArrayList<>();
    private List<String[]> searchData = new ArrayList<>();

    AnimalData() {
    }

    List<String[]> getAnimalsList() {
        return animalsList;
    }

    List<String[]> getAnimalsList(boolean deepCopy) {
        if (deepCopy) {
            //returns a deep copy of animalsList so the original animalsList can't change.
            return new ArrayList<>(animalsList);
        } else {
            return getAnimalsList();
        }
    }

    List<String[]> getSearchData() {
        return searchData;
    }

    void setSearchData(AnimalData searchData) {
        this.searchData = searchData.getSearchData();
    }


    double computeRevenueDay(String year, String month, String day) {
        double sum = 0;
        String date = year + "-" + month + "-" + day;
        for (var animal : animalsList) {
            if (animal[6] == null) continue;
            if (animal[6].length() < 7) continue;
            if (Objects.equals(date, animal[6])) {
                sum += getSum(sum, animal);
            }
        }
        return sum;
    }

    double computeRevenueMonth(String year, String month) {
        double sum = 0;
        String date = year + "-" + month;
        for (var animal : animalsList) {
            if (animal[6] == null) continue;
            if (animal[6].length() < 7) continue;
            if (Objects.equals(date, animal[6].substring(0, 7))) {
                sum = getSum(sum, animal);
            }
        }
        return sum;
    }

    private double getSum(double sum, String[] animal) {
        var arrivalDate = DateHelper.dateStringToDate(animal[5]);
        if (arrivalDate == null) return sum;
        var sellingDate = DateHelper.dateStringToDate(animal[6]);
        if (sellingDate == null) return sum;

        var startCalendar = new GregorianCalendar();
        startCalendar.setTime(arrivalDate);

        var endCalendar = new GregorianCalendar();
        endCalendar.setTime(sellingDate);

        var yearDifference = endCalendar.get(Calendar.YEAR) - startCalendar.get(Calendar.YEAR);
        var monthDifference = yearDifference * 12 + endCalendar.get(Calendar.MONTH) - startCalendar.get(Calendar.MONTH);
        var dayDifferenceOfMonth = endCalendar.get(Calendar.DAY_OF_MONTH) - startCalendar.get(Calendar.DAY_OF_MONTH);

        var reductionMultiplier = 1.0;

        if (monthDifference >= 4) {
            if (dayDifferenceOfMonth >= 0) {
                reductionMultiplier = 0.8;
            }
            else {
                reductionMultiplier = 0.9;
            }

        }
        else if (monthDifference >= 2) {
            if (dayDifferenceOfMonth >= 0) {
                reductionMultiplier = 0.9;
            }
        }

        try {
            sum += Double.parseDouble(animal[2]) * reductionMultiplier;
        } catch (Exception ignored) {
        }
        return sum;
    }
}