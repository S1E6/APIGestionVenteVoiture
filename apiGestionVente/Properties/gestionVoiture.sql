DROP DATABASE IF EXISTS gestionvente;

CREATE DATABASE IF NOT EXISTS gestionvente;
USE gestionvente;
# -----------------------------------------------------------------------------
#       TABLE : MARQUES
# -----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS MARQUES
 (
   IDMARQUE CHAR(32) NOT NULL  ,
   DESIGNMARQUE CHAR(32) NULL  
   , PRIMARY KEY (IDMARQUE) 
 ) 
 comment = "";

# -----------------------------------------------------------------------------
#       TABLE : VOITURES
# -----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS VOITURES
 (
   NUMSERIE CHAR(32) NOT NULL  ,
   IDCATEGORIE CHAR(32) NOT NULL  ,
   IDMARQUE CHAR(32) NOT NULL  ,
   DESIGNVOITURE CHAR(32) NULL  ,
   PRIX BIGINT(15) NULL  ,
   IMG CHAR(32) NULL  ,
   TYPE CHAR(32) NULL  ,
   BOITEVITESSE CHAR(32) NULL ,
   STATUS INTEGER NOT NULL 
 , PRIMARY KEY (NUMSERIE) 
 ) 
 comment = "";

# -----------------------------------------------------------------------------
#       INDEX DE LA TABLE VOITURES
# -----------------------------------------------------------------------------


CREATE  INDEX I_FK_VOITURES_CATEGORIES
     ON VOITURES (IDCATEGORIE ASC);

CREATE  INDEX I_FK_VOITURES_MARQUES
     ON VOITURES (IDMARQUE ASC);

# -----------------------------------------------------------------------------
#       TABLE : CLIENTS
# -----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS CLIENTS
 (
   IDCLIENT CHAR(32) NOT NULL  ,
   NOM CHAR(32) NULL  ,
   PRENOMS CHAR(32) NULL  ,
   ADRESSE CHAR(32) NULL  ,
   MAIL CHAR(32) NULL  
   , PRIMARY KEY (IDCLIENT) 
 ) 
 comment = "";

# -----------------------------------------------------------------------------
#       TABLE : CATEGORIES
# -----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS CATEGORIES
 (
   IDCATEGORIE CHAR(32) NOT NULL  ,
   DESIGNCAT CHAR(32) NULL  
   , PRIMARY KEY (IDCATEGORIE) 
 ) 
 comment = "";


# -----------------------------------------------------------------------------
#       TABLE : ACHATS
# -----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS ACHATS
 (
   NUMACHAT CHAR(10) NOT NULL ,
   IDCLIENT CHAR(32) NOT NULL  ,
   NUMSERIE CHAR(32) NOT NULL  ,
   QTE INTEGER NULL  ,
   RESTE BIGINT(15) NULL  ,
   SOMME BIGINT(15) NULL  ,
   DATEACHAT DATETIME
   , PRIMARY KEY (NUMACHAT) 
 ) 
 comment = "";

# -----------------------------------------------------------------------------
#       INDEX DE LA TABLE ACHATS
# -----------------------------------------------------------------------------


CREATE  INDEX I_FK_ACHATS_CLIENTS
     ON ACHATS (IDCLIENT ASC);

CREATE  INDEX I_FK_ACHATS_VOITURES
     ON ACHATS (NUMSERIE ASC);


# -----------------------------------------------------------------------------
#       CREATION DES REFERENCES DE TABLE
# -----------------------------------------------------------------------------


ALTER TABLE VOITURES 
  ADD FOREIGN KEY FK_VOITURES_CATEGORIES (IDCATEGORIE)
      REFERENCES CATEGORIES (IDCATEGORIE) ;


ALTER TABLE VOITURES 
  ADD FOREIGN KEY FK_VOITURES_MARQUES (IDMARQUE)
      REFERENCES MARQUES (IDMARQUE) ;

ALTER TABLE ACHATS 
  ADD FOREIGN KEY FK_ACHATS_CLIENTS (IDCLIENT)
      REFERENCES CLIENTS (IDCLIENT) ;


