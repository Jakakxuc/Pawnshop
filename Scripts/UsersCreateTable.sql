CREATE TABLE [Users] (
    UUID UNIQUEIDENTIFIER PRIMARY KEY,
    Username NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL
);