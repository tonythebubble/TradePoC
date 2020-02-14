CREATE PROCEDURE proc_Stock_Get
	@Id	AS Integer = NULL

AS

	SELECT	 Id, 
			 [Name],
			 [Type]
	FROM	 Stock
	WHERE	 Id = ISNULL(@Id, Id)
	ORDER BY [Name]