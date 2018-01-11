CREATE TABLE [dbo].[Items]
(
	[Id] int NOT NULL,
	[Name] varchar(100) NOT NULL,
	[Type] varchar(20) NOT NULL,
	[Amount] decimal(8, 2) NOT NULL,
	[IsReported] bit NOT NULL,
	[Category] varchar(30) NOT NULL,
	CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED
	(
		[Id] ASC
	)
)

INSERT INTO [dbo].[Items] VALUES (1, 'Name1', 'Red', 100, 1, 'DEV')
INSERT INTO [dbo].[Items] VALUES (2, 'Name2', 'Green', 200, 1, 'DEV')
INSERT INTO [dbo].[Items] VALUES (3, 'Name3', 'Blue', 300, 1, 'DEV')
INSERT INTO [dbo].[Items] VALUES (4, 'Name4', 'Red', 400, 0, 'DEV')
INSERT INTO [dbo].[Items] VALUES (5, 'Name5', 'Green', 500, 0, 'DEV')
