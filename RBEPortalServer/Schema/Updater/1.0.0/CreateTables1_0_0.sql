IF SCHEMA_ID('RBEPortal') IS NULL
  EXEC('CREATE SCHEMA RBEPortal AUTHORIZATION dbo')


/****** Object:  Table [RBEPortal].[User]    ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

/****** Object:  Table [RBEPortal].[Log]    ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [RBEPortal].[Log](
	[LogId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[Status] [nvarchar](80) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  ForeignKey [FK_Log_User]    ******/
ALTER TABLE [RBEPortal].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [RBEPortal].[Log] CHECK CONSTRAINT [FK_Log_User]

/****** Object:  Table [RBEPortal].[Resource]    ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [RBEPortal].[Resource](
	[ResourceId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Status] [nvarchar](80) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED
(
	[ResourceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [RBEPortal].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_User] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [RBEPortal].[Resource] CHECK CONSTRAINT [FK_Resource_User]
