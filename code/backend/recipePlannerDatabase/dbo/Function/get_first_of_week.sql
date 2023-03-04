CREATE FUNCTION [dbo].[get_first_of_week]
(
	@date DATE
)
RETURNS Date
AS
BEGIN
	DECLARE @dt DATE = '1905-01-01';
	DECLARE @firstOfWeek date = DATEADD(week, DATEDIFF(WEEK, @dt, @date), @dt)
	RETURN @firstOfWeek
END
