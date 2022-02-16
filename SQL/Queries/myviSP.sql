DECLARE @jsondata VARCHAR(MAX),@RES VARCHAR(100) 
SET @jsondata='{"UserId":2,"PlanId":3,"AlternateContactNo":"8770550882","SimtypeId":2,"Status":2,"VIPNumberId":1,"City":"Surat","State":"Gujarat","FlatNo":5,"Country":"India","PincodeNo":"382560"}'
EXECUTE [dbo].[sp_ForOrder] @JSON_TEXT = @jsondata,@RES=@RES out
PRINT @RES

ALTER PROCEDURE sp_ForOrder (@JSON_TEXT VARCHAR(8000),@RES VARCHAR(100) OUT)
WITH RECOMPILE
AS 
DECLARE @AddressId INT,@PortNumberId INT,@PortNumber VARCHAR(15),@VIPNumberId INT,@TotalPrice INT,@PlanId INT,@DefaultSIMPrice INT
DECLARE @JSON varchar(MAX)
SET @JSON=@JSON_TEXT

IF ISJSON(@JSON) = 1
BEGIN
	IF EXISTS(SELECT 1 FROM OPENJSON(@JSON) WHERE [key] = 'VIPNumberId') 
		SET @VIPNumberId = JSON_VALUE(@JSON,'$.VIPNumberId');
	ELSE 
		SET @VIPNumberId = 0
	
	SET @PlanId = JSON_VALUE(@JSON,'$.PlanId');
	
	IF EXISTS(SELECT 1 FROM OPENJSON(@JSON) WHERE [key] = 'PortNumber') 
		SET @PortNumber = JSON_VALUE(@JSON,'$.PortNumber');
	ELSE 
		SET @PortNumber = NULL
		SET @PortNumberId = NULL
	
	SET @DefaultSIMPrice = 50
	SET @TotalPrice = @DefaultSIMPrice+(select Price from [dbo].[Plan] Where Id = @PlanId);
	
	BEGIN
		IF @PortNumber IS NOT NULL
		BEGIN
			--INSERT DATA IN PORTNUMBER TABLE 
			INSERT INTO [dbo].[PortNumber] (Number,SIMTypeId,IsActive,IsDeleted,OnCreated,OnUpdated)
			SELECT Number,SIMTypeId,IsActive=1,IsDeleted=0,OnCreated=GETDATE(),OnUpdated=GETDATE() FROM OPENJSON(@JSON)
				WITH(
					Number VARCHAR(20) '$.PortNumber',
					SIMTypeId INT '$.SimtypeId'
				)
				OPTION (OPTIMIZE FOR ( @JSON UNKNOWN))
			SET @PortNumberId = IDENT_CURRENT('PortNumber');	
		END
	
		IF @VIPNumberId = 0
		BEGIN
			--INSERT DATA INTO ADDRESS TABLE 
			INSERT INTO [dbo].[Address] (City,State,FlatNo,Country,PincodeNo,IsActive,IsDeleted,OnCreated,OnUpdated)
			SELECT City,State,FlatNo,Country,PincodeNo,IsActive=1,IsDeleted=0,OnCreated=GETDATE(),OnUpdated=GETDATE() FROM OPENJSON(@JSON)
				WITH(
					City VARCHAR(20) '$.City',
					State varchar(20) '$.State',
					FlatNo INT '$.FlatNo',
					Country varchar(20) '$.Country',
					PincodeNo VARCHAR(10) '$.PincodeNo'
				)
			OPTION (OPTIMIZE FOR ( @JSON UNKNOWN))
			SET @AddressId = IDENT_CURRENT('Address');
	
			--INSERT DATA IN ORDER TABLE 
			INSERT INTO [dbo].[Order] (UserId,PortNumberId,VIPNumberId,AddessId,AlternateContactNo,SIMTypeId,
									   Quantity,PlanId,TotalPrice,Status,
									   IsActive,IsDeleted,OnCreated,OnUpdated)
			SELECT UserId,PortNumberId=@PortNumberId,VIPNumberId=NULL,AddressId=@AddressId,AlternateContactNo,
				   SIMTypeId,Quantity=1,PlanId,TotalPrice = @TotalPrice,Status,IsActive=1,
				   IsDeleted=0,OnCreated=GETDATE(),OnUpdated=GETDATE() FROM OPENJSON(@JSON)
				WITH(
					UserId INT '$.UserId',
					PlanId INT '$.PlanId',
					AlternateContactNo VARCHAR(20) '$.AlternateContactNo',
					SIMTypeId INT '$.SimtypeId',
					Status INT '$.Status'
				)
				OPTION (OPTIMIZE FOR ( @JSON UNKNOWN))
		END
		ELSE
		BEGIN
			IF NOT EXISTS (SELECT Number FROM [dbo].[VIPNumber] WHERE Id = @VIPNumberId)
			BEGIN
				SET @RES = 'This vip number id does not exist'
			END
			ELSE
			BEGIN
				--INSERT DATA INTO ADDRESS TABLE 
				INSERT INTO [dbo].[Address] (City,State,FlatNo,Country,PincodeNo,IsActive,IsDeleted,OnCreated,OnUpdated)
				SELECT City,State,FlatNo,Country,PincodeNo,IsActive=1,IsDeleted=0,OnCreated=GETDATE(),OnUpdated=GETDATE() FROM OPENJSON(@JSON)
					WITH(
						City VARCHAR(20) '$.City',
						State varchar(20) '$.State',
						FlatNo INT '$.FlatNo',
						Country varchar(20) '$.Country',
						PincodeNo VARCHAR(10) '$.PincodeNo'
					)
				OPTION (OPTIMIZE FOR ( @JSON UNKNOWN))
				SET @AddressId = IDENT_CURRENT('Address');
				print @PortNumberId
				--INSERT DATA IN ORDER TABLE 
				INSERT INTO [dbo].[Order] (UserId,PortNumberId,VIPNumberId,AddessId,AlternateContactNo,SIMTypeId,
										   Quantity,PlanId,TotalPrice,Status,
										   IsActive,IsDeleted,OnCreated,OnUpdated)
				SELECT UserId,PortNumberId=@PortNumberId,VIPNumberId=@VIPNumberId,AddressId=@AddressId,AlternateContactNo,
					   SIMTypeId,Quantity=1,PlanId,TotalPrice = @TotalPrice,Status,IsActive=1,
					   IsDeleted=0,OnCreated=GETDATE(),OnUpdated=GETDATE() FROM OPENJSON(@JSON)
					WITH(
						UserId INT '$.UserId',
						PlanId INT '$.PlanId',
						AlternateContactNo VARCHAR(20) '$.AlternateContactNo',
						SIMTypeId INT '$.SimtypeId',
						Status INT '$.Status'
					)

				--UPDATE DATA IN VIPNUMBER TABLE
				UPDATE [dbo].[VIPNumber]
				SET    IsDeleted = 1,
					   OnUpdated = GETDATE()
				WHERE  id = @VIPNumberId
					--OPTION (OPTIMIZE FOR ( @JSON UNKNOWN))
			END
		END
	END
END

CREATE PROCEDURE sp_Temp (@num int,@res int out)
as
begin
	set @res = @num*@num
end

declare @res int
exec sp_Temp @num=5,@res=@res out
select @res
