CREATE TABLE [dbo].[TblUsers] (
    [UserID]        INT          NOT NULL,
    [Name]          VARCHAR (60) NOT NULL,
    [Phone]         CHAR (10)    NOT NULL,
    [Country]       VARCHAR (30) NOT NULL,
    [NumberOfGames] INT          NOT NULL,
    CONSTRAINT [PK_TblUsers] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_TblUsers_Country] FOREIGN KEY ([Country]) REFERENCES [dbo].[TblCountries] ([Country])
);

