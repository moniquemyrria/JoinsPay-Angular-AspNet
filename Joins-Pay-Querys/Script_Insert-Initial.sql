USE [JOINS_PAY_BACK_SERVICE]
GO
SET IDENTITY_INSERT [dbo].[Register.Department_Category] ON 

INSERT [dbo].[Register.Department_Category] ([id], [description], [deleted], [dateCreated]) VALUES (1, N'STORE', N'N', CAST(N'2022-01-27T23:08:16.5720000' AS DateTime2))
INSERT [dbo].[Register.Department_Category] ([id], [description], [deleted], [dateCreated]) VALUES (2, N'COMPANY', N'N', CAST(N'2022-01-27T23:08:16.5720000' AS DateTime2))
INSERT [dbo].[Register.Department_Category] ([id], [description], [deleted], [dateCreated]) VALUES (3, N'PEOPLE', N'N', CAST(N'2022-01-27T23:08:16.5720000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Register.Department_Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Register.Payment_Method_Category] ON 

INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (1, N'PIX', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (2, N'DINHEIRO', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (3, N'CHEQUE', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (5, N'CARTÃO DE DÉBITO', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (6, N'CARTÃO DE CRÉDITO', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (7, N'BOLETO BANCÁRIO', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (8, N'CREDIÁRIO', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (9, N'CARTEIRA DIGITAL (E-WALLETS)', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (10, N'LINK DE PAGAMENTO', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (12, N'TRANSFERÊNCIA BANCÁRIA', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (13, N'TRANSFERÊNCIA ELETRÔNICA', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (14, N'TROCA DE PONTOS / MILHAS', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (15, N'INTERMEDIADOR DE PAGAMENTOS', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
INSERT [dbo].[Register.Payment_Method_Category] ([id], [description], [deleted], [dateCreated]) VALUES (17, N'DEPÓSITO BANCÁRIO', N'N', CAST(N'2022-02-02T15:50:21.5550000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Register.Payment_Method_Category] OFF
GO
