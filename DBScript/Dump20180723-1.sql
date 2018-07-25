CREATE DATABASE  IF NOT EXISTS `jlt_smdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `jlt_smdb`;
-- MySQL dump 10.13  Distrib 5.6.23, for Win64 (x86_64)

DROP TABLE IF EXISTS `action_vt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `action_vt` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `actionname` varchar(200) DEFAULT NULL,
  `description` varchar(200) DEFAULT NULL,
  `actionvalue` bigint(20) unsigned DEFAULT '0',
  `isactive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `action_vt`
--

LOCK TABLES `action_vt` WRITE;
/*!40000 ALTER TABLE `action_vt` DISABLE KEYS */;
INSERT INTO `action_vt` VALUES (1,'General','Action for basic application requrements',1,1),(2,'Add_Edit_User','Action for adding new or updating existing user',2,1),(3,'Delete_Archive_User','Action for deleting an user',4,1),(4,'Add_Edit_Test','Action for adding/updading test entity',8,1),(5,'Authorizing','Action for candidate authorization',16,1),(6,'Proctoring','Action for candidate proctoring',32,1);
/*!40000 ALTER TABLE `action_vt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `associate`
--

DROP TABLE IF EXISTS `associate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `associate` (
  `associateid` int(11) NOT NULL,
  `associateno` varchar(50) DEFAULT NULL,
  `name` varchar(100) DEFAULT NULL,
  `dob` date DEFAULT NULL,
  `phoneno` varchar(13) DEFAULT NULL,
  `emailid` varchar(50) DEFAULT NULL,
  `Profile` varchar(100) DEFAULT NULL,
  `ut1` varchar(50) DEFAULT NULL,
  `ut2` varchar(50) DEFAULT NULL,
  `ut3` varchar(50) DEFAULT NULL,
  `ut4` varchar(50) DEFAULT NULL,
  `ut5` varchar(50) DEFAULT NULL,
  `departmentid` int(11) DEFAULT NULL,
  `Photo` longblob,
  `isactive` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`associateid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `associate`
--

