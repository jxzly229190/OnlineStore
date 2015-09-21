using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V5.DataAccess;
using V5.DataAccess.Product;
using V5.DataContract.Product;

namespace V5.Service.Product
{
    public class BrandInformationService
    {
        private readonly IBrandInformationDA brandInformationDA;

        public BrandInformationService()
        {
            this.brandInformationDA = new DAFactoryProduct().CreateBrandDescriptionDA();
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int Insert(Brand_Information brand)
        {
            return this.brandInformationDA.Insert(brand);
        }
        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int Update(Brand_Information brand)
        {
            return this.brandInformationDA.Update(brand);
        }
        /// <summary>
        /// 根据ID查数据
        /// </summary>
        /// <param name="id">主键标识</param>
        /// <returns></returns>
        public Brand_Information QueryByID(int id)
        {
            return this.brandInformationDA.QueryByID(id);
        }

        /// <summary>
        /// 根据商品ID执行查询操作
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public Brand_Information QueryByBrandID(int brandId)
        {
            return this.brandInformationDA.QueryByBrandID(brandId);
        }

        /// <summary>
        /// 根据品牌ID执行修改操作 
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public int UpdateByBrandID(Brand_Information brand)
        {
            return this.brandInformationDA.UpdateByBrandID(brand);
        }
        /// <summary>
        /// 修改Logo
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="logo"></param>
        /// <returns></returns>
        public int UpdateLogo(int brandId, string logo)
        {
            return this.brandInformationDA.UpdateLogo(brandId, logo);
        }
        /// <summary>
        /// 修改ProductID,为字符串类型
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="productString">为字符串类型</param>
        /// <returns></returns>
        public int UpdateProductString(int brandId, string productString)
        {
            return this.brandInformationDA.UpdteProductId(brandId, productString);
        }
    }
}
