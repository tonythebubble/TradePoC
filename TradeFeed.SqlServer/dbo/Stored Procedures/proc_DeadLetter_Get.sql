CREATE PROCEDURE [dbo].[proc_DeadLetter_Get]
	@Id	AS Integer  = NULL

AS

	SELECT	 Id, 
			 Body,
			 [Message]
	FROM	 DeadLetter
	WHERE	 Id = ISNULL(@Id, Id)
	ORDER BY Id DESC