USE [master]
GO
/****** Object:  Database [VendorManagementProject]    Script Date: 19-Mar-25 9:55:06 AM ******/
CREATE DATABASE [VendorManagementProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VendorManagementProject', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\VendorManagementProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VendorManagementProject_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\VendorManagementProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [VendorManagementProject] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VendorManagementProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VendorManagementProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VendorManagementProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VendorManagementProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VendorManagementProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VendorManagementProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [VendorManagementProject] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [VendorManagementProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VendorManagementProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VendorManagementProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VendorManagementProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VendorManagementProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VendorManagementProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VendorManagementProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VendorManagementProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VendorManagementProject] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VendorManagementProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VendorManagementProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VendorManagementProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VendorManagementProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VendorManagementProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VendorManagementProject] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [VendorManagementProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VendorManagementProject] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VendorManagementProject] SET  MULTI_USER 
GO
ALTER DATABASE [VendorManagementProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VendorManagementProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VendorManagementProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VendorManagementProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VendorManagementProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VendorManagementProject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VendorManagementProject] SET QUERY_STORE = ON
GO
ALTER DATABASE [VendorManagementProject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [VendorManagementProject]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 19-Mar-25 9:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankAccounts]    Script Date: 19-Mar-25 9:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccounts](
	[BankID] [int] IDENTITY(1,1) NOT NULL,
	[IBAN] [nvarchar](max) NOT NULL,
	[BIC] [nvarchar](max) NOT NULL,
	[BankName] [nvarchar](100) NOT NULL,
	[AccountHolder] [nvarchar](50) NOT NULL,
	[VendorID] [int] NOT NULL,
 CONSTRAINT [PK_BankAccounts] PRIMARY KEY CLUSTERED 
(
	[BankID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactPersons]    Script Date: 19-Mar-25 9:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactPersons](
	[ContactPID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Mobile] [nvarchar](max) NOT NULL,
	[EMail] [nvarchar](max) NOT NULL,
	[VendorID] [int] NOT NULL,
 CONSTRAINT [PK_ContactPersons] PRIMARY KEY CLUSTERED 
(
	[ContactPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 19-Mar-25 9:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[VendorID] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [nvarchar](100) NOT NULL,
	[VendorName2] [nvarchar](max) NOT NULL,
	[Address1] [nvarchar](50) NOT NULL,
	[Address2] [nvarchar](max) NOT NULL,
	[ZIP] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[EMail] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Mobile] [nvarchar](max) NOT NULL,
	[Notes] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VendorUsers]    Script Date: 19-Mar-25 9:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorUsers](
	[UID] [int] IDENTITY(1,1) NOT NULL,
	[UserFirstName] [nvarchar](50) NOT NULL,
	[UserLastName] [nvarchar](50) NOT NULL,
	[UserID] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_VendorUsers] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_BankAccounts_VendorID]    Script Date: 19-Mar-25 9:55:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_BankAccounts_VendorID] ON [dbo].[BankAccounts]
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ContactPersons_VendorID]    Script Date: 19-Mar-25 9:55:06 AM ******/
CREATE NONCLUSTERED INDEX [IX_ContactPersons_VendorID] ON [dbo].[ContactPersons]
(
	[VendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BankAccounts]  WITH CHECK ADD  CONSTRAINT [FK_BankAccounts_Vendors_VendorID] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendors] ([VendorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BankAccounts] CHECK CONSTRAINT [FK_BankAccounts_Vendors_VendorID]
GO
ALTER TABLE [dbo].[ContactPersons]  WITH CHECK ADD  CONSTRAINT [FK_ContactPersons_Vendors_VendorID] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendors] ([VendorID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactPersons] CHECK CONSTRAINT [FK_ContactPersons_Vendors_VendorID]
GO
USE [master]
GO
ALTER DATABASE [VendorManagementProject] SET  READ_WRITE 
GO
