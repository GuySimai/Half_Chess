CREATE TABLE [dbo].[TblRecordings] (
    [GameID]      INT          IDENTITY (1, 1) NOT NULL,
    [UserID]      INT          NOT NULL,
    [UserName]    VARCHAR (20) NOT NULL,
    [UserColor]   VARCHAR (20) NOT NULL,
    [date]        DATETIME     NOT NULL,
    [MovesNumber] INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([GameID] ASC)
);

