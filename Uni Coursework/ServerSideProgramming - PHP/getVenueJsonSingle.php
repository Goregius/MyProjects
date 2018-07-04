<?php
//Sets up database connection.
require_once "connectDB.php";

$date = $_GET['date'];
$partySize = $_GET['partySize'];
$grade = $_GET['grade'];

//Gets data from venue, venue_booking, and catering using inner joins,
//where the venue isn't booked, and correct grades and capacities selected.
//Grouped by venue_id to remove duplicates, and results are ordered.
//$db->quote is used to make sure '$date' is a date, and $grade and $partySize as integers.
$sql="SELECT venue.name, venue.capacity, venue.weekday_price, venue.weekend_price, venue.licensed, catering.cost
FROM venue 
INNER JOIN venue_booking 
ON venue.venue_id = venue_booking.venue_id 
INNER JOIN catering 
ON venue.venue_id = catering.venue_id
WHERE venue.venue_id not in (
    SELECT venue_booking.venue_id 
    FROM venue_booking 
    WHERE venue_booking.date_booked = {$db->quote($date, 'date')}
)
AND catering.grade = {$db->quote($grade, 'integer')}
AND venue.capacity >= {$db->quote($partySize, 'integer')}
GROUP BY venue.venue_id
ORDER BY venue.name";

$res =& $db->query($sql);

//Stops execution and outputs error message if there is an error with the query.
if(PEAR::isError($res)){
    die($res->getMessage());
}
$result = $res->fetchAll();

for ($i = 0; $i < count($result); $i++) {
    if (date('N', strtotime($date)) >= 6) { //Date is on a weekend.
        $result[$i]['price'] = $result[$i]['weekend_price'];
    }
    else {
        $result[$i]['price'] = $result[$i]['weekday_price'];
    }
}

$value = json_encode($result);
echo $value;