USE [master]
GO
/****** Object:  Database [Shop_Online_Alpha]    Script Date: 12.04.2024 09:40:45:PM ******/
CREATE DATABASE [Shop_Online_Alpha]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Shop_Online_Alpha', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER02\MSSQL\DATA\Shop_Online_Alpha.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Shop_Online_Alpha_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER02\MSSQL\DATAe\Shop_Online_Alpha_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Shop_Online_Alpha] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Shop_Online_Alpha].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Shop_Online_Alpha] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET ARITHABORT OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Shop_Online_Alpha] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Shop_Online_Alpha] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Shop_Online_Alpha] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Shop_Online_Alpha] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET RECOVERY FULL 
GO
ALTER DATABASE [Shop_Online_Alpha] SET  MULTI_USER 
GO
ALTER DATABASE [Shop_Online_Alpha] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Shop_Online_Alpha] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Shop_Online_Alpha] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Shop_Online_Alpha] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Shop_Online_Alpha] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Shop_Online_Alpha] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Shop_Online_Alpha', N'ON'
GO
ALTER DATABASE [Shop_Online_Alpha] SET QUERY_STORE = ON
GO
ALTER DATABASE [Shop_Online_Alpha] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Shop_Online_Alpha]
GO
/****** Object:  Table [dbo].[Admini]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admini](
	[id_admin] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[rol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Admini] PRIMARY KEY CLUSTERED 
(
	[id_admin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adresa]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adresa](
	[id_adresa] [int] IDENTITY(1,1) NOT NULL,
	[judet] [varchar](50) NOT NULL,
	[oras] [varchar](50) NOT NULL,
	[strada] [varchar](50) NOT NULL,
	[numar] [int] NOT NULL,
 CONSTRAINT [PK_Adresa] PRIMARY KEY CLUSTERED 
(
	[id_adresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorii]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorii](
	[id_categorie] [int] IDENTITY(1,1) NOT NULL,
	[nume] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categorii] PRIMARY KEY CLUSTERED 
(
	[id_categorie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cos]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cos](
	[id_cos] [int] IDENTITY(1,1) NOT NULL,
	[nr_bucati] [int] NULL,
	[total_pret] [money] NULL,
	[id_user] [int] NOT NULL,
	[id_produs] [int] NOT NULL,
 CONSTRAINT [PK_Cos] PRIMARY KEY CLUSTERED 
(
	[id_cos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Furnizori]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Furnizori](
	[id_furnizor] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NOT NULL,
	[iban] [varchar](50) NOT NULL,
	[nume_firma] [varchar](200) NOT NULL,
	[id_adresa] [int] NOT NULL,
 CONSTRAINT [PK_Furnizori] PRIMARY KEY CLUSTERED 
(
	[id_furnizor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagini]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagini](
	[id_imagine] [int] IDENTITY(1,1) NOT NULL,
	[id_produs] [int] NOT NULL,
	[imagine] [image] NOT NULL,
	[nume] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Imagini] PRIMARY KEY CLUSTERED 
(
	[id_imagine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produse]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produse](
	[id_produs] [int] IDENTITY(1,1) NOT NULL,
	[id_categorie] [int] NOT NULL,
	[id_furnizor] [int] NOT NULL,
	[nume] [varchar](50) NOT NULL,
	[cantitate] [int] NULL,
	[pret] [money] NOT NULL,
	[descriere] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Produse] PRIMARY KEY CLUSTERED 
(
	[id_produs] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produse_Favorite]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produse_Favorite](
	[id_favorite] [int] IDENTITY(1,1) NOT NULL,
	[id_produs] [int] NOT NULL,
	[id_user] [int] NOT NULL,
 CONSTRAINT [PK_Produse_Favorite] PRIMARY KEY CLUSTERED 
(
	[id_favorite] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[id_review] [int] IDENTITY(1,1) NOT NULL,
	[id_produs] [int] NOT NULL,
	[id_user] [int] NOT NULL,
	[comentariu] [varchar](250) NOT NULL,
	[nr_stele] [int] NOT NULL,
	[data] [date] NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[id_review] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specificatii]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specificatii](
	[id_specificatii] [int] IDENTITY(1,1) NOT NULL,
	[id_produs] [int] NOT NULL,
	[nume] [varchar](50) NOT NULL,
	[valoare] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Specificatii] PRIMARY KEY CLUSTERED 
(
	[id_specificatii] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tranzactii]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tranzactii](
	[id_tranzactie] [int] IDENTITY(1,1) NOT NULL,
	[id_produs] [int] NOT NULL,
	[id_user] [int] NOT NULL,
	[id_furnizor] [int] NOT NULL,
	[data] [date] NOT NULL,
	[metoda_plata] [varchar](50) NOT NULL,
	[suma] [money] NOT NULL,
	[status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tranzactii] PRIMARY KEY CLUSTERED 
(
	[id_tranzactie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Useri]    Script Date: 12.04.2024 09:40:46:PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Useri](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[parola] [varchar](20) NOT NULL,
	[telefon] [varchar](15) NOT NULL,
	[email] [varchar](50) NULL,
	[id_adresa] [int] NOT NULL,
 CONSTRAINT [PK_Useri] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admini]  WITH CHECK ADD  CONSTRAINT [FK_Admini_Useri] FOREIGN KEY([id_user])
REFERENCES [dbo].[Useri] ([id_user])
GO
ALTER TABLE [dbo].[Admini] CHECK CONSTRAINT [FK_Admini_Useri]
GO
ALTER TABLE [dbo].[Cos]  WITH CHECK ADD  CONSTRAINT [FK_Cos_Produse] FOREIGN KEY([id_produs])
REFERENCES [dbo].[Produse] ([id_produs])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cos] CHECK CONSTRAINT [FK_Cos_Produse]
GO
ALTER TABLE [dbo].[Cos]  WITH CHECK ADD  CONSTRAINT [FK_Cos_Useri] FOREIGN KEY([id_user])
REFERENCES [dbo].[Useri] ([id_user])
GO
ALTER TABLE [dbo].[Cos] CHECK CONSTRAINT [FK_Cos_Useri]
GO
ALTER TABLE [dbo].[Furnizori]  WITH CHECK ADD  CONSTRAINT [FK_Furnizori_Adrese] FOREIGN KEY([id_adresa])
REFERENCES [dbo].[Adresa] ([id_adresa])
GO
ALTER TABLE [dbo].[Furnizori] CHECK CONSTRAINT [FK_Furnizori_Adrese]
GO
ALTER TABLE [dbo].[Furnizori]  WITH CHECK ADD  CONSTRAINT [FK_Furnizori_Useri] FOREIGN KEY([id_user])
REFERENCES [dbo].[Useri] ([id_user])
GO
ALTER TABLE [dbo].[Furnizori] CHECK CONSTRAINT [FK_Furnizori_Useri]
GO
ALTER TABLE [dbo].[Imagini]  WITH CHECK ADD  CONSTRAINT [FK_Imagini_Produse] FOREIGN KEY([id_produs])
REFERENCES [dbo].[Produse] ([id_produs])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Imagini] CHECK CONSTRAINT [FK_Imagini_Produse]
GO
ALTER TABLE [dbo].[Produse]  WITH CHECK ADD  CONSTRAINT [FK_Produse_Categorii] FOREIGN KEY([id_categorie])
REFERENCES [dbo].[Categorii] ([id_categorie])
GO
ALTER TABLE [dbo].[Produse] CHECK CONSTRAINT [FK_Produse_Categorii]
GO
ALTER TABLE [dbo].[Produse]  WITH CHECK ADD  CONSTRAINT [FK_Produse_Furnizori] FOREIGN KEY([id_furnizor])
REFERENCES [dbo].[Furnizori] ([id_furnizor])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Produse] CHECK CONSTRAINT [FK_Produse_Furnizori]
GO
ALTER TABLE [dbo].[Produse_Favorite]  WITH CHECK ADD  CONSTRAINT [FK_Produse_Favorite_Produse] FOREIGN KEY([id_produs])
REFERENCES [dbo].[Produse] ([id_produs])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Produse_Favorite] CHECK CONSTRAINT [FK_Produse_Favorite_Produse]
GO
ALTER TABLE [dbo].[Produse_Favorite]  WITH CHECK ADD  CONSTRAINT [FK_Produse_Favorite_Useri] FOREIGN KEY([id_user])
REFERENCES [dbo].[Useri] ([id_user])
GO
ALTER TABLE [dbo].[Produse_Favorite] CHECK CONSTRAINT [FK_Produse_Favorite_Useri]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Produse] FOREIGN KEY([id_produs])
REFERENCES [dbo].[Produse] ([id_produs])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Produse]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Useri] FOREIGN KEY([id_user])
REFERENCES [dbo].[Useri] ([id_user])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Useri]
GO
ALTER TABLE [dbo].[Specificatii]  WITH CHECK ADD  CONSTRAINT [FK_Produs_Specificatii] FOREIGN KEY([id_produs])
REFERENCES [dbo].[Produse] ([id_produs])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Specificatii] CHECK CONSTRAINT [FK_Produs_Specificatii]
GO
ALTER TABLE [dbo].[Tranzactii]  WITH CHECK ADD  CONSTRAINT [FK_Tranzactii_Furnizori] FOREIGN KEY([id_furnizor])
REFERENCES [dbo].[Furnizori] ([id_furnizor])
GO
ALTER TABLE [dbo].[Tranzactii] CHECK CONSTRAINT [FK_Tranzactii_Furnizori]
GO
ALTER TABLE [dbo].[Tranzactii]  WITH CHECK ADD  CONSTRAINT [FK_Tranzactii_Produse] FOREIGN KEY([id_produs])
REFERENCES [dbo].[Produse] ([id_produs])
GO
ALTER TABLE [dbo].[Tranzactii] CHECK CONSTRAINT [FK_Tranzactii_Produse]
GO
ALTER TABLE [dbo].[Tranzactii]  WITH CHECK ADD  CONSTRAINT [FK_Tranzactii_Useri] FOREIGN KEY([id_user])
REFERENCES [dbo].[Useri] ([id_user])
GO
ALTER TABLE [dbo].[Tranzactii] CHECK CONSTRAINT [FK_Tranzactii_Useri]
GO
ALTER TABLE [dbo].[Useri]  WITH CHECK ADD  CONSTRAINT [FK_Useri_Adresa] FOREIGN KEY([id_adresa])
REFERENCES [dbo].[Adresa] ([id_adresa])
GO
ALTER TABLE [dbo].[Useri] CHECK CONSTRAINT [FK_Useri_Adresa]
GO
USE [master]
GO
ALTER DATABASE [Shop_Online_Alpha] SET  READ_WRITE 
GO
