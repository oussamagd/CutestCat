IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('PS_InsertVote'))
   exec('CREATE PROCEDURE PS_InsertVote AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE PS_InsertVote
	 @WinCatReference varchar(50), 
	 @LostCatReference varchar(50)
AS

IF NOT EXISTS (SELECT * FROM Tbl_Cat WHERE Reference = @WinCatReference)
BEGIN
	INSERT INTO Tbl_Cat (Reference, WinVoteCount,LostVoteCount)
		VALUES(@WinCatReference , 1, 0)
END
ELSE
BEGIN
	UPDATE Tbl_Cat
	SET WinVoteCount = WinVoteCount + 1
	WHERE Reference = @WinCatReference
END


IF NOT EXISTS (SELECT * FROM Tbl_Cat WHERE Reference = @LostCatReference)
BEGIN
	INSERT INTO Tbl_Cat (Reference, WinVoteCount,LostVoteCount)
		VALUES(@LostCatReference , 0, 1)
END
ELSE
BEGIN
	UPDATE Tbl_Cat
	SET LostVoteCount = LostVoteCount + 1
	WHERE Reference = @LostCatReference
END



GO

