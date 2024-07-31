<?php
// Set headers for JSON response and CORS
header("Content-Type: application/json; charset=UTF-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE");
header("Access-Control-Allow-Headers: Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With");

include_once './dbcon.php';

$contentType = $_SERVER["CONTENT_TYPE"] ?? '';

if (strpos($contentType, 'application/json') !== false) {
    $request_body = file_get_contents('php://input');
    $data = json_decode($request_body, true);
} else {
    $data = $_POST;
}

$response = [];

// Validate input data
if (isset($data) && !empty($data)) {
    if (isset($data["nom"], $data["prenom"], $data["age"])) {
        $name = htmlspecialchars($data["nom"], ENT_QUOTES, 'UTF-8');
        $last = htmlspecialchars($data["prenom"], ENT_QUOTES, 'UTF-8');
        $age = (int) $data["age"];

        try {
            $statement = $db->prepare("INSERT INTO `users` (nom, prenom, age) VALUES (:first, :last, :age)");
            $statement->bindParam(':first', $name);
            $statement->bindParam(':last', $last);
            $statement->bindParam(':age', $age);

            if ($statement->execute()) {
                $response["message"] = "Created";
                http_response_code(201); // Created
            } else {
                $response["message"] = "Failed to create record";
                http_response_code(500); // Internal Server Error
            }
        } catch (PDOException $e) {
            $response["error"] = "Connection failed: " . $e->getMessage();
            http_response_code(500); // Internal Server Error
        }
    } else {
        $response["error"] = "Invalid input data";
        http_response_code(400); // Bad Request
    }
} else {
    $response["error"] = "No data provided";
    http_response_code(400); // Bad Request
}

echo json_encode($response, JSON_PRETTY_PRINT);
?>
