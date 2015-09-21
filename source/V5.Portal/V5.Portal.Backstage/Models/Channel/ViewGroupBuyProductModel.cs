// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewGroupBuyProductModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the view_GroupBuy_Product type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Channel
{
    using global::System;
    using global::System.Web.UI;

    /// <summary>
    /// 团购商品视图
    /// </summary>
    public class ViewGroupBuyProductModel
    {
        #region Public Properties

        /// <summary>
        /// 团购商品ID
        /// </summary>
        public int ProductId
        {
            get;
            set;
        }

        /// <summary>
        /// 团购状态
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        public string StatusName
        {
            get
            {
                switch (this.Status)
                {
                    case 2:
                        return "<span style='color:red'>进行中</span>";
                    case 3:
                        return "<span>已停止</span>";
                    case 4:
                        return "<span style='color:red'>已暂停</span>";
                }

                return " ";
            }
        }

        /// <summary>
        /// 团购名称
        /// </summary>
        public string GroupBuyName
        {
            get;
            set;
        }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 条形码，商品编码
        /// </summary>
        public string Barcode
        {
            get;
            set;
        }

        /// <summary>
        /// 团购开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 团购结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 市场价格
        /// </summary>
        public float GoujiuPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 团购价格
        /// </summary>
        public float GBPrice
        {
            get;
            set;
        }

        /// <summary>
        /// 会员级别
        /// </summary>
        public string LevelName
        {
            get
            {
                switch (this.UserLevelID)
                {
                    case 0:
                        return "不限制";
                    case 1:
                        return "普通会员";
                    case 2:
                        return "铜牌会员";
                    case 3:
                        return "银牌会员";
                    case 4:
                        return "金牌会员";
                    case 5:
                        return "VIP";
                }

                return " ";
            }
        }

        /// <summary>
        /// 会员级别ID
        /// </summary>
        public int UserLevelID
        {
            get;
            set;
        }

        /// <summary>
        /// 显示级别
        /// </summary>
        public int ShowLevel
        {
            get;
            set;
        }

        public string ShowLevelName
        {
            get
            {
                switch (this.ShowLevel)
                {
                    case 1:
                        return "第一级别";
                    case 2:
                        return "第二级别";
                    case 3:
                        return "第三级别";
                }

                return " ";
            }
        }

        public string OperatorColumns
        {
            get
            {
                switch (this.Status)
                {
                    case 2:
                        return "<a href='javascript:loadLookView(" + ProductId + ")'>查看</a>&nbsp;&nbsp;<a href='javascript:transformSuspend(" + ProductId + ")' id='suspend" + ProductId + "' name='suspend' value='" + ProductId + "'>暂停</a>&nbsp;&nbsp;<a href='javascript:transformSuspendS(" + ProductId + ")' id='setback" + ProductId + "' vname='" + GroupBuyName + "' value='" + ProductId + "'>停止</a>&nbsp;&nbsp;<a href='javascript:deleteGroup(" + ProductId + ")'>删除</a>";
                    case 3:
                        return "<a href='javascript:loadLookView(" + ProductId + ")'>查看</a>&nbsp;&nbsp;<a style='color:red'>已停止</a>&nbsp;&nbsp;<a href='javascript:deleteGroup(" + ProductId + ")'>删除</a>";
                    case 4:
                        return "<a href='javascript:loadLookView(" + ProductId + ")'>查看</a>&nbsp;&nbsp;<a href='javascript:transformSuspend(" + ProductId + ")' id='suspend" + ProductId + "' vname='" + GroupBuyName + "' value='" + ProductId + "'>恢复</a>&nbsp;&nbsp;<a href='javascript:deleteGroup(" + ProductId + ")'>删除</a>";
                }

                return " ";
            }
        }

        #endregion
    }
}
