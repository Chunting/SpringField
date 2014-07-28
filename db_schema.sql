IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Membership_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Membership_BasicAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Membership_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Membership_FullAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Membership_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Membership_ReportingAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Personalization_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Personalization_BasicAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Personalization_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Personalization_FullAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Personalization_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Personalization_ReportingAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Profile_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Profile_BasicAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Profile_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Profile_FullAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Profile_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Profile_ReportingAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Roles_BasicAccess' AND type = 'R')
CREATE ROLE [aspnet_Roles_BasicAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Roles_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_Roles_FullAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_Roles_ReportingAccess' AND type = 'R')
CREATE ROLE [aspnet_Roles_ReportingAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'aspnet_WebEvent_FullAccess' AND type = 'R')
CREATE ROLE [aspnet_WebEvent_FullAccess] AUTHORIZATION [dbo]
GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Membership_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Membership_BasicAccess] AUTHORIZATION [aspnet_Membership_BasicAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Membership_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Membership_FullAccess] AUTHORIZATION [aspnet_Membership_FullAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Membership_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Membership_ReportingAccess] AUTHORIZATION [aspnet_Membership_ReportingAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Personalization_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Personalization_BasicAccess] AUTHORIZATION [aspnet_Personalization_BasicAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Personalization_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Personalization_FullAccess] AUTHORIZATION [aspnet_Personalization_FullAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Personalization_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Personalization_ReportingAccess] AUTHORIZATION [aspnet_Personalization_ReportingAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Profile_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Profile_BasicAccess] AUTHORIZATION [aspnet_Profile_BasicAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Profile_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Profile_FullAccess] AUTHORIZATION [aspnet_Profile_FullAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Profile_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Profile_ReportingAccess] AUTHORIZATION [aspnet_Profile_ReportingAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Roles_BasicAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Roles_BasicAccess] AUTHORIZATION [aspnet_Roles_BasicAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Roles_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Roles_FullAccess] AUTHORIZATION [aspnet_Roles_FullAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_Roles_ReportingAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_Roles_ReportingAccess] AUTHORIZATION [aspnet_Roles_ReportingAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'aspnet_WebEvent_FullAccess')
EXEC sys.sp_executesql N'CREATE SCHEMA [aspnet_WebEvent_FullAccess] AUTHORIZATION [aspnet_WebEvent_FullAccess]'

GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'compass')
CREATE USER [compass] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteFeedbackById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Feedback By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteFeedbackById] 
	@FeedbackId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE [dbo].[sf_Feedback] WHERE [FeedbackId] = @FeedbackId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectFeedbackById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Feedback By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectFeedbackById] 
	@FeedbackId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_Feedback] WHERE [FeedbackId] = @FeedbackId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateDecisionMailStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

create PROCEDURE [dbo].[sf_UpdateDecisionMailStatus] 
	  @interviewId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[sf_Interview]
	SET [IsSendMail] = 1
      
 WHERE [interviewId] = @interviewId

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Advisor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Advisor](
	[AdvisorId] [int] NOT NULL,
	[FirstName] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[Organization] [nvarchar](256) NULL,
	[ResearchApproach] [int] NULL,
 CONSTRAINT [PK_Advisor] PRIMARY KEY CLUSTERED 
(
	[AdvisorId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Feedback]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Feedback](
	[FeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[InterviewerId] [uniqueidentifier] NOT NULL,
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[InterviewId] [int] NULL,
	[SuggestionResult] [int] NULL,
	[FeedbackContent] [nvarchar](4000) NULL,
	[InterviewDate] [datetime] NULL,
	[IsComplete] [bit] NOT NULL CONSTRAINT [DF_sf_Feedback_IsComplete]  DEFAULT ((0)),
	[DueDate] [datetime] NOT NULL,
	[InterviewerAlias] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectReferralById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Referral By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectReferralById] 
	@ReferralId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT * FROM [dbo].[sf_Referral] WHERE [ReferralId] = @ReferralId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteReferralById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Referral By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteReferralById] 
	@ReferralId int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM [dbo].[sf_Referral] WHERE [ReferralId] = @ReferralId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_RelatedReferrer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_RelatedReferrer](
	[RelatedReferrerId] [int] IDENTITY(1,1) NOT NULL,
	[ReferralId] [int] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[Gender] [int] NULL CONSTRAINT [DF_sf_RelatedReferrer_Gender]  DEFAULT ((0)),
 CONSTRAINT [PK_sf_RelatedReferrer] PRIMARY KEY CLUSTERED 
(
	[RelatedReferrerId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectReferralByReferrerId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Referral By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectReferralByReferrerId] 
	@ReferrerId uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM [dbo].[sf_Referral] WHERE [ReferrerId] = @ReferrerId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[CommentTime] [datetime] NOT NULL,
	[CommentContent] [nvarchar](4000) NULL,
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[Commenter] [uniqueidentifier] NOT NULL,
	[CommenterAlias] [nvarchar](256) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_EmailTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_EmailTemplate](
	[EmailType] [int] NOT NULL,
	[EmailTemplate] [nvarchar](max) NOT NULL,
	[EmailSubject] [nvarchar](256) NULL,
	[EmailFrom] [nvarchar](256) NULL,
	[EmailTo] [nvarchar](256) NULL,
	[EmailCc] [nvarchar](256) NULL,
	[EmailBcc] [nvarchar](256) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetRecentInterviewIdByApplicant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Hui Yang
-- Create date: 2006-11-14
-- Description:	Get Interview ID
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetRecentInterviewIdByApplicant]
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	SELECT 
		Top (1)
		*
	FROM 
		[dbo].[sf_Interview] 
	WHERE 
		[ApplicantId] = @ApplicantId 	
	Order by
		InterviewId desc
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Document]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Document](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentType] [int] NOT NULL,
	[OriginalName] [nvarchar](256) NOT NULL,
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Interview]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Interview](
	[InterviewId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NULL,
	[InterviewStatus] [int] NULL,
	[EndDate] [datetime] NULL,
	[HiringManagerId] [uniqueidentifier] NOT NULL,
	[HiringManagerResult] [int] NULL,
	[HiringManagerComment] [nvarchar](4000) NULL,
	[GroupManagerId] [uniqueidentifier] NULL,
	[GroupManagerResult] [int] NULL,
	[GroupManagerComment] [nvarchar](4000) NULL,
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[MentorDecisionTime] [datetime] NULL CONSTRAINT [DF_sf_Interview_MentorDecisionTime_1]  DEFAULT ('2000-01-01 00:00:00'),
	[ManagerDecisionTime] [datetime] NULL CONSTRAINT [DF_sf_Interview_ManagerDecisionTime_1]  DEFAULT ('2000-01-01 00:00:00'),
	[IsSendMail] [int] NULL CONSTRAINT [DF_sf_Interview_IsSendMail]  DEFAULT ((0)),
	[CheckInFormId] [int] NULL,
 CONSTRAINT [PK_Interview] PRIMARY KEY CLUSTERED 
(
	[InterviewId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_IsIdentityNumberUsed]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sf_IsIdentityNumberUsed]
	@IdentityNumber nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Count(ApplicantId) FROM [dbo].[sf_ApplicantBasicInfo] WHERE [IdentityNumber] = @IdentityNumber
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_ApplicantBasicInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_ApplicantBasicInfo](
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[Gender] [int] NOT NULL CONSTRAINT [DF_sf_ApplicantBasicInfo_Gender]  DEFAULT ((0)),
	[Nationality] [nvarchar](256) NULL CONSTRAINT [DF_sf_ApplicantBasicInfo_Nationality]  DEFAULT ((0)),
	[IdentityNumber] [nvarchar](256) NULL,
	[Webpage] [nvarchar](256) NULL,
	[Address] [nvarchar](256) NULL,
	[PhoneNumber] [nvarchar](256) NULL,
	[CurrentCity] [nvarchar](256) NULL,
	[CurrentProvince] [nvarchar](256) NULL,
	[CurrentCountry] [nvarchar](256) NULL CONSTRAINT [DF_sf_ApplicantBasicInfo_CurrentCountry]  DEFAULT ((0)),
	[Status] [int] NOT NULL CONSTRAINT [DF_sf_ApplicantBasicInfo_Status]  DEFAULT ((0)),
	[Priority] [int] NOT NULL CONSTRAINT [DF_sf_ApplicantBasicInfo_Priority]  DEFAULT ((0)),
	[ApplicationDate] [datetime] NULL CONSTRAINT [DF_sf_ApplicantBasicInfo_ApplicationDate]  DEFAULT (getdate()),
	[Email] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[ReferralId] [int] NULL CONSTRAINT [DF_sf_ApplicantBasicInfo_ReferralId]  DEFAULT ((0)),
	[NameInChinese] [nvarchar](256) NULL,
 CONSTRAINT [PK_sf_ApplicantBasicInfo] PRIMARY KEY CLUSTERED 
(
	[ApplicantId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_CheckInForm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_CheckInForm](
	[FormId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[PositionId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[InternTypeId] [int] NOT NULL,
	[MentorAlias] [varchar](50) NOT NULL,
	[PreferCheckInDate] [datetime] NOT NULL,
	[PreferLastWorkingDay] [datetime] NOT NULL,
	[AdvisorApproved] [bit] NOT NULL,
	[Comments] [nvarchar](max) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_ApplicantRelatedInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_ApplicantRelatedInfo](
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[PreferredStartDate] [datetime] NOT NULL,
	[SecondaryStartDate] [datetime] NULL,
	[InterestedGroup] [nvarchar](4000) NULL CONSTRAINT [DF_sf_ApplicantRelatedInfo_InterestedGroup]  DEFAULT ((0)),
	[InterestedAreas] [nvarchar](256) NULL,
	[InternshipType] [int] NULL CONSTRAINT [DF_sf_ApplicantRelatedInfo_InternshipType]  DEFAULT ((0)),
	[JobInfoCategory] [nvarchar](256) NULL,
	[JobInfoSource] [nvarchar](256) NULL,
	[JobInfoChannel] [nvarchar](256) NULL,
	[JobInfoDetail] [nvarchar](256) NULL,
	[PreferredPosition] [int] NULL,
	[SpecialProgram] [nvarchar](4000) NULL,
 CONSTRAINT [PK_sf_ApplicantRelatedInfo] PRIMARY KEY CLUSTERED 
(
	[ApplicantId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Referral]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Referral](
	[ReferralId] [int] IDENTITY(1,1) NOT NULL,
	[ReferralType] [int] NOT NULL CONSTRAINT [DF_sf_Referral_ReferralType]  DEFAULT ((0)),
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[ReferrerId] [uniqueidentifier] NOT NULL,
	[ReferredTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Referral] PRIMARY KEY CLUSTERED 
(
	[ReferralId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Email]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Email](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[Priority] [int] NULL,
	[BodyFormat] [int] NULL,
	[From] [nvarchar](256) NOT NULL,
	[To] [nvarchar](256) NOT NULL,
	[Cc] [nvarchar](256) NULL,
	[Bcc] [nvarchar](256) NULL,
	[Subject] [nvarchar](256) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[NextTryTime] [datetime] NULL,
	[NumberOfTries] [int] NULL,
	[IsSend] [int] NOT NULL CONSTRAINT [DF_sf_Email_IsSend]  DEFAULT ((0)),
	[RelatedUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_Favorites]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_Favorites](
	[FavoriteId] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[ApplicantId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[FavoriteId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectApplicantEduBackgroundById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Applicant Education Background By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectApplicantEduBackgroundById]
	   @ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[sf_ApplicantEduBackground] WHERE [ApplicantId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteApplicantEduBackgroundById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Applicant Education Background By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteApplicantEduBackgroundById]
   @ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [dbo].[sf_ApplicantEduBackground] WHERE [ApplicantId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectApplicantRelatedInfoById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Applicant Related Info By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectApplicantRelatedInfoById]
   @ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[sf_ApplicantRelatedInfo] WHERE [ApplicantId] = @ApplicantId
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteApplicantRelatedInfoById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Applicant Related Info By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteApplicantRelatedInfoById]
	@ApplicantId uniqueidentifier
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM [dbo].[sf_ApplicantRelatedInfo] WHERE [ApplicantId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetFeedbackByInterview]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Yuan Chen
-- Create date: 8/5/2006
-- Description:	Get Feedbacks For Interview
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetFeedbackByInterview]
	@InterviewId int
	,@IsComplete	bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[sf_Feedback] WHERE [InterviewId] = @InterviewId AND [IsComplete] = @IsComplete
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_WebEvent_Events]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_WebEvent_Events](
	[EventId] [char](32) NOT NULL,
	[EventTimeUtc] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[EventType] [nvarchar](256) NOT NULL,
	[EventSequence] [decimal](19, 0) NOT NULL,
	[EventOccurrence] [decimal](19, 0) NOT NULL,
	[EventCode] [int] NOT NULL,
	[EventDetailCode] [int] NOT NULL,
	[Message] [nvarchar](1024) NULL,
	[ApplicationPath] [nvarchar](256) NULL,
	[ApplicationVirtualPath] [nvarchar](256) NULL,
	[MachineName] [nvarchar](256) NOT NULL,
	[RequestUrl] [nvarchar](1024) NULL,
	[ExceptionType] [nvarchar](256) NULL,
	[Details] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetUserIdByUserName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 9/5/2006
-- Description:	Get UserId By User Name
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetUserIdByUserName]
	@UserName nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [UserId] FROM [aspnet_Users] WHERE [UserName] = @UserName
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetFullNameByUserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 10/5/2006
-- Description:	Get Full Name By UserId
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetFullNameByUserId]
	@UserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [UserName] FROM [aspnet_Users] WHERE [UserId] = @UserId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetInterviewForSiteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.18
-- Description:	Get Interview For Site User
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetInterviewForSiteUser]
	@HiringManagerId uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
		*
	FROM
		sf_Interview
	LEFT JOIN
		sf_vw_AllApplicants
	ON
		sf_Interview.ApplicantId = sf_vw_AllApplicants.ApplicantId
	WHERE
		HiringManagerId = @HiringManagerId
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetReferredApplicants]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 29/5/2006
-- Description:	Get Referred Applicants
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetReferredApplicants]
	@ReferralType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		sf_vw_AllApplicants.*,
		sf_Referral.ReferralType 
	FROM 
		(sf_vw_AllApplicants LEFT JOIN sf_Referral
		ON sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId)
	WHERE 
		[ReferralType] = @ReferralType
	ORDER BY
		ApplicationDate DESC
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertApplicantRelatedInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Applicant Related Info
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertApplicantRelatedInfo] 
   @ApplicantId uniqueidentifier
   ,@PreferredStartDate datetime
   ,@SecondaryStartDate datetime
   --,@InterestedGroup int
   ,@InterestedGroup nvarchar(4000)
   ,@InternshipType int
   ,@InterestedAreas nvarchar(256)
   --,@InforSource int
   --,@InfoSourceDetail int
   --,@InfoSourceText nvarchar(256)
   ,@JobInfoCategory nvarchar(256)
   ,@JobInfoSource nvarchar(256)
   ,@JobInfoChannel nvarchar(256)
   ,@JobInfoDetail nvarchar(256)
   ,@PreferredPosition int
   ,@SpecialProgram nvarchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_ApplicantRelatedInfo]
           ([ApplicantId]
           ,[PreferredStartDate]
           ,[SecondaryStartDate]
           ,[InterestedGroup]
           ,[InternshipType]
		   ,[InterestedAreas]
           --,[InforSource]
           --,[InfoSourceDetail]
           --,[InfoSourceText])
		   ,JobInfoCategory
		   ,JobInfoSource
		   ,JobInfoChannel
		   ,JobInfoDetail
		   ,PreferredPosition
		   ,SpecialProgram)
