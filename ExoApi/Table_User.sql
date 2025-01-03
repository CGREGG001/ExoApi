CREATE DATABASE ExoApi;

USE ExoApi;

CREATE TABLE [User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(255) NOT NULL
);

INSERT INTO [User] (UserName, Email, Password) VALUES ('John', 'john@api.com', 'MonBeauPassword');