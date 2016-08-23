USE [TrainingTracker]
GO
/****** Object:  StoredProcedure [dbo].[GET_ALL_USERS]    Script Date: 8/16/2016 6:37:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GET_ALL_USERS]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

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
  FROM [dbo].[User]
  ORDER BY [FirstName]
  
END

