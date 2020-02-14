CREATE PROCEDURE [dbo].[proc_DeadLetter_Create]

    @Body		AS nVarChar(MAX),
	@Message	AS nVarChar(MAX)

AS

	INSERT
	INTO	DeadLetter
		   (Body,  [Message])
	VALUES (@Body, @Message)