// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DAFactoryConfiguration.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   配置模块数据访问工厂类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.DataAccess
{
    using V5.DataAccess.Configuration;

    /// <summary>
    /// 配置模块数据访问工厂类
    /// </summary>
    public class DAFactoryConfiguration : DataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DAFactoryConfiguration"/> class.
        /// </summary>
        public DAFactoryConfiguration()
        {
            this.AssemblyPath = this.AssemblyPath + ".Configuration";
        }

        /// <summary>
        /// The create ConfigDeliveryCorporation DA
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigDeliveryCorporationDA"/>.
        /// </returns>
        public IConfigDeliveryCorporationDA CreateConfigDeliveryCorporationDA()
        {
            string nameSpace = AssemblyPath + ".ConfigDeliveryCorporationDA";
            object corporationDA = Create(AssemblyPath, nameSpace);
            return (IConfigDeliveryCorporationDA)corporationDA;
        }

        /// <summary>
        /// 创建快递运费操作对象
        /// </summary>
        /// <returns>运费数据库操作对象</returns>
        public IConfigDeliveryCostDA CreateConfigDeliveryCostDA()
        {
            string nameSpace = AssemblyPath + ".ConfigDeliveryCostDA";
            object costDA = Create(AssemblyPath, nameSpace);
            return (IConfigDeliveryCostDA)costDA;
        }

        public IConfigDeliveryMethodDA CreateConfigDeliveryMethodDA()
        {
            string nameSpace = AssemblyPath + ".ConfigDeliveryMethodDA";
            object configDeliveryMethodDA = Create(AssemblyPath, nameSpace);
            return (IConfigDeliveryMethodDA)configDeliveryMethodDA;
        }

        public IConfigPaymentTypeDA CreateConfigPaymentTypeDA()
        {
            string nameSpace = AssemblyPath + ".ConfigPaymentTypeDA";
            object paymentTypeDA = Create(AssemblyPath, nameSpace);
            return (IConfigPaymentTypeDA)paymentTypeDA;
        }

        public IConfigPaymentOrganizationDA CreateConfigPaymentOrganizationDA()
        {
            string nameSpace = AssemblyPath + ".ConfigPaymentOrganizationDA";
            object paymentOrganizationDA = Create(AssemblyPath, nameSpace);
            return (IConfigPaymentOrganizationDA)paymentOrganizationDA;
        }

        public IConfigInvoiceTypeDA CreateConfigInvoiceTypeDA()
        {
            string nameSpace = AssemblyPath + ".ConfigInvoiceTypeDA";
            object configInvoiceTypeDA = Create(AssemblyPath, nameSpace);
            return (IConfigInvoiceTypeDA)configInvoiceTypeDA;
        }

        public IConfigInvoiceContentDA CreateConfigInvoiceContentDA()
        {
            string nameSpace = AssemblyPath + ".ConfigInvoiceContentDA";
            object configInvoiceConentDA = Create(AssemblyPath, nameSpace);
            return (IConfigInvoiceContentDA)configInvoiceConentDA;
        }

        public IConfigPageDA CreateConfigPageDA()
        {
            string nameSpace = AssemblyPath + ".ConfigPageDA";
            object ConfigPageDA = Create(AssemblyPath, nameSpace);
            return (IConfigPageDA)ConfigPageDA;
        }
    }
}