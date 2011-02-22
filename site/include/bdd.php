<?php
// Connexion base de donnée
if (!mysql_connect("localhost", "root", "")) {
    exit();
}

// ligne7 -- li2b5cho

if (!mysql_select_db("game")) {
    exit();
}
?>