VALUES
           (@ApplicantId
           ,@PreferredStartDate
           ,@SecondaryStartDate
           ,@InterestedGroup
           ,@InternshipType
		   ,@InterestedAreas
           --,@InforSource
           --,@InfoSourceDetail
           --,@InfoSourceText
		   ,@JobInfoCategory
		   ,@JobInfoSource 
		   ,@JobInfoChannel
		   ,@JobInfoDetail
		   ,@PreferredPosition
		   ,@SpecialProgram)
END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetDurationInterviewedSiteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.18
-- Description:	Get Duration Interviewed Site User
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetDurationInterviewedSiteUser] 
	@StartDate datetime
	,@EndDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;
--	declare @strEndDate datetime
--	set @EndDate=@strEndDate;
	--dateadd(day, 1, @EndDate);
	set @EndDate=@EndDate+1
	SELECT
		COUNT(InterviewId) as InterviewCount
		,HiringManagerId 
	FROM 
		sf_Interview
	WHERE 
		StartDate >= @StartDate
	AND
		StartDate <= @EndDate
	GROUP BY HiringManagerId
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateApplicantRelatedInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Applicant Related Info
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateApplicantRelatedInfo]
   @ApplicantId uniqueidentifier
   ,@PreferredStartDate datetime
   ,@SecondaryStartDate datetime
   --,@InterestedGroup int
   ,@InterestedGroup nvarchar(4000)
   ,@InternshipType int
   ,@InterestedAreas nvarchar(256)
   --,@InforSource int
   --,@InfoSourceDetail int
   --,@InfoSourceText nvarchar(256)
   ,@JobInfoCategory nvarchar(256)
   ,@JobInfoSource nvarchar(256)
   ,@JobInfoChannel nvarchar(256)
   ,@JobInfoDetail nvarchar(256)
   ,@PreferredPosition int
   ,@SpecialProgram nvarchar(4000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [dbo].[sf_ApplicantRelatedInfo]
   SET [ApplicantId] = @ApplicantId
      ,[PreferredStartDate] = @PreferredStartDate
      ,[SecondaryStartDate] = @SecondaryStartDate
      ,[InterestedGroup] = @InterestedGroup
	  ,[InternshipType] = @InternshipType
      ,[InterestedAreas] = @InterestedAreas
      --,[InforSource] = @InforSource
      --,[InfoSourceDetail] = @InfoSourceDetail
      --,[InfoSourceText] = @InfoSourceText
	  ,[JobInfoCategory] = @JobInfoCategory
	  ,[JobInfoSource] = @JobInfoSource
	  ,[JobInfoChannel] = @JobInfoChannel
	  ,[JobInfoDetail] = @JobInfoDetail
	  ,[PreferredPosition] = @PreferredPosition
	  ,[SpecialProgram] = @SpecialProgram
 WHERE [ApplicantId] = @ApplicantId

END







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetRecentInterviewStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author: Chen Yuan
-- Create date: July 28, 2006
-- Description:	Get Recent Interview Status
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetRecentInterviewStatus]
	-- Add the parameters for the stored procedure here
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		Top (1)
		InterviewStatus
		,GroupManagerResult
		,StartDate
	FROM
		sf_Interview
	WHERE
		ApplicantId = @ApplicantId
	ORDER BY
		InterviewId DESC
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteCommentById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.22
-- Description:	Delete Comment By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteCommentById]
	@CommentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM [dbo].[sf_Comment] WHERE [CommentId] = @CommentId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectCommentById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.22
-- Description:	Select Comment By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectCommentById] 
	@CommentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_Comment] WHERE [CommentId] = @CommentId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetCommentForApplicant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.22
