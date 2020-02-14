CREATE PROCEDURE [dbo].[proc_Stock_Update]

    @Id		AS Integer,
    @Name	AS nVarChar(50),
	@Type	AS nVarChar(50)

AS

	UPDATE	Stock
	SET		[Name] = @Name,
			[Type] = @Type
	WHERE	ID = @ID