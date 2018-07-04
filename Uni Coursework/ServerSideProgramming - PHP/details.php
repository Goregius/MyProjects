<?php
//Sets up database connection.
require_once "connectDB.php";

$venueId = $_GET['venueId'];

//Checks if the input entered is a valid integer.
//The table only loads when all of the variables are valid integers,
//gives a link back to the html otherwise.
if (!ctype_digit($venueId)) {
    die("venueId not valid, it needs to be an integer value, if you would like to re-enter it click <a href='details.htm'>here</a>.");
}

//$db->quote is used to make sure '$venueId' is an integer, this is already made sure of from ctype_digit,
//but more protection can't hurt.
$sql = "SELECT * FROM venue WHERE venue_id = {$db->quote($venueId, 'integer')}";

//Gets the first row from the query result.
//The result from the sql query should only have one row.
$row = $db->queryRow($sql);

//Stops execution and outputs error message if there is an error with the query.
if (PEAR::isError($row)) {
    die($row->getMessage());
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
        <td>venue_id</td>
        <td>name</td>
        <td>capacity</td>
        <td>weekend_price</td>
        <td>weekday_price</td>
        <td>licensed</td>
    </tr>
    <tr>
        <td><?php echo $row['venue_id'] ?></td>
        <td><?php echo $row['name'] ?></td>
        <td><?php echo $row['capacity'] ?></td>
        <td><?php echo $row['weekend_price'] ?></td>
        <td><?php echo $row['weekday_price'] ?></td>
        <td><?php echo $row['licensed'] ?></td>
    </tr>
</table>
</body>
</html>