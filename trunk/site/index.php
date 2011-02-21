<?php
require("include/include.php");

// Pagination
$nbrMessageParPage = 3;
$nbrPage = nbrPage("news", $nbrMessageParPage);
                         
// $_GET pagination
$page = page($nbrPage, "FIRST");

echo $miseHTML['header'];
?>
<h1>Présentation de jeu</h1>
<p>Vous vous réveillez dans des catacombes, complètement amnésique. A
peine avez vous le temps de vous remettre sur pieds que déjà vous entendez
des hurlements. Face à vous arrive un guide des catacombes qui essaye
d’ouvrir la grille qui ferme la salle dans laquelle vous êtes, vous avez juste le
temps de le voir qu’il disparait aussitôt en poussant de grands hurlements.</p>
<p>Vous êtes donc seul sans aide contre des ennemis que vous ne connaissez pas
encore (vous apprendrez à les connaitre très très vite. . .)</p>

<?php
// Requête Sql
$sql = 'SELECT news, UNIX_TIMESTAMP(date) FROM news ORDER BY date DESC LIMIT '.($page-1)*$nbrMessageParPage.','.$nbrMessageParPage;
$req = mysql_query($sql);

while ($row = mysql_fetch_array($req)) {
    echo '<p class="date">'.getDateFr("l j F Y à G:i", intval($row['UNIX_TIMESTAMP(date)'])).',</p> ';
    echo '<p class="news">'.utf8_encode(nl2br($row['news'])).'</p>';
}
                
// Pagination
echo '<p>Page : ';
echo echoPage($page, $nbrPage, "index", "", "FIRST");
echo '</p>';
  
echo $miseHTML['footer'];
?>