-- Description:	Get Comment For Applicant
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetCommentForApplicant]
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		* 
	FROM 
		[dbo].[sf_Comment] 
	WHERE 
		[ApplicantId] = @ApplicantId
	ORDER BY
		[CommentTime] DESC
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteInCompleteFeedbackForInterview]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.24
-- Description:	Delete InCoomplete Feedback For Interview 
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteInCompleteFeedbackForInterview]
	@InterviewId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM
		sf_Feedback
	WHERE
		InterviewId = @InterviewId
	AND
		IsComplete = 0
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Setup_RestorePermissions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(20)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + '' '' + @action + '' on '' + @object + '' TO ['' + @grantee + '']''
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Setup_RemoveAllRoleMembers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = ''EXEC sp_droprolemember '' + '''''''' + @name + '''''', '''''' + USER_NAME(@user_id) + ''''''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Applications]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetInterviewIdByApplicant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Hui Yang
-- Create date: 2006-11-14
-- Description:	Get Interview ID
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetInterviewIdByApplicant]
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;	
	SELECT 
		* 
	FROM 
		[dbo].[sf_Interview] 
	WHERE 
		[ApplicantId] = @ApplicantId 	
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectInterviewById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Interview By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectInterviewById] 
	@InterviewId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_Interview] WHERE [InterviewId] = @InterviewId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteInterviewById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Interview By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteInterviewById] 
	@InterviewId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM [dbo].[sf_Interview] WHERE [InterviewId] = @InterviewId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_ApplicantEduBackground]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sf_ApplicantEduBackground](
	[ApplicantId] [uniqueidentifier] NOT NULL,
	[HighestEducationalInstitution] [nvarchar](256) NULL CONSTRAINT [DF_sf_ApplicantEduBackground_HighestEducationalInstitution]  DEFAULT ((0)),
	[MajorCategory] [int] NOT NULL CONSTRAINT [DF_sf_ApplicantEduBackground_MajorCategory]  DEFAULT ((0)),
	[Major] [nvarchar](256) NOT NULL,
	[YearOfStudy] [int] NOT NULL CONSTRAINT [DF_sf_ApplicantEduBackground_YearOfStudy]  DEFAULT ((0)),
	[Rank] [int] NOT NULL CONSTRAINT [DF_sf_ApplicantEduBackground_Rank]  DEFAULT ((0)),
	[ResumeId] [int] NULL CONSTRAINT [DF_sf_ApplicantEduBackground_ResumeId]  DEFAULT ((0)),
	[ResumeImage] [image] NULL,
	[ResumeExt] [nvarchar](50) NULL,
	[PaperAId] [int] NULL CONSTRAINT [DF_sf_ApplicantEduBackground_PaperAId]  DEFAULT ((0)),
	[PaperBId] [int] NULL CONSTRAINT [DF_sf_ApplicantEduBackground_PaperBId]  DEFAULT ((0)),
	[GraduatedDate] [datetime] NOT NULL CONSTRAINT [DF_sf_ApplicantEduBackground_GraduatedDate]  DEFAULT (getdate()),
	[AdvisorFirstName] [nvarchar](256) NULL,
	[AdvisorLastName] [nvarchar](256) NULL,
	[AdvisorFullName] [nvarchar](256) NULL,
	[AdvisorEmail] [nvarchar](256) NULL,
	[AdvisorOrganization] [nvarchar](256) NULL,
	[ResearchApproach] [int] NOT NULL CONSTRAINT [DF_sf_ApplicantEduBackground_ResearchApproach]  DEFAULT ((0)),
	[Degree] [int] NULL CONSTRAINT [DF_sf_ApplicantEduBackground_Degree]  DEFAULT ((0)),
	[College] [nvarchar](256) NULL,
	[EnrollDate] [datetime] NULL,
 CONSTRAINT [PK_sf_ApplicantEduBackground] PRIMARY KEY CLUSTERED 
(
	[ApplicantId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_SchemaVersions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetInterviewForApplicant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








-- =============================================
-- Author:		Hui Yang
-- Create date: 2007-04-06
-- Description:	get the interview history of one applicant.
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetInterviewForApplicant]
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		sf_interview.*
	FROM
		sf_interview
	WHERE
		applicantid=@ApplicantId
	ORDER BY StartDate
END









' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertDocument]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Document
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertDocument]
   @DocumentType int
   ,@OriginalName nvarchar(256)
   ,@ApplicantId uniqueidentifier
   ,@FileName nvarchar(256)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_Document]
           ([DocumentType]
           ,[OriginalName]
           ,[ApplicantId]
           ,[FileName])
     VALUES
           (@DocumentType
           ,@OriginalName
           ,@ApplicantId
           ,@FileName)
SELECT @@IDENTITY

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateDocument]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Document
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateDocument]
	@DocumentId int
   ,@DocumentType int
   ,@OriginalName nvarchar(256)
   ,@ApplicantId uniqueidentifier
   ,@FileName nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_Document]
   SET [DocumentType] = @DocumentType
      ,[OriginalName] = @OriginalName
      ,[ApplicantId] = @ApplicantId
      ,[FileName] = @FileName
 WHERE [DocumentId] = @DocumentId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteDocumentById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Document By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteDocumentById] 
	@DocumentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE [dbo].[sf_Document] WHERE [DocumentId] = @DocumentId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectDocumentById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Document By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectDocumentById]
	@DocumentId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_Document] WHERE [DocumentId] = @DocumentId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetDocumentForApplicant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Get Document For Applicant
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetDocumentForApplicant]
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[sf_Document] WHERE [ApplicantId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers](
	[PathId] [uniqueidentifier] NOT NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser](
	[Id] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[PathId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Paths]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Paths](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[Path] [nvarchar](256) NOT NULL,
	[LoweredPath] [nvarchar](256) NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL DEFAULT ((0)),
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL DEFAULT (NULL),
	[IsAnonymous] [bit] NOT NULL DEFAULT ((0)),
	[LastActivityDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL DEFAULT (newid()),
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[aspnet_Profile](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [ntext] NOT NULL,
	[PropertyValuesString] [ntext] NOT NULL,
	[PropertyValuesBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetUncompleteFeedback]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Email By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetUncompleteFeedback]
	@DueDate Datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT sf_Interview.applicantid,InterviewerAlias, UserName, FeedbackId, FirstName, LastName, NameInChinese, DueDate
	FROM 
	dbo.sf_Interview JOIN dbo.sf_Feedback 
	ON dbo.sf_Interview.InterviewId = dbo.sf_Feedback.InterviewId
	JOIN dbo.aspnet_Users 
	ON dbo.aspnet_Users.UserId = dbo.sf_Interview.HiringManagerId
	JOIN dbo.sf_ApplicantBasicInfo
	ON dbo.sf_Feedback.ApplicantId = dbo.sf_ApplicantBasicInfo.ApplicantId
	WHERE 
	dbo.sf_Feedback.IsComplete = 0 and 
	dbo.sf_Feedback.DueDate < @DueDate
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertFeedback]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Feedback
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertFeedback] 
   @InterviewerId uniqueidentifier
   ,@ApplicantId uniqueidentifier
   ,@InterviewId int
   ,@SuggestionResult int
   ,@FeedbackContent nvarchar(4000)
   ,@InterviewDate datetime
   ,@IsComplete bit
   ,@DueDate datetime
   ,@InterviewerAlias nvarchar(256)	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_Feedback]
           ([InterviewerId]
           ,[ApplicantId]
           ,[InterviewId]
           ,[SuggestionResult]
           ,[FeedbackContent]
           ,[InterviewDate]
           ,[IsComplete]
           ,[DueDate]
           ,[InterviewerAlias])
     VALUES
           (@InterviewerId
           ,@ApplicantId
           ,@InterviewId
           ,@SuggestionResult
           ,@FeedbackContent
           ,@InterviewDate
           ,@IsComplete
           ,@DueDate
           ,@InterviewerAlias)
SELECT @@IDENTITY
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateFeedback]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Feedback
-- updated by Hui Yang,2007-04-06,change the interview status.
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateFeedback] 
	@FeedbackId int
   ,@InterviewerId uniqueidentifier
   ,@ApplicantId uniqueidentifier
   ,@InterviewId int
   ,@SuggestionResult int
   ,@FeedbackContent nvarchar(4000)
   ,@InterviewDate datetime
   ,@IsComplete bit
   ,@DueDate datetime
   ,@InterviewerAlias nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @unfinished int
UPDATE [dbo].[sf_Feedback]
   SET [InterviewerId] = @InterviewerId
      ,[ApplicantId] = @ApplicantId
      ,[InterviewId] = @InterviewId
      ,[SuggestionResult] = @SuggestionResult
      ,[FeedbackContent] = @FeedbackContent
      ,[InterviewDate] = @InterviewDate
      ,[IsComplete] = @IsComplete
      ,[DueDate] = @DueDate
      ,[InterviewerAlias] = @InterviewerAlias
 WHERE [FeedbackId] = @FeedbackId
--if all feedbacks are completed and the interview status is also "interviewstarted"
--changed the status to "interview completed"
select @unfinished=count(*) from [dbo].[sf_Feedback],[dbo].[sf_Interview] 
where [dbo].[sf_Interview].[InterviewId] = @InterviewId and
[IsComplete]=0 and [dbo].[sf_Feedback].interviewid=[dbo].[sf_Interview].interviewid 
and [dbo].[sf_Interview].InterviewStatus=0
if(@unfinished=0)
begin
update [dbo].[sf_Interview] set InterviewStatus=1 where [InterviewId] = @InterviewId
end
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.22
-- Description:	Update Comment
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateComment] 
	  @CommentId int
	 ,@CommentTime datetime
     ,@CommentContent nvarchar(4000)
     ,@ApplicantId uniqueidentifier
     ,@Commenter uniqueidentifier
     ,@CommenterAlias nvarchar(256)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE [dbo].[sf_Comment]
	SET [CommentTime] = @CommentTime
      ,[CommentContent] = @CommentContent
      ,[ApplicantId] = @ApplicantId
      ,[Commenter] = @Commenter
      ,[CommenterAlias] = @CommenterAlias
 WHERE [CommentId] = @CommentId

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertComment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.22
-- Description:	Insert New Comment
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertComment]
   @CommentTime datetime
   ,@CommentContent nvarchar(4000)
   ,@ApplicantId uniqueidentifier
   ,@Commenter uniqueidentifier
   ,@CommenterAlias nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[sf_Comment]
           ([CommentTime]
           ,[CommentContent]
           ,[ApplicantId]
           ,[Commenter]
           ,[CommenterAlias])
     VALUES
           (@CommentTime
           ,@CommentContent
           ,@ApplicantId
           ,@Commenter
           ,@CommenterAlias)
	SELECT @@IDENTITY
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectRelatedReferrerByReferralId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Related Referrer By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectRelatedReferrerByReferralId] 
	@ReferralId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_RelatedReferrer] WHERE [ReferralId] = @ReferralId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertRelatedReferrer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Related Referrer
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertRelatedReferrer] 
   @ReferralId int
   ,@Email nvarchar(256)
   ,@LastName nvarchar(256)
   ,@FirstName nvarchar(256)
   ,@Gender int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_RelatedReferrer]
           ([ReferralId]
           ,[Email]
           ,[LastName]
           ,[FirstName]
           ,[Gender])
     VALUES
           (@ReferralId
           ,@Email
           ,@LastName
           ,@FirstName
           ,@Gender)
