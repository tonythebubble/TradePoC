CREATE TABLE [dbo].[Stock] (
    [Id]   INT           NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    [Type] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED ([Id] ASC)
);

