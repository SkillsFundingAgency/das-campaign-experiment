CREATE TABLE [dbo].[CitizenApplicationEvents](
	[Id] [uniqueidentifier] NOT NULL,
	[EventDate] [datetime2](7) NOT NULL,
	[EventType] [int] NOT NULL,
	[CandidateId] [nvarchar](250) NOT NULL,
	[CandidateFirstName] [nvarchar](250) NULL,
	[CandidateSurname] [nvarchar](250) NULL,
	[CandidateEmailAddress] [nvarchar](250) NOT NULL,
	[ApplicationId] [nvarchar](250) NULL,
	[VacancyId] [nvarchar](250) NULL,
	[VacancyTitle] [nvarchar](250) NULL,
	[VacancyReference] [nvarchar](250) NULL,
	[VacancyCloseDate] [datetime2](7) NULL,
	
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CitizenApplicationEvents] ADD  CONSTRAINT [DF_Citizens_Id]  DEFAULT (newid()) FOR [Id]
GO


