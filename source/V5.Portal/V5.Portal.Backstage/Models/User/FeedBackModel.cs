using NPOI.HSSF.Util;
using NPOI.SS.Formula.Functions;

namespace V5.Portal.Backstage.Models.User
{
    /// <summary>
    /// 意见反馈类
    /// </summary>
    public class FeedBackModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 意见类类型
        /// </summary>
        public int Type { get; set; }

        public string TypeDisplay
        {
            get
            {
                switch (Type)
                {
                    case 1:
                        return "网站浏览";
                    case 2:
                        return "注册登陆";
                    case 3:
                        return "商品引进需求";
                    case 4:
                        return "退换货";
                    case 5:
                        return "客户服务";
                    case 6:
                        return "访问速度或故障";
                    case 7:
                        return "广告相关";
                    case 8:
                        return "其他";
                }
                return null;
            }
        }

        /// <summary>
        /// 内容 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// 性别展示
        /// </summary>
        public string GenderDisplay
        {
            get
            {
                if (Gender)
                {
                    return "男";
                }
                return "女";
            }
        }

        /// <summary>
        /// 购酒网账号
        /// </summary>
        public string GjwNumber { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 联系号码
        /// </summary>
        public string TelPhone { get; set; }
    }
}