SELECT @@IDENTITY
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateRelatedReferrer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Related Referrer
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateRelatedReferrer] 
	@RelatedReferrerId int
   ,@ReferralId int
   ,@Email nvarchar(256)
   ,@LastName nvarchar(256)
   ,@FirstName nvarchar(256)
   ,@Gender int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_RelatedReferrer]
   SET [ReferralId] = @ReferralId
      ,[Email] = @Email
      ,[LastName] = @LastName
      ,[FirstName] = @FirstName
      ,[Gender] = @Gender
 WHERE [RelatedReferrerId] = @RelatedReferrerId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteRelatedReferrerById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Related Referrer By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteRelatedReferrerById] 
	@RelatedReferrerId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE [dbo].[sf_RelatedReferrer] WHERE [RelatedReferrerId] = @RelatedReferrerId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetEmailTemplate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 8/5/2006
-- Description:	Get Email Template
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetEmailTemplate] 
	@EmailType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_EmailTemplate] WHERE [EmailType] = @EmailType

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateEmailTemplate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Email
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateEmailTemplate]
	@EmailType int
   ,@From nvarchar(256)
   ,@To nvarchar(256)
   ,@Cc nvarchar(256)
   ,@Bcc nvarchar(256)
   ,@Subject nvarchar(256)
   ,@Body nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_EmailTemplate]
   SET [EmailTemplate] = @Body
      ,[EmailFrom] = @From
      ,[EmailTo] = @To
      ,[EmailCc] = @Cc
      ,[EmailBcc] = @Bcc
      ,[EmailSubject] = @Subject
 WHERE [EmailType] = @EmailType

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_DeleteInactiveProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    dbo.aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    dbo.aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_Profiles]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_Profiles]
  AS SELECT [dbo].[aspnet_Profile].[UserId], [dbo].[aspnet_Profile].[LastUpdatedDate],
      [DataSize]=  DATALENGTH([dbo].[aspnet_Profile].[PropertyNames])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesString])
                 + DATALENGTH([dbo].[aspnet_Profile].[PropertyValuesBinary])
  FROM [dbo].[aspnet_Profile]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_GetProperties]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProperties]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         dbo.aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteApplicantById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Hui Yang
-- Create date: 2006-10-26
-- Description:	Delete all related information of an applicant.
-- =============================================
create PROCEDURE [dbo].[sf_DeleteApplicantById] 
	-- Add the parameters for the stored procedure here
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE FROM [dbo].[sf_ApplicantBasicInfo] WHERE [ApplicantId] = @ApplicantId
	DELETE FROM [dbo].[sf_ApplicantEduBackground] WHERE [ApplicantId] = @ApplicantId
	DELETE FROM [dbo].[sf_ApplicantRelatedInfo] WHERE [ApplicantId] = @ApplicantId
	DELETE FROM [dbo].[sf_Favorites] WHERE [ApplicantId] = @ApplicantId
	DELETE FROM [dbo].[sf_Feedback] WHERE [ApplicantId] = @ApplicantId
	DELETE FROM [dbo].[sf_Interview] WHERE [ApplicantId] = @ApplicantId
	DELETE FROM [dbo].[sf_Referral] WHERE [ApplicantId] = @ApplicantId
	--delete membership
	DELETE FROM [dbo].[aspnet_Membership] WHERE [UserId] = @ApplicantId
	DELETE FROM [dbo].[aspnet_Profile] WHERE [UserId] = @ApplicantId
	DELETE FROM [dbo].[aspnet_UsersInRoles] WHERE [UserId] = @ApplicantId
	DELETE FROM [dbo].[aspnet_Users] WHERE [UserId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetCurrentInterview]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 8/5/2006
-- Description:	Get Current Interview
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetCurrentInterview]
	@ApplicantId uniqueidentifier
	,@HiringManagerId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @IsInternRecruiter varchar(200)
	select  @IsInternRecruiter=aspnet_roles.roleid from aspnet_usersinroles inner join
	aspnet_users on
	aspnet_users.userid=aspnet_usersinroles.userid
	inner join aspnet_roles 
	on 
	aspnet_roles.roleid=aspnet_usersinroles.roleid
	where aspnet_users.UserId= @HiringManagerId 
	and aspnet_roles.RoleName=''internrecruiter''
	if @IsInternRecruiter is null
	BEGIN
	SELECT 
		* 
	FROM 
		[dbo].[sf_Interview] 
	WHERE 
		[ApplicantId] = @ApplicantId 
	AND [HiringManagerId] = @HiringManagerId
	AND [InterviewStatus] < 2
	END
	ELSE
	BEGIN
	SELECT 
		* 
	FROM 
		[dbo].[sf_Interview] 
	WHERE 
		[ApplicantId] = @ApplicantId 
	AND [InterviewStatus] < 2
	END
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetIncompleteFeedbackForUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 29/5/2006
-- Update date: 01/11/2006 by Hui Yang
-- Description:	Get Incomplete Feedback For User
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetIncompleteFeedbackForUser]
	@InterviewerAlias nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--check if the user is a internrecruiter
	declare @IsInternRecruiter varchar(200)
	select  @IsInternRecruiter=aspnet_roles.roleid from aspnet_usersinroles inner join
	aspnet_users on
	aspnet_users.userid=aspnet_usersinroles.userid
	inner join aspnet_roles 
	on 
	aspnet_roles.roleid=aspnet_usersinroles.roleid
	where aspnet_users.username= @InterviewerAlias 
	and aspnet_roles.RoleName=''internrecruiter''
	if @IsInternRecruiter is null
	begin
	SELECT
		sf_Feedback.ApplicantId
		,FeedbackId
		,InterviewId
		,DueDate
		,FirstName
		,LastName
	FROM
		sf_Feedback inner JOIN sf_ApplicantBasicInfo
	ON sf_Feedback.ApplicantId = sf_ApplicantBasicInfo.ApplicantId
	WHERE
		[InterviewerAlias] = @InterviewerAlias
	AND [IsComplete] = 0
	ORDER BY
		DueDate ASC
	end
	else
	begin
	SELECT
		sf_Feedback.ApplicantId
		,FeedbackId
		,InterviewId
		,DueDate
		,FirstName
		,LastName
	FROM
		sf_Feedback inner JOIN sf_ApplicantBasicInfo
	ON sf_Feedback.ApplicantId = sf_ApplicantBasicInfo.ApplicantId
	WHERE
		[IsComplete] = 0
	ORDER BY
		DueDate ASC
	end
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_ZZGetUserRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sf_ZZGetUserRole]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select aspnet_Users.username, aspnet_Roles.rolename
	from aspnet_Users, aspnet_UsersInRoles, aspnet_Roles
	where
	aspnet_users.UserId = aspnet_usersinroles.UserId and 
	aspnet_usersinroles.roleid = aspnet_Roles.roleid and
	(aspnet_users.username like ''fareast\%'' or aspnet_users.username like ''redmond\%'')
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_IsUserInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    dbo.aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM dbo.aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_DeleteRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM dbo.aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM dbo.aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_GetRolesForUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    dbo.aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   dbo.aspnet_Roles r, dbo.aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_AddUsersToRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) IN (SELECT LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	dbo.aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) = au.LoweredUserName AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 dbo.aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO dbo.aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   dbo.aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredRoleName AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'''', Name
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT ar.LoweredRoleName FROM dbo.aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N'','', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   dbo.aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) = ar.LoweredUserName AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''''
		FROM   @tbNames
		WHERE  LOWER(Name) NOT IN (SELECT au.LoweredUserName FROM dbo.aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	dbo.aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, dbo.aspnet_Users u, dbo.aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM dbo.aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM dbo.aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM dbo.aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_GetUsersInRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles_FindUsersInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    dbo.aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   dbo.aspnet_Users u, dbo.aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_RoleExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM dbo.aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_GetAllRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   dbo.aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_Roles]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_Roles]
  AS SELECT [dbo].[aspnet_Roles].[ApplicationId], [dbo].[aspnet_Roles].[RoleId], [dbo].[aspnet_Roles].[RoleName], [dbo].[aspnet_Roles].[LoweredRoleName], [dbo].[aspnet_Roles].[Description]
  FROM [dbo].[aspnet_Roles]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertInterview]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Interview
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertInterview] 
   @StartDate datetime
   ,@InterviewStatus int
   ,@EndDate datetime
   ,@HiringManagerId uniqueidentifier
   ,@HiringManagerResult int
   ,@HiringManagerComment nvarchar(4000)
   ,@GroupManagerId uniqueidentifier
   ,@GroupManagerResult int
   ,@GroupManagerComment nvarchar(4000)
   ,@ApplicantId uniqueidentifier
   ,@CheckInFormId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_Interview]
           ([StartDate]
           ,[InterviewStatus]
           ,[EndDate]
           ,[HiringManagerId]
           ,[HiringManagerResult]
           ,[HiringManagerComment]
           ,[GroupManagerId]
           ,[GroupManagerResult]
           ,[GroupManagerComment]
           ,[ApplicantId]
		   ,[CheckInFormId])
     VALUES
           (@StartDate
           ,@InterviewStatus
           ,@EndDate
           ,@HiringManagerId
           ,@HiringManagerResult
           ,@HiringManagerComment
           ,@GroupManagerId
           ,@GroupManagerResult
           ,@GroupManagerComment
           ,@ApplicantId
		   ,@CheckInFormId)
SELECT @@IDENTITY
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateInterview]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Interview
-- Modified by Hui Yang at 3/15/2007
-- Add the column ''MentorDecisionTime'' and ''ManagerDecisionTime''
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateInterview] 
	@InterviewId int
   ,@StartDate datetime
   ,@InterviewStatus int
   ,@EndDate datetime
   ,@HiringManagerId uniqueidentifier
   ,@HiringManagerResult int
   ,@HiringManagerComment nvarchar(4000)
   ,@GroupManagerId uniqueidentifier
   ,@GroupManagerResult int
   ,@GroupManagerComment nvarchar(4000)
   ,@ApplicantId uniqueidentifier
   ,@MentorDecisionTime datetime
   ,@ManagerDecisionTime datetime
   ,@CheckInFormId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_Interview]
   SET [StartDate] = @StartDate
      ,[InterviewStatus] = @InterviewStatus
      ,[EndDate] = @EndDate
      ,[HiringManagerId] = @HiringManagerId
      ,[HiringManagerResult] = @HiringManagerResult
      ,[HiringManagerComment] = @HiringManagerComment
      ,[GroupManagerId] = @GroupManagerId
      ,[GroupManagerResult] = @GroupManagerResult
      ,[GroupManagerComment] = @GroupManagerComment
      ,[ApplicantId] = @ApplicantId
	  ,[MentorDecisionTime] = @MentorDecisionTime
	  ,[ManagerDecisionTime] = @ManagerDecisionTime
	  ,[CheckInFormId] = @CheckInFormId
 WHERE [InterviewId] = @InterviewId

