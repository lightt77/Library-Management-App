CREATE OR ALTER PROCEDURE dbo.GetRoleByEmailAddress(@email_address varchar(100))
AS
	SELECT role_name from roles join users on roles.role_id=users.role_id where users.email_address= @email_address;
GO
EXEC dbo.GetRoleByEmailAddress @email_address='user1@acc.com';

-----------------------------------------------------------------------------------------------------------------------------------------------

--display all books
CREATE OR ALTER PROCEDURE dbo.GetAllTitles
AS
SELECT * FROM dbo.title;
GO
dbo.GetAllTitles;

--------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddTitle (@title_name varchar(50),@author_name varchar(50),@rating int,@quantity int,@price int)

AS
	INSERT INTO dbo.title(title_name,author,rating,quantity,price,created_on,last_updated)
		VALUES (@title_name,@author_name,@rating,@quantity,@price,SYSDATETIME(),SYSDATETIME());
GO
EXEC dbo.AddTitle @author_name ='hjdahkdhaskdhaskjhdksjhd', @title_name='abhsdijid', @rating=5, @quantity=100, @price=100 ;

--------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.DeleteTitle (@title_name varchar(50))
AS
declare @title_id int 
begin
	SET @title_id = (Select title_id from dbo.title where title_name=@title_name);

	DELETE FROM dbo.title_genre_map where title_id=@title_id;
	DELETE FROM dbo.book where title_id=@title_id;
	DELETE FROM dbo.title where title_id=@title_id;
END
select * from dbo.title;
EXEC dbo.DeleteTitle @title_name='abc';

--------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.GetTitleIdByTitleName (@title_name nvarchar(50))
AS
	SELECT title_id FROM dbo.title WHERE title_name = @title_name;
GO
EXEC GetTitleIdByTitleName'efg';

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

--------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.GetBooksCurrentlySubcscribedToGivenUser (@user_id int)
AS
	--SELECT * from dbo.title WHERE author=@author_name;
	SELECT title_name from dbo.title join dbo.book on dbo.book.title_id=dbo.title.title_id join dbo.rental on dbo.book.book_id=dbo.rental.book_id where dbo.rental.user_id=@user_id AND dbo.rental.rental_status=0;
GO
EXEC dbo.GetBooksCurrentlySubcscribedToGivenUser @user_id = 1;
-------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.AddBook (@title_id int)
AS
	INSERT INTO dbo.book(title_id,created_on,last_updated)
	VALUES (@title_id,SYSDATETIME(),SYSDATETIME());
GO
EXEC dbo.AddBook @title_id = 2;
