if not exists ( select 1 from sysobjects where id = OBJECT_ID('Tbl_Cat') and type = 'U')
CREATE TABLE [Tbl_Cat] (
    [CatId] INT NOT NULL IDENTITY,
	[Reference] VARCHAR(50) NOT NULL,
	[Url] VARCHAR(200) NOT NULL,
	[WinVoteCount] INT NOT NULL,
	[LostVoteCount] INT NOT NULL,
    CONSTRAINT [PK_Cat] PRIMARY KEY ([CatId])
);
Go


if not exists ( select 1 from sysobjects where id = OBJECT_ID('Tbl_CatVote') and type = 'U')
CREATE TABLE [Tbl_CatVote] (
    [CatVoteId] int NOT NULL IDENTITY,
    [WinCatId] int NOT NULL,
	[LostCatId] int NOT NULL,
    [CreationDate] DATETIME2,
    CONSTRAINT [PK_Vote] PRIMARY KEY ([CatVoteId]),
    CONSTRAINT [FK_Vote_WinCat_CatId] FOREIGN KEY ([WinCatId]) REFERENCES [Tbl_Cat] ([CatId]),
	CONSTRAINT [FK_Vote_LostCat_CatId] FOREIGN KEY ([LostCatId]) REFERENCES [Tbl_Cat] ([CatId])
);
Go



