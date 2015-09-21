// --------------------------------------------------------------------------------------------------------------------
// <copyright file="County.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   区县类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataContract.System
{
    using global::System;

    /// <summary>
    /// 区县类
    /// </summary>
    [Serializable]
    public class County
    {
        #region Public Properties

        /// <summary>
        /// 获取或设置主键编号．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置区县所在城市编号．
        /// </summary>
        public int CityID { get; set; }

	    /// <summary>
	    /// 获取或设置区县名称．
	    /// </summary>
	    public string Name { get; set; }

	    /// <summary>
		/// 获取或设置可在线支付区域
		/// </summary>
		public bool CanLandPay { get; set; }

        /// <summary>
        /// 获取或设置排序编号．
        /// </summary>
        public string PostCode { get; set; }

        #endregion
    }
}