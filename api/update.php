<?php
include_once  './dbcon.php';
header("Content-Type: application/json; charset=UTF-8");
//header("Content-Type: text/html; charset=UTF-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

// Vérifier le type de contenu de la requête
$contentType = $_SERVER["CONTENT_TYPE"] ?? '';
echo "Content-Type: $contentType <br>";

// Lire les données en fonction du type de contenu
//if (strpos($contentType, 'application/json') !== false) {
//    $request_body = file_get_contents('php://input');
//    $data = json_decode($request_body, true);
//} else {
//    $data = $_POST;
//}

$data = $_POST;
echo json_encode($data);
echo $data['id'];

// doing update here
if(isset($data) && !empty($data)) {
    $name = $data["nom"];
    $last = $data["prenom"];
    $age = (int) $data["age"];
//    $id = (int) $_GET["id"];
    $id = $data["id"];
    try {
        $statement = $db->prepare("UPDATE `users` SET nom = :first, prenom = :last, age = :age WHERE id = :id");
        $statement->bindParam(':first', $name);
        $statement->bindParam(':last', $last);
        $statement->bindParam(':age', $age);
        $statement->bindParam(':id', $id);
        $ok = $statement->execute();
        echo json_encode("Updated");
    } catch (PDOException $e) {
        echo "Connection failed: " . $e->getMessage() . "<br>";
    }
}else {
    echo json_encode("400, ERROR PASSING THE DATA", JSON_PRETTY_PRINT);
}
