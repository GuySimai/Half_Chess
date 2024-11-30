CREATE TABLE [dbo].[TblMoves] (
    [MoveID]     INT          IDENTITY (1, 1) NOT NULL,
    [GameID]     INT          NOT NULL,
    [MoveNumber] INT          NOT NULL,
    [FromX]      INT          NOT NULL,
    [FromY]      INT          NOT NULL,
    [ToX]        INT          NOT NULL,
    [ToY]        INT          NOT NULL,
    [Promotion]  VARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([MoveID] ASC),
    FOREIGN KEY ([GameID]) REFERENCES [dbo].[TblRecordings] ([GameID])
);

