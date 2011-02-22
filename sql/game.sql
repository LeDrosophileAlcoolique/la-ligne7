-- phpMyAdmin SQL Dump
-- version 3.2.0.1
-- http://www.phpmyadmin.net
--
-- Serveur: localhost
-- Généré le : Lun 21 Février 2011 à 15:27
-- Version du serveur: 5.1.36
-- Version de PHP: 5.3.0

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données: `game`
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
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=7 ;

--
-- Contenu de la table `news`
--

INSERT INTO `news` (`id`, `news`, `date`) VALUES
(1, 'Aujourd''hui nous nous sommes penchés sur le problème des collisions avec les décors de ce fait, nous avons créer une méthode pour créer une bounding box à chaque nouvel objet 3d que nous loadions.', '2011-02-07 13:57:55'),
(2, 'Finalement en utilisant la méthode citée au dernier post, nous avons réussi à appliquer une bounding box à chaque nouvel élément 3d importer dans notre jeu.', '2011-02-11 23:05:34'),
(3, 'ba moi, arnaud, je me suis mis a la tache sur la réalisation des modèle 3D pour les ennemis et le décor ou je me suis bien énervé car les coordonnées 3Dsmax ne sont pas forcement celle de notre jeu.', '2011-01-30 00:30:31'),
(4, 'Le weekend juste avant la soutenance j''ai du faire un rush  sur le projet car la collision ne fonctionnais toujours pas. j''ai passe la nuit a l''epita pour que cela marche et le matin j''ai travailler avec Jacques pour qu''il prenne le relais avant de partir dormir', '2011-02-20 08:12:37'),
(6, 'Dernière modification du site avant la seconde soutenance avec la mise en place du design du site, du système de news et de message. Un site maintenant dynamique. Le système de news va maintenant vous permettre de suivre l''avancement et le système de message va vous permettre de donner votre avis sur notre jeu.', '2011-02-21 15:32:54');
