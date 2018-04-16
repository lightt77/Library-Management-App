use library;

CREATE TABLE dbo.roles  
   (role_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
    role_name varchar(50) NOT NULL,
	created_on datetime,   
	last_updated datetime 
	)  
GO  

CREATE TABLE dbo.title  
   (title_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
    title_name varchar(50) NOT NULL,  
    author varchar(50) NULL,  
	rating int,
	quantity int NOT NULL,
	price int NULL,
	created_on datetime,   
	last_updated datetime 
	)  
GO

CREATE TABLE dbo.users  
   (user_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
    role_id int NOT NULL,  
    user_name varchar(50) NOT NULL,  
    email_address varchar(100) NOT NULL,
	password varchar(MAX) NOT NULL,
	mobile_number varchar(15) NOT NULL,
	residential_address varchar(100) NULL,
	created_on datetime,   
	last_updated datetime,
	CONSTRAINT users_table_role_id_fkey FOREIGN KEY (role_id)     
    REFERENCES dbo.roles(role_id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE 
	)
GO  

CREATE TABLE dbo.book  
   (book_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
    title_id int NOT NULL,  
	created_on datetime,   
	last_updated datetime 
	CONSTRAINT book_table_title_id_fkey FOREIGN KEY (title_id)     
    REFERENCES dbo.title(title_id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE
	)  
GO

CREATE TABLE dbo.rental 
   (rental_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
	user_id int NOT NULL,
	book_id int NOT NULL,
	issue_date datetime NOT NULL,
	return_date datetime NOT NULL,
	rental_status int NOT NULL,  
	created_on datetime,   
	last_updated datetime 
	CONSTRAINT rental_table_user_id_fkey FOREIGN KEY (user_id)     
    REFERENCES dbo.users(user_id),  
	CONSTRAINT rental_table_book_id_fkey FOREIGN KEY (book_id)     
    REFERENCES dbo.book(book_id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE
	)  
GO

CREATE TABLE dbo.genre  
   (genre_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
    genre_name varchar(50) NOT NULL,
	created_on datetime,   
	last_updated datetime
	)  
GO

CREATE TABLE dbo.title_genre_map  
	(title_id int NOT NULL,  
	genre_id int NOT NULL,
	created_on datetime,   
	last_updated datetime
	CONSTRAINT title_genre_map_table_title_id_fkey FOREIGN KEY (title_id)     
    REFERENCES dbo.title(title_id),
	CONSTRAINT title_genre_map_table_genre_id_fkey FOREIGN KEY (genre_id)     
    REFERENCES dbo.genre(genre_id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE
	)
GO

CREATE TABLE dbo.wishlist  
   (wishlist_id int PRIMARY KEY IDENTITY(1,1) NOT NULL,  
    user_id int NOT NULL,
	title_id int NOT NULL,
	created_on datetime,   
	last_updated datetime
	CONSTRAINT wishlist_table_user_id_fkey FOREIGN KEY (user_id)     
    REFERENCES dbo.users(user_id),
	CONSTRAINT wishlist_table_title_id_fkey FOREIGN KEY (title_id)     
    REFERENCES dbo.title(title_id)     
    ON DELETE CASCADE    
    ON UPDATE CASCADE
	)  
GO 