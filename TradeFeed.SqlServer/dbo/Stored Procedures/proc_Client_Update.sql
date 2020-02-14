CREATE PROCEDURE [dbo].[proc_Client_Update]

    @Id		AS Integer,
    @Name	AS nVarChar(50)

AS

	UPDATE	Client
	SET		[Name] = @Name
	WHERE	ID = @ID