USE [EBSDB]
GO
ALTER TABLE [dbo].[tblTransactionHistory] DROP CONSTRAINT [FK_tblTransactionHistory_tblPayHistory]
GO
ALTER TABLE [dbo].[tblTransactionHistory] DROP CONSTRAINT [FK_tblTransactionHistory_tblCustomer]
GO
ALTER TABLE [dbo].[tblTransactionHistory] DROP CONSTRAINT [FK_tblTransactionHistory_tblBillHistory]
GO
ALTER TABLE [dbo].[tblPayHistory] DROP CONSTRAINT [FK_tblPayHistory_tblUser]
GO
ALTER TABLE [dbo].[tblPayHistory] DROP CONSTRAINT [FK_tblPayHistory_tblCustomer]
GO
ALTER TABLE [dbo].[tblPayHistory] DROP CONSTRAINT [FK_tblPayHistory_tblBillHistory]
GO
ALTER TABLE [dbo].[tblMeterType] DROP CONSTRAINT [FK_tblMeterType_tblUser]
GO
ALTER TABLE [dbo].[tblCustomer] DROP CONSTRAINT [FK_tblCustomer_tblUser]
GO
ALTER TABLE [dbo].[tblBillHistory] DROP CONSTRAINT [FK_tblBillHistory_tblUser]
GO
ALTER TABLE [dbo].[tblBillHistory] DROP CONSTRAINT [FK_tblBillHistory_tblCustomer]
GO
ALTER TABLE [dbo].[CustomerMeter] DROP CONSTRAINT [FK_CustomerMeter_tblUser]
GO
ALTER TABLE [dbo].[CustomerMeter] DROP CONSTRAINT [FK_CustomerMeter_tblMeterType]
GO
ALTER TABLE [dbo].[CustomerMeter] DROP CONSTRAINT [FK_CustomerMeter_CustomerMeter]
GO
ALTER TABLE [dbo].[tblUser] DROP CONSTRAINT [DF_tblUser_CreatedDate]
GO
ALTER TABLE [dbo].[tblTransactionHistory] DROP CONSTRAINT [DF_tblTransactionHistory_Status]
GO
ALTER TABLE [dbo].[tblTransactionHistory] DROP CONSTRAINT [DF_tblTransactionHistory_LateMonthCount]
GO
ALTER TABLE [dbo].[tblMeterType] DROP CONSTRAINT [DF_tblMeterType_CreatedDate]
GO
ALTER TABLE [dbo].[tblMeterType] DROP CONSTRAINT [DF_tblMeterType_IsActive]
GO
ALTER TABLE [dbo].[tblCustomer] DROP CONSTRAINT [DF_tblCustomer_DeletedDate]
GO
ALTER TABLE [dbo].[tblCustomer] DROP CONSTRAINT [DF_tblCustomer_IsActive]
GO
ALTER TABLE [dbo].[tblCustomer] DROP CONSTRAINT [DF_tblCustomer_CreatedDate]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 12/6/2022 8:43:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUser]') AND type in (N'U'))
DROP TABLE [dbo].[tblUser]
GO
/****** Object:  Table [dbo].[tblTransactionHistory]    Script Date: 12/6/2022 8:43:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTransactionHistory]') AND type in (N'U'))
DROP TABLE [dbo].[tblTransactionHistory]
GO
/****** Object:  Table [dbo].[tblPayHistory]    Script Date: 12/6/2022 8:43:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPayHistory]') AND type in (N'U'))
DROP TABLE [dbo].[tblPayHistory]
GO
/****** Object:  Table [dbo].[tblMeterType]    Script Date: 12/6/2022 8:43:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMeterType]') AND type in (N'U'))
DROP TABLE [dbo].[tblMeterType]
GO
/****** Object:  Table [dbo].[tblCustomer]    Script Date: 12/6/2022 8:43:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCustomer]') AND type in (N'U'))
DROP TABLE [dbo].[tblCustomer]
GO
/****** Object:  Table [dbo].[tblBillHistory]    Script Date: 12/6/2022 8:43:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblBillHistory]') AND type in (N'U'))
DROP TABLE [dbo].[tblBillHistory]
GO
/****** Object:  Table [dbo].[CustomerMeter]    Script Date: 12/6/2022 8:43:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerMeter]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerMeter]
GO
USE [master]
GO
/****** Object:  Database [EBSDB]    Script Date: 12/6/2022 8:43:47 AM ******/
DROP DATABASE [EBSDB]
GO
/****** Object:  Database [EBSDB]    Script Date: 12/6/2022 8:43:47 AM ******/
CREATE DATABASE [EBSDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EBSDB', FILENAME = N'C:\Users\ASUS\EBSDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EBSDB_log', FILENAME = N'C:\Users\ASUS\EBSDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EBSDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EBSDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EBSDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EBSDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EBSDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EBSDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EBSDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EBSDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EBSDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EBSDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EBSDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EBSDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EBSDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EBSDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EBSDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EBSDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EBSDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EBSDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EBSDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EBSDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EBSDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EBSDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EBSDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EBSDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EBSDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EBSDB] SET  MULTI_USER 
GO
ALTER DATABASE [EBSDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EBSDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EBSDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EBSDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EBSDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EBSDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [EBSDB] SET QUERY_STORE = OFF
GO
USE [EBSDB]
GO
/****** Object:  Table [dbo].[CustomerMeter]    Script Date: 12/6/2022 8:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerMeter](
	[Id] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[MeterId] [bigint] NOT NULL,
	[Status] [nchar](10) NULL,
 CONSTRAINT [PK_CustomerMeter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBillHistory]    Script Date: 12/6/2022 8:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBillHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[ReadDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[LastUnit] [bigint] NULL,
	[CurrentUnit] [bigint] NOT NULL,
	[UsedUnit] [bigint] NOT NULL,
	[Currency] [varchar](50) NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[BillPeriod] [varchar](10) NOT NULL,
	[PrepaidAmount] [decimal](18, 0) NULL,
	[RemainAmount] [decimal](18, 0) NULL,
	[FineAmount] [decimal](18, 0) NULL,
 CONSTRAINT [PK_tblBillHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCustomer]    Script Date: 12/6/2022 8:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCustomer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Address] [nvarchar](150) NULL,
	[Barcode] [varchar](50) NOT NULL,
	[MeterNo] [varchar](50) NOT NULL,
	[Township] [nvarchar](100) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[DeletedBy] [bigint] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_tblCustomer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMeterType]    Script Date: 12/6/2022 8:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMeterType](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblMeterType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPayHistory]    Script Date: 12/6/2022 8:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPayHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[BillId] [bigint] NOT NULL,
	[PayAmount] [decimal](18, 0) NOT NULL,
	[PayDate] [datetime] NOT NULL,
	[Method] [bigint] NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [bigint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblPayHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTransactionHistory]    Script Date: 12/6/2022 8:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTransactionHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[BillId] [bigint] NOT NULL,
	[PayId] [bigint] NOT NULL,
	[MeterNo] [varchar](50) NOT NULL,
	[PrepaidAmount] [decimal](18, 0) NULL,
	[RemainAmount] [decimal](18, 0) NULL,
	[FineAmount] [decimal](18, 0) NULL,
	[LateMonthCount] [tinyint] NULL,
	[BillPeriod] [varchar](10) NOT NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_tblTransactionHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 12/6/2022 8:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreatedBy] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCustomer] ADD  CONSTRAINT [DF_tblCustomer_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblCustomer] ADD  CONSTRAINT [DF_tblCustomer_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblCustomer] ADD  CONSTRAINT [DF_tblCustomer_DeletedDate]  DEFAULT (getdate()) FOR [DeletedDate]
GO
ALTER TABLE [dbo].[tblMeterType] ADD  CONSTRAINT [DF_tblMeterType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[tblMeterType] ADD  CONSTRAINT [DF_tblMeterType_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblTransactionHistory] ADD  CONSTRAINT [DF_tblTransactionHistory_LateMonthCount]  DEFAULT ((0)) FOR [LateMonthCount]
GO
ALTER TABLE [dbo].[tblTransactionHistory] ADD  CONSTRAINT [DF_tblTransactionHistory_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[tblUser] ADD  CONSTRAINT [DF_tblUser_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CustomerMeter]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMeter_CustomerMeter] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([Id])
GO
ALTER TABLE [dbo].[CustomerMeter] CHECK CONSTRAINT [FK_CustomerMeter_CustomerMeter]
GO
ALTER TABLE [dbo].[CustomerMeter]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMeter_tblMeterType] FOREIGN KEY([MeterId])
REFERENCES [dbo].[tblMeterType] ([Id])
GO
ALTER TABLE [dbo].[CustomerMeter] CHECK CONSTRAINT [FK_CustomerMeter_tblMeterType]
GO
ALTER TABLE [dbo].[CustomerMeter]  WITH CHECK ADD  CONSTRAINT [FK_CustomerMeter_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[CustomerMeter] CHECK CONSTRAINT [FK_CustomerMeter_tblUser]
GO
ALTER TABLE [dbo].[tblBillHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblBillHistory_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([Id])
GO
ALTER TABLE [dbo].[tblBillHistory] CHECK CONSTRAINT [FK_tblBillHistory_tblCustomer]
GO
ALTER TABLE [dbo].[tblBillHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblBillHistory_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblBillHistory] CHECK CONSTRAINT [FK_tblBillHistory_tblUser]
GO
ALTER TABLE [dbo].[tblCustomer]  WITH CHECK ADD  CONSTRAINT [FK_tblCustomer_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblCustomer] CHECK CONSTRAINT [FK_tblCustomer_tblUser]
GO
ALTER TABLE [dbo].[tblMeterType]  WITH CHECK ADD  CONSTRAINT [FK_tblMeterType_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblMeterType] CHECK CONSTRAINT [FK_tblMeterType_tblUser]
GO
ALTER TABLE [dbo].[tblPayHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblPayHistory_tblBillHistory] FOREIGN KEY([BillId])
REFERENCES [dbo].[tblBillHistory] ([Id])
GO
ALTER TABLE [dbo].[tblPayHistory] CHECK CONSTRAINT [FK_tblPayHistory_tblBillHistory]
GO
ALTER TABLE [dbo].[tblPayHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblPayHistory_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([Id])
GO
ALTER TABLE [dbo].[tblPayHistory] CHECK CONSTRAINT [FK_tblPayHistory_tblCustomer]
GO
ALTER TABLE [dbo].[tblPayHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblPayHistory_tblUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblPayHistory] CHECK CONSTRAINT [FK_tblPayHistory_tblUser]
GO
ALTER TABLE [dbo].[tblTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblTransactionHistory_tblBillHistory] FOREIGN KEY([BillId])
REFERENCES [dbo].[tblBillHistory] ([Id])
GO
ALTER TABLE [dbo].[tblTransactionHistory] CHECK CONSTRAINT [FK_tblTransactionHistory_tblBillHistory]
GO
ALTER TABLE [dbo].[tblTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblTransactionHistory_tblCustomer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[tblCustomer] ([Id])
GO
ALTER TABLE [dbo].[tblTransactionHistory] CHECK CONSTRAINT [FK_tblTransactionHistory_tblCustomer]
GO
ALTER TABLE [dbo].[tblTransactionHistory]  WITH CHECK ADD  CONSTRAINT [FK_tblTransactionHistory_tblPayHistory] FOREIGN KEY([PayId])
REFERENCES [dbo].[tblPayHistory] ([Id])
GO
ALTER TABLE [dbo].[tblTransactionHistory] CHECK CONSTRAINT [FK_tblTransactionHistory_tblPayHistory]
GO
USE [master]
GO
ALTER DATABASE [EBSDB] SET  READ_WRITE 
GO
