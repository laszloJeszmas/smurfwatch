CREATE TABLE [dbo].[Player]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(25) NOT NULL, 
    [Level] INT NOT NULL, 
    [Battletag] VARCHAR(30) NULL, 
    [Description] VARCHAR(MAX) NULL, 
    [Rank] VARCHAR(15) NOT NULL, 
    [MainRank] VARCHAR(15) NULL, 
    [PrivateProfile] BIT NULL
)
