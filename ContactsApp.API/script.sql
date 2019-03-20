IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [contacts] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NULL,
    [Gender] nvarchar(max) NOT NULL,
    [IsFavorite] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    CONSTRAINT [PK_contacts] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [contacts_info] (
    [Id] int NOT NULL,
    [Company] nvarchar(max) NOT NULL,
    [Avatar] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Comments] nvarchar(max) NULL,
    CONSTRAINT [PK_contacts_info] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_contacts_info_contacts_Id] FOREIGN KEY ([Id]) REFERENCES [contacts] ([Id]) ON DELETE CASCADE
);

GO

CREATE UNIQUE INDEX [IX_contacts_Email] ON [contacts] ([Email]) WHERE [Email] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190208130843_initial', N'2.1.4-rtm-31024');

GO

