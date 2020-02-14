CREATE TABLE [dbo].[DeadLetter] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Body]    NVARCHAR (MAX) NOT NULL,
    [Message] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_DeadLetter] PRIMARY KEY CLUSTERED ([Id] ASC)
);

