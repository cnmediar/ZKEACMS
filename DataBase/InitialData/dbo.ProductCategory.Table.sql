SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([ID], [Title], [Description], [ParentID], [Url], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (1, N'产品', NULL, 0, NULL, 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:49:32.000' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2017-11-17 15:10:08.193' AS DateTime))
INSERT [dbo].[ProductCategory] ([ID], [Title], [Description], [ParentID], [Url], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (2, N'耳机', NULL, 1, N'headset', 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:49:50.000' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2018-08-15 15:30:59.233' AS DateTime))
INSERT [dbo].[ProductCategory] ([ID], [Title], [Description], [ParentID], [Url], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate]) VALUES (3, N'键盘', NULL, 1, N'keyboard', 1, N'admin', N'ZKEASOFT', CAST(N'2016-03-10 13:50:07.000' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2018-08-15 15:31:08.063' AS DateTime))
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF

GO

GO