END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_UsersInRoles]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_UsersInRoles]
  AS SELECT [dbo].[aspnet_UsersInRoles].[UserId], [dbo].[aspnet_UsersInRoles].[RoleId]
  FROM [dbo].[aspnet_UsersInRoles]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_ChangeApplicantStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen	
-- Create date: 16/5/2006
-- Description:	Change Applicatant Status
-- =============================================
CREATE PROCEDURE [dbo].[sf_ChangeApplicantStatus]
	@ApplicantId uniqueidentifier
	,@Status int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	UPDATE
		[dbo].[sf_ApplicantBasicInfo]
	SET
		[Status] = @Status
	WHERE
		ApplicantId = @ApplicantId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteApplicantBasicInfoById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteApplicantBasicInfoById] 
   @ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM [dbo].[sf_ApplicantBasicInfo] WHERE [ApplicantId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectApplicantBasicInfoById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectApplicantBasicInfoById]
   @ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[sf_ApplicantBasicInfo] WHERE [ApplicantId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateApplicantBasicInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateApplicantBasicInfo] 
   (@ApplicantId uniqueidentifier
   ,@Gender int
   --,@Nationality int
   ,@Nationality nvarchar(256)
   ,@IdentityNumber nvarchar(256)
   ,@Webpage nvarchar(256)
   ,@Address nvarchar(256)
   ,@PhoneNumber nvarchar(256)
   ,@CurrentCity nvarchar(256)
   ,@CurrentProvince nvarchar(256)
   --,@CurrentCountry int
   ,@CurrentCountry nvarchar(256)
   ,@Status int
   ,@Priority int
   ,@ApplicationDate datetime
   ,@Email nvarchar(256)
   ,@FirstName nvarchar(256)
   ,@LastName nvarchar(256)
   ,@ReferralId int
   ,@NameInChinese nvarchar(256))
   ,@IsOffline int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [dbo].[sf_ApplicantBasicInfo]
   SET [ApplicantId] = @ApplicantId
      ,[Gender] = @Gender
      ,[Nationality] = @Nationality
      ,[IdentityNumber] = @IdentityNumber
      ,[Webpage] = @Webpage
      ,[Address] = @Address
      ,[PhoneNumber] = @PhoneNumber
      ,[CurrentCity] = @CurrentCity
      ,[CurrentProvince] = @CurrentProvince
      ,[CurrentCountry] = @CurrentCountry
      ,[Status] = @Status
      ,[Priority] = @Priority
      ,[ApplicationDate] = @ApplicationDate
      ,[Email] = @Email
      ,[FirstName] = @FirstName
      ,[LastName] = @LastName
      ,[ReferralId] = @ReferralId
      ,[NameInChinese] = @NameInChinese
      ,[IsOffline] = @IsOffline
 WHERE [ApplicantId] = @ApplicantId

END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertApplicantBasicInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Applicant Basic Info
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertApplicantBasicInfo] 
   (@ApplicantId uniqueidentifier
   ,@Gender int
   --,@Nationality int
   ,@Nationality nvarchar(256)
   ,@IdentityNumber nvarchar(256)
   ,@Webpage nvarchar(256)
   ,@Address nvarchar(256)
   ,@PhoneNumber nvarchar(256)
   ,@CurrentCity nvarchar(256)
   ,@CurrentProvince nvarchar(256)
   --,@CurrentCountry int
   ,@CurrentCountry nvarchar(256)
   ,@Status int
   ,@Priority int
   ,@ApplicationDate datetime
   ,@Email nvarchar(256)
   ,@FirstName nvarchar(256)
   ,@LastName nvarchar(256)
   ,@ReferralId int
   ,@NameInChinese nvarchar(256))
   ,@IsOffline int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO [dbo].[sf_ApplicantBasicInfo]
           ([ApplicantId]
           ,[Gender]
           ,[Nationality]
           ,[IdentityNumber]
           ,[Webpage]
           ,[Address]
           ,[PhoneNumber]
           ,[CurrentCity]
           ,[CurrentProvince]
           ,[CurrentCountry]
           ,[Status]
           ,[Priority]
           ,[ApplicationDate]
           ,[Email]
           ,[FirstName]
           ,[LastName]
           ,[ReferralId]
           ,[NameInChinese]
           ,[IsOffline])
     VALUES
		   (@ApplicantId 
		   ,@Gender 
		   ,@Nationality 
		   ,@IdentityNumber 
		   ,@Webpage 
		   ,@Address
		   ,@PhoneNumber
		   ,@CurrentCity
		   ,@CurrentProvince
		   ,@CurrentCountry 
		   ,@Status 
		   ,@Priority 
		   ,@ApplicationDate 
		   ,@Email
		   ,@FirstName
		   ,@LastName
           ,@ReferralId
           ,@NameInChinese
           ,@IsOffline)
END







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetDecidedApplicants]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








-- =============================================
-- Author:		Yuan Chen
-- Create date: 10/5/2006
-- Modifiy date: 16/5/2006
-- Description:	Get Decided Applicants
-- Modified:rejected applicant(status=5).2007-04-04,by Hui Yang
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetDecidedApplicants] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
	InterviewId
	,EndDate
	,sf_ApplicantBasicInfo.ApplicantId
	,FirstName
	,LastName
	,Status
	,HiringManagerResult
	,GroupManagerResult
	FROM
		[dbo].[sf_Interview] INNER JOIN [dbo].[sf_ApplicantBasicInfo] 
		ON [dbo].[sf_Interview].ApplicantId = [dbo].[sf_ApplicantBasicInfo].ApplicantId
	WHERE [InterviewStatus] = 6	ORDER BY EndDate DESC
END









' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[sf_vw_AllApplicants]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[sf_vw_AllApplicants]
AS
SELECT     TOP (100) PERCENT dbo.sf_ApplicantBasicInfo.ApplicantId, dbo.sf_ApplicantEduBackground.Major, dbo.sf_ApplicantBasicInfo.Status, 
                      dbo.sf_ApplicantRelatedInfo.InterestedAreas, dbo.sf_ApplicantRelatedInfo.PreferredStartDate, dbo.sf_ApplicantRelatedInfo.SecondaryStartDate, 
                      dbo.sf_ApplicantBasicInfo.FirstName, dbo.sf_ApplicantBasicInfo.LastName, dbo.sf_ApplicantEduBackground.MajorCategory, 
                      dbo.sf_ApplicantEduBackground.Degree, dbo.sf_ApplicantEduBackground.HighestEducationalInstitution, dbo.sf_ApplicantBasicInfo.ReferralId, 
                      dbo.sf_ApplicantBasicInfo.Priority, dbo.sf_ApplicantBasicInfo.ApplicationDate, dbo.sf_ApplicantRelatedInfo.InterestedGroup, 
                      dbo.sf_ApplicantRelatedInfo.JobInfoSource, dbo.sf_ApplicantBasicInfo.NameInChinese, 
                      dbo.sf_ApplicantBasicInfo.FirstName + '' '' + dbo.sf_ApplicantBasicInfo.LastName + '' '' + dbo.sf_ApplicantBasicInfo.NameInChinese AS FullName
FROM         dbo.sf_ApplicantBasicInfo INNER JOIN
                      dbo.sf_ApplicantRelatedInfo INNER JOIN
                      dbo.sf_ApplicantEduBackground ON dbo.sf_ApplicantRelatedInfo.ApplicantId = dbo.sf_ApplicantEduBackground.ApplicantId ON 
                      dbo.sf_ApplicantBasicInfo.ApplicantId = dbo.sf_ApplicantRelatedInfo.ApplicantId
ORDER BY dbo.sf_ApplicantBasicInfo.ApplicationDate DESC
' 
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'sf_vw_AllApplicants', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "sf_ApplicantBasicInfo"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 122
               Right = 195
            End
            DisplayFlags = 280
            TopColumn = 14
         End
         Begin Table = "sf_ApplicantRelatedInfo"
            Begin Extent = 
               Top = 6
               Left = 490
               Bottom = 122
               Right = 667
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "sf_ApplicantEduBackground"
            Begin Extent = 
               Top = 6
               Left = 233
               Bottom = 122
               Right = 452
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'sf_vw_AllApplicants'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'sf_vw_AllApplicants', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'sf_vw_AllApplicants'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_unsendmailist]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'










-- =============================================
-- Author:		Hui Yang
-- Create date: 4/17/2007
-- Description:	Get unsend mail Applicants
-- =============================================
create PROCEDURE [dbo].[sf_unsendmailist] 
	@InterviewStatus Int
AS
	
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
	InterviewId
	,EndDate
	,sf_ApplicantBasicInfo.ApplicantId
	,FirstName
	,LastName
	,Status
	,HiringManagerResult
	,GroupManagerResult
	FROM
		[dbo].[sf_Interview] INNER JOIN [dbo].[sf_ApplicantBasicInfo] 
		ON [dbo].[sf_Interview].ApplicantId = [dbo].[sf_ApplicantBasicInfo].ApplicantId
	WHERE [InterviewStatus] = @InterviewStatus and IsSendMail=0	ORDER BY EndDate DESC
END











' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectCheckInFormById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.22
-- Description:	Select Comment By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectCheckInFormById] 
	@FormId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [FormId]
      ,[GroupId]
      ,[ProjectId]
	  ,[PositionId]
      ,[InternTypeId]
      ,[MentorAlias]
      ,[PreferCheckInDate]
      ,[PreferLastWorkingDay]
      ,[AdvisorApproved]
      ,[Comments]
  FROM [springfield].[dbo].[sf_CheckInForm]
  WHERE [FormId] = @FormId
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateCheckInForm]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Email
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateCheckInForm]

	
	@FormId int
	,@GroupId int
	,@PositionId int
	,@ProjectId int
	,@InternTypeId int
	,@MentorAlias varchar(50)
	,@PreferCheckInDate datetime
	,@PreferLastWorkingDay datetime
	,@AdvisorApproved bit
	,@Comments nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_CheckInForm]
   SET [GroupId] = @GroupId
      ,[ProjectId] = @ProjectId
	  ,[PositionId] = @PositionId
      ,[InternTypeId] = @InternTypeId
      ,[MentorAlias] = @MentorAlias
      ,[PreferCheckInDate] = @PreferCheckInDate
      ,[PreferLastWorkingDay] = @PreferLastWorkingDay
      ,[AdvisorApproved] = @AdvisorApproved
      ,[Comments] = @Comments
 WHERE [FormId] = @FormId

