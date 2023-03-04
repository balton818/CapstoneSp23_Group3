CREATE FUNCTION [dbo].[get_first_of_week]
(
	@date DATE
)
RETURNS DATE
AS
BEGIN
	DECLARE @dt DATE = '1905-01-01';
	DECLARE @firstOfWeek DATE = DATEADD(week, DATEDIFF(WEEK, @dt, @date), @dt)
	RETURN @firstOfWeek
END
