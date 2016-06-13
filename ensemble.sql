-- phpMyAdmin SQL Dump
-- version 4.1.4
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jun 10, 2016 at 06:03 PM
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
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Dumping data for table `activity`
--

INSERT INTO `activity` (`id`, `act_name`, `act_date`, `act_startTime`, `act_endTime`, `budget`, `introduction`, `createdUserID`, `city`, `act_location`, `tag`, `picURL`) VALUES
(4, 'asd', '2016-06-03', 'asd', 'asd', 123, 'asd', 123, 'asd', 'asd', 'asd', 'asd');

-- --------------------------------------------------------

--
-- Table structure for table `city`
--

CREATE TABLE IF NOT EXISTS `city` (
  `cityId` int(11) NOT NULL AUTO_INCREMENT,
  `cityName` varchar(200) NOT NULL,
  PRIMARY KEY (`cityId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
(5, 4, 'awesome!', '2016-06-03');

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
(5, 4);

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
(5, 4);

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
(5, 6);

-- --------------------------------------------------------

--
-- Table structure for table `tag`
--

CREATE TABLE IF NOT EXISTS `tag` (
  `activityId` int(11) NOT NULL,
  `tagId` int(11) NOT NULL,
  PRIMARY KEY (`activityId`,`tagId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(255) NOT NULL AUTO_INCREMENT,
  `email` varchar(255) NOT NULL,
  `name` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `photo` varchar(255) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1 AUTO_INCREMENT=13 ;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `email`, `name`, `password`, `photo`) VALUES
(12, 'frank', 'frank', '3244185981728979115075721453575112', ''),
(11, 'frank@123.com', 'frank', '3244185981728979115075721453575112', ''),
(6, 'lilian@123.com', 'lilian', '', ''),
(9, 'ASD', 'ASD', '1782391561231623591335495145523220418474', ''),
(10, '123', '123', '3244185981728979115075721453575112', '');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
