CREATE TABLE [dbo].[Trade] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [StockId]  INT             NOT NULL,
    [ClientId] INT             NOT NULL,
    [Venue]    NVARCHAR (50)   NOT NULL,
    [Quantity] INT             NOT NULL,
    [Price]    DECIMAL (18, 4) NOT NULL,
    [BuySell]  NVARCHAR (4)    NOT NULL,
    CONSTRAINT [PK_Trade] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Trade_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]),
    CONSTRAINT [FK_Trade_Stock] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stock] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Trade_StockId]
    ON [dbo].[Trade]([StockId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Trade_ClientID]
    ON [dbo].[Trade]([ClientId] ASC);