ALTER TABLE ACHATS 
  ADD FOREIGN KEY FK_ACHATS_VOITURES (NUMSERIE)
      REFERENCES VOITURES (NUMSERIE) ;

use gestionvente ;

INSERT INTO MARQUES (IDMARQUE, DESIGNMARQUE) VALUES
                                                 ('MAR001', 'Volvo'),
                                                 ('MAR002', 'Kia');


INSERT INTO CATEGORIES (IDCATEGORIE, DESIGNCAT) VALUES
                                                    ('CAT001', 'Camion'),
                                                    ('CAT002', '4*4'),
                                                    ('CAT003', 'SUV');

INSERT INTO VOITURES (NUMSERIE, IDCATEGORIE, IDMARQUE, DESIGNVOITURE, PRIX, IMG, TYPE, BOITEVITESSE, STATUS) VALUES
                                                                                                         ('WWW001', 'CAT001', 'MAR001', 'FH 16', 300000, 'volvo_camion_1.jpg', 'Essence', 'Manuelle', 0),
                                                                                                         ('WWW002', 'CAT001', 'MAR001', 'FH 16', 300000, 'volvo_camion_2.jpg', 'Diesel', 'Automatique', 0),
                                                                                                         ('WWW003', 'CAT002', 'MAR001', 'CX 60', 40000, 'volvo_4x4_1.jpg', 'Eléctrique', 'Automatique', 0),
                                                                                                         ('WWW004', 'CAT002', 'MAR001', 'XC 60', 40000, 'volvo_4x4_2.jpg', 'Diesel', 'Automatique', 0),
                                                                                                         ('WWW005', 'CAT003', 'MAR001', 'XC 90', 35000, 'volvo_suv_1.jpg', 'Essence', 'Manuelle', 0),
                                                                                                         ('WWW006', 'CAT003', 'MAR001', 'CX 90', 35000, 'volvo_suv_2.jpg', 'Eléctrique', 'Automatique', 0),
                                                                                                         ('WWW007', 'CAT001', 'MAR002', 'K2700', 280000, 'kia_camion_1.jpg', 'Essence', 'Manuelle', 0),
                                                                                                         ('WWW008', 'CAT001', 'MAR002', 'K2700', 280000, 'kia_camion_2.jpg', 'Diesel', 'Automatique', 0),
                                                                                                         ('WWW009', 'CAT002', 'MAR002', 'SPORTAGE', 40000, 'kia_4x4_1.jpg', 'Essence', 'Manuelle', 0),
                                                                                                         ('WWW010', 'CAT002', 'MAR002', 'SPORTAGE', 40000, 'kia_4x4_2.jpg', 'Diesel', 'Automatique', 0),
                                                                                                         ('WWW011', 'CAT003', 'MAR002', 'SORENTO', 35000, 'kia_suv_1.jpg', 'Essence', 'Manuelle', 0),
                                                                                                         ('WWW012', 'CAT003', 'MAR002', 'SORENTO', 35000, 'kia_suv_2.jpg', 'Diesel', 'Automatique', 0);
INSERT INTO CLIENTS (IDCLIENT, NOM, PRENOMS, ADRESSE, MAIL) VALUES
                                                                ('CLT001', 'Dupont', 'Jean', '123 Rue de la Republique', 'jean.dupont@example.com'),
                                                                ('CLT002', 'Martin', 'Sophie', '456 Avenue des Fleurs', 'sophie.martin@example.com'),
                                                                ('CLT003', 'Lefevre', 'Pierre', '789 Chemin du Lac', 'pierre.lefevre@example.com');


INSERT INTO ACHATS (NUMACHAT, IDCLIENT, NUMSERIE, QTE, RESTE, SOMME, DATEACHAT) VALUES
                                                                                    ('ACH001', 'CLT001', 'WWW001', 2, 6000, 4000, '2023-07-26 09:00:00'),
                                                                                    ('ACH002', 'CLT002', 'WWW003', 1, 4000, 4000, '2023-07-26 10:30:00'),
                                                                                    ('ACH003', 'CLT003', 'WWW008', 3, 9300, 9300, '2023-07-26 11:45:00'),
                                                                                    ('ACH004', 'CLT001', 'WWW011', 2, 6400, 6400, '2023-07-26 13:15:00');
