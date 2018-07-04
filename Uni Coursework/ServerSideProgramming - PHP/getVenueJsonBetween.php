<?php
require_once 'connectDB.php';
$startDateString = $_GET['startDate'];
$endDateString = $_GET['endDate'];
$partySize = $_GET['partySize'];
$grade = $_GET['grade'];

$startDate = date_create($startDateString);
$endDate = date_create($endDateString);

$result = [];
$date = $startDate;
$interval = date_diff($startDate, $endDate);
$dateDiff = 1 + (int)$interval->format('%R%a days');

//Iterates through each day and queries for every day, the results are stored in '$result'.
for ($i = 0; $i < $dateDiff && $i < 7; $i++) {
    $dateString = $date->format('Y-m-d');
    //Gets data from venue, venue_booking, and catering using inner joins,
    //where the venue isn't booked, and correct grades and capacities selected.
    //Grouped by venue_id to remove duplicates.
    //'$db->quote' is used to make sure '$date' is a date, and '$grade' and '$partySize' as integers.
    $sql="SELECT venue.name, venue.capacity, venue.weekday_price, venue.weekend_price, venue.licensed, catering.cost, venue_booking.date_booked
FROM venue 
INNER JOIN venue_booking 
ON venue.venue_id = venue_booking.venue_id 
INNER JOIN catering 
ON venue.venue_id = catering.venue_id
WHERE venue.venue_id not in (
    SELECT venue_booking.venue_id 
    FROM venue_booking 
    WHERE venue_booking.date_booked = {$db->quote($dateString, 'date')}
)
AND catering.grade = {$db->quote($grade, 'integer')}
AND venue.capacity >= {$db->quote($partySize, 'integer')}
GROUP BY venue.venue_id";

    $res =& $db->query($sql);
    //Stops execution and outputs error message if there is an error with the query.
    if(PEAR::isError($res)){
        die($res->getMessage());
    }

    $sqlArray = $res->fetchAll();

    for ($j = 0; $j < count($sqlArray); $j++) {
        //Sets the date booked to the correct date.
        $sqlArray[$j]['date_booked'] = $dateString;
        //'N' is the format for day (1 for Monday, 7 for Sunday).
        if (date('N', strtotime($dateString)) >= 6) {
            $sqlArray[$j]['price'] = $sqlArray[$j]['weekend_price'];
        }
        else {
            $sqlArray[$j]['price'] = $sqlArray[$j]['weekday_price'];
        }
    }
    //Appends $sqlArray onto $result.
    $result = array_merge($result, $sqlArray);

    try {
        //Adds 1 day to $date.
        $date->add(new DateInterval('P1D'));
    } catch (Exception $e) {
        die($e->getMessage());
    }
}

//Sorts the array firstly with name then with the date booked second.
usort($result, function($a, $b) {
    if ($a['name'] == $b['name']) {
        //Returns if date of '$a' is before '$b'.
        return (int)date_diff(date_create($a['date_booked']), date_create($b['date_booked']))->format('%R%a days') < 0;
    }
    //Returns if name of '$a' is lower alphabetically than '$b'.
    return $a['name'] > $b['name'];
});

echo json_encode($result);
