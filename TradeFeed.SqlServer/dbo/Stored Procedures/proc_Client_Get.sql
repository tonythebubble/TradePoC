CREATE PROCEDURE [dbo].[proc_Client_Get]
	@Id	AS Integer  = NULL

AS

	SELECT	 Id, 
			 [Name]
	FROM	 Client
	WHERE	 Id = ISNULL(@Id, Id)
	ORDER BY [Name]