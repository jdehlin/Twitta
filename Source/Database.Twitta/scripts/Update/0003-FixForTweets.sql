/****** Object:  Table [dbo].[Tweets]    Script Date: 03/01/2014 10:14:21 ******/

CREATE TABLE [dbo].[Tweets](
	[TweetId] [bigint] NOT NULL,
	[IdStr] [varchar](500) NOT NULL,
	[TwitterUserId] [bigint] NULL,
	[InReplyToScreenName] [varchar](500) NULL,
	[InReplyToStatusId] [bigint] NULL,
	[InReplyToUserId] [bigint] NOT NULL,
	[IsFavorited] [bit] NOT NULL,
	[IsTruncated] [bit] NOT NULL,
	[Source] [varchar](500) NULL,
	[Text] [varchar](500) NULL,
	[Language] [varchar](500) NULL,
	[IsPossiblySensitive] [bit] NULL,
	[RetweetCount] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[SearchId] [bigint] NULL,
 CONSTRAINT [PK_Tweets] PRIMARY KEY CLUSTERED 
(
	[TweetId] ASC
) 
) ON [PRIMARY]

GO