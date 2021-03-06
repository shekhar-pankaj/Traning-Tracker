USE [TrainingTracker]
GO
/****** Object:  StoredProcedure [dbo].[GET_USER_FEEDBACKS]    Script Date: 9/13/2016 6:06:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Harsh Wardhan
-- Create date: 13th July, 2016
-- Description:	Scenario:
--				1 = Get feedbacks on user id.
--				2 = Gets user feedbacks with pagination.
-- EXEC [GET_USER_FEEDBACKS] @UserId=20,@PageSize=15,@FeedbackId=1
-- =============================================
ALTER PROCEDURE [dbo].[GET_USER_FEEDBACKS]
@UserId INT,
@StartDate DATE = null,
@EndDate DATE = null,
@FeedbackId INT = null,
@PageSize INT = 5,
@Offset INT = 0,
@Scenario INT=0,
@StartAddedOn DATE = null,
@EndAddedOn DATE = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Gets all user feedbacks
	--IF (@Scenario = 1)
	--BEGIN 
	--	SELECT [FeedbackId]
	--	  ,[FeedbackText]
	--	  ,[Title]
	--	  ,[FeedbackType]
	--	  ,[Description] AS [FeedbackTypeName]
	--	  ,[ProjectId]
	--	  ,[SkillId]
	--	  ,[Rating]
	--	  ,[AddedBy]
	--	  ,u.[FirstName] + ' ' + u.[LastName] AS AddedByUser
	--	  ,u.ProfilePictureName AS AddedByUserImage
	--	  ,[AddedFor]
	--	  ,[AddedOn]
	--	  ,[StartDate]
	--	  ,[EndDate]
	--	FROM [dbo].[Feedback] f
	--	JOIN [User] u
	--	ON f.[AddedBy] = u.[UserId]
	--	JOIN [FedbackType] t
	--	ON f.[FeedbackType] = t.[FeedbackTypeId]
	--	WHERE f.[AddedFor] = @UserId
	--	ORDER BY [AddedOn] DESC
	--END
	
	-- Gets user feedbacks with pagination
	--IF (@Scenario = 2)
	BEGIN 
		SELECT DISTINCT [FeedbackId]
		  ,[FeedbackText]
		  ,[Title]
		  ,[FeedbackType]
		  ,[Description] AS [FeedbackTypeName]
		  ,[ProjectId]
		  ,[SkillId]
		  ,[Rating]
		  ,[AddedBy]
		  ,u.[FirstName] + ' ' + u.[LastName] AS AddedByUser
		  ,u.ProfilePictureName AS AddedByUserImage
		  ,[AddedFor]
		  ,[AddedOn]
		  ,[StartDate]
		  ,[EndDate]
		FROM [dbo].[Feedback] f
		JOIN [User] u
		ON f.[AddedBy] = u.[UserId]
		JOIN [FeedbackType] t
		ON f.[FeedbackType] = t.[FeedbackTypeId]
		WHERE f.[AddedFor] = @UserId AND 
			  f.[FeedbackType] = ISNULL(@FeedbackId,f.[FeedbackType]) AND 
			  f.[StartDate] >= ISNULL(@StartDate,f.[StartDate]) AND 
			  f.[EndDate] >= ISNULL(@EndDate,f.[EndDate])  AND 
			  ( CAST( f.[AddedOn] AS DATE) between CAST( ISNULL(@StartAddedOn,f.[AddedOn]) AS DATE) 
			  AND CAST( ISNULL(@EndAddedOn,f.[AddedOn]) AS DATE) )

		ORDER BY [AddedOn] DESC
		OFFSET  @Offset ROWS 
		FETCH NEXT @PageSize ROWS ONLY 
	END

END
