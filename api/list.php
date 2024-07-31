<?php
//global $db;

// header("Content-Type: application/json; charset=UTF-8");
header("Content-Type: text/html; charset=UTF-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

include_once './dbcon.php';

$table = "users";

try {
    $statement = $db->prepare("SELECT * FROM $table");
    $statement->execute();

    $data = $statement->fetchAll(PDO::FETCH_ASSOC);
    $result = [];
    foreach ($data as $row) {
        $result[] = $row;
    }
    echo json_encode($result);
}
catch (PDOException $e) {
    echo "Connection failed: " . $e->getMessage() . "<br>";
}
