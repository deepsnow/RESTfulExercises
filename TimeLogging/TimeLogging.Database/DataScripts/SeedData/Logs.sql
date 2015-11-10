--IF NOT Exists(Select Id From dbo.Logs)
BEGIN
	DELETE FROM [dbo].[Logs]
	SET IDENTITY_INSERT [dbo].[Logs] ON
	INSERT INTO [dbo].[Logs] ([Id], [UserId], [StartTime], [EndTime], [Comment], [Billable]) VALUES (1, N'1         ', N'2015-10-01 08:00:00', N'2015-10-01 09:00:00', N'Testing                                                                                                                                                                                                                                                        ', 0)
	INSERT INTO [dbo].[Logs] ([Id], [UserId], [StartTime], [EndTime], [Comment], [Billable]) VALUES (3, N'1         ', N'2015-10-01 09:00:00', N'2015-10-01 11:00:00', N'Billable Log Item                                                                                                                                                                                                                                              ', 1)
	SET IDENTITY_INSERT [dbo].[Logs] OFF
END