CREATE TABLE [dbo].[PlayerMap]
(
	[PlayerId] INT NOT NULL, 
    [MapId] INT NOT NULL, 
    CONSTRAINT [FK_PlayerMap_ToTable] FOREIGN KEY ([PlayerId]) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_PlayerMap_ToTable_1] FOREIGN KEY ([MapId]) REFERENCES [Map]([Id])
)
