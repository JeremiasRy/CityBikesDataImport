    -- =============================================
-- Author:		Jeremias Ryttäri
-- Create date: 14.10.2022
-- Description:	Get journeys with or without parameters. Executing without any parameters returns first 50 items.
-- =============================================

	CREATE PROCEDURE [dbo].[Sp_GetJourneys]
	    @PageIndex int = 1,
	    @PageSize int = 50,
	    @Month int = null,
	    @DepartureStationId char(3) = NULL,
	    @ReturnStationId char(3) = NULL,
	    @Distance int = NULL,
	    @Duration int = NULL,
	    @OrderBy varchar(50) = NULL,
	    @OrderDirection char(1) = NULL,
	    @DistanceOperator char(4) = 'MORE',
	    @DurationOperator char(4) = 'MORE'
    AS
    BEGIN
    SELECT TOP (@PageSize) *
    FROM	( SELECT    ROW_NUMBER() OVER ( ORDER BY 
											CASE WHEN @OrderDirection = 'A' THEN
												CASE WHEN @OrderBy = 'Distance' THEN Distance
													 WHEN @OrderBy = 'Duration' THEN Duration
													 WHEN @OrderBy = 'Date' THEN Id
													 WHEN @OrderBy IS NULL THEN Id
													 END 
												END ASC,
											CASE WHEN @OrderDirection = 'D' THEN
												CASE WHEN @OrderBy = 'Distance' THEN Distance
													 WHEN @OrderBy = 'Duration' THEN Duration
													 WHEN @OrderBy = 'Date' THEN Id
													 WHEN @OrderBy IS NULL THEN Id
													 END 
												END DESC,
											CASE WHEN @OrderDirection IS NULL THEN Id END) AS RowNum, *
          FROM      journeys
          WHERE     (1=(CASE when @Month is null THEN 1 else 0 end) or MONTH(DepartureDate) = @Month) and
					(1=(CASE when @DepartureStationId is null THEN 1 else 0 end) or @DepartureStationId = DepartureStationId) and
					(1=(CASE when @ReturnStationId is null THEN 1 else 0 end) or @ReturnStationId = ReturnStationId) and
					(1=(CASE when @Distance is null THEN 1 else 0 end) or CASE 
																			WHEN @DistanceOperator = 'MORE' THEN Distance
																			WHEN @DistanceOperator = 'LESS' THEN @Distance
																			END >= 
																		  CASE
																			WHEN @DistanceOperator = 'MORE' THEN @Distance
																			WHEN @DistanceOperator = 'LESS' THEN Distance 
																			END) and 
					(1=(CASE when @Duration is null THEN 1 else 0 end) or CASE 
																			WHEN @DurationOperator = 'MORE' THEN Duration
																			WHEN @DurationOperator = 'LESS' THEN @Duration
																			END >= 
																		  CASE
																			WHEN @DurationOperator = 'MORE' THEN @Duration
																			WHEN @DurationOperator = 'LESS' THEN Duration
																			END)
																			
																			
			
        ) AS result 
    WHERE   RowNum >= @PageSize * @PageIndex - @PageSize 
    ORDER BY RowNum
    OPTION(RECOMPILE)
    END
    
GO


