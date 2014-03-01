/****** Object:  Table [dbo].[TwitterApps]    Script Date: 03/01/2014 09:13:08 ******/

CREATE TABLE [dbo].[TwitterApps](
	[ApiApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[AppName] [nvarchar](100) NOT NULL,
	[ConsumerKey] [nvarchar](100) NOT NULL,
	[ConsumerKeySecret] [nvarchar](100) NOT NULL,
	[Token] [nvarchar](100) NULL,
	[TokenSecret] [nvarchar](100) NULL,
	[LastAccessedDTM] [date] NULL,
	[RateLimitRemaining] [int] NOT NULL,
 CONSTRAINT [PK_ApiApplications] PRIMARY KEY CLUSTERED 
(
	[ApiApplicationId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TwitterApps] ADD  CONSTRAINT [DF_TwitterApps_RateLimitRemaining]  DEFAULT ((180)) FOR [RateLimitRemaining]
GO

/****** Object:  Table [dbo].[Searches]    Script Date: 03/01/2014 09:25:24 ******/

CREATE TABLE [dbo].[Searches](
	[SearchId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[AllOfTheseWords] [nvarchar](200) NULL,
	[ThisExactPhrase] [nvarchar](200) NULL,
	[AnyOfTheseWords] [nvarchar](200) NULL,
	[NoneOfTheseWords] [nvarchar](200) NULL,
	[NearThisPlace] [nvarchar](200) NULL,
	[Radius] [int] NULL,
	[ResultType] [int] NOT NULL,
 CONSTRAINT [PK_Searches] PRIMARY KEY CLUSTERED 
(
	[SearchId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[ApiAccessHistoryLogs]    Script Date: 03/01/2014 09:27:48 ******/

CREATE TABLE [dbo].[ApiAccessHistoryLogs](
	[ApiAccessHistoryLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[ApiApplicationId] [int] NOT NULL,
	[AccessTime] [datetime] NOT NULL,
 CONSTRAINT [PK_ApiAccessHistoryLogs] PRIMARY KEY CLUSTERED 
(
	[ApiAccessHistoryLogId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ApiAccessHistoryLogs]  WITH CHECK ADD  CONSTRAINT [FK_ApiAccessHistoryLogs_ApiApplications] FOREIGN KEY([ApiApplicationId])
REFERENCES [dbo].[TwitterApps] ([ApiApplicationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ApiAccessHistoryLogs] CHECK CONSTRAINT [FK_ApiAccessHistoryLogs_ApiApplications]
GO

/****** Object:  Table [dbo].[HashTags]    Script Date: 03/01/2014 09:28:43 ******/

CREATE TABLE [dbo].[HashTags](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](500) NOT NULL,
 CONSTRAINT [PK_HashTags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[IgnoreWords]    Script Date: 03/01/2014 09:29:38 ******/

CREATE TABLE [dbo].[IgnoreWords](
	[IgnoreWordId] [int] IDENTITY(1,1) NOT NULL,
	[WordText] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_IgnoreWords] PRIMARY KEY CLUSTERED 
(
	[IgnoreWordId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[SearchHistoryLogs]    Script Date: 03/01/2014 09:31:10 ******/

CREATE TABLE [dbo].[SearchHistoryLogs](
	[SearchHistoryLogId] [bigint] IDENTITY(1,1) NOT NULL,
	[SearchId] [bigint] NOT NULL,
	[SearchDate] [datetime] NOT NULL,
	[LastTweetId] [bigint] NOT NULL,
	[TweetCount] [int] NOT NULL,
 CONSTRAINT [PK_SearchHistoryLogs] PRIMARY KEY CLUSTERED 
(
	[SearchHistoryLogId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SearchHistoryLogs]  WITH CHECK ADD  CONSTRAINT [FK_SearchHistoryLogs_Searches] FOREIGN KEY([SearchId])
REFERENCES [dbo].[Searches] ([SearchId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SearchHistoryLogs] CHECK CONSTRAINT [FK_SearchHistoryLogs_Searches]
GO

/****** Object:  Table [dbo].[TwitterStatusesHashTags]    Script Date: 03/01/2014 09:35:30 ******/

CREATE TABLE [dbo].[TwitterStatusesHashTags](
	[TwitterStatusId] [bigint] NOT NULL,
	[HashTagId] [bigint] NOT NULL,
 CONSTRAINT [PK_TwitterStatusesHashTags] PRIMARY KEY CLUSTERED 
(
	[TwitterStatusId] ASC,
	[HashTagId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Users]    Script Date: 03/01/2014 09:36:30 ******/

CREATE TABLE [dbo].[Users](
	[UserId] [bigint] NOT NULL,
	[ScreenName] [varchar](500) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Description] [varchar](500) NULL,
	[FavoritesCount] [int] NOT NULL,
	[FollowersCount] [int] NOT NULL,
	[FriendsCount] [int] NOT NULL,
	[IsVerified] [bit] NULL,
	[Language] [varchar](500) NULL,
	[ListedCount] [int] NOT NULL,
	[ProfileImageUrl] [varchar](500) NULL,
	[Name] [varchar](500) NULL,
	[TimeZone] [varchar](500) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
) ON [PRIMARY]
) ON [PRIMARY]

GO