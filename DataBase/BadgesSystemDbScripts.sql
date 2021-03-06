USE [BadgesSystemDb]
GO
/****** Object:  Table [dbo].[BadgesGifts]    Script Date: 11/27/2014 11:57:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BadgesGifts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[FlagType] [bit] NOT NULL,
	[FileID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Badges_Gifts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BadgeTags]    Script Date: 11/27/2014 11:57:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BadgeTags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TagID] [int] NOT NULL,
	[BadgesGiftID] [int] NOT NULL,
 CONSTRAINT [PK_Badge_Tags] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Files]    Script Date: 11/27/2014 11:57:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Files](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 11/27/2014 11:57:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserBadges]    Script Date: 11/27/2014 11:57:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBadges](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SenderID] [int] NOT NULL,
	[RecipientID] [int] NULL,
	[BadgesGiftID] [int] NOT NULL,
	[GiveDate] [datetime2](7) NULL,
	[Reason] [nvarchar](2000) NULL,
 CONSTRAINT [PK_User_Badges] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/27/2014 11:57:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[UserRole] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BadgesGifts]  WITH CHECK ADD  CONSTRAINT [FK_Badges_Gifts_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[BadgesGifts] CHECK CONSTRAINT [FK_Badges_Gifts_Users]
GO
ALTER TABLE [dbo].[BadgesGifts]  WITH CHECK ADD  CONSTRAINT [FK_BadgesGifts_Files] FOREIGN KEY([FileID])
REFERENCES [dbo].[Files] ([ID])
GO
ALTER TABLE [dbo].[BadgesGifts] CHECK CONSTRAINT [FK_BadgesGifts_Files]
GO
ALTER TABLE [dbo].[BadgeTags]  WITH CHECK ADD  CONSTRAINT [FK_Badge_Tags_Badges_Gifts] FOREIGN KEY([BadgesGiftID])
REFERENCES [dbo].[BadgesGifts] ([ID])
GO
ALTER TABLE [dbo].[BadgeTags] CHECK CONSTRAINT [FK_Badge_Tags_Badges_Gifts]
GO
ALTER TABLE [dbo].[BadgeTags]  WITH CHECK ADD  CONSTRAINT [FK_Badge_Tags_Tags] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tags] ([ID])
GO
ALTER TABLE [dbo].[BadgeTags] CHECK CONSTRAINT [FK_Badge_Tags_Tags]
GO
ALTER TABLE [dbo].[UserBadges]  WITH CHECK ADD  CONSTRAINT [FK_User_Badges_Badges_Gifts] FOREIGN KEY([BadgesGiftID])
REFERENCES [dbo].[BadgesGifts] ([ID])
GO
ALTER TABLE [dbo].[UserBadges] CHECK CONSTRAINT [FK_User_Badges_Badges_Gifts]
GO
ALTER TABLE [dbo].[UserBadges]  WITH CHECK ADD  CONSTRAINT [FK_UserBadges_Users] FOREIGN KEY([SenderID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserBadges] CHECK CONSTRAINT [FK_UserBadges_Users]
GO
ALTER TABLE [dbo].[UserBadges]  WITH CHECK ADD  CONSTRAINT [FK_UserBadges_Users1] FOREIGN KEY([RecipientID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserBadges] CHECK CONSTRAINT [FK_UserBadges_Users1]
GO
