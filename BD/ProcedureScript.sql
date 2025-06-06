USE [ProjectKr]
GO
/****** Object:  StoredProcedure [dbo].[AddCategory]    Script Date: 10.04.2023 22:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCategory]
@Name nvarchar(20),
@Decription nvarchar(500) = null,
@Genre bit = 0,
@Id INT OUTPUT 
AS
BEGIN
	INSERT INTO Category(Name,Description,Genre)
	VALUES(@Name, @Decription, @Genre);
	SELECT @Id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[ChangePriceBoardGameAndHisAddition]    Script Date: 10.04.2023 22:59:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangePriceBoardGameAndHisAddition]
@IdBoardGame INT,
@Price decimal(7,2),
@count INT OUTPUT 
AS
BEGIN
	UPDATE BoardGame
	SET Price+=@Price
	WHERE IdBoardGame = @IdBoardGame;
	UPDATE Addition
	SET Price+=@Price
	WHERE IdBoardGame = @IdBoardGame;
	SELECT @count = @@ROWCOUNT
END
GO
