CREATE PROCEDURE [dbo].[proc_Trade_Create]

	@StockId	AS Integer, 
	@ClientId	AS Integer, 
	@Venue		AS nVarChar(50), 
	@Quantity	AS Integer, 
	@Price		AS Decimal(18,4), 
	@BuySell	AS nVarChar(4)
AS

	INSERT
	INTO	Trade
		   (StockId,  ClientId,  Venue,  Quantity,  Price,  BuySell)
	VALUES (@StockId, @ClientId, @Venue, @Quantity, @Price, @BuySell)