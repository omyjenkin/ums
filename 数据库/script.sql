USE [UMSDB]
GO
/****** Object:  Table [dbo].[SysLog]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysLog](
	[Id] [uniqueidentifier] NOT NULL,
	[Category] [varchar](50) NULL,
	[ErrorUrl] [varchar](100) NULL,
	[Message] [varchar](1000) NULL,
	[Exception] [varchar](8000) NULL,
	[Method] [varchar](100) NULL,
	[Line] [varchar](50) NULL,
	[Params] [varchar](5000) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_SysLog_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysLoginInfo]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysLoginInfo](
	[Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LoginToken] [uniqueidentifier] NOT NULL,
	[LastAccessTime] [datetime] NOT NULL,
	[SysUserId] [varchar](50) NOT NULL,
	[LoginName] [varchar](50) NOT NULL,
	[BusinessPermissionString] [varchar](4000) NULL,
	[ClientIP] [varchar](90) NULL,
	[EnumLoginAccountType] [int] NOT NULL,
 CONSTRAINT [PK_SysLoginInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysModule]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysModule](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[EnglishName] [varchar](200) NULL,
	[ParentId] [varchar](50) NOT NULL,
	[Url] [varchar](200) NULL,
	[Iconic] [varchar](200) NULL,
	[Sort] [int] NULL,
	[Remark] [varchar](200) NULL,
	[Enable] [bit] NOT NULL,
	[CreateUser] [varchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[IsLast] [bit] NOT NULL,
 CONSTRAINT [PK_SysModule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysModuleOperate]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysModuleOperate](
	[Id] [varchar](100) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[KeyCode] [varchar](200) NOT NULL,
	[ModuleId] [varchar](50) NOT NULL,
	[IsValid] [bit] NOT NULL,
	[Sort] [int] NOT NULL,
 CONSTRAINT [PK_SysModuleOperate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysRight]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRight](
	[Id] [varchar](100) NOT NULL,
	[RoleId] [varchar](50) NULL,
	[ModuleId] [varchar](50) NULL,
	[Rightflag] [bit] NOT NULL,
 CONSTRAINT [PK_SysRight_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysRightOperate]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRightOperate](
	[Id] [varchar](200) NOT NULL,
	[RightId] [varchar](100) NOT NULL,
	[KeyCode] [varchar](100) NOT NULL,
	[IsValid] [bit] NOT NULL,
 CONSTRAINT [PK_SysRightOperate_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRole](
	[Id] [varchar](50) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [varchar](4000) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateUser] [varchar](200) NOT NULL,
 CONSTRAINT [PK_SysRole_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[Id] [varchar](50) NOT NULL,
	[UserName] [varchar](200) NOT NULL,
	[Password] [varchar](200) NOT NULL,
	[TrueName] [varchar](200) NULL,
	[Cards] [varchar](50) NULL,
	[MobileNumber] [varchar](200) NULL,
	[PhoneNumber] [varchar](200) NULL,
	[QQ] [varchar](50) NULL,
	[EmailAddress] [varchar](200) NULL,
	[OtherContact] [varchar](200) NULL,
	[Province] [varchar](200) NULL,
	[City] [varchar](200) NULL,
	[Village] [varchar](200) NULL,
	[Address] [varchar](200) NULL,
	[State] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUser] [varchar](200) NULL,
	[Sex] [varchar](10) NULL,
	[Birthday] [datetime] NULL,
	[JoinDate] [datetime] NULL,
	[Marital] [varchar](10) NULL,
	[Political] [varchar](50) NULL,
	[Nationality] [varchar](20) NULL,
	[Native] [varchar](20) NULL,
	[School] [varchar](50) NULL,
	[Professional] [varchar](100) NULL,
	[Degree] [varchar](20) NULL,
	[Expertise] [varchar](3000) NULL,
	[JobState] [varchar](20) NULL,
	[Photo] [varchar](200) NULL,
	[Attach] [varchar](200) NULL,
 CONSTRAINT [PK_SysUser_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUserRole]    Script Date: 2018/11/22 16:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUserRole](
	[Id] [uniqueidentifier] NOT NULL,
	[SysUserId] [varchar](50) NOT NULL,
	[SysRoleId] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SysUserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SysLog] ADD  CONSTRAINT [DF_SysLog_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[SysLoginInfo] ADD  CONSTRAINT [DF_SysLoginInfo_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
ALTER TABLE [dbo].[SysLoginInfo] ADD  CONSTRAINT [DF__SysLoginI__EnumL__25869641]  DEFAULT ((0)) FOR [EnumLoginAccountType]
GO
ALTER TABLE [dbo].[SysModule] ADD  CONSTRAINT [DF_SysModule_Id]  DEFAULT (newid()) FOR [Id]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'MobileNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'婚姻' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Marital'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'党派' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Political'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'民族' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Nationality'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'籍贯' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Native'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'毕业学校' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'School'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'就读专业' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Professional'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'学历' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Degree'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'个人简介' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Expertise'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'在职状况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'JobState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'照片' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Photo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'附件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Attach'
GO
