<?php

$servername = "localhost";
$username = "root";
$password = "";
$database = "etkinlik";

$conn = new mysqli($servername, $username, $password, $database);

if ($conn->connect_error) {
    die("Ba�lant� hatas�: " . $conn->connect_error);
}


?>