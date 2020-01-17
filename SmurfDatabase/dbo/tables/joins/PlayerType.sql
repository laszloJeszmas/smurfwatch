CREATE TABLE [dbo].[PlayerPlayerType]
(
	[PlayerId] INT NOT NULL, 
    [TypeId] INT NOT NULL, 
    CONSTRAINT [FK_PlayerType_ToTable] FOREIGN KEY ([PlayerId]) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_PlayerType_ToTable_1] FOREIGN KEY ([TypeId]) REFERENCES [PlayerType]([Id])
)
