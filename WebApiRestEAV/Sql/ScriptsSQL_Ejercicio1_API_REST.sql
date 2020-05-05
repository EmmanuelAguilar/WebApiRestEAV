--=============================================================================
-- EJERCICIO NO.1 "API REST EN .NET"                                          =
-- Scripts Necesarios														  =
--=============================================================================

-------------------------------------------------------------------------------
-- 1.- Creación de DB PRACTICACENTAURO										  -
-------------------------------------------------------------------------------
CREATE DATABASE PRACTICACENTAURO

-------------------------------------------------------------------------------
-- 2.- Creación de Tabla GPS_DATA											  -
-------------------------------------------------------------------------------
CREATE TABLE GPS_DATA(
						[Id]         [int]      IDENTITY(1,1) NOT NULL,
						[DateSystem] [datetime] NOT NULL,
						[DateEvent]  [datetime] NULL,
						[Latitude]   [float]    NULL,
						[Longitude]  [float]    NULL,
						[Battery]    [int]      NULL,
						[Source]     [int]      NULL,
						[Type]       [int]      NULL
					  )

-------------------------------------------------------------------------------
-- 3. Inserción de datos(ficticios) en GPS_DATA								  -
-------------------------------------------------------------------------------
INSERT INTO GPS_DATA VALUES(GETDATE()-5,  GETDATE()+1, 19.34430381459724,  99.1870792542428,  50, 10, 1)
INSERT INTO GPS_DATA VALUES(GETDATE()-20, GETDATE()+4, 19.3437976567879,   99.18807703600056, 30, 20, 2)
INSERT INTO GPS_DATA VALUES(GETDATE()-15, GETDATE()+3, 19.34430381459724,  99.1870792542428,  50, 30, 3)
INSERT INTO GPS_DATA VALUES(GETDATE()-19, GETDATE()+5, 19.345133910017196, 99.18231565103568, 30, 40, 4)
INSERT INTO GPS_DATA VALUES(GETDATE()-22, GETDATE()+6, 19.348757935688297, 99.18942886933793, 40, 50, 5)
INSERT INTO GPS_DATA VALUES(GETDATE()-32, GETDATE()+4, 19.353353651415247, 99.18720800028149, 85, 60, 6)
INSERT INTO GPS_DATA VALUES(GETDATE()-10, GETDATE()+7, 19.34972972780386,  99.18058830843869, 25, 70, 7)
INSERT INTO GPS_DATA VALUES(GETDATE()-19, GETDATE()+5, 19.341692838970133, 99.1839496521716,  40, 80, 8)
INSERT INTO GPS_DATA VALUES(GETDATE()-20, GETDATE()+6, 19.339364462763612, 99.18732923552518, 75, 90, 9)

-------------------------------------------------------------------------------
-- 4. Creación de función para obtener todos los registros de la tabla        -
--    GPS_DATA o un registro especifico.									  -
-------------------------------------------------------------------------------
USE PRACTICACENTAURO
GO
/*
	OBJECT:			FN_GET_GPS_DATA
	DEVELOPER:		Ing.Emmanuel Aguilar Ventura
	CREATE DATE:	17/04/2020
	DESCRIPTION:	Function that lists a specific record or all records in the GPS_DATA table
*/
CREATE FUNCTION FN_GET_GPS_DATA
(
  @Id  INT = NULL
)

RETURNS @TBL TABLE
(
	Id            INT,
	DateSystem    DATETIME,
	DateEvent     DATETIME NULL,
	Latitude      FLOAT,
	Longitude     FLOAT,
	Battery       INT ,
	[Source]      INT ,
	[Type]        INT
)

AS 
BEGIN

	IF (@Id IS NOT NULL)
		BEGIN 
		         INSERT INTO @TBL SELECT * FROM GPS_DATA WHERE Id = @Id   --specific record
		END
    ELSE 
        BEGIN
                 INSERT INTO @TBL SELECT * FROM GPS_DATA                  --All records
        END

   RETURN

END

-------------------------------------------------------------------------------
-- Test de funcionamiento 													  -
-------------------------------------------------------------------------------
SELECT * FROM FN_GET_GPS_DATA(NULL)   --Todos los registros
SELECT * FROM FN_GET_GPS_DATA(5)      --Registro especifico por ID

-------------------------------------------------------------------------------
-- 4. Creación de SP para Insertar, Editar o Eliminar un registro de la tabla -
--    GPS_DATA																  -
-------------------------------------------------------------------------------
USE PRACTICACENTAURO
GO
/*
	OBJECT:			SP_GPS_DATA
	DEVELOPER:		Ing.Emmanuel Aguilar Ventura
	CREATE DATE:	17/04/2020
	DESCRIPTION:	Stored procedure that Inserts, Edits or Deletes a record from the GPS_DATA table
*/
CREATE PROCEDURE SP_GPS_DATA
(
  @Action     CHAR(2)  ,
  @Id         INT      = NULL,
  @DateSystem DATETIME = NULL,
  @DateEvent  DATETIME = NULL,
  @Latitude   FLOAT    = NULL,
  @Longitude  FLOAT    = NULL,
  @Battery    INT      = NULL,
  @Source     INT      = NULL,
  @Type       INT      = NULL,
  @Result	  VARCHAR(MAX) OUTPUT 
)
AS
BEGIN TRY

  SET @Result = '0';

  IF (@Action = 'IN') --INSERT
  BEGIN 
      IF (@DateSystem IS NOT NULL)
	  BEGIN
			INSERT INTO GPS_DATA VALUES(
										@DateSystem ,
										@DateEvent	,
										@Latitude	,
										@Longitude	,
										@Battery	,
										@Source	    ,
										@Type		
										)
			   SET @Result='1';           --Inserted record
		END
		ELSE
		BEGIN
		       SET @Result='2';           --Record not inserted
		END
  END

  ELSE IF (@Action = 'UP')--UPDATE
  BEGIN 
        DECLARE @EXIST INT;
        SET @EXIST = (SELECT Id FROM GPS_DATA WHERE Id = @Id)
		IF (@EXIST != 0)
		BEGIN

		    UPDATE GPS_DATA SET
			[DateSystem]	   = ISNULL( @DateSystem  ,DateSystem ),
			[DateEvent]		   = ISNULL( @DateEvent   ,DateEvent  ),
			[Latitude]		   = ISNULL( @Latitude    ,Latitude   ),
			[Longitude]		   = ISNULL( @Longitude   ,Longitude  ),
			[Battery]		   = ISNULL( @Battery     ,Battery    ),
			[Source]		   = ISNULL( @Source      ,Source     ),
			[Type]			   = ISNULL( @Type        ,Type       )
			WHERE [Id] = @Id;			
		   SET @Result='1';   --Updated registration
		END
		ELSE
		BEGIN
		SET @Result='2';      --Registration does not exist
		END
        
  END

  ELSE IF (@Action = 'DE')--DELETE
  BEGIN 
       DECLARE @EXIST2 INT;
        SET @EXIST2 = (SELECT Id FROM GPS_DATA WHERE Id = @Id)
		IF (@EXIST2 != 0)
		BEGIN
				DELETE FROM GPS_DATA WHERE Id = @Id
				SET @Result='1';        ---Record deleted
		END
		ELSE
		BEGIN
		        SET @Result='2';        --Registration does not exist
		END
  END
END TRY
 BEGIN CATCH SET @Result='0'; --ERROR
       PRINT ERROR_MESSAGE();
END CATCH
GO
