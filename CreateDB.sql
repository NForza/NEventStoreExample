USE [NEventStoreExample]

CREATE TABLE [dbo].[ActiveAccounts](
	[Id] [uniqueidentifier],
	[Name] [nvarchar](50),
	[Amount] [float],
	[Address] [nvarchar](max),
	[City] [nvarchar](max),
		CONSTRAINT [PK_dbo.ActiveAccounts] PRIMARY KEY CLUSTERED ([Id] ASC))
GO

CREATE TABLE [dbo].[Modifications](
	[Id] [uniqueidentifier],
	[AccountId] [uniqueidentifier],
	[ModificationType] [int],
	[Amount] [float],
	[ModificationDateTime] [datetime],
 CONSTRAINT [PK_dbo.Modifications] PRIMARY KEY CLUSTERED ([Id] ASC))
GO
