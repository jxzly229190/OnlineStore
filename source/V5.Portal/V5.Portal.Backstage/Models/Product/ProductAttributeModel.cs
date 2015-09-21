using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V5.Portal.Backstage.Models.Product
{
    using global::System.ComponentModel.DataAnnotations;

    public class ProductAttributeModel
    {
        #region Public Properties

        /// <summary>
        ///     获取或设置．
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置商品类别编号．
        /// </summary>
        public int ProductCategoryID { get; set; }

        /// <summary>
        /// 获取或设置属性名称．
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "属性名称不能为空")]
        public string AttributeName { get; set; }

        /// <summary>
        /// 获取或设置排序编号．
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        /// 获取或设置创建时间．
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 输入类型 
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        ///     获取或设置显示类型
        /// </summary>
        public string DisplayType { get; set; }

        /// <summary>
        ///     获取或设置数据类型
        /// </summary>
        public string DataType { get; set; }

        public string DataTypeDisply
        {

            get
            {
                switch (DataType)
                {
                    case "int":
                        return "整数";
                    case "float":
                        return "小数";
                    case "string":
                        return "字符串";

                }
                return null;
            }
        }

        public string InputTypeDisplay
        {
            get
            {
                switch (InputType)
                {
                    case "select":
                        return "下拉";
                    case "text":
                        return "文本";
                    case "number":
                        return "数字";
                    case "radio":
                        return "单选";
                    case "checkbox":
                        return "多选";
                }
                return null;
            }

        }

        /// <summary>
        ///     获取或设置数据长度
        /// </summary>
        public int DataLength { get; set; }

        /// <summary>
        ///     获取或设置属性编码
        /// </summary>
        public string AttributeCode { get; set; }

        #region 新增属性对应多个属性值

        /// <summary>
        /// 属性对应的属性值集合 2013/11/04
        /// </summary>
        public List<ProductAttributeValueModel> ProductAttributeValues { get; set; }

        #endregion

        #endregion
    }
}