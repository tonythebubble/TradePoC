CREATE PROCEDURE [dbo].[proc_Trade_Get]
	@Id			AS Integer  = NULL,
	@ClientId	AS Integer  = NULL,
	@StockId	AS Integer  = NULL

AS

	SELECT	 Id, 
			 StockId,
			 ClientId,
			 Venue,
			 Quantity,
			 Price,
			 BuySell
	FROM	 Trade
	WHERE	 Id			= ISNULL(@Id, Id)				AND
			 StockId	= ISNULL(@StockId,  StockId)	AND
			 ClientId	= ISNULL(@ClientId, ClientId)
	ORDER BY Id DESC