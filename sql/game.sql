
--
-- Base de données: `test`
--

-- --------------------------------------------------------

--
-- Structure de la table `message`
--

CREATE TABLE IF NOT EXISTS `message` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `pseudo` varchar(30) NOT NULL,
  `message` varchar(255) NOT NULL,
  `valide` set('oui','non','attente') NOT NULL DEFAULT 'attente',
  KEY `id` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `message`
--

INSERT INTO `message` (`id`, `pseudo`, `message`, `valide`) VALUES
(1, 'Hanza', 'J''attends avec impatience la version finale !', 'oui'),
(2, 'Welleow', 'Un jeu très prometteur mais reste à terminer.', 'oui');

-- --------------------------------------------------------

--
-- Structure de la table `news`
--

CREATE TABLE IF NOT EXISTS `news` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `news` mediumtext NOT NULL,
  `date` datetime NOT NULL,
  KEY `id` (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=15 ;

--
-- Contenu de la table `news`
--

INSERT INTO `news` (`id`, `news`, `date`) VALUES
(1, 'Aujourd''hui nous nous sommes penchés sur le problème des collisions avec les décors de ce fait, nous avons créer une méthode pour créer une bounding box à chaque nouvel objet 3d que nous loadions.', '2011-02-07 13:57:55'),
(2, 'Finalement en utilisant la méthode citée au dernier post, nous avons réussi à appliquer une bounding box à chaque nouvel élément 3d importer dans notre jeu.', '2011-02-11 23:05:34'),
(3, 'ba moi, arnaud, je me suis mis a la tache sur la réalisation des modèle 3D pour les ennemis et le décor ou je me suis bien énervé car les coordonnées 3Dsmax ne sont pas forcement celle de notre jeu.', '2011-01-30 00:30:31'),
(4, 'Le weekend juste avant la soutenance j''ai du faire un rush  sur le projet car la collision ne fonctionnais toujours pas. j''ai passe la nuit a l''epita pour que cela marche et le matin j''ai travailler avec Jacques pour qu''il prenne le relais avant de partir dormir', '2011-02-20 08:12:37'),
(6, 'Dernière modification du site avant la seconde soutenance avec la mise en place du design du site, du système de news et de message. Un site maintenant dynamique. Le système de news va maintenant vous permettre de suivre l''avancement et le système de message va vous permettre de donner votre avis sur notre jeu.', '2011-02-21 15:32:54'),
(7, 'Ça y est ! Après toute une journée de travail les zombies se tournent enfin vers le joueur (ils ne nous attaquent plus de dos).', '2011-04-04 00:59:59'),
(8, 'Les évitements d''obstacles sont désormais implémentés : les zombies ne deviendrons plus inerte dès qu''ils toucheront un pylône ou un mur. Des zombies un peu plus intelligent.', '2011-04-12 12:30:10'),
(9, 'Notre équipe viens de perdre Thibault pour qui l''appel du Sud a été trop fort. Nous nous retrouvons donc à 3 pour finir le jeu. Nous venons de perdre un membre face aux zombies !', '2011-03-15 17:26:20'),
(10, 'Une très grand avancée esthétique puisque nous avons intégré des textures aux modèles 3D. Un jeu radicalement plus beau !', '2011-04-22 22:25:18'),
(11, 'Le jeu a été presque totalement recodé pour pouvoir chargé plus de zombie, tirer plus de fois sans aucun lag !', '2011-03-30 17:35:00'),
(12, 'Aujourd''hui, nous avons commencé à implémenter les vidéos dans le code. C''est à ce moment là qu''on a remarqué que l''on avait commencé le jeu sous xna 3.0. Or, les vidéos ne sont importable que sur xna 3.1. On a alors dû upgrade le projet pour le passer en 3.1 et là, on a remarqué que plein de bug se sont apparu et on a dû revoir tout le code.\r\n\r\nPendant cette semaine de rush, nous avons passé pas mal de temps à coder ainsi qu''à créer les différentes cartes avec leurs modèles. C''était sympa de pouvoir modifier les décors avec des textures différentes.\r\n\r\nVoilà nous venons de passer nos partiels. Maintenant il faut qu''on rattrape le retard qu''on a eu lors des autres soutenances.', '2011-06-17 21:47:01'),
(13, 'Nous attaquons aujourd''hui le réseau qui va permettre à plusieurs personnes de jouer ensemble sur deux ordinateurs différents. Nous avons le choix d''utiliser le réseau de la bibliothèque de xna ou de charp. Pour l''instant, nous optons pour la première solution car xna est avant tout un framework pour faire des jeux. Donc, des fonctions plus proches de ce que nous avons besoin pour notre jeu.\r\n\r\nNous allons donc passer beaucoup de temps à comprendre comment cela marche en faisant des recherches. Cependant, nous avons pas trouvé énormément de documents traitant du réseau.', '2011-05-27 09:31:37'),
(14, 'Cela fait depuis deux jours que nous attaquons le menu, nous avons très bien avancé au niveau code. Mais reste encore la partie visuelle.\r\n\r\nNous avons un énorme problème puisque les deux menus (le nouveau et l''ancien) ne fonctionne pas du tout de la même manière. Nous avons dû recoder une partie.', '2011-06-13 12:24:33');