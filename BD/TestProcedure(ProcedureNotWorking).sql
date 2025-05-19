ALTER PROCEDURE dbo.AddPictureBoardGame
@fileName nvarchar(70),
@nameBoardGame nvarchar(50)
AS
BEGIN
	DECLARE @sql nvarchar(160);
	SET @sql = 'UPDATE BoardGame 
			SET Picture = (SELECT * FROM OPENROWSET(BULK  N'+@fileName+', SINGLE_BLOB) AS image)
			WHERE BoardGame.Name = @nameBoardGame';
	EXEC sp_executesql @sql
END;

UPDATE BoardGame 
			SET Picture = (SELECT * FROM OPENROWSET(BULK 'C:\Users\FVST\Desktop\Картинки БД\2.jpg', SINGLE_BLOB) AS image)
			WHERE BoardGame.Name = 'Бункер';

SELECT Picture
FROM BoardGame
WHERE IdBoardGame = 1
FOR XML RAW, BINARY BASE64;

EXEC dbo.AddPictureBoardGame N'C:\Users\FVST\Desktop\Картинки БД\2.jpg', 'Бункер';