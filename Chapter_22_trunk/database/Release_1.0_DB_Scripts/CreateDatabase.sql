USE [master]
GO

/****** Object:  Database [EmployeeTraining]    Script Date: 09/23/2012 10:34:43 ******/
CREATE DATABASE [EmployeeTraining] ON  PRIMARY 
( NAME = N'EmployeeTraining', FILENAME = N'C:\database\mssql\EmployeeTraining\EmployeeTraining.mdf' , SIZE = 5000KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmployeeTraining_log', FILENAME = N'C:\database\mssql\EmployeeTraining\EmployeeTraining_log.ldf' , SIZE = 5000KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [EmployeeTraining] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeTraining].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [EmployeeTraining] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [EmployeeTraining] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [EmployeeTraining] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [EmployeeTraining] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [EmployeeTraining] SET ARITHABORT OFF 
GO

ALTER DATABASE [EmployeeTraining] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [EmployeeTraining] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [EmployeeTraining] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [EmployeeTraining] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [EmployeeTraining] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [EmployeeTraining] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [EmployeeTraining] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [EmployeeTraining] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [EmployeeTraining] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [EmployeeTraining] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [EmployeeTraining] SET  DISABLE_BROKER 
GO

ALTER DATABASE [EmployeeTraining] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [EmployeeTraining] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [EmployeeTraining] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [EmployeeTraining] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [EmployeeTraining] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [EmployeeTraining] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [EmployeeTraining] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [EmployeeTraining] SET  READ_WRITE 
GO

ALTER DATABASE [EmployeeTraining] SET RECOVERY FULL 
GO

ALTER DATABASE [EmployeeTraining] SET  MULTI_USER 
GO

ALTER DATABASE [EmployeeTraining] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [EmployeeTraining] SET DB_CHAINING OFF 
GO
