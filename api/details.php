<?php
include_once  './dbcon.php';

$id = $_GET['id'];
$statement = $db->prepare("SELECT * FROM `users` WHERE id = :id");
$statement->bindParam(':id', $id);
$statement->execute();
$data = $statement->fetch(PDO::FETCH_ASSOC);
echo json_encode($data);