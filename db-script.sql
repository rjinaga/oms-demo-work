USE [master]
GO
/****** Object:  Database [OrderManagement]    Script Date: 9/13/2020 12:32:36 PM ******/
CREATE DATABASE [OrderManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrderManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS2016\MSSQL\DATA\OrderManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OrderManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS2016\MSSQL\DATA\OrderManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [OrderManagement] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrderManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrderManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrderManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrderManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrderManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrderManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrderManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrderManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrderManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrderManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrderManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrderManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrderManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrderManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrderManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrderManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OrderManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrderManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrderManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrderManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrderManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrderManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrderManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrderManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OrderManagement] SET  MULTI_USER 
GO
ALTER DATABASE [OrderManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrderManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrderManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrderManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OrderManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OrderManagement] SET QUERY_STORE = OFF
GO
USE [OrderManagement]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [OrderManagement]
GO
/****** Object:  Schema [Inventory]    Script Date: 9/13/2020 12:32:36 PM ******/
CREATE SCHEMA [Inventory]
GO
/****** Object:  Schema [Sales]    Script Date: 9/13/2020 12:32:36 PM ******/
CREATE SCHEMA [Sales]
GO
/****** Object:  Schema [SysRef]    Script Date: 9/13/2020 12:32:36 PM ******/
CREATE SCHEMA [SysRef]
GO
/****** Object:  Table [Inventory].[Product]    Script Date: 9/13/2020 12:32:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Inventory].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[Weight] [decimal](9, 2) NOT NULL,
	[Height] [decimal](9, 2) NOT NULL,
	[Image] [varchar](100) NOT NULL,
	[SKU] [varchar](40) NOT NULL,
	[Barcode] [varchar](50) NOT NULL,
	[AvailableQuantity] [decimal](18, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Inventory].[ProductCategory]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Inventory].[ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Buyer]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[Buyer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](35) NOT NULL,
	[LastName] [varchar](35) NOT NULL,
	[Email] [varchar](320) NOT NULL,
	[Phone] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Buyer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[Order]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BuyerId] [int] NOT NULL,
	[ShippingAddressId] [int] NOT NULL,
	[OrderStatusId] [char](1) NOT NULL,
	[OrderCreatedTimestamp] [datetime] NOT NULL,
	[LastUpdatedTimestamp] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[OrderItem]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[OrderItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[Weight] [decimal](9, 2) NOT NULL,
	[Height] [decimal](9, 2) NOT NULL,
	[SKU] [varchar](40) NOT NULL,
	[Barcode] [varchar](50) NOT NULL,
	[Image] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Sales].[ShippingAddress]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Sales].[ShippingAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZIP] [varchar](9) NOT NULL,
 CONSTRAINT [PK_ShippingAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SysRef].[OrderStatus]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SysRef].[OrderStatus](
	[Id] [char](1) NOT NULL,
	[Description] [varchar](15) NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductCategory]    Script Date: 9/13/2020 12:32:37 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductCategory] ON [Inventory].[ProductCategory]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrderStatus_Description]    Script Date: 9/13/2020 12:32:37 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_OrderStatus_Description] ON [SysRef].[OrderStatus]
(
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Inventory].[Product]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Product] FOREIGN KEY([ProductCategoryId])
REFERENCES [Inventory].[ProductCategory] ([Id])
GO
ALTER TABLE [Inventory].[Product] CHECK CONSTRAINT [FK_ProductCategory_Product]
GO
ALTER TABLE [Sales].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Buyer] FOREIGN KEY([BuyerId])
REFERENCES [Sales].[Buyer] ([Id])
GO
ALTER TABLE [Sales].[Order] CHECK CONSTRAINT [FK_Order_Buyer]
GO
ALTER TABLE [Sales].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderShippingAddress] FOREIGN KEY([ShippingAddressId])
REFERENCES [Sales].[ShippingAddress] ([Id])
GO
ALTER TABLE [Sales].[Order] CHECK CONSTRAINT [FK_Order_OrderShippingAddress]
GO
ALTER TABLE [Sales].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY([OrderStatusId])
REFERENCES [SysRef].[OrderStatus] ([Id])
GO
ALTER TABLE [Sales].[Order] CHECK CONSTRAINT [FK_Order_OrderStatus]
GO
ALTER TABLE [Sales].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderId])
REFERENCES [Sales].[Order] ([Id])
GO
ALTER TABLE [Sales].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [Sales].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([ProductId])
REFERENCES [Inventory].[Product] ([Id])
GO
ALTER TABLE [Sales].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product]
GO
/****** Object:  StoredProcedure [Sales].[GetAllOrders]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Sales].[GetAllOrders] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT O.Id as OrderId, B.FirstName, B.LastName, B.Email, B.Phone,
		   S.AddressLine1, S.AddressLine2, S.City, S.State, S.ZIP,
		   O.OrderCreatedTimestamp, COUNT(OI.Id) as ItemsCount
	FROM [Sales].[Order] O
	JOIN [Sales].[Buyer] B ON O.BuyerId = B.Id
	JOIN [Sales].[ShippingAddress] S ON O.ShippingAddressId = S.Id
	JOIN [Sales].[OrderItem] OI ON O.Id = OI.OrderId
	
	GROUP BY O.Id, B.FirstName, B.LastName, B.Email, B.Phone,
			 O.OrderCreatedTimestamp,
		     S.AddressLine1, S.AddressLine2, S.City, S.State, S.ZIP;

END
GO
/****** Object:  StoredProcedure [Sales].[GetOrdersByBuyerId]    Script Date: 9/13/2020 12:32:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Sales].[GetOrdersByBuyerId]
@BuyerId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT O.Id as OrderId, B.FirstName, B.LastName, B.Email, B.Phone,
		   S.AddressLine1, S.AddressLine2, S.City, S.State, S.ZIP,
		   O.OrderCreatedTimestamp,
		   COUNT(OI.Id) as ItemsCount
	FROM [Sales].[Order] O
	JOIN [Sales].[Buyer] B ON O.BuyerId = B.Id
	JOIN [Sales].[ShippingAddress] S ON O.ShippingAddressId = S.Id
	JOIN [Sales].[OrderItem] OI ON O.Id = OI.OrderId
	
	WHERE O.BuyerId = @BuyerId

	GROUP BY O.Id, B.FirstName, B.LastName, B.Email, B.Phone,
			 O.OrderCreatedTimestamp,
		     S.AddressLine1, S.AddressLine2, S.City, S.State, S.ZIP;

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image versions to be maintained' , @level0type=N'SCHEMA',@level0name=N'Inventory', @level1type=N'TABLE',@level1name=N'Product'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image versions to be maintained' , @level0type=N'SCHEMA',@level0name=N'Sales', @level1type=N'TABLE',@level1name=N'OrderItem'
GO
USE [master]
GO
ALTER DATABASE [OrderManagement] SET  READ_WRITE 
GO
