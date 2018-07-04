<?php
//Sets up database connection.
require_once "connectDB.php";

//Minimum capacity.
$min = $_GET["minCapacity"];
//Maximum capacity.
$max = $_GET["maxCapacity"];

//Checks if the inputs entered are valid integers.
//The table only loads when all of the variables are valid integers,
//gives a link back to the html otherwise.
if (!ctype_digit($min) || !ctype_digit($max)) {
    die("The minimum and maximum capacity values aren't valid integers, if you would like to re-enter them click <a href='details.htm'>here</a>.");
}

//$db->quote is used to make sure $min and $max are integers, in case ctype_digit didn't work.
$sql = "SELECT * FROM venue WHERE (capacity BETWEEN {$db->quote($min, 'integer')} AND {$db->quote($max, 'integer')}) AND licensed = '1'";
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
        <td><?php echo 'name' ?></td>
        <td><?php echo 'weekend_price' ?></td>
        <td><?php echo 'weekday_price' ?></td>
    </tr>
    <?php
    //Iterates through each row from $res and outputs them into the table.
    while ($row = $res->fetchRow()) { ?>
        <tr>
            <td><?php echo $row['name'] ?></td>
            <td><?php echo $row['weekend_price'] ?></td>
            <td><?php echo $row['weekday_price'] ?></td>
        </tr>
    <?php } ?>
</table>
</body>
</html>