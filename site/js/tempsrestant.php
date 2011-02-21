<?php
// Header
header("Cache-Control: no-store, no-cache, must-revalidate");
 
// Les includes
require("../include/include.php");

// $_GET
if (isset($_GET['heure']) AND isset($_GET['minute']) AND isset($_GET['seconde']) AND isset($_GET['mois']) AND isset($_GET['jour']) AND isset($_GET['annee'])){
    $heure = intval($_GET['heure']);
    $minute = intval($_GET['minute']);
    $seconde = intval($_GET['seconde']);
    $mois = intval($_GET['mois']);
    $jour = intval($_GET['jour']);
    $annee = intval($_GET['annee']);

    echo tempsRestant($heure, $minute, $seconde, $mois, $jour, $annee);
}
?>