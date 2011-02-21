<?php
// Connexion base de donnée
if (!mysql_connect("localhost", "root", "")) {
    exit();
}

if (!mysql_select_db("game")) {
    exit();
}
?>