END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertCheckInForm]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.22
-- Description:	Insert New Comment
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertCheckInForm]
	@GroupId int
	,@ProjectId int
	,@PositionId int
	,@InternTypeId int
	,@MentorAlias varchar(50)
	,@PreferCheckInDate datetime
	,@PreferLastWorkingDay datetime
	,@AdvisorApproved bit
	,@Comments nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[sf_CheckInForm]
		([GroupId]
		,[ProjectId]
		,[PositionId]
		,[InternTypeId]
		,[MentorAlias]
		,[PreferCheckInDate]
		,[PreferLastWorkingDay]
		,[AdvisorApproved]
		,[Comments])
     VALUES
			(@GroupId
	,@ProjectId
	,@PositionId
	,@InternTypeId
	,@MentorAlias
	,@PreferCheckInDate
	,@PreferLastWorkingDay
	,@AdvisorApproved
	,@Comments )
           
	SELECT @@IDENTITY
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Paths_CreatePath]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Paths_CreatePath]
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM dbo.aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT dbo.aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM dbo.aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_WebPartState_Paths]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Paths]
  AS SELECT [dbo].[aspnet_Paths].[ApplicationId], [dbo].[aspnet_Paths].[PathId], [dbo].[aspnet_Paths].[Path], [dbo].[aspnet_Paths].[LoweredPath]
  FROM [dbo].[aspnet_Paths]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetAllInterviewApplicants]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.17
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetAllInterviewApplicants]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		*
	FROM
		sf_Interview
	LEFT JOIN
		sf_vw_AllApplicants
	LEFT JOIN
		sf_Referral
	ON
		sf_vw_AllApplicants.ApplicantId = sf_Referral.ApplicantId
	ON 
		sf_Interview.ApplicantId = sf_vw_AllApplicants.ApplicantId	
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetDurationInterview]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.18
-- Description:	Get Duration Interview
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetDurationInterview]
	@StartDate datetime
	,@EndDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @EndDate=@EndDate+1
	--select @EndDate
	SELECT
		*
	FROM
		sf_Interview
	LEFT JOIN
		sf_vw_AllApplicants
	LEFT JOIN
		sf_Referral
	ON
		sf_vw_AllApplicants.ApplicantId = sf_Referral.ApplicantId
	ON
		sf_Interview.ApplicantId = sf_vw_AllApplicants.ApplicantId
	WHERE
		StartDate >= @StartDate
	AND
		StartDate <= @EndDate
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertReferral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Referral
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertReferral] 
   @ReferralType int
   ,@ApplicantId uniqueidentifier
   ,@ReferrerId uniqueidentifier
   ,@ReferredTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_Referral]
           ([ReferralType]
           ,[ApplicantId]
           ,[ReferrerId]
           ,[ReferredTime])
     VALUES
           (@ReferralType
           ,@ApplicantId
           ,@ReferrerId
		   ,@ReferredTime)
SELECT @@IDENTITY
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateReferral]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Referral
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateReferral] 
	@ReferralId int
   ,@ReferralType int
   ,@ApplicantId uniqueidentifier
   ,@ReferrerId uniqueidentifier
   ,@ReferredTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_Referral]
   SET [ReferralType] = @ReferralType
      ,[ApplicantId] = @ApplicantId
      ,[ReferrerId] = @ReferrerId
      ,[ReferredTime] = @ReferredTime
 WHERE [ReferralId] = @ReferralId

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetApplicantsByDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'










-- =============================================
-- Author: Hui Yang
-- Create date: Mar 13, 2006
-- Description:	Get applicants by application date
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetApplicantsByDate] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		sf_vw_AllApplicants.*,
		sf_Referral.ReferralType 
	FROM 
		(sf_vw_AllApplicants LEFT JOIN sf_Referral
		ON sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId) where convert(varchar(10),applicationDate,120)>=@StartDate and
	convert(varchar(10),applicationDate,120)
<=@EndDate
	ORDER BY
		ReferralType DESC, ApplicationDate DESC
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Email
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertEmail]
   @Priority int
   ,@BodyFormat int
   ,@From nvarchar(256)
   ,@To nvarchar(256)
   ,@Cc nvarchar(256)
   ,@Bcc nvarchar(256)
   ,@Subject nvarchar(256)
   ,@Body nvarchar(max)
   ,@NextTryTime datetime
   ,@NumberOfTries int
   ,@IsSend int
   ,@RelatedUserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_Email]
           ([Priority]
           ,[BodyFormat]
           ,[From]
           ,[To]
           ,[Cc]
           ,[Bcc]
           ,[Subject]
           ,[Body]
           ,[NextTryTime]
           ,[NumberOfTries]
           ,[IsSend]
           ,[RelatedUserId])
     VALUES
           (@Priority
           ,@BodyFormat
           ,@From
           ,@To
           ,@Cc
           ,@Bcc
           ,@Subject
           ,@Body
           ,@NextTryTime
           ,@NumberOfTries
           ,@IsSend
           ,@RelatedUserId)
SELECT @@IDENTITY
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Email
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateEmail]
	@EmailId int
   ,@Priority int
   ,@BodyFormat int
   ,@From nvarchar(256)
   ,@To nvarchar(256)
   ,@Cc nvarchar(256)
   ,@Bcc nvarchar(256)
   ,@Subject nvarchar(256)
   ,@Body nvarchar(max)
   ,@NextTryTime datetime
   ,@NumberOfTries int
   ,@IsSend int
   ,@RelatedUserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_Email]
   SET [Priority] = @Priority
      ,[BodyFormat] = @BodyFormat
      ,[From] = @From
      ,[To] = @To
      ,[Cc] = @Cc
      ,[Bcc] = @Bcc
      ,[Subject] = @Subject
      ,[Body] = @Body
      ,[NextTryTime] = @NextTryTime
      ,[NumberOfTries] = @NumberOfTries
      ,[IsSend] = @IsSend
      ,[RelatedUserId] = @RelatedUserId
 WHERE [EmailId] = @EmailId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectEmailById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Email By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectEmailById]
	@EmailId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_Email] WHERE [EmailId] = @EmailId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteEmailById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Email By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteEmailById]
	@EmailId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE [dbo].[sf_Email] WHERE [EmailId] = @EmailId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_WebPartState_Shared]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_WebPartState_Shared]
  AS SELECT [dbo].[aspnet_PersonalizationAllUsers].[PathId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationAllUsers].[PageSettings]), [dbo].[aspnet_PersonalizationAllUsers].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationAllUsers]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_WebPartState_User]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_WebPartState_User]
  AS SELECT [dbo].[aspnet_PersonalizationPerUser].[PathId], [dbo].[aspnet_PersonalizationPerUser].[UserId], [DataSize]=DATALENGTH([dbo].[aspnet_PersonalizationPerUser].[PageSettings]), [dbo].[aspnet_PersonalizationPerUser].[LastUpdatedDate]
  FROM [dbo].[aspnet_PersonalizationPerUser]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertFavorite]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Favorites
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertFavorite]
   @OwnerId uniqueidentifier
   ,@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

INSERT INTO [dbo].[sf_Favorites]
           ([OwnerId]
           ,[ApplicantId])
     VALUES
           (@OwnerId
           ,@ApplicantId)
SELECT @@IDENTITY
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateFavorite]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Favorites
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateFavorite]
	@FavoriteId int
   ,@OwnerId uniqueidentifier
   ,@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [dbo].[sf_Favorites]
   SET [OwnerId] = @OwnerId
      ,[ApplicantId] = @ApplicantId
 WHERE [FavoriteId] = @FavoriteId

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteFavoriteById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Delete Favorite By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_DeleteFavoriteById]
	@FavoriteId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE [dbo].[sf_Favorites] WHERE [FavoriteId] = @FavoriteId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SelectFavoriteById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Favorite By Id
-- =============================================
CREATE PROCEDURE [dbo].[sf_SelectFavoriteById] 
	@FavoriteId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[sf_Favorites] WHERE [FavoriteId] = @FavoriteId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_IsFavoriteExist]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sf_IsFavoriteExist]
	@OwnerId uniqueidentifier
	,@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT COUNT(FavoriteId) FROM [sf_Favorites] WHERE [OwnerId] = @OwnerId AND [ApplicantId] = @ApplicantId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_DeleteFavorite]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Hui Yang
-- Create date: 4/3/2007
-- Description:	Delete Favorite By Owner and applicant.
-- =============================================
create PROCEDURE [dbo].[sf_DeleteFavorite]
	@OwnerId uniqueidentifier,
	@ApplicantId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE [dbo].[sf_Favorites] WHERE [OwnerId] = @OwnerId and [ApplicantId]=@ApplicantId
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_WebEvent_LogEvent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_WebEvent_LogEvent]
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        dbo.aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_InsertApplicantEduBackground]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'






-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Insert Applicant Education Background
-- Modified: Hui Yang,add resume image and resume extension,2007-3-16
-- =============================================
CREATE PROCEDURE [dbo].[sf_InsertApplicantEduBackground]
   @ApplicantId uniqueidentifier
   --,@HighestEducationalInstitution int
   ,@HighestEducationalInstitution nvarchar(256)
   ,@MajorCategory int
   ,@Major nvarchar(256)
   ,@YearOfStudy int
   ,@Rank int
   ,@ResumeId int
   ,@ResumeImage image
   ,@ResumeExt nvarchar(10)
   ,@PaperAId int
   ,@PaperBId int
   ,@GraduatedDate datetime
   ,@AdvisorFirstName nvarchar(256)
   ,@AdvisorLastName nvarchar(256)
   ,@AdvisorFullName nvarchar(256)
   ,@AdvisorEmail nvarchar(256)
   ,@AdvisorOrganization nvarchar(256)
   ,@ResearchApproach int
   ,@Degree int
   ,@EnrollDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
