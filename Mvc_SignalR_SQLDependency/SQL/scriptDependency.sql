USE [master]
GO
/****** Object:  Database [SQLDependency]    Script Date: 6/11/2017 2:13:27 PM ******/
CREATE DATABASE [SQLDependency]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SQLDependency', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SQLDependency.mdf' , SIZE = 92160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SQLDependency_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\SQLDependency_log.ldf' , SIZE = 123648KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SQLDependency] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SQLDependency].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SQLDependency] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SQLDependency] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SQLDependency] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SQLDependency] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SQLDependency] SET ARITHABORT OFF 
GO
ALTER DATABASE [SQLDependency] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SQLDependency] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SQLDependency] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SQLDependency] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SQLDependency] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SQLDependency] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SQLDependency] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SQLDependency] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SQLDependency] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SQLDependency] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SQLDependency] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SQLDependency] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SQLDependency] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SQLDependency] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SQLDependency] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SQLDependency] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SQLDependency] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SQLDependency] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SQLDependency] SET RECOVERY FULL 
GO
ALTER DATABASE [SQLDependency] SET  MULTI_USER 
GO
ALTER DATABASE [SQLDependency] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SQLDependency] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SQLDependency] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SQLDependency] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SQLDependency]
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a]    Script Date: 6/11/2017 2:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a') > 0)   DROP SERVICE [SqlQueryNotificationService-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a]; if (OBJECT_ID('SqlQueryNotificationService-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-45d7dc8c-8ba8-46ed-bef1-5d6bb89d136a]; END COMMIT TRANSACTION; END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-a1a40238-a5c6-4918-ba46-3bcdcc2e730a]    Script Date: 6/11/2017 2:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-a1a40238-a5c6-4918-ba46-3bcdcc2e730a] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-a1a40238-a5c6-4918-ba46-3bcdcc2e730a]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-a1a40238-a5c6-4918-ba46-3bcdcc2e730a] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-a1a40238-a5c6-4918-ba46-3bcdcc2e730a') > 0)   DROP SERVICE [SqlQueryNotificationService-a1a40238-a5c6-4918-ba46-3bcdcc2e730a]; if (OBJECT_ID('SqlQueryNotificationService-a1a40238-a5c6-4918-ba46-3bcdcc2e730a', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-a1a40238-a5c6-4918-ba46-3bcdcc2e730a]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-a1a40238-a5c6-4918-ba46-3bcdcc2e730a]; END COMMIT TRANSACTION; END
GO
/****** Object:  Table [dbo].[NotificationList]    Script Date: 6/11/2017 2:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[UserID] [nvarchar](50) NOT NULL,
	[UserConnectionID] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_NotificationList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersChat]    Script Date: 6/11/2017 2:13:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsersChat](
	[IDConnection] [uniqueidentifier] NOT NULL,
	[loginName] [varchar](50) NULL,
 CONSTRAINT [PK_UsersChat] PRIMARY KEY CLUSTERED 
(
	[IDConnection] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [SQLDependency] SET  READ_WRITE 
GO
