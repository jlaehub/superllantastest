USE master;
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Superllantas')
BEGIN
    ALTER DATABASE Superllantas SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE Superllantas;
END
GO

CREATE DATABASE Superllantas;
GO

USE Superllantas;
GO

CREATE TABLE Companies (
    companyId INT PRIMARY KEY IDENTITY,
    name NVARCHAR(255),
    address NVARCHAR(255),
    phone NVARCHAR(20)
);

CREATE TABLE Branches (
    branchId INT PRIMARY KEY IDENTITY,
    companyId INT,
    name NVARCHAR(100),
    address NVARCHAR(255),
    phone NVARCHAR(20),
    FOREIGN KEY (companyId) REFERENCES Companies(companyId)
);

CREATE TABLE Users (
    userId INT PRIMARY KEY IDENTITY,
    name NVARCHAR(100),
    lastName NVARCHAR(100),
    phone NVARCHAR(20),
    email NVARCHAR(100) UNIQUE,
    [password] NVARCHAR(255),
    userType NVARCHAR(50) CHECK (userType IN ('salesAdvisor', 'billClerk', 'manager'))
);

CREATE TABLE Customers (
    customerId INT PRIMARY KEY IDENTITY,
    customerType NVARCHAR(50) CHECK (customerType IN ('generalPublic', 'preferred', 'corporate')),
    name NVARCHAR(100),
    email NVARCHAR(100) UNIQUE,
    phone NVARCHAR(20),
    specialDiscount DECIMAL(5,2),
    salesAdvisorId INT,
    UNIQUE(email)
);

CREATE TABLE Sales (
    saleId INT PRIMARY KEY IDENTITY,
    customerId INT,
    branchId INT,
    saleType NVARCHAR(50) CHECK (saleType IN ('credit', 'cash')),
    temporaryDiscount DECIMAL(5,2),
    saleDate DATE,
    FOREIGN KEY (customerId) REFERENCES Customers(customerId),
    FOREIGN KEY (branchId) REFERENCES Branches(branchId)
);

CREATE TABLE Products (
    productId INT PRIMARY KEY IDENTITY,
    name NVARCHAR(100),
    price DECIMAL(10,2),
    branchId INT,
    withholdingTax DECIMAL(5,2),
    salesTax DECIMAL(5,2),
    FOREIGN KEY (branchId) REFERENCES Branches(branchId)
);

ALTER TABLE Customers
ADD CONSTRAINT fk_salesAdvisorId
FOREIGN KEY (salesAdvisorId) REFERENCES Users(userId);




-- Insertar datos en la tabla Companies
INSERT INTO Companies (name, address, phone)
VALUES ('Superllantas', 'Av. Veracruz y 14 de Abril S/N', '6621094005');

-- Insertar datos en la tabla Branches
INSERT INTO Branches (companyId, name, address, phone)
VALUES (1, 'Sucursal Centro', 'Blvd. Hidalgo #100', '662-123-4567'),
       (1, 'Sucursal Norte', 'Av. Zaragoza #200', '662-234-5678'),
       (1, 'Sucursal Sur', 'Av. García #300', '662-345-6789'),
       (1, 'Sucursal Poniente', 'Calle Madero #400', '662-456-7890'),
       (1, 'Sucursal Oriente', 'Blvd. Morelos #500', '662-567-8901');

-- Insertar datos en la tabla Users
INSERT INTO Users (name, lastName, phone, email, [password], userType)
VALUES ('José', 'González', '662-111-1111', 'jose.gonzalez@example.com', 'password123', 'salesAdvisor'),
       ('María', 'Martínez', '662-222-2222', 'maria.martinez@example.com', 'password456', 'billClerk'),
       ('Carlos', 'Hernández', '662-333-3333', 'carlos.hernandez@example.com', 'password789', 'manager'),
       ('Ana', 'López', '662-444-4444', 'ana.lopez@example.com', 'passwordabc', 'salesAdvisor'),
       ('Juan', 'Díaz', '662-555-5555', 'juan.diaz@example.com', 'passwordxyz', 'billClerk');

-- Insertar datos en la tabla Customers
INSERT INTO Customers (customerType, name, email, phone, specialDiscount, salesAdvisorId)
VALUES ('generalPublic', 'Pedro García', 'pedro.garcia@example.com', '662-666-6666', 0.00, 1),
       ('preferred', 'Alejandra Rodríguez', 'alejandra.rodriguez@example.com', '662-777-7777', 5.00, 1),
       ('corporate', 'Grupo Zapata', 'info@grupozapata.com', '662-888-8888', 10.00, 1),
       ('generalPublic', 'Laura Torres', 'laura.torres@example.com', '662-999-9999', 0.00, 1),
       ('corporate', 'Industrias Gutiérrez', 'info@industriasgutierrez.com', '662-000-0000', 15.00, 1);

-- Insertar datos en la tabla Sales
INSERT INTO Sales (customerId, branchId, saleType, temporaryDiscount, saleDate)
VALUES (1, 1, 'credit', 0.00, '2024-05-01'),
       (2, 2, 'cash', 0.00, '2024-05-02'),
       (3, 3, 'credit', 5.00, '2024-05-03'),
       (4, 4, 'cash', 0.00, '2024-05-04'),
       (5, 5, 'credit', 0.00, '2024-05-05');

-- Insertar datos en la tabla Products
INSERT INTO Products (name, price, branchId, withholdingTax, salesTax)
VALUES ('Llantas Todo Terreno', 1000.00, 1, 50.00, 100.00),
       ('Llantas Deportivas', 1200.00, 2, 60.00, 120.00),
       ('Llantas para Automóviles', 900.00, 3, 45.00, 90.00),
       ('Llantas de Carrera', 1500.00, 4, 75.00, 150.00),
       ('Llantas para Camiones', 1300.00, 5, 65.00, 130.00);
SELECT c.customerId, c.name AS customer_name, c.salesAdvisorId, u.name AS salesAdvisor_name
FROM Customers c
INNER JOIN Users u ON c.salesAdvisorId = u.userId;