INSERT INTO [dbo].[sf_ApplicantEduBackground]
           ([ApplicantId]
           ,[HighestEducationalInstitution]
           ,[MajorCategory]
           ,[Major]
           ,[YearOfStudy]
           ,[Rank]
           ,[ResumeId]
		   ,[ResumeImage]
		   ,[ResumeExt]
           ,[PaperAId]
           ,[PaperBId]
           ,[GraduatedDate]
           ,[AdvisorFirstName]
           ,[AdvisorLastName]
           ,[AdvisorFullName]
           ,[AdvisorEmail]
           ,[AdvisorOrganization]
           ,[ResearchApproach]
           ,[Degree]
		   ,[EnrollDate])
     VALUES
           (@ApplicantId
           ,@HighestEducationalInstitution
           ,@MajorCategory
           ,@Major
           ,@YearOfStudy
           ,@Rank
           ,@ResumeId
		   ,@ResumeImage
		   ,@ResumeExt
           ,@PaperAId
           ,@PaperBId
           ,@GraduatedDate
           ,@AdvisorFirstName
           ,@AdvisorLastName
           ,@AdvisorFullName
           ,@AdvisorEmail
           ,@AdvisorOrganization
           ,@ResearchApproach
           ,@Degree
		   ,@EnrollDate)
END








' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_UpdateApplicantEduBackground]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Applicant Education Background
-- Modified: Hui Yang,add resume image and resume extension,2007-3-16
-- =============================================
CREATE PROCEDURE [dbo].[sf_UpdateApplicantEduBackground]
   @ApplicantId uniqueidentifier
   --,@HighestEducationalInstitution int
   ,@HighestEducationalInstitution nvarchar(256)
   ,@MajorCategory int
   ,@Major nvarchar(256)
   ,@YearOfStudy int
   ,@Rank int
   ,@ResumeId int
   ,@ResumeImage image
   ,@ResumeExt nvarchar(10)
   ,@PaperAId int
   ,@PaperBId int
   ,@GraduatedDate datetime
   ,@AdvisorFirstName nvarchar(256)
   ,@AdvisorLastName nvarchar(256)
   ,@AdvisorFullName nvarchar(256)
   ,@AdvisorEmail nvarchar(256)
   ,@AdvisorOrganization nvarchar(256)
   ,@ResearchApproach int
   ,@Degree int
   ,@EnrollDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [dbo].[sf_ApplicantEduBackground]
   SET [ApplicantId] = @ApplicantId
      ,[HighestEducationalInstitution] = @HighestEducationalInstitution
      ,[MajorCategory] = @MajorCategory
      ,[Major] = @Major
      ,[YearOfStudy] = @YearOfStudy
      ,[Rank] = @Rank
      ,[ResumeId] = @ResumeId
	  ,[ResumeImage] = @ResumeImage
	  ,[ResumeExt] = @ResumeExt
      ,[PaperAId] = @PaperAId
      ,[PaperBId] = @PaperBId
      ,[GraduatedDate] = @GraduatedDate
      ,[AdvisorFirstName] = @AdvisorFirstName
      ,[AdvisorLastName] = @AdvisorLastName
      ,[AdvisorFullName] = @AdvisorFullName
      ,[AdvisorEmail] = @AdvisorEmail
      ,[AdvisorOrganization] = @AdvisorOrganization
      ,[ResearchApproach] = @ResearchApproach
      ,[Degree] = @Degree
	  ,[EnrollDate] = @EnrollDate
 WHERE ApplicantId = @ApplicantId

END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_ZZImportResume]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Update Applicant Education Background
-- Modified: Hui Yang,add resume image and resume extension,2007-3-16
-- =============================================
create PROCEDURE [dbo].[sf_ZZImportResume]
   @ApplicantId uniqueidentifier
   ,@ResumeImage image
   ,@ResumeExt nvarchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [dbo].[sf_ApplicantEduBackground]
   SET 
	   [ResumeImage] = @ResumeImage
	  ,[ResumeExt] = @ResumeExt
 WHERE ApplicantId = @ApplicantId

END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Users_DeleteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_MembershipUsers'') AND (type = ''V''))))
    BEGIN
        DELETE FROM dbo.aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_UsersInRoles'') AND (type = ''V''))) )
    BEGIN
        DELETE FROM dbo.aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_Profiles'') AND (type = ''V''))) )
    BEGIN
        DELETE FROM dbo.aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_WebPartState_User'') AND (type = ''V''))) )
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM dbo.aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM dbo.aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Applications_CreateApplication]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM dbo.aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  dbo.aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_AnyDataInTables]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_MembershipUsers'') AND (type = ''V''))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Membership))
        BEGIN
            SELECT N''aspnet_Membership''
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_Roles'') AND (type = ''V''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM dbo.aspnet_Roles))
        BEGIN
            SELECT N''aspnet_Roles''
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_Profiles'') AND (type = ''V''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Profile))
        BEGIN
            SELECT N''aspnet_Profile''
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''vw_aspnet_WebPartState_User'') AND (type = ''V''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N''aspnet_PersonalizationPerUser''
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N''aspnet_WebEvent_LogEvent'') AND (type = ''P''))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM dbo.aspnet_WebEvent_Events))
        BEGIN
            SELECT N''aspnet_WebEvent_Events''
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM dbo.aspnet_Users))
        BEGIN
            SELECT N''aspnet_Users''
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM dbo.aspnet_Applications))
        BEGIN
            SELECT N''aspnet_Applications''
            RETURN
        END
    END
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_Applications]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_Applications]
  AS SELECT [dbo].[aspnet_Applications].[ApplicationName], [dbo].[aspnet_Applications].[LoweredApplicationName], [dbo].[aspnet_Applications].[ApplicationId], [dbo].[aspnet_Applications].[Description]
  FROM [dbo].[aspnet_Applications]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetUserByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, @CurrentTimeUtc, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Personalization_GetApplicationId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Personalization_GetApplicationId] (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_GetProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_GetProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    dbo.aspnet_Users u, dbo.aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetAllUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_FindUsersByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_FindUsersByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM dbo.aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   dbo.aspnet_Users u, dbo.aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   dbo.aspnet_Membership m, dbo.aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetNumberOfUsersOnline]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    dbo.aspnet_Users u(NOLOCK),
            dbo.aspnet_Applications a(NOLOCK),
            dbo.aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Membership m, dbo.aspnet_Users u, dbo.aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE dbo.aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetUserByEmail]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_ResetPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM dbo.aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_UpdateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE dbo.aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE dbo.aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_UnlockUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 ),
        LastLockoutDate = CONVERT( datetime, ''17540101'', 112 )
    WHERE @UserId = UserId

    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_UpdateUserInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            SET @LastLockoutDate = CONVERT( datetime, ''17540101'', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE dbo.aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetPasswordWithFormat]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  dbo.aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  dbo.aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_SetPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    dbo.aspnet_Users u, dbo.aspnet_Applications a, dbo.aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE dbo.aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetPassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    dbo.aspnet_Applications a, dbo.aspnet_Users u, dbo.aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )
            END
        END

        UPDATE dbo.aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_MembershipUsers]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_MembershipUsers]
  AS SELECT [dbo].[aspnet_Membership].[UserId],
            [dbo].[aspnet_Membership].[PasswordFormat],
            [dbo].[aspnet_Membership].[MobilePIN],
            [dbo].[aspnet_Membership].[Email],
            [dbo].[aspnet_Membership].[LoweredEmail],
            [dbo].[aspnet_Membership].[PasswordQuestion],
            [dbo].[aspnet_Membership].[PasswordAnswer],
            [dbo].[aspnet_Membership].[IsApproved],
            [dbo].[aspnet_Membership].[IsLockedOut],
            [dbo].[aspnet_Membership].[CreateDate],
            [dbo].[aspnet_Membership].[LastLoginDate],
            [dbo].[aspnet_Membership].[LastPasswordChangedDate],
            [dbo].[aspnet_Membership].[LastLockoutDate],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAttemptWindowStart],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptCount],
            [dbo].[aspnet_Membership].[FailedPasswordAnswerAttemptWindowStart],
            [dbo].[aspnet_Membership].[Comment],
            [dbo].[aspnet_Users].[ApplicationId],
            [dbo].[aspnet_Users].[UserName],
            [dbo].[aspnet_Users].[MobileAlias],
            [dbo].[aspnet_Users].[IsAnonymous],
            [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Membership] INNER JOIN [dbo].[aspnet_Users]
      ON [dbo].[aspnet_Membership].[UserId] = [dbo].[aspnet_Users].[UserId]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_GetUserByUserId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   dbo.aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     dbo.aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    dbo.aspnet_Users u, dbo.aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_aspnet_Users]'))
EXEC dbo.sp_executesql @statement = N'
  CREATE VIEW [dbo].[vw_aspnet_Users]
  AS SELECT [dbo].[aspnet_Users].[ApplicationId], [dbo].[aspnet_Users].[UserId], [dbo].[aspnet_Users].[UserName], [dbo].[aspnet_Users].[LoweredUserName], [dbo].[aspnet_Users].[MobileAlias], [dbo].[aspnet_Users].[IsAnonymous], [dbo].[aspnet_Users].[LastActivityDate]
  FROM [dbo].[aspnet_Users]
  ' 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Users_CreateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM dbo.aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT dbo.aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_RegisterSchemaVersion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM dbo.aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE dbo.aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  dbo.aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_CheckSchemaVersion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    dbo.aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_UnRegisterSchemaVersion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM dbo.aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetInterviewForCurrentUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'










-- =============================================
-- Author:		Yuan Chen
-- Create date: 16/5/2006
-- Description:	Get Interview For Current User
-- Modified by: Hui Yang
-- Modified date: 3/29/2007
-- updated date:2007-04-09 interviewer can see the related applicants.
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetInterviewForCurrentUser]
	@HiringManagerId uniqueidentifier,
	@alias nvarchar(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 
		distinct sf_vw_AllApplicants.*, sf_interview.*,sf_Referral.ReferralType
	FROM
		sf_vw_AllApplicants INNER JOIN sf_Interview
	ON sf_Interview.ApplicantId = sf_vw_AllApplicants.ApplicantId
	left join sf_Referral on sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId
	left join sf_feedback on sf_vw_AllApplicants.applicantid= sf_Feedback.ApplicantId
	WHERE
		(sf_Interview.HiringManagerId = @HiringManagerId or sf_Interview.GroupManagerId = @HiringManagerId or (sf_interview.interviewid=sf_feedback.interviewid and sf_feedback.InterviewerAlias=@alias))
		AND sf_Interview.InterviewStatus < 3 
	ORDER BY StartDate DESC
END













' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_SetProperties]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_SetProperties]
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   dbo.aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE dbo.aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   dbo.aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE dbo.aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO dbo.aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Roles_CreateRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM dbo.aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO dbo.aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_GetCountOfState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_GetCountOfState] (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_ResetUserState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetUserState] (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_ResetSharedState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetSharedState] (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM dbo.aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_DeleteAllState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_DeleteAllState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM dbo.aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser_SetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC dbo.aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM dbo.aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE dbo.aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO dbo.aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser_GetPageSettings]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM dbo.aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM dbo.aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   dbo.aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM dbo.aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetAllApplicants]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'













