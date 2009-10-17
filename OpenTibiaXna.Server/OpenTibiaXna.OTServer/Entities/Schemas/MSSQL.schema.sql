CREATE DATABASE MSSQL

USE MSSQL

CREATE TABLE [dbo].[Account](
    [AccountId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(25) NOT NULL UNIQUE,
    [Password] VARCHAR(256) NOT NULL
)
GO

CREATE TABLE [dbo].[Player] (
    [PlayerId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [AccountId] INT NOT NULL,
    [GameWorldId] INT NOT NULL,
    [Name] VARCHAR(25) NOT NULL UNIQUE,
    [Gender] SMALLINT NOT NULL DEFAULT 1,
    [Vocation] INT NOT NULL DEFAULT 0,
    [Level] INT NOT NULL DEFAULT 1,
    [MagicLevel] SMALLINT NOT NULL DEFAULT 0,
    [Experience] BIGINT NOT NULL DEFAULT 0,
    [MaxHealth] INT NOT NULL DEFAULT 100,
    [MaxMana] INT NOT NULL DEFAULT 100,
    [Capacity] INT NOT NULL DEFAULT 0,
    [OutfitLookType] SMALLINT NOT NULL DEFAULT 128,
    [OutfitHead] SMALLINT NOT NULL DEFAULT 0,
    [OutfitBody] SMALLINT NOT NULL DEFAULT 0,
    [OutfitLegs] SMALLINT NOT NULL DEFAULT 0,
    [OutfitFeet] SMALLINT NOT NULL DEFAULT 0,
    [OutfitAddons] SMALLINT NOT NULL DEFAULT 0,
    [LocationX] SMALLINT,
    [LocationY] SMALLINT,
    [LocationZ] SMALLINT,
    [Direction] SMALLINT,
    CONSTRAINT [FK_Player_AccountId_Account_AccountId] 
    FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([AccountId]),
    CONSTRAINT [FK_Player_GameWorldId_GameWorld_GameWorldId] 
    FOREIGN KEY ([GameWorldId]) REFERENCES [dbo].[GameWorld] ([GameWorldId])
)
GO

CREATE TABLE [dbo].[GameWorld] (
    [GameWorldId] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [GameWorldName] VARCHAR(25) NOT NULL UNIQUE,
    [GameWorldIp] BINARY(4) NOT NULL UNIQUE,
    [GameWorldPort] SMALLINT NOT NULL
)
GO

CREATE TABLE [dbo].[MapTile] (
    [X] SMALLINT NOT NULL,
    [Y] SMALLINT NOT NULL,
    [Z] SMALLINT NOT NULL,
    [GroundId] SMALLINT NOT NULL,
    CONSTRAINT [PK_MapTile] PRIMARY KEY ([X], [Y], [Z])
)
GO

CREATE TABLE [dbo].[MapItem] (
    [X] SMALLINT NOT NULL,
    [Y] SMALLINT NOT NULL,
    [Z] SMALLINT NOT NULL,
    [StackPosition] SMALLINT NOT NULL,
    [Id] SMALLINT NOT NULL,
    [Extra] SMALLINT NOT NULL,
    CONSTRAINT [PK_MapItem] PRIMARY KEY ([X], [Y], [Z], [StackPosition])
)