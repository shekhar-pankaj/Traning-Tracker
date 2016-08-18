ALTER TABLE [dbo].[User]
  ADD IsActive bit NOT NULL
    CONSTRAINT DF_User_IsActive DEFAULT 1
