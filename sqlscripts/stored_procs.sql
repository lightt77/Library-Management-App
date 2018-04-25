CREATE OR ALTER PROCEDURE dbo.GetRoleByEmailAddress(@email_address varchar(100))
AS
	SELECT role_name from roles join users on roles.role_id=users.role_id where users.email_address= @email_address;
GO
EXEC dbo.GetRoleByEmailAddress @email_address='user1@acc.com';

-------------------------------------------------------------------------------------------------------

--INCOMPLETE
CREATE OR ALTER PROCEDURE dbo.GetBooksCurrentlySubcscribedToGivenUser (@user_id int)
AS
	--SELECT * from dbo.title WHERE author=@author_name;
	SELECT title_name from dbo.title join dbo.book on dbo.book.title_id=dbo.title.title_id join dbo.rental on dbo.book.book_id=dbo.rental.book_id where dbo.rental.user_id=@user_id AND dbo.rental.rental_status=0;
GO
EXEC dbo.GetBooksCurrentlySubcscribedToGivenUser @user_id = 1;

-------------------------------------------------------------------------------------------------------
--works
CREATE OR ALTER PROCEDURE dbo.GetAllBooks
AS
	SELECT * from dbo.title;
	Select * from dbo.genre join dbo.title_genre_map on dbo.genre.genre_id=dbo.title_genre_map.genre_id;
GO
EXEC GetAllBooks;

------------------------------------------------------------------------------------------------------------------------------------------
--works
CREATE OR ALTER PROCEDURE dbo.GetBooksByGenre(@genre_name VARCHAR(50))
AS
	SELECT * from dbo.title 
			join dbo.title_genre_map on dbo.title.title_id=dbo.title_genre_map.title_id
			join dbo.genre on dbo.title_genre_map.genre_id=dbo.genre.genre_id
			where dbo.genre.genre_name=@genre_name;
GO
EXEC GetBooksByGenre @genre_name='Fantasy';

------------------------------------------------------------------------------------------------------------------------------------------
--works
CREATE OR ALTER PROCEDURE dbo.GetQuantity(@title_name nvarchar(50), @author_name nvarchar(50))
AS
BEGIN
 SELECT quantity FROM dbo.title WHERE @title_name = title_name AND @author_name=author;
END
EXEC dbo.GetQuantity @title_name = 'abc', @author_name='xyz';

------------------------------------------------------------------------------------------------------------------------------------------


CREATE OR ALTER PROCEDURE dbo.AddTitle(@title_name VARCHAR(50), @author_name VARCHAR(50), @rating INT, @quantity INT, @price INT, @genre_name VARCHAR(50))
AS
	INSERT INTO dbo.title(title_name,author,)
GO
EXEC GetBooksByGenre @genre_name='Fantasy';

------------------------------------------------------------------------------------------------------------------------------------------
--works
CREATE OR ALTER PROCEDURE dbo.DeleteTitle (@title_name nvarchar(50))
AS
DECLARE @title_id int
BEGIN
 SET @title_id = (SELECT title_id FROM dbo.title WHERE title_name = @title_name);
 DELETE FROM dbo.title_genre_map WHERE @title_id = title_id;
 DELETE FROM dbo.title WHERE title_name = @title_name;
END
EXEC dbo.DeleteTitle @title_name='efg';

------------------------------------------------------------------------------------------------------------------------------------------

