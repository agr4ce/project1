USE [ProjectKr]
GO
/****** Object:  View [dbo].[MonthOrders]    Script Date: 05.04.2023 0:12:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MonthOrders]
AS
SELECT Purshases.IdPurshases, 
	   Purshases.DateTime, 
	   CONCAT_WS(' ',Purshases.BuyersName,Purshases.BuyersSurname,Purshases.BuyersPatronymic) AS Buyer,
	   ISNULL(SUM(BoardGame.Price * PurshasedBoardGame.Amount),0) AS BoardGamesPrice,
	   ISNULL(SUM(Addition.Price * PurshasedAddition.Amount),0) AS AdditionGamesPrice,
	   ISNULL(SUM(BoardGame.Price * PurshasedBoardGame.Amount),0) + ISNULL(SUM(Addition.Price * PurshasedAddition.Amount),0) AS OrderPrice
FROM BoardGame RIGHT JOIN
	PurshasedBoardGame ON BoardGame.IdBoardGame = PurshasedBoardGame.IdBoardGame RIGHT JOIN
	Purshases ON PurshasedBoardGame.IdPurshases = Purshases.IdPurshases LEFT JOIN
	PurshasedAddition ON Purshases.IdPurshases = PurshasedAddition.IdPurshases LEFT JOIN
	Addition ON PurshasedAddition.IdAddition = Addition.IdAddition
WHERE MONTH(Purshases.DateTime)=MONTH(GETDATE()) AND YEAR(Purshases.DateTime)=YEAR(GETDATE())
GROUP BY Purshases.IdPurshases, Purshases.DateTime, Purshases.BuyersName,Purshases.BuyersSurname,Purshases.BuyersPatronymic
GO
/****** Object:  View [dbo].[Top10BoardGame]    Script Date: 05.04.2023 0:12:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Top10BoardGame]
AS
SELECT TOP 10 BoardGame.IdBoardGame, BoardGame.Name, SUM(PurshasedBoardGame.Amount) AS FinalyAmount
FROM BoardGame JOIN
	 PurshasedBoardGame ON BoardGame.IdBoardGame = PurshasedBoardGame.IdBoardGame
GROUP BY BoardGame.IdBoardGame, BoardGame.Name
ORDER BY FinalyAmount DESC
GO
