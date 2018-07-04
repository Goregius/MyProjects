<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Wedding</title>
    <link rel="stylesheet" href="weddingStyle.css">
    <style>

    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            //Used as a toggle so that there will only be an ajax request when the user clicks on the date radio inputs that are not selected,
            //so the request will only be made once after multiple clicks on a date radio input.
            var singleDate = true;

            $("#single").click(function () {
                if (!singleDate) {
                    update();
                    singleDate = true;
                }

            });
            $("#between").click(function () {
                setBetweenActive();
            });


            $('#startDateInput').change('input', function (e) {
                onSingleInputChanged();
            });

            $('#endDateInput').change('input', function (e) {
                onEndInputChanged();
            });
            $('#endInputContainer').mousedown(setBetweenActive);

            $('#partySizeInput').change('input', function (e) {
                update();
            });
            $('#gradeInput').change('input', function (e) {
                update();
            });

            //Sets between date elements set to an 'active' visibility and the table is updated.
            function setBetweenActive() {
                var e = $("#endDateContainer");
                e.css('opacity', '1');
                e.css('cursor', 'auto');
                $("#endDateInput").css('cursor', 'auto');
                $("#between").prop("checked", true);
                onEndInputChanged();
            }

            function onSingleInputChanged() {
                $("#endDateInput").val($("#startDateInput").val());
                update();
            }

            function onEndInputChanged() {
                update();
                singleDate = false;
            }



            function updateDateInputLimits() {
                var endDateInput = $('#endDateInput');
                var startDateString = $('#startDateInput').val();
                //sets the minimum date to value (date) of '#startDateInput'.
                endDateInput.attr('min', startDateString);

                var maxDate = stringToDate(startDateString);
                //sets the maximum date to a week after the minimum.
                maxDate.setDate(maxDate.getDate() + 6);
                endDateInput.attr('max', maxDate.toISOString());
            }

            //Entry point for updating the table and some styling.
            function update() {
                var startDateInput = $("#startDateInput").val();
                var startDate = stringToDate(startDateInput);
                if (checkDate(startDateInput)) {
                    //Updates the table using single or between dates
                    //depending on whether '#single' is selected.
                    if ($("#single").is(':checked')) {
                        ajaxVenueSingle(startDate);
                        var e = $("#endDateContainer");
                        e.css('opacity', '0.2');
                        e.children().css('cursor', 'context-menu');
                        var input = $("#endDateInput");
                        input.css('cursor', 'context-menu');
                        input.css('border-color', 'white');
                    }
                    else {
                        var endDateInput = $("#endDateInput").val();
                        var endDate = stringToDate(endDateInput);
                        if (checkDate(endDateInput)) {
                            ajaxVenueBetween(startDate, endDate);
                        }
                    }
                }
                updateDateInputLimits();
            }

            function ajaxVenueSingle(date) {
                //Sends an ajax request to get json data from 'getVenueJsonSingle.php' using GET variables in the url.
                $.getJSON("getVenueJsonSingle.php?date=" + date.toISOString() + "&partySize=" + $("#partySizeInput").val() + "&grade=" + $("#gradeInput").val(), function (res) {
                    setVenueTableSingle(res);
                });
            }

            function ajaxVenueBetween(startDate, endDate) {
                //Sends an ajax request to get json data from 'getVenueJsonBetween.php' using GET variables in the url.
                $.getJSON("getVenueJsonBetween.php?startDate=" + startDate.toISOString() + "&endDate=" + endDate.toISOString() + "&partySize=" + $("#partySizeInput").val() + "&grade=" + $("#gradeInput").val(), function (res) {
                    setVenueTableBetween(res);
                });
            }

            $.ajaxSetup({
                //Alerts an error message if an ajax request failed.
                "error": function () {
                    alert("Error: Couldn't receive the correct JSON data from server.");
                }
            });

            function checkDate(dateInput) {
                var sepDate = dateInput.split('-');
                var date = new Date();
                date.setFullYear(sepDate[0]);
                date.setMonth(sepDate[1] - 1);
                date.setDate(sepDate[2]);
                return sepDate.length === 3 && sepDate[0].length === 4 && date && (date.getMonth() + 1) === parseInt(sepDate[1]);
            }

            function stringToDate(string) {
                var sepString = string.split('-');
                var date = new Date();
                date.setFullYear(parseInt(sepString[0]));
                date.setMonth(parseInt(sepString[1]) - 1);
                date.setDate(parseInt(sepString[2]));
                return date;
            }

            //Converts date to ISO format (YYYY-MM-DD).
            Date.prototype.toISOString = function () {
                var mm = this.getMonth() + 1;
                var dd = this.getDate();

                return this.getFullYear() + "-" + (mm > 9 ? '' : '0') + mm + "-" + (dd > 9 ? '' : '0') + dd;
            };

            //Converts date to format DD/MM/YYYY.
            Date.prototype.toString = function () {
                var mm = this.getMonth() + 1;
                var dd = this.getDate();

                return (dd > 9 ? '' : '0') + dd + "/" + (mm > 9 ? '' : '0') + mm + "/" + this.getFullYear();
            };

            //Sets table for the single date.
            function setVenueTableSingle(json) {
                var resultsTable = $("#resultsTable");
                resultsTable.empty();
                //Sets table header.
                resultsTable.append("<tr><th style='border-top-left-radius: 25px' >Name</th><th>Capacity</th><th>Price</th><th>Catering Cost</th><th style='border-top-right-radius: 25px'>Licensed</th></tr>");
                var tr;
                //Adds rows to the table.
                for (var i = 0; i < json.length; i++) {
                    //Creates new row.
                    tr = $('<tr/>');

                    //Adds correct cells to the row.
                    tr.append("<td style='padding-left: 20px;'>" + json[i].name + "</td>");
                    tr.append("<td>" + json[i].capacity + "</td>");
                    tr.append("<td>£" + json[i].price + "</td>");
                    tr.append("<td>£" + json[i].cost + "</td>");
                    //Converts 1 to "Yes" and 0 to "No" for easier readability.
                    if (json[i].licensed === "1") {
                        tr.append("<td>Yes</td>");
                    }
                    else {
                        tr.append("<td>No</td>");
                    }

                    tr.css('margin', '50px');
                    resultsTable.append(tr);
                }
            }

            //Sets table for between dates.
            function setVenueTableBetween(json) {
                var resultsTable = $("#resultsTable");
                resultsTable.empty();
                //Sets table header.
                resultsTable.append("<tr><th style='border-top-left-radius: 25px' >Date</th><th>Name</th><th>Capacity</th><th>Price</th><th>Catering Cost</th><th style='border-top-right-radius: 25px'>Licensed</th></tr>");
                var tr;
                //Adds rows to the table.
                for (var i = 0; i < json.length; i++) {
                    tr = $('<tr/>');
                    //Converts 'json[i].date_booked' to format DD/MM/YYYY.
                    var dateBooked = stringToDate(json[i].date_booked).toString();
                    //Adds correct cells to the row.
                    tr.append("<td  style='padding-left: 20px;'>" + dateBooked + "</td>");
                    tr.append("<td>" + json[i].name + "</td>");
                    tr.append("<td>" + json[i].capacity + "</td>");
                    tr.append("<td>£" + json[i].price + "</td>");
                    tr.append("<td>£" + json[i].cost + "</td>");
                    //Converts 1 to "Yes" and 0 to "No" for easier readability.
                    if (json[i].licensed === "1") {
                        tr.append("<td>Yes</td>");
                    }
                    else {
                        tr.append("<td>No</td>");
                    }


                    resultsTable.append(tr);
                }
            }

            update();
        });
    </script>
