-- Create Database
CREATE DATABASE ChatGPTIntegrationDB;
GO

USE ChatGPTIntegrationDB;
GO

-- Create Customer table
CREATE TABLE Customer (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100)
);

-- Create Product table
CREATE TABLE Product (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Price DECIMAL(10,2)
);

-- Create Order table
CREATE TABLE [Order] (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT FOREIGN KEY REFERENCES Customer(CustomerId),
    ProductId INT FOREIGN KEY REFERENCES Product(ProductId),
    Quantity INT,
    OrderDate DATETIME DEFAULT GETDATE()
);

-- Create Settings table
CREATE TABLE Settings (
    SettingId INT IDENTITY(1,1) PRIMARY KEY,
    [Key] NVARCHAR(100),
    [Value] NVARCHAR(500)
);

-- Insert sample settings
INSERT INTO Settings ([Key], [Value]) VALUES ('OpenAI-ApiKey', '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');