-- =============================================
-- Author: Chen Yuan
-- Create date: Apr 28, 2006
-- Description:	Get all applicants to show on the main page
-- updated:2007-04-19,suit for more than one interview.
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetAllApplicants] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		sf_vw_AllApplicants.*,
		sf_Referral.ReferralType,sf_interview.InterviewStatus
	FROM 
		(sf_vw_AllApplicants LEFT JOIN sf_Referral
		ON sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId)
		left join (select sf_interview.* from sf_interview,(select applicantid,max(startdate) as startdate
 from sf_interview group by applicantid)a where sf_interview.applicantid=a.applicantid
and a.startdate=sf_interview.startdate)sf_interview on sf_vw_AllApplicants.applicantid=sf_interview.applicantid
	ORDER BY
		ReferralType DESC, ApplicationDate DESC
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetFavoriteForOwner]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Yuan Chen
-- Create date: 2/5/2006
-- Description:	Select Favorite For Owner
-- Modifier: Hui Yang
-- Modify Date: 2006-11-13
-- Modified Date:2007-03-29 for unify the display list.
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetFavoriteForOwner]
	@OwnerId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
SELECT 
		sf_vw_AllApplicants.*, sf_Favorites.*,sf_Referral.ReferralType
	FROM
		sf_vw_AllApplicants INNER JOIN [sf_Favorites]
	ON [sf_Favorites].ApplicantId = sf_vw_AllApplicants.ApplicantId
	left join sf_Referral on sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId
WHERE [OwnerId] = @OwnerId
--	SELECT 
--		FavoriteId
--		,sf_Favorites.ApplicantId
--		,FirstName
--		,LastName
--	FROM [dbo].[sf_Favorites] inner JOIN [dbo].[sf_ApplicantBasicInfo] 
--	ON [dbo].[sf_Favorites].ApplicantId = [dbo].[sf_ApplicantBasicInfo].ApplicantId
--	WHERE [OwnerId] = @OwnerId
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetGeneralInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'








-- =============================================
-- Author:		Yuan Chen
-- Create date: 23/5/2006
-- Description:	Get Site''s General Information
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetGeneralInfo]
	@UserAlias nvarchar(256)
	,@UserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @p1 int
	declare @p2 int
    -- Insert statements for procedure here
	SELECT 
		COUNT(sf_vw_AllApplicants.ApplicantId) 
	AS
		ApplicationCount
	From
		sf_vw_AllApplicants left join sf_Referral
		ON sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId
	
	SELECT 
		ProcssingCount=count(distinct sf_Interview.applicantid)
	FROM
		sf_vw_AllApplicants INNER JOIN sf_Interview
	ON sf_Interview.ApplicantId = sf_vw_AllApplicants.ApplicantId
	left join sf_Referral on sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId
	inner join sf_feedback on sf_vw_AllApplicants.applicantid= sf_Feedback.ApplicantId
	WHERE
		(sf_Interview.HiringManagerId = @UserId or sf_Interview.GroupManagerId = @UserId or (sf_interview.interviewid=sf_feedback.interviewid and sf_feedback.InterviewerAlias=@UserAlias))
		AND sf_Interview.InterviewStatus < 3 
	
	SELECT
		COUNT(FeedbackId)
	AS
		UserFeedbackCount
	FROM
		sf_Feedback
	WHERE
		[InterviewerAlias] = @UserAlias
	AND IsComplete = 0

	SELECT
		COUNT(InterviewId)
	AS
		DecisionCount
	FROM
		sf_Interview
	WHERE
		[InterviewStatus] in(3,4,5)

	SELECT
		COUNT(FeedbackId)
	AS
		CompleteFeedbackCount
	FROM
		sf_Feedback
	WHERE
		IsComplete = 1

	SELECT
		COUNT(FeedbackId)
	AS
		IncompleteFeedbackCount
	FROM
		sf_Feedback
	WHERE
		IsComplete = 0

	SELECT
		COUNT(InterviewId)
	AS
		HiredInterviewCount
	FROM
		sf_Interview
	WHERE
		InterviewStatus = 3

	SELECT
		COUNT(InterviewId)
	AS
		RejectedInterviewCount
	FROM
		sf_Interview
	WHERE
		InterviewStatus = 4

	SELECT
		COUNT(InterviewId)
	AS
		CompleteInterviewCount
	FROM
		sf_Interview
	WHERE
		InterviewStatus >= 3
	
END












' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_GetAllProcessingInterview]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Yuan Chen
-- Create date: 2006.8.24
-- Description:	Get All Processing Interview
-- modified by Hui Yang,2007-04-01
-- =============================================
CREATE PROCEDURE [dbo].[sf_GetAllProcessingInterview]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		sf_vw_AllApplicants.*,sf_Referral.ReferralType 
	FROM
		sf_vw_AllApplicants
		
	inner JOIN
		sf_Interview
	ON
		sf_Interview.ApplicantId = sf_vw_AllApplicants.ApplicantId
	LEFT JOIN sf_Referral
		ON sf_vw_AllApplicants.ReferralId = sf_Referral.ReferralId
	WHERE 
		InterviewStatus < 3 order by applicationdate desc
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAdministration_FindState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_FindState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC dbo.aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM dbo.aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM dbo.aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM dbo.aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM dbo.aspnet_PersonalizationPerUser PerUser, dbo.aspnet_Users Users, dbo.aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Membership_CreateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, ''17540101'', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, ''17540101'', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC dbo.aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM dbo.aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = dbo.aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   dbo.aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  dbo.aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE dbo.aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO dbo.aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[aspnet_Profile_DeleteProfiles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteProfiles]
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N'','', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC dbo.aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sf_SearchApplicantsByResume]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Hui Yang	
-- Create date: 3/27/2007
-- Description:	get applicant by resume
-- =============================================
CREATE PROCEDURE [dbo].[sf_SearchApplicantsByResume]
	 @Condition varchar(1000)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if ((@Condition is null) OR (@Condition=''''))
	exec sf_getallapplicants
	else
	SELECT     TOP (100) PERCENT dbo.sf_ApplicantBasicInfo.ApplicantId, dbo.sf_ApplicantEduBackground.Major, dbo.sf_ApplicantBasicInfo.Status, 
                      dbo.sf_ApplicantRelatedInfo.InterestedAreas, dbo.sf_ApplicantRelatedInfo.PreferredStartDate, dbo.sf_ApplicantRelatedInfo.SecondaryStartDate, 
                      dbo.sf_ApplicantBasicInfo.FirstName, dbo.sf_ApplicantBasicInfo.LastName, dbo.sf_ApplicantEduBackground.MajorCategory, 
                      dbo.sf_ApplicantEduBackground.Degree, dbo.sf_ApplicantEduBackground.HighestEducationalInstitution, dbo.sf_ApplicantBasicInfo.ReferralId, 
                      dbo.sf_ApplicantBasicInfo.Priority, dbo.sf_ApplicantBasicInfo.ApplicationDate, dbo.sf_ApplicantRelatedInfo.InterestedGroup, 
                      dbo.sf_ApplicantRelatedInfo.JobInfoSource, dbo.sf_ApplicantBasicInfo.NameInChinese,dbo.sf_ApplicantBasicInfo.FirstName + '' ''+dbo.sf_ApplicantBasicInfo.LastName+'' ''+dbo.sf_ApplicantBasicInfo.NameInChinese as FullName,sf_Referral.ReferralType,sf_interview.InterviewStatus
FROM         dbo.sf_ApplicantBasicInfo INNER JOIN
                      dbo.sf_ApplicantRelatedInfo INNER JOIN
                      dbo.sf_ApplicantEduBackground ON dbo.sf_ApplicantRelatedInfo.ApplicantId = dbo.sf_ApplicantEduBackground.ApplicantId ON 
                      dbo.sf_ApplicantBasicInfo.ApplicantId = dbo.sf_ApplicantRelatedInfo.ApplicantId
					  left join sf_Referral on sf_ApplicantBasicInfo.ReferralId = sf_Referral.ReferralId
					  left join sf_interview on sf_ApplicantBasicInfo.applicantid=sf_interview.applicantid
	where freetext(ResumeImage, @Condition)
ORDER BY sf_Referral.ReferralType DESC, dbo.sf_ApplicantBasicInfo.ApplicationDate DESC

END





' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__RoleI__1DB06A4F]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[aspnet_Roles] ([RoleId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__UserI__1CBC4616]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_UsersInRoles]'))
ALTER TABLE [dbo].[aspnet_UsersInRoles]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__31B762FC]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationAllUsers]'))
ALTER TABLE [dbo].[aspnet_PersonalizationAllUsers]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__PathI__3587F3E0]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]'))
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([PathId])
REFERENCES [dbo].[aspnet_Paths] ([PathId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pe__UserI__367C1819]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_PersonalizationPerUser]'))
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pa__Appli__2BFE89A6]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Paths]'))
ALTER TABLE [dbo].[aspnet_Paths]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__Appli__7A672E12]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Me__UserI__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Membership]'))
ALTER TABLE [dbo].[aspnet_Membership]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Us__Appli__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Users]'))
ALTER TABLE [dbo].[aspnet_Users]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Ro__Appli__18EBB532]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Roles]'))
ALTER TABLE [dbo].[aspnet_Roles]  WITH CHECK ADD FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[aspnet_Applications] ([ApplicationId])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__aspnet_Pr__UserI__0F624AF8]') AND parent_object_id = OBJECT_ID(N'[dbo].[aspnet_Profile]'))
ALTER TABLE [dbo].[aspnet_Profile]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
