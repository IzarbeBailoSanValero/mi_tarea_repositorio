-- Crear la base de datos
CREATE DATABASE RestauranteDB;


-- Ver bases de datos existentes
SELECT name FROM sys.databases;


-- Usar la base de datos
USE RestauranteDB;


-- Crear tabla PlatoPrincipal
CREATE TABLE PlatoPrincipal (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Ingredientes TEXT NOT NULL
);

-- Insertar datos en PlatoPrincipal
INSERT INTO PlatoPrincipal (Nombre, Precio, Ingredientes)
VALUES 
('Plato combinado', 12.50, 'Pollo, patatas, tomate'),
('Plato vegetariano', 10.00, 'Tofu, verduras, arroz');


-- comprobar PlatoPrincipal
SELECT * FROM PlatoPrincipal;


-- Crear tabla Bebida
CREATE TABLE Bebida (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    EsAlcoholica BIT NOT NULL
);


-- Insertar datos en Bebida
INSERT INTO Bebida (Nombre, Precio, EsAlcoholica)
VALUES 
('Agua mineral', 1.20, 0),         
('Cerveza artesanal', 3.50, 1),    
('Zumo de naranja', 2.00, 0);   


-- comprobar Bebida
SELECT * FROM Bebida;


-- Crear tabla Postre
CREATE TABLE Postre (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    Calorias INT NOT NULL CHECK (Calorias >= 0)
);


-- Insertar datos en Postre
INSERT INTO Postre (Nombre, Precio, Calorias)
VALUES 
('Tarta de queso', 3.50, 420),
('Fruta fresca', 2.00, 90),
('Helado de vainilla', 2.75, 250);


-- comprobar Postre
SELECT * FROM Postre;


-- Crear tabla Combo
CREATE TABLE Combo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL CHECK (Precio >= 0),
    PlatoPrincipalId INT NOT NULL,
    BebidaId INT NOT NULL,
    PostreId INT NOT NULL,

    -- Claves foráneas (asumiendo que existen estas tablas)
    FOREIGN KEY (PlatoPrincipalId) REFERENCES PlatoPrincipal(Id),
    FOREIGN KEY (BebidaId) REFERENCES Bebida(Id),
    FOREIGN KEY (PostreId) REFERENCES Postre(Id)
);


-- Insertar datos en Combo 
INSERT INTO Combo (Nombre, Precio, PlatoPrincipalId, BebidaId, PostreId)
VALUES 
('Combo Vegetariano', 10.50, 2, 1, 1),
('Combo Ejecutivo', 15.00, 1, 2, 2),
('Combo Infantil', 8.75, 1, 3, 3);

-- comprobar Combo
SELECT * FROM Combo;

-- FALTA TABLA DE PEDIDO 
