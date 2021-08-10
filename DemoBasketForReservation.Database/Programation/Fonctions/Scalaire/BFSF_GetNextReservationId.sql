CREATE FUNCTION [dbo].[BFSF_GetNextReservationId]
(
	@Year INT
)
RETURNS NCHAR(12)
AS
BEGIN
	DECLARE @Number INT;
	SET @Number = COALESCE((SELECT MAX(SUBSTRING(Id, 6, 7)) + 1 FROM Reservation WHERE Id like CONVERT(NCHAR(4), @Year) + N'%'), 1);
	RETURN Convert(NCHAR(4), @Year) + N'-' + RIGHT('0000000' + ltrim(str(@Number)), 7);
END