LOCK TABLES `associate` WRITE;
/*!40000 ALTER TABLE `associate` DISABLE KEYS */;
INSERT INTO `associate` VALUES (1,'100001','dummy user 1','1995-10-10','9876543456','dummy1@abc.com',NULL,NULL,NULL,NULL,NULL,NULL,1,NULL,1);
/*!40000 ALTER TABLE `associate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bldgfloor`
--

DROP TABLE IF EXISTS `bldgfloor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `bldgfloor` (
  `floorid` int(11) NOT NULL,
  `floorcode` varchar(50) DEFAULT NULL,
  `floorname` varchar(100) DEFAULT NULL,
  `buildingid` int(11) DEFAULT NULL,
  `isactive` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`floorid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bldgfloor`
--

LOCK TABLES `bldgfloor` WRITE;
/*!40000 ALTER TABLE `bldgfloor` DISABLE KEYS */;
INSERT INTO `bldgfloor` VALUES (1,NULL,'10B-LS',2,1),(2,NULL,'10B-MV',2,1);
/*!40000 ALTER TABLE `bldgfloor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `building`
--

DROP TABLE IF EXISTS `building`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `building` (
  `buildingid` int(11) NOT NULL,
  `buildingcode` varchar(50) DEFAULT NULL,
  `buildingname` varchar(100) DEFAULT NULL,
  `cityid` int(11) DEFAULT NULL,
  `isactive` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`buildingid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `building`
--

LOCK TABLES `building` WRITE;
/*!40000 ALTER TABLE `building` DISABLE KEYS */;
INSERT INTO `building` VALUES (1,'IN-MUM-SPM-A','Supreme Business Park A',1,1),(2,'IN-MUM-SPM-B','Supreme Business Park B',1,1);
/*!40000 ALTER TABLE `building` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `city`
--

DROP TABLE IF EXISTS `city`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `city` (
  `cityid` int(11) NOT NULL AUTO_INCREMENT,
  `cityname` varchar(100) NOT NULL,
  `district` varchar(100) DEFAULT NULL,
  `orgcountryid` int(11) DEFAULT NULL,
  `isactive` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`cityid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `city`
--

LOCK TABLES `city` WRITE;
/*!40000 ALTER TABLE `city` DISABLE KEYS */;
INSERT INTO `city` VALUES (1,'Mumbai','Mumbai',1,1),(2,'Pune','Pune',1,1);
/*!40000 ALTER TABLE `city` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `department` (
  `departmentid` int(11) NOT NULL AUTO_INCREMENT,
  `departmentname` varchar(100) DEFAULT NULL,
  `departmentcode` varchar(50) DEFAULT NULL,
  `orgcountryid` int(11) DEFAULT NULL,
  `isactive` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`departmentid`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `department`
--

LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
INSERT INTO `department` VALUES (1,'IT Application and Support','IT',1,1),(2,'Finance','FN',1,1),(3,'Accounting','AC',1,1),(4,'Operation','OP',1,1);
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `elmah_error`
--

DROP TABLE IF EXISTS `elmah_error`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `elmah_error` (
  `ErrorId` char(36) NOT NULL,
  `Application` varchar(60) NOT NULL,
  `Host` varchar(50) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `Source` varchar(60) NOT NULL,
  `Message` varchar(500) NOT NULL,
  `User` varchar(50) NOT NULL,
  `StatusCode` int(10) NOT NULL,
  `TimeUtc` datetime NOT NULL,
  `Sequence` int(10) NOT NULL AUTO_INCREMENT,
  `AllXml` text NOT NULL,
  PRIMARY KEY (`Sequence`),
  UNIQUE KEY `IX_ErrorId` (`ErrorId`(8)),
  KEY `IX_ELMAH_Error_App_Time_Seql` (`Application`(10),`TimeUtc`,`Sequence`),
  KEY `IX_ErrorId_App` (`ErrorId`(8),`Application`(10))
) ENGINE=MyISAM DEFAULT CHARSET=utf8 CHECKSUM=1 DELAY_KEY_WRITE=1 ROW_FORMAT=DYNAMIC;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `elmah_error`
--

LOCK TABLES `elmah_error` WRITE;
/*!40000 ALTER TABLE `elmah_error` DISABLE KEYS */;
/*!40000 ALTER TABLE `elmah_error` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orgcountry`
--

DROP TABLE IF EXISTS `orgcountry`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orgcountry` (
  `orgcountryid` int(11) NOT NULL,
  `countryname` varchar(100) DEFAULT NULL,
  `countrycode` varchar(50) DEFAULT NULL,
  `isactive` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`orgcountryid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orgcountry`
--

LOCK TABLES `orgcountry` WRITE;
/*!40000 ALTER TABLE `orgcountry` DISABLE KEYS */;
INSERT INTO `orgcountry` VALUES (1,'India','IN',1);
/*!40000 ALTER TABLE `orgcountry` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_vt`
--

DROP TABLE IF EXISTS `role_vt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `role_vt` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `rolename` varchar(200) DEFAULT NULL,
  `description` varchar(200) DEFAULT NULL,
  `rolevalue` bigint(20) unsigned DEFAULT '0',
  `isactive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_vt`
--

LOCK TABLES `role_vt` WRITE;
/*!40000 ALTER TABLE `role_vt` DISABLE KEYS */;
INSERT INTO `role_vt` VALUES (1,'General','Role for managing User',1,1),(2,'Admin','Role for System Admin',15,1),(3,'Manager','Role for Managers',17,1),(4,'Team Member','Team member',33,1);
/*!40000 ALTER TABLE `role_vt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seat`
--

DROP TABLE IF EXISTS `seat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `seat` (
  `seatid` int(11) NOT NULL,
  `seatlabel` varchar(50) DEFAULT NULL,
  `cubicalno` varchar(100) DEFAULT NULL,
  `rowno` int(11) DEFAULT NULL,
  `columnno` int(11) DEFAULT NULL,
  `teamid` int(11) DEFAULT NULL,
  `floorid` int(11) NOT NULL,
  `type` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`seatid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seat`
--

LOCK TABLES `seat` WRITE;
/*!40000 ALTER TABLE `seat` DISABLE KEYS */;
/*!40000 ALTER TABLE `seat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seatallocation`
--

DROP TABLE IF EXISTS `seatallocation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `seatallocation` (
  `allocationid` int(11) NOT NULL AUTO_INCREMENT,
  `associateid` int(11) DEFAULT NULL,
  `seatid` int(11) DEFAULT NULL,
  `startdate` date DEFAULT NULL,
  `enddate` date DEFAULT NULL,
  `days` int(11) DEFAULT NULL,
  `isactive` tinyint(4) DEFAULT '1',
  PRIMARY KEY (`allocationid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seatallocation`
--

LOCK TABLES `seatallocation` WRITE;
/*!40000 ALTER TABLE `seatallocation` DISABLE KEYS */;
/*!40000 ALTER TABLE `seatallocation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seatvacancy`
--

DROP TABLE IF EXISTS `seatvacancy`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `seatvacancy` (
  `seatvacancyid` int(11) NOT NULL AUTO_INCREMENT,
  `associateno` varchar(50) DEFAULT NULL,
  `startdate` date DEFAULT NULL,
  `enddate` date DEFAULT NULL,
  `days` int(11) DEFAULT NULL,
  `vacancytype` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`seatvacancyid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seatvacancy`
--

LOCK TABLES `seatvacancy` WRITE;
/*!40000 ALTER TABLE `seatvacancy` DISABLE KEYS */;
/*!40000 ALTER TABLE `seatvacancy` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `team`
--

DROP TABLE IF EXISTS `team`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `team` (
  `teamid` int(11) NOT NULL AUTO_INCREMENT,
  `teamname` varchar(100) DEFAULT NULL,
  `departmentid` int(11) DEFAULT NULL,
  `mgrid` int(11) DEFAULT NULL,
  PRIMARY KEY (`teamid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `team`
--

LOCK TABLES `team` WRITE;
/*!40000 ALTER TABLE `team` DISABLE KEYS */;
INSERT INTO `team` VALUES (1,'IT Australia',1,10001),(2,'IT SIngapore',1,10001);
/*!40000 ALTER TABLE `team` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_vt`
--

DROP TABLE IF EXISTS `user_vt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_vt` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `userid` varchar(200) DEFAULT NULL,
  `usertypeid` int(11) DEFAULT '1',
  `firstname` varchar(200) DEFAULT NULL,
  `middlename` varchar(200) DEFAULT NULL,
  `lastname` varchar(200) DEFAULT NULL,
  `emailid` varchar(100) DEFAULT NULL,
  `phone` varchar(30) DEFAULT NULL,
  `mobile` varchar(15) DEFAULT NULL,
  `profilepic` varchar(100) DEFAULT 'profile-placeholder.jpg',
  `country` int(11) DEFAULT NULL,
  `address` varchar(512) DEFAULT NULL,
  `city` varchar(512) DEFAULT NULL,
  `state` varchar(100) DEFAULT NULL,
  `zip` varchar(100) DEFAULT NULL,
  `isenabled` tinyint(4) DEFAULT '1',
  `code` varchar(50) DEFAULT NULL,
  `hash` varchar(100) DEFAULT NULL,
  `password` varbinary(200) DEFAULT NULL,
  `salt` varchar(100) DEFAULT 'abcdefgh',
  `addedby` bigint(20) unsigned DEFAULT NULL,
  `editedby` bigint(20) unsigned DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `islockedout` tinyint(1) DEFAULT '0',
  `lastlogindate` datetime DEFAULT NULL,
  `lastpasswordchangedate` datetime DEFAULT NULL,
  `lastfailedattemptdate` datetime DEFAULT NULL,
  `failedpasswordattemptcount` int(11) DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `usertypeid` (`usertypeid`),
  CONSTRAINT `user_vt_ibfk_1` FOREIGN KEY (`usertypeid`) REFERENCES `usertype_vt` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_vt`
--

LOCK TABLES `user_vt` WRITE;
/*!40000 ALTER TABLE `user_vt` DISABLE KEYS */;
INSERT INTO `user_vt` VALUES (1,'admin',1,'Administrator','','','dharmendra_singh@jltasia.com','7658926669','7658926669','a0.jpg',NULL,'Add1','mumbai','TS','502032',1,'','','à­sìÝÈé\nÕpOu±Û','T/wm8C+NzRiyWOhPfG8upQ==',0,0,'2018-07-16 14:33:29',0,'2018-07-22 01:46:06',NULL,NULL,0),(2,'manager1',2,'manager1','','','dharmendra_singh@jltasia.com','7658926669','7658926669','a1.jpg',NULL,'Add1','mumbai','TS','502032',1,'','','à­sìÝÈé\nÕpOu±Û','abcdefgh',0,0,'2018-07-16 14:33:29',0,NULL,NULL,NULL,0),(3,'manager2',2,'manager2','','','dharmendra_singh@jltasia.com','7658926669','7658926669','a2.jpg',NULL,'Add1','mumbai','TS','502032',1,'','','à­sìÝÈé\nÕpOu±Û','abcdefgh',0,0,'2018-07-16 14:33:29',0,NULL,NULL,NULL,0),(4,'member1',3,'member1','','','dharmendra_singh@jltasia.com','7658926669','7658926669','a3.jpg',NULL,'Add1','mumbai','TS','502032',1,'','','à­sìÝÈé\nÕpOu±Û','abcdefgh',0,0,'2018-07-16 14:33:29',0,NULL,NULL,NULL,0),(5,'member2',3,'member2','','','dharmendra_singh@jltasia.com','7658926669','7658926669','a4.jpg',NULL,'Add1','mumbai','TS','502032',1,'','','à­sìÝÈé\nÕpOu±Û','abcdefgh',0,0,'2018-07-16 14:33:29',0,NULL,NULL,NULL,0);
/*!40000 ALTER TABLE `user_vt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userroleassociation_vt`
--

DROP TABLE IF EXISTS `userroleassociation_vt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `userroleassociation_vt` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `roleid` bigint(20) unsigned DEFAULT NULL,
  `userid` bigint(20) unsigned DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `roleid` (`roleid`),
  KEY `userid` (`userid`),
  CONSTRAINT `userroleassociation_vt_ibfk_1` FOREIGN KEY (`roleid`) REFERENCES `role_vt` (`id`),
  CONSTRAINT `userroleassociation_vt_ibfk_2` FOREIGN KEY (`userid`) REFERENCES `user_vt` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userroleassociation_vt`
--

LOCK TABLES `userroleassociation_vt` WRITE;
/*!40000 ALTER TABLE `userroleassociation_vt` DISABLE KEYS */;
INSERT INTO `userroleassociation_vt` VALUES (4,3,4),(5,3,5),(8,2,1);
/*!40000 ALTER TABLE `userroleassociation_vt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usertype_vt`
--

DROP TABLE IF EXISTS `usertype_vt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usertype_vt` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `usertype` varchar(200) DEFAULT NULL,
  `description` varchar(200) DEFAULT NULL,
  `isactive` tinyint(1) DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usertype_vt`
--

LOCK TABLES `usertype_vt` WRITE;
/*!40000 ALTER TABLE `usertype_vt` DISABLE KEYS */;
INSERT INTO `usertype_vt` VALUES (1,'Admin','Admin User',1),(2,'Manager','Manager',1),(3,'Teammember','Team member',1);
/*!40000 ALTER TABLE `usertype_vt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'jlt_smdb'
--
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `elmah_geterrorsxml`(
  IN App VARCHAR(60),
  IN PageIndex INT(10),
  IN PageSize INT(10),
  OUT TotalCount INT(10)
)
    READS SQL DATA
BEGIN
    
    SELECT  count(*) INTO TotalCount
    FROM    `elmah_error`
    WHERE   `Application` = App;

    SET @index = PageIndex * PageSize;
    SET @count = PageSize;
    SET @app = App;
    PREPARE STMT FROM '
    SELECT
        `ErrorId`,
        `Application`,
        `Host`,
        `Type`,
        `Source`,
        `Message`,
        `User`,
        `StatusCode`,
        CONCAT(`TimeUtc`, '' Z'') AS `TimeUtc`
    FROM
        `elmah_error` error
    WHERE
        `Application` = ?
    ORDER BY
        `TimeUtc` DESC,
        `Sequence` DESC
    LIMIT
        ?, ?';
    EXECUTE STMT USING @app, @index, @count;

END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `elmah_geterrorxml`(
  IN Id CHAR(36),
  IN App VARCHAR(60)
)
    READS SQL DATA
BEGIN
    SELECT  `AllXml`
    FROM    `elmah_error`
    WHERE   `ErrorId` = Id AND `Application` = App;
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `elmah_logerror`(
    IN ErrorId CHAR(36), 
    IN Application varchar(60), 
    IN Host VARCHAR(30), 
    IN Type VARCHAR(100), 
    IN Source VARCHAR(60), 
    IN Message VARCHAR(500), 
    IN User VARCHAR(50), 
    IN AllXml TEXT, 
    IN StatusCode INT(10), 
    IN TimeUtc DATETIME
)
    MODIFIES SQL DATA
BEGIN
    INSERT INTO `elmah_error` (
        `ErrorId`, 
        `Application`, 
        `Host`, 
        `Type`, 
        `Source`, 
        `Message`, 
        `User`, 
        `AllXml`, 
        `StatusCode`, 
        `TimeUtc`
    ) VALUES (
        ErrorId, 
        Application, 
        Host, 
        Type, 
        Source, 
        Message, 
        User, 
        AllXml, 
        StatusCode, 
        TimeUtc
    );
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `raise_error`(errno BIGINT UNSIGNED, message VARCHAR(256))
BEGIN
	SIGNAL SQLSTATE
		'ERR0R'
	SET
		MESSAGE_TEXT = message,
		MYSQL_ERRNO = errno;
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `uasp_tokenservicehashsalt`(p_userid int, p_salt varchar(50))
BEGIN
	UPDATE user_vt SET salt=p_salt WHERE id = p_userid;
	
END ;;
DELIMITER ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `uasp_user`(
	p_id BIGINT UNSIGNED,
	p_userid varchar(200) ,
	p_usertypeid int ,
	p_firstname varchar(200) ,
	p_middlename varchar(200) ,
	p_lastname varchar(200) ,
	p_emailid varchar(100) ,
	p_phone varchar(30) ,
	p_mobile varchar(15) ,
	p_profilepic varchar(100) ,
	p_country int(11) ,
	p_address varchar(512) ,
	p_city varchar(512) ,
	p_state varchar(100) ,
	p_zip varchar(100) ,
	p_isenabled tinyint ,
	p_code varchar(50) ,
	p_hash varchar(100) ,
	p_password varchar(200) ,
	p_salt varchar(100) ,
	p_addedby BIGINT UNSIGNED,
	p_editedby BIGINT UNSIGNED,
	p_timestamp datetime ,
	p_islockedout tinyint(1) ,
    p_encrykey varchar(200),
    p_roles varchar(50)
)
BEGIN 
	if(p_encrykey is null)then
		call raise_error(30006,'Error!: encryption key can not be null');
    end if;
	IF(p_id is null or p_id <=0)THEN
		IF exists(Select id from user_vt where emailid=p_emailid or userid=p_userid) THEN
			call raise_error(30006,'Error!: Customer existes with emailid "' + p_emailid + '"');
		End If;
	    INSERT INTO `user_vt`(`userid`,`usertypeid`,`firstname`,`middlename`,`lastname`,`emailid`,`phone`,`mobile`,
				`country`,`address`,`city`,`state`,`zip`,`isenabled`,`password`,`timestamp`)
		VALUES(p_userid,p_usertypeid,p_firstname,p_middlename,p_lastname,p_emailid,p_phone,p_mobile,
				p_country,p_address,p_city,p_state,p_zip,p_isenabled,aes_encrypt(p_password,p_encrykey),utc_timestamp());
                      
		select LAST_Insert_Id() into p_id;
	else
		update `user_vt` set 
		`userid`= CASE WHEN p_userid IS NULL THEN `userid` ELSE p_userid end,
		`usertypeid`= CASE WHEN p_usertypeid IS NULL THEN `usertypeid` ELSE p_usertypeid end,
		`firstname`= CASE WHEN p_firstname IS NULL THEN `firstname` ELSE p_firstname end,
		`middlename`= CASE WHEN p_middlename IS NULL THEN `middlename` ELSE p_middlename end,
		`lastname`= CASE WHEN p_lastname IS NULL THEN `lastname` ELSE p_lastname end,
		`emailid`= CASE WHEN p_emailid IS NULL THEN `emailid` ELSE p_emailid end,
		`phone`= CASE WHEN p_phone IS NULL THEN `phone` ELSE p_phone end,
		`mobile`= CASE WHEN p_mobile IS NULL THEN `mobile` ELSE p_mobile end,
		`profilepic`= CASE WHEN p_profilepic IS NULL THEN `profilepic` ELSE p_profilepic end,
		`country`= CASE WHEN p_country IS NULL THEN `country` ELSE p_country end,
		`address`= CASE WHEN p_address IS NULL THEN `address` ELSE p_address end,
		`city`= CASE WHEN p_city IS NULL THEN `city` ELSE p_city end,
		`state`= CASE WHEN p_state IS NULL THEN `state` ELSE p_state end,
		`zip`= CASE WHEN p_zip IS NULL THEN `zip` ELSE p_zip end,
		`isenabled`= CASE WHEN p_isenabled IS NULL THEN `isenabled` ELSE p_isenabled end,
		`code`= CASE WHEN p_code IS NULL THEN `code` ELSE p_code end,
		`hash`= CASE WHEN p_hash IS NULL THEN `hash` ELSE p_hash end,
		`password`= CASE WHEN p_password IS NULL THEN `password` ELSE aes_encrypt(p_password,p_encrykey) end,
		`salt`= CASE WHEN p_salt IS NULL THEN `salt` ELSE p_salt end,
		`islockedout`= CASE WHEN p_islockedout IS NULL THEN `islockedout` ELSE p_islockedout end
		where id=p_id;
        
	end if;
    if(p_roles is not null)Then
		delete from userroleassociation_vt where userid=p_id;
		call usp_splitfn(p_roles,','); -- It create a temporary talbe: output(id int,splitdata varchar(200));        
		insert into userroleassociation_vt(roleid,userid) 
		select convert(splitdata, unsigned int),p_id from output;    
	end if;	
	select p_id;
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_splitfn`( 
p_string varchar(2000), 
p_delimiter CHAR(1)
)
BEGIN 
DECLARE  p_start int;
declare p_end INT;
declare p_id INT;
drop table if exists output; 
CREATE TEMPORARY TABLE output(id int,splitdata varchar(200)); 
SET p_id = 1;
SELECT 1, Locate(p_delimiter,p_string) into p_start,p_end;
WHILE (p_start < Length(p_string) + 1) do 
	IF (p_end = 0) then
		SET p_end = Length(p_string) + 1;
	end if;

	INSERT INTO output (id, splitdata)  
	VALUES(p_id, SUBSTRING(P_string, p_start, p_end - p_start)); 
	SET p_id = p_id + 1;
	SET p_start = p_end + 1;
	SET p_end = Locate(p_delimiter, p_string, p_start);
end while;
select * from output;
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_tokenservicehashsalt`(p_userid int)
BEGIN
	SELECT salt FROM user_vt WHERE id = p_userid;
	
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_user`(
p_id int,
p_userid varchar(200),
p_usertypeid int,
p_firstname varchar(200),
p_lastname varchar(200),
p_emailid varchar(100) ,
p_mobile varchar(15),
p_isenabled tinyint,
p_islockedout tinyint(1)
)
BEGIN 
	
	SELECT id,
		userid,
		usertypeid,
		firstname,
		ifnull(middlename,'') middlename,
		ifnull(lastname,'') lastname,
		emailid,
		ifnull(phone,'') phone,
		ifnull(mobile,'') mobile,
		profilepic,
		ifnull(country,0) country,
		ifnull(address,'') address,
		ifnull(city,'') city, 
		ifnull(state,'') state,
		ifnull(zip,'') zip,
		isenabled,
		islockedout,
		lastlogindate,
		lastpasswordchangedate,
		lastfailedattemptdate,
		ifnull(failedpasswordattemptcount,0) failedpasswordattemptcount,
    (select BIT_OR(r.rolevalue) As roles 
		from userroleassociation_vt ura
		inner join role_vt r on ura.roleid = r.id
		WHERE ura.userid = U.id) role,
	    '' `assigntests`
	FROM user_vt U
	where (U.id=p_id or p_id  is null)
	AND(userid=p_userid or p_userid  is null)
	AND(firstname =p_firstname or p_firstname is null) 
	AND(lastname =p_lastname or p_lastname is null)
	AND(emailid=p_emailid or p_emailid  is null)
	AND(mobile =p_mobile or p_mobile is null)
	AND(isenabled=p_isenabled or p_isenabled  is null)
	AND(islockedout=p_islockedout or p_islockedout  is null)
    AND isenabled != -1;
END ;;
DELIMITER ;

DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_validateusercredential`( 
in p_userid varchar(50),  
in p_password varchar(50),
in p_secretkey varchar(50)
)
BEGIN
	declare v_islockedout tinyint(1);
    declare v_lastfailedattemptdate datetime;
    declare v_failedattcount int;
    declare v_lastlogindate datetime;
    declare v_rolevalue bigint default 0;
    declare v_uservalid tinyint(1) default 0;
    declare v_schools varchar(2000) default '<option value="">No schools</option>';
    declare v_isenabled tinyint(1) default 1;
    
	-- -----------------------------------------------------------------------------------------------------------------
		select IFNULL(lastfailedattemptdate,TIMESTAMPADD(MINUTE,-59,now())), IFNULL(islockedout,0),IFNULL(failedpasswordattemptcount,0), lastlogindate, isenabled
        INTO v_lastfailedattemptdate,v_islockedout,v_failedattcount,v_lastlogindate,v_isenabled from user_vt where userid=p_userid;
		if (v_islockedout=1 and v_lastfailedattemptdate > TIMESTAMPADD(MINUTE,-30,now())) then
			call raise_error(30001,concat('Account has been locked try after 30 min(UserId:', p_userid, ')')); -- RAISERROR('-1',16,1); -- Locked try after 30 min
		end if;
        -- ------------------------------------------------------------------------------------------------------------------
        if(v_isenabled = 0) then begin
			call raise_error(30006,'Account has been disabled, contact administrator');
        end;
        end if;
		-- ------------------------------------------------------------------------------------------------------------------
		if not exists(select userid from user_vt where userid=p_userid and CONVERT(aes_decrypt(password, p_secretkey) USING utf8)COLLATE utf8_general_ci =p_password) then
			if (v_failedattcount= 4) then begin
					UPDATE user_vt set lastfailedattemptdate=now(),failedpasswordattemptcount=IFNULL(failedpasswordattemptcount,0) +1,islockedout=1 
                    where userid=p_userid;
					call raise_error(30005, concat('Account Locked(UserId/Pass:', p_userid, '/' , p_password, ')')); -- RAISERROR('-5',16,1) -- Account Locked
                    
				end;
			else 
				UPDATE user_vt set lastfailedattemptdate=now(),failedpasswordattemptcount=IFNULL(failedpasswordattemptcount,0) +1 
                where userid=p_userid;
			end if;
            
			if (v_failedAttCount>=1) then
				call raise_error(30002, concat('Suspicious attempt Show Captch(UserId/Pass:', p_userid, '/' , p_password, ')')); -- RAISERROR('-2',16,1)-- Suspicious attempt Show Captch 
			else
				call raise_error(30003, concat('Wrong credentials(UserId/Pass:', p_userid, '/' , p_password, ')')); -- RAISERROR('-3',16,1) -- Wrong credentials
			end if;
        else
            UPDATE user_vt set islockedout = 0 
                where userid=p_userid;
		end if; 
		-- -------------------------------------------------------------------------------------------------------------------
		If exists (Select u.id from user_vt u join userroleassociation_vt ura on u.id=ura.userid join role_vt r on ura.roleid=r.id
			where u.userid=p_userid and CONVERT(aes_decrypt(u.`password`, p_secretkey) USING utf8)COLLATE utf8_general_ci =p_password  
            and u.islockedout=0  and r.isactive=1) then
			BEGIN		
				Update user_vt set lastlogindate=now(),failedpasswordattemptcount=0,islockedout=0 
                where userid=p_userid and CONVERT(aes_decrypt(password, p_secretkey) USING utf8) COLLATE utf8_general_ci=p_password;

				select BIT_OR(IFNULL(R.rolevalue,0)) into v_rolevalue from user_vt U 
					Inner Join userroleassociation_vt URA ON U.id = URA.userid
					Inner join role_vt R on R.id = URA.roleid
					WHERE U.userid = p_userid;

				set v_uservalid = 1;
			END;
		else
			call raise_error(30004, concat('User does not have any role assigned(UserId/Pass:', p_userid, '/' , p_password, ')')); -- RAISERROR('-4',16,1) -- User does not have any role assigned 
		end if;

	if(v_uservalid = 1) then begin
		Select U.id as id,
			U.userid,
            U.usertypeid, 
			'' as password,
			IFNULL(U.firstname,U.userid) firstname,
            IFNULL(U.middlename,'') middlename,
			IFNULL(U.lastname,'') lastname,
			IFNULL(U.emailid,'') emailid
			,Convert(IFNULL(v_lastlogindate,now()) USING utf8) lastlogindate
			,Convert(IFNULL(lastpasswordchangedate,now()) USING utf8) lastpasswordchangedate
			,Convert(IFNULL(v_lastfailedattemptdate,now()) USING utf8) lastfailedattemptdate
			,'' authtoken
			, v_rolevalue role
			,IFNULL(U.mobile,'') mobile
			,IFNULL(U.profilepic,'') profilepic
            
		from user_vt U
		where userid=p_userid and islockedout=0;
	end;
	END IF; -- v_uservalid ends
END ;;
DELIMITER ;
-- Dump completed on 2018-07-23  0:29:04
