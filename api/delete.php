<?php

include_once './dbcon.php';

// Get and sanitize the ID from the request
$id = filter_input(INPUT_POST, 'id', FILTER_VALIDATE_INT);

$response = [];

if ($id !== false && $id !== null) {
    try {
        $statement = $db->prepare("DELETE FROM `users` WHERE id = :id");
        $statement->bindParam(':id', $id, PDO::PARAM_INT);
        $data = $statement->execute();

        if ($data) {
            $response["message"] = "DELETED";
            http_response_code(200); // OK
        } else {
            $response["message"] = "Failed to delete";
            http_response_code(500); // Internal Server Error
        }
    } catch (PDOException $e) {
        $response["error"] = "Failed to delete: " . $e->getMessage();
        http_response_code(500); // Internal Server Error
    }
} else {
    $response["error"] = "Invalid ID";
    http_response_code(400); // Bad Request
}

echo json_encode($response, JSON_PRETTY_PRINT);
?>
