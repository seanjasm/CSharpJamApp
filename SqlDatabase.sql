USE [csharpjamDB]
GO

/****** Object: Table [dbo].[Game] Script Date: 6/16/2019 8:53:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


GO
CREATE TABLE [dbo].[Game] (
    [MatchId] INT IDENTITY NOT NULL,
    [TeamId]  NVARCHAR(25) NOT NULL,
    [Score]   INT NOT NULL
);

ALTER TABLE [dbo].[Game]
ADD CONSTRAINT PK_Comp_Game
PRIMARY KEY(MatchId, TeamId);

ALTER TABLE [dbo].[Game]
ADD CONSTRAINT FK_Game_Match
FOREIGN KEY ([MatchId]) 
REFERENCES dbo.[Match]([Id]);

ALTER TABLE [dbo].[Game]
ADD CONSTRAINT FK_Game_Team
FOREIGN KEY ([TeamId]) 
REFERENCES dbo.[Team]([Name]);


--Create Team Table


CREATE TABLE [dbo].[Team] (
    [Name]     NVARCHAR (25)  NOT NULL PRIMARY KEY,
    [OwnerId]  NVARCHAR (128) NOT NULL,
    [Win]      TINYINT        NOT NULL,
    [Lost]     TINYINT        NOT NULL,
    [Draw]     TINYINT        NOT NULL,
    [Location] NVARCHAR (MAX) NOT NULL
);

ALTER TABLE [dbo].[Team]
ADD CONSTRAINT FK_Team_Owner
FOREIGN KEY ([OwnerId]) REFERENCES dbo.AspNetUsers([Id]);


CREATE TABLE [dbo].[Player] (
    [Id]         NVARCHAR(10)  NOT NULL,
    [TeamId]     NVARCHAR (25) NOT NULL,
    [FirstName]  NVARCHAR (20) NOT NULL,
    [LastName]   NVARCHAR (20) NOT NULL,
    [Skill]      TINYINT       NOT NULL,
    [Agility]    TINYINT       NOT NULL,
    [Strength]   TINYINT       NOT NULL,
    [Endurance]  TINYINT       NOT NULL,
    [Aggression] TINYINT       NOT NULL,
    [Humor]      TINYINT       NOT NULL,
    [TeamWork]   TINYINT       NOT NULL,
	[Rating]	 TINYINT	   NOT NULL,
	[Height]	 DECIMAL	   NOT NULL,
	[Weight]	 DECIMAL	   NOT NULL,
	[Description] NVARCHAR(MAX)	   NOT NULL,
	[PictureUrl] VARCHAR(MAX) NULL
);

ALTER TABLE [dbo].[Player]
ADD CONSTRAINT PK_Comp_Player
PRIMARY KEY (Id, TeamId)

GO

ALTER TABLE [dbo].[Player]
ADD CONSTRAINT FK_Player_Team
FOREIGN KEY (TeamId) REFERENCES dbo.Team ([Name])

GO

--Insert procedure

 CREATE PROCEDURE [dbo].[usp_InsertTeam]
	  @Name NVARCHAR(25), 
	  @OwnerId NVARCHAR(128),
	  @Win TINYINT,
	  @Lost TINYINT,
	  @Draw TINYINT,
	  @Location NVARCHAR(max),
	  @message NVARCHAR(MAX) OUTPUT
  AS
  BEGIN
  INSERT INTO Team(Name,OwnerId,Win,Lost,Draw,Location)VALUES(@Name,@OwnerId,@Win,@Lost,@Draw,@Location)
  IF(@@ROWCOUNT > 0)
	BEGIN
		SET @message = 'Team ' + @Name + ' added successfully!';
	END
	ELSE
	BEGIN 
		SET @message = 'Oops! Could not add team ' + @name;
	END
  END


GO

CREATE PROCEDURE usp_InsertPlayer
@Id varchar(10),
@TeamId nvarchar(20),
@FirstName nvarchar(20),
@LastName nvarchar(20),
@Skill tinyint,
@Agility tinyint,
@Strength tinyint,
@Endurance tinyint,
@Aggression tinyint,
@Humor tinyint,
@TeamWork tinyint,
@Rating tinyint,
@Height decimal,
@Weight decimal,
@desc varchar(max)
AS
BEGIN
INSERT INTO PLAYER
(ID,TeamId,FirstName,LastName,Skill,Agility,Strength,Endurance,Aggression,Humor,TeamWork, Height, [Weight], Rating, [Description])

VALUES
(@ID,@TeamId,@FirstName,@LastName,@Skill,@Agility,@Strength,@Endurance,@Aggression,@Humor,@TeamWork, @Height, @Weight, @Rating, @desc)

END

GO

---Update Procedure


  CREATE PROCEDURE [dbo].[usp_UpdateTeam]
	  @Name NVARCHAR(25), 
	  @OwnerId NVARCHAR(128),
	  @Win TINYINT,
	  @Lost TINYINT,
	  @Draw TINYINT,
	  @Location NVARCHAR(max),
	  @message NVARCHAR(max) OUTPUT
  AS
  BEGIN
  SET NOCOUNT ON;
  UPDATE Team 
  SET 
	[Name] = @Name, 
	Win = @Win, 
	Lost = @Lost, 
	Draw = @Draw,
	[Location] = @Location
  WHERE 
	OwnerId= @OwnerId

	IF(@@ROWCOUNT > 0)
	BEGIN
		SET @message = 'Team ' + @Name + ' updated successfully!';
	END
	ELSE
	BEGIN 
		SET @message = 'Oops! Could not update team ' + @name;
	END
  END

  GO

CREATE PROCEDURE usp_UpdatePlayer
@Id nvarchar(10),
@Skill tinyint,
@Agility tinyint,
@Strength tinyint,
@Endurance tinyint,
@Aggression tinyint,
@Humor tinyint,
@TeamWork tinyint
AS
BEGIN
	UPDATE Player 
SET 
	Skill += @Skill,
	Agility += @Agility,
	Strength += @Strength,
	Endurance += @Endurance,
	Aggression += @Aggression,
	Humor += @Humor,
	TeamWork += @TeamWork
WHERE 
	Id=@Id
END

GO


----Delete Procedure

GO
CREATE PROCEDURE usp_DeleteTeam
	@OwnerId NVARCHAR(128)
AS
 BEGIN
    DELETE TEAM WHERE OwnerId=@OwnerId
 END

 GO