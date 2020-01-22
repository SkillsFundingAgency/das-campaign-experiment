CREATE TABLE [dbo].[CitizenApplicationEvents](
	[Id] [uniqueidentifier] NOT NULL,
	[CitizenFlag] [bit] NOT NULL,
	[CandidateFlag] [bit] NOT NULL,
	[CandidateEmailAddress] [nvarchar](250) NOT NULL,
	[CandidateId] [nvarchar](250) NOT NULL,
	[CandidateFirstName] [nvarchar](250) NOT NULL,
	[CandidateSurname] [nvarchar](250) NOT NULL,
	[CandidateDetailsChangeDate] [datetime2](7) NULL,
	[CandidateAccountDeletionDate] [datetime2](7) NULL,
	[ApplicationId] [nvarchar](250) NULL,
	[ApplicationStartDate] [datetime2](7) NULL,
	[ApplicationSubmitDate] [datetime2](7) NULL,
	[VacancyLevel] [int] NOT NULL,
	[VacancyTitle] [nvarchar](250) NOT NULL,
	[VacancyReference] [nvarchar](250) NOT NULL,
	[VacancyCloseDate] [datetime2](7) NOT NULL,
	[EmailCampaignName] [nvarchar](250) NULL,
	[EmailSendDate] [datetime2](7) NULL,
	[Ethnicity] [int] NOT NULL,
	[EventType] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CitizenApplicationEvents] ADD  CONSTRAINT [DF_Citizens_Id]  DEFAULT (newid()) FOR [Id]
GO


