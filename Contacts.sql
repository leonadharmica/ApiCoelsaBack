CREATE DATABASE Contacts

CREATE TABLE
Contacts (
    ContactID INT NOT NULL IDENTITY(1,1), 
	FirstName VARCHAR(50) NOT NULL, 
	LastName VARCHAR(50) NOT NULL, 
	Company VARCHAR(50) NOT NULL, 
	Email VARCHAR(50) NOT NULL, 
	PhoneNumber INT NOT NULL
);
