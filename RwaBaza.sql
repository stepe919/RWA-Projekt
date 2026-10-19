


-- ================================
-- RwaLibrary — 6 tablica + seed
-- Pokreni u bazi RwaLibraryLearn.
-- BEZ CREATE DATABASE/USE u predajnoj verziji!
-- ================================

-- 1. GENRE (žanr) — bez FK
CREATE TABLE Genre (
    Id          INT IDENTITY(1,1) NOT NULL,
    Name        NVARCHAR(256) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT PK_Genre PRIMARY KEY (Id)
);
GO

-- 2. LOCATION (lokacija)
CREATE TABLE Location (
    Id      INT IDENTITY(1,1) NOT NULL,
    Name    NVARCHAR(256) NOT NULL,
    Address NVARCHAR(256) NULL,
    CONSTRAINT PK_Location PRIMARY KEY (Id)
);
GO

-- 3. USER — [User] u zagradama jer je rezervirana riječ
CREATE TABLE [User] (
    Id       INT IDENTITY(1,1) NOT NULL,
    Username NVARCHAR(256) NOT NULL,
    Email    NVARCHAR(256) NULL,
    PwdHash  NVARCHAR(256) NOT NULL,
    PwdSalt  NVARCHAR(256) NOT NULL,
    IsAdmin  BIT NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id)
);
GO

-- 4. LOG (zapisnik)
CREATE TABLE Log (
    Id        INT IDENTITY(1,1) NOT NULL,
    Timestamp DATETIME2 NOT NULL,
    Level     INT NOT NULL,
    Message   NVARCHAR(MAX) NOT NULL,
    CONSTRAINT PK_Log PRIMARY KEY (Id)
);
GO

-- 5. BOOK — FK na Genre (veza 1-N)
CREATE TABLE Book (
    Id            INT IDENTITY(1,1) NOT NULL,
    Name          NVARCHAR(256) NOT NULL,
    Description   NVARCHAR(MAX) NULL,
    Author        NVARCHAR(256) NOT NULL,
    Isbn          NVARCHAR(256) NULL,
    PublishedYear INT NULL,
    GenreId       INT NOT NULL,
    CONSTRAINT PK_Book PRIMARY KEY (Id),
    CONSTRAINT FK_Book_Genre FOREIGN KEY (GenreId) REFERENCES Genre(Id)
);
GO

-- 6. BOOKLOCATION — spojna tablica (M-N), FK na Book i Location
CREATE TABLE BookLocation (
    Id         INT IDENTITY(1,1) NOT NULL,
    BookId     INT NOT NULL,
    LocationId INT NOT NULL,
    Quantity   INT NOT NULL,
    CONSTRAINT PK_BookLocation PRIMARY KEY (Id),
    CONSTRAINT FK_BookLocation_Book     FOREIGN KEY (BookId)     REFERENCES Book(Id),
    CONSTRAINT FK_BookLocation_Location FOREIGN KEY (LocationId) REFERENCES Location(Id)
);
GO
