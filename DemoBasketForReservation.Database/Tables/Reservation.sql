CREATE TABLE [dbo].[Reservation]
(
	[Id] NCHAR(12) NOT NULL, 
	[Date] DATE NOT NULL,
    CONSTRAINT [PK_Reservation] PRIMARY KEY ([Id]),
	CONSTRAINT [CK_Reservation_Date] CHECK ([Date] >= CONVERT(DATE, GETDATE()))
)
