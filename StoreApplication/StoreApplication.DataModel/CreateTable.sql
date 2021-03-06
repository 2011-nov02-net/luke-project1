
CREATE SCHEMA StoreApp;
GO

-- tables for managing customer/order/location/product

CREATE TABLE StoreApp.Customer (
	CustomerId NVARCHAR(30) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL
);

CREATE TABLE StoreApp.Product (
	ProductId NVARCHAR(30) NOT NULL PRIMARY KEY,
	Name NVARCHAR(30) NOT NULL UNIQUE,
	Price DECIMAL NOT NULL, 
);

CREATE TABLE StoreApp.Location (
	LocationId NVARCHAR(30) NOT NULL PRIMARY KEY,
	Name NVARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE StoreApp.Orders (
	OrderId NVARCHAR(30) NOT NULL PRIMARY KEY,
	LocationId NVARCHAR(30) NOT NULL
		FOREIGN KEY REFERENCES StoreApp.Location (LocationId) ON DELETE CASCADE ON UPDATE CASCADE,
	CustomerId NVARCHAR(30) NOT NULL
		FOREIGN KEY REFERENCES StoreApp.Customer (CustomerId) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE StoreApp.OrderDetails (
	Id NVARCHAR(30) NOT NULL PRIMARY KEY,
	OrderId NVARCHAR(30) NOT NULL
		FOREIGN KEY REFERENCES StoreApp.Orders (OrderId) ON DELETE CASCADE ON UPDATE CASCADE,
	ProductId NVARCHAR(30) NOT NULL
		FOREIGN KEY REFERENCES StoreApp.Product (ProductId) ON DELETE CASCADE ON UPDATE CASCADE,
	Quantity INT NOT NULL CHECK (Quantity <= 10),
	OrderTime DATE NOT NULL
);

CREATE TABLE StoreApp.StoreInventory (
	StoreInventoryId NVARCHAR(30) NOT NULL PRIMARY KEY,
	LocationId NVARCHAR(30) NOT NULL
		FOREIGN KEY REFERENCES StoreApp.Location (LocationId) ON DELETE CASCADE ON UPDATE CASCADE,
	ProductId NVARCHAR(30) NOT NULL
		FOREIGN KEY REFERENCES StoreApp.Product (ProductId) ON DELETE CASCADE ON UPDATE CASCADE,
	Quantity INT NOT NULL CHECK (Quantity >= 0)
);
