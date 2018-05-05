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

CREATE OR ALTER PROCEDURE dbo.AddBook(@title_name VARCHAR(50))
AS
	--add book only if the title exists
	IF((Select Count(*) from dbo.title where title_name=@title_name)!=0)
	begin
		DECLARE @title_id INT;

		--add new entry in book table
		SET @title_id=(Select title_id from dbo.title where title_name=@title_name);
		INSERT INTO dbo.book(title_id,created_on,last_updated)
			VALUES (@title_id,SYSDATETIME(),SYSDATETIME());
	end
GO

EXEC dbo.AddBook @title_name='title4';

------------------------------------------------------------------------------------------------------------------------------------------

--works for single genre
--for multiple genres, make a call to AddGenre from server side
CREATE OR ALTER PROCEDURE dbo.AddTitle(@title_name VARCHAR(50), @author_name VARCHAR(50), @rating INT, @price INT, @genre_name VARCHAR(50))
AS
	DECLARE @title_id INT;
	DECLARE @genre_id INT;
	DECLARE @title_quantity INT;

	--add title only if the title is not already there 
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
		
			--add new entry in book table
			SET @title_id=(Select title_id from dbo.title where title_name=@title_name AND author=@author_name);
			INSERT INTO dbo.book(title_id,created_on,last_updated)
				VALUES (@title_id,SYSDATETIME(),SYSDATETIME());
		end
GO

EXEC dbo.AddTitle @title_name='title4', @author_name='author4', @rating=5, @price=299,@genre_name='Mathematics';

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

CREATE OR ALTER PROCEDURE dbo.CheckEmailExists(@emailAddress nvarchar(50), @emailExist nvarchar(50) out)
AS
BEGIN
 SET @emailExist = (SELECT email_address FROM dbo.users WHERE dbo.users.email_address = @emailAddress);
END
DECLARE @emExist nvarchar(50);
Execute dbo.CheckEmailExists 'kart@acc.com', @emExist out;
PRINT @emExist;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.CheckRoleExists(@rolename nvarchar(50), @roleExist int out)
AS
BEGIN
 SET @roleExist = (SELECT role_id FROM dbo.roles WHERE dbo.roles.role_name = @rolename);
END
DECLARE @rolExist int;
Execute dbo.CheckRoleExists 'admin', @rolExist out;
PRINT @rolExist;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.CheckTitleExists(@title_name nvarchar(50), @titleExist nvarchar(50) out)
AS
BEGIN
 SET @titleExist = (SELECT title_name FROM dbo.title WHERE dbo.title.title_name = @title_name);
END
DECLARE @titlExist nvarchar(50);
Execute dbo.CheckTitleExists 'abc', @titlExist out;
PRINT @titlExist;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.RegisterUser (
    @rolename nvarchar(50), 
    @username nvarchar(50),
    @email nvarchar(50), 
    @password nvarchar(50),
    @mobile_number nvarchar(50),
    @address nvarchar(50))
AS
DECLARE @role_id int;
DECLARE @emailExist nvarchar(50);
DECLARE @roleExist int;
BEGIN
 Execute dbo.CheckEmailExists @email, @emailExist out;
 IF @emailExist IS NULL
 BEGIN
  Execute dbo.CheckRoleExists @rolename, @roleExist out;
  IF @roleExist IS NOT NULL
  BEGIN
   SET @role_id = (SELECT role_id FROM dbo.roles WHERE role_name = @rolename);
   INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
    VALUES (@role_id, @username, @email, @password, @mobile_number, SYSDATETIME(), SYSDATETIME());
  END
 END
END
EXEC dbo.RegisterUser 'admin', 'Sai', 'si@acc.com', 'sai', '8888888888', 'Chennai';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.DeleteUser (@email nvarchar(50))
AS
DECLARE @emailExist varchar(50);
BEGIN
 EXEC dbo.CheckEmailExists @email, @emailExist out;
 IF @emailExist IS NOT NULL
  DELETE FROM dbo.users WHERE email_address = @email
END
EXEC dbo.DeleteUser 'si@acc.com';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddToWishList(@user_name nvarchar(50), @title_name nvarchar(50))
AS
BEGIN
	DECLARE @user_id INT;
	DECLARE @title_id INT;

	SET @user_id=(Select user_id from dbo.users where dbo.users.user_name=@user_name);
	SET @title_id=(Select title_id from dbo.title where dbo.title.title_name=@title_name);

	INSERT INTO dbo.wishlist(user_id,title_id,created_on,last_updated)
		VALUES(@user_id,@title_id,SYSDATETIME(),SYSDATETIME());
END

