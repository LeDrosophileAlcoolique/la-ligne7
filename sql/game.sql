-- phpMyAdmin SQL Dump
-- version 3.3.9
-- http://www.phpmyadmin.net
--
-- Serveur: localhost
-- Généré le : Mar 26 Avril 2011 à 14:18
-- Version du serveur: 5.5.8
-- Version de PHP: 5.3.5

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";

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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=12 ;

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
(11, 'Le jeu a été presque totalement recodé pour pouvoir chargé plus de zombie, tirer plus de fois sans aucun lag !', '2011-03-30 17:35:00');
