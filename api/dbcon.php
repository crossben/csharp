<?php
/**
 * Ce fichier établit une connexion à la base de données en utilisant PDO (PHP Data Objects).
 * Il définit le nom de la base de données, l'hôte, le nom d'utilisateur et le mot de passe, puis tente de se connecter à la base de données.
 * Si la connexion réussit, il définit le mode d'erreur et prépare la base de données pour l'exécution des requêtes.
 * Si la connexion échoue, il affiche un message d'erreur.
 */

 // Database configuration
 $dbName = "examen";
 $dbHost = "localhost";
 $dbUser = "root";
 $dbPass = "12334567";
 
 try {
     // Create a PDO instance (connect to the database)
     $dsn = "mysql:host=$dbHost;dbname=$dbName;charset=utf8mb4";
     $db = new PDO($dsn, $dbUser, $dbPass);
 
     // Set PDO attributes
     $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
     $db->setAttribute(PDO::ATTR_EMULATE_PREPARES, false);
 
     // Connection successful message (for debugging purposes, remove in production)
    //  echo "Connected successfully" . "<br>";
 } catch (PDOException $e) {
     // Handle connection error
     http_response_code(500); // Internal Server Error
     echo json_encode(["error" => "Connection failed: " . $e->getMessage()], JSON_PRETTY_PRINT);
     exit; // Terminate script execution
 }
 ?>
 
