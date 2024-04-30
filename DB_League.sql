USE [master]
GO
/****** Object:  Database [LeagueDinsdag]    Script Date: 30/04/2024 11:10:40 ******/
CREATE DATABASE [LeagueDinsdag]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LeagueDinsdag', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LeagueDinsdag.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LeagueDinsdag_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LeagueDinsdag_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LeagueDinsdag] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LeagueDinsdag].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LeagueDinsdag] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET ARITHABORT OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LeagueDinsdag] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LeagueDinsdag] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LeagueDinsdag] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LeagueDinsdag] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LeagueDinsdag] SET  MULTI_USER 
GO
ALTER DATABASE [LeagueDinsdag] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LeagueDinsdag] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LeagueDinsdag] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LeagueDinsdag] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LeagueDinsdag] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LeagueDinsdag] SET QUERY_STORE = OFF
GO
USE [LeagueDinsdag]
GO
/****** Object:  Table [dbo].[Speler]    Script Date: 30/04/2024 11:10:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naam] [nvarchar](150) NOT NULL,
	[Rugnummer] [int] NULL,
	[Lengte] [int] NULL,
	[Gewicht] [int] NULL,
	[TeamId] [int] NULL,
 CONSTRAINT [PK_Speler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 30/04/2024 11:10:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[Stamnummer] [int] NOT NULL,
	[Naam] [nvarchar](150) NOT NULL,
	[Bijnaam] [nvarchar](150) NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Stamnummer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfer]    Script Date: 30/04/2024 11:10:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Prijs] [int] NOT NULL,
	[SpelerId] [int] NOT NULL,
	[OudTeamId] [int] NULL,
	[NieuwTeamId] [int] NULL,
 CONSTRAINT [PK_Transfer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Speler]  WITH CHECK ADD  CONSTRAINT [FK_Speler_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Stamnummer])
GO
ALTER TABLE [dbo].[Speler] CHECK CONSTRAINT [FK_Speler_Team]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Speler] FOREIGN KEY([SpelerId])
REFERENCES [dbo].[Speler] ([Id])
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_Speler]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Team_Nieuw] FOREIGN KEY([OudTeamId])
REFERENCES [dbo].[Team] ([Stamnummer])
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_Team_Nieuw]
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Team_Oud] FOREIGN KEY([NieuwTeamId])
REFERENCES [dbo].[Team] ([Stamnummer])
GO
ALTER TABLE [dbo].[Transfer] CHECK CONSTRAINT [FK_Transfer_Team_Oud]
GO
USE [master]
GO
ALTER DATABASE [LeagueDinsdag] SET  READ_WRITE 
GO
