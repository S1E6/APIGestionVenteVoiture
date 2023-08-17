-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Aug 17, 2023 at 02:19 PM
-- Server version: 8.0.31
-- PHP Version: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `gestionvente`
--

-- --------------------------------------------------------

--
-- Table structure for table `achats`
--

DROP TABLE IF EXISTS `achats`;
CREATE TABLE IF NOT EXISTS `achats` (
  `NUMACHAT` char(10) NOT NULL,
  `IDCLIENT` char(32) NOT NULL,
  `NUMSERIE` char(32) NOT NULL,
  `QTE` int DEFAULT NULL,
  `RESTE` bigint DEFAULT NULL,
  `SOMME` bigint DEFAULT NULL,
  `DATEACHAT` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`NUMACHAT`),
  KEY `I_FK_ACHATS_CLIENTS` (`IDCLIENT`),
  KEY `I_FK_ACHATS_VOITURES` (`NUMSERIE`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `achats`
--

INSERT INTO `achats` (`NUMACHAT`, `IDCLIENT`, `NUMSERIE`, `QTE`, `RESTE`, `SOMME`, `DATEACHAT`) VALUES
('ACH010', 'CLT003', 'WWW007', 3, 0, 25000000, '2023-06-26 11:45:00'),
('ACH008', 'CLT001', 'WWW004', 2, 0, 25000000, '2023-06-26 13:15:00'),
('ACH004', 'CLT001', 'WWW004', 2, 0, 25000000, '2023-02-26 13:15:00'),
('ACH007', 'CLT003', 'WWW002', 3, 0, 180000000, '2023-04-26 11:45:00'),
('ACH016', 'CLT001', 'WWW010', 2, 0, 45000000, '2023-01-26 09:00:00'),
('ACH011', 'CLT001', 'WWW010', 2, 0, 45000000, '2023-05-26 09:00:00'),
('ACH005', 'CLT001', 'WWW001', 2, 0, 40000000, '2023-03-26 09:00:00'),
('ACH003', 'CLT003', 'WWW002', 3, 0, 180000000, '2023-02-26 11:45:00'),
('ACH009', 'CLT002', 'WWW009', 1, 0, 100000000, '2023-05-26 10:30:00'),
('ACH021', 'CLT004', 'WWW004', 1, 25000000, 25000000, '2023-08-17 17:00:33'),
('ACH017', 'CLT002', 'WWW006', 1, 0, 23000000, '2023-02-26 10:30:00'),
('ACH001', 'CLT001', 'WWW001', 2, 0, 40000000, '2023-01-26 09:00:00'),
('ACH020', 'CLT003', 'WWW010', 1, 41000000, 40000000, '2023-08-17 15:55:09'),
('ACH018', 'CLT003', 'WWW002', 3, 0, 180000000, '2023-07-26 11:45:00'),
('ACH019', 'CLT001', 'WWW004', 2, 0, 25000000, '2023-07-26 13:15:00');

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
CREATE TABLE IF NOT EXISTS `categories` (
  `IDCATEGORIE` char(32) NOT NULL,
  `DESIGNCAT` char(32) DEFAULT NULL,
  PRIMARY KEY (`IDCATEGORIE`),
  UNIQUE KEY `DESIGNCAT` (`DESIGNCAT`),
  KEY `IDCATEGORIE` (`IDCATEGORIE`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`IDCATEGORIE`, `DESIGNCAT`) VALUES
('CAT008', 'Camion'),
('CAT002', '4*4'),
('CAT003', 'SUV'),
('CAT004', 'BUS'),
('CAT011', 'légère'),
('CAT012', 'Poids lourd');

-- --------------------------------------------------------

--
-- Table structure for table `clients`
--

DROP TABLE IF EXISTS `clients`;
CREATE TABLE IF NOT EXISTS `clients` (
  `IDCLIENT` char(32) NOT NULL,
  `NOM` char(32) DEFAULT NULL,
  `PRENOMS` char(32) DEFAULT NULL,
  `ADRESSE` char(32) DEFAULT NULL,
  `MAIL` char(32) DEFAULT NULL,
  PRIMARY KEY (`IDCLIENT`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `clients`
--

INSERT INTO `clients` (`IDCLIENT`, `NOM`, `PRENOMS`, `ADRESSE`, `MAIL`) VALUES
('CLT004', 'Mihajasoa', 'Léa', 'Antsirabe', 'valsumihaja@gmail.com'),
('CLT003', 'Rakoto', 'Malala', 'Ankofafa', 'rako@gmail.com'),
('CLT001', 'RAFANOMEZANTSOA', 'Alfredo', 'Manaotsara 1', 'elyse.alfredo@gmail.com');

-- --------------------------------------------------------

--
-- Table structure for table `marques`
--

DROP TABLE IF EXISTS `marques`;
CREATE TABLE IF NOT EXISTS `marques` (
  `IDMARQUE` char(32) NOT NULL,
  `DESIGNMARQUE` char(32) DEFAULT NULL,
  PRIMARY KEY (`IDMARQUE`),
  UNIQUE KEY `DESIGNMARQUE` (`DESIGNMARQUE`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `marques`
--

INSERT INTO `marques` (`IDMARQUE`, `DESIGNMARQUE`) VALUES
('MAR001', 'Volvo'),
('MAR002', 'Kia'),
('MAR003', 'Hyundai'),
('MAR004', 'SangYang'),
('MAR005', 'VolsksWagen'),
('MAR009', 'Mercedes Benz'),
('MAR008', 'Toyota'),
('MAR010', 'Scania');

-- --------------------------------------------------------

--
-- Table structure for table `voitures`
--

DROP TABLE IF EXISTS `voitures`;
CREATE TABLE IF NOT EXISTS `voitures` (
  `NUMSERIE` char(32) NOT NULL,
  `IDCATEGORIE` char(32) NOT NULL,
  `IDMARQUE` char(32) NOT NULL,
  `DESIGNVOITURE` char(32) DEFAULT NULL,
  `PRIX` bigint DEFAULT NULL,
  `IMG` char(32) DEFAULT NULL,
  `TYPE` char(32) DEFAULT NULL,
  `BOITEVITESSE` char(32) DEFAULT NULL,
  `STATUS` int NOT NULL,
  PRIMARY KEY (`NUMSERIE`),
  KEY `I_FK_VOITURES_CATEGORIES` (`IDCATEGORIE`),
  KEY `I_FK_VOITURES_MARQUES` (`IDMARQUE`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `voitures`
--

INSERT INTO `voitures` (`NUMSERIE`, `IDCATEGORIE`, `IDMARQUE`, `DESIGNVOITURE`, `PRIX`, `IMG`, `TYPE`, `BOITEVITESSE`, `STATUS`) VALUES
('WWW011', 'CAT012', 'MAR010', 'Daf', 500000000, 'string', 'Diesel', 'Automatique', 1),
('WWW010', 'CAT004', 'MAR009', 'Sprinter 312', 45000000, 'string', 'Diesel', 'Manuelle', 1),
('WWW009', 'CAT008', 'MAR009', 'ACTROS', 100000000, 'string', 'Diesel', 'Automatique', 0),
('WWW008', 'CAT008', 'MAR001', 'FH 16', 100000000, 'string', 'Diesel', 'Automatique', 0),
('WWW006', 'CAT003', 'MAR003', 'Starex', 23000000, 'string', 'Diesel', 'Manuelle', 0),
('WWW005', 'CAT011', 'MAR002', 'Pride', 20000000, 'string', 'Essence', 'Manuelle', 0),
('WWW004', 'CAT011', 'MAR005', 'Golf', 25000000, 'string', 'Diesel', 'Manuelle', 1),
('WWW003', 'CAT011', 'MAR005', 'Golf', 40000000, 'string', 'Diesel', 'Manuelle', 0),
('WWW002', 'CAT002', 'MAR008', 'Prado', 180000000, 'string', 'Diesel', 'Automatique', 0),
('WWW001', 'CAT002', 'MAR004', 'Rexton', 40000000, 'string', 'Diesel', 'Automatique', 0);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
