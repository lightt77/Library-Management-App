use library;

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
INSERT INTO dbo.title(title_name,author,rating,quantity,created_on,last_updated)
VALUES ('LOTR','Tolkien',5,5,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.title(title_name,author,rating,quantity,created_on,last_updated)
VALUES ('CLRS','Cormen',5,5,SYSDATETIME(),SYSDATETIME());

SELECT * from dbo.title;

--------------------------------------------------------------------------------------------
-- first 10 ids reserved for special cases
-- denotes everyone
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (2,'All','all@acc.com','Hello','987651234',SYSDATETIME(),SYSDATETIME());

-- denotes all users
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (2,'All Users','allusers@acc.com','Hello','987651234',SYSDATETIME(),SYSDATETIME());

-- denotes all admins 
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'All Admins','alladmins@acc.com','Hello','987651234',SYSDATETIME(),SYSDATETIME());

-- denotes all admin-users
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (3,'All Admin-users','alladminusers@acc.com','Hello','987651234',SYSDATETIME(),SYSDATETIME());

-- reserved 5
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'reserved slot 5','reservedslot5@acc.com','World','897651234',SYSDATETIME(),SYSDATETIME());

-- reserved 6
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'reserved slot 6','reservedslot6@acc.com','World','897651234',SYSDATETIME(),SYSDATETIME());

-- reserved 7
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (2,'reserved slot 7','reservedslot7@acc.com','World','897651234',SYSDATETIME(),SYSDATETIME());

-- reserved 8
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (2,'reserved slot 8','reservedslot8@acc.com','World','897651234',SYSDATETIME(),SYSDATETIME());

-- reserved 9
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (3,'reserved slot 9','reservedslot9@acc.com','World','897651234',SYSDATETIME(),SYSDATETIME());

-- reserved 10
INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (3,'reserved slot 10','reservedslot10@acc.com','World','897651234',SYSDATETIME(),SYSDATETIME());

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'Abhishek','abhishek@acc.com','HelloWorld','897651234',SYSDATETIME(),SYSDATETIME());

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'Karthik','karthik@acc.com','HelloWorld','897656764',SYSDATETIME(),SYSDATETIME());

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (2,'user1','admin1@acc.com','HelloWorld','897656764',SYSDATETIME(),SYSDATETIME());

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (1,'admin1','user1@acc.com','HelloWorld','897656764',SYSDATETIME(),SYSDATETIME());

INSERT INTO dbo.users (role_id,user_name,email_address,password,mobile_number,created_on,last_updated)
VALUES (3,'useradmin1','adminuser1@acc.com','HelloWorld','897656764',SYSDATETIME(),SYSDATETIME());

SELECT * from dbo.users;

--------------------------------------------------------------------------------------------

INSERT INTO dbo.book(title_id,created_on,last_updated)
VALUES (1,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.book(title_id,created_on,last_updated)
VALUES (2,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.book(title_id,created_on,last_updated)
VALUES (3,SYSDATETIME(),SYSDATETIME());

SELECT * from dbo.book;

--------------------------------------------------------------------------------------------

INSERT INTO dbo.genre(genre_name, created_on,last_updated)
VALUES ('Horror',SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.genre(genre_name,created_on,last_updated)
VALUES ('Fantasy',SYSDATETIME(),SYSDATETIME());

SELECT * from dbo.genre;

--------------------------------------------------------------------------------------------

INSERT INTO dbo.title_genre_map(title_id, genre_id,created_on,last_updated)
VALUES (1,2,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.title_genre_map(title_id, genre_id,created_on,last_updated)
VALUES (2,2,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.title_genre_map(title_id, genre_id,created_on,last_updated)
VALUES (2,1,SYSDATETIME(),SYSDATETIME());

SELECT * FROM dbo.title_genre_map;

---------------------------------------------------------------------------------------------

INSERT INTO dbo.rental(user_id, book_id,issue_date,return_date,rental_status,created_on,last_updated)
VALUES (11,2,SYSDATETIME(),SYSDATETIME(),0,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.rental(user_id, book_id,issue_date,return_date,rental_status,created_on,last_updated)
VALUES (12,1,SYSDATETIME(),SYSDATETIME(),0,SYSDATETIME(),SYSDATETIME());

SELECT * FROM dbo.rental;

---------------------------------------------------------------------------------------------

INSERT INTO dbo.wishlist(user_id, title_id, created_on, last_updated)
VALUES (12,3,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.wishlist(user_id, title_id, created_on, last_updated)
VALUES (11,4,SYSDATETIME(),SYSDATETIME());

select * from dbo.wishlist; 

---------------------------------------------------------------------------------------------

INSERT INTO dbo.notifications(user_id,notification_type,notification_message,notification_status,created_on, last_updated)
VALUES (1,0,'New book arrival dummy notification',0,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.notifications(user_id,notification_type,notification_message,notification_status,created_on, last_updated)
VALUES (11,1,'Book return date due dummy notification',0,SYSDATETIME(),SYSDATETIME());
INSERT INTO dbo.notifications(user_id,notification_type,notification_message,notification_status,created_on, last_updated)
VALUES (12,2,'Book in wishlist available dummy notification',0,SYSDATETIME(),SYSDATETIME());

select * from dbo.notifications; 
