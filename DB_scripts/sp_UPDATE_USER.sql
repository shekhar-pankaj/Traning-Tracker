USE [TrainingTracker]
GO
/****** Object:  StoredProcedure [dbo].[UPDATE_USER]    Script Date: 8/18/2016 2:46:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Divya>
-- Create date: <16th August 2016>
-- Description:	<Used for updating user>
-- =============================================
ALTER PROCEDURE [dbo].[UPDATE_USER]
	@UserId INT,
	@FirstName varchar(100),
	@LastName varchar(100),
	@Username  VARCHAR(200),
	@Password  VARCHAR(200)=null,
	@Email varchar(100),
	@Designation varchar(100),
	@ProfilePictureName varchar(100),
	@IsFemale bit,
	@IsAdministrator bit,
	@IsTrainer bit,
	@IsTrainee bit,
	@IsManager bit,
	@IsActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;
	if(@Password='')
	BEGIN
	SET @Password = NULL
	END
	
	UPDATE [dbo].[User]
	SET		[FirstName] = @FirstName 
		   ,[LastName] = @LastName
           ,[UserName] = @Username
           ,[Email] = @Email
           ,[Designation]= @Designation
           ,[ProfilePictureName] = @ProfilePictureName
           ,[IsFemale] = @IsFemale
           ,[IsAdministrator] = @IsAdministrator
           ,[IsTrainer] = @IsTrainer
           ,[IsTrainee] = @IsTrainee
           ,[IsManager] = @IsManager
           ,[Password] = ISNULL(@Password,[Password])
           ,[IsActive] = @IsActive
     WHERE UserId=@UserId;
	 
END





