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
EXEC GetBooksByGenre @genre_name='Horror';

------------------------------------------------------------------------------------------------------------------------------------------
--works
CREATE OR ALTER PROCEDURE dbo.GetBooksByAuthor(@author_name VARCHAR(50))
AS
	DECLARE @title_id INT;
	
	SET @title_id=(SELECT title_id from dbo.title where dbo.title.author=@author_name);

	SELECT * from dbo.title where dbo.title.author=@author_name
	Select * from dbo.genre 
			join dbo.title_genre_map on dbo.genre.genre_id=dbo.title_genre_map.genre_id
			where dbo.title_genre_map.title_id=@title_id;
GO
EXEC GetBooksByAuthor @author_name='JKR';

------------------------------------------------------------------------------------------------------------------------------------------
--works
CREATE OR ALTER PROCEDURE dbo.GetQuantity(@title_name nvarchar(50), @author_name nvarchar(50))
AS
BEGIN
 SELECT quantity FROM dbo.title WHERE @title_name = title_name AND @author_name=author;
END
EXEC dbo.GetQuantity @title_name = 'abc', @author_name='xyz';

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

EXEC dbo.AddBook @title_name='title1', @author_name='author1', @rating=5, @price=299,@genre_name='Fantasy';
EXEC dbo.AddBook @title_name='title2', @author_name='author2', @rating=5, @price=299,@genre_name='Horror';

------------------------------------------------------------------------------------------------------------------------------------------
--works
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
--works
CREATE OR ALTER PROCEDURE dbo.FindUsersForBook (@title_name nvarchar(50))
AS
BEGIN
 Select user_name,email_address,password,mobile_number,residential_address,role_name from dbo.users 
     JOIN dbo.rental ON dbo.rental.user_id = dbo.users.user_id
     JOIN dbo.book ON dbo.rental.book_id = dbo.book.book_id  
     JOIN dbo.title ON dbo.book.title_id = dbo.title.title_id
	 JOIN dbo.roles ON dbo.users.role_id=dbo.roles.role_id
     WHERE dbo.title.title_name = @title_name;
END
EXEC dbo.FindUsersForBook @title_name = 'abc'

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddRental (@user_name nvarchar(50), @title_name nvarchar(50), @no_of_days int)
AS
DECLARE @user_id int;
DECLARE @book_id int;
DECLARE @title_id int;
BEGIN

	SET @user_id = (SELECT user_id FROM dbo.users WHERE user_name = @user_name);
	SET @title_id = (SELECT title_id FROM dbo.title WHERE title_name = @title_name); 
 
	SET @book_id = (Select MIN(dbo.book.book_id) from dbo.book
					where dbo.book.title_id=@title_id AND dbo.book.availability_status = 1);

	INSERT INTO dbo.rental(user_id, book_id,issue_date,return_date,rental_status,created_on,last_updated)
		VALUES (@user_id,@book_id,SYSDATETIME(),DATEADD(DAY,@no_of_days,SYSDATETIME()),0,SYSDATETIME(),SYSDATETIME());
END

EXEC dbo.AddRental @user_name = 'Abhishek', @title_name = 'CLRS', @no_of_days=15;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.CheckAdmin(@user_name nvarchar(50), @checkAdmin int out)
AS
DECLARE @user_id int;
DECLARE @role_id int;
BEGIN
 SET @user_id = (SELECT user_id FROM dbo.users WHERE dbo.users.user_name = @user_name);
 SET @role_id = (SELECT role_id FROM dbo.users WHERE dbo.users.user_id = @user_id);
 IF @role_id = 1 OR @role_id = 3
  SET @checkAdmin = 1;
 ELSE
  SET @checkAdmin = 0; 
END
DECLARE @checkAdministrator int;
Execute dbo.CheckAdmin 'user1', @checkAdministrator out;
PRINT @checkAdministrator;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddRoles(@role_name nvarchar(50))
AS
DECLARE @roleExist int;
BEGIN
 SET @roleExist = (SELECT role_id FROM dbo.roles WHERE dbo.roles.role_name = @role_name);
 IF @roleExist IS NULL
  INSERT INTO dbo.roles (role_name,created_on,last_updated)
   VALUES (@role_name,SYSDATETIME(),SYSDATETIME());
END
EXEC dbo.AddRoles 'ad';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.DeleteRoles(@role_name nvarchar(50))
AS
DECLARE @roleExist int;
BEGIN
 SET @roleExist = (SELECT role_id FROM dbo.roles WHERE dbo.roles.role_name = @role_name);
 IF @roleExist IS NOT NULL
  DELETE FROM dbo.roles WHERE role_name=@role_name
END
EXEC dbo.DeleteRoles @role_name='adf';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddRental (@user_name nvarchar(50), @title_name nvarchar(50), @no_of_days int)
AS
DECLARE @user_id int;
DECLARE @book_id int;
DECLARE @title_id int;
DECLARE @title_count int;
DECLARE @user_count int;
BEGIN

		SET @user_id = (SELECT user_id FROM dbo.users WHERE user_name = @user_name);
		SET @title_id = (SELECT title_id FROM dbo.title WHERE title_name = @title_name); 
 
		SET @book_id = (Select MIN(dbo.book.book_id) from dbo.book
						where dbo.book.title_id=@title_id AND dbo.book.availability_status = 1);

		INSERT INTO dbo.rental(user_id, book_id,issue_date,return_date,rental_status,created_on,last_updated)
			VALUES (@user_id,@book_id,SYSDATETIME(),DATEADD(DAY,@no_of_days,SYSDATETIME()),0,SYSDATETIME(),SYSDATETIME());
	
END

EXEC dbo.AddRental @user_name = 'Abhishek', @title_name = 'CLRS', @no_of_days=15;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.CheckTitleExists(@title_name nvarchar(50))
AS
BEGIN
	Select Count(title_name) as title_count from dbo.title where dbo.title.title_name=@title_name;
END

DECLARE @titlExist nvarchar(50);
Execute @titlExist= dbo.CheckTitleExists 'Goosebumps';
PRINT @titlExist;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.CheckUserExists(@user_name nvarchar(50))
AS
BEGIN
	Select Count(user_name) as user_count from dbo.users where dbo.users.user_name=@user_name;
END

DECLARE @userExist nvarchar(50);
Execute @userExist= dbo.CheckUserExists 'Karthik';
PRINT @userExist;