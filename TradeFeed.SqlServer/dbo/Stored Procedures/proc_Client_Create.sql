CREATE PROCEDURE [dbo].[proc_Client_Create]

    @Id		AS Integer,
    @Name	AS nVarChar(50)

AS

	INSERT
	INTO	Client
		   (Id,[Name])
	VALUES (@Id, @Name)