EXEC dbo.AddToWishList @title_name = 'HP', @user_name='Abhishek';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.CheckIfWishlistEntryExists(@user_name nvarchar(50), @title_name nvarchar(50))
AS
BEGIN
	DECLARE @user_id INT;
	DECLARE @title_id INT;

	SET @user_id=(Select user_id from dbo.users where dbo.users.user_name=@user_name);
	SET @title_id=(Select title_id from dbo.title where dbo.title.title_name=@title_name);

	Select Count(*) as result_count from dbo.wishlist where dbo.wishlist.user_id=@user_id AND dbo.wishlist.title_id=@title_id;
END

EXEC dbo.CheckIfWishlistEntryExists @title_name = 'HP', @user_name='Abhishek';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddNotification(@type int,@user_name VARCHAR(50),@message VARCHAR(50),@status int)
AS
BEGIN
	DECLARE @user_id int;
	
	SET @user_id=(Select user_id from dbo.users where dbo.users.user_name=@user_name);

	-- to denote everyone, use reserved user_id=1
	IF(@user_name='All')
	begin
		SET @user_id=1;
	end

	Insert into dbo.notifications (notification_type, user_id, notification_status, notification_message, created_on, last_updated)
		values(@type, @user_id, @status, @message, SYSDATETIME(), SYSDATETIME());
END

EXEC dbo.AddNotification @user_name='Abhishek',@type=1,@message='Hello World',@status=0;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.CheckIfNotificationExists(@user_name VARCHAR(50), @notification_type int,@message VARCHAR(50))
AS
BEGIN
	DECLARE @user_id int;
	
	SET @user_id=(Select user_id from dbo.users where dbo.users.user_name=@user_name);

	Select Count(*) as result_count from dbo.notifications where dbo.notifications.user_id=@user_id 
				AND dbo.notifications.notification_type=@notification_type
				AND dbo.notifications.notification_message=@message; 

END

EXEC dbo.CheckIfNotificationExists @user_name='Abhishek',@notification_type=1,@message='Hello World';

------------------------------------------------------------------------------------------------------------------------------------------

-- for return date due notifications
CREATE OR ALTER PROCEDURE dbo.GetAllBookReturnDateDueRecords
AS
BEGIN
	Select dbo.users.user_name, dbo.title.title_name, dbo.rental.return_date from dbo.rental
		join dbo.book on dbo.rental.book_id=dbo.book.book_id
		join dbo.users on dbo.rental.user_id=dbo.users.user_id
		join dbo.title on dbo.book.title_id=dbo.title.title_id 
		where DATEADD(DAY,-1,dbo.rental.return_date)<=SYSDATETIME();  

END

EXEC dbo.GetAllBookReturnDateDueRecords;

------------------------------------------------------------------------------------------------------------------------------------------

-- for wishlist notifications
CREATE OR ALTER PROCEDURE dbo.GetAllBooksInWishlistAvailableRecords
AS
BEGIN
	Select dbo.users.user_name,dbo.title.title_name from dbo.wishlist
		join dbo.title on dbo.wishlist.title_id=dbo.title.title_id
		join dbo.users on dbo.wishlist.user_id=dbo.users.user_id
		where dbo.title.quantity>0;

END

EXEC dbo.GetAllBooksInWishlistAvailableRecords;

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.GetUserDetailsForLoginValidation(@email_address VARCHAR(50))
AS
BEGIN

	Select dbo.users.password from dbo.users where dbo.users.email_address=@email_address;

END

EXEC dbo.GetUserDetailsForLoginValidation @email_address='abhishek@acc.co';

------------------------------------------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.AddNewUserRegistration(@name VARCHAR(50),@email VARCHAR(50),@password VARCHAR(50),@mobile VARCHAR(50),@residential_address VARCHAR(100))
AS
BEGIN
	INSERT INTO dbo.users (user_name,email_address,password,mobile_number,residential_address,created_on,last_updated)
		VALUES(@name,@email,@password,@mobile,@residential_address,SYSDATETIME(),SYSDATETIME());
END

EXEC dbo.AddNewUserRegistration @name='NewUser1',@email='NewEmail',@password='HelloWorld',@mobile='1234598765',@residential_address='NewAddress';

------------------------------------------------------------------------------------------------------------------------------------------
-- for validation before registration
CREATE OR ALTER PROCEDURE dbo.CheckIfEmailAddressAlreadyExists(@email_address VARCHAR(50))
AS
BEGIN
	SELECT Count(*) as count from dbo.users where dbo.users.email_address=@email_address;
END

EXEC dbo.CheckIfEmailAddressAlreadyExists @email_address='NewEmaidsdl';

