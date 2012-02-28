/****** Object:  Table [RBEPortal].[Request]    ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [RBEPortal].[Request](
	[RequestId] [uniqueidentifier] NOT NULL,
	[ResourceId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Location] [nvarchar](4000) NOT NULL,
	[Amount] [float](53) NOT NULL,
	[UoM] [nvarchar](80) NOT NULL,
	[Status] [nvarchar](80) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED
(
	[RequestId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [RBEPortal].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Resource] FOREIGN KEY([ResourceId])
REFERENCES [RBEPortal].[Resource] ([ResourceId])
ALTER TABLE [RBEPortal].[Request] CHECK CONSTRAINT [FK_Request_Resource]

ALTER TABLE [RBEPortal].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [RBEPortal].[Request] CHECK CONSTRAINT [FK_Request_User]

ALTER TABLE [RBEPortal].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [RBEPortal].[Request] CHECK CONSTRAINT [FK_Request_ModifiedBy]


/****** Object:  Table [RBEPortal].[Share]    ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [RBEPortal].[Share](
	[ShareId] [uniqueidentifier] NOT NULL,
	[ResourceId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Location] [nvarchar](4000) NOT NULL,
	[Amount] [float](53) NOT NULL,
	[UoM] [nvarchar](80) NOT NULL,
	[Status] [nvarchar](80) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Share] PRIMARY KEY CLUSTERED
(
	[ShareId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [RBEPortal].[Share]  WITH CHECK ADD  CONSTRAINT [FK_Share_Resource] FOREIGN KEY([ResourceId])
REFERENCES [RBEPortal].[Resource] ([ResourceId])
ALTER TABLE [RBEPortal].[Share] CHECK CONSTRAINT [FK_Share_Resource]

ALTER TABLE [RBEPortal].[Share]  WITH CHECK ADD  CONSTRAINT [FK_Share_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [RBEPortal].[Share] CHECK CONSTRAINT [FK_Share_User]

ALTER TABLE [RBEPortal].[Share]  WITH CHECK ADD  CONSTRAINT [FK_Share_ModifiedBy] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[aspnet_Users] ([UserId])
ALTER TABLE [RBEPortal].[Share] CHECK CONSTRAINT [FK_Share_ModifiedBy]
