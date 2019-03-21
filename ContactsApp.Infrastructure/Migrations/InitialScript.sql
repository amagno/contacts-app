IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Contacts] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(200) NOT NULL,
    [LastName] nvarchar(200) NOT NULL,
    [Birthday] datetime2 NOT NULL,
    [Email] nvarchar(500) NOT NULL,
    [Gender] nvarchar(1) NOT NULL,
    [IsFavorite] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [Updated] datetime2 NULL,
    CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [ContactsInfos] (
    [Id] int NOT NULL,
    [Company] nvarchar(200) NULL,
    [Avatar] nvarchar(1000) NULL,
    [Address] nvarchar(500) NULL,
    [Phone] nvarchar(100) NULL,
    [Comments] nvarchar(1000) NULL,
    CONSTRAINT [PK_ContactsInfos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContactsInfos_Contacts_Id] FOREIGN KEY ([Id]) REFERENCES [Contacts] ([Id]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_Contacts_Email] ON [Contacts] ([Email]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190321030647_Initial', N'2.2.2-servicing-10034');

GO

