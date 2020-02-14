CREATE PROCEDURE [dbo].[proc_Stock_Create]

    @Id		AS Integer,
    @Name	AS nVarChar(50),
	@Type	AS nVarChar(50)

AS

	INSERT
	INTO	Stock
		   (Id, [Name], [Type])
	VALUES (@Id, @Name,  @Type)