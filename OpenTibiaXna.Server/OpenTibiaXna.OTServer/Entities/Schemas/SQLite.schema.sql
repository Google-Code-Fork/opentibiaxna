CREATE TABLE [Account] (
    [AccountId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [Name] TEXT NOT NULL UNIQUE,
    [Password] TEXT NOT NULL
);

CREATE TABLE [Player] (
    [PlayerId] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
    [AccountId] INTEGER NOT NULL,
    [Name] TEXT NOT NULL UNIQUE,
    [Gender] INTEGER NOT NULL DEFAULT 1,
    [Vocation] INTEGER NOT NULL DEFAULT 0,
    [Level] INTEGER NOT NULL DEFAULT 1,
    [MagicLevel] INTEGER NOT NULL DEFAULT 0,
    [Experience] INTEGER NOT NULL DEFAULT 0,
    [MaxHealth] INTEGER NOT NULL DEFAULT 100,
    [MaxMana] INTEGER NOT NULL DEFAULT 100,
    [Capacity] INTEGER NOT NULL DEFAULT 0,
    [OutfitLookType] INTEGER NOT NULL DEFAULT 128,
    [OutfitHead] INTEGER NOT NULL DEFAULT 0,
    [OutfitBody] INTEGER NOT NULL DEFAULT 0,
    [OutfitLegs] INTEGER NOT NULL DEFAULT 0,
    [OutfitFeet] INTEGER NOT NULL DEFAULT 0,
    [OutfitAddons] INTEGER NOT NULL DEFAULT 0,
    [LocationX] INTEGER,
    [LocationY] INTEGER,
    [LocationZ] INTEGER,
    [Direction] INTEGER,
    CONSTRAINT [FK_Player_AccountId_Account_AccountId] 
    FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccountId])
);

CREATE TABLE [MapTile] (
    [X] INTEGER NOT NULL,
    [Y] INTEGER NOT NULL,
    [Z] INTEGER NOT NULL,
    [GroundId] INTEGER NOT NULL,
    CONSTRAINT [PK_MapTile] PRIMARY KEY ([X], [Y], [Z])
);

CREATE TABLE [MapItem] (
    [X] INTEGER NOT NULL,
    [Y] INTEGER NOT NULL,
    [Z] INTEGER NOT NULL,
    [StackPosition] INTEGER NOT NULL,
    [Id] INTEGER NOT NULL,
    [Extra] INTEGER NOT NULL,
    CONSTRAINT [PK_MapItem] PRIMARY KEY ([X], [Y], [Z], [StackPosition])
);

CREATE TABLE [Item] (
    [ItemId] INTEGER PRIMARY KEY NOT NULL,
    [Name] TEXT
);