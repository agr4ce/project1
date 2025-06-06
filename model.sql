USE [master]
GO
/****** Object:  Database [pcs0118cp]    Script Date: 18.03.2023 14:06:47 ******/
CREATE DATABASE [pcs0118cp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'pcs0118cp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\pcs0118cp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'pcs0118cp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\pcs0118cp_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [pcs0118cp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pcs0118cp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pcs0118cp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pcs0118cp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pcs0118cp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pcs0118cp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pcs0118cp] SET ARITHABORT OFF 
GO
ALTER DATABASE [pcs0118cp] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [pcs0118cp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pcs0118cp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pcs0118cp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pcs0118cp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pcs0118cp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pcs0118cp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pcs0118cp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pcs0118cp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pcs0118cp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [pcs0118cp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pcs0118cp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pcs0118cp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pcs0118cp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pcs0118cp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pcs0118cp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pcs0118cp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pcs0118cp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [pcs0118cp] SET  MULTI_USER 
GO
ALTER DATABASE [pcs0118cp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pcs0118cp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pcs0118cp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pcs0118cp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [pcs0118cp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [pcs0118cp] SET QUERY_STORE = OFF
GO
USE [pcs0118cp]
GO
/****** Object:  Table [dbo].[Addition]    Script Date: 18.03.2023 14:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addition](
	[IdAddition] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [decimal](7, 2) NOT NULL,
	[MinAge] [tinyint] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Picture] [varbinary](max) NOT NULL,
	[Amount] [smallint] NOT NULL,
	[IdTableGame] [int] NOT NULL,
 CONSTRAINT [PK_Addition] PRIMARY KEY CLUSTERED 
(
	[IdAddition] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 18.03.2023 14:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[IdCategory] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Genre] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[IdCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurshasedAddition]    Script Date: 18.03.2023 14:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurshasedAddition](
	[IdAddition] [int] NOT NULL,
	[IdPurshases] [int] NOT NULL,
	[Amount] [smallint] NOT NULL,
 CONSTRAINT [PK_PurshasedAddition] PRIMARY KEY CLUSTERED 
(
	[IdAddition] ASC,
	[IdPurshases] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurshasedTableGame]    Script Date: 18.03.2023 14:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurshasedTableGame](
	[IdTableGame] [int] NOT NULL,
	[IdPurshases] [int] NOT NULL,
	[Amount] [smallint] NOT NULL,
 CONSTRAINT [PK_PurshasedTableGame] PRIMARY KEY CLUSTERED 
(
	[IdTableGame] ASC,
	[IdPurshases] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purshases]    Script Date: 18.03.2023 14:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purshases](
	[IdPurshases] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[BuyersSurname] [nvarchar](60) NOT NULL,
	[BuyersName] [nvarchar](60) NOT NULL,
	[BuersPatronymic] [nvarchar](60) NULL,
	[DeliveryAddress] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_Purshases] PRIMARY KEY CLUSTERED 
(
	[IdPurshases] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableGame]    Script Date: 18.03.2023 14:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableGame](
	[IdTableGame] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [decimal](7, 2) NOT NULL,
	[MinAge] [tinyint] NOT NULL,
	[MinCountPlayers] [tinyint] NOT NULL,
	[MaxCountPlayer] [tinyint] NULL,
	[Description] [nvarchar](1000) NULL,
	[AverageTime] [time](7) NULL,
	[Picture] [varbinary](max) NOT NULL,
	[Amount] [smallint] NOT NULL,
 CONSTRAINT [PK_TableGame] PRIMARY KEY CLUSTERED 
(
	[IdTableGame] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TableGameCategory]    Script Date: 18.03.2023 14:06:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableGameCategory](
	[IdTableGame] [int] NOT NULL,
	[idCategory] [int] NOT NULL,
 CONSTRAINT [PK_TableGameCategory] PRIMARY KEY CLUSTERED 
(
	[IdTableGame] ASC,
	[idCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_Genre]  DEFAULT ((0)) FOR [Genre]
GO
ALTER TABLE [dbo].[TableGame] ADD  CONSTRAINT [DF_TableGame_NumberOfPlayersMin]  DEFAULT ((1)) FOR [MinCountPlayers]
GO
ALTER TABLE [dbo].[Addition]  WITH CHECK ADD  CONSTRAINT [FK_Addition_TableGame] FOREIGN KEY([IdTableGame])
REFERENCES [dbo].[TableGame] ([IdTableGame])
GO
ALTER TABLE [dbo].[Addition] CHECK CONSTRAINT [FK_Addition_TableGame]
GO
ALTER TABLE [dbo].[PurshasedAddition]  WITH CHECK ADD  CONSTRAINT [FK_PurshasedAddition_Addition] FOREIGN KEY([IdAddition])
REFERENCES [dbo].[Addition] ([IdAddition])
GO
ALTER TABLE [dbo].[PurshasedAddition] CHECK CONSTRAINT [FK_PurshasedAddition_Addition]
GO
ALTER TABLE [dbo].[PurshasedAddition]  WITH CHECK ADD  CONSTRAINT [FK_PurshasedAddition_Purshases] FOREIGN KEY([IdPurshases])
REFERENCES [dbo].[Purshases] ([IdPurshases])
GO
ALTER TABLE [dbo].[PurshasedAddition] CHECK CONSTRAINT [FK_PurshasedAddition_Purshases]
GO
ALTER TABLE [dbo].[PurshasedTableGame]  WITH CHECK ADD  CONSTRAINT [FK_PurshasedTableGame_Purshases] FOREIGN KEY([IdPurshases])
REFERENCES [dbo].[Purshases] ([IdPurshases])
GO
ALTER TABLE [dbo].[PurshasedTableGame] CHECK CONSTRAINT [FK_PurshasedTableGame_Purshases]
GO
ALTER TABLE [dbo].[PurshasedTableGame]  WITH CHECK ADD  CONSTRAINT [FK_PurshasedTableGame_TableGame] FOREIGN KEY([IdTableGame])
REFERENCES [dbo].[TableGame] ([IdTableGame])
GO
ALTER TABLE [dbo].[PurshasedTableGame] CHECK CONSTRAINT [FK_PurshasedTableGame_TableGame]
GO
ALTER TABLE [dbo].[TableGameCategory]  WITH CHECK ADD  CONSTRAINT [FK_TableGameCategory_Category] FOREIGN KEY([idCategory])
REFERENCES [dbo].[Category] ([IdCategory])
GO
ALTER TABLE [dbo].[TableGameCategory] CHECK CONSTRAINT [FK_TableGameCategory_Category]
GO
ALTER TABLE [dbo].[TableGameCategory]  WITH CHECK ADD  CONSTRAINT [FK_TableGameCategory_TableGame] FOREIGN KEY([IdTableGame])
REFERENCES [dbo].[TableGame] ([IdTableGame])
GO
ALTER TABLE [dbo].[TableGameCategory] CHECK CONSTRAINT [FK_TableGameCategory_TableGame]
GO
USE [master]
GO
ALTER DATABASE [pcs0118cp] SET  READ_WRITE 
GO
