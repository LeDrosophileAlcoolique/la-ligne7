<?php                  
require("bdd.php");
   
function mysqlProtectAndHTML ($string, $method = "POST") {
    switch ($method) {
        case "POST":
            $method = $_POST;
        break;
        
        case "GET":
            $method = $_GET;
        break;
        
        case "SESSION":
            $method = $_SESSION;
        break;
    }    
    
    if (isset($method[$string])) {
        $string = $method[$string];
        $string = trim(mysql_real_escape_string(htmlspecialchars($string)));
    }else{
        $string = "";
    }
 
    return $string;
}
   
function tempsRestant ($heure, $minute, $seconde, $mois, $jour, $annee) {
    $timestampRestant = mktime($heure, $minute, $seconde, $mois, $jour, $annee) - time();
   
    $diffJour = floor($timestampRestant/60/60/24);
    $timestampRestant -= $diffJour*60*60*24;
   
    $diffHeure = floor($timestampRestant/60/60);
    $timestampRestant -= $diffHeure*60*60;
   
    $diffMinute = floor($timestampRestant/60);
    $timestampRestant -= $diffMinute*60;
   
    $diffSeconde = $timestampRestant;
   
    if ($diffJour >= 0 AND $diffHeure >= 0 AND $diffMinute >= 0 AND $diffSeconde >=0 ) {
        $dateJour = isPluriel($diffJour, "jour", "jours");
        $dateHeure = isPluriel($diffHeure, "heure", "heures");
        $dateMinute = isPluriel($diffMinute, "minute", "minutes");
        $dateSeconde = isPluriel($diffSeconde, "seconde", "secondes");
   
        return ($diffJour.' '.$dateJour.', '.$diffHeure.' '.$dateHeure.', '.$diffMinute.' '.$dateMinute.' et '.$diffSeconde.' '.$dateSeconde);   
    }else{
        return ("C'est parti pour la dernière soutenance");
    }
}

function isPluriel ($int, $singulier, $pluriel) {
    if ($int <= 1) {
        return $singulier;
    }else{
        return $pluriel;
    }
}

function getDateFr ($format, $timestamp){
    $anglais = array('Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday', 'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
    $francais = array('Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi', 'Dimanche', 'Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre');

    $date = date($format, $timestamp);
    $date = str_replace($anglais, $francais, $date);
    return $date;
}

function mysqlSelect ($sql, $count = "NON", $return = "OUI") {
    $req = mysql_query($sql);
   
    if ($count == "COUNT") {
   
        if ($return == "OUI") {
            $row = mysql_fetch_array($req);   
        }   
        $row['count'] = mysql_num_rows($req);
    }else{
        $row = mysql_fetch_array($req);
    }  
    
    return $row;
}

function nbrPage ($table, $nbrParPage, $condition = "", $nbr = "NON") {
    if ($condition != "") {
        $where = ' WHERE '.$condition;
    }else{
        $where = "";
    }

    // Requête Sql
    $sql = 'SELECT id FROM '.$table.$where;
    $row = mysqlSelect($sql, "COUNT", "NON");
   
    if ($nbr == "NON") {
        $nbr = $row['count'];
       
        $nbrPage = ceil($nbr / $nbrParPage);
        return $nbrPage;
    }else{
        $return['nbr'] = $row['count'];
       
        $return['nbrPage'] = ceil($return['nbr'] / $nbrParPage);
        return $return;
    }
}

function page ($nbrPage, $pointeur = "LAST") {
    if (isset($_GET['page'])) {
        $page = intval($_GET['page']);
    }else{
        $page = 0;
    }
   
    if ($page < 1 OR $page > $nbrPage) {
   
        if ($pointeur == "FIRST") {
            $page = 1;
        }else{
            $page = $nbrPage;
        }   
    }
   
    return $page;
}

function echoPage ($page, $nbrPage, $lien, $valeurForGET = "", $pointeur = "LAST") {
    if ($valeurForGET != "") {
        $signeIntero = "?";
        $signeAnd = "&";
    }else{
        $signeIntero = "";
        $signeAnd = "";
    }
   
    for ($i = 1; $i <= $nbrPage; $i++) {
        if ($i == $page){
            echo '['.$i.'] ';
        }else{
       
            if ($pointeur == "FIRST") {
                $finPage = 1;
            }else{
                $finPage = $nbrPage;
            }
       
            if ($i == $finPage) {
                echo '<a href="'.$lien.'.php'.$signeIntero.$valeurForGET.'">['.$i.']</a> ';
            }else{
                echo '<a href="'.$lien.'.php?'.$valeurForGET.$signeAnd.'page='.$i.'">['.$i.']</a> ';
            }
        }
    }
}      

function getDedicace () {
    $sql = "SELECT pseudo, message FROM message WHERE valide=\"oui\" ORDER BY RAND() LIMIT 2";
    $req = mysql_query($sql);
   
    $return = "";
   
    while ($row = mysql_fetch_array($req)) {
        $return .= '<p class="pseudo">'.utf8_encode(strip_tags($row['pseudo'])).' :</p>';
        $return .= '<p class="message">'.utf8_encode(strip_tags($row['message'])).'</p>';
    }
    
    $return .= '<a href="add-message.php">Ajouter un message</a>';
   
    return $return;
}    

$miseHTML['header'] = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="fr">
  <head>
    <title>La Ligne 7, jeu gratuit de survival horror par la team RETP</title>  
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="description" content="Survival horror gratuit dans le métro mettant en scène un homme seul et amnésique face a des hordes grandissantes de zombies. Projet Epita par la team Les Rats Envahissent Tout Paris" />
    <link rel="stylesheet" media="screen" type="text/css" title="design" href="css/design.css" />
    <script type="text/javascript" src="lib/jquery.js"></script>
    <script type="text/javascript" src="js/index.js"></script>
  </head>
  <body>
  
  <div id="container">
      <div id="header">
          <div id="pres">
	           <p>Un survival horror dans le métro mettant en scène un <b>homme seul</b> et amnésique face a des <b>hordes grandissantes de zombies</b>.</p>     
          </div>
      </div>

      <div id="menu">
          <ul>
              <li><a href="index.php">Accueil</a></li>
              <li><a href="project.php">Projet</a></li>
              <li><a href="telechargement.php">Téléchargement</a></li>
              <li><a href="team.php">L\'équipe</a></li>
              <li><a href="contact.php">Contact</a></li>
          </ul>
      </div>
      
      <div id="milieu">
          <div id="contenu">';
$miseHTML['footer'] = '</div>
          
          <div id="coldroite">
              <p class="date">Début de la soutenance finale : <br /> 
              <span class="hasCountdown">'.tempsRestant(0, 0, 0, 6, 20, 2011).'</span>
              
              '.getDedicace().'
          </div>
          
          <div class="clear">
          </div>
      </div>
      
      <div id="footer">
          <div id="gauche">
              <p>Copyright 2010 - Tous droits réservés</p>
          </div>
          
          <div id="droite">
              <p>Site optimisé pour le navigateur Mozilla Firefox.</p>
          </div>
      </div>
  </div>
  </body>
</html>';
?>