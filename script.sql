USE [master]
GO
/****** Object:  Database [Notas]    Script Date: 24/1/2024 08:52:49 ******/
CREATE DATABASE [Notas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Notas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Notas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Notas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Notas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Notas] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Notas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Notas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Notas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Notas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Notas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Notas] SET ARITHABORT OFF 
GO
ALTER DATABASE [Notas] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Notas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Notas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Notas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Notas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Notas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Notas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Notas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Notas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Notas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Notas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Notas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Notas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Notas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Notas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Notas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Notas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Notas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Notas] SET  MULTI_USER 
GO
ALTER DATABASE [Notas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Notas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Notas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Notas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Notas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Notas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Notas] SET QUERY_STORE = OFF
GO
USE [Notas]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 24/1/2024 08:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Color] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notas]    Script Date: 24/1/2024 08:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notas](
	[IdNota] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Archivada] [bit] NULL,
	[IdCategoria] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotasCategorias]    Script Date: 24/1/2024 08:52:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotasCategorias](
	[IdNotaCategoria] [int] IDENTITY(1,1) NOT NULL,
	[IdNota] [int] NULL,
	[IdCategoria] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNotaCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([IdCategoria], [Descripcion], [Color]) VALUES (1, N'Importante', N'Rojo')
INSERT [dbo].[Categoria] ([IdCategoria], [Descripcion], [Color]) VALUES (2, N'Postergable', N'Verde')
INSERT [dbo].[Categoria] ([IdCategoria], [Descripcion], [Color]) VALUES (3, N'Urgente', N'Azul')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Notas] ON 

INSERT [dbo].[Notas] ([IdNota], [Descripcion], [Archivada], [IdCategoria]) VALUES (50, N'Nota 2', 1, NULL)
INSERT [dbo].[Notas] ([IdNota], [Descripcion], [Archivada], [IdCategoria]) VALUES (67, N'Nota 12', 0, NULL)
INSERT [dbo].[Notas] ([IdNota], [Descripcion], [Archivada], [IdCategoria]) VALUES (72, N'Nota 622', 0, NULL)
INSERT [dbo].[Notas] ([IdNota], [Descripcion], [Archivada], [IdCategoria]) VALUES (73, N'Nota 1231', 0, NULL)
INSERT [dbo].[Notas] ([IdNota], [Descripcion], [Archivada], [IdCategoria]) VALUES (74, N'Nota 55', 0, NULL)
SET IDENTITY_INSERT [dbo].[Notas] OFF
GO
SET IDENTITY_INSERT [dbo].[NotasCategorias] ON 

INSERT [dbo].[NotasCategorias] ([IdNotaCategoria], [IdNota], [IdCategoria]) VALUES (19, 50, 3)
INSERT [dbo].[NotasCategorias] ([IdNotaCategoria], [IdNota], [IdCategoria]) VALUES (41, 67, 2)
INSERT [dbo].[NotasCategorias] ([IdNotaCategoria], [IdNota], [IdCategoria]) VALUES (49, 72, 1)
INSERT [dbo].[NotasCategorias] ([IdNotaCategoria], [IdNota], [IdCategoria]) VALUES (50, 72, 2)
INSERT [dbo].[NotasCategorias] ([IdNotaCategoria], [IdNota], [IdCategoria]) VALUES (51, 72, 3)
INSERT [dbo].[NotasCategorias] ([IdNotaCategoria], [IdNota], [IdCategoria]) VALUES (52, 73, 3)
INSERT [dbo].[NotasCategorias] ([IdNotaCategoria], [IdNota], [IdCategoria]) VALUES (53, 74, 1)
SET IDENTITY_INSERT [dbo].[NotasCategorias] OFF
GO
ALTER TABLE [dbo].[Notas]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[NotasCategorias]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[NotasCategorias]  WITH CHECK ADD FOREIGN KEY([IdNota])
REFERENCES [dbo].[Notas] ([IdNota])
GO
USE [master]
GO
ALTER DATABASE [Notas] SET  READ_WRITE 
GO
