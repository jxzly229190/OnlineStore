USE [goujiuwang_v5_test]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddOrder]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_AddOrder
-- Author:	张连印
-- Description:	后台添加订单信息
-- ==========================================================================================

Create Procedure [dbo].[sp_AddOrder]
@xml nvarchar(4000),
@totalMoney float output,
@totalIntegral float output,
@paymentMoney float output
As 
Begin
	
	DECLARE @doc nvarchar(1000)
	SET @doc = '<Order OrderID = "1011">
	   <Item ProductID="1" Quantity="2" Price="98"/>
	   <Item ProductID="2" Quantity="1" Price="99.5"/>
	  </Order>'
	DECLARE @xmlDoc integer
	EXEC sp_xml_preparedocument @xmlDoc OUTPUT, @doc
	SELECT *, Quantity*Price [Money] FROM
	OPENXML (@xmlDoc, 'Order/Item', 1)
	WITH
	(OrderID integer '../@OrderID',
	 ProductID integer,
	 Quantity integer,
	 Price float)
	EXEC sp_xml_removedocument @xmlDoc

End
GO
/****** Object:  Table [dbo].[Province]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Province](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[AreaID] [int] NULL,
	[Sorting] [int] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省会数据表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Province'
GO
/****** Object:  Table [dbo].[Promote_MuchBottled_Rule]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MuchBottled_Rule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MuchBottledID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[DiscountAmount] [float] NOT NULL,
	[TotalMoney] [float] NOT NULL,
	[ImageUrl] [nvarchar](128) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多瓶装促销活动规则名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠金额（(商品原价 - 单价) * 数量 = 优惠金额）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'DiscountAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总金额（单价 * 数量 = 总金额）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'TotalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缩略图路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'ImageUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否默认显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多瓶装促销活动规则表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled_Rule'
GO
/****** Object:  Table [dbo].[Promote_MuchBottled]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MuchBottled](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IsOnlinePayment] [bit] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[IsDisplayTime] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Promote_MuchBottled] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多瓶装促销活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否仅限在线支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'IsOnlinePayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否显示时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'IsDisplayTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'多瓶装促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MuchBottled'
GO
/****** Object:  Table [dbo].[Promote_MeetMoney_Scope]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetMoney_Scope](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MeetMoneyID] [int] NOT NULL,
	[Scope] [text] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Promote_MeetMoney_Scope] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Scope', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Scope', @level2type=N'COLUMN',@level2name=N'MeetMoneyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参与活动的商品' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Scope', @level2type=N'COLUMN',@level2name=N'Scope'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Scope', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
/****** Object:  Table [dbo].[Promote_MeetMoney_Rule]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetMoney_Rule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PromoteMeetMoneyID] [int] NOT NULL,
	[MeetMoney] [float] NOT NULL,
	[IsNoCeiling] [bit] NULL,
	[DecreaseCash] [float] NULL,
	[IsDecreaseCash] [bit] NOT NULL,
	[IsGiveGift] [bit] NOT NULL,
	[ProductID] [int] NULL,
	[IsGiveIntegral] [bit] NOT NULL,
	[Integral] [int] NULL,
	[IsNoPostage] [bit] NOT NULL,
	[IsGiveCoupon] [bit] NOT NULL,
	[CouponType] [int] NULL,
	[CouponID] [int] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Promote_MeetMoney_Rule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'PromoteMeetMoneyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满足金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'MeetMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否上不封顶' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'IsNoCeiling'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'减现金数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'DecreaseCash'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否减现金' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'IsDecreaseCash'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否送礼物' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'IsGiveGift'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠礼品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否送积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'IsGiveIntegral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠积分数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否免邮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'IsNoPostage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否送优惠券' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'IsGiveCoupon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠券类型（0：现金券，1：满减券）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'CouponType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动赠券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'CouponID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送促销活动规则表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney_Rule'
GO
/****** Object:  Table [dbo].[Promote_MeetMoney]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetMoney](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Description] [nvarchar](128) NULL,
	[IsMobileValidate] [bit] NULL,
	[IsUseCoupon] [bit] NULL,
	[IsNewUser] [bit] NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动描述信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要手机验证（true:手机验证后才可参与）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'IsMobileValidate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可使用优惠券（true:可以使用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'IsUseCoupon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是新会员活动（true:只能新会员参与）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'IsNewUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动状态（1：可用，2：停用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetMoney'
GO
/****** Object:  Table [dbo].[Promote_MeetAmount_Scope]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetAmount_Scope](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MeetAmountID] [int] NOT NULL,
	[Scope] [text] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Promote_MeetAmount_Scope] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满就送活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope', @level2type=N'COLUMN',@level2name=N'MeetAmountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'参与活动的商品' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope', @level2type=N'COLUMN',@level2name=N'Scope'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Scope', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
/****** Object:  Table [dbo].[Promote_MeetAmount_Rule]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetAmount_Rule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PromoteMeetAmountID] [int] NOT NULL,
	[MeetAmount] [int] NOT NULL,
	[IsDiscount] [bit] NOT NULL,
	[Discount] [float] NULL,
	[IsNoPostage] [bit] NOT NULL,
	[IsGiveGift] [bit] NOT NULL,
	[ProductID] [int] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Promote_MeetAmount_Rule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠促销活动编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'PromoteMeetAmountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满足件数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'MeetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否打折' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'IsDiscount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'打折优惠折扣' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'Discount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否包邮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'IsNoPostage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否送礼物' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'IsGiveGift'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'礼物的商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount_Rule', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
/****** Object:  Table [dbo].[Promote_MeetAmount]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_MeetAmount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[IsMobileValidate] [bit] NULL,
	[IsUseCoupon] [bit] NULL,
	[IsNewUser] [bit] NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Description] [nvarchar](128) NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Promote_MeetAmount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要手机验证（true:手机验证后才可参与）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'IsMobileValidate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可使用优惠券（true:可以使用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'IsUseCoupon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是新会员活动（true:只能新会员参与）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'IsNewUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动状态（1：可用，2：暂停，3：停止）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满件优惠促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_MeetAmount'
GO
/****** Object:  Table [dbo].[Promote_Limited_Discount]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promote_Limited_Discount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Discount] [float] NOT NULL,
	[DiscountPrice] [float] NOT NULL,
	[TotalQuantity] [int] NOT NULL,
	[LimitedBuyQuantity] [int] NOT NULL,
	[IsMobileValidate] [bit] NULL,
	[IsUseCoupon] [bit] NULL,
	[IsNewUser] [bit] NULL,
	[IsOnlinePayment] [bit] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Promote_Limited_Discount] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'折扣' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'Discount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单品折后价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'DiscountPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动商品总数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'TotalQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'每人限制购买数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'LimitedBuyQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要手机验证（true:手机验证后才可参与）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'IsMobileValidate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可使用优惠券（true:可以使用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'IsUseCoupon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否是新会员活动（true:只能新会员参与）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'IsNewUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否仅支持在线支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'IsOnlinePayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动状态（1：可用，2：暂停，3：停止）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限时打折促销活动表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_Limited_Discount'
GO
/****** Object:  Table [dbo].[Promote_LandingPage]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Promote_LandingPage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NULL,
	[Name] [varchar](200) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Link] [varchar](1000) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[Content] [text] NULL,
	[MasterPicture] [varchar](1000) NULL,
	[Picture01] [varchar](1000) NULL,
	[Picture02] [varchar](1000) NULL,
	[Picture03] [varchar](1000) NULL,
	[Picture04] [varchar](1000) NULL,
	[Picture05] [varchar](1000) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LP主题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动页连接地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Link'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态(0 未启用 1 启用 2 关闭 3 暂停)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LP内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主图' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'MasterPicture'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副图01' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Picture01'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副图02' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Picture02'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副图03' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Picture03'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副图04' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Picture04'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'副图05' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'Picture05'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Promote_LandingPage', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
/****** Object:  Table [dbo].[Product_Status_Log]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Status_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[StatusCode] [int] NOT NULL,
	[Description] [nvarchar](128) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Product_Status_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Status_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Status_Log', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Status_Log', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品状态编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Status_Log', @level2type=N'COLUMN',@level2name=N'StatusCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品状态变更描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Status_Log', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Status_Log', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品状态日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Status_Log'
GO
/****** Object:  Table [dbo].[Product_Search_KeyWord]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Search_KeyWord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BussinessID] [int] NULL,
	[BussinessType] [int] NULL,
	[BussinessName] [nvarchar](128) NULL,
	[Keyword] [varchar](8000) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Search_KeyWord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Search_KeyWord', @level2type=N'COLUMN',@level2name=N'BussinessID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务类型(0:大类 1:品牌 2:地区 )' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Search_KeyWord', @level2type=N'COLUMN',@level2name=N'BussinessType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Search_KeyWord', @level2type=N'COLUMN',@level2name=N'BussinessName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关键词' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Search_KeyWord', @level2type=N'COLUMN',@level2name=N'Keyword'
GO
/****** Object:  Table [dbo].[Product_Search]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Search](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[ProductName] [nvarchar](128) NULL,
	[ProductNamePinYin] [varchar](500) NULL,
	[ProductBarcode] [varchar](64) NULL,
	[ProductCategory] [varchar](100) NULL,
	[ProductCategoryPinYin] [varchar](100) NULL,
	[ProductBrand] [varchar](100) NULL,
	[ProductBrandPinYin] [varchar](100) NULL,
	[ParentCategory] [varchar](100) NULL,
	[ParentCategoryPinYin] [varchar](100) NULL,
	[ParentBrand] [varchar](100) NULL,
	[ParentBrandPinYin] [varchar](100) NULL,
	[MarketPrice] [float] NULL,
	[GoujiuPrice] [float] NULL,
	[ProductSearchText]  AS ((((((((((((((((((((coalesce([ProductCategoryPinYin],'')+'-')+coalesce([ParentBrandPinYin],''))+'-')+coalesce([ProductBrandPinYin],''))+'-')+coalesce([ProductNamePinYin],''))+'-')+coalesce([ProductName],''))+'-')+coalesce([ProductCategory],''))+'-')+coalesce([ProductBrand],''))+'-')+coalesce([ParentCategory],''))+'-')+coalesce([ParentBrand],''))+'-')+coalesce(CONVERT([varchar](50),[GoujiuPrice],(0)),''))+'-')+coalesce([ProductBarcode],'')) PERSISTED,
	[Status] [int] NULL,
 CONSTRAINT [PK_Product_Search] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Product_Picture]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Picture](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[PictureID] [int] NOT NULL,
	[IsMaster] [bit] NOT NULL,
	[IsDelete] [int] NULL,
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
/****** Object:  Table [dbo].[Product_LimitedBuy_Condition]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_LimitedBuy_Condition](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[LimitedDays] [int] NOT NULL,
	[LimitedQuantity] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品限制购买天数（例如：7天内只能购买一次）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'LimitedDays'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品限制购买数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'LimitedQuantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制购买状态（1：未启用，2：启用中，3：已停用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品限制购买条件表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Condition'
GO
/****** Object:  Table [dbo].[Product_LimitedBuy_Area]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_LimitedBuy_Area](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[AreaID] [int] NOT NULL,
	[AreaType] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'AreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域类型（1：省会，2：城市，3：区县）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'AreaType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品购买限制区域表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_LimitedBuy_Area'
GO
/****** Object:  Table [dbo].[Product_Consult_Reply]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Consult_Reply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConsultID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Content] [nvarchar](256) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](32) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Product_Consult_Reply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'ConsultID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询回复内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品咨询回复表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult_Reply'
GO
/****** Object:  Table [dbo].[Product_Consult]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Consult](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ConsultPerson] [nvarchar](16) NULL,
	[ConsultPersonMobile] [varchar](16) NULL,
	[ConsultPersonEmail] [varchar](64) NULL,
	[Content] [nvarchar](256) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](32) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Product_Consult] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询用户编号（非网址用户编号为：0）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'ConsultPerson'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询人手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'ConsultPersonMobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'咨询内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品咨询表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Consult'
GO
/****** Object:  Table [dbo].[Product_Comment_Reply]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Comment_Reply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CommentID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[Content] [nvarchar](256) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](32) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Product_Comment_Reply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'CommentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回复用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父回复编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论回复内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品评论回复表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment_Reply'
GO
/****** Object:  Table [dbo].[Product_Comment]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Content] [nvarchar](1000) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[EmployeeID] [int] NULL,
	[AuditTime] [datetime] NULL,
	[AuditDescription] [nvarchar](128) NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](32) NULL,
 CONSTRAINT [PK_Product_Comment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论分数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评论审核状态（1：未通过，2：已通过，3：已锁定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'AuditTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment', @level2type=N'COLUMN',@level2name=N'AuditDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品评论表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Comment'
GO
/****** Object:  Table [dbo].[Product_Category]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[CategoryName] [nvarchar](16) NOT NULL,
	[CategoryNameSpell] [varchar](32) NOT NULL,
	[CategoryNameEnglish] [varchar](32) NULL,
	[SEOKeywords] [nvarchar](512) NULL,
	[SEODescription] [nvarchar](512) NULL,
	[IsGjw] [bit] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[Layer] [int] NOT NULL,
	[Sorting] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](1024) NULL,
 CONSTRAINT [PK_Product_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CategoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别名称拼音' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CategoryNameSpell'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别名称英文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CategoryNameEnglish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别 SE0 关键字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'SEOKeywords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别 SE0 描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'SEODescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别是否属于购酒网' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'IsGjw'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别是否显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Category'
GO
/****** Object:  Table [dbo].[Product_Brand]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product_Brand](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[BrandName] [nvarchar](200) NOT NULL,
	[BrandNameSpell] [varchar](200) NOT NULL,
	[BrandNameEnglish] [varchar](200) NULL,
	[SEOKeywords] [nvarchar](512) NULL,
	[SEODescription] [nvarchar](512) NULL,
	[IsDisplay] [bit] NOT NULL,
	[Layer] [int] NOT NULL,
	[Sorting] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](1024) NULL,
 CONSTRAINT [PK_Product_Brand_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'ProductCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父品牌编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'BrandName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌名称拼音' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'BrandNameSpell'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌名称英文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'BrandNameEnglish'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌 SEO 关键字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'SEOKeywords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌 SEO 描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'SEODescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌是否显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Brand'
GO
/****** Object:  Table [dbo].[Product_AttributeValueSet]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_AttributeValueSet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[AttributeID] [int] NOT NULL,
	[AttributeValueID] [int] NOT NULL,
	[IsDelete] [int] NULL,
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
/****** Object:  Table [dbo].[Product_AttributeValue]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_AttributeValue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AttributeID] [int] NOT NULL,
	[AttributeValue] [nvarchar](64) NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[IsDefault] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性值表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_AttributeValue'
GO
/****** Object:  Table [dbo].[Product_Attribute]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Attribute](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[AttributeName] [nvarchar](32) NOT NULL,
	[Sorting] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[InputType] [nvarchar](50) NULL,
	[DataType] [nvarchar](50) NULL,
	[DataLength] [int] NULL,
	[AttributeCode] [nvarchar](50) NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'AttributeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'InputType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'DataType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute', @level2type=N'COLUMN',@level2name=N'DataLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品属性表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product_Attribute'
GO
/****** Object:  Table [dbo].[Product]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentCategoryID] [int] NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[ParentBrandID] [int] NOT NULL,
	[ProductBrandID] [int] NULL,
	[Barcode] [varchar](64) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[SEOTitle] [nvarchar](128) NULL,
	[SEOKeywords] [nvarchar](512) NULL,
	[SEODescription] [nvarchar](512) NULL,
	[Advertisement] [nvarchar](128) NULL,
	[MarketPrice] [float] NOT NULL,
	[GoujiuPrice] [float] NOT NULL,
	[Introduce] [ntext] NOT NULL,
	[Integral] [int] NOT NULL,
	[InventoryNumber] [int] NOT NULL,
	[CommentNumber] [int] NOT NULL,
	[CommentScore] [float] NOT NULL,
	[SoldOfReality] [int] NOT NULL,
	[SoldOfVirtual] [int] NOT NULL,
	[PageView] [int] NOT NULL,
	[Sorting] [int] NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[Attributes] [nvarchar](4000) NULL,
	[PromoteEndTime] [datetime] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ParentCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一级品牌编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ParentBrandID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'ProductBrandID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品条形码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Barcode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品 SEO 标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SEOTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品 SEO 关键字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SEOKeywords'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品 SEO 描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SEODescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品广告词' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Advertisement'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品市场价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'MarketPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品购酒价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'GoujiuPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品详细介绍信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Introduce'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购买商品所赠积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品库存数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'InventoryNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品评论数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'CommentNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品真实销售数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SoldOfReality'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品虚拟销售数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'SoldOfVirtual'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品页面浏览数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'PageView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品状态（1：未上架，2：已上架，3：已下架，4：已回收）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展属性值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product', @level2type=N'COLUMN',@level2name=N'Attributes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Product'
GO
/****** Object:  Table [dbo].[plus_file]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[plus_file](
	[id] [varchar](50) NULL,
	[name] [varchar](500) NULL,
	[type] [varchar](50) NULL,
	[size] [varchar](50) NULL,
	[url] [varchar](500) NULL,
	[pid] [varchar](50) NULL,
	[createdate] [datetime] NULL,
	[updatedate] [datetime] NULL,
	[folder] [int] NULL,
	[num] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Picture_Category]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Picture_Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Sorting] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Picture_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父图片分类编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片分类名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片分类表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture_Category'
GO
/****** Object:  Table [dbo].[Picture]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Picture](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentCategoryID] [int] NOT NULL,
	[ProductCategoryID] [int] NULL,
	[ParentBrandID] [int] NULL,
	[ProductBrandID] [int] NULL,
	[Name] [varchar](64) NOT NULL,
	[Path] [varchar](128) NULL,
	[ThumbnailPath] [varchar](128) NULL,
	[FileName] [varchar](64) NOT NULL,
	[Type] [varchar](8) NOT NULL,
	[Size] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[UploadTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品品牌编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'ProductBrandID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片存放路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缩略图存放路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'ThumbnailPath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片文件名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片类型（即：扩展名）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片大小（单位：byte）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Size'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片高度（单位：px）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片宽度（单位：px）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片状态（1：未引用，2：已引用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'UploadTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Picture'
GO
/****** Object:  UserDefinedTableType [dbo].[OrderProductTable]    Script Date: 03/02/2014 20:58:37 ******/
CREATE TYPE [dbo].[OrderProductTable] AS TABLE(
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TransactPrice] [float] NOT NULL,
	[PromotionID] [nvarchar](200) NULL,
	[PromotionType] [nvarchar](500) NULL,
	[PromotionResult] [nvarchar](200) NULL,
	[MarketPrice] [float] NULL,
	[GoujiuPrice] [float] NULL,
	[ProductName] [nvarchar](128) NULL,
	[Integral] [int] NULL,
	[RebateRate] [float] NULL,
	[Commission] [float] NULL,
	[Remark] [nvarchar](512) NULL,
	[ExtField] [nvarchar](max) NULL,
	[CreateTime] [datetime] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OrderProductPromoteTable]    Script Date: 03/02/2014 20:58:37 ******/
CREATE TYPE [dbo].[OrderProductPromoteTable] AS TABLE(
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[OrderProductID] [int] NULL,
	[PromoteDiscount] [float] NULL,
	[PromoteType] [int] NOT NULL,
	[PromoteID] [int] NOT NULL,
	[Remark] [nvarchar](512) NULL,
	[ExtField] [nvarchar](256) NULL,
	[CreateTime] [datetime] NOT NULL
)
GO
/****** Object:  Table [dbo].[Order_Status_Tracking]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Status_Tracking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[EmployeeID] [int] NULL,
	[UserID] [int] NULL,
	[Remark] [nvarchar](2000) NULL,
	[Status] [int] NOT NULL,
	[ExpressNumber] [varchar](20) NULL,
	[MailNo] [varchar](64) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](128) NULL,
 CONSTRAINT [PK_Order_Status_Tracking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单变更具体信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单变更具体状态编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司代号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'ExpressNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快递单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'MailNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态跟踪信息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Tracking'
GO
/****** Object:  Table [dbo].[Order_Status_Log]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Status_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Remark] [nvarchar](512) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Order_Status_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Status_Log'
GO
/****** Object:  Table [dbo].[Order_Product_Promote]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Product_Promote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[OrderProductID] [int] NULL,
	[PromoteType] [int] NOT NULL,
	[PromoteResult] [int] NULL,
	[PromoteDiscount] [float] NULL,
	[PromoteID] [int] NOT NULL,
	[Remark] [nvarchar](256) NULL,
	[ExtField] [nvarchar](512) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK__Order_Pr__3214EC270CC5D56F] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单商品编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product_Promote', @level2type=N'COLUMN',@level2name=N'OrderProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动类型（限时抢购：1，团购：2，等级价格：3，满额促销：11，满件促销：12）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product_Promote', @level2type=N'COLUMN',@level2name=N'PromoteType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'促销优惠金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product_Promote', @level2type=N'COLUMN',@level2name=N'PromoteDiscount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'促销编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product_Promote', @level2type=N'COLUMN',@level2name=N'PromoteID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product_Promote', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product_Promote', @level2type=N'COLUMN',@level2name=N'ExtField'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product_Promote', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
/****** Object:  Table [dbo].[Order_Product]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[GoujiuPrice] [float] NULL,
	[TransactPrice] [float] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[PromotionID] [nvarchar](200) NULL,
	[PromotionType] [nvarchar](500) NULL,
	[PromotionResult] [nvarchar](200) NULL,
	[MarketPrice] [float] NULL,
	[ProductName] [nvarchar](128) NULL,
	[Integral] [int] NULL,
	[RebateRate] [float] NULL,
	[Commission] [float] NULL,
	[Remark] [nvarchar](100) NULL,
	[ExtField] [nvarchar](128) NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'购酒网价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'GoujiuPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成交价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'TransactPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市场价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'MarketPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠送积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返利比率' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'RebateRate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系列佣金比例 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'Commission'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注、描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Product'
GO
/****** Object:  Table [dbo].[Order_Payment_Account]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Payment_Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderPaymentID] [int] NOT NULL,
	[UserAccountID] [int] NOT NULL,
	[Deduction] [float] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Order_Payment_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'OrderPaymentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员账户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'UserAccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员账户抵扣金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'Deduction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付虚拟账户支付表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment_Account'
GO
/****** Object:  Table [dbo].[Order_Payment]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[PaymentOrgID] [int] NOT NULL,
	[PaymentMoney] [float] NOT NULL,
	[IsUseCoupon] [bit] NOT NULL,
	[IsUseIntegral] [bit] NOT NULL,
	[IsUseAccount] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[TradeNo] [varchar](64) NULL,
 CONSTRAINT [PK_Order_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'PaymentOrgID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'PaymentMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用优惠券支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'IsUseCoupon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用积分支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'IsUseIntegral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用账户余额支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'IsUseAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'第三方交易号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment', @level2type=N'COLUMN',@level2name=N'TradeNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单支付表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Payment'
GO
/****** Object:  Table [dbo].[Order_Invoice]    Script Date: 03/02/2014 20:58:35 ******/
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
	[InvoiceCost] [float] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[InvoiceContent] [nvarchar](50) NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'InvoiceCost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单发票表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Invoice'
GO
/****** Object:  Table [dbo].[Order_Erp_Log]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Erp_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ERP] [nvarchar](64) NOT NULL,
	[OrderID] [int] NOT NULL,
	[OperateType] [int] NOT NULL,
	[UserID] [int] NULL,
	[ReqContent] [nvarchar](max) NULL,
	[ResContent] [nvarchar](max) NULL,
	[IsSuccess] [bit] NOT NULL,
	[Operator] [int] NULL,
	[ExtField] [nvarchar](max) NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Erp_log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单推送ERP编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ERP名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'ERP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类别（1：订单推送（下单），2：订单状态查询）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'OperateType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请求内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'ReqContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ERP返回内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'ResContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否操作成功（0：失败，1：成功）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'IsSuccess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人编码（通常是SystemUser，若0或Null，则为系统）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'Operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Erp_Log', @level2type=N'COLUMN',@level2name=N'ExtField'
GO
/****** Object:  Table [dbo].[Order_Display_Reply]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Display_Reply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDisplayID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[Content] [nvarchar](256) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父晒单回复编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回复内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单晒单回复表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Reply'
GO
/****** Object:  Table [dbo].[Order_Display_Image]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Display_Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDisplayID] [int] NOT NULL,
	[URL] [varchar](128) NOT NULL,
	[Sorting] [int] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image', @level2type=N'COLUMN',@level2name=N'OrderDisplayID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单晒单图片表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display_Image'
GO
/****** Object:  Table [dbo].[Order_Display]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Display](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Score] [int] NOT NULL,
	[Content] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[EmployeeID] [int] NULL,
	[AuditTime] [datetime] NULL,
	[AuditDescription] [nvarchar](128) NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单分数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'Score'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单审核人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'AuditTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'晒单审核描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display', @level2type=N'COLUMN',@level2name=N'AuditDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单晒单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Display'
GO
/****** Object:  Table [dbo].[Order_Discount]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Discount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[Money] [float] NOT NULL,
	[DType] [tinyint] NOT NULL,
	[IsDelete] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ExtField] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单折扣记录（odr_OrderDiscount）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Discount'
GO
/****** Object:  Table [dbo].[Order_Delivery_Tracking_old]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order_Delivery_Tracking_old](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[DeliveryMethodID] [int] NOT NULL,
	[DeliveryCorporationID] [int] NOT NULL,
	[DeliveryNumber] [varchar](64) NULL,
	[IsDelete] [int] NULL,
	[CreateTime] [datetime] NULL,
	[Status] [varchar](16) NULL,
 CONSTRAINT [PK_Order_Delivery_Tracking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_old', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_old', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送方式编号(0：公司直送，1：第三方快递，2：物流)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_old', @level2type=N'COLUMN',@level2name=N'DeliveryMethodID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_old', @level2type=N'COLUMN',@level2name=N'DeliveryCorporationID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_old', @level2type=N'COLUMN',@level2name=N'DeliveryNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单配送跟踪表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_old'
GO
/****** Object:  Table [dbo].[Order_Delivery_Tracking_Details]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Delivery_Tracking_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderDeilveryTrackingID] [int] NOT NULL,
	[OperateTime] [datetime] NOT NULL,
	[OperateSummary] [nvarchar](512) NOT NULL,
	[Operator] [nvarchar](16) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Order_Delivery_Tracking_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_Details', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单配送跟踪编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_Details', @level2type=N'COLUMN',@level2name=N'OrderDeilveryTrackingID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_Details', @level2type=N'COLUMN',@level2name=N'OperateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作概要' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_Details', @level2type=N'COLUMN',@level2name=N'OperateSummary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_Details', @level2type=N'COLUMN',@level2name=N'Operator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单配送跟踪明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking_Details'
GO
/****** Object:  Table [dbo].[Order_Delivery_Tracking]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Delivery_Tracking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[Express] [nvarchar](64) NULL,
	[MailNo] [nvarchar](64) NOT NULL,
	[Status] [nvarchar](32) NULL,
	[StatusTime] [datetime] NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[Steps] [nvarchar](max) NOT NULL,
	[GJWStatus] [int] NOT NULL,
	[ExtField] [nvarchar](max) NULL,
	[IsDelete] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Order_Delivery_Tracking_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快递公司' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking', @level2type=N'COLUMN',@level2name=N'Express'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快递单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Delivery_Tracking', @level2type=N'COLUMN',@level2name=N'MailNo'
GO
/****** Object:  Table [dbo].[Order_Cancel_Cause]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Cancel_Cause](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Cause] [nvarchar](32) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Order_Cancel_Cause] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel_Cause', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单取消原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel_Cause', @level2type=N'COLUMN',@level2name=N'Cause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel_Cause', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单取消原因表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel_Cause'
GO
/****** Object:  Table [dbo].[Order_Cancel]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Cancel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[OrderCancelCauseID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Description] [nvarchar](256) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Order_Apply_Cancel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单申请取消原因编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel', @level2type=N'COLUMN',@level2name=N'OrderCancelCauseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'后台员工操作编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单申请取消备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'未出库订单取消表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order_Cancel'
GO
/****** Object:  Table [dbo].[Order]    Script Date: 03/02/2014 20:58:35 ******/
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
	[CpsID] [int] NULL,
	[PaymentMethodID] [int] NOT NULL,
	[OrderCode] [varchar](64) NOT NULL,
	[OrderNumber] [varchar](64) NOT NULL,
	[DeliveryCost] [float] NULL,
	[DeliveryCorporationID] [int] NULL,
	[TotalMoney] [float] NOT NULL,
	[Discount] [float] NULL,
	[TotalIntegral] [int] NOT NULL,
	[PaymentStatus] [int] NOT NULL,
	[IsRequireInvoice] [bit] NULL,
	[Status] [int] NOT NULL,
	[Description] [nvarchar](512) NULL,
	[Remark] [nvarchar](512) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[v4_TransferTime] [nvarchar](40) NULL,
	[v4_cps_User] [varchar](32) NULL,
	[v4_CookieFile] [varchar](80) NULL,
	[v4_ClientIP] [varchar](32) NULL,
	[v4_ClientBrowser] [nvarchar](256) NULL,
	[v4_Warn] [tinyint] NULL,
	[v4_UpdateInventoryNo] [bit] NULL,
	[v4_ApiType] [int] NULL,
	[v4_ApiOrderNo] [varchar](50) NULL,
	[ExtField] [nvarchar](50) NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货地址编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'RecieveAddressID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式编号(0： 在线支付；1 货到付款)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'PaymentMethodID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号（按一定生成规则生成）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单流水号（当天时间 + 订单索引，例如：201311070001）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单运费' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'DeliveryCost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'DeliveryCorporationID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'TotalMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单总积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'TotalIntegral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付状态（0：未支付，1：已支付）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'PaymentStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否需要开发票' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'IsRequireInvoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态（100：待付款，0：待确认，1：已确认，2：已发货，3：已签收，4：ERP 作废，5：已损失，6：已取消，8：官网作废）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户订单补充说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工操作订单备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order'
GO
/****** Object:  Table [dbo].[hw_Log]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hw_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](50) NULL,
	[Content] [ntext] NULL,
	[State] [tinyint] NULL,
	[CreateTime] [datetime] NULL,
	[ExtField] [nvarchar](50) NULL,
 CONSTRAINT [PK_hw_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetThumbnailPath]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetThumbnailPath] (@filePath as nvarchar(1000))
Returns Varchar(1000)
AS
BEGIN
	Declare @fileThumbnailPath Varchar(200)
	If @filePath Is Null Or @filePath = '' 
	Begin
		Set @fileThumbnailPath = ''
	End
	Else
	Begin
		SET @filePath = REVERSE(@filePath)
		SET @fileThumbnailPath = LEFT(@filePath,CHARINDEX('/',@filePath,0)-1) + '/' + Right(@filePath,Len(@filePath) - (CHARINDEX('/',@filePath,0)-1))
		SET @fileThumbnailPath = REVERSE(@fileThumbnailPath)
	End 
	Return(@fileThumbnailPath)
END
GO
/****** Object:  Table [dbo].[Cps_OrderPushRecord]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cps_OrderPushRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CpsID] [int] NOT NULL,
	[OrderCode] [varchar](64) NOT NULL,
	[PushURL] [varchar](2048) NOT NULL,
	[AcceptParameter] [varchar](1024) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](50) NULL,
	[OrderID] [int] NULL,
 CONSTRAINT [PK_Cps_OrderPushRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'OrderCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 订单推送地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'PushURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'AcceptParameter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 订单推送记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_OrderPushRecord'
GO
/****** Object:  Table [dbo].[Cps_LinkRecord]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cps_LinkRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CpsID] [int] NOT NULL,
	[URL] [nvarchar](1024) NULL,
	[TargetURL] [nvarchar](4000) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](50) NULL,
 CONSTRAINT [PK_Cps_LinkRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部链入地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'TargetURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 链接记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_LinkRecord'
GO
/****** Object:  Table [dbo].[Cps_CommissionRatio]    Script Date: 03/02/2014 20:58:35 ******/
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
	[IsDelete] [int] NULL,
	[ExtField] [nvarchar](32) NULL,
 CONSTRAINT [PK_Cps_CommissionRatio] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 平台编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品类别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'ProductCategoryID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'佣金比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'CommissionRatio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 佣金比例表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps_CommissionRatio'
GO
/****** Object:  Table [dbo].[Cps]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cps](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[UserName] [varchar](32) NULL,
	[URL] [nvarchar](80) NULL,
	[TrackingURL] [nvarchar](128) NULL,
	[Linkman] [nvarchar](20) NOT NULL,
	[Mobile] [varchar](20) NULL,
	[Tel] [varchar](20) NULL,
	[Email] [varchar](64) NULL,
	[QQ] [varchar](20) NULL,
	[Company] [nvarchar](64) NOT NULL,
	[CompanyAddress] [nvarchar](64) NULL,
	[ZipCode] [varchar](8) NULL,
	[ExtField] [nvarchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 平台名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 平台用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 平台网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 跟踪网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'TrackingURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 平台联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Linkman'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人电话号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人电子邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人 QQ 号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'QQ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'Company'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公司地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'CompanyAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮政编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'ZipCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cps 合作平台表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Cps'
GO
/****** Object:  Table [dbo].[Coupon_Scope]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon_Scope](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CouponID] [int] NOT NULL,
	[CouponTypeID] [int] NOT NULL,
	[ScopeType] [int] NOT NULL,
	[TargetTypeID] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Coupon_Scope] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Scope', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Scope', @level2type=N'COLUMN',@level2name=N'CouponID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券类型编号（0：现金券，1：满减券）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Scope', @level2type=N'COLUMN',@level2name=N'CouponTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'范围类型（0：全场，1：商品类别，2：商品品牌，3：具体商品）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Scope', @level2type=N'COLUMN',@level2name=N'ScopeType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标类型对应编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Scope', @level2type=N'COLUMN',@level2name=N'TargetTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Scope', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'优惠券使用范围表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Scope'
GO
/****** Object:  Table [dbo].[Coupon_Decrease_Binding]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Coupon_Decrease_Binding](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CouponDecreaseID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[GetOrderID] [int] NULL,
	[SetOrderID] [int] NULL,
	[Number] [varchar](32) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Cause] [nvarchar](512) NULL,
	[Status] [int] NOT NULL,
	[UseTime] [datetime] NULL,
	[BindingTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Coupon_Decrease_Binding] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'CouponDecreaseID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'获取券的订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'GetOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用券的订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'SetOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定满减券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定满减券密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠券原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'Cause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定满减券状态（0：已绑定，1：已使用，2：未激活）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定满减券使用时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'UseTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券绑定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'BindingTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券绑定表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease_Binding'
GO
/****** Object:  Table [dbo].[Coupon_Decrease]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon_Decrease](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[MeetAmount] [float] NOT NULL,
	[FaceValue] [float] NOT NULL,
	[InitialNumber] [int] NOT NULL,
	[Description] [nvarchar](128) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Coupon_Decrease] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券使用需满足金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'MeetAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券面值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'FaceValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券初始数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'InitialNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券生效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券失效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识0：正常 ，255：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：正常，1：作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'满减券表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Decrease'
GO
/****** Object:  Table [dbo].[Coupon_Cash_Binding]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Coupon_Cash_Binding](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CouponCashID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[GetOrderID] [int] NULL,
	[SetOrderID] [int] NULL,
	[Number] [varchar](32) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Cause] [nvarchar](512) NULL,
	[Status] [int] NOT NULL,
	[UseTime] [datetime] NULL,
	[BindingTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Coupon_Cash_Binding] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'CouponCashID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'获取券的订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'GetOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'使用券的订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'SetOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定现金券编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定现金券密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'赠券原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'Cause'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定现金券状态（0：已绑定，1：已使用，2：已冻结）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已绑定现金券使用时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'UseTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券绑定时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'BindingTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识0：正常 ，255：删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券绑定表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash_Binding'
GO
/****** Object:  Table [dbo].[Coupon_Cash]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coupon_Cash](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[FaceValue] [float] NOT NULL,
	[InitialNumber] [int] NOT NULL,
	[Description] [nvarchar](128) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Coupon_Cash] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券面值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'FaceValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券初始数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'InitialNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券生效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券失效时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0：正常，1：作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'现金券表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Coupon_Cash'
GO
/****** Object:  Table [dbo].[County]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[County](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CityID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[PostCode] [varchar](8) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_County] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区县所在城市编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'CityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区县名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮政编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County', @level2type=N'COLUMN',@level2name=N'PostCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区县数据表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'County'
GO
/****** Object:  Table [dbo].[Config_ToPayCounty]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_ToPayCounty](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTypeID] [int] NOT NULL,
	[CountyID] [int] NOT NULL,
	[isDelete] [tinyint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ExtField] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'到付支持地区（odr_PayWayCounty）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_ToPayCounty'
GO
/****** Object:  Table [dbo].[Config_Payment_Type]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Payment_Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentMethodID] [int] NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Config_Payment_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付方式编号（0：在线支付，1：货到付款）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'PaymentMethodID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类型配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Type'
GO
/****** Object:  Table [dbo].[Config_Payment_Organization]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Payment_Organization](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTypeID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[URL] [varchar](50) NULL,
	[ImageURL] [varchar](50) NULL,
	[Number] [varchar](20) NULL,
	[Sorting] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Config_Payment_Organization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization', @level2type=N'COLUMN',@level2name=N'PaymentTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付机构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付机构网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付机构图片网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization', @level2type=N'COLUMN',@level2name=N'ImageURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付机构排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支付机构配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Payment_Organization'
GO
/****** Object:  Table [dbo].[Config_Page]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Page](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Content] [text] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[Source] [varchar](1000) NULL,
 CONSTRAINT [PK_Config_News] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Page', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Page', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Page', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Page', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容正文' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Page', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Page', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Page', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
/****** Object:  Table [dbo].[Config_Invoice_Type]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Invoice_Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Description] [ntext] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Config_Invoice_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票类型备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票类型配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Type'
GO
/****** Object:  Table [dbo].[Config_Invoice_Content]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Invoice_Content](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Description] [ntext] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Config_Invoice_Content] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票内容名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票内容描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票内容配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Invoice_Content'
GO
/****** Object:  Table [dbo].[Config_Delivery_Method]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Delivery_Method](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Description] [ntext] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Config_Delivery_Method] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送方式名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送方式备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送方式配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Method'
GO
/****** Object:  Table [dbo].[Config_Delivery_Cost]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config_Delivery_Cost](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryCorporationID] [int] NOT NULL,
	[CityID] [int] NOT NULL,
	[Duration] [int] NOT NULL,
	[Cost] [float] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Config_Delivery_Cost] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'DeliveryCorporationID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送城市编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'CityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送时长' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'Cost'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送金额配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Cost'
GO
/****** Object:  Table [dbo].[Config_Delivery_Corporation]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Config_Delivery_Corporation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Tel] [varchar](64) NULL,
	[URL] [varchar](64) NULL,
	[Number] [varchar](20) NULL,
	[Description] [ntext] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[DeliveryWay] [nvarchar](50) NULL,
	[ExtField] [nvarchar](200) NULL,
 CONSTRAINT [PK_Config_Delivery_Corporation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司代号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'Number'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'DeliveryWay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation', @level2type=N'COLUMN',@level2name=N'ExtField'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配送公司配置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Config_Delivery_Corporation'
GO
/****** Object:  Table [dbo].[Code]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Code](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](50) NULL,
	[Business] [nvarchar](20) NULL,
	[PrefixName] [nvarchar](max) NULL,
	[DateFormat] [nvarchar](50) NULL,
	[TransactLength] [int] NOT NULL,
	[Transaction] [nvarchar](max) NULL,
	[CodeFormat] [nvarchar](50) NULL,
	[IsIterator] [bit] NOT NULL,
	[Iterator] [int] NULL,
	[StartTime] [datetime] NULL,
	[UserIterator] [int] NULL,
	[ExpireDate] [int] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_dbo.Code] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在省会编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'ProvinceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'城市数据表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'City'
GO
/****** Object:  Table [dbo].[Channel_GroupBuy]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Channel_GroupBuy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[UserLevelID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[ImageUrl] [nvarchar](128) NOT NULL,
	[GBPrice] [float] NOT NULL,
	[TotalNumber] [int] NOT NULL,
	[Introduce] [ntext] NULL,
	[ShowLevel] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[IsShowTime] [bit] NULL,
	[IsOnlinePayment] [bit] NULL,
	[SoldOfReality] [int] NOT NULL,
	[SoldOfVirtual] [int] NOT NULL,
	[PageView] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Channel_GroupBuy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户等级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'UserLevelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'ImageUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'GBPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动商品总数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'TotalNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动介绍' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'Introduce'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动显示级别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'ShowLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否显示时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'IsShowTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否仅限在线支付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'IsOnlinePayment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实销售数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'SoldOfReality'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'虚拟销售数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'SoldOfVirtual'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'活动页面浏览数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'PageView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购活动状态（1：未开始，2：进行中，3：已结束）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'团购频道表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Channel_GroupBuy'
GO
/****** Object:  UserDefinedTableType [dbo].[BrandInformation]    Script Date: 03/02/2014 20:58:36 ******/
CREATE TYPE [dbo].[BrandInformation] AS TABLE(
	[BrandID] [int] NOT NULL,
	[Title] [nvarchar](1000) NULL,
	[Introduce] [text] NULL,
	[Logo] [varchar](1000) NULL,
	[ProductID] [varchar](1000) NULL,
	[CreateTime] [datetime] NULL
)
GO
/****** Object:  Table [dbo].[Brand_Information]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Brand_Information](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BrandID] [int] NULL,
	[Title] [text] NULL,
	[Introduce] [text] NULL,
	[Logo] [varchar](1000) NULL,
	[ProductID] [varchar](1000) NULL,
	[IsDelete] [int] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK__Brand_In__3214EC272E11BAA1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Aftersale_Return_Product]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Return_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Aftersale_Return_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Return_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后服务退货商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Return_Product'
GO
/****** Object:  Table [dbo].[Aftersale_Return_Log]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Return_Log](
	[ID] [int] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Aftersale_Operate_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Return_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后服务退货操作日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Return_Log'
GO
/****** Object:  Table [dbo].[Aftersale_Return]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Return](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Aftersale_Return] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Return', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后服务退货表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Return'
GO
/****** Object:  Table [dbo].[Aftersale_Refund_Log]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Refund_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Aftersale_Refund_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后服务退款操作日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund_Log'
GO
/****** Object:  Table [dbo].[Aftersale_Refund]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Refund](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RefundSourceID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[RefundMethodID] [int] NOT NULL,
	[ActualRefundMoney] [float] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Description] [nvarchar](512) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Order_Refund] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退款来源编号（1：取消订单，2：退货，3：换货）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'RefundSourceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'OrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退款方式编号（1：退至虚拟账户，2：人工退款至指定帐号）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'RefundMethodID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际退款金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'ActualRefundMoney'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退款状态（1：审核中，2：退款中，3：已退款）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单退款表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Refund'
GO
/****** Object:  Table [dbo].[Aftersale_Exchange_Product]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Exchange_Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Aftersale_Exchange_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Exchange_Product', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后服务换货商品表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Exchange_Product'
GO
/****** Object:  Table [dbo].[Aftersale_Exchange_Log]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Exchange_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Aftersale_Exchange_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Exchange_Log', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后服务换货操作日志表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Exchange_Log'
GO
/****** Object:  Table [dbo].[Aftersale_Exchange]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aftersale_Exchange](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_Aftersale_Exchange] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Exchange', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售后服务换货表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Aftersale_Exchange'
GO
/****** Object:  UserDefinedTableType [dbo].[AdvertiseType]    Script Date: 03/02/2014 20:58:36 ******/
CREATE TYPE [dbo].[AdvertiseType] AS TABLE(
	[PID] [int] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Source] [varchar](50) NULL,
	[URL] [varchar](500) NULL
)
GO
/****** Object:  Table [dbo].[Advertise_Config]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertise_Config](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](500) NULL,
	[Source] [varchar](50) NULL,
	[URL] [varchar](500) NULL,
	[ImageID] [int] NULL,
	[ImagePath] [varchar](500) NULL,
	[ThumbnailImagePath] [varchar](500) NULL,
	[Enabled] [int] NULL,
	[Description] [varchar](1000) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[IndexID] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[BackgroundColor] [varchar](50) NULL,
	[IsOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源(1:产品 2:LP 3:其他)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'Source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'ImageID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'ImagePath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'缩略图地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'ThumbnailImagePath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片宽度（单位:PX）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'Width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片高度（单位:PX）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'索引ID（用于存储引用的产品、LP的ID）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'IndexID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'背景颜色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config', @level2type=N'COLUMN',@level2name=N'BackgroundColor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Advertise_Config'
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_Select]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Advertise_Config_Select]
 @count int,
 @search nvarchar(1000)
As
Begin
	Declare @Sql nvarchar(2000);
	
	IF @count IS NOT NULL OR @count > 0
	Begin
		Set @Sql = N'select top ' + cast(@count as nvarchar(100));
	End	
	
	Set @Sql = @Sql + N' id,pid,name,source,url,imagepath,description,enabled,createtime,indexid,width,height,ThumbnailImagePath,imageid,backgroundcolor from advertise_config where isdelete=0 ';
	
	IF @search IS NOT NULL OR @search <> ''
	Begin
		Set @Sql = @Sql + N' and ' + @Search;	
	End
	
	exec sp_executesql @Sql;
End
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInfomation_UpdateLogo]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_BrandInfomation_UpdateLogo]
@brandId int,
@Logo varchar(1000)
as 
begin
update Brand_Infomation set Logo=@Logo where BrandID=@brandId
end
GO
/****** Object:  UserDefinedFunction [dbo].[GetFileName]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetFileName] (@filePath as nvarchar(1000))
Returns Varchar(200)
AS
BEGIN
	Declare @fileName Varchar(200)
	If @filePath Is Null Or @filePath = '' 
	Begin
		Set @fileName = ''
	End
	Else
	Begin
		SET @filePath = REVERSE(@filePath)
		SET @fileName = LEFT(@filePath,CHARINDEX('/',@filePath,0)-1)	
		SET @fileName = REVERSE(@fileName)
	End
	Return(@fileName)
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetQuanPin]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[fn_GetQuanPin](@str varchar(100))
returns varchar(8000)
as
begin
 declare @re varchar(8000),@crs varchar(10)
 declare @strlen int 
 select @strlen=len(@str),@re=''
 while @strlen>0
 begin  
  set @crs= substring(@str,@strlen,1)
      select @re=
        CASE 
        when @crs<'吖' COLLATE Chinese_PRC_CS_AS_KS_WS then @crs
        when @crs<='厑' COLLATE Chinese_PRC_CS_AS_KS_WS then 'A'
        when @crs<='靉' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ai'
        when @crs<='黯' COLLATE Chinese_PRC_CS_AS_KS_WS then 'An'
        when @crs<='醠' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ang'
        when @crs<='驁' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ao'
        when @crs<='欛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ba'
        when @crs<='瓸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bai'
        when @crs<='瓣' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ban'
        when @crs<='鎊' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bang'
        when @crs<='鑤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bao'
        when @crs<='鐾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bei'
        when @crs<='輽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ben'
        when @crs<='鏰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Beng'
        when @crs<='鼊' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bi'
        when @crs<='變' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bian'
        when @crs<='鰾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Biao'
        when @crs<='彆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bie'
        when @crs<='鬢' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bin'
        when @crs<='靐' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bing'
        when @crs<='蔔' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bo'
        when @crs<='簿' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Bu'
        when @crs<='囃' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ca'
        when @crs<='乲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cai'
        when @crs<='爘' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Can'
        when @crs<='賶' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cang'
        when @crs<='鼜' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cao'
        when @crs<='簎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ce'
        when @crs<='笒' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cen'
        when @crs<='乽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ceng'
        when @crs<='詫' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cha'
        when @crs<='囆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chai'
        when @crs<='顫' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chan'
        when @crs<='韔' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chang'
        when @crs<='觘' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chao'
        when @crs<='爡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Che'
        when @crs<='讖' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chen'
        when @crs<='秤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cheng'
        when @crs<='鷘' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chi'
        when @crs<='銃' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chong'
        when @crs<='殠' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chou'
        when @crs<='矗' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chu'
        when @crs<='踹' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chuai'
        when @crs<='鶨' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chuan'
        when @crs<='愴' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chuang'
        when @crs<='顀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chui'
        when @crs<='蠢' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chun'
        when @crs<='縒' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Chuo'
        when @crs<='嗭' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ci'
        when @crs<='謥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cong'
        when @crs<='輳' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cou'
        when @crs<='顣' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cu'
        when @crs<='爨' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cuan'
        when @crs<='臎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cui'
        when @crs<='籿' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cun'
        when @crs<='錯' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Cuo'
        when @crs<='橽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Da'
        when @crs<='靆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dai'
        when @crs<='饏' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dan'
        when @crs<='闣' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dang'
        when @crs<='纛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dao'
        when @crs<='的' COLLATE Chinese_PRC_CS_AS_KS_WS then 'De'
        when @crs<='扽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Den'
        when @crs<='鐙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Deng'
        when @crs<='螮' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Di'
        when @crs<='嗲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dia'
        when @crs<='驔' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dian'
        when @crs<='鑃' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Diao'
        when @crs<='嚸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Die'
        when @crs<='顁' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ding'
        when @crs<='銩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Diu'
        when @crs<='霘' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dong'
        when @crs<='鬭' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dou'
        when @crs<='蠹' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Du'
        when @crs<='叾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Duan'
        when @crs<='譵' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dui'
        when @crs<='踲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Dun'
        when @crs<='鵽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Duo'
        when @crs<='鱷' COLLATE Chinese_PRC_CS_AS_KS_WS then 'E'
        when @crs<='摁' COLLATE Chinese_PRC_CS_AS_KS_WS then 'En'
        when @crs<='鞥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Eng'
        when @crs<='樲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Er'
        when @crs<='髮' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fa'
        when @crs<='瀪' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fan'
        when @crs<='放' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fang'
        when @crs<='靅' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fei'
        when @crs<='鱝' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fen'
        when @crs<='覅' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Feng'
        when @crs<='梻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fo'
        when @crs<='鴀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fou'
        when @crs<='猤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Fu'
        when @crs<='魀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ga'
        when @crs<='瓂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gai'
        when @crs<='灨' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gan'
        when @crs<='戇' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gang'
        when @crs<='鋯' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gao'
        when @crs<='獦' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ge'
        when @crs<='給' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gei'
        when @crs<='搄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gen'
        when @crs<='堩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Geng'
        when @crs<='兣' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gong'
        when @crs<='購' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gou'
        when @crs<='顧' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gu'
        when @crs<='詿' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gua'
        when @crs<='恠' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Guai'
        when @crs<='鱹' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Guan'
        when @crs<='撗' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Guang'
        when @crs<='鱥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gui'
        when @crs<='謴' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Gun'
        when @crs<='腂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Guo'
        when @crs<='哈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ha'
        when @crs<='饚' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hai'
        when @crs<='鶾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Han'
        when @crs<='沆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hang'
        when @crs<='兞' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hao'
        when @crs<='靏' COLLATE Chinese_PRC_CS_AS_KS_WS then 'He'
        when @crs<='嬒' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hei'
        when @crs<='恨' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hen'
        when @crs<='堼' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Heng'
        when @crs<='鬨' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hong'
        when @crs<='鱟' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hou'
        when @crs<='鸌' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hu'
        when @crs<='蘳' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hua'
        when @crs<='蘾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Huai'
        when @crs<='鰀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Huan'
        when @crs<='鎤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Huang'
        when @crs<='顪' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hui'
        when @crs<='諢' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Hun'
        when @crs<='夻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Huo'
        when @crs<='驥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ji'
        when @crs<='嗧' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jia'
        when @crs<='鑳' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jian'
        when @crs<='謽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jiang'
        when @crs<='釂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jiao'
        when @crs<='繲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jie'
        when @crs<='齽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jin'
        when @crs<='竸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jing'
        when @crs<='蘔' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jiong'
        when @crs<='欍' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jiu'
        when @crs<='爠' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ju'
        when @crs<='羂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Juan'
        when @crs<='钁' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jue'
        when @crs<='攈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Jun'
        when @crs<='鉲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ka'
        when @crs<='乫' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kai'
        when @crs<='矙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kan'
        when @crs<='閌' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kang'
        when @crs<='鯌' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kao'
        when @crs<='騍' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ke'
        when @crs<='褃' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ken'
        when @crs<='鏗' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Keng'
        when @crs<='廤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kong'
        when @crs<='鷇' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kou'
        when @crs<='嚳' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ku'
        when @crs<='骻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kua'
        when @crs<='鱠' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kuai'
        when @crs<='窾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kuan'
        when @crs<='鑛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kuang'
        when @crs<='鑎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kui'
        when @crs<='睏' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kun'
        when @crs<='穒' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Kuo'
        when @crs<='鞡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'La'
        when @crs<='籟' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lai'
        when @crs<='糷' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lan'
        when @crs<='唥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lang'
        when @crs<='軂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lao'
        when @crs<='餎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Le'
        when @crs<='脷' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lei'
        when @crs<='睖' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Leng'
        when @crs<='瓈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Li'
        when @crs<='倆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lia'
        when @crs<='纞' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lian'
        when @crs<='鍄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Liang'
        when @crs<='瞭' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Liao'
        when @crs<='鱲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lie'
        when @crs<='轥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lin'
        when @crs<='炩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ling'
        when @crs<='咯' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Liu'
        when @crs<='贚' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Long'
        when @crs<='鏤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lou'
        when @crs<='氇' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lu'
        when @crs<='鑢' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lv'
        when @crs<='亂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Luan'
        when @crs<='擽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lue'
        when @crs<='論' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Lun'
        when @crs<='鱳' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Luo'
        when @crs<='嘛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ma'
        when @crs<='霢' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mai'
        when @crs<='蘰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Man'
        when @crs<='蠎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mang'
        when @crs<='唜' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mao'
        when @crs<='癦' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Me'
        when @crs<='嚜' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mei'
        when @crs<='們' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Men'
        when @crs<='霥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Meng'
        when @crs<='羃' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mi'
        when @crs<='麵' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mian'
        when @crs<='廟' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Miao'
        when @crs<='鱴' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mie'
        when @crs<='鰵' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Min'
        when @crs<='詺' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ming'
        when @crs<='謬' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Miu'
        when @crs<='耱' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mo'
        when @crs<='麰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mou'
        when @crs<='旀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Mu'
        when @crs<='魶' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Na'
        when @crs<='錼' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nai'
        when @crs<='婻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nan'
        when @crs<='齉' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nang'
        when @crs<='臑' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nao'
        when @crs<='呢' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ne'
        when @crs<='焾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nei'
        when @crs<='嫩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nen'
        when @crs<='能' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Neng'
        when @crs<='嬺' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ni'
        when @crs<='艌' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nian'
        when @crs<='釀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Niang'
        when @crs<='脲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Niao'
        when @crs<='钀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nie'
        when @crs<='拰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nin'
        when @crs<='濘' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ning'
        when @crs<='靵' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Niu'
        when @crs<='齈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nong'
        when @crs<='譳' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nou'
        when @crs<='搙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nu'
        when @crs<='衄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nv'
        when @crs<='瘧' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nue'
        when @crs<='燶' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nuan'
        when @crs<='桛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Nuo'
        when @crs<='鞰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'O'
        when @crs<='漚' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ou'
        when @crs<='袙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pa'
        when @crs<='磗' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pai'
        when @crs<='鑻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pan'
        when @crs<='胖' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pang'
        when @crs<='礮' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pao'
        when @crs<='轡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pei'
        when @crs<='喯' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pen'
        when @crs<='喸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Peng'
        when @crs<='鸊' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pi'
        when @crs<='騙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pian'
        when @crs<='慓' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Piao'
        when @crs<='嫳' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pie'
        when @crs<='聘' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pin'
        when @crs<='蘋' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ping'
        when @crs<='魄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Po'
        when @crs<='哛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pou'
        when @crs<='曝' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Pu'
        when @crs<='蟿' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qi'
        when @crs<='髂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qia'
        when @crs<='縴' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qian'
        when @crs<='瓩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qiang'
        when @crs<='躈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qiao'
        when @crs<='籡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qie'
        when @crs<='藽' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qin'
        when @crs<='櫦' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qing'
        when @crs<='瓗' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qiong'
        when @crs<='糗' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qiu'
        when @crs<='覻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qu'
        when @crs<='勸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Quan'
        when @crs<='礭' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Que'
        when @crs<='囕' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Qun'
        when @crs<='橪' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ran'
        when @crs<='讓' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Rang'
        when @crs<='繞' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Rao'
        when @crs<='熱' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Re'
        when @crs<='餁' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ren'
        when @crs<='陾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Reng'
        when @crs<='馹' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ri'
        when @crs<='穃' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Rong'
        when @crs<='嶿' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Rou'
        when @crs<='擩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ru'
        when @crs<='礝' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ruan'
        when @crs<='壡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Rui'
        when @crs<='橍' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Run'
        when @crs<='鶸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ruo'
        when @crs<='栍' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sa'
        when @crs<='虄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sai'
        when @crs<='閐' COLLATE Chinese_PRC_CS_AS_KS_WS then 'San'
        when @crs<='喪' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sang'
        when @crs<='髞' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sao'
        when @crs<='飋' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Se'
        when @crs<='篸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sen'
        when @crs<='縇' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Seng'
        when @crs<='霎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sha'
        when @crs<='曬' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shai'
        when @crs<='鱔' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shan'
        when @crs<='緔' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shang'
        when @crs<='潲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shao'
        when @crs<='欇' COLLATE Chinese_PRC_CS_AS_KS_WS then 'She'
        when @crs<='瘮' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shen'
        when @crs<='賸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sheng'
        when @crs<='瓧' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shi'
        when @crs<='鏉' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shou'
        when @crs<='虪' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shu'
        when @crs<='誜' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shua'
        when @crs<='卛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shuai'
        when @crs<='腨' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shuan'
        when @crs<='灀' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shuang'
        when @crs<='睡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shui'
        when @crs<='鬊' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shun'
        when @crs<='鑠' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Shuo'
        when @crs<='乺' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Si'
        when @crs<='鎹' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Song'
        when @crs<='瘶' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sou'
        when @crs<='鷫' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Su'
        when @crs<='算' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Suan'
        when @crs<='鐩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sui'
        when @crs<='潠' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Sun'
        when @crs<='蜶' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Suo'
        when @crs<='襨' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ta'
        when @crs<='燤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tai'
        when @crs<='賧' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tan'
        when @crs<='燙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tang'
        when @crs<='畓' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tao'
        when @crs<='蟘' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Te'
        when @crs<='朰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Teng'
        when @crs<='趯' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ti'
        when @crs<='舚' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tian'
        when @crs<='糶' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tiao'
        when @crs<='餮' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tie'
        when @crs<='乭' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ting'
        when @crs<='憅' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tong'
        when @crs<='透' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tou'
        when @crs<='鵵' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tu'
        when @crs<='褖' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tuan'
        when @crs<='駾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tui'
        when @crs<='坉' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tun'
        when @crs<='籜' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Tuo'
        when @crs<='韤' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wa'
        when @crs<='顡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wai'
        when @crs<='贎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wan'
        when @crs<='朢' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wang'
        when @crs<='躛' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wei'
        when @crs<='璺' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wen'
        when @crs<='齆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Weng'
        when @crs<='齷' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wo'
        when @crs<='鶩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Wu'
        when @crs<='衋' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xi'
        when @crs<='鏬' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xia'
        when @crs<='鼸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xian'
        when @crs<='鱌' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xiang'
        when @crs<='斆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xiao'
        when @crs<='躞' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xie'
        when @crs<='釁' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xin'
        when @crs<='臖' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xing'
        when @crs<='敻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xiong'
        when @crs<='齅' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xiu'
        when @crs<='蓿' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xu'
        when @crs<='贙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xuan'
        when @crs<='瀥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xue'
        when @crs<='鑂' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Xun'
        when @crs<='齾' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ya'
        when @crs<='灩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yan'
        when @crs<='樣' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yang'
        when @crs<='鑰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yao'
        when @crs<='岃' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ye'
        when @crs<='齸' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yi'
        when @crs<='檼' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yin'
        when @crs<='譍' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ying'
        when @crs<='喲' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yo'
        when @crs<='醟' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yong'
        when @crs<='鼬' COLLATE Chinese_PRC_CS_AS_KS_WS then 'You'
        when @crs<='爩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yu'
        when @crs<='願' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yuan'
        when @crs<='鸙' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yue'
        when @crs<='韻' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Yun'
        when @crs<='雥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Za'
        when @crs<='縡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zai'
        when @crs<='饡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zan'
        when @crs<='臟' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zang'
        when @crs<='竈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zao'
        when @crs<='稄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Ze'
        when @crs<='鱡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zei'
        when @crs<='囎' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zen' 
        when @crs<='贈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zeng'
        when @crs<='醡' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zha'
        when @crs<='瘵' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhai'
        when @crs<='驏' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhan'
        when @crs<='瞕' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhang'
        when @crs<='羄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhao'
        when @crs<='鷓' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhe'
        when @crs<='黮' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhen'
        when @crs<='證' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zheng'
        when @crs<='豒' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhi'
        when @crs<='諥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhong'
        when @crs<='驟' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhou'
        when @crs<='鑄' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhu'
        when @crs<='爪' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhua'
        when @crs<='跩' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhuai'
        when @crs<='籑' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhuan'
        when @crs<='戅' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhuang'
        when @crs<='鑆' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhui'
        when @crs<='稕' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhun'
        when @crs<='籱' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zhuo'
        when @crs<='漬' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zi'
        when @crs<='縱' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zong'
        when @crs<='媰' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zou'
        when @crs<='謯' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zu'
        when @crs<='攥' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zuan'
        when @crs<='欈' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zui'
        when @crs<='銌' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zun'
        when @crs<='咗' COLLATE Chinese_PRC_CS_AS_KS_WS then 'Zuo'
        else  @crs end+@re,@strlen=@strlen-1 
   end
 return(@re)
END
GO
/****** Object:  Table [dbo].[FeedBack]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedBack](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Content] [text] NULL,
	[ImgUrl] [nvarchar](500) NULL,
	[Name] [nvarchar](128) NULL,
	[Gender] [bit] NOT NULL,
	[GjwNumber] [nvarchar](128) NULL,
	[Email] [nvarchar](256) NULL,
	[TelPhone] [nvarchar](128) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_FeedBack] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_Paging_old]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Paging_old]      
(      
 @tableName varchar(100),     -- 表名称      
 @pageIndex int,     -- 页码      
 @pageSize int,     -- 每页大小      
 @pageColumn varchar(1024),  -- 分页的列      
 @columns varchar(1024) = null,       -- 查询的列，已逗号分隔，为空则查询所有列      
 @orderBy varchar(1024) = null,       -- 排序的列，DESC / ASC      
 @condition nvarchar(1024) = null,    -- 查询条件      
 @totalCount int output       -- 总记录数      
)      
AS      
 -- 判断对象是否存在      
 IF OBJECT_ID(@tableName) IS NULL      
 BEGIN   RAISERROR(N'对象不存在', 1, 16, @tableName)   RETURN  END      
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
       
 DECLARE @totalCountCommandText nvarchar(2048)      
 DECLARE @commandText nvarchar(2048)       
       
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
   --SET @condition = REPLACE(@condition, 'WHERE', 'AND')     
     
   SET @commandText += N') AS T) ' + @condition + @orderBy      
   EXEC(@commandText)      
  END
GO
/****** Object:  StoredProcedure [dbo].[sp_Paging]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Paging]  
(  
 @tableName NVARCHAR(128),    -- 表名  
 @columnName NVARCHAR(512) = N'*', -- 查询的列名  
 @primaryKey NVARCHAR(32) = N'ID', -- 主键列名  
 @condition NVARCHAR(512) = NULL,  -- 查询条件（不需要 WHERE 关键字）  
 @pageIndex INT = 1,      -- 页码  
 @pageSize INT = 10,      -- 每页记录数量  
 @orderColumn NVARCHAR(64) = NULL, -- 排序列名  
 @orderType INT = 1,      -- 排序类型（0：升序，1：降序）  
 @distinct BIT = 1,      -- 是否添加 DISTINCT 排除重复记录，当主键列自增时，无须添加（0：添加，1：不添加）  
 @pageCount INT OUTPUT,     -- 总页数  
 @totalCount INT OUTPUT     -- 总记录数  
)  
AS 
 -- 设置：不返回 Transact-SQL 语句影响的行数  
 -- 原因：如果存储过程中包含的一些语句并不返回许多实际的数据，则该设置由于大量减少了网络流量，因此可显著提高性能  
 SET NOCOUNT ON  
   
 -- 检查对象名称是否为空
 IF @tableName IS NULL OR @tableName = N''  
 BEGIN  
  RAISERROR(N'指定查询的对象不能为空', 1, 16, @tableName)  
  RETURN  
 END  
 -- 检查对象是否存在  
 IF OBJECT_ID(@tableName) IS NULL  
 BEGIN  
  RAISERROR(N'指定查询的对象（表、视图、表值函数）不存在', 1, 16, @tableName)  
  RETURN  
 END  
 -- 检查对象类型是否正确  
 IF OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTable') = 0  
 AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsView') = 0  
 AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTableFunction') = 0  
 BEGIN  
  RAISERROR(N'指定对象的类型必须为：表、视图或表值函数', 1, 16, @tableName)  
  RETURN  
 END  
  
 -- 检查列名
 IF @columnName IS NULL OR @columnName = N''  
 BEGIN
  Set @columnName = '*'
 END
 
 -- 定义变量  
 DECLARE @cmdText NVARCHAR(1024)  
 DECLARE @cmdTextForTotalCount NVARCHAR(1024)  
 DECLARE @partOfSelect NVARCHAR(512)  
 DECLARE @partOfCount NVARCHAR(512)  
 DECLARE @partOfWhere NVARCHAR(512)  
 DECLARE @partOfOrderBy NVARCHAR(512)  
 DECLARE @partOfFrom NVARCHAR(512)  
 
 IF OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTable') != 0 
 BEGIN
   IF EXISTS(SELECT * FROM syscolumns WHERE id=object_id(@tableName) AND name='IsDelete')
   BEGIN  
	  IF @condition IS NOT NULL OR @condition <> ''  
	   BEGIN  
	    SET @condition = @condition + N' And IsDelete = 0'  
	   END  
	   ELSE  
	   BEGIN  	    
	    SET @condition = N' IsDelete = 0'  
	   END
   END
 END
 
 -- 获取 SELECT 和 COUNT 部分命令  
 IF @primaryKey <> N'ID'  
 BEGIN  
  -- 判断主键列是否为自增列以及是否添加 DISTINCT 排除重复记录  
  IF COLUMNPROPERTY(OBJECT_ID(@tableName), @primaryKey, N'IsIdentity') <> 1 AND @distinct = 0  
  BEGIN  
   SET @partOfSelect = N'SELECT DISTINCT '  
   SET @partOfCount = N'COUNT(DISTINCT' + @primaryKey + N') '  
  END  
  ELSE  
  BEGIN  
   SET @partOfSelect = N'SELECT '  
   -- 带查询条件时，Count(1) 为首选，总耗时略低于 Count(*)，但 CPU 耗时远低于 Count(*)  
   IF @condition IS NOT NULL OR @condition <> ''  
   BEGIN  
    SET @partOfCount = N'COUNT(1) '  
   END  
   ELSE  
   BEGIN  
    SET @partOfCount = N'COUNT(*) '  
   END  
  END  
 END  
 ELSE  
 BEGIN  
  SET @partOfSelect = N'SELECT '  
  -- 带查询条件时，Count(1) 为首选，总耗时略低于 Count(*)，但 CPU 耗时远低于 Count(*)  
  IF @condition IS NOT NULL OR @condition <> ''  
  BEGIN  
   SET @partOfCount = N'COUNT(1) '  
  END  
  ELSE  
  BEGIN  
   SET @partOfCount = 'COUNT(*) '  
  END  
 END  
 -- 获取 FROM 部分命令  
 SET @partOfFrom = N'FROM ' + @tableName + N' '  
 -- 获取 WHERE 部分命令  
 SET @partOfWhere = N'WHERE (' + @condition + ') '  
 -- 获取 ORDER BY 部分命令  
 IF @orderColumn IS NOT NULL AND @orderColumn <> N''  
 BEGIN  
  IF @orderType = 1  
  BEGIN  
   SET @partOfOrderBy = N'ORDER BY ' + @orderColumn + N' DESC '  
  END  
  ELSE IF @orderType = 0  
  BEGIN  
   SET @partOfOrderBy = N'ORDER BY ' + @orderColumn + N' ASC '  
  END  
 END  
 ELSE  
 BEGIN  
  SET @partOfOrderBy = N''  
 END  
 -- 获取查询总记录数命令  
 IF @condition IS NULL OR @condition = N''  
 BEGIN  
  SET @cmdTextForTotalCount = @partOfSelect + N'@totalCount = ' + @partOfCount + @partOfFrom  
 END  
 ELSE  
 BEGIN  
  SET @cmdTextForTotalCount = @partOfSelect + N'@totalCount = ' + @partOfCount + @partOfFrom + @partOfWhere  
 END  
  
 -- 查询总记录数  
 EXEC sp_executesql @cmdTextForTotalCount, N'@totalCount INT OUTPUT', @totalCount OUTPUT  
 -- 计算总页数  
 DECLARE @tempTotalCount INT  
 IF @totalCount = 0  
 BEGIN  
  SET @tempTotalCount = 1    
 END  
 ELSE  
 BEGIN  
  SET @tempTotalCount = @totalCount  
 END  
 SET @pageCount = (@tempTotalCount + @pageSize - 1) / @pageSize  
 -- 查询数据  
 -- 查询第一页数据  
 IF @pageIndex = 1  
 BEGIN  
  -- 查询条件、排序列均为空  
  IF (@condition IS NULL OR @condition = N'') AND (@orderColumn IS NULL OR @orderColumn = '')  
  BEGIN  
   SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @columnName + N' ' + @partOfFrom  
  END  
  ELSE  
  BEGIN  
   -- 查询条件不为空，排序列为空  
   IF (@condition IS NOT NULL AND @condition <> N'') AND (@orderColumn IS NULL OR @orderColumn = N'')  
   BEGIN  
    SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @columnName + N' ' + @partOfFrom + @partOfWhere
   END
   -- 排序列不为空，查询条件为空  
   ELSE IF (@orderColumn IS NOT NULL AND @orderColumn <> N'') AND (@condition IS NULL OR @condition = N'')  
   BEGIN  
    SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @columnName + N' ' + @partOfFrom + @partOfOrderBy  
   END  
   -- 查询条件、排序列都不为空  
   ELSE  
   BEGIN  
    SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @columnName + N' ' + @partOfFrom + @partOfWhere + @partOfOrderBy  
   END  
  END  
 END  
 -- 查询非第一页数据  
 ELSE  
 BEGIN  
  -- 查询条件、排序列均为空  
  IF (@condition IS NULL OR @condition = N'') AND (@orderColumn IS NULL OR @orderColumn = '')  
  BEGIN  
   SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @columnName + N' ' + @partOfFrom + N'WHERE ' + @primaryKey  
   SET @cmdText += N' > (SELECT MAX(' + @primaryKey + N') FROM '  
   SET @cmdText += N'(' + @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @primaryKey + N' ' + @partOfFrom + N'ORDER BY ' + @primaryKey + N') AS T) '  
  END  
  ELSE  
  BEGIN  
   -- 查询条件不为空，排序列为空  
   IF (@condition IS NOT NULL AND @condition <> N'') AND (@orderColumn IS NULL OR @orderColumn = N'')  
   BEGIN  
    SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @columnName + N' ' + @partOfFrom + N'WHERE ' + @primaryKey  
    SET @cmdText += N' > (SELECT MAX(' + @primaryKey + N') FROM '  
    SET @cmdText += N'(' + @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' ' + @primaryKey + N' ' + @partOfFrom + @partOfWhere + N'ORDER BY ' + @primaryKey + N') AS T) '  
    SET @cmdText += N'AND ' + @condition  
   END  
   -- 排序列不为空，查询条件为空  
   ELSE IF (@orderColumn IS NOT NULL AND @orderColumn <> N'') AND (@condition IS NULL OR @condition = N'')  
   BEGIN  
    SET @cmdText = @partOfSelect + @columnName + N' FROM '  
    SET @cmdText += N'(SELECT ROW_NUMBER() OVER(' + @partOfOrderBy + N') AS rowNumber, ' + @columnName + N' ' + @partOfFrom + N') AS T '  
    SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex))  
   END  
   -- 查询条件、排序列都不为空  
   ELSE  
   BEGIN  
    SET @cmdText = @partOfSelect + @columnName + N' FROM '  
    SET @cmdText += N'(SELECT ROW_NUMBER() OVER(' + @partOfOrderBy + N') AS rowNumber, ' + @columnName + N' ' + @partOfFrom + @partOfWhere + N') AS T ' 
    -- modify by caiyp
    --SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex)) + N' '      
    --SET @partOfWhere = REPLACE(@partOfWhere, N'WHERE', N'AND')  
    --SET @cmdText += @partOfWhere  
    If @partOfWhere Is Null Or @partOfWhere = N'' 
    Begin
    	SET @cmdText += ' Where 1=1 '
    End
    Else
    Begin
    	SET @cmdText += @partOfWhere
    End
    SET @cmdText += N'And rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex)) + N' '    
   END  
  END  
 END
 EXEC sp_executesql @cmdText  
 SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_Select_User_Message]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE Procedure [dbo].[sp_Select_User_Message]
(
	@tableName NVARCHAR(128),		  -- 表名
	@type int,
	@condition NVARCHAR(1024) = NULL -- 查询条件（不需要 WHERE 关键字）
)
AS
	-- 设置：不返回 Transact-SQL 语句影响的行数
	-- 原因：如果存储过程中包含的一些语句并不返回许多实际的数据，则该设置由于大量减少了网络流量，因此可显著提高性能
	SET NOCOUNT ON
		
	-- 定义变量
	DECLARE @cmdText NVARCHAR(1024)
	DECLARE @partOfWhere NVARCHAR(1024)
	
	-- 获取 WHERE 部分命令
	SET @partOfWhere = N'WHERE (' + @condition + ') '
	
	-- 查询条件不为空，排序列为空
	IF (@condition IS NOT NULL and @condition <> N'')
	BEGIN
		if (@type = 1)
		begin
			SET @cmdText = N'SELECT '+@tableName+'.Email FROM ' + @tableName + @partOfWhere
		end
		else if (@type = 2)
		begin
			SET @cmdText = N'SELECT '+@tableName+'.Mobile FROM ' + @tableName + @partOfWhere
		end		
	END
	else
	begin
		if (@type = 1)
		begin
			SET @cmdText = N'SELECT '+@tableName+'.Email FROM ' + @tableName 
		end
		else if (@type = 2)
		begin
			SET @cmdText = N'SELECT '+@tableName+'.Mobile FROM ' + @tableName 
		end	
	end	
	EXEC sp_executesql @cmdText
	SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Paging]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Paging]
(
	@tableName NVARCHAR(128),		  -- 表名
	@condition NVARCHAR(1024) = NULL, -- 查询条件（不需要 WHERE 关键字）
	@pageIndex INT = 1,				  -- 页码
	@pageSize INT = 10,				  -- 每页记录数量
	@orderColumn NVARCHAR(64) = NULL, -- 排序列名
	@orderType INT = 1,				  -- 排序类型（0：升序，1：降序）
	@pageCount INT OUTPUT,			  -- 总页数
	@totalCount INT OUTPUT			  -- 总记录数
)
AS
	-- 设置：不返回 Transact-SQL 语句影响的行数
	-- 原因：如果存储过程中包含的一些语句并不返回许多实际的数据，则该设置由于大量减少了网络流量，因此可显著提高性能
	SET NOCOUNT ON
		
	-- 定义变量
	DECLARE @cmdText NVARCHAR(1024)
	DECLARE @cmdTextForTotalCount NVARCHAR(1024)
	DECLARE @partOfWhere NVARCHAR(1024)
	DECLARE @partOfOrderBy NVARCHAR(1024)
	
	-- 获取 WHERE 部分命令
	SET @partOfWhere = N'WHERE (' + @condition + ') '
	-- 获取 ORDER BY 部分命令
	IF @orderColumn IS NOT NULL AND @orderColumn <> N''
	BEGIN
		IF @orderType = 1
		BEGIN
			SET @partOfOrderBy = N'ORDER BY ' + @orderColumn + N' DESC '
		END
		ELSE IF @orderType = 0
		BEGIN
			SET @partOfOrderBy = N'ORDER BY ' + @orderColumn + N' ASC '
		END
	END
	ELSE
	BEGIN
		SET @partOfOrderBy = N''
	END
	
	-- 获取查询总记录数命令
	IF @condition IS NULL OR @condition = N''
	BEGIN
		SET @cmdTextForTotalCount = N'SELECT @totalCount = COUNT(*) FROM ' + @tableName + ''
	END
	ELSE
	BEGIN
		SET @cmdTextForTotalCount = N'SELECT @totalCount = COUNT(1) FROM ' + @tableName + '' + @partOfWhere
	END
	-- 查询总记录数
	EXEC sp_executesql @cmdTextForTotalCount, N'@totalCount INT OUTPUT', @totalCount OUTPUT
	-- 计算总页数
	DECLARE @tempTotalCount INT
	IF @totalCount = 0
	BEGIN
		SET @tempTotalCount = 1		
	END
	ELSE
	BEGIN
		SET @tempTotalCount = @totalCount
	END
	SET @pageCount = (@tempTotalCount + @pageSize - 1) / @pageSize
	
	-- 查询数据
	-- 查询第一页数据
	IF @pageIndex = 1
	BEGIN
		-- 查询条件、排序列均为空
		IF (@condition IS NULL OR @condition = N'') AND (@orderColumn IS NULL OR @orderColumn = '')
		BEGIN
			SET @cmdText = N'SELECT TOP ' + LTRIM(STR(@pageSize)) + N' ' + @tableName + '.*, [User_Level].Name AS UserLevelName FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID '
		END
		ELSE
		BEGIN
			-- 查询条件不为空，排序列为空
			IF (@condition IS NOT NULL AND @condition <> N'') AND (@orderColumn IS NULL OR @orderColumn = N'')
			BEGIN
				SET @cmdText = N'SELECT TOP ' + LTRIM(STR(@pageSize)) + N' ' + @tableName + '.*, [User_Level].Name AS UserLevelName FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID ' + @partOfWhere
			END
			-- 排序列不为空，查询条件为空
			ELSE IF (@orderColumn IS NOT NULL AND @orderColumn <> N'') AND (@condition IS NULL OR @condition = N'')
			BEGIN
				SET @cmdText = N'SELECT TOP ' + LTRIM(STR(@pageSize)) + N' ' + @tableName + '.*, [User_Level].Name AS UserLevelName FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID ' + @partOfOrderBy
			END
			-- 查询条件、排序列都不为空
			ELSE
			BEGIN
				SET @cmdText = N'SELECT TOP ' + LTRIM(STR(@pageSize)) + N' ' + @tableName + '.*, [User_Level].Name AS UserLevelName FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID ' + @partOfWhere + @partOfOrderBy
			END
		END
	END
	-- 查询非第一页数据
	ELSE
	BEGIN
		-- 查询条件、排序列均为空
		IF (@condition IS NULL OR @condition = N'') AND (@orderColumn IS NULL OR @orderColumn = '')
		BEGIN
			SET @cmdText = N'SELECT TOP ' + LTRIM(STR(@pageSize)) + N' ' + @tableName + '.*, [User_Level].Name FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID WHERE ' + @tableName + '.ID > '
			SET @cmdText += N'(SELECT MAX(ID) FROM '
			SET @cmdText += N'(SELECT TOP ' + LTRIM(STR(@pageSize * (@pageIndex - 1))) + N' ' + @tableName + '.ID FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID ORDER BY ' + @tableName + '.ID) AS T)'
		END
		ELSE
		BEGIN
			-- 查询条件不为空，排序列为空
			IF (@condition IS NOT NULL AND @condition <> N'') AND (@orderColumn IS NULL OR @orderColumn = N'')
			BEGIN
				SET @cmdText = N'SELECT u.* FROM ';
				SET @cmdText += N'(SELECT ROW_NUMBER() OVER(ORDER BY ' + @tableName + '.ID) AS rowNumber, ' + @tableName + '.*, [User_Level].Name AS UserLevelName FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID ' + @partOfWhere + N') as u ';
				SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex))
				SET @partOfWhere = REPLACE(@partOfWhere, N'WHERE', N' AND')
				SET @partOfWhere = REPLACE(@partOfWhere, N'' + @tableName + '', N'u')
				SET @cmdText += @partOfWhere
			END
			-- 排序列不为空，查询条件为空
			ELSE IF (@orderColumn IS NOT NULL AND @orderColumn <> N'') AND (@condition IS NULL OR @condition = N'')
			BEGIN
				SET @cmdText = N'SELECT u.* FROM ';
				SET @cmdText += N'(SELECT ROW_NUMBER() OVER(' + @partOfOrderBy + N') AS rowNumber, ' + @tableName + '.*, [User_Level].Name AS UserLevelName FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID) as u ';
				SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex))
			END
			-- 查询条件、排序列都不为空
			ELSE
			BEGIN
				SET @cmdText = N'SELECT u.* FROM ';
				SET @cmdText += N'(SELECT ROW_NUMBER() OVER(' + @partOfOrderBy + N') AS rowNumber, ' + @tableName + '.*, [User_Level].Name AS UserLevelName FROM ' + @tableName + ' INNER JOIN [User_Level] ON [User_Level].[ID] = ' + @tableName + '.UserLevelID ' + @partOfWhere + N') as u ';
				SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex))
				SET @partOfWhere = REPLACE(@partOfWhere, N'WHERE', N' AND')
				SET @partOfWhere = REPLACE(@partOfWhere, N'' + @tableName + '', N'u')
				SET @cmdText += @partOfWhere
			END
		END
	END
	EXEC sp_executesql @cmdText
	SET NOCOUNT OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Paging]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Order_Paging]
(
	@tableName NVARCHAR(128),		  -- 表名
	@columnName NVARCHAR(256) = N'*', -- 查询的列名
	@primaryKey NVARCHAR(32) = N'ID', -- 主键列名
	@condition NVARCHAR(1024) = NULL,  -- 查询条件（不需要 WHERE 关键字）
	@pageIndex INT = 1,				  -- 页码
	@pageSize INT = 10,				  -- 每页记录数量
	@orderColumn NVARCHAR(64) = NULL, -- 排序列名
	@orderType INT = 1,				  -- 排序类型（0：升序，1：降序）
	@distinct BIT = 1,				  -- 是否添加 DISTINCT 排除重复记录，当主键列自增时，无须添加（0：添加，1：不添加）
	@pageCount INT OUTPUT,			  -- 总页数
	@totalCount INT OUTPUT			  -- 总记录数
)
AS
	-- 设置：不返回 Transact-SQL 语句影响的行数
	-- 原因：如果存储过程中包含的一些语句并不返回许多实际的数据，则该设置由于大量减少了网络流量，因此可显著提高性能
	SET NOCOUNT ON
	
	-- 检查对象名称是否为空
	IF @tableName IS NULL OR @tableName = N''
	BEGIN
		RAISERROR(N'指定查询的对象不能为空', 1, 16, @tableName)
		RETURN
	END
	-- 检查对象是否存在
	IF OBJECT_ID(@tableName) IS NULL
	BEGIN
		RAISERROR(N'指定查询的对象（表、视图、表值函数）不存在', 1, 16, @tableName)
		RETURN
	END
	-- 检查对象类型是否正确
	IF OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTable') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsView') = 0
	AND OBJECTPROPERTY(OBJECT_ID(@tableName), N'IsTableFunction') = 0
	BEGIN
		RAISERROR(N'指定对象的类型必须为：表、视图或表值函数', 1, 16, @tableName)
		RETURN
	END
	
	-- 定义变量
	DECLARE @cmdText NVARCHAR(1024)
	DECLARE @cmdTextForTotalCount NVARCHAR(1024)
	DECLARE @partOfSelect NVARCHAR(1024)
	DECLARE @partOfCount NVARCHAR(1024)
	DECLARE @partOfWhere NVARCHAR(1024)
	DECLARE @partOfOrderBy NVARCHAR(1024)
	DECLARE @partOfFrom NVARCHAR(1024)
	
	-- 获取 SELECT 和 COUNT 部分命令
	IF @primaryKey <> N'ID'
	BEGIN
		-- 判断主键列是否为自增列以及是否添加 DISTINCT 排除重复记录
		IF COLUMNPROPERTY(OBJECT_ID(@tableName), @primaryKey, N'IsIdentity') <> 1 AND @distinct = 0
		BEGIN
			SET @partOfSelect = N'SELECT DISTINCT '
			SET @partOfCount = N'COUNT(DISTINCT' + @primaryKey + N') '
		END
		ELSE
		BEGIN
			SET @partOfSelect = N'SELECT '
			-- 带查询条件时，Count(1) 为首选，总耗时略低于 Count(*)，但 CPU 耗时远低于 Count(*)
			IF @condition IS NOT NULL OR @condition <> ''
			BEGIN
				SET @partOfCount = N'COUNT(1) '
			END
			ELSE
			BEGIN
				SET @partOfCount = N'COUNT(*) '
			END
		END
	END
	ELSE
	BEGIN
		SET @partOfSelect = N'SELECT '
		-- 带查询条件时，Count(1) 为首选，总耗时略低于 Count(*)，但 CPU 耗时远低于 Count(*)
		IF @condition IS NOT NULL OR @condition <> ''
		BEGIN
			SET @partOfCount = N'COUNT(1) '
		END
		ELSE
		BEGIN
			SET @partOfCount = 'COUNT(*) '
		END
	END
	-- 获取 FROM 部分命令
	SET @partOfFrom = N'FROM ' + @tableName + N' '
	-- 获取 WHERE 部分命令
	SET @partOfWhere = N'WHERE (' + @condition + ') '
	-- 获取 ORDER BY 部分命令
	IF @orderColumn IS NOT NULL AND @orderColumn <> N''
	BEGIN
		IF @orderType = 1
		BEGIN
			SET @partOfOrderBy = N'ORDER BY ' + @orderColumn + N' DESC '
		END
		ELSE IF @orderType = 0
		BEGIN
			SET @partOfOrderBy = N'ORDER BY ' + @orderColumn + N' ASC '
		END
	END
	ELSE
	BEGIN
		SET @partOfOrderBy = N''
	END
	-- 获取查询总记录数命令
	IF @condition IS NULL OR @condition = N''
	BEGIN
		SET @cmdTextForTotalCount = @partOfSelect + N'@totalCount = ' + @partOfCount + @partOfFrom
	END
	ELSE
	BEGIN
		SET @cmdTextForTotalCount = @partOfSelect + N'@totalCount = ' + @partOfCount + + N' FROM 
		[Order] as [Order]
		INNER JOIN [User] as [User] on [Order].UserID = [User].ID
		INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
		INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID ' + @partOfWhere
	END
	-- 查询总记录数
	EXEC sp_executesql @cmdTextForTotalCount, N'@totalCount INT OUTPUT', @totalCount OUTPUT
	-- 计算总页数
	DECLARE @tempTotalCount INT
	IF @totalCount = 0
	BEGIN
		SET @tempTotalCount = 1		
	END
	ELSE
	BEGIN
		SET @tempTotalCount = @totalCount
	END
	SET @pageCount = (@tempTotalCount + @pageSize - 1) / @pageSize
	-- 查询数据
	-- 查询第一页数据
	IF @pageIndex = 1
	BEGIN
		-- 查询条件、排序列均为空
		IF (@condition IS NULL OR @condition = N'') AND (@orderColumn IS NULL OR @orderColumn = '')
		BEGIN
			SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' [Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID ' 
		END
		ELSE
		BEGIN
			-- 查询条件不为空，排序列为空
			IF (@condition IS NOT NULL OR @condition <> N'') AND (@orderColumn IS NULL OR @orderColumn = N'')
			BEGIN
				SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' [Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID ' + @partOfWhere
			END
			-- 排序列不为空，查询条件为空
			ELSE IF (@orderColumn IS NOT NULL OR @orderColumn <> N'') AND (@condition IS NULL OR @condition = N'')
			BEGIN
				SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' [Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID '
			END
			-- 查询条件、排序列都不为空
			ELSE
			BEGIN
				SET @cmdText = @partOfSelect + N'TOP ' + LTRIM(STR(@pageSize)) + N' [Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID '
				SET @cmdText += @partOfWhere + @partOfOrderBy
			END
		END
	END
	-- 查询非第一页数据
	ELSE
	BEGIN
		-- 查询条件、排序列均为空
		IF (@condition IS NULL OR @condition = N'') AND (@orderColumn IS NULL OR @orderColumn = '')
		BEGIN
				SET @cmdText = N'SELECT * FROM '
				SET @cmdText += N'(SELECT ROW_NUMBER() OVER(ORDER BY [Order].ID) AS rowNumber, ' + N'[Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID) AS T '
				SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex))
		END
		ELSE
		BEGIN
			-- 查询条件不为空，排序列为空
			IF (@condition IS NOT NULL OR @condition <> N'') AND (@orderColumn IS NULL OR @orderColumn = N'')
			BEGIN
				SET @cmdText = N'SELECT * FROM '
				SET @cmdText += N'(SELECT ROW_NUMBER() OVER(ORDER BY [Order].ID) AS rowNumber, ' + N'[Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID ' + @partOfWhere + N') AS T '
				SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex)) + N' '
				SET @partOfWhere = REPLACE(@partOfWhere, N'WHERE', N'AND')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[Order].', N'T.')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[User].', N'T.')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[User_RecieveAddress].', N'T.')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[Cps].', N'T.')
				SET @cmdText += @partOfWhere
			END
			-- 排序列不为空，查询条件为空
			ELSE IF (@orderColumn IS NOT NULL OR @orderColumn <> N'') AND (@condition IS NULL OR @condition = N'')
			BEGIN
				SET @cmdText = N'SELECT * FROM '
				SET @cmdText += N'(SELECT ROW_NUMBER() OVER(' + @partOfOrderBy + N') AS rowNumber, ' + N'[Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID) AS T '
				SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex))
			END
			-- 查询条件、排序列都不为空
			ELSE
			BEGIN
				SET @cmdText = N'SELECT * FROM '
				SET @cmdText += N'(SELECT ROW_NUMBER() OVER(' + @partOfOrderBy + N') AS rowNumber, ' + N'[Order].*, [User].Name as [UserName], [User].Mobile as [UserMobile], [User].Email as [UserEmail], [User_RecieveAddress].Consignee as Consignee, [Cps].Name as CpsName FROM 
				[Order] as [Order]
				INNER JOIN [User] as [User] on [Order].UserID = [User].ID
				INNER JOIN [User_RecieveAddress] as [User_RecieveAddress] on [Order].RecieveAddressID = [User_RecieveAddress].ID
				INNER JOIN [Cps] as [Cps] on [Order].CpsID = [Cps].ID ' + @partOfWhere + N') AS T '
				SET @cmdText += N'WHERE rowNumber BETWEEN ' + LTRIM(STR(@pageSize * (@pageIndex - 1) + 1)) + N' AND ' + LTRIM(STR(@pageSize * @pageIndex)) + N' '
				SET @partOfWhere = REPLACE(@partOfWhere, N'WHERE', N'AND')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[Order].', N'T.')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[User].', N'T.')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[User_RecieveAddress].', N'T.')
				SET @partOfWhere = REPLACE(@partOfWhere, N'[Cps].', N'T.')
				SET @cmdText += @partOfWhere
			END
		END
	END
	print @cmdText
	EXEC sp_executesql @cmdText
	SET NOCOUNT OFF
GO
/****** Object:  Table [dbo].[v4_usr_UserSignRecord]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v4_usr_UserSignRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SignTime] [nvarchar](32) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[v4_usr_Subscibe]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v4_usr_Subscibe](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[user_Mail] [nvarchar](50) NOT NULL,
	[IsSubscibe] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[MailVali] [int] NULL,
	[State] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[v4_Usr_FindMailPassword]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[v4_Usr_FindMailPassword](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[usr_UserID] [int] NOT NULL,
	[VirificationCode] [varchar](100) NOT NULL,
	[Mail] [varchar](100) NULL,
	[State] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[FailTime] [datetime] NULL,
	[ExtField] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[v4_odr_OrderReturnProduct]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v4_odr_OrderReturnProduct](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[odr_OrderReturnID] [int] NOT NULL,
	[odr_OrderProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[State] [int] NULL,
	[CreateTime] [datetime] NULL,
	[ExtField] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[v4_odr_OrderReturnAudit]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v4_odr_OrderReturnAudit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeState] [int] NOT NULL,
	[adm_AdminID] [int] NOT NULL,
	[odr_OrderReturnID] [int] NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[State] [int] NULL,
	[CreateTime] [datetime] NULL,
	[ExtField] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[v4_odr_OrderReturn]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v4_odr_OrderReturn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[odr_OrderID] [int] NOT NULL,
	[Reason] [int] NULL,
	[Remark] [nvarchar](500) NULL,
	[ReturnState] [tinyint] NOT NULL,
	[TotalPrice] [float] NULL,
	[State] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
	[ExtField] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[v4_odr_OrderDiscount]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v4_odr_OrderDiscount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[odr_OrderID] [int] NOT NULL,
	[Intro] [nvarchar](50) NULL,
	[Money] [float] NOT NULL,
	[DType] [tinyint] NOT NULL,
	[State] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ExtField] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单折扣记录（v4_odr_OrderDiscount）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'v4_odr_OrderDiscount'
GO
/****** Object:  Table [dbo].[v4_odr_OrderChange]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[v4_odr_OrderChange](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[odr_OrderID] [int] NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
	[adm_AdminID] [int] NOT NULL,
	[State] [tinyint] NULL,
	[CreateTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_RecieveAddress]    Script Date: 03/02/2014 20:58:36 ******/
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
	[Address] [nvarchar](128) NOT NULL,
	[Consignee] [nvarchar](50) NULL,
	[Mobile] [varchar](16) NOT NULL,
	[Tel] [varchar](16) NULL,
	[IsDefault] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[Email] [varchar](256) NULL,
	[ZipCode] [varchar](50) NULL,
	[ExtField] [nvarchar](50) NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在区县编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_RecieveAddress', @level2type=N'COLUMN',@level2name=N'CountyID'
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
/****** Object:  Table [dbo].[User_Message_Sms]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Message_Sms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Content] [nvarchar](256) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User_Message_Sms] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Sms', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Sms', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短信名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Sms', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短信内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Sms', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（0：启用，1：禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Sms', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Sms', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户短信消息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Sms'
GO
/****** Object:  Table [dbo].[User_Message_SendRecord]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Message_SendRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[MessageID] [int] NOT NULL,
	[MessageTypeID] [int] NOT NULL,
	[UserCount] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User_Message_SendRecord] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逐渐编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_SendRecord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_SendRecord', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_SendRecord', @level2type=N'COLUMN',@level2name=N'MessageID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型编号（1：邮件，2：短信）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_SendRecord', @level2type=N'COLUMN',@level2name=N'MessageTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_SendRecord', @level2type=N'COLUMN',@level2name=N'UserCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_SendRecord', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户消息发送记录表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_SendRecord'
GO
/****** Object:  Table [dbo].[User_Message_Email]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Message_Email](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Title] [nvarchar](128) NOT NULL,
	[Content] [ntext] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User_Message_Email] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮件状态（0：启用，1：禁用）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户邮件消息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Message_Email'
GO
/****** Object:  Table [dbo].[User_Level_Price]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Level_Price](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[UserLevelID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User_Level_Price] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员工编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'EmployeeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员等级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'UserLevelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'针对的商品条形码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员价格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'Price'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态（1：可用，2：暂停）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员等级价格表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level_Price'
GO
/****** Object:  Table [dbo].[User_Level]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Level](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Money] [float] NOT NULL,
	[Level] [int] NULL,
	[ExtField] [nvarchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[OfferPercent] [float] NULL,
 CONSTRAINT [PK_User_Level] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员等级名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员等级金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员等级表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Level'
GO
/****** Object:  Table [dbo].[User_Integral_Details]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Integral_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Integral] [int] NOT NULL,
	[OperateType] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User_Integral_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型（1：获取，2：消耗）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'OperateType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员积分明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Integral_Details'
GO
/****** Object:  Table [dbo].[User_CollectRecord]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_CollectRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ProductID] [int] NULL,
	[CreateTime] [datetime] NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User_Collect_Record] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_CollectRecord', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_CollectRecord', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_CollectRecord', @level2type=N'COLUMN',@level2name=N'ProductID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_CollectRecord', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_CollectRecord', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员收藏商品记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_CollectRecord'
GO
/****** Object:  Table [dbo].[User_BrowseHistory]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_BrowseHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_BrowseHistory', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户浏览历史表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_BrowseHistory'
GO
/****** Object:  Table [dbo].[User_Account_Details]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Account_Details](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserAccountID] [int] NOT NULL,
	[UserID] [int] NULL,
	[Money] [float] NOT NULL,
	[OperateType] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_User_Account_Details] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员账户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'UserAccountID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型（1：存入，2：支出，3：退款）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'OperateType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员账户明细表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account_Details'
GO
/****** Object:  Table [dbo].[User_Account]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Balance] [float] NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_User_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员账户余额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'Balance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员账户状态（1：正常，2：锁定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员账户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Account'
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/02/2014 20:58:36 ******/
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
	[Email] [varchar](64) NULL,
	[EmailValidate] [bit] NOT NULL,
	[Mobile] [varchar](32) NULL,
	[MobileValidate] [bit] NOT NULL,
	[Name] [nvarchar](64) NULL,
	[Age] [int] NULL,
	[Gender] [bit] NULL,
	[Address] [nvarchar](256) NULL,
	[LoginName] [varchar](64) NULL,
	[LoginPassword] [varchar](64) NOT NULL,
	[Integral] [int] NOT NULL,
	[Head] [nvarchar](1024) NULL,
	[NickName] [nvarchar](64) NULL,
	[Birthday] [datetime] NULL,
	[QQ] [varchar](20) NULL,
	[MSN] [varchar](64) NULL,
	[OpenID] [varchar](64) NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[LastLoginTime] [datetime] NULL,
	[IsDelete] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPS 编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CpsID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员级别编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UserLevelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员所在区县编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CountyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员电子邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电子邮箱是否验证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'EmailValidate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码是否验证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'MobileValidate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登陆名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登陆密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'LoginPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员积分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Integral'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Head'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员生日' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Birthday'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员 QQ 号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'QQ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员 MSN 账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'MSN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'互联登陆编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'OpenID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员状态（1：正常，2：锁定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登陆时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会员表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User'
GO
/****** Object:  Table [dbo].[System_Visitor]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Visitor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
	[SessionID] [nvarchar](200) NULL,
	[UserName] [nvarchar](50) NULL,
	[IP4Address] [nvarchar](50) NULL,
	[IP6Address] [nvarchar](50) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_Visitor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'StartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'EndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'会话ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'SessionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IPV4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'IP4Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IPV6' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'IP6Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Visitor', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
/****** Object:  Table [dbo].[System_User]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[LoginName] [varchar](16) NOT NULL,
	[LoginPassword] [varchar](64) NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_User] PRIMARY KEY CLUSTERED 
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'RoleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登陆名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登陆密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'LoginPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户状态（1：正常，2：锁定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_User'
GO
/****** Object:  Table [dbo].[System_Role_Permission]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Role_Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[PermissionID] [int] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_Role_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission', @level2type=N'COLUMN',@level2name=N'RoleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission', @level2type=N'COLUMN',@level2name=N'PermissionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统角色权限关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role_Permission'
GO
/****** Object:  Table [dbo].[System_Role]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Headcount] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色使用人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'Headcount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Role'
GO
/****** Object:  Table [dbo].[System_Rights]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Rights](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NULL,
	[UserID] [int] NULL,
	[UserRights] [text] NULL,
	[IsDelete] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Rights', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Rights', @level2type=N'COLUMN',@level2name=N'RoleID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Rights', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Rights', @level2type=N'COLUMN',@level2name=N'UserRights'
GO
/****** Object:  Table [dbo].[System_Resources]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_Resources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[ParentCode] [varchar](100) NOT NULL,
	[Key] [varchar](100) NULL,
	[Description] [varchar](200) NOT NULL,
	[Position] [int] NOT NULL,
	[IsDelete] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Position] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[System_Permission]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[System_Permission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Layer] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限层级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统权限表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Permission'
GO
/****** Object:  Table [dbo].[System_Menu]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[PermissionID] [int] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[URL] [varchar](32) NULL,
	[Layer] [int] NOT NULL,
	[Sorting] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父菜单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'PermissionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'URL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单层级编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'Layer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'Sorting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统菜单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Menu'
GO
/****** Object:  Table [dbo].[System_Log]    Script Date: 03/02/2014 20:58:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SessionID] [varchar](200) NULL,
	[UserID] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[Action] [nvarchar](50) NULL,
	[Level] [int] NULL,
	[Location] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[IsDelete] [int] NULL,
	[UserName] [varchar](100) NULL,
 CONSTRAINT [PK__System_L__3214EC27607D3EDD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[System_Employee]    Script Date: 03/02/2014 20:58:36 ******/
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
	[Name] [nvarchar](32) NOT NULL,
	[IdentityCard] [varchar](32) NULL,
	[IdentityCardAddress] [nvarchar](64) NULL,
	[BankCard] [varchar](32) NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](2) NOT NULL,
	[Mobile] [varchar](16) NULL,
	[HomeAddress] [nvarchar](32) NULL,
	[Status] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工所属部门编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'DepartmentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工户口所在区县编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'CountyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工身份证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'IdentityCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工身份证地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'IdentityCardAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工工资卡' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'BankCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工年龄' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Age'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Gender'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工家庭住址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'HomeAddress'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工状态（1：在职，2：离职）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统员工表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Employee'
GO
/****** Object:  Table [dbo].[System_Department]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_Department](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](8) NOT NULL,
	[Headcount] [int] NOT NULL,
	[Principal] [nvarchar](16) NOT NULL,
	[PrincipalMobile] [varchar](16) NOT NULL,
	[Description] [ntext] NULL,
	[CreateTime] [datetime] NOT NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_Department] PRIMARY KEY CLUSTERED 
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门人数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'Headcount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'Principal'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门负责人手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'PrincipalMobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统部门表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_Department'
GO
/****** Object:  Table [dbo].[System_DataDictionary]    Script Date: 03/02/2014 20:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[System_DataDictionary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](50) NULL,
	[TableDescription] [varchar](200) NULL,
	[ColumnName] [varchar](50) NULL,
	[ColumnType] [varchar](50) NULL,
	[ColumnLength] [int] NULL,
	[ColumnDescription] [varchar](200) NULL,
	[IsDelete] [int] NULL,
 CONSTRAINT [PK_System_DataDictionary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_DataDictionary', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_DataDictionary', @level2type=N'COLUMN',@level2name=N'TableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_DataDictionary', @level2type=N'COLUMN',@level2name=N'TableDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_DataDictionary', @level2type=N'COLUMN',@level2name=N'ColumnName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列数据类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_DataDictionary', @level2type=N'COLUMN',@level2name=N'ColumnType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_DataDictionary', @level2type=N'COLUMN',@level2name=N'ColumnLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'System_DataDictionary', @level2type=N'COLUMN',@level2name=N'ColumnDescription'
GO
/****** Object:  UserDefinedFunction [dbo].[split]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create FUNCTION [dbo].[split](@Long_str varchar(8000),@split_str varchar(100))    
RETURNS  @tmp TABLE(        
    short_str varchar(8000)    
)    
AS   
BEGIN
	IF CHARINDEX(@split_str,@Long_str)<=0
	INSERT INTO @tmp SELECT @Long_str 
	ELSE
	BEGIN
    DECLARE @long_str_Tmp varchar(8000),
   @short_str varchar(8000),
   @split_str_length int   
 
    SET @split_str_length = LEN(@split_str)    
 
    IF CHARINDEX(@split_str,@Long_str)=1 
         SET @long_str_Tmp=SUBSTRING(@Long_str,
     @split_str_length+1,
     LEN(@Long_str)-@split_str_length)
 
    ELSE
         SET @long_str_Tmp=@Long_str
 
    IF CHARINDEX(REVERSE(@split_str),REVERSE(@long_str_Tmp))>1    
        SET @long_str_Tmp=@long_str_Tmp+@split_str    
    ELSE   
        SET @long_str_Tmp=@long_str_Tmp    
 
    WHILE CHARINDEX(@split_str,@long_str_Tmp)>0    
        BEGIN   
            SET @short_str=SUBSTRING(@long_str_Tmp,1,
     CHARINDEX(@split_str,@long_str_Tmp)-1)    
            DECLARE @long_str_Tmp_LEN INT,@split_str_Position_END int   
            SET @long_str_Tmp_LEN = LEN(@long_str_Tmp)    
            SET @split_str_Position_END = LEN(@short_str)+@split_str_length    
            SET @long_str_Tmp=REVERSE(SUBSTRING(REVERSE(@long_str_Tmp),1,
     @long_str_Tmp_LEN-@split_str_Position_END))
            IF @short_str<>'' INSERT INTO @tmp SELECT @short_str    
        END   
	END
    RETURN     
END
GO
/****** Object:  View [dbo].[view_Product_Consults]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Product_Consults]
AS
SELECT C.[ID] ID    
      ,[ProductID]    
      ,P.Name ProductName    
      ,[UserID]    
      ,U.Name UserName    
      ,[ConsultPerson]    
      ,[ConsultPersonMobile]    
      ,[ConsultPersonEmail]    
      ,C.[Content]  ConsultContent 
      ,R.[Content] Reply      
      ,C.[CreateTime] ConsultTime    
  FROM [Product_Consult] C 
  Left Join Product_Consult_Reply R ON C.ID = R.ConsultID
  JOIN [User] U ON C.UserID= U.ID  And U.IsDelete = 0  
  Join Product P ON C.ProductID = P.ID  And P.IsDelete = 0  
  And C.IsDelete = 0
GO
/****** Object:  View [dbo].[view_Product_Consult_Reply]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Product_Consult_Reply]
AS
SELECT CR.[ID] ID
      ,[ProductID]
      ,P.Name ProductName
      ,[UserID]
      ,U.Name UserName
      ,CR.EmployeeID EmployeeID,
      SE.Name EmployeeName,
      CR.Content Content,
      CR.CreateTime ConsultTime     
      ,U.Name [ConsultPerson]
      ,U.Mobile [ConsultPersonMobile]
      ,U.Email [ConsultPersonEmail]
      ,C.[Content] ConsultContent
      ,C.[CreateTime] CreateTime
  FROM Product_Consult_Reply CR left JOIN [Product_Consult] C On CR.ConsultID=C.ID And C.IsDelete = 0
  left Join [User] U On C.UserID=U.ID And U.IsDelete = 0
  left Join [System_Employee] SE ON SE.ID=CR.EmployeeID And SE.IsDelete = 0
  Join Product P ON C.ProductID = P.ID And P.IsDelete = 0
  Where CR.IsDelete = 0
GO
/****** Object:  View [dbo].[view_Product_Consult]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Product_Consult]
AS
SELECT C.[ID] ID  
      ,[ProductID]  
      ,P.Name ProductName  
      ,[UserID]  
      ,U.Name UserName  
      ,[ConsultPerson]  
      ,[ConsultPersonMobile]  
      ,[ConsultPersonEmail]  
      ,[Content]  ConsultContent
      ,C.[CreateTime] ConsultTime  
  FROM [Product_Consult] C JOIN [User] U ON C.UserID= U.ID  And U.IsDelete = 0
  Join Product P ON C.ProductID = P.ID  And P.IsDelete = 0
  WHERE	C.ID not in (
	select ConsultID ID from Product_Consult_Reply Where IsDelete = 0
  )
  And C.IsDelete = 0
GO
/****** Object:  View [dbo].[view_product_Comment]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_product_Comment]
as
SELECT pc.[ID] [ID]
      ,[ProductID] 
      ,p.Name ProductName
      ,[OrderID]
      ,[UserID]
      ,u.Name UserName
      ,[EmployeeID]
      ,emp.Name EmployeeName
      ,[Score]
      ,[Content]
      ,pc.[Status]
      ,pc.[CreateTime]
      ,[AuditTime]
      ,[AuditDescription]
  FROM [Product_Comment] pc join Product p
  on pc.ProductID=p.ID And P.IsDelete = 0
  join [user] u on pc.UserID=u.ID And U.IsDelete = 0
  join System_Employee emp on pc.EmployeeID=emp.ID And emp.IsDelete = 0
  Where PC.IsDelete = 0
GO
/****** Object:  View [dbo].[view_Orders]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Orders] As
	Select O.[ID] [ID]
		  ,O.[UserID]
		  ,U.Name UserName
		  ,U.Mobile [UserMoblie]
		  ,U.Email [UserEmail]
		  ,[RecieveAddressID]
		  ,URA.Consignee [Consignee]
		  ,O.[CpsID] [CpsID]
		  ,Cps.Name [CpsName]
		  ,[PaymentMethodID]
		  ,[OrderCode]
		  ,[OrderNumber]
		  ,[DeliveryCost]
		  ,[TotalMoney]
		  ,[TotalIntegral]
		  ,[PaymentStatus]
		  ,[IsRequireInvoice]
		  ,O.[Status] [Status]
		  ,[Description]
		  ,[Remark]
		  ,O.[CreateTime] [CreateTime]
	 from [Order] O 
	 Left Join [User] U On O.UserID = U.ID And U.IsDelete = 0
	 Left Join [User_RecieveAddress] URA On URA.ID = O.RecieveAddressID And U.ID = URA.UserID  And URA.IsDelete = 0
	 Left Join [Cps] On O.CpsID = Cps.ID And Cps.IsDelete = 0
	 Where O.IsDelete = 0
GO
/****** Object:  View [dbo].[view_Order_Products]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Order_Products] As
	Select distinct
		OP.[ID],
		OrderID,
		OrderCode,
		U.ID UserID,
		U.Name UserName,
		URA.Consignee Consignee,
		P.ID ProductID,
		p.Barcode Barcode,
		P.Name ProductName,
		Pic.[Path] [Path],
		Quantity,
		P.MarketPrice MarkerPrice,
		P.GoujiuPrice GoujiuPrice,
		TransactPrice,
		OP.CreateTime Createtime
	From [Order] O Left Join Order_Product OP On O.ID = OP.OrderID And OP.IsDelete = 0
	Left Join [User] U On O.UserID = U.ID  And U.IsDelete = 0
	Left Join [User_RecieveAddress] URA On O.RecieveAddressID = URA.ID  And URA.IsDelete = 0
	Left join Product P On OP.ProductID = P.ID  And P.IsDelete = 0
	left JOin Product_Picture PPic On PPic.ProductID=P.ID and PPic.IsMaster = 1  And PPic.IsDelete = 0
	Left Join Picture Pic On Pic.ID = PPic.PictureID  And Pic.IsDelete = 0
	Where O.IsDelete = 0
GO
/****** Object:  View [dbo].[view_GroupBuy_Product]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[view_GroupBuy_Product]
as 
select
Product.ID ProductId,
Channel_GroupBuy.Status,
Channel_GroupBuy.Name GroupBuyName,
Product.Barcode,
Channel_GroupBuy.ImageUrl,
Channel_GroupBuy.StartTime,
Channel_GroupBuy.EndTime,
Product.Name ProductName,
Channel_GroupBuy.TotalNumber,
Product.GoujiuPrice,
User_Level.Name LevelName,
Channel_GroupBuy.ShowLevel,
User_Level.ID UserLevelID,
Channel_GroupBuy.GBPrice,
Channel_GroupBuy.IsDelete
 from Channel_GroupBuy left join Product on Channel_GroupBuy.ProductID=Product.ID left join User_Level on Channel_GroupBuy.UserLevelID=User_Level.ID where Channel_GroupBuy.IsDelete=0
GO
/****** Object:  View [dbo].[view_Coupon_Paging]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Coupon_Paging]
As
	Select 
		'0' As Type,
		'现金券' As TypeName,
		[ID],
		[EmployeeID],
		[Name],
		[FaceValue],
		[InitialNumber],
		[Description],
		[StartTime],
		[EndTime],
		[CreateTime]
	From Coupon_Cash
	Where IsDelete=0 And [EndTime]>CONVERT(datetime,GETDATE(),102)
	Union
	Select
		'1' As Type,
		'满减券' As TypeName,
		[ID],
		[EmployeeID],
		[Name],
		[FaceValue],
		[InitialNumber],
		[Description],
		[StartTime],
		[EndTime],
		[CreateTime]
	From Coupon_Decrease
	Where IsDelete=0 And [EndTime]>CONVERT(datetime,GETDATE(),102)
GO
/****** Object:  View [dbo].[view_Coupon_Decrease_SelectAllInfo]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[view_Coupon_Decrease_SelectAllInfo]
as
SELECT
	[Coupon_Decrease].[ID],
	[Coupon_Decrease].[Name],
	[Coupon_Decrease].[FaceValue],
	[Coupon_Decrease].[MeetAmount],
	[Coupon_Decrease].[EmployeeID],
	[Coupon_Decrease].[StartTime],
	[Coupon_Decrease].[EndTime],
	[Coupon_Decrease].[Description],
	[Coupon_Decrease].[CreateTime],
	[Coupon_Decrease].[InitialNumber],
	(select COUNT([Coupon_Decrease_Binding].[CouponDecreaseID])from [Coupon_Decrease_Binding] where [Coupon_Decrease_Binding].[CouponDecreaseID]= [Coupon_Decrease].[ID]) as[Bind],
	([InitialNumber]-(select COUNT([Coupon_Decrease_Binding].[CouponDecreaseID])from [Coupon_Decrease_Binding] where [Coupon_Decrease_Binding].[CouponDecreaseID]= [Coupon_Decrease].[ID])) as [Remain],
	(select COUNT([Coupon_Decrease_Binding].[CouponDecreaseID])from [Coupon_Decrease_Binding] where [Coupon_Decrease_Binding].[CouponDecreaseID]= [Coupon_Decrease].[ID] and [Coupon_Decrease_Binding].[Status]=1) as [Cost]
from [Coupon_Decrease]
where [Coupon_Decrease].IsDelete = 0
GO
/****** Object:  View [dbo].[view_Coupon_Cash_SelectAllInfo]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[view_Coupon_Cash_SelectAllInfo]
as
SELECT
	[Coupon_Cash].[ID],
	[Coupon_Cash].[Name],
	[Coupon_Cash].[FaceValue],
	[Coupon_Cash].[EmployeeID],
	[Coupon_Cash].[StartTime],
	[Coupon_Cash].[EndTime],
	[Coupon_Cash].[Description],
	[Coupon_Cash].[CreateTime],
	[Coupon_Cash].[InitialNumber],
	(select COUNT([Coupon_Cash_Binding].[CouponCashID])from [Coupon_Cash_Binding] where [Coupon_Cash_Binding].[CouponCashID]= [Coupon_Cash].[ID]) as[Bind],
	([InitialNumber]-(select COUNT([Coupon_Cash_Binding].[CouponCashID])from [Coupon_Cash_Binding] where [Coupon_Cash_Binding].[CouponCashID]= [Coupon_Cash].[ID])) as [Remain],
	(select COUNT([Coupon_Cash_Binding].[CouponCashID])from [Coupon_Cash_Binding] where [Coupon_Cash_Binding].[CouponCashID]= [Coupon_Cash].[ID] and [Coupon_Cash_Binding].[Status]=1) as [Cost]
from [Coupon_Cash]
	where [Coupon_Cash].IsDelete = 0
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_PaymentInfo_SelectByOrderID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_PaymentInfo_SelectByOrderID
-- Author:	张连印
-- Description:	根据订单编码查询订单支付信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_PaymentInfo_SelectByOrderID]
@OrderID int
As
Begin
	Select 
		OP.[ID] PaymentID,
		O.ID OrderID,
		[PaymentOrgID], 
		isnull([PaymentMoney],0) ActualPaid, --实际已支付金额，若为Null，则为0
		O.TotalMoney ActualMoney, --实际需支付金额
		[IsUseCoupon],
		[IsUseIntegral],
		[IsUseAccount],
		OP.[CreateTime]
	From Order_Payment OP Right Join [Order] O On OP.OrderID = O.ID And O.IsDelete = 0
	Where OP.IsDelete = 0 and OrderID = @OrderID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Payment_SelectByTradeNo]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_Payment_SelectByTradeNo
-- Author:	张连印
-- Description:	根据第三方交易编号查询订单支付信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Payment_SelectByTradeNo]
	@TradeNo varchar(64)
As
Begin
	Select 
	 [ID]
      ,[OrderID]
      ,[PaymentOrgID]
      ,[PaymentMoney]
      ,[IsUseCoupon]
      ,[IsUseIntegral]
      ,[IsUseAccount]
      ,[CreateTime]
      ,[IsDelete]
      ,[TradeNo]
	From [Order_Payment] OP	
	Where
		OP.TradeNo = @TradeNo
	And OP.IsDelete = 0
	Order By ID desc
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Payment_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Order_Payment_Insert]
	 @OrderID Int
	,@PaymentOrgID Int
	,@PaymentMoney Float
	,@TradeNo varchar(64) = default
	,@IsUseCoupon bit
	,@IsUseIntegral bit
	,@IsUseAccount bit
	,@CreateTime datetime
	,@ReferenceID int output
As
Begin
	Insert Into [Order_Payment]
	(
	  [OrderID]
	  ,[PaymentOrgID]
	  ,[PaymentMoney]
	  ,[TradeNo]
	  ,[IsUseCoupon]
	  ,[IsUseIntegral]
	  ,[IsUseAccount]
	  ,[CreateTime])
	Values
		(@OrderID,@PaymentOrgID,@PaymentMoney,@TradeNo,@IsUseCoupon ,@IsUseIntegral,@IsUseAccount,@CreateTime)

	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Cancel_Cause_Select]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Cancel_Cause_Select
-- Author:	张连印
-- Description:	查询所有订单取消原因
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Cancel_Cause_Select]
As
Begin
	Select 
		[ID],
		[Cause],
		[CreateTime]
	From Order_Cancel_Cause
	Where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectByProductCount]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: <Create Date,,>
-- Description:	商品最大编号
-- =============================================
CREATE PROCEDURE [dbo].[sp_Product_SelectByProductCount]
AS
BEGIN
	select top 1 ID from [Product] order by ID desc
END
GO
/****** Object:  View [dbo].[view_UserNoHasOrder]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_UserNoHasOrder]
As
Select u.*,[User_Level].Name As UserLevelName 
From [User] As u
Left Join User_Level On [User_Level].ID = u.UserLevelID
Where u.IsDelete = 0 And User_Level.IsDelete = 0 And u.ID Not In
(Select UserID From [Order] Where [Status] = 7 Group By UserID)
GO
/****** Object:  View [dbo].[view_UserHasOrder]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_UserHasOrder]
as
select u.*,OrderTime,[User_Level].Name as UserLevelName 
From [User] As u
Inner Join (Select MIN(CreateTime) As OrderTime,UserID From [Order] Where [Status] = 7 And IsDelete = 0 Group By UserID) As o on u.ID = o.UserID
Left Join User_Level On [User_Level].ID = u.UserLevelID
Where u.IsDelete = 0 And User_Level.IsDelete = 0
GO
/****** Object:  View [dbo].[view_User_SelectAllInfo]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_User_SelectAllInfo]
AS
Select 
[User].*,
case UserLevelID when 0 then '普通会员' else (select Name from User_Level where User_Level.ID = UserLevelID) end as UserLevelName  
From [User] 
--Left Join User_Level On [User_Level].ID = [User].UserLevelID
Where [User].IsDelete = 0 --And User_Level.IsDelete = 0
GO
/****** Object:  View [dbo].[view_User_Level_Price_SelectAll]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询所有会员等级价格
-- =============================================
CREATE VIEW [dbo].[view_User_Level_Price_SelectAll]
as
select 
		[User_Level_Price].[ID],
	--	[System_User].[ID] as [SystemUserID],
		[User_Level_Price].[EmployeeID],
		[System_Employee].[Name]as[EmployeeName],
		[User_Level_Price].[UserLevelID],
		[User_Level].[Name] as [UserLevelName],
		[User_Level_Price].[ProductID],
		[Product].[Barcode] as [ProductBarcode],
		[Product].[Name] as [ProductName],
		[User_Level_Price].[Price],		
		[User_Level_Price].[Status],
		[User_Level_Price].[CreateTime]		
from [User_Level_Price]
left join [Product] on [User_Level_Price].[productID]=[Product].[ID] and Product.IsDelete = 0
left join [User_Level] on [User_Level_Price].[UserLevelID]=[User_Level].[ID] and [User_Level].IsDelete = 0
left join [System_Employee] on [User_Level_Price].[EmployeeID]=[System_Employee].[ID] and [System_Employee].IsDelete = 0
left join [System_User] on [User_Level_Price].[EmployeeID]=[System_User].[EmployeeID] and [System_User].IsDelete = 0
GO
/****** Object:  View [dbo].[view_User_Collect]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_User_Collect]
AS
SELECT     
	User_CollectRecord.ID,
	User_CollectRecord.UserID, 
	User_CollectRecord.ProductID, 
	User_CollectRecord.CreateTime, 
	User_CollectRecord.IsDelete, 
	Product.Name as ProductName, 
	Product.GoujiuPrice,
	Product.MarketPrice,
	Product.[Status] as ProductStatus,
	Picture.[Path]
FROM  User_CollectRecord 
	Left JOIN Product ON Product.ID = User_CollectRecord.ProductID and Product.IsDelete = 0
	Left Join Product_Picture ON User_CollectRecord.ProductID = Product_Picture.ProductID and IsMaster = 1 and Product_Picture.IsDelete = 0
	Left Join Picture ON Product_Picture.PictureID = Picture.ID and Picture.IsDelete = 0 
where User_CollectRecord.IsDelete = 0
GO
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
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 21
         End
         Begin Table = "User_CollectRecord"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 125
               Right = 402
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_User_Collect'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_User_Collect'
GO
/****** Object:  View [dbo].[view_User_BrowerHistory]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_User_BrowerHistory]
AS
SELECT     
		User_BrowseHistory.ID,
		User_BrowseHistory.UserID,
		User_BrowseHistory.ProductID, 
		User_BrowseHistory.CreateTime, 
		User_BrowseHistory.IsDelete, 
		Product.Name AS ProductName, 
		Product.GoujiuPrice, 
		Product.Status AS ProductStatus,
	Picture.[Path]
FROM  User_BrowseHistory   
Left JOIN  Product  ON Product.ID = User_BrowseHistory.ProductID and Product.IsDelete = 0
	Left Join Product_Picture ON User_BrowseHistory.ProductID = Product_Picture.ProductID and IsMaster = 1 and Product_Picture.IsDelete = 0
	Left Join Picture ON Product_Picture.PictureID = Picture.ID and Picture.IsDelete = 0 
WHERE User_BrowseHistory.IsDelete = 0
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[27] 4[34] 2[13] 3) )"
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
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 21
         End
         Begin Table = "User_BrowseHistory"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 125
               Right = 402
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
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
         Alias = 1560
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_User_BrowerHistory'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_User_BrowerHistory'
GO
/****** Object:  View [dbo].[view_Role_User]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Role_User]
As
Select id as ID, name as Name, 0 as RoleID
From System_Role 
Union
Select id, name, RoleID
From [System_User]
Where RoleID in ( Select RoleID From System_Role )
GO
/****** Object:  View [dbo].[view_Promote_MuchBottled]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Promote_MuchBottled]
AS
SELECT     
	Product.Name AS ProductName,
	Product.GoujiuPrice ,
	Promote_MuchBottled.ID, 
	Promote_MuchBottled.EmployeeID, 
	Promote_MuchBottled.ProductID,
	Promote_MuchBottled.Name, 
	Promote_MuchBottled.StartTime, 
	Promote_MuchBottled.EndTime,
	Promote_MuchBottled.CreateTime
FROM         
	Product right JOIN
	Promote_MuchBottled ON Product.ID = Promote_MuchBottled.ProductID
	where Promote_MuchBottled.IsDelete = 0 and Product.IsDelete = 0
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[25] 4[31] 2[25] 3) )"
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
         Begin Table = "Product"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 220
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Promote_MuchBottled"
            Begin Extent = 
               Top = 6
               Left = 258
               Bottom = 125
               Right = 404
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
      Begin ColumnWidths = 9
         Width = 284
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
         Alias = 2235
         Table = 1740
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_Promote_MuchBottled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_Promote_MuchBottled'
GO
/****** Object:  View [dbo].[view_Promote_Limited_Discount]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Promote_Limited_Discount]
AS
SELECT  Product.Name AS ProductName,
	  Product.GoujiuPrice ,
	  Promote_Limited_Discount.[ID],
	  Promote_Limited_Discount.[ProductID],
	  Promote_Limited_Discount.[Name],
	  Promote_Limited_Discount.[Discount],
	  Promote_Limited_Discount.[DiscountPrice],
	  Promote_Limited_Discount.[TotalQuantity],
	  Promote_Limited_Discount.[LimitedBuyQuantity],
	  Promote_Limited_Discount.[IsOnlinePayment],
	  Promote_Limited_Discount.[IsUseCoupon],
	  Promote_Limited_Discount.[IsNewUser],
	  Promote_Limited_Discount.[IsMobileValidate],
	  Promote_Limited_Discount.[StartTime],
	  Promote_Limited_Discount.[EndTime],
	  Promote_Limited_Discount.[CreateTime]
FROM  Product INNER JOIN
      Promote_Limited_Discount ON Product.ID = Promote_Limited_Discount.ProductID
WHERE Product.IsDelete = 0
GO
/****** Object:  View [dbo].[view_Product_SelectAllInfo]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Product_SelectAllInfo]
AS
--SELECT     p.ID, p.ParentCategoryID, p.ProductCategoryID, p.ParentBrandID, p.ProductBrandID, p.Barcode, p.Name AS ProductName, p.SEOTitle, p.SEOKeywords, 
--                      p.SEODescription, p.Advertisement, p.MarketPrice, p.GoujiuPrice, p.Integral, p.InventoryNumber, p.CommentNumber, p.SoldOfReality, p.SoldOfVirtual, p.PageView, 
--                      p.Sorting, p.Status, p.CreateTime, pb.BrandName, pc.CategoryName, pic.Path, pic.Name, pic.Name AS picName
--FROM         dbo.Product AS p INNER JOIN
--                      dbo.Product_Brand AS pb ON p.ProductBrandID = pb.ID INNER JOIN
--                      dbo.Product_Category AS pc ON p.ProductCategoryID = pc.ID LEFT OUTER JOIN
--                          (SELECT     ppic.ID, ppic.ProductID, ppic.PictureID, ppic.IsMaster
--                            FROM          dbo.Product_Picture AS ppic INNER JOIN
--                                                       (SELECT     MIN(ID) AS proID, ProductID
--                                                         FROM          dbo.Product_Picture
--                                                         WHERE      (IsMaster = 1)
--                                                         GROUP BY ProductID) AS temp ON ppic.ID = temp.proID) AS pp ON p.ID = pp.ProductID LEFT OUTER JOIN
--                      dbo.Picture AS pic ON pp.PictureID = pic.ID
--GROUP BY p.ID, p.ParentCategoryID, p.ProductCategoryID, p.ParentBrandID, p.ProductBrandID, p.Barcode, p.Name, p.SEOTitle, p.SEOKeywords, p.SEODescription, 
--                      p.Advertisement, p.MarketPrice, p.GoujiuPrice, p.Integral, p.InventoryNumber, p.CommentNumber, p.SoldOfReality, p.SoldOfVirtual, p.PageView, p.Sorting, p.Status, 
--                      p.CreateTime, pb.BrandName, pc.CategoryName, pic.Path, pic.Name

SELECT
  p.ID,
  p.ParentCategoryID,
  p.ProductCategoryID,
  p.ParentBrandID,
  p.ProductBrandID,
  p.Barcode,
  p.Name AS ProductName,
  p.SEOTitle,
  p.SEOKeywords,
  p.SEODescription,
  p.Advertisement,
  p.MarketPrice,
  p.GoujiuPrice,
  p.Integral,
  p.InventoryNumber,
  p.CommentNumber,
  p.SoldOfReality,
  p.SoldOfVirtual,
  p.PageView,
  p.Sorting,
  p.Status,
  p.CreateTime,
  pb.BrandName,
  pc.CategoryName,
  pic.Path,
  pic.Name,
  pic.Name AS picName
FROM
  dbo.Product AS p 
  INNER JOIN dbo.Product_Brand AS pb ON p.ProductBrandID = pb.ID 
  INNER JOIN dbo.Product_Category AS pc ON p.ProductCategoryID = pc.ID 
  LEFT OUTER JOIN
  (
    SELECT
      ppic.ID,
      ppic.ProductID,
      ppic.PictureID,
      ppic.IsMaster
    FROM
      dbo.Product_Picture AS ppic INNER JOIN 
      (
        SELECT
          MIN
          (
            ID
          )
          AS proID,
          ProductID
        FROM
          dbo.Product_Picture
        WHERE 
          (
            IsMaster = 1
          )
        GROUP BY
          ProductID
      )
      AS temp ON ppic.ID = temp.proID
  )
  AS pp ON p.ID = pp.ProductID 
  LEFT OUTER JOIN dbo.Picture AS pic ON pp.PictureID = pic.ID
WHERE p.IsDelete = 0
GROUP BY
  p.ID,
  p.ParentCategoryID,
  p.ProductCategoryID,
  p.ParentBrandID,
  p.ProductBrandID,
  p.Barcode,
  p.Name,
  p.SEOTitle,
  p.SEOKeywords,
  p.SEODescription,
  p.Advertisement,
  p.MarketPrice,
  p.GoujiuPrice,
  p.Integral,
  p.InventoryNumber,
  p.CommentNumber,
  p.SoldOfReality,
  p.SoldOfVirtual,
  p.PageView,
  p.Sorting,
  p.Status,
  p.CreateTime,
  pb.BrandName,
  pc.CategoryName,
  pic.Path,
  pic.Name
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[11] 4[52] 2[32] 3) )"
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
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pb"
            Begin Extent = 
               Top = 6
               Left = 249
               Bottom = 114
               Right = 422
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "pc"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pic"
            Begin Extent = 
               Top = 222
               Left = 38
               Bottom = 330
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pp"
            Begin Extent = 
               Top = 6
               Left = 460
               Bottom = 114
               Right = 593
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
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 3465
         Alias = 4305
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
End' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_Product_SelectAllInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_Product_SelectAllInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_Product_SelectAllInfo'
GO
/****** Object:  View [dbo].[view_Product_Paging]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Product_Paging]
As
Select Product.[ID]
      ,Product.[ParentCategoryID]
      ,Product.[ProductCategoryID]
      ,Product.[ParentBrandID]
      ,Product.[ProductBrandID]
      ,Product.[Barcode]
      ,Product.[Name]
      ,Product.[SEOTitle]
      ,Product.[SEOKeywords]
      ,Product.[SEODescription]
      ,Product.[Advertisement]
      ,Product.[MarketPrice]
      ,Product.[GoujiuPrice]
      ,Product.[Introduce]
      ,Product.[Integral]
      ,Product.[InventoryNumber]
      ,Product.[CommentNumber]
      ,Product.[SoldOfReality]
      ,Product.[SoldOfVirtual]
      ,Product.[PageView]
      ,Product.[Sorting]
      ,Product.[Status]
      ,Product.[CreateTime]
      ,Product.[IsDelete]
      ,Picture.[FileName]
      ,Picture.[Path]
      ,Product_Category.CategoryName 
      ,Product_Brand.BrandName
      ,c.BrandName as ParentBrandName
      ,Picture.thumbnailPath
From Product
Left Join Product_Category on Product.ProductCategoryID = Product_Category.ID
Left Join Product_Brand on Product.ProductBrandID = Product_Brand.ID
Left Join Product_Brand as c on Product.ParentBrandID = c.ID
Left Join Product_Picture on Product.ID = Product_Picture.ProductID And Product_Picture.IsMaster = 1
Left Join Picture on Product_Picture.PictureID = Picture.ID
Where Product.IsDelete = 0
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Update_UserMessage]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_User_Update_UserMessage]
	@ID int,
	@CountyID int,
	@Email varchar(64),
	@NickName nvarchar(32),
	@Gender bit,
	@address nvarchar(256),
	@Head text
As
Begin
	Update [User]
	Set
		CountyID=@CountyID,
		Email=@Email,
		NickName=@NickName,
		Gender=@Gender,
		Address=@address,
		Head=@Head
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Update_Status]    Script Date: 03/02/2014 20:58:33 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Update_ResetPassword]    Script Date: 03/02/2014 20:58:33 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Update_LastLoginTime]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_User_Update_LastLoginTime]
	@ID int
As
Begin
	Update [User]
	Set
		[LastLoginTime] = GETDATE()
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Statistics]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Statistics]
AS
BEGIN
SELECT
	--当日客户注册数量
	(SELECT COUNT(1) FROM [User] WHERE [Status]<>1 AND DATEDIFF(dd,CreateTime,GETDATE())=0) AS User_Register_Day,
	--当月客户注册数量
	(SELECT COUNT(1) FROM [User] WHERE [Status]<>1 AND DATEDIFF(month,CreateTime,GETDATE())=0)AS User_Register_Day_Month,
	--当日新客户成交订单
	(SELECT COUNT(1) FROM [Order]
	WHERE [Order].[UserID] IN (SELECT [User].[ID] FROM [User] WHERE [User].[Status]<>1 AND DATEDIFF(dd,[User].[CreateTime],GETDATE())=0)
	AND [Order].[Status] NOT IN (2,3,13)
	) AS NewUser_Order_Day,
	--当月新客户成交订单
	(SELECT COUNT(1) FROM [Order]
	WHERE [Order].[UserID] IN (SELECT [User].[ID] FROM [User] WHERE [User].[Status]<>1 AND DATEDIFF(month,[User].[CreateTime],GETDATE())=0)
	AND [Order].[Status] NOT IN (2,3,13)
	) AS NewUser_Order_Month,
	--当日客户成交数量
	(SELECT count(1) FROM [Order] 
	left join [User] on([User].[ID]=[Order].[UserID])
	where [Order].[Status] not in (2,3,13) AND DATEDIFF(dd,[Order].[CreateTime],GETDATE())=0
	) AS AllUser_Order_Day,
	--当月客户成交数量
	(SELECT count(1) FROM [Order] 
	left join [User] on([User].[ID]=[Order].[UserID])
	where [Order].[Status] not in (2,3,13) AND DATEDIFF(month,[Order].[CreateTime],GETDATE())=0
	) AS AllUser_Order_Month
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectRow]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  冯瑶  
-- Create date: 2013-12-28 09:32:23  
-- Description: 查询指定的会员信息  
-- =============================================  
CREATE Procedure [dbo].[sp_User_SelectRow]  
 @ID int  
As  
Begin  
 Select   
  [User].[ID],  
  [User].[CpsID],  
  [UserLevelID],  
  [User_Level].[Name] as [UserLevelName],  
  [CountyID],  
  [Address],  
  [Email],  
  [EmailValidate],  
  [Mobile],  
  [MobileValidate],  
  [User].[Name],  
  [Age],  
  [Gender],  
  [LoginName],  
  [LoginPassword],  
  [NickName],  
  [Birthday],  
  [head],  
  [QQ],  
  [MSN],  
  [OpenID],  
  [User].[Status],  
  [User].[CreateTime],  
  [LastLoginTime],  
  [Balance],  
  (select count(ID) from [Order] where [Order].[UserID]=@ID and IsDelete = 0 and Status = 3)as [OrderCount],  
  (select count(ID) from [Order] where [Order].[UserID]=@ID and IsDelete = 0 and Status in (100,0,1,2))as [UnFinishedCount],  
  (select SUM(TotalMoney) from [Order] where [Order].[UserID]=@ID and IsDelete = 0 and Status in(100,0,1,2,3))as [TotalExpenses],  
  (select COUNT(cb.ID) from [Coupon_Cash_Binding] as cb left join [Coupon_Cash] as c on cb.CouponCashID = c.ID where cb.UserID =@ID and cb.IsDelete =0 and cb.[Status] =0 and c.EndTime > GETDATE())  
  +(select COUNT(db.ID) from [Coupon_Decrease_Binding] db left join [Coupon_Decrease] d on db.CouponDecreaseID = d.ID where db.UserID =@ID and db.IsDelete =0 and db.[Status]=0 and d.EndTime > GETDATE()) as CouponCount  
 From [User]  
 left join [User_Level] on [User].[UserLevelID]=[User_Level].[ID] and User_Level.IsDelete=0  
 left join [dbo].[User_Account] on [User].ID=[dbo].[User_Account].[UserID]and [User_Account].IsDelete=0  
 Where  
  [User].[ID] = @ID  
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectLogin]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,冯瑶>
-- Create date: <Create Date,2013-10-15,>
-- Description:	<Description,查询所有用户信息,>
-- =============================================
CREATE Procedure [dbo].[sp_User_SelectLogin]
	@Login varchar(100)
As
Begin
	Select 
		[ID],		
		[Email],		
		[Mobile],		
		[Name],
		[LoginName],
		[LoginPassword],
		[NickName]		
	From [User]
	where IsDelete = 0 
	and( [Email] = @Login 
	or [LoginName] = @Login 
	or [Mobile] = @Login)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectByOpenID]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,冯瑶>
-- Create date: <Create Date,2013-10-15,>
-- Description:	<Description,查询所有用户信息,>
-- =============================================
Create Procedure [dbo].[sp_User_SelectByOpenID]
	@OpenID varchar(100)
As
Begin
	Select 
		[ID],		
		[Email],		
		[Mobile],		
		[Name],
		[LoginName],
		[LoginPassword],
		[NickName]		
	From [User]
	where IsDelete = 0 and OpenID = @OpenID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectByMobileOrEmail]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_User_SelectByMobileOrEmail
-- Author:	张连印
-- Description:	根据手机或者邮箱查询用户信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_User_SelectByMobileOrEmail]
	@searchStr varchar(64)
As
Begin
	Select 
		[ID],
		[CpsID],
		[UserLevelID],
		[CountyID],
		[Email],
		[EmailValidate],
		[Mobile],
		[MobileValidate],
		[Name],
		[Age],
		[Gender],
		[Address],
		[LoginName],
		[LoginPassword],
		[Integral],
		[Head],
		[NickName],
		[Birthday],
		[QQ],
		[MSN],
		[OpenID],
		[Status],
		[CreateTime],
		[LastLoginTime]
	From [User]
	Where IsDelete = 0 And (Mobile =@searchStr or Email = @searchStr);		
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectByLevelID]    Script Date: 03/02/2014 20:58:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_SelectByLevelID]
	@UserLevelID int
AS
BEGIN
	Select [User].[ID] From [User] Where IsDelete=0 And UserLevelID=@UserLevelID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_SelectAll]    Script Date: 03/02/2014 20:58:33 ******/
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
	where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_RecieveAddressSecond_Update]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================  
-- Name: sp_User_RecieveAddress_Update  
-- Author: 张连印  
-- Description: 根据编码更新用户收货地址  
-- ==========================================================================================  
CREATE Procedure [dbo].[sp_User_RecieveAddressSecond_Update]  
 @ID int,  
 @CountyID int,  
 @Address nvarchar(128),  
 @Consignee nvarchar(16),  
 @Mobile varchar(16),  
 @Tel varchar(16) = default,
 @Email varchar(256),
 @ZipCode varchar(50),
 @isDefault bit=0
As  
Begin  
if @IsDefault=1
 begin
	Update User_RecieveAddress set isDefault=0 where UserId = (Select UserID from User_RecieveAddress where ID =@ID)
 end
 Update User_RecieveAddress  
 Set  
  [CountyID] = @CountyID,  
  [Address] = @Address,  
  [Consignee] = @Consignee,  
  [Mobile] = @Mobile,  
  [Tel] = @Tel,
  [Email]=@Email,
  [ZipCode]=@ZipCode,
  IsDefault=1
 Where    
  [ID] = @ID  
  
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_RecieveAddress_Update]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================    
-- Name: sp_User_RecieveAddress_Update    
-- Author: 张连印    
-- Description: 根据编码更新用户收货地址    
-- ==========================================================================================    
CREATE Procedure [dbo].[sp_User_RecieveAddress_Update]    
 @ID int,    
 @CountyID int,    
 @Address nvarchar(128),    
 @Consignee nvarchar(16),    
 @Mobile varchar(16)=default,    
 @Tel varchar(16) = default,  
 @Email varchar(256)=default,  
 @ZipCode varchar(50)=default,
 @IsDefault bit=0
As    
Begin    

 if @IsDefault=1
 begin
	Update User_RecieveAddress set IsDefault=0 where UserId = (Select UserID from User_RecieveAddress where ID =@ID)
 end

 Update User_RecieveAddress    
 Set    
  [CountyID] = @CountyID,    
  [Address] = @Address,    
  [Consignee] = @Consignee,    
  [Mobile] = @Mobile,    
  [Tel] = @Tel,  
  [Email]=@Email,  
  [ZipCode]=@ZipCode
 Where      
  [ID] = @ID      
    
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_RecieveAddress_SetDefault]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_User_RecieveAddress_SetDefault
-- Author:	张连印
-- Description:	根据编码逻辑删除用户收货地址
-- ==========================================================================================
Create Procedure [dbo].[sp_User_RecieveAddress_SetDefault]
	@ID int,
	@UserID int
As
Begin
	update User_RecieveAddress
	Set [IsDefault] = 0
	Where UserID = @UserID;
	
	Update User_RecieveAddress
	Set
		[IsDefault]= 1
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_RecieveAddress_SelectByUserID]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_User_RecieveAddress_SelectByUserID
-- Author:	张连印
-- Description:	根据会员编码查询对应的收货信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_User_RecieveAddress_SelectByUserID]
@UserID int
As
Begin
	Select 
	   [ID]
      ,[UserID]
      ,[CountyID]
      ,[Address]
      ,[Consignee]
      ,[Mobile]
      ,[Tel]
      ,[IsDefault]
      ,[CreateTime]
	From [User_RecieveAddress]
	Where IsDelete = 0 And UserID = @UserID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_RecieveAddress_SelectByID]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_User_RecieveAddress_SelectByID
-- Author:	张连印
-- Description:	根据地址编码查询用户收货地址信息
-- ==========================================================================================
CREATE Procedure [dbo].[sp_User_RecieveAddress_SelectByID]
	@ID int
As
Begin
	Select 
		U.[ID] ID,
		[UserID],
		[CountyID],
		[Address],
		[PostCode],
		[Consignee],
		[Mobile],
		[Tel],
		[IsDefault],
		[CreateTime],
		[Email],
		[ZipCode] 
	From User_RecieveAddress U Left Join County C On U.CountyID = C.ID
	Where
		U.[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_RecieveAddress_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================  
-- Name: sp_User_RecieveAddress_Insert  
-- Author: 张连印  
-- Description: 插入一条用户收货信息  
-- ==========================================================================================  
CREATE Procedure [dbo].[sp_User_RecieveAddress_Insert]  
 @UserID int,  
 @CountyID int,  
 @Address nvarchar(128),  
 @Consignee nvarchar(16),  
 @Mobile varchar(16)=default,    
 @Tel varchar(16) = default,  
 @Email varchar(256)=default,  
 @ZipCode varchar(50)=default,  
 @IsDefault bit=0,  
 @CreateTime datetime,  
 @ReferenceID int output  
As  
Begin

 if @IsDefault=1
 begin
	Update User_RecieveAddress set IsDefault=0 where UserId = @UserID
 end

 Insert Into User_RecieveAddress  
  ([UserID],[CountyID],[Address],[Consignee],[Mobile],[Tel],[IsDefault],[CreateTime])  
 Values  
  (@UserID,@CountyID,@Address,@Consignee,@Mobile,@Tel,@IsDefault,@CreateTime)  
  
 Select @ReferenceID = @@IDENTITY  
  
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_RecieveAddress_DeleteByID]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_User_RecieveAddress_DeleteByID
-- Author:	张连印
-- Description:	根据编码逻辑删除用户收货地址
-- ==========================================================================================
Create Procedure [dbo].[sp_User_RecieveAddress_DeleteByID]
	@ID int
As
Begin
	Update User_RecieveAddress
	Set
		[IsDelete]= 255
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Price_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Level_Price_Insert]
	@EmployeeID int,
	@UserLevelID int,
	@ProductBarcode varchar(100),
	@Price float,
	@Status int,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin	
	DECLARE @ProductID int
	SELECT @ProductID =(SELECT TOP 1 [Product].[ID] FROM [Product] WHERE [Product].[Barcode]=@ProductBarcode)
	
	Insert Into User_Level_Price
		([EmployeeID],[UserLevelID],[ProductID],[Price],[Status],[CreateTime])
	Values
		(@EmployeeID,@UserLevelID,@ProductID,@Price,@Status,@CreateTime)

	Select @ReferenceID = @@IDENTITY


End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Price_DeleteRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	逻辑删除会员等级价格
-- =============================================
CREATE Procedure [dbo].[sp_User_Level_Price_DeleteRow]
	@ID int
As
Begin
	Update [User_Level_Price] set [IsDelete] = 255 
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Insert]    Script Date: 03/02/2014 20:58:32 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Level_DeleteRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	逻辑删除会员等级
-- =============================================
CREATE Procedure [dbo].[sp_User_Level_DeleteRow]
	@ID int
As
Begin
	Update [User_Level] set [IsDelete] = 255
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_User_Insert]
	@CpsID int = null,
	@UserLevelID int = null,
	@CountyID int = null,
	@Address nvarchar(200)= null,
	@Email varchar(100)= null,
	@EmailValidate bit= null,
	@Mobile varchar(20)= null,
	@MobileValidate bit= null,
	@Name nvarchar(20)= null,
	@Age int= null,
	@Gender bit= null,
	@LoginName varchar(50)= null,
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
/****** Object:  StoredProcedure [dbo].[sp_User_Exists_Mobile]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Exists_Mobile]
	@Mobile nvarchar(50)
As
Begin
	Select 		
		Mobile
	From [User]
	Where IsDelete = 0 And Mobile = @Mobile
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Exists_LoginName]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Exists_LoginName]
	@LoginName nvarchar(50)
As
Begin
	Select 		
		LoginName
	From [User]
	Where IsDelete = 0 And LoginName = @LoginName
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Exists_Email]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_User_Exists_Email]
	@Email nvarchar(50)
As
Begin
	Select 		
		Email
	From [User]
	Where IsDelete = 0 And Email = @Email
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_CollectRecord_SelectRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_CollectRecord_SelectRow]
	@UserID int,
	@ProductID int
AS
BEGIN
	select ID,UserID,ProductID From User_CollectRecord Where UserID=@UserID and ProductID =@ProductID and IsDelete =0
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_CollectRecord_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_CollectRecord_Insert] 
	@UserID int,
	@ProductID int,
	@ReferenceID int out
AS
BEGIN
	Insert Into User_CollectRecord
		([UserID],[ProductID])
	Values
		(@UserID,@ProductID)
		
	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_CollectRecord_DeleteRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create Procedure [dbo].[sp_User_CollectRecord_DeleteRow]
	@ID int
As
Begin
	Update User_CollectRecord
	Set IsDelete =255
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_CollectRecord_DeleteBatch]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_CollectRecord_DeleteBatch]
	@IDItem varchar(100)
AS
BEGIN
Update User_CollectRecord Set IsDelete =255 where ID in (@IDItem)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_BrowseHistory_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_User_BrowseHistory_Insert]
	@UserID int,
	@ProductID int,
	@ReferenceID int out
As
Begin
	IF EXISTS (SELECT * FROM User_BrowseHistory WHERE UserID = @UserID AND ProductID = @ProductID and IsDelete = 0)
		Begin
		  Update User_BrowseHistory set CreateTime = GETDATE() where UserID = @UserID AND ProductID = @ProductID
		  
		  Select @ReferenceID =ID FROM User_BrowseHistory WHERE UserID = @UserID AND ProductID = @ProductID

		  Return @ReferenceID
		End
	ELSE
		Begin
		print 1
			Insert Into User_BrowseHistory
				([UserID],[ProductID])
			Values
				(@UserID,@ProductID)

			Select @ReferenceID = @@IDENTITY

			Return @ReferenceID
		End
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_BrowseHistory_DeleteBatch]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_BrowseHistory_DeleteBatch]
	@IDItem varchar(100)
AS
BEGIN
Update User_BrowseHistory Set IsDelete =255 where ID in (@IDItem)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Account_UpdateBalance]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_User_Account_UpdateBalance
-- Author:	张连印
-- Description:	虚拟账户余额更新操作
-- ==========================================================================================
CREATE Procedure [dbo].[sp_User_Account_UpdateBalance]
	@ID int,
	@OperateType int, --操作类型：1-存入，2-支出，3-退款
	@Money float	  --更新的金额，必须为不小于0的正数
As
Begin
	if(@Money<0)
		RAISERROR ('金额不能为负数' , 16, 1) WITH NOWAIT
		
	Begin TRAN
	Begin Try
		if(@OperateType =2) --若是支出，则转换为负数
			set @Money = - @Money;
			
		Update User_Account
		Set
			[Balance] =ISNULL(Balance,0) + @Money
		Where		
			[ID] = @ID
			
		Insert Into User_Account_Details
			([UserAccountID],[Money],[OperateType],[CreateTime])
		Values
			(@ID,@Money,@OperateType,GETDATE())
		
		Commit TRAN
	 End Try
	 Begin Catch
		declare @errmsg nvarchar(1024)
		select @errmsg = ERROR_MESSAGE() 
		IF @@TRANCOUNT > 0---------------判断有没有事务
		BEGIN
			ROLLBACK TRAN----------回滚事务
		END 
		RAISERROR (@errmsg , 16, 1) WITH NOWAIT 
	 End catch	
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Account_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <冯瑶>  
-- Create date: 2013/12/23 13:28:34  
-- Description: 添加会员账户余额信息  
-- =============================================  
CREATE Procedure [dbo].[sp_User_Account_Insert]  
 @UserID int,  
 @Balance float,  
 @Status int,  
 @CreateTime datetime,  
 @ReferenceID int out   
As  
Begin
 
if EXISTS(Select * From [User_Account] where Userid = @UserID)
begin
	RAISERROR ('会员虚拟账户已经存在，执行存储过程：sp_User_Account_Insert' , 16, 1) WITH NOWAIT
end

 Insert Into User_Account  
  ([UserID],[Balance],[Status],[CreateTime])  
 Values  
  (@UserID,@Balance,@Status,@CreateTime)  
  
 Select @ReferenceID = @@IDENTITY  
  
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Account_Exists_UserID]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<冯瑶>
-- Create date: 2013/12/23 13:28:34
-- Description:	检查是否存在会员账户余额信息
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_Account_Exists_UserID]
	@UserID int
As
Begin
	Select 
		[ID],
		[UserID],
		[Balance],
		[Status],
		[CreateTime]
	From User_Account
	Where IsDelete = 0 And [UserID] = @UserID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_TreelandingPage_SelectAll]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_TreelandingPage_SelectAll]
as
select distinct 'year_'+CONVERT(varchar(50), YEAR(createtime)) ID,'0' as PID,CONVERT(varchar(50), YEAR(CreateTime)) as Name 
from Promote_LandingPage
where IsDelete = 0

union

select distinct 'month_'+CONVERT(varchar(50), YEAR(createtime))+'-'+Convert(varchar(50),MONTH(CreateTime)) ID,'year_'+CONVERT(varchar(50), YEAR(CreateTime)) as PID,CONVERT(varchar(50), YEAR(createtime))+'-'+Convert(varchar(50),MONTH(CreateTime)) as Name 
from Promote_LandingPage
where IsDelete = 0

union

select CONVERT(varchar(50), ID),'month_'+CONVERT(varchar(50), YEAR(createtime))+'-'+Convert(varchar(50),MONTH(CreateTime)) as PID, Name 
from Promote_LandingPage
where IsDelete = 0
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Visitor_Update]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_System_Visitor_Update]
  @SessionID nvarchar(200)
  as
  begin
  update System_Visitor set EndTime=GETDATE() where SessionID=@SessionID
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_System_visitor_InsertUser]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_System_visitor_InsertUser]
  @SessionID nvarchar(200),
  @UserName nvarchar(50),
  @IP4Address nvarchar(50),
  @EndTime Datetime,
  @ReferenceID int out
  as
  begin
  insert into System_Visitor(StartTime,SessionID,UserName,IP4Address,EndTime,IsDelete) values(GETDATE(),@SessionID,@UserName,@IP4Address,@EndTime,0)
  select @ReferenceID= @@IDENTITY
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Visitor_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Visitor_Insert]
As
Begin
	declare @i int, @count int
	declare @bgt datetime,@edt datetime,@bgt_tmp datetime,@edt_tmp datetime
	declare @name varchar(50)
	set @i=1
	set @count = 100*RAND()
	set @bgt = dateadd(SECOND,rand()*3600*2,GETDATE())
	set @edt = dateadd(SECOND,rand()*3600*2,@bgt)
	while @i<=@count
	begin
	set @bgt_tmp = dateadd(SECOND,convert(int,(rand()*datediff(SECOND,@bgt,@edt))),@bgt)
	set @edt_tmp = dateadd(SECOND,360*rand(),@bgt_tmp)
	select @name = name from [system_user] order by NEWID()
	insert into [System_Visitor](Starttime,[EndTime],UserName,[SessionID])
	values (@bgt_tmp,@edt_tmp,@name,NEWID())
	set @i=@i+1
	end
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Visitor_Count]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_System_Visitor_Count]
@starttime datetime,
@endtime datetime,
@condition nvarchar(10)
As

If @condition='day'
Begin
	Select (Datepart(Hour,starttime) * 60 + Datepart(Minute,starttime))/15, Count(*) visitorCount
	From System_Visitor 
	Where starttime >= @starttime And starttime < @endtime
	Group By (Datepart(Hour,starttime) * 60 + Datepart(Minute,starttime))/15
	Order By (Datepart(Hour,starttime) * 60 + Datepart(Minute,starttime))/15
	--select  (datepart(DAY,starttime)-datepart(DAY,@starttime ))*24 + datepart(HOUR,starttime) departDate ,COUNT(*) visitorCount
	--from System_Visitor 
	--where starttime >= @starttime and starttime < @endtime
	--group by  (datepart(DAY,starttime)-datepart(DAY,@starttime ))*24 + datepart(HOUR,starttime)
	--order by  (datepart(DAY,starttime)-datepart(DAY,@starttime ))*24 + datepart(HOUR,starttime)
End

If @condition='month'
Begin
	Select (Day(starttime)*24+Datepart(Hour,starttime))/12 departDate, Count(*) visitorCount
	From System_Visitor 
	Where starttime > @starttime And starttime < @endtime
	Group by (Day(starttime)*24+Datepart(Hour,starttime))/12
	Order by (Day(starttime)*24+Datepart(Hour,starttime))/12
	--select  (datepart(MONTH,starttime)-datepart(MONTH,@starttime ))*30 + datepart(DAY,starttime) departDate ,COUNT(*) visitorCount
	--from System_Visitor 
	--where starttime >= @starttime and starttime < @endtime
	--group by  (datepart(MONTH,starttime)-datepart(MONTH,@starttime ))*30 + datepart(DAY,starttime)
	--order by  (datepart(MONTH,starttime)-datepart(MONTH,@starttime ))*30 + datepart(DAY,starttime)
End

If @condition='season'
Begin
	Select (Month(starttime)*31+Day(starttime))/2 departDate,Count(*) visitorCount
	From System_Visitor 
	Where starttime >= @starttime And starttime <= @endtime
	Group by (Month(starttime)*31+Day(starttime))/2
	Order by (Month(starttime)*31+Day(starttime))/2
End

If @condition='year'
Begin
	Select (Month(starttime)*31+Day(starttime))/8 departDate,Count(*) visitorCount
	From System_Visitor 
	Where starttime >= @starttime And starttime <= @endtime
	Group by (Month(starttime)*31+Day(starttime))/8
	Order by (Month(starttime)*31+Day(starttime))/8
	--select  (datepart(YEAR,starttime)-datepart(YEAR,@starttime ))*360 + datepart(MONTH,starttime) departDate ,COUNT(*) visitorCount
	--from System_Visitor 
	--where starttime >= @starttime and starttime < @endtime
	--group by  (datepart(YEAR,starttime)-datepart(YEAR,@starttime ))*360 + datepart(MONTH,starttime)
	--order by  (datepart(YEAR,starttime)-datepart(YEAR,@starttime ))*360 + datepart(MONTH,starttime)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_Update_Password]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_User_Update_Password]
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
/****** Object:  StoredProcedure [dbo].[sp_System_User_Update]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_User_Update]
	@ID int,
	@EmployeeID int,
	@RoleID int,
	@Name varchar(50),
	@LoginName varchar(50),
	@Status int
As
Begin
	Update [System_User]
	Set
		[EmployeeID] = @EmployeeID,
		[RoleID] = @RoleID,
		[LoginName] = @LoginName,
		[Name] = @Name,
		[Status] = @Status
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_SelectRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_User_SelectRow]
	@LoginName varchar(50)
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[RoleID],
		[LoginName],
		[LoginPassword],
		[Name],
		[Status],
		[CreateTime]
	From [System_User]
	Where [IsDelete] = 0 And [Status] = 1 And [LoginName] = @LoginName
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_SelectByRoleID]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_User_SelectByRoleID]
	@RoleID int
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name]
	From [System_User]
	Where IsDelete = 0 And [RoleID] = @RoleID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_User_Insert]
	@EmployeeID int,
	@RoleID	int,
	@LoginName varchar(50),
	@LoginPassword varchar(50),
	@Name nvarchar(50),
	@Status int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into [System_User]
		([EmployeeID],[RoleID],[LoginName],[LoginPassword],[Name],[Status],[CreateTime])
	Values
		(@EmployeeID,@RoleID,@LoginName,@LoginPassword,@Name,@Status,@CreateTime)

	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_Exists_LoginName]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_User_Exists_LoginName]
	@LoginName nvarchar(50)
As
Begin
	Select 		
		LoginName
	From [System_User]
	Where IsDelete = 0 And LoginName = @LoginName
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_User_DeleteRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_User_DeleteRow]
	@ID int
As
Begin
	--Delete [System_User]
	--Where
	--	[ID] = @ID
	Update [System_User] Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Role_SelectAllWithUser]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Role_SelectAllWithUser]
As
Begin
	Select 
		[ID],
		[Name],
		0 As PID,
		1 As [Type]
	From System_Role Where [IsDelete] = 0
	Union
	Select
		[ID],
		[Name],
		[RoleID],
		0 AS [Type]
	From [System_User]
	Where [IsDelete] =0 And [RoleID] in (Select R.ID From System_Role R)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Role_SelectAll]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Role_SelectAll]
As
Begin
	Select 
		[ID],
		[Headcount],
		[Name],
		[CreateTime]
	From System_Role
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Role_Permission_SelectByRoleID]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Role_Permission_SelectByRoleID]
	@RoleID int
As
Begin
	Select 
		[ID],
		[RoleID],
		[PermissionID]
	From System_Role_Permission
	Where [IsDelete] = 0 And [RoleID] = @RoleID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Role_Permission_Insert]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Role_Permission_Insert]
	@RoleID int,
	@PermissionID int
As
Begin
	Insert Into System_Role_Permission
		([RoleID],[PermissionID])
	Values
		(@RoleID,@PermissionID)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Role_Permission_DeleteRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Role_Permission_DeleteRow]
	@RoleID int
As
Begin
	--Delete System_Role_Permission
	--Where
	--	[RoleID] = @RoleID
 	Update System_Role_Permission Set IsDelete = 255 Where RoleID = @RoleID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Role_Insert]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Role_Insert]
	@Name nvarchar(50),
	@Headcount int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into System_Role
		([Name],[Headcount],[CreateTime])
	Values
		(@Name,@Headcount,@CreateTime)

	Select @ReferenceID = @@IDENTITY
	
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Role_DeleteRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Role_DeleteRow]
	@ID int
As
Begin
	--Delete System_Role
	--Where
	--	[ID] = @ID
	Update System_Role Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Rights_Update]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Rights_Update]
	@UserID int,
	@RoleID int,
	@UserRights varchar(8000)
As
Begin
	Update System_Rights
	Set
		[UserRights] = @UserRights
	Where
		[RoleID] = @RoleID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Rights_SelectByUserID]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_System_Rights_SelectByUserID
-- Author:	张连印
-- Create date:	2013/12/24 19:41:50
-- Description:	This stored procedure is intended for selecting a specific row from System_Rights table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_System_Rights_SelectByUserID]
	@UserID int
As
Begin
	Select 
		[ID],
		[RoleID],
		[UserID],
		[UserRights]
	From System_Rights
	Where [IsDelete] = 0 And ([UserID] = @UserID OR [RoleID] in (select RoleID from [System_User] where [ID] = @UserID))
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Rights_SelectByRoleID]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Rights_SelectByRoleID]
	@RoleID int
As
Begin
	Select 
		[ID],
		[RoleID],
		[UserID],
		[UserRights]
	From System_Rights
	Where [IsDelete] = 0 And [RoleID] = @RoleID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Rights_Insert]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Rights_Insert]
	@UserID int,
	@RoleID int,
	@UserRights varchar(8000),
	@ReferenceID int output
As
Begin
	Insert Into System_Rights
		([UserID],[RoleID],[UserRights])
	Values
		(@UserID,@RoleID,@UserRights)
	
	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Rights_Exists]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Rights_Exists]
	@RoleID int
As
Begin
   Select Count(*) From System_Rights Where IsDelete = 0 And [RoleID] = @RoleID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Resources_SelectAll]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_System_Resources_SelectAll
-- Author:	张连印
-- Create date:	2013/12/24 19:40:34
-- Description:	This stored procedure is intended for selecting all rows from System_Resources table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_System_Resources_SelectAll]
As
Begin
	Select 
		[ID],
		[Code],
		[ParentCode],
		[Key],
		[Description],
		[Position]
	From System_Resources Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Permission_Update]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Permission_Update]
	@ID int,
	@ParentID int,
	@Name nvarchar(50),
	@Layer int
As
Begin
	Update System_Permission
	Set
		[ParentID] = @ParentID,
		[Name] = @Name,
		[Layer] = @Layer
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Permission_SelectAllByRoleID]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Permission_SelectAllByRoleID]
@RoleID int
As
Begin
	Select 
		System_Permission.[ID],
		System_Permission.[ParentID],
		System_Permission.[Name],
		System_Permission.[Layer],
		System_Permission.[CreateTime]
	From System_Permission Inner join System_Role_Permission on System_Permission.ID = System_Role_Permission.PermissionID
	Where System_Permission.[IsDelete] = 0 And System_Role_Permission.RoleID = @RoleID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Permission_SelectAll]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Permission_SelectAll]
As
Begin
	Select 
		[ID],
		[ParentID],
		[Name],
		[Layer],
		[CreateTime]
	From System_Permission
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Permission_Insert]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_System_Permission_Insert]
	@ParentID int,
	@Name nvarchar(50),
	@Layer int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into System_Permission
		([ParentID],[Name],[Layer],[CreateTime])
	Values
		(@ParentID,@Name,@Layer,@CreateTime)

	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Permission_HasChildren]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Permission_HasChildren]
	@ID int,
	@ChildrenCount int output
As
Begin
	Select @ChildrenCount = COUNT(*) From System_Permission Where IsDelete = 0 And ParentID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Permission_DeleteRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Permission_DeleteRow]
	@ID int
As
Begin
	--Delete System_Permission Where ParentID in (Select ID From System_Permission Where ParentID = @ID)
	--Delete System_Permission Where [ID] = @ID Or ParentID = @ID
	
	Update System_Permission Set IsDelete = 255 Where [ID] = @ID Or ParentID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Menu_Update]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_System_Menu_Update]
	@ID int,
	@PermissionID int,
	@ParentID int,
	@Layer int,
	@Name nvarchar(20),
	@URL varchar(100),
	@Sorting int
As
Begin
	Update System_Menu
	Set
		[PermissionID] = @PermissionID,
		[ParentID] = @ParentID,
		[Layer] = @Layer,
		[Name] = @Name,
		[URL] = @URL,
		[Sorting] = @Sorting
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Menu_SelectByRoleID]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Menu_SelectByRoleID]
	@RoleID int
As
Begin
	Select 
		System_Menu.[ID],
		System_Menu.[PermissionID],
		System_Menu.[ParentID],
		System_Menu.[Layer],
		System_Menu.[Name],
		System_Menu.[URL],
		System_Menu.[Sorting],
		System_Menu.[CreateTime]
	From System_Menu Inner Join System_Role_Permission On System_Menu.PermissionID = System_Role_Permission.PermissionID
	Where System_Menu.[IsDelete] = 0 And System_Role_Permission.RoleID = @RoleID
	Order by System_Menu.[Sorting]
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Menu_SelectAll]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Menu_SelectAll]
As
Begin
	Select 
		[ID],
		[PermissionID],
		[ParentID],
		[Layer],
		[Name],
		[URL],
		[Sorting],
		[CreateTime]
	From [System_Menu]
	Where [IsDelete] = 0
	Order by System_Menu.[Sorting]
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Menu_Insert]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_System_Menu_Insert]
	@PermissionID int,
	@ParentID int,
	@Layer int,
	@Name nvarchar(20),
	@URL varchar(100),
	@Sorting int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into System_Menu
		([PermissionID],[ParentID],[Layer],[Name],[URL],[Sorting],[CreateTime])
	Values
		(@PermissionID,@ParentID,@Layer,@Name,@URL,@Sorting,@CreateTime)
		
	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Menu_DeleteRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Menu_DeleteRow]
	@ID int
As
Begin
	--Delete System_Menu
	--Where
	--	[ID] = @ID or ParentID = @ID
	Update System_Menu Set IsDelete = 255 Where [ID] = @ID Or [ParentID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Log_SelectRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Log_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[SessionID],
		[UserID],
		[Message],
		[Action],
		[Level],
		[Location]
	From [System_Log]
	Where [IsDelete] = 0 And [ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Log_Insert]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Log_Insert]
	@SessionID varchar(200),
	@UserID int,
	@Message varchar(8000),
	@Action varchar(200),
	@Level int,
	@Location varchar(200)
As
Begin
	Declare @UserName varchar(100);
	Select Top 1 @UserName = Name From [System_User] Where ID = @UserID;
	
	Insert Into System_Log
		([SessionID],[UserID],[Message],[Action],[Level],[Location],[UserName])
	Values
		(@SessionID,@UserID,@Message,@Action,@Level,@Location,@UserName)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Log_DeleteRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Log_DeleteRow]
	@ID int
As
Begin
 	Update System_Log Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_Update]    Script Date: 03/02/2014 20:58:31 ******/
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
	@Status int
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
		[Status] = @Status
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_SelectRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_System_Employee_SelectRow
-- Author:	张连印
-- Create date:	2013/12/11 11:02:33
-- Description:	This stored procedure is intended for selecting a specific row from System_Employee table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_System_Employee_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[DepartmentID],
		[CountyID],
		[Name],
		[IdentityCard],
		[IdentityCardAddress],
		[BankCard],
		[Age],
		[Gender],
		[Mobile],
		[HomeAddress],
		[Status],
		[CreateTime]
	From System_Employee
	Where [IsDelete] = 0 And [ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_SelectAll]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Employee_SelectAll]
As
Begin
	Select 
		[ID],
		[Name]
	From System_Employee
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_Insert]    Script Date: 03/02/2014 20:58:31 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_System_Employee_DeleteRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Employee_DeleteRow]
	@ID int
As
Begin
	--Delete System_Employee
	--Where
	--	[ID] = @ID
	Update System_Employee Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_Update_Headcount]    Script Date: 03/02/2014 20:58:31 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_System_Department_Update]    Script Date: 03/02/2014 20:58:31 ******/
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
	@Description ntext
As
Begin
	Update System_Department
	Set
		[Name] = @Name,
		[Headcount] = @Headcount,
		[Principal] = @Principal,
		[PrincipalMobile] = @PrincipalMobile,
		[Description] = @Description
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_SelectRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Department_SelectRow]
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
	Where [IsDelete] = 0 And [ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_SelectAll]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Department_SelectAll]
As
Begin

	UPDATE [System_Department] SET [Headcount] = (SELECT COUNT(1) FROM System_Employee WHERE DepartmentID = [System_Department].ID)

	Select 
		[ID],
		[Name],
		[Headcount],
		[Principal],
		[PrincipalMobile],
		[Description],
		[CreateTime]
	From System_Department
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_System_Department_Insert]    Script Date: 03/02/2014 20:58:31 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_System_Department_DeleteRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_System_Department_DeleteRow]
	@ID int
As
Begin
	--Delete System_Department
	--Where
	--	[ID] = @ID
	Update System_Department Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Invoice_Update]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_Invoice_Update
-- Author:	张连印
-- Description:	更新一条订单发票信息
-- ==========================================================================================
Create Procedure [dbo].[sp_Order_Invoice_Update]
	@ID int,
	@OrderID int,
	@InvoiceTypeID int,
	@InvoiceContentID int,
	@InvoiceTitle nvarchar(64),
	@InvoiceCost float
As
Begin
	Update Order_Invoice
	Set
		[OrderID] = @OrderID,
		[InvoiceTypeID] = @InvoiceTypeID,
		[InvoiceContentID] = @InvoiceContentID,
		[InvoiceTitle] = @InvoiceTitle,
		[InvoiceCost] = @InvoiceCost
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Invoice_SelectByOrderID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Invoice_SelectByOrderID
-- Author:	张连印
-- Description:	根据订单编码获取订单发票
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Invoice_SelectByOrderID]
	@OrderID int
As
Begin
	Select 
		OI.[ID],
		[OrderID],
		[InvoiceTypeID],
		CIT.[Name] InvoiceTypeName,
		[InvoiceContentID],
		[InvoiceTitle],
		[InvoiceCost],
		OI.[CreateTime]
	From Order_Invoice OI Left Join Config_Invoice_Type CIT
	On OI.InvoiceTypeID=CIT.ID And CIT.IsDelete = 0
	Where
		[OrderID] = @OrderID
	And OI.IsDelete = 0 
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Invoice_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Invoice_Insert
-- Author:	张连印
-- Description:	新增订单发票信息
-- ==========================================================================================
Create Procedure [dbo].[sp_Order_Invoice_Insert]
	@OrderID int,
	@InvoiceTypeID int,
	@InvoiceContentID int,
	@InvoiceTitle nvarchar(64),
	@InvoiceCost float,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Order_Invoice
		([OrderID],[InvoiceTypeID],[InvoiceContentID],[InvoiceTitle],[InvoiceCost],[CreateTime])
	Values
		(@OrderID,@InvoiceTypeID,@InvoiceContentID,@InvoiceTitle,@InvoiceCost,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================  
-- Entity: sp_Order_Insert  
-- Author: 张连印  
-- Description: 创建一条订单信息  
-- ==========================================================================================  
CREATE Procedure [dbo].[sp_Order_Insert]  
 @UserID int,  
 @RecieveAddressID int,  
 @CpsID int,  
 @PaymentMethodID int,  
 @OrderCode varchar(64),
 @OrderNumber  varchar(64),
 @TotalMoney float,  
 @TotalIntegral int,  
 @PaymentStatus int,  
 @IsRequireInvoice bit,  
 @DeliveryCost float, 
 @Discount float, 
 @Status int,  
 @Description nvarchar(512)=default,  
 @Remark nvarchar(512)=default,  
 @CreateTime datetime,  
 @ReferenceID int output   
As  
Begin  
 Insert Into [Order]  
  ([UserID],[RecieveAddressID],[CpsID],[PaymentMethodID],[OrderCode],[OrderNumber],[TotalMoney],[Discount],[TotalIntegral],[PaymentStatus],[Status],[Description],[Remark],[IsRequireInvoice],[DeliveryCost],[CreateTime])  
 Values  
  (@UserID,@RecieveAddressID,@CpsID,@PaymentMethodID,@OrderCode,@OrderNumber,@TotalMoney,@Discount,@TotalIntegral,@PaymentStatus,@Status,@Description,@Remark,@IsRequireInvoice,@DeliveryCost,@CreateTime)  
  
 Select @ReferenceID = @@IDENTITY  
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Erp_Log_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_Erp_Log_Insert
-- Author:	张连印
-- Create date:	2014/2/16 16:22:27
-- Description:	This stored procedure is intended for inserting values to Order_Erp_Log table
-- ==========================================================================================
Create Procedure [dbo].[sp_Order_Erp_Log_Insert]
	@ERP nvarchar(64),
	@OrderID int,
	@OperateType int,
	@UserID int,
	@ReqContent nvarchar(MAX),
	@ResContent nvarchar(MAX),
	@IsSuccess bit,
	@Operator int,
	@ExtField nvarchar(MAX),
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Order_Erp_Log
		([ERP],[OrderID],[OperateType],[UserID],[ReqContent],[ResContent],[IsSuccess],[Operator],[ExtField],[CreateTime])
	Values
		(@ERP,@OrderID,@OperateType,@UserID,@ReqContent,@ResContent,@IsSuccess,@Operator,@ExtField,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Delivery_Tracking_Details_SelectByOrderID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Delivery_Tracking_Details_SelectByOrderID
-- Author:	张连印
-- Description:	查询订单物流轮转信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Delivery_Tracking_Details_SelectByOrderID]
	@orderID int
As
Begin
	Select 
	   [ID]
      ,[OrderID]
      ,[Express]
      ,[MailNo]
      ,[Status]
      ,[StatusTime]
      ,[Remark]
      ,[Steps]
      ,[GJWStatus]
      ,[ExtField]
      ,[IsDelete]
      ,[CreateTime]
	From  Order_Delivery_Tracking Track 
	Where
	    Track.OrderID =@orderID
	And Track.IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectAllProduct]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_SelectAllProduct]
As
Begin
	Select 'c' + Convert(Varchar(50), id) As id, Convert(Varchar(50), parentid) As pid, categoryname As name ,''as GoujiuPrice,''as InventoryNumber
	From product_category Where IsDelete = 0
	Union
	Select 'bp' + Convert(Varchar(50), id) As id, 'c' + Convert(Varchar(50), productcategoryid) As pid, brandname As name ,''as GoujiuPrice,''as InventoryNumber
	From product_brand Where IsDelete = 0 And Layer = 1
	Union
	Select 'b' + Convert(Varchar(50), id) As id, 'bp' + Convert(Varchar(50), parentid) As pid, brandname As name ,''as GoujiuPrice,''as InventoryNumber
	From product_brand Where IsDelete = 0 And Layer = 2
	Union
	Select 'p' + Convert(Varchar(50), id) As id, 'b' + Convert(Varchar(50),productbrandid) As pid, name ,CONVERT(varchar(20),Product.GoujiuPrice),CONVERT(varchar(20),Product.InventoryNumber)
	From product Where IsDelete = 0 And productcategoryid In(
		Select id From product_category Where IsDelete = 0
	) And productbrandid in (
		Select id From product_brand Where IsDelete = 0
	) 
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectAll_Tree]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_SelectAll_Tree]
As
Begin
	Select 'c' + Convert(Varchar(50), id) As id, Convert(Varchar(50), parentid) As pid, categoryname As name 
	From product_category Where IsDelete = 0
	Union
	Select 'bp' + Convert(Varchar(50), id) As id, 'c' + Convert(Varchar(50), productcategoryid) As pid, brandname As name 
	From product_brand Where IsDelete = 0 And Layer = 1
	Union
	Select 'b' + Convert(Varchar(50), id) As id, 'bp' + Convert(Varchar(50), parentid) As pid, brandname As name 
	From product_brand Where IsDelete = 0 And Layer = 2
	Union
	Select 'p' + Convert(Varchar(50), id) As id, 'b' + Convert(Varchar(50),productbrandid) As pid, name 
	From product Where IsDelete = 0 And productcategoryid In(
		Select id From product_category Where IsDelete = 0
	) And productbrandid in (
		Select id From product_brand Where IsDelete = 0
	) 
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectAll]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_SelectAll]
As
Begin
	Select 
		[ID],
		[ProductCategoryID],
		[ProductBrandID],
		[Barcode],
		[Name],
		[SEOTitle],
		[SEOKeywords],
		[SEODescription],
		[Advertisement],
		[MarketPrice],
		[GoujiuPrice],
		[Introduce],
		[Integral],
		[InventoryNumber],
		[CommentNumber],
		[PageView],
		[Sorting],
		[Status],
		[CreateTime]
	From Product Where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Search_SuggestTip]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Search_SuggestTip]
	@Keyword varchar(200),
	@Attr varchar(1000)
As
Begin			
	--大类
	Select 
	'ProductCategory' As Type,
	ProductCategory As Name,
	ProductCategoryPinYin As [Search],
	Count(ProductCategory) As Num
	From [Product_Search]
	Where Status = 2 And ProductCategory Is Not Null And ProductSearchText Like ''+@Attr+'%' And ProductSearchText Like '%'+@Keyword+'%'
	Group By ProductCategory,ProductCategoryPinYin
	Union
	
	--品牌
	Select 
	'ParentBrand' As Type,
	ParentBrand As Name,	
	ProductCategoryPinYin + '-' + ParentBrandPinYin As [Search],
	Count(ParentBrand) As Num
	From [Product_Search]
	Where Status = 2 And ParentBrand Is Not Null And ProductSearchText Like ''+@Attr+'%' And ProductSearchText Like '%'+@Keyword+'%'
	Group by ProductCategoryPinYin,ParentBrand,ParentBrandPinYin
	Union

	--子品牌
	Select 
	'ProductBrand' As Type,
	ProductBrand As Name,
	ProductCategoryPinYin + '-' + ParentBrandPinYin + '-' + ProductBrandPinYin As [Search],
	Count(ProductBrand) As Num
	From [Product_Search]
	Where Status = 2 And ProductBrand Is Not Null And ProductSearchText Like ''+@Attr+'%' And ProductSearchText Like '%'+@Keyword+'%'
	Group by ProductCategoryPinYin,ParentBrandPinYin,ProductBrand,ProductBrandPinYin
	Union
	
	--价格
	Select 
	'GoujiuPrice' As Type,
	Case 
		When GoujiuPrice >= 6000 Then '6000以上'
		When GoujiuPrice >= 2000 And GoujiuPrice < 6000 Then '2000-5999'
		When GoujiuPrice >= 1000 And GoujiuPrice < 2000 Then '1000-1999'
		When GoujiuPrice >= 600  And GoujiuPrice < 1000 Then '600-999'
		When GoujiuPrice >= 200  And GoujiuPrice < 600  Then '200-599'
		When GoujiuPrice >= 100  And GoujiuPrice < 200  Then '100-199'
		When GoujiuPrice >= 1    And GoujiuPrice < 100  Then '0-99'
		Else '其他' End As Name,
	'' As [Search],
	Count(*) As Num
	From [Product_Search]
	Where Status = 2 And ProductBrand Is Not Null And ProductSearchText Like ''+@Attr+'%' And ProductSearchText Like '%'+@Keyword+'%'
	Group by Case 
		When GoujiuPrice >= 6000 Then '6000以上'
		When GoujiuPrice >= 2000 And GoujiuPrice < 6000 Then '2000-5999'
		When GoujiuPrice >= 1000 And GoujiuPrice < 2000 Then '1000-1999'
		When GoujiuPrice >= 600  And GoujiuPrice < 1000 Then '600-999'
		When GoujiuPrice >= 200  And GoujiuPrice < 600  Then '200-599'
		When GoujiuPrice >= 100  And GoujiuPrice < 200  Then '100-199'
		When GoujiuPrice >= 1    And GoujiuPrice < 100  Then '0-99'
		Else '其他' End
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Search_Suggest]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Search_Suggest]
	@SearchText varchar(200)
As
Begin
	Select Top 10
		[ID],
		[Name]
	From Product
	Where [IsDelete] = 0 And [ID] In(
		Select [ProductID] From [Product_Search] Where Status = 2 And ProductSearchText Like '%'+@SearchText+'%'
	)
	Order By PageView
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Search_SelectByID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Search_SelectByID]
	@id int
As
Begin
	Select 
		ID,
		ProductID,
		ProductName,
		ProductNamePinYin,
		ProductBarcode,
		ProductCategory,
		ProductCategoryPinYin,
		ProductBrand,
		ProductBrandPinYin,
		ParentCategory,
		ParentCategoryPinYin,
		ParentBrand,
		ParentBrandPinYin,
		MarketPrice,
		GoujiuPrice,
		ProductSearchText
	From Product_Search
	Where ProductID = @id
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Search]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Search]
	@SearchText varchar(200)
As
Begin
	Select 
		[ID],
		[Name],
		[Advertisement],
		[MarketPrice],
		[GoujiuPrice]
	From Product
	Where [IsDelete] = 0 And [ID] In(
		Select [ProductID] From [Product_Search] Where ProductSearchText Like '%'+@SearchText+'%'
	)
	Order By PageView
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Sms_Update]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Message_Sms_Update]
	@ID int,
	@EmployeeID int,
	@Name nvarchar(32),
	@Content nvarchar(256),
	@Status int,
	@CreateTime datetime
As
Begin
	Update User_Message_Sms
	Set
		[EmployeeID] = @EmployeeID,
		[Name] = @Name,
		[Content] = @Content,
		[Status] = @Status,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Sms_SelectRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Message_Sms_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[Content],
		[Status],
		[CreateTime]
	From User_Message_Sms
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Sms_SelectAll]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询所有的短信信息
-- =============================================
CREATE Procedure [dbo].[sp_User_Message_Sms_SelectAll]
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[Content],
		[Status],
		[CreateTime]
	From User_Message_Sms
	where [Status] = 0
	and [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Sms_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Message_Sms_Insert]
	@EmployeeID int,
	@Name nvarchar(32),
	@Content nvarchar(256),
	@Status int,
	@CreateTime datetime,
	@ReferenceID  int out
As
Begin
	Insert Into User_Message_Sms
		([EmployeeID],[Name],[Content],[Status],[CreateTime])
	Values
		(@EmployeeID,@Name,@Content,@Status,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Sms_DeleteRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_Message_Sms_DeleteRow]
	@ID int
AS
BEGIN
	Update User_Message_Sms set IsDelete = 255 where ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_SendRecord_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Message_SendRecord_Insert]
	@EmployeeID int,
	@MessageID int,
	@MessageTypeID int,
	@UserCount int,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into User_Message_SendRecord
		([EmployeeID],[MessageID],[MessageTypeID],[UserCount],[CreateTime])
	Values
		(@EmployeeID,@MessageID,@MessageTypeID,@UserCount,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Email_Update]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_User_Message_Email_Update]
	@ID int,
	@EmployeeID int,
	@Name nvarchar(32),
	@Title nvarchar(128),
	@Content ntext,
	@Status int
As
Begin
	Update User_Message_Email
	Set
		[EmployeeID] = @EmployeeID,
		[Name] = @Name,
		[Title] = @Title,
		[Content] = @Content,
		[Status] = @Status
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Email_SelectRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Message_Email_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[Title],
		[Content],
		[Status],
		[CreateTime]
	From User_Message_Email
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Email_SelectAll]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询所有的邮件信息
-- =============================================
CREATE Procedure [dbo].[sp_User_Message_Email_SelectAll]
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[Title],
		[Content],
		[Status],
		[CreateTime]
	From User_Message_Email
	Where [Status] = 0
	and [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Email_Insert]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_User_Message_Email_Insert]
	@EmployeeID int,
	@Name nvarchar(32),
	@Title nvarchar(128),
	@Content ntext,
	@Status int,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into User_Message_Email
		([EmployeeID],[Name],[Title],[Content],[Status],[CreateTime])
	Values
		(@EmployeeID,@Name,@Title,@Content,@Status,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Message_Email_DeleteRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_User_Message_Email_DeleteRow]
	@ID int
AS
BEGIN
	Update User_Message_Email set IsDelete = 255 where ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Update]    Script Date: 03/02/2014 20:58:32 ******/
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
/****** Object:  StoredProcedure [dbo].[sp_User_Level_SelectAll]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询所有的会员等级
-- =============================================
CREATE Procedure [dbo].[sp_User_Level_SelectAll]
As
Begin
	Select 
		[ID],
		[Name],
		[Money],
		[CreateTime]
	From User_Level
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Price_Update]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_User_Level_Price_Update]
	@ID int,
	@UserLevelID int,
	@EmployeeID int,
	@Price float,
	@Status int,
	@CreateTime datetime
As
Begin
	Update User_Level_Price
	Set		
		[UserLevelID] = @UserLevelID,
		[EmployeeID]=@EmployeeID,
		[Price] = @Price,
		[Status] = @Status,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Province_SelectAll]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Province_SelectAll]
As
Begin
	Select 
		[ID],
		[Name],
		[Sorting]
	From Province
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MuchBottled_Update]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MuchBottled_Update]
	@ID int,
	@EmployeeID int,
	@IsOnlinePayment bit,
	@EndTime datetime,
	@IsDisplayTime bit
As
Begin
	Update Promote_MuchBottled
	Set
		[EmployeeID] = @EmployeeID,		
		[IsOnlinePayment] = @IsOnlinePayment,
		[EndTime] = @EndTime,
		[IsDisplayTime] = @IsDisplayTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MuchBottled_SelectRow]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MuchBottled_SelectRow]
	@ID int
As
Begin
	Select 
		[Promote_MuchBottled].[ID],
		[EmployeeID],
		[ProductID],
		[Product].[Name] as [ProductName],
		[Product].[GoujiuPrice],
		[Promote_MuchBottled].[Name],
		[IsOnlinePayment],
		[StartTime],
		[EndTime],
		[IsDisplayTime],
		[Promote_MuchBottled].[CreateTime]
	From [Promote_MuchBottled]
	join [Product] on [Product].[ID]=[Promote_MuchBottled].[ProductID]
	Where
		[Promote_MuchBottled].[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MuchBottled_SelectByProductID]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询指定商品的多瓶装促销
-- =============================================
CREATE Procedure [dbo].[sp_Promote_MuchBottled_SelectByProductID]
	@ProductID int
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[ProductID],
		[Name],
		[IsOnlinePayment],
		[StartTime],
		[EndTime],
		[IsDisplayTime],
		[CreateTime]
	From Promote_MuchBottled
	Where
		[ProductID] = @ProductID
		and [EndTime]>CONVERT(datetime,GETDATE(),102)
		and Promote_MuchBottled.IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MuchBottled_Rule_Update]    Script Date: 03/02/2014 20:58:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MuchBottled_Rule_Update]
	@ID int,
	@Name nvarchar(32),
	@Quantity int,
	@UnitPrice float,
	@DiscountAmount float,
	@TotalMoney float,
	@ImageUrl nvarchar(128),
	@IsDefault bit
As
Begin
	Update Promote_MuchBottled_Rule
	Set
		[Name] = @Name,
		[Quantity] = @Quantity,
		[UnitPrice] = @UnitPrice,
		[DiscountAmount] = @DiscountAmount,
		[TotalMoney] = @TotalMoney,
		[ImageUrl] = @ImageUrl,
		[IsDefault] = @IsDefault
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MuchBottled_Rule_SelectByMuchBottledID]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询指定的多瓶装促销信息
-- =============================================
CREATE Procedure [dbo].[sp_Promote_MuchBottled_Rule_SelectByMuchBottledID]
	@MuchBottledID int
As
Begin
	Select 
		[ID],
		[MuchBottledID],
		[Name],
		[Quantity],
		[UnitPrice],
		[DiscountAmount],
		[TotalMoney],
		[ImageUrl],
		[IsDefault]
	From Promote_MuchBottled_Rule
	Where
		[MuchBottledID] = @MuchBottledID
		and IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MuchBottled_Rule_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MuchBottled_Rule_Insert]
	@MuchBottledID int,
	@Name nvarchar(32),
	@Quantity int,
	@UnitPrice float,
	@DiscountAmount float,
	@TotalMoney float,
	@ImageUrl nvarchar(128),
	@IsDefault bit,
	@ReferenceID int out
As
Begin
	Insert Into Promote_MuchBottled_Rule
		([MuchBottledID],[Name],[Quantity],[UnitPrice],[DiscountAmount],[TotalMoney],[ImageUrl],[IsDefault])
	Values
		(@MuchBottledID,@Name,@Quantity,@UnitPrice,@DiscountAmount,@TotalMoney,@ImageUrl,@IsDefault)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MuchBottled_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MuchBottled_Insert]
	@EmployeeID int,
	@ProductID int,
	@Name nvarchar(64),
	@IsOnlinePayment bit,
	@StartTime datetime,
	@EndTime datetime,
	@IsDisplayTime bit,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Promote_MuchBottled
		([EmployeeID],[ProductID],[Name],[IsOnlinePayment],[StartTime],[EndTime],[IsDisplayTime],[CreateTime])
	Values
		(@EmployeeID,@ProductID,@Name,@IsOnlinePayment,@StartTime,@EndTime,@IsDisplayTime,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_UpdateStatus]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description: 暂停、停止、开始满就送促销活动
-- =============================================
CREATE Procedure [dbo].[sp_Promote_MeetMoney_UpdateStatus]
	@ID int,
	@Status int
As
Begin
	Update Promote_MeetMoney
	Set		
		[Status] = @Status
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MeetMoney_Update]
	@ID int,
	@EmployeeID int,
	@Name nvarchar(64),
	@StartTime datetime,
	@EndTime datetime,
	@Description nvarchar(128),
	@IsMobileValidate bit,
	@IsUseCoupon bit,
	@IsNewUser bit
As
Begin
	Update Promote_MeetMoney
	Set
		[EmployeeID] = @EmployeeID,
		[Name] = @Name,
		[StartTime] = @StartTime,
		[EndTime] = @EndTime,
		[Description] = @Description,
		[IsMobileValidate] = @IsMobileValidate,
		[IsUseCoupon] = @IsUseCoupon,
		[IsNewUser] = @IsNewUser
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_SelectRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetMoney_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[StartTime],
		[EndTime],
		[Description],
		[IsMobileValidate],
		[IsUseCoupon],
		[IsNewUser],
		[Status],
		[CreateTime]
	From Promote_MeetMoney
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Scope_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MeetMoney_Scope_Update]
	@MeetMoneyID int,
	@Scope text
As
Begin
	Update Promote_MeetMoney_Scope
	Set
		[Scope] = @Scope
	Where
		[MeetMoneyID] = @MeetMoneyID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Scope_SelectRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MeetMoney_Scope_SelectRow]
	@MeetMoneyID int
As
Begin
	Select 
		[ID],
		[MeetMoneyID],
		[Scope],
		[IsDelete]
	From Promote_MeetMoney_Scope
	Where
		[MeetMoneyID] = @MeetMoneyID and IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Scope_SelectAll]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetMoney_Scope_SelectAll]
As
Begin
	Select 
		Promote_MeetMoney_Scope.[ID],
		Promote_MeetMoney_Scope.[MeetMoneyID],
		Promote_MeetMoney_Scope.[Scope],
		Promote_MeetMoney_Scope.[IsDelete]
	From Promote_MeetMoney_Scope
	left join Promote_MeetMoney on Promote_MeetMoney_Scope.[MeetMoneyID] = Promote_MeetMoney.ID 
	where Promote_MeetMoney_Scope.IsDelete = 0 
	and Promote_MeetMoney.IsDelete = 0 
	and Promote_MeetMoney.EndTime > GETDATE()
	and Promote_MeetMoney.[Status] = 1
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Scope_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetMoney_Scope_Insert]
	@MeetMoneyID int,
	@Scope text,
	@ReferenceID int out
As
Begin
	Insert Into Promote_MeetMoney_Scope
		([MeetMoneyID],[Scope])
	Values
		(@MeetMoneyID,@Scope)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Rule_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_MeetMoney_Rule_Update]
	@ID int,
	@MeetMoney float,
	@IsNoCeiling bit,
	@DecreaseCash float,
	@IsDecreaseCash bit,
	@IsGiveGift bit,
	@ProductID int,
	@IsGiveIntegral bit,
	@Integral int,
	@IsNoPostage bit,
	@IsGiveCoupon bit,
	@CouponType int,
	@CouponID int
As
Begin
	Update Promote_MeetMoney_Rule
	Set
		[MeetMoney] = @MeetMoney,
		[IsNoCeiling] = @IsNoCeiling,
		[DecreaseCash] = @DecreaseCash,
		[IsDecreaseCash] = @IsDecreaseCash,
		[IsGiveGift] = @IsGiveGift,
		[ProductID] = @ProductID,
		[IsGiveIntegral] = @IsGiveIntegral,
		[Integral] = @Integral,
		[IsNoPostage] = @IsNoPostage,
		[IsGiveCoupon] = @IsGiveCoupon,
		[CouponType] = @CouponType,
		[CouponID] = @CouponID
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Rule_SelectByMeetMoneyID]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询指定满就送促销活动的信息
-- =============================================
CREATE Procedure [dbo].[sp_Promote_MeetMoney_Rule_SelectByMeetMoneyID]
	@PromoteMeetMoneyID int
As
Begin
	Select 
		[Promote_MeetMoney_Rule].[ID],
		[PromoteMeetMoneyID],
		[MeetMoney],
		[IsDecreaseCash],
		[DecreaseCash],
		[IsGiveGift],
		[ProductID],
		[Product].[Name] as ProductName,
		[IsGiveIntegral],
		[Promote_MeetMoney_Rule].[Integral],
		[IsNoPostage],
		[IsGiveCoupon],
		[CouponType],
		[CouponID],
		[Coupon_Cash].Name as CashName,
		[Coupon_Decrease].Name as DecreaseName
	From [Promote_MeetMoney_Rule]
	left join [Product] on [Promote_MeetMoney_Rule].[ProductID] = [Product].[ID] and [Product].IsDelete = 0
	left join [Coupon_Cash] on [Promote_MeetMoney_Rule].CouponID = [Coupon_Cash].[ID] and [Promote_MeetMoney_Rule].[CouponType] = 0 and [Coupon_Cash].[IsDelete] = 0
	left join [Coupon_Decrease] on [Promote_MeetMoney_Rule].CouponID = [Coupon_Decrease].[ID] and [Promote_MeetMoney_Rule].[CouponType] = 1 and [Coupon_Decrease].[IsDelete] = 0
	Where
		[PromoteMeetMoneyID] = @PromoteMeetMoneyID and [Promote_MeetMoney_Rule].IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Rule_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_MeetMoney_Rule_Insert]
	@PromoteMeetMoneyID int,
	@MeetMoney float,
	@IsNoCeiling bit,
	@DecreaseCash float,
	@IsDecreaseCash bit,
	@IsGiveGift bit,
	@ProductID int,
	@IsGiveIntegral bit,
	@Integral int,
	@IsNoPostage bit,
	@IsGiveCoupon bit,
	@CouponType int,
	@CouponID int,
	@ReferenceID int out
As
Begin
	Insert Into Promote_MeetMoney_Rule
		([PromoteMeetMoneyID],[MeetMoney],[IsNoCeiling],[DecreaseCash],[IsDecreaseCash],[IsGiveGift],[ProductID],[IsGiveIntegral],[Integral],[IsNoPostage],[IsGiveCoupon],[CouponType],[CouponID])
	Values
		(@PromoteMeetMoneyID,@MeetMoney,@IsNoCeiling,@DecreaseCash,@IsDecreaseCash,@IsGiveGift,@ProductID,@IsGiveIntegral,@Integral,@IsNoPostage,@IsGiveCoupon,@CouponType,@CouponID)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Rule_DeleteRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	逻辑删除满就送活动规则
-- =============================================
CREATE Procedure [dbo].[sp_Promote_MeetMoney_Rule_DeleteRow]
	@ID int
As
Begin
	Update Promote_MeetMoney_Rule set IsDelete = 255
	Where
		[ID] = @ID		

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_MeetMoney_Insert]
	@EmployeeID int,
	@Name nvarchar(64),
	@StartTime datetime,
	@EndTime datetime,
	@Description nvarchar(128),
	@IsMobileValidate bit,
	@IsUseCoupon bit,
	@IsNewUser bit,
	@Status int,
	@ReferenceID int out 
AS
Begin
	Insert Into Promote_MeetMoney
		([EmployeeID],[Name],[StartTime],[EndTime],[Description],[IsMobileValidate],[IsUseCoupon],[IsNewUser],[Status])
	Values
		(@EmployeeID,@Name,@StartTime,@EndTime,@Description,@IsMobileValidate,@IsUseCoupon,@IsNewUser,@Status)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetMoney_Delete]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE Procedure [dbo].[sp_Promote_MeetMoney_Delete]
	@ID int
As
Begin
	Update Promote_MeetMoney Set IsDelete = 255 where ID = @ID
	Update Promote_MeetMoney_Rule set IsDelete = 255 where PromoteMeetMoneyID = @ID
	Update Promote_MeetMoney_Scope Set IsDelete = 255 where MeetMoneyID = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_UpdateStatus]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetAmount_UpdateStatus]
	@ID int,
	@Status int
As
Begin
	Update Promote_MeetAmount
	Set [Status] = @Status
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_MeetAmount_Update]
	@ID int,
	@EmployeeID int,
	@Name nvarchar(64),
	@IsMobileValidate bit,
	@IsUseCoupon bit,
	@IsNewUser bit,
	@StartTime datetime,
	@EndTime datetime,
	@Description nvarchar(128)
As
Begin
	Update Promote_MeetAmount
	Set
		[EmployeeID] = @EmployeeID,
		[Name] = @Name,
		[IsMobileValidate] = @IsMobileValidate,
		[IsUseCoupon] = @IsUseCoupon,
		[IsNewUser] = @IsNewUser,
		[StartTime] = @StartTime,
		[EndTime] = @EndTime,
		[Description] = @Description
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_SelectRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013/12/17 15:00:50
-- Description:	查询指定的满件优惠促销
-- =============================================
CREATE Procedure [dbo].[sp_Promote_MeetAmount_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[IsMobileValidate],
		[IsUseCoupon],
		[IsNewUser],
		[StartTime],
		[EndTime],
		[Description],
		[Status],
		[CreateTime],
		[IsDelete]
	From Promote_MeetAmount
	Where
		[ID] = @ID
		and IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Scope_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MeetAmount_Scope_Update]
	@MeetAmountID int,
	@Scope text
As
Begin
	Update Promote_MeetAmount_Scope
	Set
		[Scope] = @Scope
	Where
		[MeetAmountID] = @MeetAmountID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Scope_SelectRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetAmount_Scope_SelectRow]
	@MeetAmountID int
As
Begin
	Select 
		[ID],
		[MeetAmountID],
		[Scope],
		[IsDelete]
	From Promote_MeetAmount_Scope
	Where
		[MeetAmountID] = @MeetAmountID and IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Scope_SelectAll]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetAmount_Scope_SelectAll]
As
Begin
	Select 
		Promote_MeetAmount_Scope.[ID],
		Promote_MeetAmount_Scope.[MeetAmountID],
		Promote_MeetAmount_Scope.[Scope],
		Promote_MeetAmount_Scope.[IsDelete]
	From Promote_MeetAmount_Scope
	left join Promote_MeetAmount on Promote_MeetAmount_Scope.[MeetAmountID] = Promote_MeetAmount.ID 
	where Promote_MeetAmount_Scope.IsDelete = 0 
	and Promote_MeetAmount.IsDelete = 0 
	and Promote_MeetAmount.EndTime > GETDATE()
	and Promote_MeetAmount.[Status] = 1
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Scope_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetAmount_Scope_Insert]
	@MeetAmountID int,
	@Scope text,
	@ReferenceID int out
As
Begin
	Insert Into Promote_MeetAmount_Scope
		([MeetAmountID],[Scope])
	Values
		(@MeetAmountID,@Scope)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Rule_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MeetAmount_Rule_Update]
	@ID int,
	@PromoteMeetAmountID int,
	@MeetAmount int,
	@IsDiscount bit,
	@Discount float,
	@IsNoPostage bit,
	@IsGiveGift bit,
	@ProductID int
As
Begin
	Update Promote_MeetAmount_Rule
	Set
		[PromoteMeetAmountID] = @PromoteMeetAmountID,
		[MeetAmount] = @MeetAmount,
		[IsDiscount] = @IsDiscount,
		[Discount] = @Discount,
		[IsNoPostage] = @IsNoPostage,
		[IsGiveGift] = @IsGiveGift,
		[ProductID] = @ProductID
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Rule_SelectByMeetAmountID]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetAmount_Rule_SelectByMeetAmountID]
	@PromoteMeetAmountID int
As
Begin
	Select 
		[Promote_MeetAmount_Rule].[ID],
		[PromoteMeetAmountID],
		[MeetAmount],		
		[IsDiscount],
		[Discount],
		[IsGiveGift],
		[ProductID],
		[Product].[Name] as ProductName,
		[IsNoPostage]
	From [Promote_MeetAmount_Rule]
	left join [Product] on [Promote_MeetAmount_Rule].[ProductID] = [Product].[ID] and [Product].IsDelete = 0
	Where
		[PromoteMeetAmountID] = @PromoteMeetAmountID and [Promote_MeetAmount_Rule].IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Rule_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MeetAmount_Rule_Insert]
	@PromoteMeetAmountID int,
	@MeetAmount int,
	@IsDiscount bit,
	@Discount float,
	@IsNoPostage bit,
	@IsGiveGift bit,
	@ProductID int,
	@ReferenceID int out
As
Begin
	Insert Into Promote_MeetAmount_Rule
		([PromoteMeetAmountID],[MeetAmount],[IsDiscount],[Discount],[IsNoPostage],[IsGiveGift],[ProductID])
	Values
		(@PromoteMeetAmountID,@MeetAmount,@IsDiscount,@Discount,@IsNoPostage,@IsGiveGift,@ProductID)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Rule_DeleteRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetAmount_Rule_DeleteRow]
	@ID int
As
Begin
	Update Promote_MeetAmount_Rule
	Set [IsDelete] = 255
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_MeetAmount_Insert]
	@EmployeeID int,
	@Name nvarchar(64),
	@IsMobileValidate bit,
	@IsUseCoupon bit,
	@IsNewUser bit,
	@StartTime datetime,
	@EndTime datetime,
	@Description nvarchar(128) = null,
	@Status int,
	@ReferenceID int out
As
Begin
	Insert Into Promote_MeetAmount
		([EmployeeID],[Name],[IsMobileValidate],[IsUseCoupon],[IsNewUser],[StartTime],[EndTime],[Description],[Status])
	Values
		(@EmployeeID,@Name,@IsMobileValidate,@IsUseCoupon,@IsNewUser,@StartTime,@EndTime,@Description,@Status)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_MeetAmount_DeleteRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Promote_MeetAmount_DeleteRow]
	@ID int
As
Begin
	Update Promote_MeetAmount
	Set IsDelete = 255
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_Limited_Discount_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_Limited_Discount_Update]
	@ID int,
	@Name nvarchar(64),
	@Discount float,
	@DiscountPrice float,
	@TotalQuantity int,
	@LimitedBuyQuantity int,
	@IsOnlinePayment bit,
	@IsMobileValidate bit,
	@IsUseCoupon bit,
	@IsNewUser bit,
	@StartTime datetime,
	@EndTime datetime
As
Begin
	Update Promote_Limited_Discount
	Set
		[Name] = @Name,
		[Discount] = @Discount,
		[DiscountPrice] = @DiscountPrice,
		[TotalQuantity] = @TotalQuantity,
		[LimitedBuyQuantity] = @LimitedBuyQuantity,
		[IsOnlinePayment] = @IsOnlinePayment,
		[IsMobileValidate] = @IsMobileValidate,
		[IsUseCoupon] = @IsUseCoupon,
		[IsNewUser] = @IsNewUser,
		[StartTime] = @StartTime,
		[EndTime] = @EndTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_Limited_Discount_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Promote_Limited_Discount_Insert]
	@ProductID int,
	@Status int,
	@Name nvarchar(64),
	@Discount float,
	@DiscountPrice float,
	@TotalQuantity int,
	@LimitedBuyQuantity int,
	@IsOnlinePayment bit,
	@IsMobileValidate bit,
	@IsUseCoupon bit,
	@IsNewUser bit,
	@StartTime datetime,
	@EndTime datetime,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Promote_Limited_Discount
		([ProductID],[Name],[Discount],[DiscountPrice],[TotalQuantity],[LimitedBuyQuantity],[IsOnlinePayment],[IsMobileValidate],[IsUseCoupon],[IsNewUser],[StartTime],[EndTime],[CreateTime],[Status])
	Values
		(@ProductID,@Name,@Discount,@DiscountPrice,@TotalQuantity,@LimitedBuyQuantity,@IsOnlinePayment,@IsMobileValidate,@IsUseCoupon,@IsNewUser,@StartTime,@EndTime,@CreateTime,@Status)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_LandingPage_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_LandingPage_Update]
	@ID int,
	@PID int = null,
	@Name varchar(200),
	@EmployeeID int,
	@StartTime datetime,
	@EndTime datetime,
	@Link varchar(1000),
	@Content text,
	@MasterPicture varchar(1000) = null,
	@Picture01 varchar(1000) = null,
	@Picture02 varchar(1000) = null,
	@Picture03 varchar(1000) = null,
	@Picture04 varchar(1000) = null,
	@Picture05 varchar(1000) = null
As
Begin
	UPDATE [Promote_LandingPage] SET 
		[PID] = @PID,
		[Name] = @Name,
		[EmployeeID] = @EmployeeID,
		[StartTime] = @StartTime,
		[EndTime] = @EndTime,
		[Link] = @Link,
		[Content] = @Content,
		[MasterPicture] = @MasterPicture,
		[Picture01] = @Picture01,
		[Picture02] = @Picture02,
		[Picture03] = @Picture03,
		[Picture04] = @Picture04,
		[Picture05] = @Picture05
	WHERE ID=@ID 
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_LandingPage_SelectRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_LandingPage_SelectRow]
	@ID int
As
Begin
	 Select 
		ID,
		PID,
		Name,
		EmployeeID,
		StartTime,
		EndTime,
		Status,
		Link,
		Content,
		MasterPicture,
		Picture01,
		Picture02,
		Picture03,
		Picture04,
		Picture05
	 From [Promote_LandingPage]
	 Where IsDelete = 0 And ID=@ID 
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_LandingPage_SelectAll]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_LandingPage_SelectAll]
As
Begin
	 Select 
		[Promote_LandingPage].ID,
		[Promote_LandingPage].PID,
		[Promote_LandingPage].Name,
		[Promote_LandingPage].EmployeeID
	 From [Promote_LandingPage]
	 Where IsDelete = 0 And ID IN(
	 	Select Distinct PID From [Promote_LandingPage]
	 )
	 Union
	 Select 
		[Promote_LandingPage].ID,
		[Promote_LandingPage].PID,
		Coalesce([Promote_LandingPage].Name,'') + ' [' + Coalesce((Select TOP 1 [System_User].Name From [System_User] Where [System_User].ID = [Promote_LandingPage].EmployeeID),'') + ']' As Name,
		[Promote_LandingPage].EmployeeID
	 From [Promote_LandingPage]
	 Where IsDelete = 0 And ID Not IN(
	 	Select Distinct PID From [Promote_LandingPage]
	 )
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Promote_LandingPage_Insert]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Promote_LandingPage_Insert]
	@Name varchar(200),
	@EmployeeID int,
	@StartTime datetime,
	@EndTime datetime,
	@Status int,
	@Link varchar(1000),
	@Content text,
	@MasterPicture varchar(1000) = null,
	@Picture01 varchar(1000) = null,
	@Picture02 varchar(1000) = null,
	@Picture03 varchar(1000) = null,
	@Picture04 varchar(1000) = null,
	@Picture05 varchar(1000) = null,
	@ReferenceID int out
As
Begin
	INSERT INTO [Promote_LandingPage](
		[Name],[EmployeeID],[StartTime],[EndTime],[Status],[Link],[Content],[MasterPicture],[Picture01],[Picture02],[Picture03],[Picture04],[Picture05]
	)VALUES(
		@Name,@EmployeeID,@StartTime,@EndTime,@Status,@Link,@Content,@MasterPicture,@Picture01,@Picture02,@Picture03,@Picture04,@Picture05
	)
	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_UpdateStates]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_UpdateStates]
	@ID nvarchar(200),
	@Status int
As
Begin
	Update Product Set [Status] = @Status Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Update_SaleAmount]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Product_Update
-- Author:	张连印
-- Create date:	2013/12/17 10:08:58
-- Description:	更新库存、销售量
-- ==========================================================================================
Create Procedure [dbo].[sp_Product_Update_SaleAmount]
	@ID int,
	@SaleAmount int
As
Begin
	Update Product
	Set
		[InventoryNumber] -= @SaleAmount ,
		[SoldOfReality] += @SaleAmount
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Update]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Update]
	@ID int,
	@Barcode varchar(64),
	@Name nvarchar(128),
	@SEOTitle nvarchar(128),
	@SEOKeywords nvarchar(512),
	@SEODescription nvarchar(512),
	@Advertisement nvarchar(128),
	@MarketPrice float,
	@GoujiuPrice float,
	@Introduce ntext,
	@Integral int,
	@InventoryNumber int,
	@SoldOfVirtual int,
	@Status int,
	@Attributes text
As
Begin
	Update Product
	Set
		[Barcode] = @Barcode,
		[Name] = @Name,
		[SEOTitle] = @SEOTitle,
		[SEOKeywords] = @SEOKeywords,
		[SEODescription] = @SEODescription,
		[Advertisement] = @Advertisement,
		[MarketPrice] = @MarketPrice,
		[GoujiuPrice] = @GoujiuPrice,
		[Introduce] = @Introduce,
		[Integral] = @Integral,
		[InventoryNumber] = @InventoryNumber,
		[SoldOfVirtual] = @SoldOfVirtual,
		[Status] = @Status,
		[Attributes] = @Attributes
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectRow]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[ParentCategoryID],
		[ProductCategoryID],
		[ParentBrandID],
		[ProductBrandID],
		[Barcode],
		[Name],
		[SEOTitle],
		[SEOKeywords],
		[SEODescription],
		[Advertisement],
		[MarketPrice],
		[GoujiuPrice],
		[Introduce],
		[Integral],
		[InventoryNumber],
		[CommentNumber],
		[SoldOfReality],
		[SoldOfVirtual],
		[PageView],
		[Sorting],
		[Status],
		[CreateTime],
		[Attributes]
	From Product
	Where [IsDelete] = 0 And [ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectGuessLike]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	猜你喜欢
-- =============================================
CREATE PROCEDURE [dbo].[sp_Product_SelectGuessLike] 
	
AS
BEGIN
	Select top 5 * from Product order by newid()  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectfromInfoId]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_Product_SelectfromInfoId]
  @condition nvarchar(1000)
  as
  begin
  if @condition!=null
  SELECT
  [ID]
      ,[ParentCategoryID]
      ,[ProductCategoryID]
      ,[ParentBrandID]
      ,[ProductBrandID]
      ,[Barcode]
      ,[Name]
      ,[SEOTitle]
      ,[SEOKeywords]
      ,[SEODescription]
      ,[Advertisement]
      ,[MarketPrice]
      ,[GoujiuPrice]
      ,[Introduce]
      ,[Integral]
      ,[InventoryNumber]
      ,[CommentNumber]
      ,[SoldOfReality]
      ,[SoldOfVirtual]
      ,[PageView]
      ,[Sorting]
      ,[Status]
      ,[CreateTime]
      ,[IsDelete]
      ,[Attributes]
  FROM [dbo].[Product] where ID=@condition
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_hw_Log_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_hw_Log_Insert]
	@Number nvarchar(50),
	@Content ntext,
	@State tinyint,
	@CreateTime datetime,
	@ExtField nvarchar(50),
	@ReferenceID int output
As
Begin
	Insert Into hw_Log
		([Number],[Content],[State],[CreateTime],[ExtField])
	Values
		(@Number,@Content,@State,@CreateTime,@ExtField)
		
	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOrderCount]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_GetOrderCount]
as
SELECT 
count(*) AS OrderCount,
getdate() AS CreateTime
FROM 
[Order]
WHERE  
DATEDIFF(dd,CreateTime,getdate())=0
GO
/****** Object:  StoredProcedure [dbo].[sp_Get_Order_ActualPayment]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Get_Order_ActualPayment
-- Author:	张连印
-- Description:	获取订单实际付款金额
-- ==========================================================================================

CREATE PROCEDURE [dbo].[sp_Get_Order_ActualPayment]
@OrderId int,
@ActualPaymentMoney float output
As
Begin
	Declare @paymentId int, @discountMoney float =0
	Select top 1 @ActualPaymentMoney = isnull(PaymentMoney,0), @paymentId=ID From Order_Payment where  OrderID = @OrderId;
	
	Select @discountMoney = SUM(isnull(PaymentMoney,0)) From (
		Select ISNULL(Deduction,0) PaymentMoney  From Order_Payment_Coupon where OrderPaymentID = @paymentId and isDelete = 0
		union all
		Select ISNULL(Deduction,0) PaymentMoney From Order_Payment_Integral Where OrderPaymentID =@paymentId and isDelete = 0) Order_Payment_Discount
		
	Select @ActualPaymentMoney = isnull(@ActualPaymentMoney,0) - isnull(@discountMoney,0);
End
GO
/****** Object:  StoredProcedure [dbo].[sp_FeedBackInsert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_FeedBackInsert]
	
      @Type int
      ,@Name varchar(128)
      ,@Content text
      ,@ImgUrl varchar(500)
      ,@Gender bit=1
      ,@GjwNumber nvarchar(128)
      ,@Email nvarchar(256)
      ,@TelPhone nvarchar(128)
	  ,@ReferenceID int output
As
Begin
	Insert Into [FeedBack]
		(
		 [Type]
		 , Content
		 , ImgUrl
		 , Name
		 , Gender
		 , GjwNumber
		 , Email
		 , TelPhone
     
      )
	Values(@Type,@Content,@ImgUrl,@Name,@Gender,@GjwNumber,@Email,@TelPhone)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Feed_SelectById]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_feedback_SelectbyID
-- Author:	张马
-- Description:	查询所有Cps信息
-- ==========================================================================================
create PROCEDURE [dbo].[sp_Feed_SelectById]
@ID int
As
Begin
	SELECT TOP 1000 [ID]
      ,[Type]
      ,[Content]
      ,[ImgUrl]
      ,[Name]
      ,[Gender]
      ,[GjwNumber]
      ,[Email]
      ,[TelPhone]
  FROM [goujiuwang_v5_test].[dbo].[FeedBack] where [IsDelete]=0 and ID=@ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Cps_Update]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Cps_Update]
	@ID int,
	@Name nvarchar(20),
	@UserName nvarchar(50)=null,
	@URL nvarchar(80),
	@Linkman nvarchar(20),
	@Mobile varchar(20),
	@Tel varchar(20)=null,
	@Email varchar(50)=null,
	@QQ varchar(20)=null,
	@Company nvarchar(50)=null,
	@CompanyAddress nvarchar(50)=null,
	@ZipCode varchar(10)=null,
	@TrackingURL varchar(100)=null,
	@CreateTime datetime
As
Begin
	Update Cps
	Set
		[Name] = @Name,
		[UserName] = @UserName,
		[URL] = @URL,
		[Linkman] = @Linkman,
		[Mobile] = @Mobile,
		[Tel] = @Tel,
		[Email] = @Email,
		[QQ] = @QQ,
		[Company] = @Company,
		[CompanyAddress] = @CompanyAddress,
		[ZipCode] = @ZipCode,
		[TrackingURL] = @TrackingURL,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Cps_SelectAll]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Cps_SelectAll
-- Author:	张连印
-- Description:	查询所有Cps信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Cps_SelectAll]
As
Begin
	Select 
		[ID]
	  ,[Name]
      ,[UserName]
      ,[URL]
      ,[TrackingURL]
      ,[Linkman]
      ,[Mobile]
      ,[Tel]
      ,[Email]
      ,[QQ]
      ,[Company]
      ,[CompanyAddress]
      ,[ZipCode]
      ,[CreateTime]
	From Cps
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Cps_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Cps_Insert]
	@Name nvarchar(20),
	@UserName nvarchar(50),
	@URL nvarchar(80),
	@Linkman nvarchar(20),
	@Mobile varchar(20)= null,
	@Tel varchar(20)= null,
	@Email varchar(50)= null,
	@QQ varchar(20)= null,
	@Company nvarchar(50)= null,
	@CompanyAddress nvarchar(50)= null,
	@ZipCode varchar(10)= null,
	@TrackingURL varchar(100)= null,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Cps
		([Name],[UserName],[URL],[Linkman],[Mobile],[Tel],[Email],[QQ],[Company],[CompanyAddress],[ZipCode],[TrackingURL],[CreateTime])
	Values
		(@Name,@UserName,@URL,@Linkman,@Mobile,@Tel,@Email,@QQ,@Company,@CompanyAddress,@ZipCode,@TrackingURL,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Cps_CommissionRatio_Update]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Cps_CommissionRatio_Update]
	@ID int,
	@CpsID int,
	@ProductCategoryID int,
	@CommissionRatio float,
	@CreateTime datetime
As
Begin
	Update Cps_CommissionRatio
	Set
		[CpsID] = @CpsID,
		[ProductCategoryID] = @ProductCategoryID,
		[CommissionRatio] = @CommissionRatio,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Cps_CommissionRatio_SelectRow]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Cps_CommissionRatio_SelectRow]
	@ID int
As
Begin
	Select 
		[Cps_CommissionRatio].[ID],
		[CpsID],
		[ProductCategoryID],
		[Product_Category].[CategoryName] as [ProductCategoryName],
		[CommissionRatio],
		[Cps_CommissionRatio].[CreateTime]
	From [Cps_CommissionRatio]
	left join [Product_Category] on [Cps_CommissionRatio].[ProductCategoryID]=[Product_Category].[ID]
	Where
		[Cps_CommissionRatio].[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Cps_CommissionRatio_SelectByCpsID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Cps_CommissionRatio_SelectByCpsID]
	@CpsID int
As
Begin
	Select 
		c.[ID],
		c.[CpsID],
		c.[ProductCategoryID],
		p.[CategoryName] as [ProductCategoryName],
		c.[CommissionRatio],
		c.[CreateTime]
	from Cps_CommissionRatio as c
	inner join dbo.Product_Category as p on  c.ProductCategoryID=p.ID
	Where c.IsDelete = 0 And c.[CpsID] = @CpsID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Cps_CommissionRatio_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Cps_CommissionRatio_Insert]
	@CpsID int,
	@ProductCategoryID int,
	@CommissionRatio float,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Cps_CommissionRatio
		([CpsID],[ProductCategoryID],[CommissionRatio],[CreateTime])
	Values
		(@CpsID,@ProductCategoryID,@CommissionRatio,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Scope_SelectByCouponID]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Coupon_Scope_SelectByCouponID]
	@CouponID int,
	@CouponTypeID int
As
Begin

	Select [Coupon_Scope].[ID],[CouponID],[CouponTypeID],[ScopeType],[TargetTypeID],[Coupon_Scope].[CreateTime],
	'TargetTypeName' = Case [ScopeType] When 0 Then '全场'
						  When 1 Then (Select [CategoryName] From [Product_Category] Where [Product_Category].[ID]=[Coupon_Scope].[TargetTypeID])
						  When 2 Then (Select [CategoryName] From [Product_Category] Where [Product_Category].[ID]=[Coupon_Scope].[TargetTypeID])
						  When 3 Then (Select [BrandName] From [Product_Brand] Where [Product_Brand].[ID]=[Coupon_Scope].[TargetTypeID])
						  When 4 Then (Select [Name] From [Product] Where [Product].[ID] = [Coupon_Scope].[TargetTypeID])
	End
	From [Coupon_Scope] 
	Where [Coupon_Scope].[IsDelete] = 0 And [Coupon_Scope].[CouponID] = @CouponID and [Coupon_Scope].[CouponTypeID] = @CouponTypeID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Scope_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Coupon_Scope_Insert]
	@CouponID int,
	@CouponTypeID int,
	@ScopeType int,
	@TargetTypeID int,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Coupon_Scope
		([CouponID],[CouponTypeID],[ScopeType],[TargetTypeID],[CreateTime])
	Values
		(@CouponID,@CouponTypeID,@ScopeType,@TargetTypeID,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Decrease_Update_InitialNumber]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE Procedure [dbo].[sp_Coupon_Decrease_Update_InitialNumber]
	@ID int,
	@InitialNumber int
As
Begin
	Update Coupon_Decrease
	Set
		[InitialNumber] = @InitialNumber
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Decrease_SelectRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Coupon_Decrease_SelectRow]
	@ID int
As
Begin
	
Select 
		[Coupon_Decrease].[ID],
		[EmployeeID],
		[Name],
		[FaceValue],
		[MeetAmount],
		[InitialNumber],
		[Description],
		[StartTime],
		[EndTime],
		[Coupon_Decrease].[CreateTime],
		([InitialNumber]-(SELECT COUNT(1) FROM dbo.Coupon_Decrease_Binding WHERE (CouponDecreaseID = @ID))) AS Remain
	From [Coupon_Decrease]

	Where
		[Coupon_Decrease].[ID] =@ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Decrease_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Coupon_Decrease_SelectAll]
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[MeetAmount],
		[FaceValue],
		[InitialNumber],
		[Description],
		[StartTime],
		[EndTime],
		[CreateTime]
	From Coupon_Decrease
	Where IsDelete=0 And [EndTime]>CONVERT(datetime,GETDATE(),102)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Decrease_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Coupon_Decrease_Insert]
	@EmployeeID int,
	@Name nvarchar(32),
	@MeetAmount float,
	@FaceValue float,
	@InitialNumber int,
	@Description nvarchar(128),
	@StartTime datetime,
	@EndTime datetime,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Coupon_Decrease
		([EmployeeID],[Name],[MeetAmount],[FaceValue],[InitialNumber],[Description],[StartTime],[EndTime],[CreateTime])
	Values
		(@EmployeeID,@Name,@MeetAmount,@FaceValue,@InitialNumber,@Description,@StartTime,@EndTime,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Decrease_Exists_Name]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Coupon_Decrease_Exists_Name]
	@Name nvarchar(32)
AS
BEGIN
	Select [Name] From [Coupon_Decrease] Where IsDelete=0 And [Name]=@Name And [EndTime]>CONVERT(datetime,GETDATE(),102)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Decrease_Binding_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Coupon_Decrease_Binding_Insert]
	@CouponDecreaseID int,
	@UserID int,
	@GetOrderID int =default,
	@SetOrderID int =default,
	@Number varchar(32),
	@Password varchar(32),
	@Cause nvarchar(512),
	@Status int,
	@UseTime datetime =null,
	@BindingTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Coupon_Decrease_Binding
		([CouponDecreaseID],[UserID],[SetOrderID],[GetOrderID],[Number],[Password],[Cause],[Status],[UseTime],[BindingTime])
	Values
		(@CouponDecreaseID,@UserID,@SetOrderID,@GetOrderID,@Number,@Password,@Cause,@Status,@UseTime,@BindingTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Cash_Update_InitialNumber]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Coupon_Cash_Update_InitialNumber]
	@ID int,
	@InitialNumber int
As
Begin
	Update Coupon_Cash
	Set
		[InitialNumber] = @InitialNumber
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Cash_SelectRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Coupon_Cash_SelectRow]
	@ID int
As
Begin
	Select 
		[Coupon_Cash].[ID],
		[EmployeeID],
		[Name],
		[FaceValue],
		[InitialNumber],
		[Description],
		[StartTime],
		[EndTime],
		[Coupon_Cash].[CreateTime],
		([InitialNumber]-(SELECT COUNT(1) FROM dbo.Coupon_Cash_Binding WHERE (CouponCashID = @ID))) AS Remain
	From [Coupon_Cash]
	Where
		[Coupon_Cash].[ID] = @ID 
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Cash_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Coupon_Cash_SelectAll]
As
Begin
	Select 
		[ID],
		[EmployeeID],
		[Name],
		[FaceValue],
		[InitialNumber],
		[Description],
		[StartTime],
		[EndTime],
		[CreateTime]
	From Coupon_Cash
	Where IsDelete=0 And [EndTime]>CONVERT(datetime,GETDATE(),102)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Cash_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Coupon_Cash_Insert]
	@EmployeeID int,
	@Name nvarchar(32),
	@FaceValue float,
	@InitialNumber int,
	@Description nvarchar(128)=null,
	@StartTime datetime,
	@EndTime datetime,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Coupon_Cash
		([EmployeeID],[Name],[FaceValue],[InitialNumber],[Description],[StartTime],[EndTime],[CreateTime])
	Values
		(@EmployeeID,@Name,@FaceValue,@InitialNumber,@Description,@StartTime,@EndTime,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Cash_Exists_Name]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Coupon_Cash_Exists_Name]
	@Name nvarchar(32)
AS
BEGIN
	SELECT [Name] FROM [Coupon_Cash] WHERE IsDelete=0 And [Name]=@Name And [EndTime]>CONVERT(datetime,GETDATE(),102)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Coupon_Cash_Binding_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Coupon_Cash_Binding_Insert]
	@CouponCashID int,
	@UserID int,
	@SetOrderID int =default,
	@GetOrderID int =default,
	@Number varchar(32),
	@Password varchar(32),
	@Status int,
	@Cause nvarchar(512),
	@UseTime datetime =null,
	@BindingTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Coupon_Cash_Binding
		([CouponCashID],[UserID],[SetOrderID],[GetOrderID],[Number],[Password],[Status],[Cause],[UseTime],[BindingTime])
	Values
		(@CouponCashID,@UserID,@SetOrderID,@GetOrderID,@Number,@Password,@Status,@Cause,@UseTime,@BindingTime)
	
	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_County_SelectPostByID]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_County_SelectPostByID]
	@ID int
As
Begin
	Select 
		[PostCode]
	From County
	WHERE [ID]=@ID
	And IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_County_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_County_SelectAll]
As
Begin
	Select 
		[ID],
		[CityID],
		[Name],
		[PostCode]
	From County
	Where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_ConfigPage_UpdateLink]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ConfigPage_UpdateLink]
  @id int,
  @source varchar(1000),
  @name varchar(1000)
  as 
  begin
  update Config_Page set Name=@name,Source=@source where ID=@id
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Type_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Payment_Type_Update]
	@ID int,
	@PaymentMethodID int,
	@Name nvarchar(50),
	@CreateTime datetime
As
Begin
	Update Config_Payment_Type
	Set
		[PaymentMethodID] = @PaymentMethodID,
		[Name] = @Name,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Type_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Payment_Type_SelectAll]
As
Begin
	Select 
		[ID],
		[PaymentMethodID],
		[Name],
		[CreateTime]
	From Config_Payment_Type
	Where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Type_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Payment_Type_Insert]
	@PaymentMethodID int,
	@Name nvarchar(16),
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Config_Payment_Type
		([PaymentMethodID],[Name],[CreateTime])
	Values
		(@PaymentMethodID,@Name,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Type_DeleteRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Payment_Type_DeleteRow]
	@ID int
As
Begin
	Update Config_Payment_Organization
	Set IsDelete = 255
	where PaymentTypeID=@ID;
	
	UPdate Config_Payment_Type
	Set IsDelete = 255
	Where
		[ID] = @ID;
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Organization_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Payment_Organization_Update]
	@ID int,
	@PaymentTypeID int,
	@Name nvarchar(32),
	@URL nvarchar(50)=default,
	@ImageURL nvarchar(50)=default,
	@Sorting int,
	@CreateTime datetime
As
Begin
	Update Config_Payment_Organization
	Set
		[PaymentTypeID] = @PaymentTypeID,
		[Name] = @Name,
		[URL] = @URL,
		[ImageURL] = @ImageURL,
		[Sorting] = @Sorting,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Organization_SelectByTypeID]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Payment_Organization_SelectByTypeID]
@PaymentTypeID int
As
Begin
	Select 
		[ID],
		[Name],
		[URL],
		[ImageURL],
		[Sorting],
		[CreateTime]
	From Config_Payment_Organization
	Where 
		[PaymentTypeID]=@PaymentTypeID
		And IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Organization_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Payment_Organization_SelectAll]
As
Begin
	Select 
		[ID],
		[PaymentTypeID],
		[Name],
		[URL],
		[ImageURL],
		[Sorting],
		[CreateTime]
	From Config_Payment_Organization
	Where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Organization_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Payment_Organization_Insert]
	@PaymentTypeID int,
	@Name nvarchar(32),
	@URL varchar(50)=default,
	@ImageURL varchar(50)=default,
	@Sorting int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Config_Payment_Organization
		([PaymentTypeID],[Name],[URL],[ImageURL],[Sorting],[CreateTime])
	Values
		(@PaymentTypeID,@Name,@URL,@ImageURL,@Sorting,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Payment_Organization_DeleteRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Payment_Organization_DeleteRow]
	@ID int
As
Begin
	Update Config_Payment_Organization
	Set IsDelete = 255
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Page_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Config_Page_Update]
	@ID int,
	@Name varchar(200),
	@Content text
As
Begin
	Update Config_Page
	Set
		Content=@Content,
		Name=@Name
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Page_SelectByID]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Config_Page_SelectByID]
@ID int
As
Begin
	Select 
		[ID]
      ,[PID]
      ,[Type]
      ,[Name]
      ,[Content]
      ,[CreateTime]
      ,[Source]
      ,[IsDelete]
  FROM [goujiuwang_v5_test].[dbo].[Config_Page] where ID=@ID and IsDelete=0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Page_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Config_Page_SelectAll]
@type int
As
Begin
	Select 
		[ID]
      ,[PID]
      ,[Type]
      ,[Name]
      ,[Content]
      ,[CreateTime]
      ,[IsDelete]
  FROM [goujiuwang_v5_test].[dbo].[Config_Page] where Type=@type and IsDelete=0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Page_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Config_Page_Insert]
	   @PID int
      ,@Type int
      ,@Name varchar(200)
      ,@Content text
      ,@Source varchar(1000)
	  ,@ReferenceID int output
As
Begin
	Insert Into [Config_Page]
		(
      [PID]
      ,[Type] 
      ,[Name] 
      ,[Content]
      ,[Source]
      ,IsDelete
      ,CreateTime
      )
	Values(@PID,@Type,@Name,@Content,@Source,0,GETDATE())

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Page_DeleteRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_Config_Page_DeleteRow]
	@ID int
As
Begin
	Update Config_Page Set IsDelete=255 Where ID=@ID;
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Type_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Invoice_Type_Update]
	@ID int,
	@Name nvarchar(16),
	@Description ntext=default,
	@CreateTime datetime
As
Begin
	Update Config_Invoice_Type
	Set
		[Name] = @Name,
		[Description] = @Description,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Type_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Invoice_Type_SelectAll]
As
Begin
	Select 
		[ID],
		[Name],
		[Description],
		[CreateTime]
	From Config_Invoice_Type
	Where IsDelete =0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Type_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Invoice_Type_Insert]
	@Name nvarchar(16),
	@Description ntext=default,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Config_Invoice_Type
		([Name],[Description],[CreateTime])
	Values
		(@Name,@Description,@CreateTime)

	Select @ReferenceID = @@IDENTITY
	
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Type_DeleteRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Invoice_Type_DeleteRow]
	@ID int
As
Begin

	Update Config_Invoice_Type
	Set IsDelete = 255
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Content_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Invoice_Content_Update]
	@ID int,
	@Name nvarchar(16),
	@Description ntext =default,
	@CreateTime datetime
As
Begin
	Update Config_Invoice_Content
	Set
		[Name] = @Name,
		[Description] = @Description,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Content_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Invoice_Content_SelectAll]
As
Begin
	Select 
		[ID],
		[Name],
		[Description],
		[CreateTime]
	From Config_Invoice_Content
	Where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Content_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Invoice_Content_Insert]
	@Name nvarchar(16),
	@Description ntext=default,
	@CreateTime datetime,
	@ReferenceID int OUTPUT
As
Begin
	Insert Into Config_Invoice_Content
		([Name],[Description],[CreateTime])
	Values
		(@Name,@Description,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Invoice_Content_DeleteRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Invoice_Content_DeleteRow]
	@ID int
As
Begin
	Update Config_Invoice_Content
	Set IsDelete = 255
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Method_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Delivery_Method_Update]
	@ID int,
	@Name nvarchar(16),
	@Description ntext=default,
	@CreateTime datetime
As
Begin
	Update Config_Delivery_Method
	Set
		[Name] = @Name,
		[Description] = @Description,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Method_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Method_SelectAll]
As
Begin
	Select 
		[ID],
		[Name],
		[Description],
		[CreateTime]
	From Config_Delivery_Method
	Where IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Method_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Delivery_Method_Insert]
	@Name nvarchar(50),
	@Description ntext = default,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Config_Delivery_Method
		([Name],[Description],[CreateTime])
	Values
		(@Name,@Description,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Method_DeleteRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Method_DeleteRow]
	@ID int
As
Begin
	Update Config_Delivery_Method
	Set isDelete = 255
	Where
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Cost_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Cost_Update]
	@ID int,
	@DeliveryCorporationID int,
	@CityID int,
	@Duration int,
	@Cost float,
	@CreateTime datetime
As
Begin
	Update Config_Delivery_Cost
	Set
		[DeliveryCorporationID] = @DeliveryCorporationID,
		[CityID] = @CityID,
		[Duration] = @Duration,
		[Cost] = @Cost,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Cost_SelectRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Cost_SelectRow]
	@CorporationID int
As
Begin
	Select 
		[ID],
		[DeliveryCorporationID],
		[CityID],
		[Duration],
		[Cost],
		[CreateTime]
	From Config_Delivery_Cost
	Where isDelete = 0 And [DeliveryCorporationID] = @CorporationID		
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Cost_SelectByCorId]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Cost_SelectByCorId]
	@CorporationID int
As
Begin
	Select 
		[ID],
		[DeliveryCorporationID],
		[CityID],
		[Duration],
		[Cost],
		[CreateTime]
	From Config_Delivery_Cost
	Where IsDelete=0 And [DeliveryCorporationID] = @CorporationID	
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Cost_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Cost_SelectAll]
As
Begin
	Select 
		[ID],
		[DeliveryCorporationID],
		[CityID],
		[Duration],
		[Cost],
		[CreateTime]
	From Config_Delivery_Cost
	Where isDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Cost_Insert]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Config_Delivery_Cost_Insert]
	@DeliveryCorporationID int,
	@CityID int,
	@Duration int,
	@Cost float,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Config_Delivery_Cost
		([DeliveryCorporationID],[CityID],[Duration],[Cost],[CreateTime])
	Values
		(@DeliveryCorporationID,@CityID,@Duration,@Cost,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Cost_DeleteRow]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Cost_DeleteRow]
	@ID int
As
Begin
	Update Config_Delivery_Cost	Set isDelete= 255 Where [ID]=@ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Corporation_Update]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Config_Delivery_Corporation_Update]
	@ID int,
	@Name nvarchar(32),
	@Number varchar(20) =default,
	@Tel varchar(20) =default,
	@URL varchar(64)=default,
	@Description ntext=default,
	@CreateTime datetime
As
Begin
	Update Config_Delivery_Corporation
	Set
		[Name] = @Name,
		[URL] = @URL,
		[Number] = @Number,
		[Tel] = @Tel,
		[Description] = @Description,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Corporation_SelectAll]    Script Date: 03/02/2014 20:58:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Corporation_SelectAll]
As
Begin
	Select 
		[ID]
      ,[Name]
      ,[Tel]
      ,[URL]
      ,[Number]
      ,[Description]
      ,[CreateTime]
      ,[IsDelete]
	From Config_Delivery_Corporation
	Where isDelete =0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Corporation_Insert]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Config_Delivery_Corporation_Insert]
	@Name nvarchar(32),
	@Number varchar(20) =default,
	@Tel varchar(20) =default,
	@URL varchar(64)=default,
	@Description ntext=default,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Config_Delivery_Corporation
		([Name],[Number],[Tel],[URL],[Description],[CreateTime])
	Values
		(@Name,@Number,@Tel,@URL,@Description,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Config_Delivery_Corporation_DeleteRow]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Config_Delivery_Corporation_DeleteRow
-- Author:	张连印
-- Create date:	2013/10/30 9:07:31
-- Description:	This stored procedure is intended for deleting a specific row from sp_Config_Delivery_Corporation_DeleteRow table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Config_Delivery_Corporation_DeleteRow]
	@ID int
As
Begin
	BEGIN TRAN T;
	
	Update Config_Delivery_Cost 
	Set isDelete = 255
	Where DeliveryCorporationID=@ID;
	
	Update Config_Delivery_Corporation
	Set isDelete = 255
	Where [ID] = @ID

	COMMIT TRAN T
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Code_UserIterator_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Code_UserIterator_Update]
	@ID int,
	
	--@Iterator int,
	--@StartTime int
	@UserIterator int
As
Begin
	Update Code
	Set
		--[Iterator] = @Iterator,
		--[StartTime] = @StartTime
		[UserIterator] = @UserIterator
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Code_UserCode_SelectRow]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Code_UserCode_SelectRow]
	@UserCode nvarchar(50)
As
Begin
	Select 
		[ID],
		[UserCode],
		[Business],
		[PrefixName],
		[DateFormat],
		[TransactLength],
		[Transaction],
		[CodeFormat],
		[IsIterator],
		[Iterator],
		[StartTime],
		[UserIterator],
		[ExpireDate] 
	From Code
	Where
		[UserCode] = @UserCode
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Code_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Code_Update]
	@ID int,
	@Business nvarchar(20),
	@PrefixName nvarchar(MAX),
	@DateFormat nvarchar(50),
	@TransactLength int,
	@Transaction nvarchar(MAX),
	@CodeFormat nvarchar(50),
	@IsIterator bit,
	@Iterator int,
	@ExpireDate int
	
As
Begin
	Update Code
	Set
		
		[Business] = @Business,
		[PrefixName] = @PrefixName,
		[DateFormat] = @DateFormat,
		[TransactLength] = @TransactLength,
		[Transaction] = @Transaction,
		[CodeFormat] = @CodeFormat,
		[IsIterator] = @IsIterator,
		[Iterator] = @Iterator,
		[ExpireDate]=@ExpireDate
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Code_StartTime_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Code_StartTime_Update]
	@ID int,
	
    @iterator int,
	@StartTime datetime
	--@UserIterator int
As
Begin
	Update Code
	Set
	
		[StartTime] = @StartTime,
		[Iterator]=@iterator
		--[UserIterator] = @UserIterator
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Code_SelectRow]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Code_SelectRow]
	@ID int
As
Begin
	Select 
		[ID],
		[UserCode],
		[Business],
		[PrefixName],
		[DateFormat],
		[TransactLength],
		[Transaction],
		[CodeFormat],
		[IsIterator],
		[Iterator],
		[StartTime],
		[UserIterator],
		[ExpireDate]
	From Code
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Code_Iterator_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Code_Iterator_Update]
	@ID int,
	@Iterator int
As
Begin
	Update Code
	Set
		Iterator = @Iterator
	Where		
		ID = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Code_Insert]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Code_Insert]
	@UserCode nvarchar(50),
	@Business nvarchar(20),
	@PrefixName nvarchar(MAX),
	@DateFormat nvarchar(50),
	@TransactLength int,
	@Transaction nvarchar(MAX),
	@CodeFormat nvarchar(50),
	@IsIterator bit,
	@Iterator int,
	@StartTime int,
	@UserIterator int,
	@ExpireDate int,
	@ReferenceID int output
As
Begin
	Insert Into Code
		([UserCode],[Business],[PrefixName],[DateFormat],[TransactLength],[Transaction],[CodeFormat],[IsIterator],[Iterator],[StartTime],[UserIterator],[ExpireDate])
	Values
		(@UserCode,@Business,@PrefixName,@DateFormat,@TransactLength,@Transaction,@CodeFormat,@IsIterator,@Iterator,@StartTime,@UserIterator,@ExpireDate)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_City_SelectAll]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_City_SelectAll]
As
Begin
	Select 
		[ID],
		[ProvinceID],
		[Name]
	From City
	Where [IsDelete] = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Channel_GroupBuy_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Procedure [dbo].[sp_Channel_GroupBuy_Update]
	@ProductID int,
	@UserLevelID int,
	@Name nvarchar(128),
	@GBPrice float,
	@TotalNumber int,
	@Introduce ntext,
	@ShowLevel int,
	@StartTime datetime,
	@EndTime datetime,
	@IsShowTime bit,
	@IsOnlinePayment bit,
	@SoldOfReality int,
	@SoldOfVirtual int,
	@PageView int,
	@Status int,
	@CreateTime datetime
As
Begin
	Update Channel_GroupBuy
	Set
		[ProductID] = @ProductID,
		[UserLevelID] = @UserLevelID,
		[Name] = @Name,
		[GBPrice] = @GBPrice,
		[TotalNumber] = @TotalNumber,
		[Introduce] = @Introduce,
		[ShowLevel] = @ShowLevel,
		[StartTime] = @StartTime,
		[EndTime] = @EndTime,
		[IsShowTime] = @IsShowTime,
		[IsOnlinePayment] = @IsOnlinePayment,
		[SoldOfReality] = @SoldOfReality,
		[SoldOfVirtual] = @SoldOfVirtual,
		[PageView] = @PageView,
		[Status] = @Status,
		[CreateTime] = @CreateTime
	Where		
		[ProductID] = @ProductID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Channel_GroupBuy_Status_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Channel_GroupBuy_Status_Update]
	@ProductID int,
	
	@Status int
	
As
Begin
	Update Channel_GroupBuy
	Set
		[Status] = @Status
		
	Where		
		[ProductID] = @ProductID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Channel_GroupBuy_SelectProductId]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Channel_GroupBuy_SelectProductId]
	@ProductID int
As
Begin
	Select 
		[ID],
		[ProductID],
		[UserLevelID],
		[Name],
		[ImageUrl],
		[GBPrice],
		[TotalNumber],
		[Introduce],
		[ShowLevel],
		[StartTime],
		[EndTime],
		[IsShowTime],
		[IsOnlinePayment],
		[SoldOfReality],
		[SoldOfVirtual],
		[PageView],
		[Status],
		[CreateTime]
	From Channel_GroupBuy
	Where IsDelete = 0 And ProductID = @ProductID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Channel_GroupBuy_Insert]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Channel_GroupBuy_Insert]
	@ProductID int,
	@UserLevelID int,
	@Name nvarchar(128),
	@GBPrice float,
	@TotalNumber int,
	@Introduce ntext,
	@ShowLevel int,
	@StartTime datetime,
	@EndTime datetime,
	@IsShowTime bit,
	@IsOnlinePayment bit,
	@SoldOfReality int,
	@SoldOfVirtual int,
	@PageView int,
	@Status int,
	@CreateTime datetime,
	@ReferenceID int out
As
Begin
	Insert Into Channel_GroupBuy
		([ProductID],[UserLevelID],[Name],[GBPrice],[TotalNumber],[Introduce],[ShowLevel],[StartTime],[EndTime],[IsShowTime],[IsOnlinePayment],[SoldOfReality],[SoldOfVirtual],[PageView],[Status],[CreateTime])
	Values
		(@ProductID,@UserLevelID,@Name,@GBPrice,@TotalNumber,@Introduce,@ShowLevel,@StartTime,@EndTime,@IsShowTime,@IsOnlinePayment,@SoldOfReality,@SoldOfVirtual,@PageView,@Status,@CreateTime)

	Select @ReferenceID = @@IDENTITY



End
GO
/****** Object:  StoredProcedure [dbo].[sp_Channel_GroupBuy_Img_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Channel_GroupBuy_Img_Update]
	@ProductID int,
	@ImageUrl nvarchar(128)
As
Begin
	Update Channel_GroupBuy
	Set	
		[ImageUrl] = @ImageUrl
	Where		
		[ProductID] = @ProductID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Channel_GroupBuy_DeleteRow]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Channel_GroupBuy_DeleteRow]
	@ProductID int
As
Begin
	Update Channel_GroupBuy Set IsDelete=255 Where [ProductID]=@ProductID;
End
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInformation_UpdateProductID]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_BrandInformation_UpdateProductID]
@brandId int,
@ProductID varchar(1000)
as
begin
update Brand_Information set ProductID=@ProductID where BrandID=@brandId
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInformation_UpdateByBrandID]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_BrandInformation_UpdateByBrandID]
@BrandID int,
@Title text,
@Introduce text,
@logo varchar(1000),
@ProductID varchar(1000)
as
begin
update Brand_Information set Title=@Title,Introduce=@Introduce,Logo=@logo,ProductID=@ProductID where BrandID=@BrandID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInformation_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_BrandInformation_Update]
@ID int,
@BrandID int,
@Title text,
@Introduce text,
@Logo varchar(1000),
@ProductID varchar(1000)
as
begin
update Brand_Information set BrandID=@BrandID,Title=@Title,Introduce=@Introduce,Logo=@Logo,ProductID=@ProductID where ID=@ID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInformation_SelectByID]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_BrandInformation_SelectByID]
@ID int
as
begin
select ID, BrandID, Title, Introduce, Logo, ProductID, IsDelete, CreateTime from Brand_Information where IsDelete=0 and ID=@ID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInformation_SelectByBrandID]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_BrandInformation_SelectByBrandID]
@BrandID int
as
begin
select ID, BrandID, Title, Introduce, Logo, ProductID, CreateTime from Brand_Information where BrandID=@BrandID and IsDelete=0
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInformation_Insert]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_BrandInformation_Insert]
@BrandID int,
@Title text,
@Introduce text,
@Logo varchar(1000),
@ProductID varchar(1000),
@ReferenceID int out
as 
begin
insert into Brand_Information(BrandID,Title,Introduce,Logo,ProductID,CreateTime,IsDelete) values(@BrandID,@Title,@Introduce,@Logo,@ProductID,GETDATE(),0)
select @ReferenceID=@@IDENTITY
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BrandInformation_BatchInsert]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_BrandInformation_BatchInsert]
@BrandType BrandInformation readonly,
@RowCount int output
as
begin
insert into Brand_Information(BrandID,Title,Introduce,Logo,ProductID,CreateTime)
select BrandID, Title, Introduce,Logo,ProductID,GETDATE() from @BrandType;
select @RowCount=COUNT(*) from @BrandType;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Picture_SelectByProductID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Picture_SelectByProductID]
	@ProductID int
As
Begin
	Select 
		Picture.ID,		
		Picture.[Path],
		Product_Picture.ProductID,
		Product_Picture.PictureID,
		Product_Picture.IsMaster
	From Product_Picture Inner Join Picture On Product_Picture.PictureID = Picture.ID
	Where Product_Picture.IsDelete = 0 And Product_Picture.ProductID = @ProductID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Picture_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_Picture_Insert]
	@ProductID int,
	@PictureID int,
	@IsMaster bit
As
Begin
	If Not Exists(Select * From Product_Picture Where [ProductID] = @ProductID and [PictureID]= @PictureID)
	Begin
	Insert Into Product_Picture
		([ProductID],[PictureID],[IsMaster])
	Values
		(@ProductID,@PictureID,@IsMaster)
	End
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Picture_DeleteByProductID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Picture_DeleteByProductID]
	@ProductID int
As
Begin
	--Delete Product_Picture
	--Where
	--	[ProductID] = @ProductID
	Update Product_Picture Set IsDelete = 255 Where ProductID = @ProductID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Insert]
	@ParentCategoryID int,
	@ProductCategoryID int,
	@ParentBrandID int,
	@ProductBrandID int,
	@Barcode varchar(64),
	@Name nvarchar(128),
	@SEOTitle nvarchar(128),
	@SEOKeywords nvarchar(512),
	@SEODescription nvarchar(512),
	@Advertisement nvarchar(128),
	@MarketPrice float,
	@GoujiuPrice float,
	@Introduce ntext,
	@Integral int,
	@InventoryNumber int,
	@CommentNumber int,
	@SoldOfReality int,
	@SoldOfVirtual int,
	@PageView int,
	@Sorting int,
	@Status int,
	@CreateTime datetime,
	@Attributes text,
	@ReferenceID int output
As
Begin
	Insert Into Product
		([ParentCategoryID],[ProductCategoryID],[ParentBrandID],[ProductBrandID],[Barcode],[Name],[SEOTitle],[SEOKeywords],[SEODescription],[Advertisement],[MarketPrice],[GoujiuPrice],[Introduce],[Integral],[InventoryNumber],[CommentNumber],[SoldOfReality],[SoldOfVirtual],[PageView],[Sorting],[Status],[CreateTime],[Attributes])
	Values
		(@ParentCategoryID,@ProductCategoryID,@ParentBrandID,@ProductBrandID,@Barcode,@Name,@SEOTitle,@SEOKeywords,@SEODescription,@Advertisement,@MarketPrice,@GoujiuPrice,@Introduce,@Integral,@InventoryNumber,@CommentNumber,@SoldOfReality,@SoldOfVirtual,@PageView,@Sorting,@Status,@CreateTime,@Attributes)

	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_DeleteRow]
	@ID int
As
Begin
	--Delete Product
	--Where
	--	[ID] = @ID
	Update Product Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Consult_Reply_Update]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Consult_Reply_Update
-- Author:	张连印
-- Create date:	2013/11/7 17:10:05
-- Description:	This stored procedure is intended for updating Product_Consult_Reply table
-- ==========================================================================================
Create Procedure [dbo].[sp_Product_Consult_Reply_Update]
	@ID int,
	@EmployeeID int,
	@Content nvarchar(256),
	@CreateTime datetime
As
Begin	

	Update Product_Consult_Reply
	Set
	[EmployeeID] = @EmployeeID,
	[Content] = @Content,
	[CreateTime] = @CreateTime
	Where		
	[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Consult_Reply_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Consult_Reply_Update
-- Author:	张连印
-- Create date:	2013/11/7 17:10:05
-- Description:	This stored procedure is intended for updating Product_Consult_Reply table
-- ==========================================================================================
Create Procedure [dbo].[sp_Product_Consult_Reply_Insert]
	@ConsultID int,
	@EmployeeID int,
	@Content nvarchar(256),
	@CreateTime datetime
As
Begin
	declare @Count int =0;
	select @Count=count(distinct ID)
	from Product_Consult_Reply
	where ConsultID=@ConsultID;
	
	if @Count<1 
	begin
		insert into Product_Consult_Reply(ConsultID,EmployeeID,Content,CreateTime)
		Values(@ConsultID,@EmployeeID,@Content, @CreateTime);
	end
	else
	begin
		Update Product_Consult_Reply
		Set
		[EmployeeID] = @EmployeeID,
		[Content] = @Content,
		[CreateTime] = @CreateTime
		Where		
		[ID] = @ConsultID
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Consult_Reply_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Consult_Reply_DeleteRow
-- Author:	张连印
-- Create date:	2013/11/7 17:10:05
-- Description:	This stored procedure is intended for deleting a specific row from Product_Consult_Reply table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Product_Consult_Reply_DeleteRow]
	@ID int
As
Begin
	--Delete Product_Consult_Reply
	--Where
	--	[ID] = @ID
	Update Product_Consult_Reply Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Consult_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Consult_Insert
-- Author:	冯瑶
-- Create date:	2014/2/20 15:49:04
-- Description:	添加商品咨询
-- ==========================================================================================
CREATE Procedure [dbo].[sp_Product_Consult_Insert]
	@ProductID int,
	@UserID int,
	@ConsultPerson nvarchar(16) = null,
	@ConsultPersonMobile varchar(16)=null,
	@ConsultPersonEmail varchar(64)=null,
	@Content nvarchar(256),
	@ReferenceID int out
As
Begin
	Insert Into Product_Consult
		([ProductID],[UserID],[ConsultPerson],[ConsultPersonMobile],[ConsultPersonEmail],[Content])
	Values
		(@ProductID,@UserID,@ConsultPerson,@ConsultPersonMobile,@ConsultPersonEmail,@Content)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Consult_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Consult_DeleteRow
-- Author:	张连印
-- Create date:	2013/11/7 17:10:05
-- Description:	This stored procedure is intended for deleting a specific row from Product_Consult table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Product_Consult_DeleteRow]
	@ID int
As
Begin
	--Delete Product_Consult_Reply
	--where ConsultID=ID;

	--Delete Product_Consult
	--Where
	--	[ID] = @ID;
	
	Update Product_Consult_Reply Set IsDelete = 255 Where ConsultID = @ID
	Update Product_Consult Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comments_SelectByCategory]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_Product_Comments_SelectByCategory]
  @condition nvarchar(1000)
  as
  begin 
  SELECT
  top 10
   Product_comment.[ID]
      ,[ProductID]
      ,[OrderID]
      ,[UserID]
      ,[Score]
      ,[Content]
      ,Product_comment.[Status]
      ,Product_comment.[CreateTime]
      ,[EmployeeID]
      ,[AuditTime]
      ,[AuditDescription]
      ,Product_comment.[IsDelete]
      ,[User].Name  UserName
      ,Product.Name ProductName
  FROM dbo.Product_Comment left join [User] on [User].ID=Product_Comment.UserID left join[Product] on Product.ID=Product_Comment.ProductID  where Product_comment.IsDelete=0 and [ProductID] in (select id from Product where ParentBrandID = @condition)
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comment_UpdateStatus]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Comment_UpdateStatus
-- Author:	张连印
-- Create date:	2013/10/30 9:07:31
-- Description:	This stored procedure is intended for updating Product_Comment table
-- ==========================================================================================
Create Procedure [dbo].[sp_Product_Comment_UpdateStatus]
	@ID int,
	@Status int,
	@AuditDescription nvarchar(128),
	@EmployeeID int,
	@AuditTime datetime
As
Begin
	Update Product_Comment
	Set
		[Status]=@Status,
		EmployeeID=@EmployeeID,
		AuditDescription=@AuditDescription,
		AuditTime=@AuditTime
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comment_Reply_Update]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Comment_Reply_Update
-- Author:	张连印
-- Create date:	2013/11/7 17:08:23
-- Description:	This stored procedure is intended for updating Product_Comment_Reply table
-- ==========================================================================================
Create Procedure [dbo].[sp_Product_Comment_Reply_Update]
	@ID int,
	@CommentID int,
	@UserID int,
	@ParentID int,
	@Content nvarchar(256),
	@CreateTime datetime
As
Begin
	Update Product_Comment_Reply
	Set
		[CommentID] = @CommentID,
		[UserID] = @UserID,
		[ParentID] = @ParentID,
		[Content] = @Content,
		[CreateTime] = @CreateTime
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comment_Reply_SelectAll]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Comment_Reply_SelectAll]
	@ID int
As
Begin
	SELECT R.ID [ID] 
      ,R.[CommentID]
      ,R.[UserID]
      ,U.Name UserName
      ,(case R.parentId when 0 then PC.UserID else TR.UserID end) as ToUserID
      ,(case R.parentId when 0 then PC.UserName else TR.UserName end) as ToUserName
      ,R.[ParentID]
      ,R.[Content]      
      ,R.[CreateTime] CreateTime
  FROM [Product_Comment_Reply] R join [User] U
  on R.UserID=U.ID  
  left Join (select P.ID ID, P.UserID UserID, [User].Name UserName From Product_Comment_Reply P join [User] on P.UserID=[User].ID) TR on TR.ID=R.ParentID
  left Join (select P.ID ID, P.UserID UserID, [User].Name UserName From Product_Comment P join [User] on P.UserID=[User].ID)  PC on PC.ID=R.CommentID
  where R.IsDelete = 0 And R.CommentID = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comment_Reply_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Comment_Reply_Insert
-- Author:	fy
-- Create date:	2014/2/18 14:46:46
-- Description:	添加商品评价的回复
-- ==========================================================================================
CREATE Procedure [dbo].[sp_Product_Comment_Reply_Insert]
	@CommentID int,
	@UserID int,
	@ParentID int,
	@Content nvarchar(256),
	@ReferenceID int out
As
Begin
	Insert Into Product_Comment_Reply
		([CommentID],[UserID],[ParentID],[Content])
	Values
		(@CommentID,@UserID,@ParentID,@Content)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comment_Reply_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Comment_Reply_DeleteRow
-- Author:	张连印
-- Create date:	2013/11/7 17:08:23
-- Description:	This stored procedure is intended for deleting a specific row from Product_Comment_Reply table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Product_Comment_Reply_DeleteRow]
	@ID int
As
Begin
	--Delete Product_Comment_Reply
	--Where
	--	[ID] = @ID
	Update Product_Comment_Reply Set IsDelete = 255 Where [ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comment_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Comment_Insert
-- Author:	冯瑶
-- Create date:	2014/2/18 16:35:36
-- Description:	商品评论
-- ==========================================================================================
Create Procedure [dbo].[sp_Product_Comment_Insert]
	@ProductID int,
	@OrderID int,
	@UserID int,
	@Score int,
	@Content nvarchar(256),
	@Status int,
	@ReferenceID int out
As
Begin
	Insert Into Product_Comment
		([ProductID],[OrderID],[UserID],[Score],[Content],[Status])
	Values
		(@ProductID,@OrderID,@UserID,@Score,@Content,@Status)

	
	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Comment_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Product_Comment_DeleteRow
-- Author:	张连印
-- Create date:	2013/11/4 9:58:34
-- Description:	This stored procedure is intended for deleting a specific row from Product_Comment table
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Product_Comment_DeleteRow]
	@ID int
As
Begin
--	Begin Tran T
--		Delete Product_Comment_Reply
--		Where
--			CommentID = @ID
--		
--		Delete Product_Comment
--		Where
--			[ID] = @ID
--
--	Commit Tran T

		Update Product_Comment_Reply Set IsDelete = 255 Where CommentID = @ID
		Update Product_Comment Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Category_Update]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_Category_Update]
	@ID int,
	@ParentID int,
	@CategoryName nvarchar(16),
	@CategoryNameSpell nvarchar(32),
	@CategoryNameEnglish nvarchar(32),
	@SEOKeywords nvarchar(512),
	@SEODescription nvarchar(512),
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
/****** Object:  StoredProcedure [dbo].[sp_Product_Category_SelectByParentID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Category_SelectByParentID]
	@ParentID int
As
Begin
	Select 
		[ID],
		[ParentID],
		[CategoryName],
		[CategoryNameSpell],
		[CategoryNameEnglish],
		[SEOKeywords],
		[SEODescription],
		[IsGjw],
		[IsDisplay],
		[Layer],
		[Sorting],
		[CreateTime]
	From Product_Category
	Where IsDelete = 0 And [ParentID] = @ParentID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Category_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_Category_Insert]
	@ParentID int,
	@CategoryName nvarchar(16),
	@CategoryNameSpell nvarchar(32),
	@CategoryNameEnglish nvarchar(32),
	@SEOKeywords nvarchar(512),
	@SEODescription nvarchar(512),
	@IsGjw bit,
	@IsDisplay bit,
	@Layer int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Product_Category
		([ParentID],[CategoryName],[CategoryNameSpell],[CategoryNameEnglish],[SEOKeywords],[SEODescription],[IsGjw],[IsDisplay],[Layer],[Sorting],[CreateTime])
	Values
		(@ParentID,@CategoryName,@CategoryNameSpell,@CategoryNameEnglish,@SEOKeywords,@SEODescription,@IsGjw,@IsDisplay,@Layer,null,@CreateTime)  

	set @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Category_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Category_DeleteRow]
	@ID int
As
Begin
	--Delete Product_Category
	--Where
	--	[ID] = @ID or ParentID = @ID
	Update Product_Category Set IsDelete = 255 Where ID = @ID Or ParentID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_Update]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_Brand_Update]
	@ID int,
	@ProductCategoryID int,
	@ParentID int,
	@BrandName nvarchar(16),
	@BrandNameSpell nvarchar(32),
	@BrandNameEnglish nvarchar(32),
	@SEOKeywords nvarchar(512),
	@SEODescription nvarchar(512),
	@IsDisplay bit,
	@Sorting int
As
Begin
	Update Product_Brand
	Set
		[ProductCategoryID] = @ProductCategoryID,
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
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_Tree]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_Product_Brand_Tree]
As
Begin
	Select * From (
	Select 'c' + Convert(Varchar(50), id) As id, Convert(Varchar(50), parentid) As pid, categoryname As name 
	From product_category Where IsDelete = 0
	Union
	Select 'bp' + Convert(Varchar(50), id) As id, 'c' + Convert(Varchar(50), productcategoryid) As pid, brandname As name 
	From product_brand Where IsDelete = 0 And Layer = 1 ) a
	Order By pid
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_SelectByParentID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Brand_SelectByParentID]
	@ParentID int
As
Begin
	Select 
		[ID],
		[ProductCategoryID],
		[ParentID],
		[BrandName],
		[BrandNameSpell],
		[BrandNameEnglish],
		[SEOKeywords],
		[SEODescription],
		[IsDisplay],
		[Layer],
		[Sorting],
		[CreateTime]
	From Product_Brand
	Where IsDelete = 0 And [ParentID] = @ParentID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_SelectByCategoryID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Brand_SelectByCategoryID]
	@CategoryID int,
	@ParentID int
As
Begin
	Select 
		[ID],
		[ProductCategoryID],
		[ParentID],
		[BrandName],
		[BrandNameSpell],
		[BrandNameEnglish],
		[SEOKeywords],
		[SEODescription],
		[IsDisplay],
		[Layer],
		[Sorting],
		[CreateTime]
	From Product_Brand
	Where IsDelete = 0 And [ProductCategoryID] = @CategoryID And [ParentID] = @ParentID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_Brand_Insert]
	@ProductCategoryID int,
	@ParentID int,
	@BrandName nvarchar(16),
	@BrandNameSpell nvarchar(32),
	@BrandNameEnglish nvarchar(32),
	@SEOKeywords nvarchar(512),
	@SEODescription nvarchar(512),
	@IsDisplay bit,
	@Layer int,
	@ReferenceID int output
As
Begin
	Insert Into Product_Brand
		([ProductCategoryID],[ParentID],[BrandName],[BrandNameSpell],[BrandNameEnglish],[SEOKeywords],[SEODescription],[IsDisplay],[Layer],[Sorting],[CreateTime])
	Values
		(@ProductCategoryID,@ParentID,@BrandName,@BrandNameSpell,@BrandNameEnglish,@SEOKeywords,@SEODescription,@IsDisplay,@Layer,null,GETDATE())

	set @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Brand_DeleteRow]
	@ID int
As
Begin
	--Delete Product_Brand
	--Where
	--	[ID] = @ID or ParentID = @ID
	Update Product_Brand Set IsDelete = 255 Where ID = @ID Or ParentID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Brand_ById]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_Product_Brand_ById]
  @ID int
  as
  begin
  select 
       [ID]
      ,[ProductCategoryID]
      ,[ParentID]
      ,[BrandName]
      ,[BrandNameSpell]
      ,[BrandNameEnglish]
      ,[SEOKeywords]
      ,[SEODescription]
      ,[IsDisplay]
      ,[Layer]
      ,[Sorting]
      ,[CreateTime]
      ,[IsDelete]
      from
      Product_Brand
      where 
      IsDelete=0 
      and
      ID=@ID
      end
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_AttributeValueSet_SelectByProductID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_AttributeValueSet_SelectByProductID]
	@ProductID int
As
Begin
	Select 
		[ID],
		[ProductID],
		[AttributeID],
		[AttributeValueID]
	From Product_AttributeValueSet
	Where IsDelete = 0 And [ProductID] = @ProductID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_AttributeValueSet_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_AttributeValueSet_Insert]
	@ProductID int,
	@AttributeID int,
	@AttributeValueID int
As
Begin
	If Not Exists(Select * From Product_AttributeValueSet Where [ProductID]=@ProductID And [AttributeID]=@AttributeID And [AttributeValueID] = @AttributeValueID)
	Begin
	Insert Into Product_AttributeValueSet
		([ProductID],[AttributeID],[AttributeValueID])
	Values
		(@ProductID,@AttributeID,@AttributeValueID)
	End
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_AttributeValueSet_DeleteByProductID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_AttributeValueSet_DeleteByProductID]
	@ProductID int
As
Begin
	--Delete Product_AttributeValueSet
	--Where
	--	[ProductID] = @ProductID
	Update Product_AttributeValueSet Set IsDelete = 255 Where ProductID = @ProductID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_AttributeValue_Update]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_AttributeValue_Update]
	@ID int,
	@AttributeValue nvarchar(100),
	@Sorting int,
	@IsDefault int
As
Begin
if @IsDefault=1
begin
update Product_AttributeValue set IsDefault=0 where ID=@ID
end
Update Product_AttributeValue
	Set
		[AttributeValue] = @AttributeValue,
		[Sorting] = @Sorting,
		[IsDefault] =@IsDefault
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_AttributeValue_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_AttributeValue_Insert]
	@AttributeID int,
	@AttributeValue nvarchar(64),
	@Sorting int,
	@IsDefault int,
	@ReferenceID int out
As
Begin
if @IsDefault=1
begin
update Product_AttributeValue set IsDefault=0 where AttributeID=@AttributeID
end
	Insert Into Product_AttributeValue
		([AttributeID],[AttributeValue],[Sorting],[CreateTime],[IsDefault])
	Values
		(@AttributeID,@AttributeValue,@Sorting,GETDATE(),1)

	Select @ReferenceID = @@IDENTITY

	Return @ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_AttributeValue_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_AttributeValue_DeleteRow]
	@ID int
As
Begin
	--Delete Product_AttributeValue
	--Where
	--	[ID] = @ID
	Update Product_AttributeValue Set IsDelete = 255 Where ID = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Attribute_Update]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_Attribute_Update]
	@ID int,
	@AttributeName nvarchar(20),
	@Sorting int,
	@DataLength int,
	@InputType nvarchar(50),
	@DataType nvarchar(50)
As
Begin
	Update Product_Attribute
	Set
		[AttributeName] = @AttributeName,
		[Sorting] = @Sorting,
		[DataLength] =@DataLength,
		[InputType] =@InputType,
		[DataType] =@DataType,
		[AttributeCode]=dbo.fn_GetQuanPin(@AttributeName)
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Attribute_SelectByCategoryID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Attribute_SelectByCategoryID]
	@ProductCategoryID int
As
Begin
	Select
	'{ '+
	'"ID": ' + Cast(a.ID As Varchar(50)) + ', ' +
	'"AttributeCode": "' + Coalesce(a.[AttributeCode],'') + '", ' +
	'"AttributeName": "' + Coalesce(a.[AttributeName],'') + '", ' + 
	'"InputType": "' + Coalesce(a.[InputType],'') + '", ' +
	'"DataType": "' + Coalesce(a.[DataType],'') + '", ' +
	'"DataLength": ' + Coalesce(Cast(a.[DataLength] As Varchar(50)),'0') + ', ' +
	'"AttributeValues": [' + Coalesce(REPLACE(
					(SELECT 
					'{"ID":' + Cast(b.ID as Varchar(50)) + ',' +
					'"Value":"' + Coalesce(b.[AttributeValue], '') + '",' +  
					'"IsDefault":' + Coalesce(Cast(b.IsDefault as Varchar(50)),'0') + '}'
					AS [data()]
	                FROM [Product_AttributeValue] AS b 
	                WHERE b.AttributeID = a.ID 
	                Order By b.sorting FOR XML PATH('')) , ' ', ','),'')+']'+
	' }'               
	From [Product_Attribute] a
	Where a.[ProductCategoryID] = @ProductCategoryID Or a.[ProductCategoryID] In
	(
		Select c.ParentID from Product_Category c Where c.ID =  @ProductCategoryID
	)
	Order By a.Sorting
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Attribute_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_Attribute_Insert]
	@ProductCategoryID int,
	@AttributeName nvarchar(20),
	@DataLength int,
	@DataType nvarchar(50),
	@InputType nvarchar(50),
	@ReferenceID int out
As
Begin
	Insert Into Product_Attribute
		([ProductCategoryID],[AttributeName],[Sorting],[CreateTime],DataType,[DataLength],[InputType],[AttributeCode])
	Values
		(@ProductCategoryID,@AttributeName,null,GETDATE(),@DataType ,@DataLength,@InputType ,dbo.fn_GetQuanPin(@AttributeName))

	set @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Attribute_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Product_Attribute_DeleteRow]
	@ID int
As
Begin
	--Delete Product_Attribute Where ID = @ID
	--Delete Product_AttributeValue Where AttributeID = @ID
	Update Product_Attribute Set IsDelete = 255 Where ID = @ID
	Update Product_AttributeValue Set IsDelete = 255 Where AttributeID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Picture_UpdatePictureCategory]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Picture_UpdatePictureCategory]
	@ID int,
	@ParentCategoryID int,
	@ProductCategoryID int,
	@ParentBrandID int,
	@ProductBrandID int
As
Begin
	Update Picture
	Set
		[ParentCategoryID] = @ParentCategoryID,
		[ProductCategoryID] = @ProductCategoryID,
		[ParentBrandID] = @ParentBrandID,
		[ProductBrandID] = @ProductBrandID
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Picture_SelectPreview]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Picture_SelectPreview]
	@pictureId int
As
Begin
--declare @ProductID int
--select @ProductID = ProductID from Product_Picture where PictureID = @pictureId
--	Select p.[id],[ProductBrandID],[Name],[Type],[Path],[Size],[Height],[Width],[Status],[UploadTime],[CreateTime]
--	From Picture as p
--	inner join Product_Picture as pp on p.ID = pp.PictureID
--	Where
--		[pp].ProductID = @ProductID
	
	-- modify by caiyp 2013.12.31
	Select Picture.[id],
		Picture.[ProductBrandID],
		Picture.[Name],
		Picture.[Type],
		Picture.[Path],
		Picture.[Size],
		Picture.[Height],
		Picture.[Width],
		Picture.[Status],
		Picture.[UploadTime],
		Picture.[CreateTime]
	From Picture Inner Join Product_Picture On Picture.ID = Product_Picture.PictureID
	Where Product_Picture.ProductID In(
		Select ProductID From Product_Picture Where PictureID = @pictureId
	)
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Picture_SelectByProductID]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Picture_SelectByProductID]
@ProductID int
as
begin
	Select
	  Picture.ID,
	  Picture.ParentCategoryID,
	  Picture.ProductCategoryID,
	  Picture.ParentBrandID,
	  Picture.ProductBrandID,
	  Picture.Name,
	  Picture.[Path],
	  Picture.ThumbnailPath,
	  Picture.FileName,
	  Picture.Type,
	  Picture.[Size],
	  Picture.Height,
	  Picture.Width,
	  Picture.Status,
	  Picture.UploadTime,
	  Picture.CreateTime
	From Picture,Product_Picture
	Where Picture.IsDelete = 0 And Picture.ID = Product_Picture.PictureID And Product_Picture.ProductID = @ProductID
	Order By Product_Picture.IsMaster
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Picture_Insert]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Picture_Insert
-- Author:	Mehdi Keramati
-- Create date:	2013/12/5 15:50:29
-- Description:	This stored procedure is intended for inserting values to Picture table
-- ==========================================================================================
CREATE Procedure [dbo].[sp_Picture_Insert]
	@ParentCategoryID int,
	@ProductCategoryID int,
	@ParentBrandID int,
	@ProductBrandID int,
	@Name varchar(64),
	@Path varchar(128),
	@ThumbnailPath varchar(128),
	@FileName varchar(64),
	@Type varchar(8),
	@Size int,
	@Height int,
	@Width int,
	@Status int,
	@UploadTime datetime,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Picture
		([ParentCategoryID],[ProductCategoryID],[ParentBrandID],[ProductBrandID],[Name],[Path],[ThumbnailPath],[FileName],[Type],[Size],[Height],[Width],[Status],[UploadTime],[CreateTime])
	Values
		(@ParentCategoryID,@ProductCategoryID,@ParentBrandID,@ProductBrandID,@Name,@Path,@ThumbnailPath,@FileName,@Type,@Size,@Height,@Width,@Status,@UploadTime,@CreateTime)

	Select @ReferenceID = @@IDENTITY
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Picture_DeleteRow]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Picture_DeleteRow]
	@ID int
As
Begin
	Update Picture set [IsDelete] = 255 Where [ID] = @ID;
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_UpdateStatus]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_UpdateStatus
-- Author:	张连印
-- Description:	修改订单的状态
-- ==========================================================================================
Create Procedure [dbo].[sp_Order_UpdateStatus]
	@ID int,
	@Status int,
	@Description nvarchar(512)
As
Begin
	Update [Order]
	Set
		[Status] = @Status,
		[Description] = @Description
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_UpdateForConfirmOrder]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================  
-- Entity Name: sp_Order_UpdateForConfirmOrder  
-- Author: 张连印  
-- Description: 确认订单  
-- ==========================================================================================  
CREATE Procedure [dbo].[sp_Order_UpdateForConfirmOrder]  
 @ID int,  
 @CpsID int,  
 @DeliveryCost float,  
 @TotalMoney float,  
 @TotalIntegral int,  
 @IsRequireInvoice bit,  
 @Description nvarchar(512) = default  
As  
Begin  
 Update [Order]  
 Set  
  [CpsID] = @CpsID,  
  [DeliveryCost] = @DeliveryCost,  
  [TotalMoney] = @TotalMoney,  
  [TotalIntegral] = @TotalIntegral,  
  [IsRequireInvoice] = @IsRequireInvoice,  
  [Status] = 1,  
  [Description] = @Description  
 Where    
  [ID] = @ID    
  
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_UpdateDescription]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_UpdateDescription
-- Author:	张连印
-- Description:	更新订单备注信息存储过程
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_UpdateDescription]
	@ID int,
	@Description nvarchar(512)
As
Begin
	Update [Order]
	Set
		[Description] = @Description
	Where		
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Update]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_Update
-- Author:	张连印
-- Description:	This stored procedure is intended for updating Order table
-- ==========================================================================================
CREATE Procedure [dbo].[sp_Order_Update]
	@ID int,
	@UserID int,
	@RecieveAddressID int,
	@CpsID int,
	@PaymentMethodID int,
	@OrderCode varchar(64),
	@OrderNumber varchar(16),
	@DeliveryCost float,
	@DeliveryCorporationID int=default,
	@TotalMoney float,
	@TotalIntegral int,
	@PaymentStatus int,
	@IsRequireInvoice bit,
	@Status int,
	@Description nvarchar(512)=default,
	@Remark nvarchar(512)=default
As
Begin
	Update [Order]
	Set
		[UserID] = @UserID,
		[RecieveAddressID] = @RecieveAddressID,
		[CpsID] = @CpsID,
		[PaymentMethodID] = @PaymentMethodID,
		[OrderCode] = @OrderCode,
		[OrderNumber] = @OrderNumber,
		[DeliveryCost] = @DeliveryCost,
		[DeliveryCorporationID] = @DeliveryCorporationID,
		[TotalMoney] = @TotalMoney,
		[TotalIntegral] = @TotalIntegral,
		[PaymentStatus] = @PaymentStatus,
		[IsRequireInvoice] = @IsRequireInvoice,
		[Status] = @Status,
		[Description] = @Description,
		[Remark] = @Remark
	Where		
		[ID] = @ID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Status_Tracking_SelectByOrderID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Status_Tracking_SelectByOrderID
-- Author:	张连印
-- Description:	根据订单编码查询订单状态变化信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Status_Tracking_SelectByOrderID]
	@OrderID int
As
Begin
	Select 
		[ID]
      ,[OrderID]
      ,[EmployeeID]
      ,[UserID]
      ,[Remark]
      ,[Status]
      ,[MailNo]
      ,[ExpressNumber]
      ,[CreateTime]
	From Order_Status_Tracking
	Where
		[OrderID] = @OrderID
	And IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Status_Tracking_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Status_Tracking_Insert
-- Author:	张连印
-- Description:	插入一条订单状态跟踪信息
-- ==========================================================================================
CREATE Procedure [dbo].[sp_Order_Status_Tracking_Insert]
	@EmployeeID int  = default,
	@UserID int = default,
	@OrderID int,
	@Remark nvarchar(64),
	@Status int,
	@ExpressNumber varchar(20)=default,
	@MailNo varchar(20) =default,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Order_Status_Tracking
		([EmployeeID],[UserID],[OrderID],[Remark],[Status],[ExpressNumber],[MailNo],[CreateTime])
	Values
		(@EmployeeID,@UserID,@OrderID,@Remark,@Status,@ExpressNumber,@MailNo,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Status_Log_SelectByOrderID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Status_Log_SelectByOrderID
-- Author:	张连印
-- Description:	根据订单编码查询订单状态变化信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Status_Log_SelectByOrderID]
	@OrderID int
As
Begin
	Select 
		[ID],
		[OrderID],
		[EmployeeID],
		[Status],
		[Remark],
		[CreateTime]
	From Order_Status_Log
	Where
		[OrderID] = @OrderID
	And [IsDelete] = 0
	Order by CreateTime asc
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Status_Log_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:    sp_Order_Status_Log_Insert
-- Author:	张连印
-- Description:	插入订单状态记录
-- ==========================================================================================
Create Procedure [dbo].[sp_Order_Status_Log_Insert]
	@OrderID int,
	@EmployeeID int,
	@Status int,
	@Remark nvarchar(512),
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Order_Status_Log
		([OrderID],[EmployeeID],[Status],[Remark],[CreateTime])
	Values
		(@OrderID,@EmployeeID,@Status,@Remark,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_SelectByUserID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_SelectByUserID
-- Author:	张连印
-- Description:	根据用户编码查询订单信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_SelectByUserID]
	@UserID int
As
Begin
	Select 
		O.[ID] [ID],
		O.[UserID] as [UserID],
		U.Mobile as [UserMobile],
		U.Email as [UserEmail],
		U.Name as [UserName],
		[RecieveAddressID],
		[Consignee],		
		O.[CpsID] as [CpsID],
		Cps.Name as [CpsName],		
		[PaymentMethodID],
		[OrderCode],
		[OrderNumber],
		[DeliveryCost],
		[DeliveryCorporationID],
		[TotalMoney],
		[Discount],
		[TotalIntegral],
		[PaymentStatus],
		[IsRequireInvoice],
		O.[Status] as [Status],
		[Description],
		[Remark],
		O.[CreateTime] as [CreateTime]
	From [Order] O 
	Left Join [User] U On O.UserID = U.ID
	Left Join  User_RecieveAddress URA On URA.ID = O.RecieveAddressID
	Left Join Cps On O.CpsID = Cps.ID
	Where
		O.[UserID] = @UserID
	And O.IsDelete = 0
	Order By ID desc
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_SelectByOrderCode]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_SelectByOrderCode
-- Author:	张连印
-- Description:	根据用户编码查询订单信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_SelectByOrderCode]
	@OrderCode varchar(64)
As
Begin
	Select 
		O.[ID] [ID],
		O.[UserID] as [UserID],
		U.Mobile as [UserMobile],
		U.Email as [UserEmail],
		U.Name as [UserName],
		[RecieveAddressID],
		[Consignee],		
		O.[CpsID] as [CpsID],
		Cps.Name as [CpsName],		
		[PaymentMethodID],
		[OrderCode],
		[OrderNumber],
		[DeliveryCost],
		[DeliveryCorporationID],
		[TotalMoney],
		[Discount],
		[TotalIntegral],
		[PaymentStatus],
		[IsRequireInvoice],
		O.[Status] as [Status],
		[Description],
		[Remark],
		O.[CreateTime] as [CreateTime]
	From [Order] O 
	Left Join [User] U On O.UserID = U.ID
	Left Join  User_RecieveAddress URA On URA.ID = O.RecieveAddressID
	Left Join Cps On O.CpsID = Cps.ID
	Where
		O.OrderCode = @OrderCode
	And O.IsDelete = 0
	Order By ID desc
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_SelectById]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_SelectById
-- Author:	张连印
-- Description:	根据订单编码查询订单信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_SelectById]
	@ID int
As
Begin
	Select 
		O.[ID] [ID],
		O.[UserID] as [UserID],
		U.Mobile as [UserMobile],
		U.Email as [UserEmail],
		U.Name as [UserName],
		[RecieveAddressID],
		[Consignee],		
		O.[CpsID] as [CpsID],
		Cps.Name as [CpsName],		
		[PaymentMethodID],
		[OrderCode],
		[OrderNumber],
		[DeliveryCost],
		[DeliveryCorporationID],
		[TotalMoney],
		[Discount],
		[TotalIntegral],
		[PaymentStatus],
		[IsRequireInvoice],
		O.[Status] as [Status],
		[Description],
		[Remark],
		O.[CreateTime] as [CreateTime]
	From [Order] O 
	Left Join [User] U On O.UserID = U.ID
	Left Join  User_RecieveAddress URA On URA.ID = O.RecieveAddressID
	Left Join Cps On O.CpsID = Cps.ID
	Where
		O.[ID] = @ID
	And O.IsDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_SelectCount]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		fy
-- Create date: <Create Date,,>
-- Description:	查询当前会员编号购买过当前商品次数
-- =============================================
CREATE PROCEDURE [dbo].[sp_Order_Product_SelectCount]
	@UserID int,
	@ProductID int
AS
BEGIN
	select COUNT(1) from Order_Product
	left join [Order] on Order_Product.OrderID = [Order].ID
	where Order_Product.ProductID=@ProductID  
	AND [Order].UserID=@UserID 
	AND[Order].[Status] = 3 --已签收的
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_SelectByOrderId]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name: sp_Order_Product_SelectByOrderId
-- Author: 张连印
-- Description:	根据订单编码查询对应商品信息
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Product_SelectByOrderId]
@OrderId int
As
Begin
	Select distinct
		OP.[ID],
		OrderID,
		P.ID ProductID,
		p.Barcode Barcode,
		P.Name ProductName,
		P.InventoryNumber InventoryNumber,
		Pic.[Path] [Path],
		Quantity,
		P.MarketPrice MarkerPrice,
		P.GoujiuPrice GoujiuPrice,  
		TransactPrice,
		OP.CreateTime Createtime
	From Order_Product OP join Product P On OP.ProductID = P.ID And P.isDelete = 0
	left JOin Product_Picture PPic On PPic.ProductID=P.ID and PPic.IsMaster = 1 And PPic.isDelete = 0
	Left Join Picture Pic On Pic.ID = PPic.PictureID And Pic.isDelete = 0
	Where OP.OrderID = @OrderId And Op.isDelete = 0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_Promote_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_Product_Promote_Insert
-- Author:	张连印
-- Create date:	2014/2/17 16:51:37
-- Description:	This stored procedure is intended for inserting values to Order_Product_Promote table
-- ==========================================================================================
Create Procedure [dbo].[sp_Order_Product_Promote_Insert]
	@OrderID int,
	@OrderProductID int,
	@PromoteType int,
	@PromoteID int,
	@Remark nvarchar(256),
	@ExtField nvarchar(512),
	@CreateTime datetime,
	@ReferenceID int
As
Begin
	Insert Into Order_Product_Promote
		([OrderID],[OrderProductID],[PromoteType],[PromoteID],[Remark],[ExtField],[CreateTime])
	Values
		(@OrderID,@OrderProductID,@PromoteType,@PromoteID,@Remark,@ExtField,@CreateTime)

	Select @ReferenceID = @@IDENTITY

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_Promote_BatchInsert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Entity Name:	sp_Order_Product_Promote_BatchInsert
-- Author:	张连印
-- Create date:	2014/2/17 16:51:37
-- Description:	This stored procedure is intended for inserting values to Order_Product_Promote table
-- ==========================================================================================
CREATE Procedure [dbo].[sp_Order_Product_Promote_BatchInsert]
	@OPP OrderProductPromoteTable readonly,
	@Count int output
As
Begin
	Insert Into Order_Product_Promote
		([OrderID],[ProductID],[OrderProductID],[PromoteDiscount],[PromoteType],[PromoteID],[Remark],[ExtField],[CreateTime])
	Select [OrderID],[ProductID],[OrderProductID],[PromoteDiscount],[PromoteType],[PromoteID],[Remark],[ExtField],[CreateTime]
	From @Opp	
	
	declare @orderID int
	declare @createTime datetime
	
	Select top 1 @orderID= O.OrderID,@createTime = O.CreateTime from @Opp O
	
	--处理团购商品数据
	Insert Into Order_Product_Promote
		([OrderID],[ProductID],[OrderProductID],[PromoteDiscount],[PromoteType],[PromoteID],[Remark],[ExtField],[CreateTime])
	Select @orderID,[IndexID],0,0,2,20,'团购商品',null,@createTime
	From Advertise_Config A Where PID=20 and Source = 1 and IndexID in (Select distinct [OrderProductID] from @Opp O)

	Select @Count = COUNT(*) from @Opp;

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_Insert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Product_Insert
-- Author:	张连印
-- Description:	插入一条订单商品记录
-- ==========================================================================================
CREATE Procedure [dbo].[sp_Order_Product_Insert]
	@OrderID int,
	@ProductID int,
	@TransactPrice float,
	@GoujiuPrice float=default,
	@Quantity int,
	@CreateTime datetime,
	@ReferenceID int output
As
Begin
	Insert Into Order_Product
		([OrderID],[ProductID],[Quantity],[TransactPrice],GoujiuPrice,[CreateTime])
	Values
		(@OrderID,@ProductID,@Quantity,@TransactPrice,@GoujiuPrice,@CreateTime)

	Select @ReferenceID = @@IDENTITY	
End
GO
/****** Object:  UserDefinedFunction [dbo].[CreateOrderNumber]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[CreateOrderNumber]()
RETURNS char(14)
AS
BEGIN
	DECLARE @dateTime CHAR(8)
	SELECT @dateTime = CONVERT(CHAR(8), GETDATE(), 112)
	RETURN(
		SELECT @dateTime + RIGHT(1000001 + ISNULL(RIGHT(MAX(OrderNumber),6),0),6) 
		FROM [Order] WITH(XLOCK, PAGLOCK)
		WHERE OrderNumber like @dateTime + '%')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_AdvertiseConfig_BatchInsert]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_AdvertiseConfig_BatchInsert]
@TVP AdvertiseType readonly,
@RowCount int output
as
begin
insert into Advertise_Config(PID, Name, Source, URL,CreateTime)
select PID, Name, '1', '', GETDATE() from @TVP;
select @RowCount=COUNT(*) from @TVP;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_Update]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Advertise_Config_Update]
	@Name nvarchar(50),
	@Source nvarchar(50),
	@URL varchar(500),
	@ImagePath varchar(500),
	@Description varchar(100),
	@ID int,
	@IndexID int,
	@Width int,
	@Height int,
	@ThumbnailImagePath varchar(500),
	@ImageID int,
	@BackgroundColor varchar(50)
As
Begin
	Update Advertise_Config Set
      [Name]=@Name
      ,[Source]=@Source
      ,[URL]=@URL
      ,[ImagePath]=@ImagePath
      ,[Description]=@Description
      ,[IndexID] = @IndexID
      ,[Width] = @Width
	  ,[Height] = @Height
	  ,[ThumbnailImagePath] = @ThumbnailImagePath
	  ,[ImageID] = @ImageID
	  ,[BackgroundColor] = @BackgroundColor
	Where ID=@ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_Up]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_Advertise_Config_Up]
@ID int
as
begin
declare @Sort int
declare @thatId int
declare @thatSort int
select @Sort=Isorder from Advertise_Config where ID=@ID
select top 1 @thatId=ID,@thatSort=IsOrder from Advertise_Config where IsOrder<@Sort order by IsOrder desc
update Advertise_Config set IsOrder=@Sort where ID=@thatId
update Advertise_Config set IsOrder=@thatSort where ID=@ID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_SelectPID]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Advertise_Config_SelectPID]
 @pId int
As
Begin
	Select 
		[ID],
		[PID],
		[Name],
		[Source],
		[URL],
		[ImagePath],
		[Description],
		[Enabled],
		[CreateTime],
		[IsDelete],
		[IndexID],
		[Width],
		[Height],
		[ThumbnailImagePath],
		[ImageID],
		[BackgroundColor]
	From Advertise_Config where PID=@pId
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_SelectID]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Advertise_Config_SelectID]
 @Id int
As
Begin
	Select 
		[ID],
		[PID],
		[Name],
		[Source],
		[URL],
		[ImagePath],
		[Description],
		[Enabled],
		[CreateTime],
		[IsDelete],
		[IndexID],
		[Width],
		[Height],
		[ThumbnailImagePath],
		[ImageID],
		[BackgroundColor]
	From Advertise_Config where ID=@Id
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_SelectAll]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Advertise_Config_SelectAll]
As
Begin
	Select 
		[ID],
		[PID],
		[Name],
		[Source],
		[URL],
		[ImagePath],
		[Description],
		[Enabled],
		[CreateTime],
		[IsDelete],
		[IndexID],
		[Width],
		[Height],
		[ThumbnailImagePath],
		[ImageID],
		[BackgroundColor]
	From Advertise_Config where IsDelete=0
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_Insert]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Advertise_Config_Insert]
	@PID int,
	@Name nvarchar(50),
	@Source nvarchar(50),
	@URL varchar(500),
	@ImagePath varchar(500),
	@Description varchar(100),	
	@CreateTime datetime,
	@IndexID int,
	@Width int,
	@Height int,
	@ThumbnailImagePath varchar(500),
	@ImageID int,
	@BackgroundColor varchar(50),
	@ReferenceID int output
As
Begin
	Insert Into Advertise_Config
		(
      [PID]
      ,[Name]
      ,[Source]
      ,[URL]
      ,[ImagePath]
      ,[Description]    
      ,[CreateTime]
      ,[IndexID]
      ,[Width]
      ,[Height]
      ,[ThumbnailImagePath]
      ,[ImageID]
      ,[BackgroundColor]
      )
	Values
		(@PID,@Name,@Source,@URL,@ImagePath,@Description,@CreateTime,@IndexID,@Width,@Height,@ThumbnailImagePath,@ImageID,@BackgroundColor)

	Select @ReferenceID = @@IDENTITY
  update Advertise_Config set IsOrder=@ReferenceID where ID=@ReferenceID

End
GO
/****** Object:  StoredProcedure [dbo].[sp_Advertise_Config_DeleteRow]    Script Date: 03/02/2014 20:58:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Advertise_Config_DeleteRow]
	@ID int
As
Begin
	Update Advertise_Config Set IsDelete=255 Where ID=@ID;
End
GO
/****** Object:  UserDefinedFunction [dbo].[getProductPromote]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		fy 
-- Create date:	2014-02-11
-- Description:	根据商品ID、会员ID 获取商品所在促销活动，并返回促销结果
-- =============================================
CREATE FUNCTION [dbo].[getProductPromote]
(
	@ProductID int,--商品ID
	@UserID int, --用户ID
	@Quantity int -- 商品数量（多瓶装会使用到。现已不用）
)
RETURNS  @tmp TABLE
(
	[ProductID] int, -- 商品编号
	[Quantity] int, -- 商品数量
	[ProductPic] varchar(1000), -- 商品图片
	[ProductName] varchar(100), -- 商品名称	
	[GoujiuPrice] FLOAT,--原价	
	[PromotePrice] float,--促销价
	
	[PromoteIDs] varchar(20),--促销ID
	[PromoteTypes] varchar(20),--促销类型(限时抢购：1，购酒网团购：2，等级价格：3满额促销：11，满件促销：12)
	[PromoteNames] nvarchar(50),--促销名称
	/*[SinglePromoteID] int,--促销ID
	[SinglePromoteType] INT,--促销类型(1:限时抢购)
	[SinglePromoteName] nvarchar(50), --促销名称
	
	[SurplusNum] INT,--促销剩余库存
	*/
	[IsMobileValidate] Bit, -- 是否手机验证
	[IsUseCoupon] Bit, -- 能否使用优惠券
	[IsNewUser] Bit, -- 是否新会员
	[IsOnlinePayment] Bit, --是否在线支付
	[PromoteResidueQuantity] int, --活动商品剩余数量
	[LimitedBuyQuantity] int, -- 会员限购数量
	[MaxBuyQuantity] int -- 允许会员购买的最大数量
)    
AS   
BEGIN
	DECLARE @ProductName varchar(100)	
	DECLARE @GoujiuPrice FLOAT
	DECLARE @PromotePrice float
	Declare @ProductPic varchar(1000)
	DECLARE @PromoteIDs nvarchar(20)
	DECLARE @PromoteTypes nvarchar(20)	
	DECLARE @PromoteNames nvarchar(50)
	Declare @PromoteResidueQuantity int
	Declare @LimitedBuyQuantity int
	Declare @MaxBuyQuantity int
	/*DECLARE @SinglePromoteID INT
	DECLARE @SinglePromoteType INT
	DECLARE @SinglePromoteName nvarchar(50)
		
	DECLARE @PromoteEndTime DATETIME
	DECLARE @SurplusNum INT
	*/
	DECLARE @IsMobileValidate Bit
	DECLARE @IsUseCoupon Bit 
	DECLARE @IsNewUser Bit
	DECLARE @IsOnlinePayment Bit
	
	SET @IsMobileValidate = 0
	SET @IsUseCoupon = 0 
	SET @IsNewUser = 0
	SET @IsOnlinePayment= 0
	
	----------------------------------------------
	/*
	ProductID,
	ProductName,
	GoujiuPrice,
	Quantity,
	PromotePrice,
	SinglePromoteID,
	SinglePromoteType,
	SinglePromoteName,
	PromoteID,
	PromoteType,
	PromoteName	
	*/
	
	SET @ProductPic = (Select Picture.[Path] FROM Product Left join Product_Picture on Product.ID = Product_Picture.ProductID
						Left Join Picture on Picture.ID = Product_Picture.PictureID
						Where Product_Picture.IsMaster=1 and Product.ID = @ProductID)

	--抢购	开始
	SELECT TOP 1
	@ProductName = [Product].Name ,
	@GoujiuPrice = [Product].GoujiuPrice,
	/*@PromotePrice = Promote_Limited_Discount.DiscountPrice,
	@SinglePromoteType = 1,
	@SinglePromoteName = Promote_Limited_Discount.Name,
	*/
	@PromoteIDs = Convert(varchar(20),Promote_Limited_Discount.ID)+',',
	@PromotePrice=Promote_Limited_Discount.DiscountPrice,
	@PromoteTypes = '1,',
	@PromoteNames = Promote_Limited_Discount.Name+',',
	@IsMobileValidate = Promote_Limited_Discount.IsMobileValidate,
	@IsUseCoupon = Promote_Limited_Discount.IsUseCoupon,
	@IsNewUser = Promote_Limited_Discount.IsNewUser,
	@IsOnlinePayment = Promote_Limited_Discount.IsOnlinePayment,
	@PromoteResidueQuantity =Promote_Limited_Discount.TotalQuantity-isnull((select sum(Quantity) from Order_Product where ProductId = @ProductID and CreateTime between Promote_Limited_Discount.StartTime and Promote_Limited_Discount.EndTime),0),
	@LimitedBuyQuantity = Promote_Limited_Discount.LimitedBuyQuantity,
	@MaxBuyQuantity = Promote_Limited_Discount.LimitedBuyQuantity-isnull((select sum(Quantity) from order_Product 
																		left join [Order] on [Order].Id = order_Product.OrderID 
																		where  ProductId = @ProductID and [Order].CreateTime 
																		between Promote_Limited_Discount.StartTime 
																		and Promote_Limited_Discount.EndTime and UserID =@UserID),0)
	From Promote_Limited_Discount
	Left join [Product] on Promote_Limited_Discount.ProductID = Product.ID
	
	WHERE Promote_Limited_Discount.Status = 1 AND Promote_Limited_Discount.IsDelete = 0
		AND Promote_Limited_Discount.ProductID = @ProductID
		AND (getdate() BETWEEN Promote_Limited_Discount.StartTime AND Promote_Limited_Discount.EndTime)
	ORDER BY Promote_Limited_Discount.ID DESC
			
	--限时抢购	结束
	----------------------------------------------
	/*
	【团购】
	*/
	
	--满额立减	 开始
	
	SELECT TOP 1
		@ProductName = (SELECT Name from [Product] where ID = @ProductID) ,
		@GoujiuPrice = (SELECT GoujiuPrice from [Product] where ID = @ProductID),
		@PromotePrice = Isnull(@PromotePrice,@GoujiuPrice),
		/*@SinglePromoteID = Isnull(@SinglePromoteID,0),
		@SinglePromoteType = Isnull(@SinglePromoteType,0),
		@SinglePromoteName =Isnull(@SinglePromoteName,''),
		*/
		@PromoteIDs = isnull(@PromoteIDs,'')+ Convert(varchar(20),Promote_MeetMoney.ID)+',',
		@PromoteTypes = isnull(@PromoteTypes,'') + '11,',
		@PromoteNames =  isnull(@PromoteNames,'')+Promote_MeetMoney.Name+',',
		@IsMobileValidate = Promote_MeetMoney.IsMobileValidate|@IsMobileValidate,
		@IsUseCoupon = Promote_MeetMoney.IsUseCoupon|@IsUseCoupon,
		@IsNewUser = Promote_MeetMoney.IsNewUser|@IsNewUser,
		@IsOnlinePayment = @IsOnlinePayment,
		@PromoteResidueQuantity = isnull(@PromoteResidueQuantity,0),
		@LimitedBuyQuantity = isnull(@LimitedBuyQuantity,0),
		@MaxBuyQuantity=isnull(@MaxBuyQuantity,0)
	FROM Promote_MeetMoney  
		LEFT JOIN Promote_MeetMoney_Scope ON Promote_MeetMoney.ID=Promote_MeetMoney_Scope.MeetMoneyID
	WHERE Promote_MeetMoney.[Status] = 1 And Promote_MeetMoney.IsDelete = 0
		AND Promote_MeetMoney_Scope.IsDelete =0
		AND (getdate() BETWEEN Promote_MeetMoney.StartTime AND Promote_MeetMoney.EndTime)
		AND Promote_MeetMoney_Scope.ID in (Select ID FROM Promote_MeetMoney_Scope where SUBSTRING(Promote_MeetMoney_Scope.Scope,@ProductID+1,1)='1')
	ORDER BY Promote_MeetMoney.ID DESC
	
	--满额立减	 结束
	----------------------------------------------
	--满件优惠	 开始

	SELECT TOP 1 
		@ProductName = (SELECT Name from [Product] where ID = @ProductID) ,
		@GoujiuPrice = (SELECT GoujiuPrice from [Product] where ID = @ProductID),
		@PromotePrice = Isnull(@PromotePrice,@GoujiuPrice),
		/*@SinglePromoteID = Isnull(@SinglePromoteID,0),
		@SinglePromoteType = Isnull(@SinglePromoteType,0),
		@SinglePromoteName =Isnull(@SinglePromoteName,''),
		*/
		@PromoteIDs = isnull(@PromoteIDs,'') + Convert(varchar(20),Promote_MeetAmount.ID)+',',
		@PromoteTypes = isnull(@PromoteTypes,'')+ '12,',
		@PromoteNames = isnull(@PromoteNames,'') + Promote_MeetAmount.Name+',',
		@IsMobileValidate = Promote_MeetAmount.IsMobileValidate|@IsMobileValidate,
		@IsUseCoupon = Promote_MeetAmount.IsUseCoupon|@IsUseCoupon,
		@IsNewUser = Promote_MeetAmount.IsNewUser|@IsNewUser,
		@IsOnlinePayment = @IsOnlinePayment	,
		@PromoteResidueQuantity = isnull(@PromoteResidueQuantity,0),
		@LimitedBuyQuantity = isnull(@LimitedBuyQuantity,0),
		@MaxBuyQuantity=isnull(@MaxBuyQuantity,0)
	FROM Promote_MeetAmount
		LEFT JOIN Promote_MeetAmount_Scope ON Promote_MeetAmount.ID=Promote_MeetAmount_Scope.MeetAmountID
	WHERE Promote_MeetAmount.[Status] = 1 And Promote_MeetAmount.IsDelete = 0
		AND Promote_MeetAmount_Scope.IsDelete =0
		AND (getdate() BETWEEN Promote_MeetAmount.StartTime AND Promote_MeetAmount.EndTime)
		AND Promote_MeetAmount_Scope.ID in (Select ID FROM Promote_MeetAmount_Scope where SUBSTRING(Promote_MeetAmount_Scope.Scope,@ProductID+1,1)= '1')
	ORDER BY Promote_MeetAmount.ID DESC

	--满件优惠	 结束
	----------------------------------------------
	--未产生任何促销
	IF @PromoteIDs IS NULL
	BEGIN
		SELECT 
		@ProductName = [Product].Name ,
		@GoujiuPrice = [Product].GoujiuPrice,
		@PromotePrice = [Product].GoujiuPrice,
		@PromoteIDs='',
		@PromoteTypes='',
		@PromoteNames='',
		@IsMobileValidate =0,
		@IsUseCoupon = 0,
		@IsNewUser = 0,
		@IsOnlinePayment = 0,
		@PromoteResidueQuantity =0,
		@LimitedBuyQuantity = 0,
		@MaxBuyQuantity=0
		From [Product] 
		WHERE [Product].IsDelete = 0 AND [Product].ID = @ProductID
	END
	
	INSERT INTO 
		@tmp(
			ProductID,
			ProductName,
			ProductPic,
			Quantity,
			GoujiuPrice,
			PromotePrice,
			PromoteIDs,
			PromoteTypes,
			PromoteNames,
			PromoteResidueQuantity,
			LimitedBuyQuantity,
			IsOnlinePayment,
			IsUseCoupon,
			MaxBuyQuantity)
	VALUES (
			@ProductID,
			@ProductName,
			@ProductPic,
			@Quantity,
			round(@GoujiuPrice,2),
			round(@PromotePrice,2),
			@PromoteIDs,
			@PromoteTypes,
			@PromoteNames,
			@PromoteResidueQuantity,
			@LimitedBuyQuantity,
			@IsOnlinePayment,
			@IsUseCoupon,
			@MaxBuyQuantity)
	RETURN     
END
GO
/****** Object:  UserDefinedFunction [dbo].[getProductSaledNum]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create FUNCTION [dbo].[getProductSaledNum]
/*获取产品指定时间内的销量*/
(
@ProductID int,
@StartTime datetime,
@EndTime datetime
)  
RETURNS int AS
BEGIN
	DECLARE @reval int
	SELECT @reval=isnull((
			SELECT isnull(SUM(Order_Product.Quantity),0) AS SumQuantity FROM Order_Product
			LEFT JOIN [Order] ON [Order].ID=[Order_Product].[OrderID]
			WHERE [Order].[Status] NOT IN (4,8)
			AND [Order_Product].[ProductID] = @ProductID
			AND ([Order].[CreateTime] BETWEEN @StartTime AND @EndTime)),0)

	IF @reval<0 SET @reval=0
	RETURN @reval 
END
GO
/****** Object:  UserDefinedFunction [dbo].[getProductPromoteWithCart]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		fy 
-- Create date:	2014-02-11
-- Description:	根据ID、自定义ID、自定义备注 获取商品所在促销活动，并返回促销ID，类型，促销价和促销名称
-- =============================================
CREATE FUNCTION [dbo].[getProductPromoteWithCart]
(
	@ProductID int,--商品ID
	@UserID int,--用户ID
	@Quantity int--数量
)
RETURNS  @tmp TABLE
(
	[PromotionID] int,--促销ID
	[OriginPrice] FLOAT,--原价
	[PromotionType] varchar(20),--促销类型
	[PromotionPrice] float,--促销价
	[PromotionName] nvarchar(50),--促销名称
	[PromotionEndTime] DATETIME, --nvarchar(30),--结束时间
	[SurplusNum] INT,--促销剩余库存
	[IsMobileValidate] Bit, -- 是否手机验证
	[IsUseCoupon] Bit, -- 能否使用优惠券
	[IsNewUser] Bit, -- 是否新会员
	[IsOnlinePayment] Bit --是否在线支付
)    
AS   
BEGIN
	DECLARE @PromotionID INT
	DECLARE @OriginPrice FLOAT
	DECLARE @PromotionType varchar(20)
	DECLARE @PromotionPrice float
	DECLARE @PromotionName nvarchar(50)
	DECLARE @PromotionEndTime DATETIME
	DECLARE @SurplusNum INT
	DECLARE @IsMobileValidate Bit
	DECLARE @IsUseCoupon Bit 
	DECLARE @IsNewUser Bit
	DECLARE @IsOnlinePayment Bit
	
	SET @IsMobileValidate = 0
	SET @IsUseCoupon = 0 
	SET @IsNewUser = 0
	SET @IsOnlinePayment= 0
	----------------------------------------------
	--抢购	开始
	SELECT TOP 1 
	@PromotionID = Promote_Limited_Discount.ID,
	@OriginPrice = (SELECT GoujiuPrice from [Product] where ID = @ProductID),
	@PromotionName = Promote_Limited_Discount.Name,
	@PromotionPrice= Promote_Limited_Discount.DiscountPrice,
	@PromotionType= 'Limited_Discount',
	@PromotionEndTime = Promote_Limited_Discount.EndTime,
	@SurplusNum=(
			Promote_Limited_Discount.TotalQuantity-dbo.getProductSaledNum(@ProductID,Promote_Limited_Discount.StartTime, Promote_Limited_Discount.EndTime)
		),
	@IsMobileValidate = Promote_Limited_Discount.IsMobileValidate,
	@IsUseCoupon = Promote_Limited_Discount.IsUseCoupon,
	@IsNewUser = Promote_Limited_Discount.IsNewUser,
	@IsOnlinePayment = Promote_Limited_Discount.IsOnlinePayment 	
	FROM Promote_Limited_Discount
	WHERE Promote_Limited_Discount.Status = 1		
		AND Promote_Limited_Discount.ProductID = @ProductID
		AND (getdate() BETWEEN Promote_Limited_Discount.StartTime AND Promote_Limited_Discount.EndTime)
	ORDER BY Promote_Limited_Discount.ID DESC
		
	--限时抢购	结束
	----------------------------------------------
	--满额立减	 开始
	
	SELECT TOP 1 @PromotionID=Promote_MeetMoney.ID,
		@OriginPrice=(SELECT GoujiuPrice FROM [Product] WHERE ID=@ProductID),
		@PromotionName=Promote_MeetMoney.Name,
		@PromotionPrice=Isnull(@PromotionPrice,@OriginPrice),
		@PromotionType='MeetMoneyCut',
		@PromotionEndTime=Promote_MeetMoney.EndTime,
		@SurplusNum = 1,
		@IsMobileValidate = (@IsMobileValidate|Promote_MeetMoney.IsMobileValidate),
		@IsUseCoupon = (@IsMobileValidate|Promote_MeetMoney.IsUseCoupon),
		@IsNewUser = (@IsMobileValidate|Promote_MeetMoney.IsNewUser),
		@IsOnlinePayment = @IsOnlinePayment 
	FROM Promote_MeetMoney  
		LEFT JOIN Promote_MeetMoney_Scope ON Promote_MeetMoney.ID=Promote_MeetMoney_Scope.MeetMoneyID
	WHERE Promote_MeetMoney.[Status] = 1 And Promote_MeetMoney.IsDelete = 0
		AND Promote_MeetMoney_Scope.IsDelete =0
		AND (getdate() BETWEEN Promote_MeetMoney.StartTime AND Promote_MeetMoney.EndTime)
		AND SUBSTRING(Promote_MeetMoney_Scope.Scope,@ProductID,1)= '1'
	ORDER BY Promote_MeetMoney.ID DESC
	
	--满额立减	 结束
	----------------------------------------------
	--满件优惠	 开始
	
	SELECT TOP 1 @PromotionID=Promote_MeetAmount.ID,
		@OriginPrice=(SELECT GoujiuPrice FROM [Product] WHERE ID=@ProductID),
		@PromotionName=Promote_MeetAmount.Name,
		@PromotionPrice=Isnull(@PromotionPrice,@OriginPrice),
		@PromotionType='MeetAmountCut',
		@PromotionEndTime=Promote_MeetAmount.EndTime,
		@SurplusNum = 1,
		@IsMobileValidate = (@IsMobileValidate|Promote_MeetAmount.IsMobileValidate),
		@IsUseCoupon = (@IsMobileValidate|Promote_MeetAmount.IsUseCoupon),
		@IsNewUser = (@IsMobileValidate|Promote_MeetAmount.IsNewUser),
		@IsOnlinePayment = @IsOnlinePayment 
	FROM Promote_MeetAmount
		LEFT JOIN Promote_MeetAmount_Scope ON Promote_MeetAmount.ID=Promote_MeetAmount_Scope.MeetAmountID
	WHERE Promote_MeetAmount.[Status] = 1 And Promote_MeetAmount.IsDelete = 0
		AND Promote_MeetAmount_Scope.IsDelete =0
		AND (getdate() BETWEEN Promote_MeetAmount.StartTime AND Promote_MeetAmount.EndTime)
		AND SUBSTRING(Promote_MeetAmount_Scope.Scope,@ProductID,1)= '1'
	ORDER BY Promote_MeetAmount.ID DESC
	
	--满件优惠	 结束
	----------------------------------------------
	--未产生任何促销
	IF @PromotionID IS NULL
	BEGIN
	SELECT @PromotionID=0,
		@OriginPrice=GoujiuPrice,
		@PromotionName='',
		@PromotionPrice=GoujiuPrice,
		@PromotionType='',
		@PromotionEndTime=NULL,
		@SurplusNum=InventoryNumber,
		@IsOnlinePayment=0
	FROM [Product] WHERE ID=@ProductID
	END
	
	INSERT INTO 
		@tmp(PromotionID,OriginPrice,PromotionType,PromotionPrice,PromotionName,PromotionEndTime,SurplusNum,IsOnlinePayment)
	VALUES (@PromotionID,round(@OriginPrice,2),@PromotionType,round(@PromotionPrice,2),@PromotionName,@PromotionEndTime,@SurplusNum,@IsOnlinePayment)
	RETURN     
END
GO
/****** Object:  UserDefinedFunction [dbo].[getProductPrice]    Script Date: 03/02/2014 20:58:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		fy 
-- Create date:	2014-02-11
-- Description:	根据ID获取商品所在促销活动，并返回促销ID，类型，促销价和促销名称（现没有使用）
-- =============================================
CREATE FUNCTION [dbo].[getProductPrice]
(
	@ProductID int
)
RETURNS  @tmp TABLE
(
	[PromotionID] int,--促销ID
	[OriginPrice] FLOAT,--原价
	[PromotionType] varchar(20),--促销类型
	[PromotionPrice] float,--促销价
	[PromotionName] nvarchar(50),--促销名称
	[PromotionLBound] INT,--最低购买数,
	[PromotionEndTime] DATETIME,--结束时间
	[IsMobileValidate] Bit, -- 是否手机验证
	[IsUseCoupon] Bit, -- 能否使用优惠券
	[IsNewUser] Bit, -- 是否新会员
	[IsOnlinePayment] Bit --是否在线支付
)    
AS   
BEGIN
	DECLARE @PromotionID INT
	DECLARE @OriginPrice FLOAT
	DECLARE @PromotionType varchar(20)
	DECLARE @PromotionPrice float
	DECLARE @PromotionName nvarchar(50)
	DECLARE @PromotionLBound INT
	DECLARE @PromotionEndTime DATETIME
	DECLARE @SurplusNum INT
	DECLARE @IsMobileValidate Bit
	DECLARE @IsUseCoupon Bit 
	DECLARE @IsNewUser Bit
	DECLARE @IsOnlinePayment Bit

	----------------------------------------------
	--抢购	开始
	SELECT TOP 1 
	@PromotionID = Promote_Limited_Discount.ID,
	@OriginPrice = (SELECT GoujiuPrice from Product where ID = @ProductID),
	@PromotionName = Promote_Limited_Discount.Name,
	@PromotionPrice= Promote_Limited_Discount.DiscountPrice,
	@PromotionType= 'Limited_Discount',
	@PromotionLBound = 0,
	@PromotionEndTime = Promote_Limited_Discount.EndTime,
	@SurplusNum=(
			Promote_Limited_Discount.TotalQuantity-dbo.getProductSaledNum(@ProductID,Promote_Limited_Discount.StartTime, Promote_Limited_Discount.EndTime)
		),
	@IsMobileValidate = Promote_Limited_Discount.IsMobileValidate,
	@IsUseCoupon = Promote_Limited_Discount.IsUseCoupon,
	@IsNewUser = Promote_Limited_Discount.IsNewUser,
	@IsOnlinePayment = Promote_Limited_Discount.IsOnlinePayment 	
	FROM Promote_Limited_Discount
	WHERE Promote_Limited_Discount.Status = 1		
		AND Promote_Limited_Discount.ProductID = @ProductID
		AND (getdate() BETWEEN Promote_Limited_Discount.StartTime AND Promote_Limited_Discount.EndTime)
	ORDER BY Promote_Limited_Discount.ID DESC
	--限时抢购	结束
	----------------------------------------------
	
	/* --团购	TeamBuy	开始
	
	--团购	TeamBuy	结束
	*/
	----------------------------------------------
	/* --多瓶装	TeamBuy	开始
	
	--多瓶装	TeamBuy	结束
	*/
	----------------------------------------------
	/* --等级优惠	TeamBuy	开始
	
	--等级优惠	TeamBuy	结束
	*/
	----------------------------------------------
	--未产生任何促销
	IF @PromotionID IS NULL
	BEGIN
	SELECT @PromotionID=0,
		@OriginPrice=Price,
		@PromotionName='',
		@PromotionPrice=Price,
		@PromotionType='',
		@PromotionLBound=0,
		@PromotionEndTime=NULL
	FROM pro_Product WHERE ID=@ProductID
	END
	
	INSERT INTO @tmp(PromotionID,OriginPrice,PromotionType,PromotionPrice,PromotionName,PromotionLBound,PromotionEndTime) VALUES (@PromotionID,round(@OriginPrice,2),@PromotionType,round(@PromotionPrice,2),@PromotionName,@PromotionLBound,@PromotionEndTime)
	RETURN     
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_DeleteRow]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Product_DeleteRow
-- Author:	张连印
-- Description:	删除一条订单商品记录
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Product_DeleteRow]
	@ID int
As
Begin	
	Declare @saleAmount int, @productID int
	
	---删除前还原库存
	Select @productID = productID, @saleAmount = -isNull(Quantity,0) 
	from Order_Product ODP Left Join Product P on ODP.productID = p.ID And P.IsDelete = 0
	where ODP.ID = @ID And ODP.IsDelete = 0;
	
	Exec sp_Product_Update_SaleAmount @ID=@productID,@SaleAmount = @saleAmount;
	
	Update Order_Product
	Set isDelete = 255
	Where  [ID] = @ID;
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectByPromoteProduct]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: fy
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Product_SelectByPromoteProduct]
	@ProductIDs text
AS
BEGIN
	select * from dbo.view_Product_Paging where ID  in (select productID from Promote_Limited_Discount where getdate()between StartTime and EndTime and Promote_Limited_Discount.[Status] = 1 and ProductID in (Select * from split(@ProductIDs,',')))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectByProductStr]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: <Create Date,,>
-- Description:	查询指定的商品. 促销管理已选商品
-- =============================================
CREATE PROCEDURE [dbo].[sp_Product_SelectByProductStr]
	@ProductIDs text
AS
BEGIN
	Select * from view_Product_Paging where ID in (select * from split(@ProductIDs,','))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectByProductID]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Product_SelectByProductID]
@ProductID int
As
Begin
	Select *
	From view_Product_Paging
	Where 
		ID = @ProductID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_User_Level_Price_SelectRow]    Script Date: 03/02/2014 20:58:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		冯瑶
-- Create date: 2013-12-28 09:32:23
-- Description:	查询指定编号的会员等级价格
-- =============================================
CREATE Procedure [dbo].[sp_User_Level_Price_SelectRow]
	@ID int
As
Begin
	Select 
		*
	From view_User_Level_Price_SelectAll
	Where
		[ID] = @ID
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_RecoverOrderInventory]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Product_RecoverOrderInventory
-- Author:	张连印
-- Description:	根据订单恢复商品库存
-- ==========================================================================================
Create Procedure [dbo].[sp_Product_RecoverOrderInventory]
	@OrderID int
As
Begin
	Declare @productID int, @saleAmount int

	--使用游标查找订单商品的ID
	Declare Cursor_Order_Product Cursor For  
	Select ProductID, isnull(Quantity,0) Quantity  From Order_Product Where OrderId = @OrderID;
	
	Open Cursor_Order_Product;
	Fetch Next From Cursor_Order_Product Into @productID, @saleAmount	
	
	While @@FETCH_STATUS =0  --循环游标取值
	Begin
		print @productID
		print @saleAmount
		select @saleAmount = -isnull(@saleAmount,0); --由于恢复，则使用负数
		Exec sp_Product_Update_SaleAmount @ID=@productID, @SaleAmount = @saleAmount; --更新商品库存信息
		Fetch Next From Cursor_Order_Product Into @productID, @saleAmount
	End
	
	Close Cursor_Order_Product
	Deallocate Cursor_Order_Product
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_Promote_Select]    Script Date: 03/02/2014 20:58:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	查询商品促销结果
-- =============================================
CREATE PROCEDURE [dbo].[sp_Product_Promote_Select]
	@ProductID int,
	@Quantity int,
	@UserID int
AS
BEGIN
	select * from dbo.getProductPromote(@ProductID,@UserID,@Quantity)
END
GO
/****** Object:  View [dbo].[view_UserProductComment]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[view_UserProductComment]
As
Select 
	PC.[ID] CommentID
      ,PC.[ProductID]
      ,PC.[OrderID]
      ,PC.[UserID]
      ,PC.[Score]
      ,PC.[Content]
      ,PC.[Status]
      ,PC.[EmployeeID]
      ,PC.[AuditDescription]
      ,OP.Barcode
      ,OP.ProductName
      ,OP.Path [Path]
      ,OP.Createtime [TransactTime]
 from Product_Comment PC Join view_Order_Products OP On OP.ProductID =PC.ProductID and PC.UserID = OP.UserID
where PC.IsDelete=0
GO
/****** Object:  View [dbo].[view_Product_Consults_All]    Script Date: 03/02/2014 20:58:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_Product_Consults_All]
AS
SELECT C.[ID] ID    
      ,[ProductID]
      ,R.ID ReplyID
      ,P.Name ProductName
      ,p.ThumbnailPath ProductPicPath
      ,[UserID]    
      ,U.NickName UserName    
      ,[ConsultPerson]    
      ,[ConsultPersonMobile]    
      ,[ConsultPersonEmail]    
      ,C.[Content]  ConsultContent 
      ,R.[Content] [Content]      
      ,C.[CreateTime] ConsultTime    
  FROM [Product_Consult] C 
  Left Join Product_Consult_Reply R ON C.ID = R.ConsultID
  Left JOIN [User] U ON C.UserID= U.ID  And U.IsDelete = 0  
  Join [view_Product_Paging] P ON C.ProductID = P.ID And R.IsDelete = 0
  And C.IsDelete = 0
GO
/****** Object:  StoredProcedure [dbo].[sp_Product_SelectByOnsaleProductID]    Script Date: 03/02/2014 20:58:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_Product_SelectByOnsaleProductID]
@ProductID int
As
Begin
	Select *
	From view_Product_Paging
	Where 
		ID = @ProductID
		and Status= 2 and ProductCategoryID<>9 -- 9 为其他
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_BatchInsert]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Product_BatchInsert
-- Author:	张连印
-- Description:	批量订单商品记录
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Product_BatchInsert]
	@OP OrderProductTable readonly,
	@RowCount int output
As
Begin
	/*
	[OrderID] [int] NOT NULL,
								[ProductID] [int] NOT NULL,
								[Quantity] [int] NOT NULL,
								[TransactPrice] [float] NOT NULL,
								[PromotionID] [int] NULL,
								[PromotionType] [int] NULL,
								[PromotionResult] [int] NULL,
								[MarketPrice] [float] NULL,
								[ProductName] [nvarchar](1) NULL,
								[Integral] [int] NULL,
								[RebateRate] [float] NULL,
								[Commission] [float] NULL,
								[Remark] [nvarchar](1) NULL,
								[ExtField] [nvarchar](1) NULL,
								[CreateTime] [datetime] NOT NULL
	*/

	Declare @productID int, @saleAmount int
	
	Insert Into Order_Product([OrderID],[ProductID],[Quantity],[GoujiuPrice],[TransactPrice],[PromotionID],[PromotionType],[PromotionResult],[MarketPrice],
	[ProductName],[Integral],[RebateRate],[Commission],[Remark],[ExtField],[CreateTime])
	select OrderID,ProductID,Quantity,[GoujiuPrice],TransactPrice,[PromotionID],[PromotionType],[PromotionResult],[MarketPrice],
	[ProductName],[Integral],[RebateRate],[Commission],[Remark],[ExtField],Createtime from @op;
		
	--使用游标查找订单商品的ID
	Declare Cursor_Order_Product Cursor For  
	Select ProductID, Quantity From @OP;
	
	Open Cursor_Order_Product;
	Fetch Next From Cursor_Order_Product Into @productID, @saleAmount
	
	While @@FETCH_STATUS =0  --循环游标取值
	Begin		
		Exec sp_Product_Update_SaleAmount @ID=@productID, @SaleAmount = @saleAmount  --更新在售商品库存信息		
		Fetch Next From Cursor_Order_Product Into @productID, @saleAmount
	End
	
	Close Cursor_Order_Product
	Deallocate Cursor_Order_Product 	

	Select @RowCount = COUNT(*) from @OP;
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Cancel]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Cancel
-- Author:	张连印
-- Description:	后台直接取消订单
-- ==========================================================================================

Create Procedure [dbo].[sp_Order_Cancel]
	@OrderID int,	
    @OrderCancelCauseID int
   ,@EmployeeID int = default
   ,@UserID int =default
   ,@Description ntext=default
   ,@CreateTime datetime
	,@Result int output	
As
Begin
	Declare @orderStatus int, @paymentStatus int, @RefernceID int
	Begin Transaction
	
	Begin Try
		--检查订单状态，若为未发货状态，则可以取消	
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		Select @orderStatus = [Status], @paymentStatus = PaymentStatus from [Order] ROWLOCK where ID = @OrderID; --锁住本行
		
		if (@orderStatus=2) or (@orderStatus=3) --若已发货或者签收，则返回2
		Begin
			set @Result = 2
		end
		Else if (@orderStatus=4) or (@orderStatus=5) or (@orderStatus=6) or (@orderStatus=8) --若已取消或者作废，则返回3
		Begin
			set @Result = 3
		End	
		Else if (@paymentStatus=1) and ((@orderStatus=0) or (@orderStatus=1) or (@orderStatus=100)) --若订单已支付未发货,则返回4
		Begin
			set @Result = 4
		end
		Else if (@orderStatus=0) or (@orderStatus=1) or (@orderStatus=100) --若没有发货，可以取消，继续操作，返回1
		Begin
			Exec sp_Product_RecoverOrderInventory @OrderID = @OrderID; --恢复订单商品的库存
			insert into Order_Cancel(OrderID,OrderCancelCauseID,EmployeeID,[Description],CreateTime) Values(@OrderID,@OrderCancelCauseID,@EmployeeID,@Description,@CreateTime);
			Update [Order] Set [Status]=6 , [Description] = @Description  where ID= @OrderID;
			exec sp_Order_Status_Tracking_Insert @EmployeeID,@UserID,@OrderID,'取消订单',6,@Createtime, @RefernceID output;			
			set @Result = 1
		End
		Else
		Begin  --未知状态，则返回0
			set @Result= 0 
		end
		
		Commit Transaction
		
	End Try
	Begin Catch
	IF @@TRANCOUNT > 0---------------判断有没有事务
	BEGIN
		ROLLBACK Transaction----------回滚事务
	END 
	RAISERROR ('取消订单操作发生错误，存储过程：sp_Order_Cancel' , 16, 1) WITH NOWAIT 
	End catch	
	
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Cancel_Refund]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Cancel_Refund
-- Author:	张连印
-- Description:	后台直接取消已支付订单
-- ==========================================================================================

CREATE PROCEDURE [dbo].[sp_Order_Cancel_Refund]
	@OrderID int,	
    @OrderCancelCauseID int,
    @EmployeeID int = default,
    @UserID int =default,
    @CancelDescription ntext=default,
	@RefundMethodID int, --退款方式编号（1：退至虚拟账户，2：人工退款至指定帐号）
	@ActualRefundMoney float, --实际退款金额
	@RefundDescription nvarchar(512)=default,
	@CreateTime datetime,
	@Result int output	
As
Begin
	Declare @orderStatus int, @paymentStatus int, @RefernceID int, @OrderUserID int, @accountCount int =0, @userAccountID int=0, @errmsg nvarchar(1024)
	Begin Transaction 
	
	Begin Try
	
		--检查订单状态，若为未发货状态，则可以取消	
		SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
		Select @orderStatus = [Status], @paymentStatus = PaymentStatus, @OrderUserID = UserID from [Order] ROWLOCK where ID = @OrderID and IsDelete = 0; --锁住本行
		
		if (@orderStatus=2) or (@orderStatus=3) --若已发货或者签收，则返回2
		Begin
			set @Result = 2
		end
		Else if (@orderStatus=4) or (@orderStatus=5) or (@orderStatus=6) or (@orderStatus=8) --若已取消或者作废，则返回3
		Begin
			set @Result = 3
		End
		Else if (@paymentStatus=0) and ((@orderStatus=0) or (@orderStatus=1) or (@orderStatus=100)) --若订单未支付未发货,则返回4
		Begin
			set @Result = 4
		end
		Else if (@orderStatus=0) or (@orderStatus=1) or (@orderStatus=100) --若已支付未发货，可以取消，返回1
		Begin
			Exec sp_Product_RecoverOrderInventory @OrderID = @OrderID; --恢复订单商品的库存
			insert into Order_Cancel(OrderID,OrderCancelCauseID,EmployeeID,[Description],CreateTime) Values(@OrderID,@OrderCancelCauseID,@EmployeeID,@CancelDescription,@CreateTime);
			Update [Order] Set [Status]=6 , [Description] = @CancelDescription  where ID= @OrderID;
			Insert Into Aftersale_Refund([RefundSourceID],[OrderID],[RefundMethodID],[ActualRefundMoney],[EmployeeID],[Status],[Description],[CreateTime])
			Values		(1,@OrderID,@RefundMethodID,@ActualRefundMoney,@EmployeeID,6,@RefundDescription,@CreateTime);
			exec sp_Order_Status_Tracking_Insert @EmployeeID,@UserID,@OrderID,'取消订单',3,@Createtime, @RefernceID output;
						 
			select @accountCount = COUNT(*), @userAccountID = ID From User_Account where UserID = @OrderUserID and isDelete = 0 group by ID;
			
			if @RefundMethodID =1 --若退款至虚拟账户，则变更虚拟账户的余额
			Begin			
				if(@accountCount=1) and @userAccountID>0 
				Begin
					exec sp_User_Account_UpdateBalance @ID = @userAccountID, @OperateType = 3, @Money = @ActualRefundMoney;
				end
				else --若用户虚拟账户多余一个或者不存在，则抛出异常
					RAISERROR ('用户虚拟账户异常' , 16, 1) WITH NOWAIT
			End
			
			set @Result = 1
		End
		Else
		Begin  --未知状态，则返回0
			set @Result= 0 
		end
		
		Commit Transaction
		
	End Try
	begin catch 
		select @errmsg = ERROR_MESSAGE() 
		IF @@TRANCOUNT > 0---------------判断有没有事务
		BEGIN
			ROLLBACK Transaction----------回滚事务
		END 
		RAISERROR (@errmsg, 16, 1) WITH NOWAIT  
	end catch
	
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_DeleteByOrderID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================
-- Name:	sp_Order_Product_DeleteByOrderID
-- Author:	张连印
-- Description:	删除一条订单对应的订单商品记录
-- ==========================================================================================
CREATE PROCEDURE [dbo].[sp_Order_Product_DeleteByOrderID]
	@OrderID int
As
Begin		
	Declare @orderProductID int
	
	--使用游标查找订单商品的ID
	Declare Cursor_Order_Product Cursor For  
	Select ID From Order_Product Where OrderId = @OrderID And IsDelete = 0;
	
	Open Cursor_Order_Product;
	Fetch Next From Cursor_Order_Product Into @orderProductID
	
	While @@FETCH_STATUS =0  --循环游标取值
	Begin		
		Exec sp_Order_Product_DeleteRow @orderProductID  --删除订单商品信息（包括更新在售商品库存信息）
		Fetch Next From Cursor_Order_Product Into @orderProductID
	End
	
	Close Cursor_Order_Product
	Deallocate Cursor_Order_Product
End
GO
/****** Object:  StoredProcedure [dbo].[sp_Order_Product_BatchUpdateByOrderID]    Script Date: 03/02/2014 20:58:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========================================================================================  
-- Name: sp_Order_Product_BatchUpdateByOrderID  
-- Author: 张连印  
-- Description: 根据订单编码批量修改订单商品  
-- ==========================================================================================  
CREATE Procedure [dbo].[sp_Order_Product_BatchUpdateByOrderID]  
 @OPT OrderProductTable readonly,  
 @OrderID int,  
 @RowCount int output  
As  
Begin   
 /***  
  1.根据订单编码删除所有订单商品，商品库存还原。  
  2.批量新增订单商品   
 ****/  
   
 Exec sp_Order_Product_DeleteByOrderID @OrderID = @OrderID  
    
 EXEC [sp_Order_Product_BatchInsert] @op = @OPT, @RowCount = @RowCount
  
 Select @RowCount = COUNT(*) from @OPT;   
End
GO
/****** Object:  Default [DF__Advertise__Enabl__33F4B129]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Advertise_Config] ADD  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF__Advertise__IsDel__34E8D562]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Advertise_Config] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Advertise_Config_BackgroundColor]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Advertise_Config] ADD  CONSTRAINT [DF_Advertise_Config_BackgroundColor]  DEFAULT ('#ffffff') FOR [BackgroundColor]
GO
/****** Object:  Default [DF__Aftersale__IsDel__36D11DD4]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Exchange] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Aftersale__IsDel__37C5420D]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Exchange_Log] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Aftersale__IsDel__38B96646]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Exchange_Product] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Aftersale__IsDel__39AD8A7F]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Refund] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Aftersale__IsDel__3AA1AEB8]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Refund_Log] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Aftersale__IsDel__3B95D2F1]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Return] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Aftersale__IsDel__3C89F72A]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Return_Log] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Aftersale__IsDel__3D7E1B63]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Aftersale_Return_Product] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Channel_G__IsDel__3E723F9C]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Channel_GroupBuy] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__City__IsDelete__5728DECD]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF__City__IsDelete__5728DECD]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Code_IsDelete]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Code] ADD  CONSTRAINT [DF_Code_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_De__IsDel__414EAC47]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Delivery_Corporation] ADD  CONSTRAINT [DF__Config_De__IsDel__414EAC47]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_De__IsDel__4242D080]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Delivery_Cost] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_De__IsDel__4336F4B9]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Delivery_Method] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_In__IsDel__442B18F2]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Invoice_Content] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_In__IsDel__451F3D2B]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Invoice_Type] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_Pa__Creat__1392CE8F]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Page] ADD  CONSTRAINT [DF__Config_Pa__Creat__1392CE8F]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Config_Pa__IsDel__1486F2C8]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Page] ADD  CONSTRAINT [DF__Config_Pa__IsDel__1486F2C8]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_Pa__IsDel__47FBA9D6]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Payment_Organization] ADD  CONSTRAINT [DF__Config_Pa__IsDel__47FBA9D6]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_Pa__IsDel__48EFCE0F]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_Payment_Type] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Config_To__Count__164F3FA9]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_ToPayCounty] ADD  DEFAULT ((0)) FOR [CountyID]
GO
/****** Object:  Default [DF__Config_To__isDel__174363E2]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_ToPayCounty] ADD  DEFAULT ((0)) FOR [isDelete]
GO
/****** Object:  Default [DF__Config_To__Creat__1837881B]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Config_ToPayCounty] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__County__IsDelete__5ECA0095]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[County] ADD  CONSTRAINT [DF__County__IsDelete__5ECA0095]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Coupon_Ca__IsDel__4DB4832C]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Coupon_Cash] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Coupon_Ca__IsDel__4EA8A765]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Coupon_Cash_Binding] ADD  CONSTRAINT [DF__Coupon_Ca__IsDel__4EA8A765]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Coupon_De__IsDel__4F9CCB9E]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Coupon_Decrease] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Coupon_De__IsDel__5090EFD7]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Coupon_Decrease_Binding] ADD  CONSTRAINT [DF__Coupon_De__IsDel__5090EFD7]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Coupon_Sc__IsDel__51851410]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Coupon_Scope] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Cps__IsDelete__6482D9EB]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Cps] ADD  CONSTRAINT [DF__Cps__IsDelete__6482D9EB]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Cps_Commi__IsDel__536D5C82]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Cps_CommissionRatio] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Cps_LinkR__IsDel__546180BB]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Cps_LinkRecord] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Cps_Order__IsDel__675F4696]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Cps_OrderPushRecord] ADD  CONSTRAINT [DF__Cps_Order__IsDel__675F4696]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_hw_Log_State]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[hw_Log] ADD  CONSTRAINT [DF_hw_Log_State]  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF_hw_Log_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[hw_Log] ADD  CONSTRAINT [DF_hw_Log_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Order__IsDelete__68536ACF]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF__Order__IsDelete__68536ACF]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order__v4_Update__284DF453]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF__Order__v4_Update__284DF453]  DEFAULT ((0)) FOR [v4_UpdateInventoryNo]
GO
/****** Object:  Default [DF__Order_Can__IsDel__573DED66]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Cancel] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Can__IsDel__5832119F]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Cancel_Cause] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Order_Delivery_Tracking_1_IsDelete]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Delivery_Tracking] ADD  CONSTRAINT [DF_Order_Delivery_Tracking_1_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Del__IsDel__5A1A5A11]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Delivery_Tracking_Details] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Del__IsDel__5B0E7E4A]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Delivery_Tracking_old] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Order_Delivery_Tracking_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Delivery_Tracking_old] ADD  CONSTRAINT [DF_Order_Delivery_Tracking_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Order_Dis__IsDel__5CF6C6BC]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Discount] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Dis__Creat__5DEAEAF5]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Discount] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Order_Dis__IsDel__5EDF0F2E]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Display] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Dis__IsDel__5FD33367]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Display_Image] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Dis__IsDel__60C757A0]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Display_Reply] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Inv__IsDel__61BB7BD9]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Invoice] ADD  CONSTRAINT [DF__Order_Inv__IsDel__61BB7BD9]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Pay__IsDel__62AFA012]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Payment] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Pay__IsDel__63A3C44B]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Payment_Account] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Pro__IsDel__74B941B4]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Product] ADD  CONSTRAINT [DF__Order_Pro__IsDel__74B941B4]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Pro__Creat__0EAE1DE1]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Product_Promote] ADD  CONSTRAINT [DF__Order_Pro__Creat__0EAE1DE1]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Order_Sta__IsDel__668030F6]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Status_Log] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Order_Sta__IsDel__6774552F]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Order_Status_Tracking] ADD  CONSTRAINT [DF__Order_Sta__IsDel__6774552F]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Picture__IsDelet__68687968]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Picture] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Picture_C__IsDel__695C9DA1]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Picture_Category] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Product_CommentScore]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_CommentScore]  DEFAULT ((0)) FOR [CommentScore]
GO
/****** Object:  Default [DF__Product__IsDelet__797DF6D1]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF__Product__IsDelet__797DF6D1]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Product_Attribute_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Attribute] ADD  CONSTRAINT [DF_Product_Attribute_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Product_A__IsDel__6C390A4C]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Attribute] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_A__IsDel__6D2D2E85]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_AttributeValue] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_A__IsDel__6E2152BE]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_AttributeValueSet] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_B__IsDel__6F1576F7]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Brand] ADD  CONSTRAINT [DF__Product_B__IsDel__6F1576F7]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_C__IsDel__70099B30]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Category] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Product_Comment_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Comment] ADD  CONSTRAINT [DF_Product_Comment_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Product_C__IsDel__71F1E3A2]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Comment] ADD  CONSTRAINT [DF__Product_C__IsDel__71F1E3A2]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Product_Comment_Reply_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Comment_Reply] ADD  CONSTRAINT [DF_Product_Comment_Reply_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Product_C__IsDel__73DA2C14]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Comment_Reply] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Product_Consult_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Consult] ADD  CONSTRAINT [DF_Product_Consult_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Product_C__IsDel__75C27486]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Consult] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_C__IsDel__76B698BF]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Consult_Reply] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_L__IsDel__77AABCF8]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_LimitedBuy_Area] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_L__IsDel__789EE131]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_LimitedBuy_Condition] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_P__IsDel__7993056A]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Picture] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Product_S__IsDel__7A8729A3]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Product_Status_Log] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Promote_Lan__PID__7B7B4DDC]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_LandingPage] ADD  DEFAULT ((0)) FOR [PID]
GO
/****** Object:  Default [DF__Promote_L__Creat__7C6F7215]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_LandingPage] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Promote_L__IsDel__7D63964E]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_LandingPage] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Promote_L__IsDel__06D7F1EF]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_Limited_Discount] ADD  CONSTRAINT [DF__Promote_L__IsDel__06D7F1EF]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Promote_MeetAmount_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetAmount] ADD  CONSTRAINT [DF_Promote_MeetAmount_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Promote_M__IsDel__0C90CB45]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetAmount] ADD  CONSTRAINT [DF__Promote_M__IsDel__0C90CB45]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Promote_MeetAmount_Rule_IsDelete]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetAmount_Rule] ADD  CONSTRAINT [DF_Promote_MeetAmount_Rule_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Promote_MeetAmount_Scope_IsDelete]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetAmount_Scope] ADD  CONSTRAINT [DF_Promote_MeetAmount_Scope_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Promote_MeetMoney_CreateTime]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetMoney] ADD  CONSTRAINT [DF_Promote_MeetMoney_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__Promote_M__IsDel__11558062]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetMoney] ADD  CONSTRAINT [DF__Promote_M__IsDel__11558062]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Promote_M__IsDel__1249A49B]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetMoney_Rule] ADD  CONSTRAINT [DF__Promote_M__IsDel__1249A49B]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_Promote_MeetMoney_Scope_IsDelete]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MeetMoney_Scope] ADD  CONSTRAINT [DF_Promote_MeetMoney_Scope_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Promote_M__IsDel__06ED0088]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MuchBottled] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Promote_M__IsDel__07E124C1]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Promote_MuchBottled_Rule] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__Province__IsDele__15261146]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[Province] ADD  CONSTRAINT [DF__Province__IsDele__15261146]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Da__IsDel__09C96D33]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[System_DataDictionary] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_De__IsDel__0ABD916C]    Script Date: 03/02/2014 20:58:35 ******/
ALTER TABLE [dbo].[System_Department] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Em__IsDel__0BB1B5A5]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Employee] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Lo__Creat__6265874F]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Log] ADD  CONSTRAINT [DF__System_Lo__Creat__6265874F]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_System_Log_IsDelete]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Log] ADD  CONSTRAINT [DF_System_Log_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Me__IsDel__0E8E2250]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Menu] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Pe__IsDel__0F824689]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Permission] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Re__IsDel__10766AC2]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Resources] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Ri__IsDel__116A8EFB]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Rights] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Ro__IsDel__125EB334]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Role] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Ro__IsDel__1352D76D]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Role_Permission] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Us__IsDel__1446FBA6]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_User] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__System_Vi__IsDel__153B1FDF]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[System_Visitor] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_User_Integral]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Integral]  DEFAULT ((0)) FOR [Integral]
GO
/****** Object:  Default [DF_User_CreateTime]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__User__IsDelete__1FA39FB9]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF__User__IsDelete__1FA39FB9]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Acco__IsDel__190BB0C3]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Account] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Acco__IsDel__218BE82B]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Account_Details] ADD  CONSTRAINT [DF__User_Acco__IsDel__218BE82B]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_User_BrowseHistory_CreateTime]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_BrowseHistory] ADD  CONSTRAINT [DF_User_BrowseHistory_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__User_Brow__IsDel__1BE81D6E]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_BrowseHistory] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_User_Collect_Record_CreateTime]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_CollectRecord] ADD  CONSTRAINT [DF_User_Collect_Record_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_User_CollectRecord_IsDelete]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_CollectRecord] ADD  CONSTRAINT [DF_User_CollectRecord_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Inte__IsDel__1EC48A19]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Integral_Details] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_User_Level_Level]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Level] ADD  CONSTRAINT [DF_User_Level_Level]  DEFAULT ((0)) FOR [Level]
GO
/****** Object:  Default [DF__User_Leve__IsDel__246854D6]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Level] ADD  CONSTRAINT [DF__User_Leve__IsDel__246854D6]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Leve__IsDel__21A0F6C4]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Level_Price] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Mess__IsDel__22951AFD]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Message_Email] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Mess__IsDel__23893F36]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Message_SendRecord] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Mess__IsDel__247D636F]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_Message_Sms] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__User_Reci__IsDel__257187A8]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[User_RecieveAddress] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF__v4_odr_Or__State__033C6B35]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[v4_odr_OrderChange] ADD  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF__v4_odr_Or__Creat__04308F6E]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[v4_odr_OrderChange] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__v4_odr_Or__State__08F5448B]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[v4_odr_OrderDiscount] ADD  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF__v4_odr_Or__Creat__09E968C4]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[v4_odr_OrderDiscount] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__v4_Usr_Fi__State__77CAB889]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[v4_Usr_FindMailPassword] ADD  DEFAULT ((0)) FOR [State]
GO
/****** Object:  Default [DF__v4_Usr_Fi__Creat__78BEDCC2]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[v4_Usr_FindMailPassword] ADD  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__v4_Usr_Fi__FailT__79B300FB]    Script Date: 03/02/2014 20:58:36 ******/
ALTER TABLE [dbo].[v4_Usr_FindMailPassword] ADD  DEFAULT (getdate()) FOR [FailTime]
GO
