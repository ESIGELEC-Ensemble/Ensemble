-- phpMyAdmin SQL Dump
-- version 4.1.4
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jun 22, 2016 at 10:01 AM
-- Server version: 5.6.15-log
-- PHP Version: 5.5.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `ensemble`
--

-- --------------------------------------------------------

--
-- Table structure for table `activity`
--

CREATE TABLE IF NOT EXISTS `activity` (
  `id` int(255) NOT NULL AUTO_INCREMENT,
  `act_name` varchar(255) NOT NULL,
  `act_date` date NOT NULL,
  `act_startTime` varchar(100) NOT NULL,
  `act_endTime` varchar(100) DEFAULT NULL,
  `budget` int(255) NOT NULL,
  `introduction` varchar(10000) NOT NULL,
  `createdUserID` int(255) NOT NULL,
  `city` varchar(255) NOT NULL,
  `act_location` varchar(255) NOT NULL,
  `tag` varchar(100) NOT NULL,
  `picURL` varchar(200) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=24 ;

--
-- Dumping data for table `activity`
--

INSERT INTO `activity` (`id`, `act_name`, `act_date`, `act_startTime`, `act_endTime`, `budget`, `introduction`, `createdUserID`, `city`, `act_location`, `tag`, `picURL`) VALUES
(17, 'Dance party', '2016-06-30', '18:00', '20:00', 3, 'This is a big dance party! Let''s dance! Cool!!!!', 9, 'Rouen', 'Tega''s room', 'music', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\activities\\Dance_Party.jpg'),
(18, 'Minions', '2016-07-01', '15:00', '17:00', 0, 'Let''s see movie together! ', 11, 'Paris', 'Cinema', 'music', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\activities\\minions.jpg'),
(19, 'Color cun', '2016-07-08', '9:00', '11:00', 20, 'Big color run ! sports! ', 11, 'Netherlands', 'Flower park', 'music', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\activities\\run.jpg'),
(20, 'Concert', '2016-08-05', '20:00', '21:00', 20, 'Piano concert', 11, 'Paris', '30 rue Grimau, opera', 'music', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\activities\\concert.jpg'),
(21, 'Titanic', '2016-07-27', '18:00', '20:00', 2, 'Let''s see movie together!', 12, 'Rouen', 'Prince''s room', 'music', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\activities\\titanicrip.jpg'),
(22, 'Food Party', '2016-06-21', '19:00', '22:00', 20, 'Let''s eat some food and have fun!', 15, 'Rouen', 'Residence du rouvary. appt Shravan', 'other', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\activities\\food party.jpg');

-- --------------------------------------------------------

--
-- Table structure for table `city`
--

CREATE TABLE IF NOT EXISTS `city` (
  `cityId` int(11) NOT NULL AUTO_INCREMENT,
  `cityName` varchar(200) NOT NULL,
  PRIMARY KEY (`cityId`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `city`
--

INSERT INTO `city` (`cityId`, `cityName`) VALUES
(1, 'Rouen'),
(2, 'Paris'),
(3, 'Netherlands'),
(4, 'Tokyo'),
(5, 'Berlin');

-- --------------------------------------------------------

--
-- Table structure for table `comment_table`
--

CREATE TABLE IF NOT EXISTS `comment_table` (
  `userId` int(11) NOT NULL,
  `activityId` int(11) NOT NULL,
  `comment` varchar(10000) NOT NULL,
  `createdDate` date NOT NULL,
  PRIMARY KEY (`userId`,`activityId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `comment_table`
--

INSERT INTO `comment_table` (`userId`, `activityId`, `comment`, `createdDate`) VALUES
(2, 9, 'soooooooooooooooooooooooooooooooooooooooooocooooooooooooooooooooooooooooooooooooooooo!!!!!', '2016-06-16'),
(3, 4, 'asa', '2016-06-16'),
(7, 9, 'so cool!', '2016-06-17'),
(11, 22, 'Cool!!!! I like food !!', '2016-06-21'),
(11, 17, 'sooooooooooooo gooooooooooood!!!', '2016-06-21'),
(11, 18, 'so cute!!', '2016-06-21'),
(12, 17, 'olala@~!', '2016-06-21');

-- --------------------------------------------------------

--
-- Table structure for table `joined_table`
--

CREATE TABLE IF NOT EXISTS `joined_table` (
  `userId` int(11) NOT NULL,
  `actId` int(11) NOT NULL,
  PRIMARY KEY (`userId`,`actId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `joined_table`
--

INSERT INTO `joined_table` (`userId`, `actId`) VALUES
(11, 22),
(13, 18),
(13, 21),
(14, 18),
(14, 19),
(16, 18),
(16, 22),
(17, 19),
(17, 22);

-- --------------------------------------------------------

--
-- Table structure for table `liked_table`
--

CREATE TABLE IF NOT EXISTS `liked_table` (
  `userId` int(11) NOT NULL,
  `activityId` int(11) NOT NULL,
  PRIMARY KEY (`userId`,`activityId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `liked_table`
--

INSERT INTO `liked_table` (`userId`, `activityId`) VALUES
(10, 17),
(13, 18),
(13, 21),
(14, 18),
(14, 19),
(16, 18),
(16, 22),
(17, 17),
(17, 18),
(17, 19),
(17, 20),
(17, 21),
(17, 22);

-- --------------------------------------------------------

--
-- Table structure for table `relationship`
--

CREATE TABLE IF NOT EXISTS `relationship` (
  `userId` int(11) NOT NULL,
  `friendId` int(11) NOT NULL,
  PRIMARY KEY (`userId`,`friendId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `relationship`
--

INSERT INTO `relationship` (`userId`, `friendId`) VALUES
(9, 11),
(11, 9),
(11, 10),
(11, 14),
(11, 15),
(11, 16),
(13, 11);

-- --------------------------------------------------------

--
-- Table structure for table `tag`
--

CREATE TABLE IF NOT EXISTS `tag` (
  `tagId` int(11) NOT NULL AUTO_INCREMENT,
  `tagName` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`tagId`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `tag`
--

INSERT INTO `tag` (`tagId`, `tagName`) VALUES
(1, 'music'),
(2, 'picnic'),
(3, 'movie'),
(4, 'sport'),
(5, 'other');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `photo` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=19 ;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `email`, `password`, `name`, `photo`) VALUES
(13, 'weibo@esigelec.com', '3244185981728979115075721453575112', 'Weibo', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\Weibo.png'),
(12, 'prince@esigelec.com', '3244185981728979115075721453575112', 'Prince', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\Prince.png'),
(11, 'lilian@esigelec.com', '37122481812811963631412001801901341221542', 'lilian', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\profile.jpg'),
(10, 'aaron@esigelec.com', '3244185981728979115075721453575112', 'Aaron', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\Aaron.png'),
(9, 'tega@esigelec.com', '3244185981728979115075721453575112', 'Tega', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\Tega.png'),
(14, 'xingyu@esigelec.com', '3244185981728979115075721453575112', 'Xingyu', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\XingYu.png'),
(15, 'shravan@esigelec.com', '3244185981728979115075721453575112', 'Shravan', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\Shravan.png'),
(16, 'Jyoti@esigelec.com', '3244185981728979115075721453575112', 'Jyoti', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\Jyoti.png'),
(17, 'Vishal@esigelec.com', '3244185981728979115075721453575112', 'Vishal', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\users\\Vishal.png'),
(18, 'test', '3244185981728979115075721453575112', 'test', 'X:\\C#PROJECT\\Ensemble\\Ensemble\\Images\\profile.jpg');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
