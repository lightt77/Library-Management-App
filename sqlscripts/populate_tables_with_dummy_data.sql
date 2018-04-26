INSERT INTO dbo.roles (role_name,created_on,last_updated)
VALUES ('admin',SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.roles (role_name,created_on,last_updated)
VALUES ('user',SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.roles (role_name,created_on,last_updated)
VALUES ('user-admin',SYSDATETIME(),SYSDATETIME());
GO

SELECT * from dbo.roles;

--------------------------------------------------------------------------------------------

INSERT INTO dbo.title(title_name,author,rating,quantity,created_on,last_updated)
VALUES ('HP','JKR',5,5,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.title(title_name,author,rating,quantity,created_on,last_updated)
VALUES ('Goosebumps','R.L Stine',5,5,SYSDATETIME(),SYSDATETIME());

SELECT * from dbo.title;

--------------------------------------------------------------------------------------------

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'Abhishek','abhishek@acc.com','Hello','987651234',SYSDATETIME(),SYSDATETIME());

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'Karthik','karthik@acc.com','World','897651234',SYSDATETIME(),SYSDATETIME());

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (2,'user1','user1@acc.com','HelloWorld','897656764',SYSDATETIME(),SYSDATETIME());

SELECT * from dbo.users;

--------------------------------------------------------------------------------------------

INSERT INTO dbo.book(title_id,created_on,last_updated)
VALUES (1,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.book(title_id,created_on,last_updated)
VALUES (2,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.book(title_id,created_on,last_updated)
VALUES (1,SYSDATETIME(),SYSDATETIME());


SELECT * from dbo.book;

--------------------------------------------------------------------------------------------
INSERT INTO dbo.genre(genre_name, created_on,last_updated)
VALUES ('Horror',SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.genre(genre_name,created_on,last_updated)
VALUES ('Fantasy',SYSDATETIME(),SYSDATETIME());

SELECT * from dbo.genre;

--------------------------------------------------------------------------------------------
INSERT INTO dbo.title_genre_map(title_id, genre_id,created_on,last_updated)
VALUES (1,1,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.title_genre_map(title_id, genre_id,created_on,last_updated)
VALUES (2,2,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.title_genre_map(title_id, genre_id,created_on,last_updated)
VALUES (2,1,SYSDATETIME(),SYSDATETIME());

SELECT * FROM dbo.title_genre_map;

---------------------------------------------------------------------------------------------

INSERT INTO dbo.rental(user_id, book_id,issue_date,return_date,rental_status,last_updated)
VALUES (1,2,SYSDATETIME(),SYSDATETIME(),0,SYSDATETIME());
INSERT INTO dbo.rental(user_id, book_id,issue_date,return_date,rental_status,last_updated)
VALUES (1,1,SYSDATETIME(),SYSDATETIME(),0,SYSDATETIME());

SELECT * FROM dbo.rental; 