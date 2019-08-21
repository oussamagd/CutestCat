IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('PS_GetCats'))
   exec('CREATE PROCEDURE PS_GetCats AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE PS_GetCats
AS

SELECT Reference			AS 'Reference',
	   [Url]				AS 'Url',
	   WinVoteCount			AS 'WinVoteCount', 
	   LostVoteCount		AS 'LostVoteCount'
FROM Tbl_Cat

GO
