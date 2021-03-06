SET IDENTITY_INSERT [dbo].[CMS_Rule] ON 

INSERT [dbo].[CMS_Rule] ([RuleID], [Title], [ZoneName], [RuleExpression], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate], [RuleItems]) VALUES (1, N'所有页面', N'顶部', N'StartsWith(ValueOf(''Url''),''/'')', NULL, 1, N'admin', N'ZKEASOFT', CAST(N'2018-05-21 23:43:34.623' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2018-05-21 23:43:34.623' AS DateTime), N'[{"Condition":"and","FunctionName":"StartsWith","Property":"ValueOf(''Url'')","Value":"/","Title":null,"Description":null,"Status":null,"CreateBy":null,"CreatebyName":null,"CreateDate":null,"LastUpdateBy":null,"LastUpdateByName":null,"LastUpdateDate":null,"ActionType":1}]')
INSERT [dbo].[CMS_Rule] ([RuleID], [Title], [ZoneName], [RuleExpression], [Description], [Status], [CreateBy], [CreatebyName], [CreateDate], [LastUpdateBy], [LastUpdateByName], [LastUpdateDate], [RuleItems]) VALUES (2, N'所有页面', N'底部', N'StartsWith(ValueOf(''Url''),''/'')', NULL, 1, N'admin', N'ZKEASOFT', CAST(N'2018-05-21 23:43:52.343' AS DateTime), N'admin', N'ZKEASOFT', CAST(N'2018-05-21 23:43:52.343' AS DateTime), N'[{"Condition":"and","FunctionName":"StartsWith","Property":"ValueOf(''Url'')","Value":"/","Title":null,"Description":null,"Status":null,"CreateBy":null,"CreatebyName":null,"CreateDate":null,"LastUpdateBy":null,"LastUpdateByName":null,"LastUpdateDate":null,"ActionType":1}]')
SET IDENTITY_INSERT [dbo].[CMS_Rule] OFF

GO

GO
