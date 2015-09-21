namespace V5.Service.System
{
    using global::System.Collections.Generic;

    using V5.DataAccess;
    using V5.DataAccess.System;
    using V5.DataContract.System;

    public class SystemHomeService
    {
        #region Constants and Fields

        /// <summary>
        /// 系统用户数据访问对象
        /// </summary>
        private readonly ISystemHomeDA systemHomeDA;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemHomeService"/> class.
        /// </summary>
        public SystemHomeService()
        {
            this.systemHomeDA = new DAFactorySystem().CreateSystemHomeDA();
        }

        #endregion

        #region Public Methods and Operators

        public List<Province> QueryProvinces()
        {
            return this.systemHomeDA.SelectProvinces();
        }

        public List<City> QueryCities()
        {
            return this.systemHomeDA.SelectCities();
        }

        public List<County> QueryCounties()
        {
            return this.systemHomeDA.SelectCounties();
        }

        #endregion
    }
}