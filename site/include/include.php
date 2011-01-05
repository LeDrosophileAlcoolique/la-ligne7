<?php                     
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
              <p class="date">Début de la soutenance final : <br /> 
              <span class="hasCountdown">'.tempsRestant(0, 0, 0, 6, 20, 2011).'</span>
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