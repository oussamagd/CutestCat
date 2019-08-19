IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('PS_GetCats'))
   exec('CREATE PROCEDURE PS_GetCats AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE PS_GetCats
AS

SELECT Reference,
	   [Url],
	   WinVoteCount,
	   LostVoteCount
FROM Tbl_Cat

GO
