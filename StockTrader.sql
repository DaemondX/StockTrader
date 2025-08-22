CREATE DATABASE StockTrader
GO

USE StockTrader
GO

CREATE TABLE Users
(
    Id           INT           NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Username     NVARCHAR(50)  UNIQUE NOT NULL,
    Email        NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL,
    DateJoined   DATETIME      NOT NULL
)
GO

CREATE TABLE Accounts
(
    Id              INT   NOT NULL IDENTITY(1,1) PRIMARY KEY,
    AccountHolderId INT   NOT NULL,
    Balance         FLOAT NOT NULL,
    CONSTRAINT FK_Accounts_Users_AccountHolderId FOREIGN KEY (AccountHolderId) REFERENCES Users(Id)
)
GO

CREATE TABLE AssetTransactions
(
    Id                   INT            NOT NULL IDENTITY(1,1) PRIMARY KEY,
    AccountId            INT            NOT NULL,
    IsPurchase           BIT            NOT NULL,
    Asset_Symbol         NVARCHAR(4000) NOT NULL,
    Asset_PricePerShares FLOAT          NOT NULL,
    SharesAmount         INT            NOT NULL,
    DateProcessed        DATETIME       NOT NULL,
    CONSTRAINT FK_AssetTransactions_Accounts_AccountId FOREIGN KEY (AccountId) REFERENCES Accounts(Id)
)
GO