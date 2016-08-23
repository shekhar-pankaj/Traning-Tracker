USE [TrainingTracker]
GO
/****** Object:  StoredProcedure [dbo].[GET_USER]    Script Date: 8/16/2016 6:37:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Harsh Wardhan
-- Create date: 13th July, 2016
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[GET_USER]
@UserId INT = 0,
@UserName VARCHAR(50) = NULL,
@Scenario INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF(@Scenario = 2)
	BEGIN
		SET @UserId = (SELECT [UserId]
						FROM [dbo].[User]
						WHERE [UserName] = @UserName)
	END
	
	DECLARE @UserRating AS INT
		--EXEC [GET_USER_RATING] @UserId, @UserRating OUTPUT
	
		SELECT [UserId]
			,[FirstName]
			,[LastName]
			,[UserName]
			,[Password]
			,[Email]
			,[Designation]
			,[ProfilePictureName]
			,[IsFemale]
			,[IsAdministrator]
			,[IsTrainer]
			,[IsTrainee]
			,[IsManager]
			,[IsActive]
			,0 AS UserRating
			--,@UserRating AS UserRating
		FROM [dbo].[User]
		WHERE [UserId] = @UserId
END

