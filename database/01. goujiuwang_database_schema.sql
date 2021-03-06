USE [goujiuwang]
GO
/****** Object:  Table [dbo].[Order_Display_Reply]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Display_Reply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDisplayID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](250) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Display_Reply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'OrderDisplayID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父回复编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论回复内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论回复状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单回复表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply'
GO
/****** Object:  Table [dbo].[Order_Display_Image]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Display_Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DisplayOrderID] [int] NOT NULL,
	[URL] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Order_Display_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image', @level2type=N'COLUMN',@level2name=N'DisplayOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image'
GO
/****** Object:  Table [dbo].[Order_Display]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Display](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Content] [nvarchar](250) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Display] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（0：未审核，1：已通过，2：已锁定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display'
GO
/****** Object:  Table [dbo].[Group_Purchase_Subscribe]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group_Purchase_Subscribe](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](100) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Group_Purchase_Subscribe] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe', @level2type=N'COLUMN',@level2name=N'EmailAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动订阅表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe'
GO
/****** Object:  Table [dbo].[Picture_Category]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Picture_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Picture_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父图片类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片类别名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片类别表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category'
GO
/****** Object:  Table [dbo].[Config_Payment_Platform]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Payment_Platform](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[URL] [nvarchar](500) NOT NULL,
	[ImageURL] [nvarchar](100) NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment_Platform] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Platform', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付平台名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Platform', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台支付网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Platform', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示图片网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Platform', @level2type=N'COLUMN',@level2name=N'ImageURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Platform', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Platform', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付平台表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Platform'
GO
/****** Object:  Table [dbo].[Config_Payment_Method]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Payment_Method](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment_Method] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Method', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Method', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Method', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Method'
GO
/****** Object:  Table [dbo].[Config_Invoice_Type]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Invoice_Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Invoice_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票类型描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票类型表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type'
GO
/****** Object:  Table [dbo].[Config_Invoice_Content]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Invoice_Content](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Invoice_Content] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票内容名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票内容描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票内容表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content'
GO
/****** Object:  Table [dbo].[Config_Delivery_Method]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Delivery_Method](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Delivery_Method] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货方式名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货方式描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货方式表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method'
GO
/****** Object:  Table [dbo].[Channel_SmallLiquor_Product]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel_SmallLiquor_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Channel_SmallLiquor_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_SmallLiquor_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'我爱小酒版商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_SmallLiquor_Product'
GO
/****** Object:  Table [dbo].[Channel_SmallLiquor_Image]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel_SmallLiquor_Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Channel_SmallLiquor_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_SmallLiquor_Image', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'我爱小酒版图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_SmallLiquor_Image'
GO
/****** Object:  Table [dbo].[Channel_SmallLiquor]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel_SmallLiquor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Channel_SmallLiquor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_SmallLiquor', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'我爱小酒版表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_SmallLiquor'
GO
/****** Object:  Table [dbo].[Channel_Appreciate_Product]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel_Appreciate_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Channel_Appreciate_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_Appreciate_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名家鉴赏图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_Appreciate_Product'
GO
/****** Object:  Table [dbo].[Channel_Appreciate_Image]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel_Appreciate_Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Channel_Appreciate_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_Appreciate_Image', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名家鉴赏图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_Appreciate_Image'
GO
/****** Object:  Table [dbo].[Channel_Appreciate]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel_Appreciate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Channel_Appreciate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_Appreciate', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名家鉴赏表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_Appreciate'
GO
/****** Object:  Table [dbo].[Config_Delivery_Corporation]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Delivery_Corporation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[URL] [varchar](500) NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Delivery_Corporation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货公司物流信息查询网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货公司描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货公司表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation'
GO
/****** Object:  Table [dbo].[Cps]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cps](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[URL] [nvarchar](80) NOT NULL,
	[Linkman] [nvarchar](20) NOT NULL,
	[Mobile] [varchar](20) NOT NULL,
	[Tel] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[QQ] [varchar](20) NULL,
	[Company] [nvarchar](50) NOT NULL,
	[CompanyAddress] [nvarchar](50) NOT NULL,
	[ZipCode] [varchar](10) NULL,
	[TrackingURL] [varchar](100) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Cps] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 合作平台名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Linkman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'QQ 号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'QQ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Company'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'CompanyAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮政号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'ZipCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跟踪地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'TrackingURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 合作平台表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps'
GO
/****** Object:  Table [dbo].[Product_Category]    Script Date: 10/18/2013 18:24:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[CategoryNameSpell] [nvarchar](100) NOT NULL,
	[CategoryNameEnglish] [nvarchar](100) NULL,
	[SEOKeywords] [nvarchar](500) NULL,
	[SEODescription] [nvarchar](500) NULL,
	[IsGjw] [bit] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[Layer] [int] NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别名称拼音' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CategoryNameSpell'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别名称英文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CategoryNameEnglish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别 SEO 关键字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'SEOKeywords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别 SEO 描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'SEODescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别是否属于官网' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'IsGjw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别是否显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别所在层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category'
GO
/****** Object:  StoredProcedure [dbo].[sp_Paging]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Paging]
(
	@tableName varchar(100),     -- 表名称
	@pageIndex int,				 -- 页码
	@pageSize int,				 -- 每页大小
	@pageColumn varchar(50),	 -- 分页的列
	@columns varchar(200) = null,       -- 查询的列，已逗号分隔，为空则查询所有列
	@orderBy varchar(200) = null,       -- 排序的列，DESC / ASC
	@condition nvarchar(200) = null,    -- 查询条件
	@totalCount int output       -- 总记录数
)
AS
	-- 判断对象是否存在
	IF OBJECT_ID(@tableName) IS NULL
	BEGIN
		RAISERROR(N'对象不存在', 1, 16, @tableName)
		RETURN
	END
	-- 判断对象类型是否正确
	IF OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTable') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsView') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTableFunction') = 0
	BEGIN
		RAISERROR(N'对象类型不正确，必须为：表、视图或表值函数', 1, 16, @tableName)
		RETURN
	END
	-- 检查分页参数
	IF ISNULL(@pageIndex, 0) < 1 SET @pageIndex = 1
	IF ISNULL(@pageSize, 0) < 1 SET @pageSize = 10
	-- 检查分页的列
	IF ISNULL(@pageColumn, N'') = ''
	BEGIN
		RAISERROR(N'分页字段必须为主键或唯一键', 1, 16)
		RETURN
	END
	-- 检查查询的列
	IF ISNULL(@columns, N'') = N'' SET @columns = N'*'
	-- 检查排序的列
	IF ISNULL(@orderBy, N'') = N'' SET @orderBy = N''
	ELSE SET @orderBy = N' ORDER BY ' + LTRIM(@orderBy)
	-- 检查查询条件
	IF ISNULL(@condition, N'') = N'' SET @condition= N''
	ELSE SET @condition = N' WHERE (' + @condition + N')'
	
	DECLARE @totalCountCommandText nvarchar(1000)
	DECLARE @commandText nvarchar(1000)	
	
	SET @totalCountCommandText = N'SELECT @totalCount = COUNT(' + @pageColumn + N') FROM ' + @tableName + @condition
	EXEC sp_executesql @totalCountCommandText, N'@totalCount int output', @totalCount output
	--SELECT @totalCount as N'@totalCount'
	
	IF @pageIndex = 1
		BEGIN
			EXEC( N'SELECT TOP ' + @pageSize
				+ N' ' + @columns
				+ N' FROM '+ @tableName
				+ N' ' + @condition
				+ N' ' + @orderBy )
		END
	ELSE
		BEGIN
			SET @commandText = N'SELECT TOP ' + ltrim(str(@pageSize)) + N' ' + @columns + N' FROM ' + @tableName + N' WHERE ' + @pageColumn + N' > '
			SET @commandText += N'(SELECT MAX(' + @pageColumn + N') FROM '
			SET @commandText += N'(SELECT TOP ' + ltrim(str(@pageSize * (@pageIndex-1))) + N' ' + @pageColumn + N' FROM ' + @tableName + @condition + ' ORDER BY ' + @pageColumn
			SET @condition = REPLACE(@condition, 'WHERE', 'AND')
			SET @commandText += N') AS T) ' + @condition + @orderBy
			EXEC(@commandText)
		END
		print @totalCountCommandText
		print @commandText
GO
/****** Object:  Table [dbo].[Province]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sorting] [int] NOT NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省会名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省会表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province'
GO
/****** Object:  Table [dbo].[Promote_Scope]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_Scope](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Promote_Scope] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Scope', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'范围名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Scope', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'范围描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Scope', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Scope', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'促销活动范围定义表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Scope'
GO
/****** Object:  Table [dbo].[Promote_Preferential]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_Preferential](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [int] NOT NULL,
	[Description] [nchar](10) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Promote_Preferential] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Preferential', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠项名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Preferential', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠项描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Preferential', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Preferential', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'促销活动优惠项定义表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Preferential'
GO
/****** Object:  Table [dbo].[Voucher_Cash]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher_Cash](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[FaceValue] [float] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Voucher_Cash] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券初始数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券面值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'FaceValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'失效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash'
GO
/****** Object:  Table [dbo].[Voucher_Preferential]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher_Preferential](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[FaceValue] [float] NOT NULL,
	[MeetMoney] [float] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Voucher_Preferential] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券初始数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券面值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'FaceValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需满足金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'MeetMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'生效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'失效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential'
GO
/****** Object:  Table [dbo].[Voucher_Type]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher_Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Voucher_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Type', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Type', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券类型说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Type', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Type', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券类型表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Type'
GO
/****** Object:  Table [dbo].[User_Level]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Level](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Money] [float] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User_Level] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户登记表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'等级名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需要满足金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户等级表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level'
GO
/****** Object:  Table [dbo].[System_Role]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台角色状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role'
GO
/****** Object:  Table [dbo].[System_Permission]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Layer] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台权限名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台权限层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台权限状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission'
GO
/****** Object:  Table [dbo].[System_Department]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_Department](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Headcount] [int] NOT NULL,
	[Principal] [nvarchar](20) NOT NULL,
	[PrincipalMobile] [varchar](20) NULL,
	[Description] [ntext] NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_Department] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门总人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'Headcount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'Principal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门负责人手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'PrincipalMobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department'
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Update]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Level_Update]
	@ID int,
	@Name nvarchar(50),
	@Money float,
	@CreateTime datetime
As
Begin
	Update User_Level
	Set
		[Name] = @Name,
		[Money] = @Money,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_SelectAll]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Level_SelectAll]
As
Begin
	Select 
		[ID],
		[Name],
		[Money],
		[CreateTime]
	From User_Level
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Insert]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Level_Insert]
	@Name nvarchar(50),
	@Money float,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into User_Level
		([Name],[Money],[CreateTime])
	Values
		(@Name,@Money,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  Table [dbo].[System_Menu]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[Layer] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[URL] [varchar](100) NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台菜单权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'PermissionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父菜单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台菜单层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台菜单网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台菜单排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台菜单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu'
GO
/****** Object:  Table [dbo].[System_Role_Permission]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Role_Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[PermissionID] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_Role_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission', @level2type=N'COLUMN',@level2name=N'RoleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission', @level2type=N'COLUMN',@level2name=N'PermissionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台角色权限关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission'
GO
/****** Object:  Table [dbo].[Promote_MeetAmount_Scope]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetAmount_Scope](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ScopeID] [int] NOT NULL,
	[ScopeValue] [int] NOT NULL,
 CONSTRAINT [PK_Promote_MeetAmount_Scope] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动范围编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope', @level2type=N'COLUMN',@level2name=N'ScopeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动范围值（0 代表全局 or 类别编号 or 品牌编号 or 商品编号）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope', @level2type=N'COLUMN',@level2name=N'ScopeValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠促销活动范围表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope'
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_Update_Headcount]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Department_Update_Headcount]
	@ID int
As
Begin
	Update System_Department
	Set
		[Headcount] = (Select count(ID) From dbo.Backstage_Employee where DepartmentID = @ID)
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_Update]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Department_Update]
	@ID int,
	@Name nvarchar(20),
	@Headcount int,
	@Principal nvarchar(20),
	@PrincipalMobile varchar(20),
	@Description ntext,
	@CreateTime datetime
As
Begin
	Update System_Department
	Set
		[Name] = @Name,
		[Headcount] = @Headcount,
		[Principal] = @Principal,
		[PrincipalMobile] = @PrincipalMobile,
		[Description] = @Description,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_SelectRow]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Department_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[Name],
		[Headcount],
		[Principal],
		[PrincipalMobile],
		[Description],
		[CreateTime]
	From System_Department
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_SelectAll]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Department_SelectAll]
As
Begin
	Select 
		[ID],
		[Name],
		[Headcount],
		[Principal],
		[PrincipalMobile],
		[Description],
		[CreateTime]
	From System_Department
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_Insert]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Department_Insert]
	@Name nvarchar(20),
	@Headcount int,
	@Principal nvarchar(20),
	@PrincipalMobile varchar(20),
	@Description ntext,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into System_Department
		([Name],[Headcount],[Principal],[PrincipalMobile],[Description],[CreateTime])
	Values
		(@Name,@Headcount,@Principal,@PrincipalMobile,@Description,@CreateTime)
		
	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_DeleteRow]    Script Date: 10/18/2013 18:24:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Department_DeleteRow]
	@ID int
As
Begin
	Delete System_Department
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Category_Update]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_Category_Update]
	@ID int,
	@ParentID int,
	@CategoryName nvarchar(50),
	@CategoryNameSpell nvarchar(100),
	@CategoryNameEnglish nvarchar(100),
	@SEOKeywords nvarchar(500),
	@SEODescription nvarchar(500),
	@IsDisplay bit,
	@Sorting int
As
Begin
	Update Product_Category
	Set
		[ParentID] = @ParentID,
		[CategoryName] = @CategoryName,
		[CategoryNameSpell] = @CategoryNameSpell,
		[CategoryNameEnglish] = @CategoryNameEnglish,
		[SEOKeywords] = @SEOKeywords,
		[SEODescription] = @SEODescription,
		[IsDisplay] = @IsDisplay,
		[Sorting] = @Sorting
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Category_Insert]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--添加类别
Create Procedure [dbo].[sp_Product_Category_Insert]
	@ParentID int,
	@CategoryName nvarchar(50),
	@CategoryNameSpell nvarchar(100),
	@CategoryNameEnglish nvarchar(100),
	@SEOKeywords nvarchar(500),
	@SEODescription nvarchar(500),
	@IsGjw bit,
	@IsDisplay bit,
	@Layer int,
	@Sorting int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Product_Category
		([ParentID],[CategoryName],[CategoryNameSpell],[CategoryNameEnglish],[SEOKeywords],[SEODescription],[IsGjw],[IsDisplay],[Layer],[Sorting],[CreateTime])
	Values
		(@ParentID,@CategoryName,@CategoryNameSpell,@CategoryNameEnglish,@SEOKeywords,@SEODescription,@IsGjw,@IsDisplay,@Layer,@Sorting,@CreateTime)

	set @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Category_DeleteRow]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_Category_DeleteRow]
	@ID int
As
Begin
	Delete Product_Category
	Where
		[ID] = @ID

End
GO
/****** Object:  Table [dbo].[Product_Brand]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Brand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[BrandName] [nvarchar](50) NOT NULL,
	[BrandNameSpell] [nvarchar](100) NOT NULL,
	[BrandNameEnglish] [nvarchar](100) NULL,
	[SEOKeywords] [nvarchar](500) NULL,
	[SEODescription] [nvarchar](500) NULL,
	[IsDisplay] [bit] NOT NULL,
	[Layer] [int] NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Brand] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌所属商品类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'CategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父品牌编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'BrandName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌名称拼音' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'BrandNameSpell'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌名称英文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'BrandNameEnglish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌 SEO 关键字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'SEOKeywords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌 SEO 描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'SEODescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌是否显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌所在层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand'
GO
/****** Object:  Table [dbo].[Product_Attribute]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Attribute](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[AttributeName] [nvarchar](20) NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Attribute] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'ProductCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'AttributeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性定义表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute'
GO
/****** Object:  Table [dbo].[Cps_LinkRecord]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cps_LinkRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CpsID] [int] NOT NULL,
	[URL] [varchar](1000) NOT NULL,
	[TargetURL] [nvarchar](4000) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Cps_LinkRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 合作平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部链入地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'TargetURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 链接记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord'
GO
/****** Object:  Table [dbo].[Cps_CommissionRatio]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cps_CommissionRatio](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CpsID] [int] NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[CommissionRatio] [float] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Cps_CommissionRatio] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'ProductCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'佣金比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'CommissionRatio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 佣金比例表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio'
GO
/****** Object:  Table [dbo].[Config_Payment_Type]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Payment_Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentMethodID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[URL] [nvarchar](500) NOT NULL,
	[ImageURL] [nvarchar](100) NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'PaymentMethodID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网银支付网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网银显示图片网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'ImageURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类型表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type'
GO
/****** Object:  Table [dbo].[City]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sorting] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市所在省会编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'ProvinceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City'
GO
/****** Object:  Table [dbo].[Picture]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Picture](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PictureCategoryID] [int] NOT NULL,
	[Name] [nchar](10) NULL,
	[Type] [varchar](10) NOT NULL,
	[Path] [varchar](50) NOT NULL,
	[Size] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[UploadTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'PictureCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片大小（单位：KB）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片状态（0：未引用，1：已引用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'UploadTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture'
GO
/****** Object:  Table [dbo].[Order_Payment]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTypeID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'PaymentTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付状态（0：未支付，1：已支付）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment'
GO
/****** Object:  Table [dbo].[Config_Delivery_Cost]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Delivery_Cost](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryCorporationID] [int] NOT NULL,
	[CityID] [int] NOT NULL,
	[Duration] [int] NULL,
	[Cost] [float] NOT NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_Delivery_Cost] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货公司编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'DeliveryCorporationID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'CityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计持续时长（单位：天）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运费金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'Cost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货费用表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost'
GO
/****** Object:  Table [dbo].[County]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[County](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CityID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Sorting] [int] NOT NULL,
 CONSTRAINT [PK_County] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区县所在城市编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'CityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区县名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区县表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County'
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[ProductBrandID] [int] NOT NULL,
	[Barcode] [varchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Advertisement] [nvarchar](100) NULL,
	[MarketPrice] [float] NOT NULL,
	[GoujiuPrice] [float] NOT NULL,
	[Introduce] [ntext] NOT NULL,
	[IsInvoice] [bit] NOT NULL,
	[Integral] [int] NOT NULL,
	[InventoryNumber] [int] NOT NULL,
	[CommentNumber] [int] NOT NULL,
	[PageView] [int] NOT NULL,
	[Sorting] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductBrandID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品条形码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Barcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品广告词' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Advertisement'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市场价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'MarketPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购酒价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'GoujiuPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品详细介绍' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Introduce'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否开发票' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'IsInvoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买商品所赠积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'库存数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'InventoryNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论总数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'CommentNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'页面浏览量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'PageView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品状态（0：未上架，1：已上架，2：已下架，3：已删除）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product'
GO
/****** Object:  Table [dbo].[Product_AttributeValue]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_AttributeValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttributeID] [int] NOT NULL,
	[AttributeValue] [nvarchar](100) NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_AttributeValue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue', @level2type=N'COLUMN',@level2name=N'AttributeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue', @level2type=N'COLUMN',@level2name=N'AttributeValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性值定义表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue'
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_Update]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_Brand_Update]
	@ID int,
	@CategoryID int,
	@ParentID int,
	@BrandName nvarchar(50),
	@BrandNameSpell nvarchar(100),
	@BrandNameEnglish nvarchar(100),
	@SEOKeywords nvarchar(500),
	@SEODescription nvarchar(500),
	@IsDisplay bit,
	@Sorting int
As
Begin
	Update Product_Brand
	Set
		[CategoryID] = @CategoryID,
		[ParentID] = @ParentID,
		[BrandName] = @BrandName,
		[BrandNameSpell] = @BrandNameSpell,
		[BrandNameEnglish] = @BrandNameEnglish,
		[SEOKeywords] = @SEOKeywords,
		[SEODescription] = @SEODescription,
		[IsDisplay] = @IsDisplay,
		[Sorting] = @Sorting
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_Insert]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_Brand_Insert]
	@CategoryID int,
	@ParentID int,
	@BrandName nvarchar(50),
	@BrandNameSpell nvarchar(100),
	@BrandNameEnglish nvarchar(100),
	@SEOKeywords nvarchar(500),
	@SEODescription nvarchar(500),
	@IsDisplay bit,
	@Layer int,
	@Sorting int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Product_Brand
		([CategoryID],[ParentID],[BrandName],[BrandNameSpell],[BrandNameEnglish],[SEOKeywords],[SEODescription],[IsDisplay],[Layer],[Sorting],[CreateTime])
	Values
		(@CategoryID,@ParentID,@BrandName,@BrandNameSpell,@BrandNameEnglish,@SEOKeywords,@SEODescription,@IsDisplay,@Layer,@Sorting,@CreateTime)

	set @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_DeleteRow]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_Brand_DeleteRow]
	@ID int
As
Begin
	Delete Product_Brand
	Where
		[ID] = @ID

End
GO
/****** Object:  Table [dbo].[User_Level_Price]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Level_Price](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserLevelID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User_Level_Price] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户等级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'UserLevelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（0：未启用，1：已启用，2：已停止）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户等级价格表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price'
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CpsID] [int] NULL,
	[UserLevelID] [int] NOT NULL,
	[CountyID] [int] NULL,
	[Address] [nvarchar](200) NULL,
	[Email] [varchar](100) NOT NULL,
	[EmailValidate] [bit] NOT NULL,
	[Mobile] [varchar](20) NULL,
	[MobileValidate] [bit] NULL,
	[Name] [nvarchar](20) NULL,
	[Age] [int] NULL,
	[Gender] [bit] NULL,
	[LoginName] [varchar](50) NOT NULL,
	[LoginPassword] [varchar](50) NOT NULL,
	[NickName] [nvarchar](50) NULL,
	[Birthday] [datetime] NULL,
	[QQ] [varchar](20) NULL,
	[MSN] [varchar](50) NULL,
	[OpenID] [varchar](50) NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastLoginTime] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 合作平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户等级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UserLevelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户所在地区区县编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CountyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户所在地详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮箱是否验证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'EmailValidate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码是否验证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'MobileValidate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'LoginPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户生日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户 QQ 号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'QQ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户 MSN 账户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'MSN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开放平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'OpenID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户状态（0：未验证，1：已通过，2：已锁定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
/****** Object:  Table [dbo].[System_Employee]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[CountyID] [int] NOT NULL,
	[IdentityCard] [varchar](20) NOT NULL,
	[IdentityCardAddress] [nvarchar](100) NOT NULL,
	[BankCard] [varchar](30) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](2) NOT NULL,
	[Mobile] [varchar](50) NULL,
	[HomeAddress] [nvarchar](100) NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工所在部门编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'DepartmentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工户口所在区县编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'CountyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工身份证号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'IdentityCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工户口地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'IdentityCardAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工工资卡卡号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'BankCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工家庭住址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'HomeAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（0：在职，1：离职）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee'
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectAll]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_SelectAll]
As
Begin
	Select 
		[ID],
		[ProductCategoryID],
		[ProductBrandID],
		[Barcode],
		[Name],
		[Advertisement],
		[MarketPrice],
		[GoujiuPrice],
		[Introduce],
		[IsInvoice],
		[Integral],
		[InventoryNumber],
		[CommentNumber],
		[PageView],
		[Sorting],
		[Status],
		[CreateTime]
	From Product
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Insert]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_Insert]
	@ProductCategoryID int,
	@ProductBrandID int,
	@Barcode varchar(50),
	@Name nvarchar(100),
	@Advertisement nvarchar(100),
	@MarketPrice float,
	@GoujiuPrice float,
	@Introduce ntext,
	@IsInvoice bit,
	@Integral int,
	@InventoryNumber int,
	@CommentNumber int,
	@PageView int,
	@Sorting int,
	@Status int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Product
		([ProductCategoryID],[ProductBrandID],[Barcode],[Name],[Advertisement],[MarketPrice],[GoujiuPrice],[Introduce],[IsInvoice],[Integral],[InventoryNumber],[CommentNumber],[PageView],[Sorting],[Status],[CreateTime])
	Values
		(@ProductCategoryID,@ProductBrandID,@Barcode,@Name,@Advertisement,@MarketPrice,@GoujiuPrice,@Introduce,@IsInvoice,@Integral,@InventoryNumber,@CommentNumber,@PageView,@Sorting,@Status,@CreateTime)

	set @ReferenceID = @@IDENTITY

End
GO
/****** Object:  Table [dbo].[Promote_MuchBottled]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MuchBottled](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Promote_MuchBottled] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多瓶装促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled'
GO
/****** Object:  Table [dbo].[Promote_LimitedTime_Discount]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_LimitedTime_Discount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[Name] [int] NOT NULL,
	[Discount] [float] NOT NULL,
	[DiscountPrice] [float] NOT NULL,
	[MaxBuyNumber] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[OnlinePayment] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Promote_LimitedTime_Discount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动折扣' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'Discount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折后价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'DiscountPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大购买数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'MaxBuyNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否仅限在线支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'OnlinePayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限时打折促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LimitedTime_Discount'
GO
/****** Object:  Table [dbo].[Product_Picture]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Picture](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[PictureID] [int] NOT NULL,
	[IsMaster] [bit] NOT NULL,
 CONSTRAINT [PK_Product_Picture] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Picture', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Picture', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Picture', @level2type=N'COLUMN',@level2name=N'PictureID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为主图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Picture', @level2type=N'COLUMN',@level2name=N'IsMaster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Picture'
GO
/****** Object:  Table [dbo].[Product_LimitedBuy_Condition]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_LimitedBuy_Condition](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserLevelID] [int] NOT NULL,
	[LimitedDays] [int] NOT NULL,
	[LimitedQuantity] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_LimitedBuy_Condition] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户等级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'UserLevelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制天数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'LimitedDays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'LimitedQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（0：未启用，1：启用中，2：已停用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品限购条件表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition'
GO
/****** Object:  Table [dbo].[Product_LimitedBuy_Area]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_LimitedBuy_Area](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[AreaID] [int] NOT NULL,
	[AreaCategory] [int] NOT NULL,
	[CreateTime] [nchar](10) NULL,
 CONSTRAINT [PK_Product_LimitedBuy_Area] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地区编号（省会编号 or 城市编号 or 区县编号）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'AreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域类别（0：省会，1：城市，2：区县）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'AreaCategory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品限制购买区域表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area'
GO
/****** Object:  Table [dbo].[Product_AttributeValueSet]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_AttributeValueSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[AttributeID] [int] NOT NULL,
	[AttributeValueID] [int] NOT NULL,
 CONSTRAINT [PK_Product_AttributeValueSet] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValueSet', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValueSet', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValueSet', @level2type=N'COLUMN',@level2name=N'AttributeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性值编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValueSet', @level2type=N'COLUMN',@level2name=N'AttributeValueID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性属性值集合表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValueSet'
GO
/****** Object:  Table [dbo].[Order_Payment_Voucher]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Payment_Voucher](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderPaymentID] [int] NOT NULL,
	[VoucherTypeID] [int] NOT NULL,
	[VoucherBindingID] [int] NOT NULL,
	[Money] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Payment_Voucher] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Voucher', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付表编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Voucher', @level2type=N'COLUMN',@level2name=N'OrderPaymentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Voucher', @level2type=N'COLUMN',@level2name=N'VoucherTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定优惠券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Voucher', @level2type=N'COLUMN',@level2name=N'VoucherBindingID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券支付金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Voucher', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Voucher', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单电子券支付详情表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Voucher'
GO
/****** Object:  Table [dbo].[Order_Payment_Account]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Payment_Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderPaymentID] [int] NOT NULL,
	[Money] [float] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Payment_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付表编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'OrderPaymentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户支付金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单账户余额支付详情表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account'
GO
/****** Object:  Table [dbo].[Group_Purchase]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group_Purchase](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserLevelID] [int] NOT NULL,
	[PaymentMethodID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[GpPrice] [float] NOT NULL,
	[TotalQuantity] [int] NOT NULL,
	[LimitedBuyQuantity] [int] NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[IsDisplayTime] [bit] NOT NULL,
	[Description] [ntext] NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Group_Purchase] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户级别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'UserLevelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'PaymentMethodID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'GpPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购商品总数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'TotalQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制购买数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'LimitedBuyQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否显示时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'IsDisplayTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动状态（0：未开始，1：活动中，2：已结束）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase'
GO
/****** Object:  Table [dbo].[Group_Purchase_Location]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group_Purchase_Location](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupPurchaseID] [int] NOT NULL,
	[Location] [int] NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Group_Purchase_Location] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Location', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Location', @level2type=N'COLUMN',@level2name=N'GroupPurchaseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示位置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Location', @level2type=N'COLUMN',@level2name=N'Location'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Location', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Location', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动显示位置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Location'
GO
/****** Object:  Table [dbo].[Product_Consult]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Consult](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](250) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Consulting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品咨询表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult'
GO
/****** Object:  Table [dbo].[Promote_MuchBottled_Rule]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MuchBottled_Rule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MuchBottledID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[PreferentialMoney] [float] NOT NULL,
	[TotalMoney] [float] NOT NULL,
 CONSTRAINT [PK_Promote_MuchBottled_Rule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多瓶装促销活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'MuchBottledID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠金额（(商品原价 - 单价) * 数量 = 优惠金额）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'PreferentialMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总金额（单价 * 数量 = 总金额）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'TotalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多瓶装促销活动规则表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule'
GO
/****** Object:  Table [dbo].[Promote_MeetAmount]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetAmount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[MeetAmountScopeID] [int] NOT NULL,
	[MeetAmount] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Description] [nvarchar](100) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Promote_MeetAmount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设置该活动的员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠活动范围编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'MeetAmountScopeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满足件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'MeetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount'
GO
/****** Object:  Table [dbo].[Promote_MeetMoney]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetMoney](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[MeetMoney] [float] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[UpperLimit] [bit] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Promote_MeetMoney] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设置该活动的员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需满足金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'MeetMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动是否上不封顶' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'UpperLimit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney'
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Update_Status]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Update_Status]
	@ID int,
	@Status int
As
Begin
	Update [User]
	Set
		[Status] = @Status
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Update_ResetPassword]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Update_ResetPassword]
	@ID int,
	@LoginPassword varchar(50)
As
Begin
	Update [User]
	Set
		[LoginPassword] = @LoginPassword
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectRow]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,冯瑶>
-- Create date: <Create Date,2013-10-15,>
-- Description:	<Description,查询指定用户详细信息,>
-- =============================================
CREATE Procedure [dbo].[sp_User_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[CpsID],
		[UserLevelID],
		[CountyID],
		[Address],
		[Email],
		[EmailValidate],
		[Mobile],
		[MobileValidate],
		[Name],
		[Age],
		[Gender],
		[LoginName],
		[LoginPassword],
		[NickName],
		[Birthday],
		[QQ],
		[MSN],
		[OpenID],
		[Status],
		[CreateTime],
		[LastLoginTime]
	From [User]
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectAll]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,冯瑶>
-- Create date: <Create Date,2013-10-15,>
-- Description:	<Description,查询所有用户信息,>
-- =============================================
CREATE Procedure [dbo].[sp_User_SelectAll]
As
Begin
	Select 
		[ID],
		[CpsID],
		[UserLevelID],
		[CountyID],
		[Address],
		[Email],
		[EmailValidate],
		[Mobile],
		[MobileValidate],
		[Name],
		[Age],
		[Gender],
		[LoginName],
		[LoginPassword],
		[NickName],
		[Birthday],
		[QQ],
		[MSN],
		[OpenID],
		[Status],
		[CreateTime],
		[LastLoginTime]
	From [User]
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Insert]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_User_Insert]
	@CpsID int = null,
	@UserLevelID int = null,
	@CountyID int,
	@Address nvarchar(200),
	@Email varchar(100),
	@EmailValidate bit= null,
	@Mobile varchar(20)= null,
	@MobileValidate bit= null,
	@Name nvarchar(20)= null,
	@Age int= null,
	@Gender bit= null,
	@LoginName varchar(50),
	@LoginPassword varchar(50),
	@NickName nvarchar(50)= null,
	@Birthday datetime= null,
	@QQ varchar(20)= null,
	@MSN varchar(50)= null,
	@OpenID varchar(50) = null,
	@Status int,
	@CreateTime datetime,
	@LastLoginTime datetime= null,
	@ReferenceID int output
As
Begin
	Insert Into [User]
		([CpsID],[UserLevelID],[CountyID],[Address],[Email],[EmailValidate],[Mobile],[MobileValidate],[Name],[Age],[Gender],[LoginName],[LoginPassword],[NickName],[Birthday],[QQ],[MSN],[OpenID],[Status],[CreateTime],[LastLoginTime])
	Values
		(@CpsID,@UserLevelID,@CountyID,@Address,@Email,@EmailValidate,@Mobile,@MobileValidate,@Name,@Age,@Gender,@LoginName,@LoginPassword,@NickName,@Birthday,@QQ,@MSN,@OpenID,@Status,@CreateTime,@LastLoginTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  Table [dbo].[System_User]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[LoginName] [varchar](50) NOT NULL,
	[LoginPassword] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台用户登录密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'LoginPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台用户姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台用户状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User'
GO
/****** Object:  Table [dbo].[Group_Purchase_Subscribe_Content]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group_Purchase_Subscribe_Content](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GroupPurchaseID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SubscribeContent] [ntext] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Group_Purchase_Subscribe_Content] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe_Content', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe_Content', @level2type=N'COLUMN',@level2name=N'GroupPurchaseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订阅内容名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe_Content', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订阅内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe_Content', @level2type=N'COLUMN',@level2name=N'SubscribeContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe_Content', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动订阅内容表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group_Purchase_Subscribe_Content'
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_Update]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Employee_Update]
	@ID int,
	@DepartmentID int,
	@CountyID int,
	@IdentityCard varchar(20),
	@IdentityCardAddress nvarchar(100),
	@BankCard varchar(30),
	@Name nvarchar(50),
	@Age int,
	@Gender nvarchar(2),
	@Mobile varchar(50),
	@HomeAddress nvarchar(100),
	@Status int,
	@CreateTime datetime
As
Begin
	Update System_Employee
	Set
		[DepartmentID] = @DepartmentID,
		[CountyID] = @CountyID,
		[IdentityCard] = @IdentityCard,
		[IdentityCardAddress] = @IdentityCardAddress,
		[BankCard] = @BankCard,
		[Name] = @Name,
		[Age] = @Age,
		[Gender] = @Gender,
		[Mobile] = @Mobile,
		[HomeAddress] = @HomeAddress,
		[Status] = @Status,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_Insert]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Employee_Insert]
	@DepartmentID int,
	@CountyID int,
	@IdentityCard varchar(20),
	@IdentityCardAddress nvarchar(100),
	@BankCard varchar(30),
	@Name nvarchar(50),
	@Age int,
	@Gender nvarchar(2),
	@Mobile varchar(50),
	@HomeAddress nvarchar(100),
	@Status int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into System_Employee
		([DepartmentID],[CountyID],[IdentityCard],[IdentityCardAddress],[BankCard],[Name],[Age],[Gender],[Mobile],[HomeAddress],[Status],[CreateTime])
	Values
		(@DepartmentID,@CountyID,@IdentityCard,@IdentityCardAddress,@BankCard,@Name,@Age,@Gender,@Mobile,@HomeAddress,@Status,@CreateTime)
	
	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_DeleteRow]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Employee_DeleteRow]
	@ID int
As
Begin
	Delete System_Employee
	Where
		[ID] = @ID

End
GO
/****** Object:  Table [dbo].[Voucher_Preferential_Binding]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Voucher_Preferential_Binding](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherPreferentialID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Number] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
	[BindingTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Voucher_Preferential_Binding] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding', @level2type=N'COLUMN',@level2name=N'VoucherPreferentialID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券券号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券状态（0：冻结，1：正常）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'绑定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding', @level2type=N'COLUMN',@level2name=N'BindingTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券绑定表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Preferential_Binding'
GO
/****** Object:  Table [dbo].[Voucher_Cash_Binding]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Voucher_Cash_Binding](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherCashID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Number] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
	[BindingTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Voucher_Cash_Binding] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding', @level2type=N'COLUMN',@level2name=N'VoucherCashID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券券号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券状态（0：冻结，1：正常）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'绑定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding', @level2type=N'COLUMN',@level2name=N'BindingTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券绑定表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Voucher_Cash_Binding'
GO
/****** Object:  Table [dbo].[User_Account]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Balance] [float] NOT NULL,
	[Locking] [float] NOT NULL,
 CONSTRAINT [PK_User_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户余额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'Balance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁定余额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'Locking'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户帐户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account'
GO
/****** Object:  Table [dbo].[User_RecieveAddress]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User_RecieveAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CountyID] [int] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Consignee] [nvarchar](20) NOT NULL,
	[Mobile] [varchar](20) NOT NULL,
	[Tel] [varchar](20) NULL,
	[IsDefault] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User_RecieveAddress] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区县编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'CountyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'Consignee'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为默认收货地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户收货地址表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress'
GO
/****** Object:  Table [dbo].[User_Integral]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Integral](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Integral] [int] NOT NULL,
	[Locking] [int] NOT NULL,
 CONSTRAINT [PK_User_Integral] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'锁定积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral', @level2type=N'COLUMN',@level2name=N'Locking'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户积分表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral'
GO
/****** Object:  Table [dbo].[User_Head]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User_Head](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[URL] [varchar](100) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User_Head] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Head', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Head', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Head', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否默认' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Head', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Head', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户头像表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Head'
GO
/****** Object:  Table [dbo].[User_BrowseHistory]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_BrowseHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User_BrowseHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_BrowseHistory', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_BrowseHistory', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_BrowseHistory', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_BrowseHistory', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户浏览记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_BrowseHistory'
GO
/****** Object:  Table [dbo].[User_Account_Details]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Account_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserAccountID] [int] NOT NULL,
	[Money] [float] NOT NULL,
	[Type] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User_Account_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户账户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'UserAccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型（0：存入，1：支出）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户账户明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details'
GO
/****** Object:  Table [dbo].[User_Integral_Details]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Integral_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserIntegralID] [int] NOT NULL,
	[Integral] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_User_Integral_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户积分账户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'UserIntegralID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型（0：获取，1：消耗）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户积分明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details'
GO
/****** Object:  Table [dbo].[System_User_Role]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_User_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Backstage_User_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User_Role', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User_Role', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User_Role', @level2type=N'COLUMN',@level2name=N'RoleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User_Role', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户角色关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User_Role'
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_Update_Password]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_System_User_Update_Password]
	@ID int,
	@LoginPassword varchar(50)
As
Begin
	Update [System_User]
	Set
		[LoginPassword] = @LoginPassword
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_Update]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_System_User_Update]
	@ID int,
	@Name varchar(50),
	@Status int
As
Begin
	Update [System_User]
	Set
		[Name] = @Name,
		[Status] = @Status
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_SelectRow]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_System_User_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[LoginName],
		[LoginPassword],
		[Name],
		[Status],
		[CreateTime]
	From [System_User]
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_Insert]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_User_Insert]
	@EmployeeID int,
	@LoginName varchar(50),
	@LoginPassword varchar(50),
	@Name varchar(50),
	@Status int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into [System_User]
		([EmployeeID],[LoginName],[LoginPassword],[Name],[Status],[CreateTime])
	Values
		(@EmployeeID,@LoginName,@LoginPassword,@Name,@Status,@CreateTime)

	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_DeleteRow]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_System_User_DeleteRow]
	@ID int
As
Begin
	Delete [System_User]
	Where
		[ID] = @ID

End
GO
/****** Object:  Table [dbo].[Promote_MeetMoney_Preferential]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetMoney_Preferential](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MeetMoneyID] [int] NOT NULL,
	[PreferentialID] [int] NOT NULL,
	[PreferentialValue] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Promote_MeetMoney_Preferentials] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Preferential', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送促销活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Preferential', @level2type=N'COLUMN',@level2name=N'MeetMoneyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动优惠项编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Preferential', @level2type=N'COLUMN',@level2name=N'PreferentialID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动优惠项值（所减现金 or 礼品编号 or 所赠积分 or 是否免邮 or 优惠券编号）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Preferential', @level2type=N'COLUMN',@level2name=N'PreferentialValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送促销活动优惠项表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Preferential'
GO
/****** Object:  Table [dbo].[Promote_MeetAmount_Preferential]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Promote_MeetAmount_Preferential](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MeetAmountID] [int] NOT NULL,
	[ParentID] [int] NULL,
	[MeetAmout] [int] NOT NULL,
	[PreferentialID] [int] NOT NULL,
	[PreferentialValue] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Promote_MeetAmount_Preferentials] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Preferential', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠促销活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Preferential', @level2type=N'COLUMN',@level2name=N'MeetAmountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父活动优惠项编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Preferential', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需满足数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Preferential', @level2type=N'COLUMN',@level2name=N'MeetAmout'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动优惠项编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Preferential', @level2type=N'COLUMN',@level2name=N'PreferentialID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动优惠项值（所打折扣 or 是否免邮 or 礼品编号）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Preferential', @level2type=N'COLUMN',@level2name=N'PreferentialValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠促销活动优惠项表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Preferential'
GO
/****** Object:  Table [dbo].[Product_Consult_Reply]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Consult_Reply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConsultID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Content] [nvarchar](250) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Consult_Reply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品咨询编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'ConsultID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询回复内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品咨询回复表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply'
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RecieveAddressID] [int] NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[TotalMoney] [float] NOT NULL,
	[TotalFreight] [float] NOT NULL,
	[TotalIntegral] [int] NOT NULL,
	[ClientIP] [varchar](50) NULL,
	[ClientBrowser] [nvarchar](200) NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货地址编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'RecieveAddressID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'TotalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'TotalFreight'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'TotalIntegral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端 IP 地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'ClientIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端浏览器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'ClientBrowser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order'
GO
/****** Object:  Table [dbo].[Cps_OrderPushRecord]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cps_OrderPushRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[CpsID] [int] NOT NULL,
	[PushURL] [nvarchar](4000) NOT NULL,
	[AcceptParam] [nvarchar](200) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Cps_OrderPushRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被推送的 Cps 平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'推送地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'PushURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'AcceptParam'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 订单推送记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord'
GO
/****** Object:  Table [dbo].[Order_Operate_Log]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Operate_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Operate_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Operate_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Operate_Log', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Operate_Log', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单操作代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Operate_Log', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单操作描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Operate_Log', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Operate_Log', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单操作日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Operate_Log'
GO
/****** Object:  Table [dbo].[Order_Money_Adjust]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Money_Adjust](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Money] [float] NOT NULL,
	[Cause] [nvarchar](400) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Money_Adjust] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Money_Adjust', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Money_Adjust', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调整金额（+ / -）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Money_Adjust', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调整原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Money_Adjust', @level2type=N'COLUMN',@level2name=N'Cause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Money_Adjust', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单价格调整表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Money_Adjust'
GO
/****** Object:  Table [dbo].[Order_Invoice]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Invoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[InvoiceTypeID] [int] NOT NULL,
	[InvoiceContentID] [int] NOT NULL,
	[InvoiceTitle] [nvarchar](100) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Invoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'InvoiceTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票内容编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'InvoiceContentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票抬头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'InvoiceTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单发票表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice'
GO
/****** Object:  Table [dbo].[Order_Return]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Return](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[TotalMoney] [float] NOT NULL,
	[Cause] [nvarchar](400) NULL,
	[Description] [nvarchar](500) NULL,
	[Linkman] [nvarchar](20) NOT NULL,
	[Mobile] [varchar](20) NOT NULL,
	[Status] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_Order_Return] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货类型（0：全部退货，1：部分退货）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单退货总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'TotalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'Cause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'Linkman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单退货状态（0：审核中，1：退货中，2：退货成功）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单退货表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return'
GO
/****** Object:  Table [dbo].[Order_Product]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Quantity] [int] NOT NULL,
	[MarketPrice] [float] NOT NULL,
	[GoujiuPrice] [float] NOT NULL,
	[TransactPrice] [float] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'ProductName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市场价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'MarketPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购酒价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'GoujiuPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成交价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'TransactPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product'
GO
/****** Object:  Table [dbo].[Order_Exchange]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Exchange](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Cause] [nvarchar](400) NULL,
	[Description] [nvarchar](500) NULL,
	[Linkman] [nvarchar](20) NOT NULL,
	[Mobile] [varchar](20) NOT NULL,
	[CountyID] [int] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
 CONSTRAINT [PK_Order_Exchange] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'Cause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'Linkman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货地址所在区县编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'CountyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货状态（0：审核中，1：换货中，2：换货成功）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单换货表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange'
GO
/****** Object:  Table [dbo].[Order_Tracking]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Tracking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[DeliveryMethodID] [int] NOT NULL,
	[DeliveryCorporationID] [int] NOT NULL,
	[TrackingNumber] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Order_Tracking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货方式编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking', @level2type=N'COLUMN',@level2name=N'DeliveryMethodID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送货公司编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking', @level2type=N'COLUMN',@level2name=N'DeliveryCorporationID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking', @level2type=N'COLUMN',@level2name=N'TrackingNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单跟踪表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking'
GO
/****** Object:  Table [dbo].[Order_Status_Change_Log]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Status_Change_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[OrderStatus] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Status_Change] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Change_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Change_Log', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Change_Log', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Change_Log', @level2type=N'COLUMN',@level2name=N'OrderStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Change_Log', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态变更日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Change_Log'
GO
/****** Object:  Table [dbo].[Product_Comment]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Content] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论分数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论状态（0：未审核，1：已通过，2：已锁定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品评论表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment'
GO
/****** Object:  Table [dbo].[Product_Comment_Reply]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Comment_Reply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CommentID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](250) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Product_Comment_Reply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品评论编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'CommentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父商品评论回复编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论回复内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论回复状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品评论回复表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply'
GO
/****** Object:  Table [dbo].[Order_Tracking_Logistics]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Tracking_Logistics](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderTrackingID] [int] NOT NULL,
	[OperateTime] [datetime] NOT NULL,
	[OperateSummary] [nvarchar](100) NOT NULL,
	[OperateName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Order_Tracking_Logistics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking_Logistics', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单跟踪表编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking_Logistics', @level2type=N'COLUMN',@level2name=N'OrderTrackingID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking_Logistics', @level2type=N'COLUMN',@level2name=N'OperateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作概要' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking_Logistics', @level2type=N'COLUMN',@level2name=N'OperateSummary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking_Logistics', @level2type=N'COLUMN',@level2name=N'OperateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单跟踪物流信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Tracking_Logistics'
GO
/****** Object:  Table [dbo].[Order_Return_Product]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Return_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderReturnID] [int] NULL,
	[OrderProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Refund_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单退货编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Product', @level2type=N'COLUMN',@level2name=N'OrderReturnID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单商品表编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Product', @level2type=N'COLUMN',@level2name=N'OrderProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Product', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Product', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单退货商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Product'
GO
/****** Object:  Table [dbo].[Order_Return_Audit]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Return_Audit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderReturnID] [int] NOT NULL,
	[CustomerServiceID] [int] NOT NULL,
	[CSAuditStatus] [int] NOT NULL,
	[InventoryKeeperID] [int] NOT NULL,
	[IKAuditStatus] [int] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Return_Audit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'OrderReturnID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客服编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'CustomerServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客服审核状态（0：未审核，1：已通过，2：未通过）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'CSAuditStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库管理员编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'InventoryKeeperID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库管理员审核状态（0：未审核，1：已通过，2：未通过）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'IKAuditStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退货审核描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单退货审核表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Return_Audit'
GO
/****** Object:  Table [dbo].[Order_Exchange_Product]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Exchange_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderExechangeID] [int] NOT NULL,
	[OrderProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Exchange_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Product', @level2type=N'COLUMN',@level2name=N'OrderExechangeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单商品表编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Product', @level2type=N'COLUMN',@level2name=N'OrderProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Product', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Product', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单换货商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Product'
GO
/****** Object:  Table [dbo].[Order_Exchange_Audit]    Script Date: 10/18/2013 18:24:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Exchange_Audit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderExechangeID] [int] NOT NULL,
	[CustomerServiceID] [int] NOT NULL,
	[CSAuditStatus] [int] NOT NULL,
	[InventoryKeeperID] [int] NOT NULL,
	[IKAuditStatus] [int] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Exchange_Audit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'OrderExechangeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客服编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'CustomerServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客服审核状态（0：未审核，1：已通过，2：未通过）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'CSAuditStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库管理员编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'InventoryKeeperID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仓库管理员审核状态（0：未审核，1：已通过，2：未通过）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'IKAuditStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'换货描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单换货审核表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Exchange_Audit'
GO
/****** Object:  ForeignKey [FK_Backstage_Menu_Backstage_Permission]    Script Date: 10/18/2013 18:24:43 ******/
ALTER TABLE [dbo].[System_Menu]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_Menu_Backstage_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[System_Permission] ([ID])
GO
ALTER TABLE [dbo].[System_Menu] CHECK CONSTRAINT [FK_Backstage_Menu_Backstage_Permission]
GO
/****** Object:  ForeignKey [FK_Backstage_Role_Permission_Backstage_Permission]    Script Date: 10/18/2013 18:24:43 ******/
ALTER TABLE [dbo].[System_Role_Permission]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_Role_Permission_Backstage_Permission] FOREIGN KEY([PermissionID])
REFERENCES [dbo].[System_Permission] ([ID])
GO
ALTER TABLE [dbo].[System_Role_Permission] CHECK CONSTRAINT [FK_Backstage_Role_Permission_Backstage_Permission]
GO
/****** Object:  ForeignKey [FK_Backstage_Role_Permission_Backstage_Role]    Script Date: 10/18/2013 18:24:43 ******/
ALTER TABLE [dbo].[System_Role_Permission]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_Role_Permission_Backstage_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[System_Role] ([ID])
GO
ALTER TABLE [dbo].[System_Role_Permission] CHECK CONSTRAINT [FK_Backstage_Role_Permission_Backstage_Role]
GO
/****** Object:  ForeignKey [FK_Promote_MeetAmount_Scope_Promote_Scope]    Script Date: 10/18/2013 18:24:43 ******/
ALTER TABLE [dbo].[Promote_MeetAmount_Scope]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetAmount_Scope_Promote_Scope] FOREIGN KEY([ScopeID])
REFERENCES [dbo].[Promote_Scope] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetAmount_Scope] CHECK CONSTRAINT [FK_Promote_MeetAmount_Scope_Promote_Scope]
GO
/****** Object:  ForeignKey [FK_Product_Brand_Product_Category]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Brand]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brand_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Product_Category] ([ID])
GO
ALTER TABLE [dbo].[Product_Brand] CHECK CONSTRAINT [FK_Product_Brand_Product_Category]
GO
/****** Object:  ForeignKey [FK_Product_Attribute_Product_Category]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Attribute]  WITH CHECK ADD  CONSTRAINT [FK_Product_Attribute_Product_Category] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[Product_Category] ([ID])
GO
ALTER TABLE [dbo].[Product_Attribute] CHECK CONSTRAINT [FK_Product_Attribute_Product_Category]
GO
/****** Object:  ForeignKey [FK_Cps_LinkRecord_Cps]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Cps_LinkRecord]  WITH CHECK ADD  CONSTRAINT [FK_Cps_LinkRecord_Cps] FOREIGN KEY([CpsID])
REFERENCES [dbo].[Cps] ([ID])
GO
ALTER TABLE [dbo].[Cps_LinkRecord] CHECK CONSTRAINT [FK_Cps_LinkRecord_Cps]
GO
/****** Object:  ForeignKey [FK_Cps_CommissionRatio_Cps]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Cps_CommissionRatio]  WITH CHECK ADD  CONSTRAINT [FK_Cps_CommissionRatio_Cps] FOREIGN KEY([CpsID])
REFERENCES [dbo].[Cps] ([ID])
GO
ALTER TABLE [dbo].[Cps_CommissionRatio] CHECK CONSTRAINT [FK_Cps_CommissionRatio_Cps]
GO
/****** Object:  ForeignKey [FK_Cps_CommissionRatio_Product_Category]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Cps_CommissionRatio]  WITH CHECK ADD  CONSTRAINT [FK_Cps_CommissionRatio_Product_Category] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[Product_Category] ([ID])
GO
ALTER TABLE [dbo].[Cps_CommissionRatio] CHECK CONSTRAINT [FK_Cps_CommissionRatio_Product_Category]
GO
/****** Object:  ForeignKey [FK_Payment_Type_Payment_Method]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Config_Payment_Type]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Type_Payment_Method] FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[Config_Payment_Method] ([ID])
GO
ALTER TABLE [dbo].[Config_Payment_Type] CHECK CONSTRAINT [FK_Payment_Type_Payment_Method]
GO
/****** Object:  ForeignKey [FK_City_Province]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Province] FOREIGN KEY([ProvinceID])
REFERENCES [dbo].[Province] ([ID])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Province]
GO
/****** Object:  ForeignKey [FK_Picture_Picture_Category]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Picture]  WITH CHECK ADD  CONSTRAINT [FK_Picture_Picture_Category] FOREIGN KEY([PictureCategoryID])
REFERENCES [dbo].[Picture_Category] ([ID])
GO
ALTER TABLE [dbo].[Picture] CHECK CONSTRAINT [FK_Picture_Picture_Category]
GO
/****** Object:  ForeignKey [FK_Order_Payment_Payment_Type]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Payment]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment_Payment_Type] FOREIGN KEY([PaymentTypeID])
REFERENCES [dbo].[Config_Payment_Type] ([ID])
GO
ALTER TABLE [dbo].[Order_Payment] CHECK CONSTRAINT [FK_Order_Payment_Payment_Type]
GO
/****** Object:  ForeignKey [FK_Delivery_Cost_City]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Config_Delivery_Cost]  WITH CHECK ADD  CONSTRAINT [FK_Delivery_Cost_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([ID])
GO
ALTER TABLE [dbo].[Config_Delivery_Cost] CHECK CONSTRAINT [FK_Delivery_Cost_City]
GO
/****** Object:  ForeignKey [FK_Delivery_Cost_Delivery_Corporation]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Config_Delivery_Cost]  WITH CHECK ADD  CONSTRAINT [FK_Delivery_Cost_Delivery_Corporation] FOREIGN KEY([DeliveryCorporationID])
REFERENCES [dbo].[Config_Delivery_Corporation] ([ID])
GO
ALTER TABLE [dbo].[Config_Delivery_Cost] CHECK CONSTRAINT [FK_Delivery_Cost_Delivery_Corporation]
GO
/****** Object:  ForeignKey [FK_County_City]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[County]  WITH CHECK ADD  CONSTRAINT [FK_County_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([ID])
GO
ALTER TABLE [dbo].[County] CHECK CONSTRAINT [FK_County_City]
GO
/****** Object:  ForeignKey [FK_Product_Product_Brand]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Product_Brand] FOREIGN KEY([ProductBrandID])
REFERENCES [dbo].[Product_Brand] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Product_Brand]
GO
/****** Object:  ForeignKey [FK_Product_Product_Category]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Product_Category] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[Product_Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Product_Category]
GO
/****** Object:  ForeignKey [FK_Product_AttributeValue_Product_Attribute]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_AttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_Product_AttributeValue_Product_Attribute] FOREIGN KEY([AttributeID])
REFERENCES [dbo].[Product_Attribute] ([ID])
GO
ALTER TABLE [dbo].[Product_AttributeValue] CHECK CONSTRAINT [FK_Product_AttributeValue_Product_Attribute]
GO
/****** Object:  ForeignKey [FK_User_Level_Price_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_Level_Price]  WITH CHECK ADD  CONSTRAINT [FK_User_Level_Price_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[User_Level_Price] CHECK CONSTRAINT [FK_User_Level_Price_Product]
GO
/****** Object:  ForeignKey [FK_User_Level_Price_User_Level]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_Level_Price]  WITH CHECK ADD  CONSTRAINT [FK_User_Level_Price_User_Level] FOREIGN KEY([UserLevelID])
REFERENCES [dbo].[User_Level] ([ID])
GO
ALTER TABLE [dbo].[User_Level_Price] CHECK CONSTRAINT [FK_User_Level_Price_User_Level]
GO
/****** Object:  ForeignKey [FK_User_County]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_County] FOREIGN KEY([CountyID])
REFERENCES [dbo].[County] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_County]
GO
/****** Object:  ForeignKey [FK_User_Cps]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Cps] FOREIGN KEY([CpsID])
REFERENCES [dbo].[Cps] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Cps]
GO
/****** Object:  ForeignKey [FK_User_User_Level]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User_Level] FOREIGN KEY([UserLevelID])
REFERENCES [dbo].[User_Level] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User_Level]
GO
/****** Object:  ForeignKey [FK_Backstage_Employee_Backstage_Department]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[System_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_Employee_Backstage_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[System_Department] ([ID])
GO
ALTER TABLE [dbo].[System_Employee] CHECK CONSTRAINT [FK_Backstage_Employee_Backstage_Department]
GO
/****** Object:  ForeignKey [FK_Backstage_Employee_County]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[System_Employee]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_Employee_County] FOREIGN KEY([CountyID])
REFERENCES [dbo].[County] ([ID])
GO
ALTER TABLE [dbo].[System_Employee] CHECK CONSTRAINT [FK_Backstage_Employee_County]
GO
/****** Object:  ForeignKey [FK_Promote_MuchBottled_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MuchBottled]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MuchBottled_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Promote_MuchBottled] CHECK CONSTRAINT [FK_Promote_MuchBottled_Product]
GO
/****** Object:  ForeignKey [FK_Promote_LimitedTime_Discount_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_LimitedTime_Discount]  WITH CHECK ADD  CONSTRAINT [FK_Promote_LimitedTime_Discount_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Promote_LimitedTime_Discount] CHECK CONSTRAINT [FK_Promote_LimitedTime_Discount_Product]
GO
/****** Object:  ForeignKey [FK_Product_Picture_Picture]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Picture]  WITH CHECK ADD  CONSTRAINT [FK_Product_Picture_Picture] FOREIGN KEY([PictureID])
REFERENCES [dbo].[Picture] ([ID])
GO
ALTER TABLE [dbo].[Product_Picture] CHECK CONSTRAINT [FK_Product_Picture_Picture]
GO
/****** Object:  ForeignKey [FK_Product_Picture_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Picture]  WITH CHECK ADD  CONSTRAINT [FK_Product_Picture_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product_Picture] CHECK CONSTRAINT [FK_Product_Picture_Product]
GO
/****** Object:  ForeignKey [FK_Product_LimitedBuy_Condition_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_LimitedBuy_Condition]  WITH CHECK ADD  CONSTRAINT [FK_Product_LimitedBuy_Condition_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product_LimitedBuy_Condition] CHECK CONSTRAINT [FK_Product_LimitedBuy_Condition_Product]
GO
/****** Object:  ForeignKey [FK_Product_LimitedBuy_Condition_User_Level]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_LimitedBuy_Condition]  WITH CHECK ADD  CONSTRAINT [FK_Product_LimitedBuy_Condition_User_Level] FOREIGN KEY([UserLevelID])
REFERENCES [dbo].[User_Level] ([ID])
GO
ALTER TABLE [dbo].[Product_LimitedBuy_Condition] CHECK CONSTRAINT [FK_Product_LimitedBuy_Condition_User_Level]
GO
/****** Object:  ForeignKey [FK_Product_LimitedBuy_Area_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_LimitedBuy_Area]  WITH CHECK ADD  CONSTRAINT [FK_Product_LimitedBuy_Area_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product_LimitedBuy_Area] CHECK CONSTRAINT [FK_Product_LimitedBuy_Area_Product]
GO
/****** Object:  ForeignKey [FK_Product_AttributeValueSet_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_AttributeValueSet]  WITH CHECK ADD  CONSTRAINT [FK_Product_AttributeValueSet_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product_AttributeValueSet] CHECK CONSTRAINT [FK_Product_AttributeValueSet_Product]
GO
/****** Object:  ForeignKey [FK_Product_AttributeValueSet_Product_Attribute]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_AttributeValueSet]  WITH CHECK ADD  CONSTRAINT [FK_Product_AttributeValueSet_Product_Attribute] FOREIGN KEY([AttributeID])
REFERENCES [dbo].[Product_Attribute] ([ID])
GO
ALTER TABLE [dbo].[Product_AttributeValueSet] CHECK CONSTRAINT [FK_Product_AttributeValueSet_Product_Attribute]
GO
/****** Object:  ForeignKey [FK_Product_AttributeValueSet_Product_AttributeValue]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_AttributeValueSet]  WITH CHECK ADD  CONSTRAINT [FK_Product_AttributeValueSet_Product_AttributeValue] FOREIGN KEY([AttributeValueID])
REFERENCES [dbo].[Product_AttributeValue] ([ID])
GO
ALTER TABLE [dbo].[Product_AttributeValueSet] CHECK CONSTRAINT [FK_Product_AttributeValueSet_Product_AttributeValue]
GO
/****** Object:  ForeignKey [FK_Order_Payment_Voucher_Order_Payment]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Payment_Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment_Voucher_Order_Payment] FOREIGN KEY([OrderPaymentID])
REFERENCES [dbo].[Order_Payment] ([ID])
GO
ALTER TABLE [dbo].[Order_Payment_Voucher] CHECK CONSTRAINT [FK_Order_Payment_Voucher_Order_Payment]
GO
/****** Object:  ForeignKey [FK_Order_Payment_Voucher_Voucher_Type]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Payment_Voucher]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment_Voucher_Voucher_Type] FOREIGN KEY([VoucherTypeID])
REFERENCES [dbo].[Voucher_Type] ([ID])
GO
ALTER TABLE [dbo].[Order_Payment_Voucher] CHECK CONSTRAINT [FK_Order_Payment_Voucher_Voucher_Type]
GO
/****** Object:  ForeignKey [FK_Order_Payment_Account_Order_Payment]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Payment_Account]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment_Account_Order_Payment] FOREIGN KEY([OrderPaymentID])
REFERENCES [dbo].[Order_Payment] ([ID])
GO
ALTER TABLE [dbo].[Order_Payment_Account] CHECK CONSTRAINT [FK_Order_Payment_Account_Order_Payment]
GO
/****** Object:  ForeignKey [FK_Group_Purchase_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Group_Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Group_Purchase_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Group_Purchase] CHECK CONSTRAINT [FK_Group_Purchase_Product]
GO
/****** Object:  ForeignKey [FK_Group_Purchase_User_Level]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Group_Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Group_Purchase_User_Level] FOREIGN KEY([UserLevelID])
REFERENCES [dbo].[User_Level] ([ID])
GO
ALTER TABLE [dbo].[Group_Purchase] CHECK CONSTRAINT [FK_Group_Purchase_User_Level]
GO
/****** Object:  ForeignKey [FK_Group_Purchase_Location_Group_Purchase]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Group_Purchase_Location]  WITH CHECK ADD  CONSTRAINT [FK_Group_Purchase_Location_Group_Purchase] FOREIGN KEY([GroupPurchaseID])
REFERENCES [dbo].[Group_Purchase] ([ID])
GO
ALTER TABLE [dbo].[Group_Purchase_Location] CHECK CONSTRAINT [FK_Group_Purchase_Location_Group_Purchase]
GO
/****** Object:  ForeignKey [FK_Product_Consult_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Consult]  WITH CHECK ADD  CONSTRAINT [FK_Product_Consult_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product_Consult] CHECK CONSTRAINT [FK_Product_Consult_Product]
GO
/****** Object:  ForeignKey [FK_Product_Consult_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Consult]  WITH CHECK ADD  CONSTRAINT [FK_Product_Consult_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Product_Consult] CHECK CONSTRAINT [FK_Product_Consult_User]
GO
/****** Object:  ForeignKey [FK_Promote_MuchBottled_Rule_Promote_MuchBottled]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MuchBottled_Rule]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MuchBottled_Rule_Promote_MuchBottled] FOREIGN KEY([MuchBottledID])
REFERENCES [dbo].[Promote_MuchBottled] ([ID])
GO
ALTER TABLE [dbo].[Promote_MuchBottled_Rule] CHECK CONSTRAINT [FK_Promote_MuchBottled_Rule_Promote_MuchBottled]
GO
/****** Object:  ForeignKey [FK_Promote_MeetAmount_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MeetAmount]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetAmount_Backstage_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetAmount] CHECK CONSTRAINT [FK_Promote_MeetAmount_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Promote_MeetAmount_Promote_MeetAmount_Scope]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MeetAmount]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetAmount_Promote_MeetAmount_Scope] FOREIGN KEY([MeetAmountScopeID])
REFERENCES [dbo].[Promote_MeetAmount_Scope] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetAmount] CHECK CONSTRAINT [FK_Promote_MeetAmount_Promote_MeetAmount_Scope]
GO
/****** Object:  ForeignKey [FK_Promote_MeetMoney_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MeetMoney]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetMoney_Backstage_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetMoney] CHECK CONSTRAINT [FK_Promote_MeetMoney_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Backstage_User_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[System_User]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_User_Backstage_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[System_User] CHECK CONSTRAINT [FK_Backstage_User_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Group_Purchase_Subscribe_Content_Group_Purchase]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Group_Purchase_Subscribe_Content]  WITH CHECK ADD  CONSTRAINT [FK_Group_Purchase_Subscribe_Content_Group_Purchase] FOREIGN KEY([GroupPurchaseID])
REFERENCES [dbo].[Group_Purchase] ([ID])
GO
ALTER TABLE [dbo].[Group_Purchase_Subscribe_Content] CHECK CONSTRAINT [FK_Group_Purchase_Subscribe_Content_Group_Purchase]
GO
/****** Object:  ForeignKey [FK_Voucher_Preferential_Binding_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Voucher_Preferential_Binding]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Preferential_Binding_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Voucher_Preferential_Binding] CHECK CONSTRAINT [FK_Voucher_Preferential_Binding_User]
GO
/****** Object:  ForeignKey [FK_Voucher_Preferential_Binding_Voucher_Preferential]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Voucher_Preferential_Binding]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Preferential_Binding_Voucher_Preferential] FOREIGN KEY([VoucherPreferentialID])
REFERENCES [dbo].[Voucher_Preferential] ([ID])
GO
ALTER TABLE [dbo].[Voucher_Preferential_Binding] CHECK CONSTRAINT [FK_Voucher_Preferential_Binding_Voucher_Preferential]
GO
/****** Object:  ForeignKey [FK_Voucher_Cash_Binding_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Voucher_Cash_Binding]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Cash_Binding_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Voucher_Cash_Binding] CHECK CONSTRAINT [FK_Voucher_Cash_Binding_User]
GO
/****** Object:  ForeignKey [FK_Voucher_Cash_Binding_Voucher_Cash]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Voucher_Cash_Binding]  WITH CHECK ADD  CONSTRAINT [FK_Voucher_Cash_Binding_Voucher_Cash] FOREIGN KEY([VoucherCashID])
REFERENCES [dbo].[Voucher_Cash] ([ID])
GO
ALTER TABLE [dbo].[Voucher_Cash_Binding] CHECK CONSTRAINT [FK_Voucher_Cash_Binding_Voucher_Cash]
GO
/****** Object:  ForeignKey [FK_User_Account_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_Account]  WITH CHECK ADD  CONSTRAINT [FK_User_Account_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_Account] CHECK CONSTRAINT [FK_User_Account_User]
GO
/****** Object:  ForeignKey [FK_User_RecieveAddress_County]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_RecieveAddress]  WITH CHECK ADD  CONSTRAINT [FK_User_RecieveAddress_County] FOREIGN KEY([CountyID])
REFERENCES [dbo].[County] ([ID])
GO
ALTER TABLE [dbo].[User_RecieveAddress] CHECK CONSTRAINT [FK_User_RecieveAddress_County]
GO
/****** Object:  ForeignKey [FK_User_RecieveAddress_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_RecieveAddress]  WITH CHECK ADD  CONSTRAINT [FK_User_RecieveAddress_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_RecieveAddress] CHECK CONSTRAINT [FK_User_RecieveAddress_User]
GO
/****** Object:  ForeignKey [FK_User_Integral_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_Integral]  WITH CHECK ADD  CONSTRAINT [FK_User_Integral_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_Integral] CHECK CONSTRAINT [FK_User_Integral_User]
GO
/****** Object:  ForeignKey [FK_User_Head_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_Head]  WITH CHECK ADD  CONSTRAINT [FK_User_Head_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_Head] CHECK CONSTRAINT [FK_User_Head_User]
GO
/****** Object:  ForeignKey [FK_User_BrowseHistory_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_BrowseHistory]  WITH CHECK ADD  CONSTRAINT [FK_User_BrowseHistory_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[User_BrowseHistory] CHECK CONSTRAINT [FK_User_BrowseHistory_Product]
GO
/****** Object:  ForeignKey [FK_User_BrowseHistory_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_BrowseHistory]  WITH CHECK ADD  CONSTRAINT [FK_User_BrowseHistory_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[User_BrowseHistory] CHECK CONSTRAINT [FK_User_BrowseHistory_User]
GO
/****** Object:  ForeignKey [FK_User_Account_Details_User_Account]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_Account_Details]  WITH CHECK ADD  CONSTRAINT [FK_User_Account_Details_User_Account] FOREIGN KEY([UserAccountID])
REFERENCES [dbo].[User_Account] ([ID])
GO
ALTER TABLE [dbo].[User_Account_Details] CHECK CONSTRAINT [FK_User_Account_Details_User_Account]
GO
/****** Object:  ForeignKey [FK_User_Integral_Details_User_Integral]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[User_Integral_Details]  WITH CHECK ADD  CONSTRAINT [FK_User_Integral_Details_User_Integral] FOREIGN KEY([UserIntegralID])
REFERENCES [dbo].[User_Integral] ([ID])
GO
ALTER TABLE [dbo].[User_Integral_Details] CHECK CONSTRAINT [FK_User_Integral_Details_User_Integral]
GO
/****** Object:  ForeignKey [FK_Backstage_User_Role_Backstage_Role]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[System_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_User_Role_Backstage_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[System_Role] ([ID])
GO
ALTER TABLE [dbo].[System_User_Role] CHECK CONSTRAINT [FK_Backstage_User_Role_Backstage_Role]
GO
/****** Object:  ForeignKey [FK_Backstage_User_Role_Backstage_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[System_User_Role]  WITH CHECK ADD  CONSTRAINT [FK_Backstage_User_Role_Backstage_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[System_User] ([ID])
GO
ALTER TABLE [dbo].[System_User_Role] CHECK CONSTRAINT [FK_Backstage_User_Role_Backstage_User]
GO
/****** Object:  ForeignKey [FK_Promote_MeetMoney_Preferential_Promote_MeetMoney]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MeetMoney_Preferential]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetMoney_Preferential_Promote_MeetMoney] FOREIGN KEY([MeetMoneyID])
REFERENCES [dbo].[Promote_MeetMoney] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetMoney_Preferential] CHECK CONSTRAINT [FK_Promote_MeetMoney_Preferential_Promote_MeetMoney]
GO
/****** Object:  ForeignKey [FK_Promote_MeetMoney_Preferential_Promote_Preferential]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MeetMoney_Preferential]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetMoney_Preferential_Promote_Preferential] FOREIGN KEY([PreferentialID])
REFERENCES [dbo].[Promote_Preferential] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetMoney_Preferential] CHECK CONSTRAINT [FK_Promote_MeetMoney_Preferential_Promote_Preferential]
GO
/****** Object:  ForeignKey [FK_Promote_MeetAmount_Preferential_Promote_MeetAmount]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MeetAmount_Preferential]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetAmount_Preferential_Promote_MeetAmount] FOREIGN KEY([MeetAmountID])
REFERENCES [dbo].[Promote_MeetAmount] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetAmount_Preferential] CHECK CONSTRAINT [FK_Promote_MeetAmount_Preferential_Promote_MeetAmount]
GO
/****** Object:  ForeignKey [FK_Promote_MeetAmount_Preferential_Promote_Preferential]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Promote_MeetAmount_Preferential]  WITH CHECK ADD  CONSTRAINT [FK_Promote_MeetAmount_Preferential_Promote_Preferential] FOREIGN KEY([PreferentialID])
REFERENCES [dbo].[Promote_Preferential] ([ID])
GO
ALTER TABLE [dbo].[Promote_MeetAmount_Preferential] CHECK CONSTRAINT [FK_Promote_MeetAmount_Preferential_Promote_Preferential]
GO
/****** Object:  ForeignKey [FK_Product_Consult_Reply_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Consult_Reply]  WITH CHECK ADD  CONSTRAINT [FK_Product_Consult_Reply_Backstage_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Product_Consult_Reply] CHECK CONSTRAINT [FK_Product_Consult_Reply_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Product_Consult_Reply_Product_Consult]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Consult_Reply]  WITH CHECK ADD  CONSTRAINT [FK_Product_Consult_Reply_Product_Consult] FOREIGN KEY([ConsultID])
REFERENCES [dbo].[Product_Consult] ([ID])
GO
ALTER TABLE [dbo].[Product_Consult_Reply] CHECK CONSTRAINT [FK_Product_Consult_Reply_Product_Consult]
GO
/****** Object:  ForeignKey [FK_Order_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
/****** Object:  ForeignKey [FK_Order_User_RecieveAddress]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User_RecieveAddress] FOREIGN KEY([RecieveAddressID])
REFERENCES [dbo].[User_RecieveAddress] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User_RecieveAddress]
GO
/****** Object:  ForeignKey [FK_Cps_OrderPushRecord_Cps]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Cps_OrderPushRecord]  WITH CHECK ADD  CONSTRAINT [FK_Cps_OrderPushRecord_Cps] FOREIGN KEY([CpsID])
REFERENCES [dbo].[Cps] ([ID])
GO
ALTER TABLE [dbo].[Cps_OrderPushRecord] CHECK CONSTRAINT [FK_Cps_OrderPushRecord_Cps]
GO
/****** Object:  ForeignKey [FK_Cps_OrderPushRecord_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Cps_OrderPushRecord]  WITH CHECK ADD  CONSTRAINT [FK_Cps_OrderPushRecord_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Cps_OrderPushRecord] CHECK CONSTRAINT [FK_Cps_OrderPushRecord_Order]
GO
/****** Object:  ForeignKey [FK_Order_Operate_Log_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Operate_Log]  WITH CHECK ADD  CONSTRAINT [FK_Order_Operate_Log_Backstage_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Order_Operate_Log] CHECK CONSTRAINT [FK_Order_Operate_Log_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Order_Operate_Log_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Operate_Log]  WITH CHECK ADD  CONSTRAINT [FK_Order_Operate_Log_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Operate_Log] CHECK CONSTRAINT [FK_Order_Operate_Log_Order]
GO
/****** Object:  ForeignKey [FK_Order_Money_Adjust_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Money_Adjust]  WITH CHECK ADD  CONSTRAINT [FK_Order_Money_Adjust_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Money_Adjust] CHECK CONSTRAINT [FK_Order_Money_Adjust_Order]
GO
/****** Object:  ForeignKey [FK_Order_Invoice_Invoice_Content]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Order_Invoice_Invoice_Content] FOREIGN KEY([InvoiceContentID])
REFERENCES [dbo].[Config_Invoice_Content] ([ID])
GO
ALTER TABLE [dbo].[Order_Invoice] CHECK CONSTRAINT [FK_Order_Invoice_Invoice_Content]
GO
/****** Object:  ForeignKey [FK_Order_Invoice_Invoice_Type]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Order_Invoice_Invoice_Type] FOREIGN KEY([InvoiceTypeID])
REFERENCES [dbo].[Config_Invoice_Type] ([ID])
GO
ALTER TABLE [dbo].[Order_Invoice] CHECK CONSTRAINT [FK_Order_Invoice_Invoice_Type]
GO
/****** Object:  ForeignKey [FK_Order_Invoice_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Order_Invoice_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Invoice] CHECK CONSTRAINT [FK_Order_Invoice_Order]
GO
/****** Object:  ForeignKey [FK_Order_Return_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Return]  WITH CHECK ADD  CONSTRAINT [FK_Order_Return_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Return] CHECK CONSTRAINT [FK_Order_Return_Order]
GO
/****** Object:  ForeignKey [FK_Order_Product_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Product]  WITH CHECK ADD  CONSTRAINT [FK_Order_Product_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Product] CHECK CONSTRAINT [FK_Order_Product_Order]
GO
/****** Object:  ForeignKey [FK_Order_Product_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Product]  WITH CHECK ADD  CONSTRAINT [FK_Order_Product_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Order_Product] CHECK CONSTRAINT [FK_Order_Product_Product]
GO
/****** Object:  ForeignKey [FK_Order_Exchange_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Exchange]  WITH CHECK ADD  CONSTRAINT [FK_Order_Exchange_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Exchange] CHECK CONSTRAINT [FK_Order_Exchange_Order]
GO
/****** Object:  ForeignKey [FK_Order_Tracking_Delivery_Corporation]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Tracking]  WITH CHECK ADD  CONSTRAINT [FK_Order_Tracking_Delivery_Corporation] FOREIGN KEY([DeliveryCorporationID])
REFERENCES [dbo].[Config_Delivery_Corporation] ([ID])
GO
ALTER TABLE [dbo].[Order_Tracking] CHECK CONSTRAINT [FK_Order_Tracking_Delivery_Corporation]
GO
/****** Object:  ForeignKey [FK_Order_Tracking_Delivery_Method]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Tracking]  WITH CHECK ADD  CONSTRAINT [FK_Order_Tracking_Delivery_Method] FOREIGN KEY([DeliveryMethodID])
REFERENCES [dbo].[Config_Delivery_Method] ([ID])
GO
ALTER TABLE [dbo].[Order_Tracking] CHECK CONSTRAINT [FK_Order_Tracking_Delivery_Method]
GO
/****** Object:  ForeignKey [FK_Order_Tracking_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Tracking]  WITH CHECK ADD  CONSTRAINT [FK_Order_Tracking_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Tracking] CHECK CONSTRAINT [FK_Order_Tracking_Order]
GO
/****** Object:  ForeignKey [FK_Order_Status_Change_Log_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Status_Change_Log]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status_Change_Log_Backstage_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Order_Status_Change_Log] CHECK CONSTRAINT [FK_Order_Status_Change_Log_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Order_Status_Change_Log_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Status_Change_Log]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status_Change_Log_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Order_Status_Change_Log] CHECK CONSTRAINT [FK_Order_Status_Change_Log_Order]
GO
/****** Object:  ForeignKey [FK_Product_Comment_Order]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Comment]  WITH CHECK ADD  CONSTRAINT [FK_Product_Comment_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[Product_Comment] CHECK CONSTRAINT [FK_Product_Comment_Order]
GO
/****** Object:  ForeignKey [FK_Product_Comment_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Comment]  WITH CHECK ADD  CONSTRAINT [FK_Product_Comment_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product_Comment] CHECK CONSTRAINT [FK_Product_Comment_Product]
GO
/****** Object:  ForeignKey [FK_Product_Comment_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Comment]  WITH CHECK ADD  CONSTRAINT [FK_Product_Comment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Product_Comment] CHECK CONSTRAINT [FK_Product_Comment_User]
GO
/****** Object:  ForeignKey [FK_Product_Comment_Reply_Product_Comment]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Comment_Reply]  WITH CHECK ADD  CONSTRAINT [FK_Product_Comment_Reply_Product_Comment] FOREIGN KEY([CommentID])
REFERENCES [dbo].[Product_Comment] ([ID])
GO
ALTER TABLE [dbo].[Product_Comment_Reply] CHECK CONSTRAINT [FK_Product_Comment_Reply_Product_Comment]
GO
/****** Object:  ForeignKey [FK_Product_Comment_Reply_User]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Product_Comment_Reply]  WITH CHECK ADD  CONSTRAINT [FK_Product_Comment_Reply_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Product_Comment_Reply] CHECK CONSTRAINT [FK_Product_Comment_Reply_User]
GO
/****** Object:  ForeignKey [FK_Order_Tracking_Logistics_Order_Tracking]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Tracking_Logistics]  WITH CHECK ADD  CONSTRAINT [FK_Order_Tracking_Logistics_Order_Tracking] FOREIGN KEY([OrderTrackingID])
REFERENCES [dbo].[Order_Tracking] ([ID])
GO
ALTER TABLE [dbo].[Order_Tracking_Logistics] CHECK CONSTRAINT [FK_Order_Tracking_Logistics_Order_Tracking]
GO
/****** Object:  ForeignKey [FK_Order_Return_Product_Order_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Return_Product]  WITH CHECK ADD  CONSTRAINT [FK_Order_Return_Product_Order_Product] FOREIGN KEY([OrderProductID])
REFERENCES [dbo].[Order_Product] ([ID])
GO
ALTER TABLE [dbo].[Order_Return_Product] CHECK CONSTRAINT [FK_Order_Return_Product_Order_Product]
GO
/****** Object:  ForeignKey [FK_Order_Return_Product_Order_Return]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Return_Product]  WITH CHECK ADD  CONSTRAINT [FK_Order_Return_Product_Order_Return] FOREIGN KEY([OrderReturnID])
REFERENCES [dbo].[Order_Return] ([ID])
GO
ALTER TABLE [dbo].[Order_Return_Product] CHECK CONSTRAINT [FK_Order_Return_Product_Order_Return]
GO
/****** Object:  ForeignKey [FK_Order_Return_Audit_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Return_Audit]  WITH CHECK ADD  CONSTRAINT [FK_Order_Return_Audit_Backstage_Employee] FOREIGN KEY([CustomerServiceID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Order_Return_Audit] CHECK CONSTRAINT [FK_Order_Return_Audit_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Order_Return_Audit_Backstage_Employee1]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Return_Audit]  WITH CHECK ADD  CONSTRAINT [FK_Order_Return_Audit_Backstage_Employee1] FOREIGN KEY([InventoryKeeperID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Order_Return_Audit] CHECK CONSTRAINT [FK_Order_Return_Audit_Backstage_Employee1]
GO
/****** Object:  ForeignKey [FK_Order_Return_Audit_Order_Return]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Return_Audit]  WITH CHECK ADD  CONSTRAINT [FK_Order_Return_Audit_Order_Return] FOREIGN KEY([OrderReturnID])
REFERENCES [dbo].[Order_Return] ([ID])
GO
ALTER TABLE [dbo].[Order_Return_Audit] CHECK CONSTRAINT [FK_Order_Return_Audit_Order_Return]
GO
/****** Object:  ForeignKey [FK_Order_Exchange_Product_Order_Exchange]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Exchange_Product]  WITH CHECK ADD  CONSTRAINT [FK_Order_Exchange_Product_Order_Exchange] FOREIGN KEY([OrderExechangeID])
REFERENCES [dbo].[Order_Exchange] ([ID])
GO
ALTER TABLE [dbo].[Order_Exchange_Product] CHECK CONSTRAINT [FK_Order_Exchange_Product_Order_Exchange]
GO
/****** Object:  ForeignKey [FK_Order_Exchange_Product_Order_Product]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Exchange_Product]  WITH CHECK ADD  CONSTRAINT [FK_Order_Exchange_Product_Order_Product] FOREIGN KEY([OrderProductID])
REFERENCES [dbo].[Order_Product] ([ID])
GO
ALTER TABLE [dbo].[Order_Exchange_Product] CHECK CONSTRAINT [FK_Order_Exchange_Product_Order_Product]
GO
/****** Object:  ForeignKey [FK_Order_Exchange_Audit_Backstage_Employee]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Exchange_Audit]  WITH CHECK ADD  CONSTRAINT [FK_Order_Exchange_Audit_Backstage_Employee] FOREIGN KEY([CustomerServiceID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Order_Exchange_Audit] CHECK CONSTRAINT [FK_Order_Exchange_Audit_Backstage_Employee]
GO
/****** Object:  ForeignKey [FK_Order_Exchange_Audit_Backstage_Employee1]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Exchange_Audit]  WITH CHECK ADD  CONSTRAINT [FK_Order_Exchange_Audit_Backstage_Employee1] FOREIGN KEY([InventoryKeeperID])
REFERENCES [dbo].[System_Employee] ([ID])
GO
ALTER TABLE [dbo].[Order_Exchange_Audit] CHECK CONSTRAINT [FK_Order_Exchange_Audit_Backstage_Employee1]
GO
/****** Object:  ForeignKey [FK_Order_Exchange_Audit_Order_Exchange]    Script Date: 10/18/2013 18:24:44 ******/
ALTER TABLE [dbo].[Order_Exchange_Audit]  WITH CHECK ADD  CONSTRAINT [FK_Order_Exchange_Audit_Order_Exchange] FOREIGN KEY([OrderExechangeID])
REFERENCES [dbo].[Order_Exchange] ([ID])
GO
ALTER TABLE [dbo].[Order_Exchange_Audit] CHECK CONSTRAINT [FK_Order_Exchange_Audit_Order_Exchange]
GO
