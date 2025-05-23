USE [ProjectKr]
GO
/****** Object:  UserDefinedFunction [dbo].[CountAdditions]    Script Date: 05.04.2023 0:12:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CountAdditions]
(
	@NameBoardGame nvarchar(50)
)
RETURNS INT
AS BEGIN
	DECLARE @count INT
	SELECT @count = Count(*)
	FROM BoardGame JOIN
		 Addition ON BoardGame.IdBoardGame = Addition.IdBoardGame
	WHERE BoardGame.Name = @NameBoardGame
	RETURN @count
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetBoardGameByPrice]    Script Date: 05.04.2023 0:12:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetBoardGameByPrice]
(
	@min decimal(7,2),
	@max decimal(7,2)
)
RETURNS TABLE
AS 
RETURN
	SELECT *
	FROM BoardGame
	WHERE BoardGame.Price>=@min AND  BoardGame.Price<=@max
GO
