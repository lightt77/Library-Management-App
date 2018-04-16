--display all books
USE library;
GO
CREATE PROCEDURE dbo.GetAllBooks
AS
SELECT * FROM dbo.title;
GO
dbo.GetAllBooks;

--------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.GetBookByTitleName (@title_name varchar(50))
AS
	SELECT * from dbo.title WHERE title_name=@title_name;

GO
EXEC dbo.GetBookByTitleName @title_name = 'abc';

--------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.GetBookQuantityByTitleName (@title_name varchar(50))
AS
	SELECT quantity from dbo.title WHERE title_name=@title_name;
GO
EXEC dbo.GetBookQuantityByTitleName @title_name = 'abc';

--------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.GetBooksByGenre (@genre_name varchar(50))
AS
	select title_name from dbo.title join dbo.title_genre_map on dbo.title.title_id=dbo.title_genre_map.title_id 
	where genre_id=(SELECT genre_id FROM dbo.genre WHERE genre_name=@genre_name);
GO
EXEC dbo.GetBooksByGenre @genre_name ='Horror';

--------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.GetBooksByAuthor (@author_name varchar(50))
AS
	SELECT * from dbo.title WHERE author=@author_name;
GO
EXEC dbo.GetBooksByAuthor @author_name ='xyz';