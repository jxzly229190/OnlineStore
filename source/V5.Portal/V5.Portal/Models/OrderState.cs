// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderState.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   订单状态信息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Models
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 订单状态信息
    /// </summary>
    public class OrderState
    {
        /// <summary>
        /// 设置或获取订单状态:0 - 订单提交,1 - 等待付款,2 - 仓库出货,3 - 等待签收,4 - 完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 获取或设置订单支付方式：0 - 在线支付，1 - 货到付款
        /// </summary>
        public int PaymentMethod { get; set; }

        /// <summary>
        /// 获取或设置订单处理时间列表
        /// </summary>
        public List<string> ProcessDatetimes { get; set; }

        /// <summary>
        /// Gets the process datetime format.
        /// </summary>
        public string ProcessDatetimeFormat
        {
            get
            {
                var times = new StringBuilder();
                if (this.ProcessDatetimes.Count > 0)
                {
                    foreach (var processDatetime in this.ProcessDatetimes)
                    {
                        times.Append(processDatetime).Append(",");
                    }

                    times = times.Remove(times.Length - 1, 1);
                }

                return times.ToString();
            }
        }
    }
}