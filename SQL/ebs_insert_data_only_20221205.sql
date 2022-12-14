USE [EBSDB]
GO
SET IDENTITY_INSERT [dbo].[tblUser] ON 

INSERT [dbo].[tblUser] ([Id], [Name], [Email], [Password], [CreatedBy], [CreatedDate], [IsActive]) VALUES (1, N'Admin', N'admin@gmail.com', N'D2-F2-78-0E-25-1D-15-66-ED-D7-28-46-D4-0A-D9-7F', NULL, CAST(N'2022-12-04T13:46:59.813' AS DateTime), 1)
INSERT [dbo].[tblUser] ([Id], [Name], [Email], [Password], [CreatedBy], [CreatedDate], [IsActive]) VALUES (2, N'User', N'user@gmail.com', N'D2-F2-78-0E-25-1D-15-66-ED-D7-28-46-D4-0A-D9-7F', NULL, CAST(N'2022-12-04T17:39:21.223' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[tblUser] OFF
GO
SET IDENTITY_INSERT [dbo].[tblCustomer] ON 

INSERT [dbo].[tblCustomer] ([Id], [Name], [Email], [Password], [Address], [Barcode], [MeterNo], [Township], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [DeletedBy], [DeletedDate]) VALUES (1, N'Daw Myint Khine', N'user@gmail.com', NULL, N'Chanung', N'2145369871', N'2213654789', N'Chanung', NULL, CAST(N'2022-12-04T23:16:39.563' AS DateTime), NULL, NULL, 1, NULL, CAST(N'2022-12-04T23:16:39.563' AS DateTime))
INSERT [dbo].[tblCustomer] ([Id], [Name], [Email], [Password], [Address], [Barcode], [MeterNo], [Township], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [IsActive], [DeletedBy], [DeletedDate]) VALUES (2, N'Tun', N'user2@gmail.com', NULL, N'Chanung', N'2145369871', N'2136512478', N'Chanung', NULL, CAST(N'2022-12-04T23:18:12.970' AS DateTime), NULL, NULL, 1, NULL, CAST(N'2022-12-04T23:18:12.970' AS DateTime))
SET IDENTITY_INSERT [dbo].[tblCustomer] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMeterType] ON 

INSERT [dbo].[tblMeterType] ([Id], [Name], [IsActive], [CreatedBy], [CreatedDate]) VALUES (1, N'Home', 1, 1, CAST(N'2022-12-06T02:33:02.623' AS DateTime))
INSERT [dbo].[tblMeterType] ([Id], [Name], [IsActive], [CreatedBy], [CreatedDate]) VALUES (2, N'Business', 1, 1, CAST(N'2022-12-06T02:33:17.083' AS DateTime))
SET IDENTITY_INSERT [dbo].[tblMeterType] OFF
GO
INSERT [dbo].[CustomerMeter] ([Id], [CustomerId], [UserId], [MeterId], [Status]) VALUES (0, 1, 1, 1, NULL)
GO
