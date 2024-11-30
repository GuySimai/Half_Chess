CREATE TABLE [dbo].[TblChessGames] (
    [GameID]     INT          IDENTITY (1, 1) NOT NULL,
    [UserID]     INT          NOT NULL,
    [StartTime]  DATETIME     NOT NULL,
    [GameLength] VARCHAR (10) NOT NULL,
    [Winner]     VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_TblChessGames_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[TblUsers] ([UserID])
);

