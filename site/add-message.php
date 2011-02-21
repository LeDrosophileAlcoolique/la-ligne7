<?php
require("include/include.php");

if (isset($_POST) AND isset($_POST['ajouter'])) {
    $pseudo = mysqlProtectAndHTML("pseudo");
    $message = mysqlProtectAndHTML("message");
    
    $sql = 'INSERT INTO message SET pseudo="'.$pseudo.'", message="'.$message.'"';
    mysql_query($sql);
}

echo $miseHTML['header'];
?>
<h1>Ajouter un message</h1>
<p>Vous voulez donner votre avis sur notre jeu ? Cette page est faîtes pour vous.</p>

<form action="add-message.php" method="post">
    <ul class="form">
        <li><label for="pseudo">Pseudo :</label><input type="text" class="fieldInput" name="pseudo" id="pseudo"></li>
        <li><label for="message">Message :</label><textarea class="fieldInput" name="message" id="message"></textarea></li>
    </ul>
    
    <div class="center"><p><input class="submit" type="submit" name="ajouter" value="Ajouter le message" /></p></div>
</form>
<?php
echo $miseHTML['footer'];
?>