DROP DATABASE IF EXISTS gestionvoiture;

CREATE DATABASE IF NOT EXISTS gestionvoiture;
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
   BOITEVITESSE CHAR(32) NULL  
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
#       TABLE : COMMANDES
# -----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS COMMANDES
 (
   IDCLIENT CHAR(32) NOT NULL  ,
   NUMSERIE CHAR(32) NOT NULL  ,
   QTE INTEGER NULL
   , PRIMARY KEY (IDCLIENT,NUMSERIE) 
 ) 
 comment = "";

# -----------------------------------------------------------------------------
#       INDEX DE LA TABLE COMMANDES
# -----------------------------------------------------------------------------


CREATE  INDEX I_FK_COMMANDES_CLIENTS
     ON COMMANDES (IDCLIENT ASC);

CREATE  INDEX I_FK_COMMANDES_VOITURES
     ON COMMANDES (NUMSERIE ASC);

# -----------------------------------------------------------------------------
#       TABLE : ACHETERS
# -----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS ACHATS
 (
   IDCLIENT CHAR(32) NOT NULL  ,
   NUMSERIE CHAR(32) NOT NULL  ,
   QTE INTEGER NULL  ,
   RESTE BIGINT(15) NULL  ,
   SOMME BIGINT(15) NULL  
   , PRIMARY KEY (IDCLIENT,NUMSERIE) 
 ) 
 comment = "";

# -----------------------------------------------------------------------------
#       INDEX DE LA TABLE ACHETERS
# -----------------------------------------------------------------------------


CREATE  INDEX I_FK_ACHETERS_CLIENTS
     ON ACHETERS (IDCLIENT ASC);

CREATE  INDEX I_FK_ACHETERS_VOITURES
     ON ACHETERS (NUMSERIE ASC);


# -----------------------------------------------------------------------------
#       CREATION DES REFERENCES DE TABLE
# -----------------------------------------------------------------------------


ALTER TABLE VOITURES 
  ADD FOREIGN KEY FK_VOITURES_CATEGORIES (IDCATEGORIE)
      REFERENCES CATEGORIES (IDCATEGORIE) ;


ALTER TABLE VOITURES 
  ADD FOREIGN KEY FK_VOITURES_MARQUES (IDMARQUE)
      REFERENCES MARQUES (IDMARQUE) ;


ALTER TABLE COMMANDES 
  ADD FOREIGN KEY FK_COMMANDES_CLIENTS (IDCLIENT)
      REFERENCES CLIENTS (IDCLIENT) ;


ALTER TABLE COMMANDES 
  ADD FOREIGN KEY FK_COMMANDES_VOITURES (NUMSERIE)
      REFERENCES VOITURES (NUMSERIE) ;


ALTER TABLE ACHETERS 
  ADD FOREIGN KEY FK_ACHETERS_CLIENTS (IDCLIENT)
      REFERENCES CLIENTS (IDCLIENT) ;


ALTER TABLE ACHETERS 
  ADD FOREIGN KEY FK_ACHETERS_VOITURES (NUMSERIE)
      REFERENCES VOITURES (NUMSERIE) ;

