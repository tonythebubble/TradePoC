CREATE PROCEDURE [dbo].[proc_DeadLetter_Delete]
	@Id	AS Integer

AS

	DELETE
	FROM	 DeadLetter
	WHERE	 Id = @Id