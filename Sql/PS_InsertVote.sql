IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('PS_InsertVote'))
   exec('CREATE PROCEDURE PS_InsertVote AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE PS_InsertVote
	 @WinCatReference varchar(50), 
	 @WinCatUrl varchar(200), 
	 @LostCatReference varchar(50),
	 @LostCatUrl varchar(200)
AS

BEGIN TRANSACTION [InsertVoteTransaction]

  BEGIN TRY

			DECLARE @WinCatId INT, @LostCatId INT

			IF NOT EXISTS (SELECT * FROM Tbl_Cat WHERE Reference = @WinCatReference)
			BEGIN
				INSERT INTO Tbl_Cat (Reference,[Url], WinVoteCount,LostVoteCount)
					VALUES(@WinCatReference, @WinCatUrl , 1, 0)
			END
			ELSE
			BEGIN
				UPDATE Tbl_Cat
				SET WinVoteCount = WinVoteCount + 1
				WHERE Reference = @WinCatReference
			END


			IF NOT EXISTS (SELECT * FROM Tbl_Cat WHERE Reference = @LostCatReference)
			BEGIN
				INSERT INTO Tbl_Cat (Reference,[Url], WinVoteCount,LostVoteCount)
					VALUES(@LostCatReference, @LostCatUrl , 0, 1)
			END
			ELSE
			BEGIN
				UPDATE Tbl_Cat
				SET LostVoteCount = LostVoteCount + 1
				WHERE Reference = @LostCatReference
			END

			INSERT INTO Tbl_CatVote (WinCatId,
									 LostCatId, 
									 CreationDate) 
					VALUES			((SELECT CatId FROM Tbl_Cat WHERE Reference = @WinCatReference), 
									 (SELECT CatId FROM Tbl_Cat WHERE Reference = @LostCatReference),
									  GETDATE())

     COMMIT TRANSACTION [InsertVoteTransaction]

  END TRY

  BEGIN CATCH

      ROLLBACK TRANSACTION [InsertVoteTransaction]

  END CATCH 

GO

