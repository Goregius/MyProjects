<?php
require_once 'MDB2.php';

//Username and password.
include "connection.php";

//Rest of the database details.
$host = 'localhost';
$dbName = 'coa123wdb';
$dsn = "mysql://$username:$password@$host/$dbName";

//Connects to the database with the previous details.
$db =& MDB2::connect($dsn);
if(PEAR::isError($db)){
    die($db->getMessage());
}
//Sets the indexing to associative.
$db->setFetchMode(MDB2_FETCHMODE_ASSOC);