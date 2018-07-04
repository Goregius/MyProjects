<?php
//Sets up database connection.
require_once "connectDB.php";

//returns true if the date given is on a weekend.
function isWeekend($date) {
    //'N' is the format for day (1 for Monday, 7 for Sunday).
    return (date('N', strtotime($date)) >= 6);
}

$dateInput = $_GET["date"];

//Gets the day, month and year from '$dateInput' by using substr.
$day = substr($dateInput, 0, 2);
$month = substr($dateInput, 3, 2);
$year = substr($dateInput, 6, 4);


//The table only loads if the format for '$dateInput' is the correct date format (DD/MM/YYYY),
//gives a link back to the html otherwise.
if (!ctype_digit($year) || !ctype_digit($month) || !ctype_digit($day) || $dateInput[2] != "/" || $dateInput[5] != "/") {
    die("Wrong date format, DD/MM/YYYY is required, if you would like to re-enter it click <a href='costs.htm'>here</a>.");
}
//checks if the date is correct to the gregorian calendar,
//the previous if statement doesn't check for inputs such as "99/00/2018" or "30/02/2018".
elseif (!checkdate($month, $day, $year)) {
    die("Date entered isn't a valid date, if you would like to re-enter it click <a href='costs.htm'>here</a>.");
}

//sets '$date' to the ISO version of '$dateInput'.
$date = $year . "-" . $month . "-" . $day;

$partySize = $_GET["partySize"];

//Checks for ids that are not booked in 'venue_booking'
//and where capacity >= '$partySize'.
//'$db->quote' is used to make sure $date is a date and $partySize is an integer, in case the previous validation didn't work.
$sql = "SELECT venue.name, venue.weekend_price, venue.weekday_price
FROM venue
WHERE venue.venue_id not in (
    SELECT venue_booking.venue_id 
    FROM venue_booking 
    WHERE venue_booking.date_booked = {$db->quote($date, 'date')}
)
AND venue.capacity >= {$db->quote($partySize, 'integer')}";

$res =& $db->query($sql);

//Stops execution and outputs error message if there is an error with the query.
if (PEAR::isError($res)) {
    die($res->getMessage());
}
?>
<html>
<head>
    <title>Details results</title>
    <link rel="stylesheet" href="default.css">
</head>
<body>
<table border="1">
    <tr>
        <th><?php echo 'Hotel Name' ?></th>
        <th><?php
            //Displays the price, with weekend or weekday depending on which one '$date' is.
            if (isWeekend($date)) {
                echo 'Price (weekend)';
            }
            else {
                echo 'Price (weekday)';
            }
            ?>
        </th>
    </tr>
    <?php
    //Iterates through each row in '$res'
    //and displays the correct price (weekday or weekend price).
    while ($row = $res->fetchRow()) { ?>
        <tr>
            <td><?php echo  $row[strtolower('name')] ?></td>
            <td><?php if (isWeekend($date)) {
                echo $row['weekend_price'];
            }
            else {
                echo $row['weekday_price'];
            }
            ?>
            </td>
        </tr>
    <?php } ?>
</table>
</body>
</html>