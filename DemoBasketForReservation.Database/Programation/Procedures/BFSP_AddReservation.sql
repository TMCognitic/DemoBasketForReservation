CREATE PROCEDURE [dbo].[BFSP_AddReservation]
	@Date Date,
	@Guests BFT_GuestArray READONLY
AS
BEGIN
	DECLARE @ReservationId NCHAR(12)
	BEGIN TRANSACTION AddResevationTransaction;
	SET @ReservationId = dbo.BFSF_GetNextReservationId(YEAR(@Date));

	INSERT INTO Reservation ([Id], [Date]) VALUES (@ReservationId, @Date);
	INSERT INTO Guest ([LastName], [FirstName], [ReservationId]) SELECT [LastName], [FirstName], @ReservationId FROM @Guests;

	COMMIT TRANSACTION AddResevationTransaction;
END
