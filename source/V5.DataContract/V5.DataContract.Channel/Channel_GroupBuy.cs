// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel_GroupBuy.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   团购频道类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.Channel 
{
    using System;

    /// <summary>
    /// 团购频道类
    /// </summary>
    public class Channel_GroupBuy 
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置商品编号．
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 获取或设置用户等级编号．
        /// </summary>
        public int UserLevelID { get; set; }

        /// <summary>
        /// 获取或设置团购活动名称．
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置活动图片地址．
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 获取或设置团购价格．
        /// </summary>
        public double GBPrice { get; set; }

        /// <summary>
        /// 获取或设置团购活动商品总数．
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// 获取或设置团购活动介绍．
        /// </summary>
        public string Introduce { get; set; }

        /// <summary>
        /// 获取或设置团购活动显示级别．
        /// </summary>
        public int ShowLevel { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 获取或设置．
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 获取或设置是否显示时间．
        /// </summary>
        public bool IsShowTime { get; set; }

        /// <summary>
        /// 获取或设置是否仅限在线支付．
        /// </summary>
        public bool IsOnlinePayment { get; set; }

        /// <summary>
        /// 获取或设置真实销售数量．
        /// </summary>
        public int SoldOfReality { get; set; }

        /// <summary>
        /// 获取或设置虚拟销售数量．
        /// </summary>
        public int SoldOfVirtual { get; set; }

        /// <summary>
        /// 获取或设置活动页面浏览数．
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 获取或设置团购活动状态（1：未开始，2：进行中，3：已结束）．
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }

        #endregion
    }
}