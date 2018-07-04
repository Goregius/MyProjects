<html>
<head>
    <title>Catering Result</title>
    <link rel="stylesheet" href="default.css">
</head>
<body>
<?php
//'intval' converts each GET variable to an integer.
$min = intval($_GET['min']);
$max = intval($_GET['max']);
//$c is set as an array where, for example, $c[1] would be assigned from c2.
$c = [intval($_GET['c1']), intval($_GET['c2']), intval($_GET['c3']), intval($_GET['c4']), intval($_GET['c5'])];

//Checks if the inputs entered are valid integers.
//The table only loads when all of the variables are valid integers,
//gives a link back to the html otherwise.
if (!ctype_digit($_GET['min']) || !ctype_digit($_GET['max']) || !ctype_digit($_GET['c1']) || !ctype_digit($_GET['c2'])
    || !ctype_digit($_GET['c3']) || !ctype_digit($_GET['c4']) || !ctype_digit($_GET['c5'])) {
    die("One of your inputs weren't integers, if you would like to re-enter them click <a href='catering.htm'>here</a>.");
} ?>
<table border="1" style="margin: auto" >
    <tr>
        <td>Cost Per Person →<br>↓ Party Size</td>
        <?php
        //Prints out each element in $c as a table cell.
        for ($i = 0; $i <= 4; $i++) { ?>
            <td><?php echo $c[$i] ?></td>
        <?php } ?>
    </tr>
    <?php
    //Iterates by 1 for each row that should be in the table (other than the top row).
    //'$max - $min' for the difference.
    // '/ 5' since the interval step is 5.
    for ($i = 0; $i <= ($max - $min) / 5; $i++) { ?>
        <tr>
            <td><?php
                //Outputs the party size.
                //'$min' because it's counting from '$min'.
                //'5 * $i' since the interval step is 5.
                $size = $min + 5 * $i;
                echo $size; ?></td>
            <?php
            //Outputs each product of '$item' and '$size'.
            foreach ($c as $item) { ?>
                <td><?php echo $item * $size ?></td>
            <?php } ?>
        </tr>
    <?php } ?>
</table>
</body>
</html>