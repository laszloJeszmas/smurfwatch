CREATE TABLE [dbo].[PlayerHero]
(
	[PlayerId] INT NOT NULL, 
    [HeroId] INT NOT NULL, 
    CONSTRAINT [FK_PlayerHero_ToTable] FOREIGN KEY ([PlayerId]) REFERENCES [Player]([Id]), 
    CONSTRAINT [FK_PlayerHero_ToTable_1] FOREIGN KEY ([HeroId]) REFERENCES [Hero]([Id])
)