--works for single genre
--for multiple genres, make a call to AddGenre from server side
CREATE OR ALTER PROCEDURE dbo.AddBook(@title_name VARCHAR(50), @author_name VARCHAR(50), @rating INT, @price INT, @genre_name VARCHAR(50))
AS
	DECLARE @title_id INT;
	DECLARE @genre_id INT;
	DECLARE @title_quantity INT;

	--check if title of the book is absent
	IF((Select Count(title_id) from dbo.title where title_name=@title_name AND author=@author_name)=0)
		begin
			--add title
			--set quantity to 1 as this is the first book
			INSERT INTO dbo.title(title_name,author,rating,quantity,price,created_on,last_updated)
						VALUES (@title_name,@author_name,@rating,1,@price,SYSDATETIME(),SYSDATETIME());

			--check if genre is absent
			IF((Select Count(genre_id) from dbo.genre where genre_name=@genre_name)=0)
				begin
					--add genre
					INSERT INTO dbo.genre(genre_name,created_on,last_updated)
						VALUES (@genre_name,SYSDATETIME(),SYSDATETIME());
				end

			SET @title_id=(Select title_id from dbo.title where title_name=@title_name AND author=@author_name);
			SET @genre_id=(Select genre_id from dbo.genre where genre_name=@genre_name);

			--insert entry in title-genre map
			INSERT INTO dbo.title_genre_map(title_id,genre_id,created_on,last_updated)
						VALUES (@title_id,@genre_id,SYSDATETIME(),SYSDATETIME());
		end
	ELSE
		begin
			--increment title quantity as title is already present
			SET @title_quantity=(Select quantity from dbo.title where title_name=@title_name AND author=@author_name);

			UPDATE dbo.title
				SET quantity=@title_quantity+1
				where title_name=@title_name AND author=@author_name;
		end;

	--add new entry in book table
	SET @title_id=(Select title_id from dbo.title where title_name=@title_name AND author=@author_name);
	INSERT INTO dbo.book(title_id,created_on,last_updated)
				VALUES (@title_id,SYSDATETIME(),SYSDATETIME());

GO

EXEC dbo.AddBook @title_name='HP', @author_name='JKR', @rating=5, @price=299,@genre_name='Fantasy';
EXEC dbo.AddBook @title_name='HP', @author_name='JKR', @rating=5, @price=299,@genre_name='Fantasy';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddNewGenreToTitle(@title_name VARCHAR(50), @author_name VARCHAR(50), @genre_name VARCHAR(50))
AS
	DECLARE @title_id INT;
	DECLARE @genre_id INT;

	--add genre only if the title exists
	IF((Select Count(title_id) from dbo.title where title_name=@title_name AND author=@author_name)!=0)
	begin

		SET @title_id=(Select title_id from dbo.title where title_name=@title_name AND author=@author_name);

		--check if the mapping already exists
		if((Select Count(*) from dbo.title_genre_map join dbo.genre on dbo.title_genre_map.genre_id=dbo.genre.genre_id
				 where title_id=@title_id AND genre_name=@genre_name)=0)
		begin
			--check if genre exists
			IF((Select Count(genre_id) from dbo.genre where genre_name=@genre_name)=0)
			begin
				--add genre
				INSERT INTO dbo.genre(genre_name,created_on,last_updated)
					VALUES (@genre_name,SYSDATETIME(),SYSDATETIME());
			end
		
			SET @genre_id=(Select genre_id from dbo.genre where genre_name=@genre_name);

			--insert entry in title-genre map
			INSERT INTO dbo.title_genre_map(title_id,genre_id,created_on,last_updated)
				VALUES (@title_id,@genre_id,SYSDATETIME(),SYSDATETIME());
		end

	end
GO

EXEC dbo.AddNewGenreToTitle @title_name='HPP',@author_name='JKR',@genre_name='Magicc';


------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.DeleteBook (@title_name nvarchar(50), @author_name nvarchar(50))
AS
DECLARE @title_id int
DECLARE @current_quantity int
BEGIN
 SET @title_id = (SELECT title_id FROM dbo.title WHERE title_name = @title_name);
 SET @current_quantity = (SELECT quantity FROM dbo.title WHERE title_name = @title_name AND @author_name = author);
 IF(@current_quantity = 1)
  EXEC dbo.DeleteTitle @title_name;
 ELSE
  BEGIN
   DELETE FROM dbo.book WHERE book_id = (SELECT MAX(book_id) from dbo.book where title_id = @title_id);
   UPDATE dbo.title SET quantity = @current_quantity-1 WHERE title_name = @title_name;
  END
END

EXEC dbo.DeleteBook @title_name='ddd', @author_name='eee';

------------------------------------------------------------------------------------------------------------------------------------------