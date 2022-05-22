IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Promotion] (
    [Id] nvarchar(13) NOT NULL,
    [Description] nvarchar(30) NOT NULL,
    [Type] nvarchar(1) NOT NULL,
    [Value] float NOT NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Promotion] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Store] (
    [Id] nvarchar(3) NOT NULL,
    [Name] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Promotion_Item] (
    [Id] int NOT NULL IDENTITY,
    [PromotionId] nvarchar(13) NOT NULL,
    [ItemId] nvarchar(8) NOT NULL,
    CONSTRAINT [PK_Promotion_Item] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Promotion_Item_Promotion_PromotionId] FOREIGN KEY ([PromotionId]) REFERENCES [Promotion] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Promotion_Store] (
    [Id] int NOT NULL IDENTITY,
    [PromotionId] nvarchar(13) NOT NULL,
    [StoreId] nvarchar(3) NOT NULL,
    CONSTRAINT [PK_Promotion_Store] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Promotion_Store_Promotion_PromotionId] FOREIGN KEY ([PromotionId]) REFERENCES [Promotion] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Promotion_Store_Store_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Store] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Promotion_Item_PromotionId] ON [Promotion_Item] ([PromotionId]);
GO

CREATE INDEX [IX_Promotion_Store_PromotionId] ON [Promotion_Store] ([PromotionId]);
GO

CREATE INDEX [IX_Promotion_Store_StoreId] ON [Promotion_Store] ([StoreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220522085352_0.0.1_initial_migration', N'6.0.5');
GO

COMMIT;
GO

