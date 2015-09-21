using System.Linq;
using System.Net.Configuration;
using V5.DataContract.Transact.Order;
using V5.Service.Transact;

namespace V5.Service.Utility
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using V5.DataContract.Utility;

    public class MadeCodeService
    {
        private static CodeService codeService;
        private static OrderService orderService;
        private static readonly object obj = new object();
        /// <summary>
        /// 创建一条规则
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string MakeBssinessCode(Code code)
        {
            codeService.Insert(code);
            return "";
        }
        #region 自定义订单号的方法
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <param name="clientCode">
        /// </param>
        /// <returns></returns>
        public static string GetCodeByClientCode(string clientCode)
        {
            //长度
            codeService = new CodeService();
            List<Code> list = codeService.FindByUserCode(clientCode);
            if (list == null || list.Count == 0) return "";

            Code code = list[0];
            var sb = new StringBuilder();
            sb.Append(GetSplitCode(code, code.Business)); //业务点标识
            sb.Append(GetSplitCode(code, code.PrefixName)); //前缀
            sb.Append(GetSplitCode(code, GetDateFormat(code))); //日期格式化
            sb.Append(GetSplitCode(code, PreCreatTransactCode(code).ToString().PadLeft(code.TransactLength, '0'))); //自增
            return sb.ToString().TrimEnd(code.CodeFormat.ToArray());
        }
        /// <summary>
        /// 增加分隔符
        /// </summary>
        /// <returns></returns>
        private static string GetSplitCode(Code code, string value)
        {
            return string.IsNullOrEmpty(code.CodeFormat) ? value : value + code.CodeFormat;
        }

        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string GetDateFormat(Code code)
        {
            if (string.IsNullOrEmpty(code.DateFormat)) return "";
            return DateTime.Now.ToString(code.DateFormat);
        }
        /// <summary>
        /// 生成流水号,并检查是否有重复
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static int PreCreatTransactCode(Code code)
        {
            int result = 0;
            codeService = new CodeService();
            lock (obj)
            {
                if (code.IsIterator)
                {
                    //时间是否过期
                    if (PreCheckExpireTime(code))
                    {
                        //先产生一个编号
                        code.Iterator++;
                        code.Iterator = PreCheckCodeIsOnly(code, code.Iterator);
                        //在取得数据库的值上再自加，跳号
                        code.Iterator++;
                        //num++;
                        //更新数据库
                        codeService.UpdateIterator(code.Iterator, code.ID);
                        result = code.Iterator;
                    }
                    else
                    {
                        //时间过期
                        codeService.UpdateStartTime(DateTime.Now, code.ID, 1);
                        result = 1;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 校验C#生成编号和数据库中编吗的唯一性,当前生成系统中已作废
        /// </summary>
        /// <param name="code">编码对象</param>
        /// <param name="iterator">C#自增变量</param>
        /// <returns></returns>
        private static int PreCheckCodeIsOnly(Code code, int iterator)
        {
            codeService = new CodeService();
            int sourceCode = codeService.FindById(code.ID).Iterator;
            if (sourceCode >= iterator)
            {
                iterator++;
                PreCheckCodeIsOnly(code, iterator);
            }
            return sourceCode;
        }
        /// <summary>
        /// 过期时间 返回false就说明时间过期,更新开始时间
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static bool PreCheckExpireTime(Code code)
        {
            bool checkResult = false;
            switch (code.ExpireDate)
            {
                case 1: //1按年计算过期时间
                    if (DateTime.Now.Year - code.StartTime.Year == 0)
                    {
                        checkResult = true;
                    }
                    break;
                case 2://2按月计算过期时间
                    if (DateTime.Now.Month - code.StartTime.Month == 0)
                    {
                        checkResult = true;
                    }
                    break;
                case 3://按天计算过期时间
                    if (DateTime.Now.Day - code.StartTime.Day == 0)
                    {
                        checkResult = true;
                    }
                    break;
            }
            return checkResult;
        }
        /// <summary>
        /// 生成用户编号
        /// </summary>
        /// <returns></returns>
        public static string PreClientCode()
        {
            Code code = codeService.FindById(1);
            if (code != null)
            {
                return code.UserIterator.ToString().PadLeft(5, '0');
            }
            return "";
        }
        #endregion

        #region 生成订单编号的方法
        /// <summary>
        /// 获取订单生成编号，返回生成单时间
        /// </summary>
        /// <param name="createCodeTime">生成时间</param>
        /// <returns></returns>
        public static string GetOrderCode(out DateTime createCodeTime)
        {
            Order_Code order = GetOrder();
            if (order != null)
            {
                string resultCode = "GJW" + EncryptionCode(order);
                if (!CheckOrderCodeIsOnly(resultCode))
                {
                    GetOrderCode(out createCodeTime);
                }
                createCodeTime = order.CreateTime;
                return resultCode;
            }
            createCodeTime = new DateTime();
            return "";
        }
        /// <summary>
        /// 查询数据库反推订单号
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public static string ReverseOrderCode(string orderCode)
        {
            var _orderCode = orderService.QueryByOrderCode(orderCode);
            if (_orderCode != null)
            {
                string createCodeTime = _orderCode.CreateTime.ToString("yyyymmdd hhmmss");
                //去掉GJW的后面号码转成int类型的数
                int lasthalfCode = Convert.ToInt32(orderCode.Substring(3));
                //最后一位，为秒
                int lastSecond = Convert.ToInt32(createCodeTime.Substring(createCodeTime.Length - 1));
                int intFeed = lastSecond * 11111111;
                //减去变化的数 剩下的为没有首位数字的日期
                int orderpartialDay = lasthalfCode - intFeed;
                //把数据库时间去掉首位转换成10位数字
                int timeDay = Convert.ToInt32(_orderCode.CreateTime.ToString("yyyyMMdd").Substring(1).PadRight(11, '0'));
                //当天的单号
                int resultCode = orderpartialDay - timeDay;//当天第几单
                string year = _orderCode.CreateTime.Year.ToString();
                string month = _orderCode.CreateTime.Month.ToString();
                string day = _orderCode.CreateTime.Day.ToString();
                //生成新的未加密的单号
                string NewCode = "GJW" + year + month + day + resultCode.ToString().PadLeft(4, '0');
                return NewCode;
            }
            return "";
        }
        /// <summary>
        /// 反推订单编号
        /// </summary>
        /// <param name="orderCode">订单编号</param>
        /// <param name="createTime">生成单号时间</param>
        /// <returns></returns>
        public static string ReverseOrderCode(string orderCode, DateTime createTime)
        {
            if (orderCode != null)
            {
                string createCodeTime = createTime.ToString("yyyymmdd hhmmss");
                //去掉GJW的后面号码转成int类型的数
                int lasthalfCode = Convert.ToInt32(orderCode.Substring(3));
                //最后一位，为秒
                int lastSecond = Convert.ToInt32(createCodeTime.Substring(createCodeTime.Length - 1));
                int intFeed;
                if (lastSecond != 0)
                {
                    intFeed = lastSecond * 11111111;
                }
                else
                {
                    intFeed = 3 * 11111111;
                }

                //减去变化的数 剩下的为没有首位数字的日期
                int orderpartialDay = lasthalfCode - intFeed;
                //把数据库时间去掉首位转换成10位数字
                int timeDay = Convert.ToInt32(createTime.ToString("yyyyMMdd").Substring(1).PadRight(11, '0'));
                //当天的单号
                int resultCode = orderpartialDay - timeDay + 1;//当天第几单
                //生成新的未加密的单号
                string year = createTime.Year.ToString();
                string month = createTime.Month.ToString().PadLeft(2, '0');
                string day = createTime.Day.ToString().PadLeft(2, '0');

                string NewCode = "GJW" + year + month + day + resultCode.ToString().PadLeft(5, '0');
                return NewCode;
            }
            return "";
        }

        /// <summary>
        /// 获得最大订单编号
        /// </summary>
        /// <returns></returns>
        private static Order_Code GetOrder()
        {
            codeService = new CodeService();
            var _orderCode = codeService.GetOrderCount();
            if (_orderCode != null)
            {
                return _orderCode;
            }
            return null;
        }

        /// <summary>
        /// 加密订单编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string EncryptionCode(Order_Code code)
        {
            string nowtimeStr = DateTime.Today.ToString("yyyyMMdd");//获取当前时间
            //当前时间去掉第一位，加上最大订单编号，保证10位
            string strNo = nowtimeStr.Substring(1) + code.OrderCode.ToString().PadLeft(4, '0');
            string strlastTime = code.CreateTime.ToString("yyyyMMdd HHmmss");
            //当前时间的最后一位
            int lastTime = int.Parse(strlastTime.Substring(strlastTime.Length - 1));
            //根据秒自增
            lastTime = lastTime == 0 ? 3 : lastTime;

            //当前时间最后一位乘111111111
            int intfeed = lastTime * 11111111;
            long longFeed = Convert.ToInt64(strNo) + intfeed;
            return longFeed.ToString();
        }
        /// <summary>
        /// 检查订单是否重复
        /// </summary>
        /// <param name="orderCode">订单编号</param>
        /// <returns></returns>
        private static bool CheckOrderCodeIsOnly(string orderCode)
        {
            orderService = new OrderService();
            var order = orderService.QueryByOrderCode(orderCode);
            if (order != null)
            {
                if (order.OrderCode == orderCode)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
