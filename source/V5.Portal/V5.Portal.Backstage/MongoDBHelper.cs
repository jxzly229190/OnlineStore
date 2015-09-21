namespace V5.Portal.Backstage
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using V5.DataContract.System;
    using V5.Library.Storage.DB.NoSql;
    using V5.Service.System;

    /// <summary>
    /// The mongo db helper.
    /// </summary>
    public static class MongoDBHelper
    {
        /// <summary>
        /// The refresh collection.
        /// </summary>
        /// <param name="refreshCollectionName">
        /// The refresh collection name.
        /// </param>
        public static void RefreshCollection(RefreshCollectionName refreshCollectionName)
        {
            switch (refreshCollectionName)
            {
                 case RefreshCollectionName.CountyData:
                    RefreshProvinces();
                    RefreshCities();
                    RefreshCounties();
                    break;
                case RefreshCollectionName.Resources:
                    RefreshResource();
                    break;
            }
        }

        /// <summary>
        /// The refresh counties.
        /// </summary>
        public static void RefreshCounties()
        {
            var mongoDbStore = new MongoDbStore<County>("Counties");
            mongoDbStore.Delete(item => item.ID != 0);

            var systemHomeService = new SystemHomeService();
            var counties = systemHomeService.QueryCounties();
            foreach (var county in counties)
            {
                mongoDbStore.Insert(county);
            }
        }

        /// <summary>
        /// 根据城市ID获取县区数据列表
        /// </summary>
        /// <param name="cityId">
        /// The city id.
        /// </param>
        /// <returns>
        /// 返回查询结果
        /// </returns>
        public static IList<County> GetCountiesByCityID(int cityId)
        {
            if (cityId < 1)
            {
                return new List<County>();
            }

            var mongoDbStore = new MongoDbStore<County>("Counties");
            var queries = mongoDbStore.List(c => c.CityID == cityId);
            if (queries == null)
            {
                return new List<County>();
            }

            return queries.ToList();
        }

        /// <summary>
        /// The query provinces.
        /// </summary>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public static IList<Province> QueryProvinces()
        {
            var mongoDbHelper = new MongoDbStore<Province>("Provinces");
            var list = mongoDbHelper.List(p => p.ID != 0);

            return list == null ? new List<Province>() : list.ToList();
        }

        /// <summary>
        /// The query cities.
        /// </summary>
        /// <param name="provinceID">
        /// The Province ID.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public static IList<City> QueryCities(int provinceID)
        {
            var mongoDbHelper = new MongoDbStore<City>("Cities");

            var list = mongoDbHelper.List(c => c.ProvinceID == provinceID);
            if (list == null)
            {
                return new List<City>();
            }

            return list.ToList();
        }

        /// <summary>
        /// Refresh cities to MongoDb
        /// </summary>
        public static void RefreshCities()
        {
            var mongoDbStore = new MongoDbStore<City>("Cities");
            mongoDbStore.Delete(item => item.ID != 0);

            var systemHomeService = new SystemHomeService();
            var cities = systemHomeService.QueryCities();

            foreach (var city in cities)
            {
                mongoDbStore.Insert(city);
            }
        }

        /// <summary>
        /// Load provinces to MongoDb
        /// </summary>
        public static void RefreshProvinces()
        {
            var mongoDbStore = new MongoDbStore<Province>("Provinces");
            mongoDbStore.Delete(item => item.ID != 0);

            var systemHomeService = new SystemHomeService();
            var provinces = systemHomeService.QueryProvinces();

            foreach (var province in provinces)
            {
                mongoDbStore.Insert(province);
            }
        }

        /// <summary>
        /// The refresh resource.
        /// </summary>
        public static void RefreshResource()
        {
            var mongoDbStore = new MongoDbStore<System_Resources>("Resources");
            mongoDbStore.Delete(item => item.ID != 0);

            var systemResourcesService = new SystemResourcesService();
            var resources = systemResourcesService.QueryAll();

            foreach (var resource in resources)
            {
                mongoDbStore.Insert(resource);
            }
        }

        /// <summary>
        /// The get resource.
        /// </summary>
        public static IList<System_Resources> GetResource()
        {
            var mongoDbStore = new MongoDbStore<System_Resources>("Resources");
            var list = mongoDbStore.List(i => i.Key.Contains("picture"));
            return list;
        }

        /// <summary>
        /// The refresh session.
        /// </summary>
        /// <param name="userSession">
        /// The user session.
        /// </param>
        public static void RefreshSystemUserSession(SystemUserSession userSession)
        {
            var mongoDbStore = new MongoDbStore<SystemUserSession>("SystemUserSessions");
            var systemUserSession = mongoDbStore.Single(item => item.SessionID == userSession.SessionID);
            if (systemUserSession != null)
            {
                mongoDbStore.Delete(s => s.SessionID == userSession.SessionID);
            }

            mongoDbStore.Insert(userSession);
        }

        /// <summary>
        /// The get system user session.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// The <see cref="SystemUserSession"/>.
        /// </returns>
        public static SystemUserSession GetSystemUserSession(string sessionId)
        {
            var mongoDbStore = new MongoDbStore<SystemUserSession>("SystemUserSessions");
            var systemUserSession = mongoDbStore.Single(item => item.SessionID == sessionId);
            if (systemUserSession != null)
            {
                return systemUserSession;
            }
            else
            {
                return null;
            }
        }
    }



    /// <summary>
    /// The collection name.
    /// </summary>
    public enum RefreshCollectionName
    {
        CountyData, Resources
    }
}