CREATE TABLE [dbo].[Brands]
(
	[BrandId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BrandName] VARCHAR(50) NOT NULL
)

CREATE TABLE [dbo].[Fuels]
(
	[FuelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FuelName] VARCHAR(50) NOT NULL
)

CREATE TABLE [dbo].[Transmissions]
(
	[TransmissionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TransmissionName] VARCHAR(50) NOT NULL
)

CREATE TABLE [dbo].[BodyTypes]
(
	[BodyTypeId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BodyTypeName] VARCHAR(50) NOT NULL
)

CREATE TABLE [dbo].[Tractions]
(
	[TractionId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TractionName] VARCHAR(50) NOT NULL
)

CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL,
    [LastName] VARCHAR(50) NOT NULL,
    [Email] VARCHAR(50) NOT NULL,
    [PhoneNumber] VARCHAR(20) NOT NULL,
    [PasswordHash] VARBINARY(500) NOT NULL,
    [PasswordSalt] VARBINARY(500) NOT NULL
)

CREATE TABLE [dbo].[OperationClaims]
(
	[OperationClaimId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OperationClaimName] VARCHAR(250) NOT NULL
)

CREATE TABLE [dbo].[Models]
(
	[ModelId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BrandId] INT NOT NULL,
    [ModelName] VARCHAR(50) NOT NULL,
    CONSTRAINT [FK_Models_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([BrandId])
)

CREATE TABLE [dbo].[UserOperationClaims]
(
	[UserOperationClaimId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
    [OperationClaimId] INT NOT NULL,
    CONSTRAINT [FK_UserOperationClaims_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]),
    CONSTRAINT [FK_UserOperationClaims_OperationClaimId] FOREIGN KEY ([OperationClaimId]) REFERENCES [dbo].[OperationClaims] ([OperationClaimId])
)

CREATE TABLE [dbo].[Companies]
(
	[CompanyId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
    [CompanyName] VARCHAR(50) NOT NULL,
    [Address] VARCHAR(MAX) NOT NULL,
    [MersisNo] VARCHAR(50) NOT NULL,
    CONSTRAINT [FK_Companies_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
)

CREATE TABLE [dbo].[Customers]
(
	[CustomerId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL,
    [IDNo] VARCHAR(20) NOT NULL,
    [DateOfBirth] DATE NOT NULL,
    CONSTRAINT [FK_Customers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
)

CREATE TABLE [dbo].[Cars]
(
	[CarId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyId] INT NOT NULL,
    [ModelId] INT NOT NULL,
    [FuelId] INT NOT NULL,
    [TransmissionId] INT NOT NULL,
    [BodyTypeId] INT NOT NULL,
    [TractionId] INT NOT NULL,
    [Seats] SMALLINT NOT NULL,
    [EngineSize] SMALLINT NOT NULL,
    [ModelYear] SMALLINT NOT NULL,
    [DailyPrice] DECIMAL NOT NULL,
    [Description] VARCHAR(MAX) NULL,
    CONSTRAINT [FK_Cars_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [FK_Cars_ModelId] FOREIGN KEY ([ModelId]) REFERENCES [dbo].[Models] ([ModelId]),
    CONSTRAINT [FK_Cars_FuelId] FOREIGN KEY ([FuelId]) REFERENCES [dbo].[Fuels] ([FuelId]),
    CONSTRAINT [FK_Cars_TransmissionId] FOREIGN KEY ([TransmissionId]) REFERENCES [dbo].[Transmissions] ([TransmissionId]),
    CONSTRAINT [FK_Cars_BodyTypeId] FOREIGN KEY ([BodyTypeId]) REFERENCES [dbo].[BodyTypes] ([BodyTypeId]),
    CONSTRAINT [FK_Cars_TractionId] FOREIGN KEY ([TractionId]) REFERENCES [dbo].[Tractions] ([TractionId])
)

CREATE TABLE [dbo].[CarImages]
(
	[CarImageId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CarId] INT NOT NULL,
    [ImagePath] VARCHAR(250) NOT NULL,
    CONSTRAINT [FK_CarImages_CarId] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([CarId])
)

CREATE TABLE [dbo].[Rentals]
(
	[RentalId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CarId] INT NOT NULL,
    [CustomerId] INT NOT NULL,
    [RentDate] DATE NOT NULL,
    [ReturnDate] DATE NOT NULL,
    CONSTRAINT [FK_Rentals_CarId] FOREIGN KEY ([CarId]) REFERENCES [dbo].[Cars] ([CarId]),
    CONSTRAINT [FK_Rentals_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId])
)
