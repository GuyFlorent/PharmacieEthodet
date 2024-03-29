USE [master]
GO
/****** Object:  Database [PharmacieEthodet]    Script Date: 21/10/2019 06:48:58 ******/
CREATE DATABASE [PharmacieEthodet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PharmacieEthodet', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\PharmacieEthodet.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PharmacieEthodet_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS01\MSSQL\DATA\PharmacieEthodet_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PharmacieEthodet] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PharmacieEthodet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PharmacieEthodet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET ARITHABORT OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PharmacieEthodet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PharmacieEthodet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PharmacieEthodet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PharmacieEthodet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PharmacieEthodet] SET  MULTI_USER 
GO
ALTER DATABASE [PharmacieEthodet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PharmacieEthodet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PharmacieEthodet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PharmacieEthodet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PharmacieEthodet] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PharmacieEthodet] SET QUERY_STORE = OFF
GO
USE [PharmacieEthodet]
GO
/****** Object:  Table [dbo].[Achat]    Script Date: 21/10/2019 06:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achat](
	[id_achat] [int] IDENTITY(1,1) NOT NULL,
	[id_commande] [int] NOT NULL,
	[id_produit] [int] NULL,
	[quantité] [int] NULL,
	[prix_total] [money] NULL,
 CONSTRAINT [PK_Achat] PRIMARY KEY CLUSTERED 
(
	[id_achat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 21/10/2019 06:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[id_client] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](50) NULL,
	[prenom] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[password] [varchar](50) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[id_client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Commande]    Script Date: 21/10/2019 06:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commande](
	[id_commande] [int] IDENTITY(1,1) NOT NULL,
	[id_client] [int] NULL,
	[heure_commande] [varchar](50) NULL,
	[statut_commande] [varchar](50) NOT NULL,
	[statut_livraison] [varchar](50) NULL,
 CONSTRAINT [PK_Commande] PRIMARY KEY CLUSTERED 
(
	[id_commande] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produit]    Script Date: 21/10/2019 06:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produit](
	[id_produit] [int] IDENTITY(1,1) NOT NULL,
	[id_stock] [int] NULL,
	[nom_produit] [varchar](50) NULL,
	[prix_unite] [money] NULL,
 CONSTRAINT [PK_Produit] PRIMARY KEY CLUSTERED 
(
	[id_produit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock]    Script Date: 21/10/2019 06:48:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[id_stock] [int] IDENTITY(1,1) NOT NULL,
	[quantite_produit] [int] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[id_stock] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Achat]  WITH CHECK ADD  CONSTRAINT [FK_Achat_Commande] FOREIGN KEY([id_commande])
REFERENCES [dbo].[Commande] ([id_commande])
GO
ALTER TABLE [dbo].[Achat] CHECK CONSTRAINT [FK_Achat_Commande]
GO
ALTER TABLE [dbo].[Achat]  WITH CHECK ADD  CONSTRAINT [FK_Achat_Produit] FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Achat] CHECK CONSTRAINT [FK_Achat_Produit]
GO
ALTER TABLE [dbo].[Commande]  WITH CHECK ADD  CONSTRAINT [FK_Commande_Clients] FOREIGN KEY([id_client])
REFERENCES [dbo].[Clients] ([id_client])
GO
ALTER TABLE [dbo].[Commande] CHECK CONSTRAINT [FK_Commande_Clients]
GO
ALTER TABLE [dbo].[Produit]  WITH CHECK ADD  CONSTRAINT [FK_Produit_Stock] FOREIGN KEY([id_stock])
REFERENCES [dbo].[Stock] ([id_stock])
GO
ALTER TABLE [dbo].[Produit] CHECK CONSTRAINT [FK_Produit_Stock]
GO
USE [master]
GO
ALTER DATABASE [PharmacieEthodet] SET  READ_WRITE 
GO
