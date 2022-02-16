DROP PROCEDURE IF EXISTS dbo.sp_GetPlanBySimTypeId
GO
 
create PROCEDURE dbo.sp_GetPlanBySimTypeId(@simTypeId int, @jsonOutput NVARCHAR(MAX) OUTPUT)
 
AS
 
BEGIN
 
SET @jsonOutput = (SELECT p.Id,p.Price,p.Talktime,p.Call,p.Data,p.SMS,p.Validity,p.Connection,p.Benefits FROM [dbo].[Plan] as p
			 INNER JOIN PlanType ON p.PlanTypeId=PlanType.Id 
			 INNER JOIN SIMType ON PlanType.SimTypeId=SIMType.Id
			 WHERE SIMType.Id = @simTypeId
    FOR JSON PATH)
 
END