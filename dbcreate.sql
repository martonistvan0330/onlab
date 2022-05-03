BEGIN TRY

	BEGIN TRANSACTION

	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].OrderItem') AND type in (N'U'))
		drop table OrderItem
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Order') AND type in (N'U'))
		drop table [Order]
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].CartItem') AND type in (N'U'))
		drop table CartItem
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Cart') AND type in (N'U'))
		drop table Cart
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].ProductImage') AND type in (N'U'))
		drop table ProductImage
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].ProductStock') AND type in (N'U'))
		drop table ProductStock
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Product') AND type in (N'U'))
		drop table Product
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Size') AND type in (N'U'))
		drop table Size
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Customer') AND type in (N'U'))
		drop table Customer
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].ShippingInfo') AND type in (N'U'))
		drop table ShippingInfo
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].PaymentInfo') AND type in (N'U'))
		drop table PaymentInfo
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].ShippingMethod') AND type in (N'U'))
		drop table ShippingMethod
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].PaymentMethod') AND type in (N'U'))
		drop table PaymentMethod
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].AddressInfo') AND type in (N'U'))
		drop table [AddressInfo]
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Address') AND type in (N'U'))
		drop table [Address]
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Category') AND type in (N'U'))
		drop table Category
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].VAT') AND type in (N'U'))
		drop table VAT
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Status') AND type in (N'U'))
		drop table [Status]
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].Session') AND type in (N'U'))
		drop table [Session]
	IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].User') AND type in (N'U'))
		drop table [User]

	--*****************************************************************
	-- Create tables
	--*****************************************************************

	create table [VAT]
	( 
		ID int identity primary key,
		Percentage int,
	)

	create table [Status]
	(
		ID int identity primary key,
		Name nvarchar(20),
	)

	create table [Size]
	(
		ID int identity primary key,
		Name nvarchar(10),
	)

	create table [Category]
	(
		ID int identity primary key,
		Name nvarchar(50),
		ParentCategoryID int references Category(ID),
	)

	create table [Product]
	(
		ID int identity primary key,
		Name nvarchar(50),
		Price float,
		VATID int references VAT(ID),
		CategoryID int references Category(ID),
	)

	create table [ProductStock]
	(
		ID int identity primary key,
		ProductID int references Product(ID),
		SizeID int references Size(ID),
		Stock int,
	)

	create table [ProductImage]
	(
		ID int identity primary key,
		ProductID int references Product(ID),
		ImageSource nvarchar(100),
		MainImage bit,
	)

	create table [Address]
	(
		ID int identity primary key,
		Country nvarchar(50),
		Region nvarchar(50),
		ZipCode varchar(10),
		City nvarchar(50),
		Street nvarchar(50),

		CONSTRAINT UQ_ADDRESS UNIQUE(Country, Region, ZipCode, City, Street)
	)

	create table [AddressInfo]
	(
		ID int identity primary key,
		FirstName nvarchar(50),
		LastName nvarchar(50),
		PhoneNumber varchar(20),
		AddressID int references Address(ID),

		CONSTRAINT UQ_ADDRESS_INFO UNIQUE(FirstName, LastName, PhoneNumber, AddressID)
	)

	create table [ShippingMethod]
	(
		ID int identity primary key,
		Method nvarchar(50),

		CONSTRAINT UQ_SHIPPING_METHOD UNIQUE(Method)
	)

	create table [ShippingInfo]
	(
		ID int identity primary key,
		ShippingMethodID int references ShippingMethod(ID),
		ShippingAddressInfoID int references AddressInfo(ID),
	)

	
	create table [PaymentMethod]
	(
		ID int identity primary key,
		Method nvarchar(50),
		Deadline int,

		CONSTRAINT UQ_PAYMENT_METHOD UNIQUE(Method)
	)

	create table [PaymentInfo]
	(
		ID int identity primary key,
		PaymentMethodID int references PaymentMethod(ID),
		BillingAddressInfoID int references AddressInfo(ID),
	)

	create table [User]
	(
		ID int identity primary key,
		[Email] varchar(50),
		[Username] nvarchar(50),
		[Password] nvarchar(50),

		CONSTRAINT UQ_USERNAME UNIQUE(Username)
	)

	create table [Session]
	(
		ID int identity primary key,
		UserID int references [User](ID),
		SessionID varchar(50),
		Active bit,
	)

	create table [Customer]
	(
		ID int identity primary key,
		UserID nvarchar(450),
		Name nvarchar(50),
		ShippingInfoID int references ShippingInfo(ID),
		PaymentInfoID int references PaymentInfo(ID),
		MainCustomer bit,
	)

	create table [Cart]
	(
		ID int identity primary key,
		UserID nvarchar(450),
	)

	create table [CartItem]
	(
		ID int identity primary key,
		Amount int,
		CartID int references Cart(ID),
		ProductID int references Product(ID),
		SizeID int references Size(ID),
	)

	create table [Order]
	(
		ID int identity primary key,
		[Date] datetime,
		Deadline datetime,
		StatusID int references Status(ID),
		CustomerID int references Customer(ID),
	)

	create table [OrderItem]
	(
		ID int identity primary key,
		Amount int,
		Price float,
		OrderID int references [Order](ID),
		ProductID int references Product(ID),
		StatusID int references Status(ID),
		SizeID int references Size(ID),
	)


	--*****************************************************************
	-- Insert sample data
	--*****************************************************************

	SET IDENTITY_INSERT VAT ON
	insert into VAT(id, Percentage) values (1,0);
	insert into VAT(id, Percentage) values (2,5);
	insert into VAT(id, Percentage) values (3,18);
	insert into VAT(id, Percentage) values (4,27);
	SET IDENTITY_INSERT VAT OFF

	SET IDENTITY_INSERT Status ON
	insert into Status (id, Name) values (1,'New');
	insert into Status (id, Name) values (2,'Processing');
	insert into Status (id, Name) values (3,'Packaged');
	insert into Status (id, Name) values (4,'In transit');
	insert into Status (id, Name) values (5,'Delivered');
	insert into Status (id, Name) values (6,'Cancelled');
	SET IDENTITY_INSERT Status OFF

	SET IDENTITY_INSERT Size ON
	insert into Size (id, Name) values (1,'XS');
	insert into Size (id, Name) values (2,'S');
	insert into Size (id, Name) values (3,'M');
	insert into Size (id, Name) values (4,'L');
	insert into Size (id, Name) values (5,'XL');
	insert into Size (id, Name) values (6,'XXL');
	insert into Size (id, Name) values (7,'No Size');
	SET IDENTITY_INSERT Size OFF

	SET IDENTITY_INSERT Category ON
	insert into Category (id, Name, ParentCategoryID) values (1, 'Shirts', NULL);
	insert into Category (id, Name, ParentCategoryID) values (2, 'Hoodies & Sweatshirts', NULL);
	insert into Category (id, Name, ParentCategoryID) values (3, 'Pants & Shorts', NULL);
	insert into Category (id, Name, ParentCategoryID) values (4, 'Accessories', NULL);
	insert into Category (id, Name, ParentCategoryID) values (5, 'T-Shirts', 1);
	insert into Category (id, Name, ParentCategoryID) values (6, 'Long Sleeves', 1);
	insert into Category (id, Name, ParentCategoryID) values (7, 'Tanks', 1);
	insert into Category (id, Name, ParentCategoryID) values (8, 'Jerseys', 1);
	insert into Category (id, Name, ParentCategoryID) values (9, 'V-Necks', 1);
	insert into Category (id, Name, ParentCategoryID) values (10, 'Crewnecks', 2);
	insert into Category (id, Name, ParentCategoryID) values (11, 'Hoodies', 2);
	insert into Category (id, Name, ParentCategoryID) values (12, 'Sweaters', 2);
	insert into Category (id, Name, ParentCategoryID) values (13, 'Shorts', 3);
	insert into Category (id, Name, ParentCategoryID) values (14, 'Sweatpants', 3);
	insert into Category (id, Name, ParentCategoryID) values (15, 'Leggings', 3);
	insert into Category (id, Name, ParentCategoryID) values (16, 'Jeans', 3);
	insert into Category (id, Name, ParentCategoryID) values (17, 'Sunglasses', 4);
	insert into Category (id, Name, ParentCategoryID) values (18, 'Lighters', 4);
	insert into Category (id, Name, ParentCategoryID) values (19, 'Bandanas', 4);
	insert into Category (id, Name, ParentCategoryID) values (20, 'Wallets', 4);
	insert into Category (id, Name, ParentCategoryID) values (21, 'Hats', 4);
	SET IDENTITY_INSERT Category OFF

	SET IDENTITY_INSERT Product ON
	insert into Product (id, Name, Price, VATID, CategoryID) values (1, 'T-Shirt 1', 1234, 4, 5);
	insert into Product (id, Name, Price, VATID, CategoryID) values (2, 'T-Shirt 2', 2345, 4, 5);
	insert into Product (id, Name, Price, VATID, CategoryID) values (3, 'Long Sleeve 1', 5432, 4, 6);
	insert into Product (id, Name, Price, VATID, CategoryID) values (4, 'Jersey 1', 3456, 4, 8);
	insert into Product (id, Name, Price, VATID, CategoryID) values (5, 'Crewneck 1', 4567, 4, 10);
	insert into Product (id, Name, Price, VATID, CategoryID) values (6, 'Hoodie 1', 109876, 4, 11);
	insert into Product (id, Name, Price, VATID, CategoryID) values (7, 'Hoodie 2', 6789, 4, 11);
	insert into Product (id, Name, Price, VATID, CategoryID) values (8, 'Hoodie 3', 7890, 4, 11);
	insert into Product (id, Name, Price, VATID, CategoryID) values (9, 'Sweater 1', 7654, 4, 12);
	insert into Product (id, Name, Price, VATID, CategoryID) values (10, 'Shorts 1', 4765, 4, 13);
	insert into Product (id, Name, Price, VATID, CategoryID) values (11, 'Shorts 2', 6412, 4, 13);
	insert into Product (id, Name, Price, VATID, CategoryID) values (12, 'Leggings 1', 5621, 4, 15);
	insert into Product (id, Name, Price, VATID, CategoryID) values (13, 'Jeans 1', 8675, 4, 16);
	insert into Product (id, Name, Price, VATID, CategoryID) values (14, 'Sunglasses 1', 2000, 4, 17);
	insert into Product (id, Name, Price, VATID, CategoryID) values (15, 'Bandana 1', 2400, 4, 19);
	insert into Product (id, Name, Price, VATID, CategoryID) values (16, 'Bandana 2', 1200, 4, 19);
	insert into Product (id, Name, Price, VATID, CategoryID) values (17, 'Hat 1', 3000, 4, 21);
	SET IDENTITY_INSERT Product OFF

	SET IDENTITY_INSERT ProductImage ON
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (1, 1, 'images/shirt.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (2, 2, 'images/shirt.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (3, 3, 'images/long_sleeve.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (4, 4, 'images/jersey.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (5, 5, 'images/crewneck.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (6, 6, 'images/hoodie.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (7, 7, 'images/hoodie.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (8, 8, 'images/hoodie.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (9, 9, 'images/sweater.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (10, 10, 'images/shorts.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (11, 11, 'images/shorts.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (12, 12, 'images/leggings.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (13, 13, 'images/jeans.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (14, 14, 'images/sunglasses.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (15, 15, 'images/bandana.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (16, 16, 'images/bandana.png', 1);
	insert into ProductImage (id, ProductID, ImageSource, MainImage) values (17, 17, 'images/hat.png', 1);
	SET IDENTITY_INSERT ProductImage OFF

	SET IDENTITY_INSERT ProductStock ON
	insert into ProductStock (id, ProductID, SizeID, Stock) values (1, 1, 1, 3);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (2, 1, 2, 4);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (3, 1, 3, 4);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (4, 1, 4, 5);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (5, 1, 5, 2);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (6, 1, 6, 0);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (7, 2, 1, 2);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (8, 2, 2, 3);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (9, 2, 3, 5);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (10, 2, 4, 5);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (11, 2, 5, 2);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (12, 2, 6, 1);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (13, 7, 1, 3);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (14, 7, 2, 3);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (15, 7, 3, 0);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (16, 7, 4, 3);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (17, 7, 5, 2);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (18, 7, 6, 3);
	insert into ProductStock (id, ProductID, SizeID, Stock) values (19, 16, 7, 7);
	SET IDENTITY_INSERT ProductStock OFF

	SET IDENTITY_INSERT Address ON
	insert into Address (id, Country, Region, ZipCode, City, Street)
		values (1, 'Hungary', 'Pest megye','2621', 'Verőce', 'Árpád út 38.');
	insert into Address (id, Country, Region, ZipCode, City, Street)
		values (2, 'Hungary', 'Baranya megye', '7800', 'Siklós', 'Hajdu Imre utca 3/A 1. em. 4.');
	insert into Address (id, Country, Region, ZipCode, City, Street)
		values (3, 'Hungary', 'Baranya megye', '7623', 'Pécs', 'Petőfi Sándor utca 50.');
	SET IDENTITY_INSERT Address OFF

	SET IDENTITY_INSERT AddressInfo ON
	insert into AddressInfo (id, FirstName, LastName, PhoneNumber, AddressID)
		values (1, 'Jakab', 'Gipsz', '+36 20 123 4567', 1);
	insert into AddressInfo (id, FirstName, LastName, PhoneNumber, AddressID)
		values (2, 'János', 'Kovács', '+36 30 987 6543', 2);
	insert into AddressInfo (id, FirstName, LastName, PhoneNumber, AddressID)
		values (3, 'János', 'Kovács', '+36 30 987 6543', 3);
	SET IDENTITY_INSERT AddressInfo OFF

	SET IDENTITY_INSERT PaymentMethod ON
	insert into PaymentMethod (id, Method, Deadline) values (1,'Cash', 0);
	insert into PaymentMethod (id, Method, Deadline) values (2,'Credit card', 0);
	insert into PaymentMethod (id, Method, Deadline) values (3,'Paypal', 0);
	SET IDENTITY_INSERT PaymentMethod OFF

	SET IDENTITY_INSERT ShippingMethod ON
	insert into ShippingMethod (id, Method) values (1, 'Shipping');
	insert into ShippingMethod (id, Method) values (2,'Pick Up');
	insert into ShippingMethod (id, Method) values (3,'Foxpost');
	SET IDENTITY_INSERT ShippingMethod OFF

	SET IDENTITY_INSERT PaymentInfo ON
	insert into PaymentInfo (id, PaymentMethodID, BillingAddressInfoID) values (1, 2, 3);
	insert into PaymentInfo (id, PaymentMethodID, BillingAddressInfoID) values (2, 2, 2);
	SET IDENTITY_INSERT PaymentInfo OFF

	SET IDENTITY_INSERT ShippingInfo ON
	insert into ShippingInfo (id, ShippingMethodID, ShippingAddressInfoID) values (1, 1, 3);
	insert into ShippingInfo (id, ShippingMethodID, ShippingAddressInfoID) values (2, 1, 2);
	SET IDENTITY_INSERT ShippingInfo OFF

	SET IDENTITY_INSERT [User] ON
	insert into [User] (id, Email, Username, Password) values (1, 'gipsz.jakab@gmail.com', 'gipszj', 'jelszó');
	insert into [User] (id, Email, Username, Password) values (2, 'kovacs.janos@gmail.com', 'kovacsjanos', 'jani123');
	SET IDENTITY_INSERT [User] OFF

	SET IDENTITY_INSERT Session ON
	insert into Session (id, UserID, SessionID, Active) values (1, 1, 1234, 1);
	insert into Session (id, UserID, SessionID, Active) values (2, 1, 12345, 1);
	insert into Session (id, UserID, SessionID, Active) values (3, 2, 9876, 0);
	insert into Session (id, UserID, SessionID, Active) values (4, 2, 98765, 1);
	SET IDENTITY_INSERT Session OFF

	SET IDENTITY_INSERT Customer ON
	insert into Customer (id, UserID, Name, ShippingInfoID, PaymentInfoID, MainCustomer)
		values (1, 1, 'Gipsz Jakab', 2, 2, 1);
	insert into Customer (id, UserID, Name, ShippingInfoID, PaymentInfoID, MainCustomer) 
		values (2, 2, 'Kovács János', 1, 1, 1);
	SET IDENTITY_INSERT Customer OFF

	SET IDENTITY_INSERT [Order] ON
	insert into [Order] (id, Date, Deadline, Statusid, CustomerID)
		values (1,'2022-01-18','2022-01-30', 2, 1);
	insert into [Order] (id, Date, Deadline, Statusid, CustomerID)
		values (2,'2022-02-13','2022-02-15', 1, 1);
	insert into [Order] (id, Date, Deadline, Statusid, CustomerID)
		values (3,'2022-02-15','2022-02-20', 5, 2);
	SET IDENTITY_INSERT [Order] OFF

	SET IDENTITY_INSERT OrderItem ON
	-- first [Order]
	insert into OrderItem (id, Amount, Price, OrderID, Productid, Statusid, SizeID)
		values (1, 2, 2345, 1, 2, 2, 4);
	insert into OrderItem (id, Amount, Price, OrderID, Productid, Statusid, SizeID)
		values (2, 1, 7890, 1, 8, 2, 4);
	insert into OrderItem (id, Amount, Price, OrderID, Productid, Statusid, SizeID)
		values (3, 1, 3000, 1, 17, 2, 7);
	-- second [Order]
	insert into OrderItem (id, Amount, Price, OrderID, Productid, Statusid, SizeID)
		values (4, 1, 1234, 2, 1, 1, 3);
	insert into OrderItem (id, Amount, Price, OrderID, Productid, Statusid, SizeID)
		values (5, 1, 4765, 2, 10, 1, 3);
	-- third [Order]
	insert into OrderItem (id, Amount, Price, OrderID, Productid, Statusid, SizeID)
		values (6, 5, 1200, 3, 16, 5, 7);
	insert into OrderItem (id, Amount, Price, OrderID, Productid, Statusid, SizeID)
		values (7, 3, 4567, 3, 5, 5, 5);
	SET IDENTITY_INSERT OrderItem OFF

	IF @@Trancount >0
		commit
	
END TRY
BEGIN CATCH
	IF @@Trancount >0
		rollback
	IF  CURSOR_STATUS('global','cur') >= -1
		deallocate cur
	
	SELECT 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() as ErrorState,
        ERROR_PROCEDURE() as ErrorProcedure,
        ERROR_LINE() as ErrorLine,
        ERROR_MESSAGE() as ErrorMessage
END CATCH