</head>
<body>
<div id="mainContainer">
    <div id="catering">
        <div class="formSubContainer">
            <label for="single" class="dateRadioLabel">
                <input type="radio" name="dateType" id="single" value="single" checked>
                <span>Single Date</span>
            </label>
            <fieldset id="startDateContainer" class="dateContainer">
                <label for="startDateInput" id="startInputContainer" class="dateInputContainer">
                    <span>START</span>
                    <input id="startDateInput" type="date" value="<?php echo date("Y-m-d") //Sets value to current date ?>" min="2018-01-01"
                           max="2019-12-31" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}">
                </label>
            </fieldset>
            <label for="between" class="dateRadioLabel">
                <input type="radio" name="dateType" id="between" value="between">
                <span style="overflow: hidden; white-space: nowrap">Between Dates</span>
            </label>
            <fieldset id="endDateContainer" class="dateContainer">
                <label for="endDateInput" id="endInputContainer" class="dateInputContainer">
                    <span>END</span>
                    <input id="endDateInput" type="date" value="<?php echo date("Y-m-d") //Sets value to current date ?>" min="2018-01-01"
                           max="2019-12-31" required pattern="[0-9]{4}-[0-9]{2}-[0-9]{2}">
                </label>
                <div id="endDateOverlay"></div>
            </fieldset>
            <fieldset id="partySizeDiv">
                <label id="partySizeLabel" for="partySizeInput">
                    <span>Party Size</span>
                    <input id="partySizeInput" type="number" min="0" max="10000" step="50" value="0">
                </label>
            </fieldset>
            <fieldset id="gradeDiv">
                <label id="gradeLabel" for="gradeInput">
                    <span>Catering Grade</span>
                    <select id="gradeInput">
                        <option value="1">Grade 1</option>
                        <option value="2">Grade 2</option>
                        <option value="3">Grade 3</option>
                        <option value="4">Grade 4</option>
                        <option value="5" selected>Grade 5</option>
                    </select>
                </label>
            </fieldset>
        </div>
    </div>
    <div id="resultsTableContainer">
        <table id="resultsTable" border="0" align="center">
        </table>
    </div>
</div>
</body>
</html>