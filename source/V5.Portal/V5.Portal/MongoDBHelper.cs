
namespace V5.Portal
{
    using System;
    using System.Linq.Expressions;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    using Newtonsoft.Json;

    using V5.Library.Storage.DB.NoSql;
    using V5.Portal.Controllers;
    using V5.Portal.Models;
    using V5.Library.Reflecter;

    public static class MongoDBHelper
    {
        public static PageModel GetPageModel(string pageKey)
        {
            var collection = new MongoDbStore<PageModel>(ConvertCollectionName(CollectionName.Pages));
            return collection.Single(m => m.PageKey == pageKey);
        }

        /// <summary>
        /// The get model.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T GetModel<T>(Expression<Func<T, bool>> func) where T : class, new()
        {
            if (func == null)
            {
                return null;
            }
            CollectionName collectionName;
            var collectName = GetCollectionName(new T(), out collectionName);
            return new MongoDbStore<T>(collectName).Single(func);
        }


		/// <summary>
		/// 根据条件获取Model列表
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		public static List<T> GetModels<T>(Expression<Func<T, bool>> func) where T : class, new()
		{
			if (func == null)
			{
				return null;
			}
			CollectionName collectionName;
			var collectName = GetCollectionName(new T(), out collectionName);
			return new MongoDbStore<T>(collectName).List(func);
		}

        public static void DeleteModel<T>(Expression<Func<T, bool>> func) where T : class, new()
        {
            if (func == null)
            {
                return;
            }

            CollectionName collectionName;
            var collectName = GetCollectionName(new T(), out collectionName);
            new MongoDbStore<T>(collectName).Delete(func);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IList"/>.
        /// </returns>
        public static IList<T> List<T>(Expression<Func<T, bool>> func) where T : class, new()
        {
            CollectionName collectionName;
            var collectName = GetCollectionName(new T(), out collectionName);
            return new MongoDbStore<T>(collectName).List(func);
        }
        
        /*
        /// <summary>
        /// The update page model.
        /// </summary>
        /// <param name="page">
        /// The page.
        /// </param>
        public static void UpdatePageModel(PageModel page)
        {
            var collection = new MongoDbStore<PageModel>(ConvertCollectionName(CollectionName.Pages));
            collection.Update(page, p => p.PageKey == page.PageKey);
        }

        /// <summary>
        /// 更新mongoDB中的对象
        /// </summary>
        /// <param name="model">
        /// 要更新的对象
        /// </param>
        public static void Update(object model)
        {
            CollectionName collectionName;
            var collect = GetCollectionName(model, out collectionName);
            switch (collectionName)
            {
                case CollectionName.Pages:
                    var pageCollect = new MongoDbStore<PageModel>(collect);
                    var page = model as PageModel;
                    if (page != null)
                    {
                        var target = pageCollect.Single(p => p.PageKey == page.PageKey);
                        if (target == null)
                        {
                            pageCollect.Insert(page);
                        }
                        else
                        {
                            ResetModel<PageModel>(ref target, ref page);
                            pageCollect.Update(target, p => p.PageKey == target.PageKey);
                        }
                    }

                    break;
                case CollectionName.UserSessions:
                    var userCollect = new MongoDbStore<UserSession>(collect);
                    var userSession = model as UserSession;
                    if (userSession != null)
                    {
                        var targetSession = userCollect.Single(p => p.SessionId == userSession.SessionId);
                        if (targetSession == null)
                        {
                            userCollect.Insert(userSession);
                        }
                        else
                        {
                            ResetModel<UserSession>(ref targetSession, ref userSession);
                            userCollect.Update(targetSession, p => p.SessionId == targetSession.SessionId);
                        }
                    }
                    
                    break;
                case CollectionName.UserCart:
                    var userCart = model as UserCartModel;
                    if (userCart.UserId > 0)
                    {
                        UpdateModel<UserCartModel>(userCart, uc => uc.UserId == userCart.UserId);
                    }
                    else
                    {
                        UpdateModel<UserCartModel>(userCart, uc => uc.VisitorKey == userCart.VisitorKey);
                    }

                    break;
                case CollectionName.Province:
                    var province = model as Province;
                    UpdateModel<Province>(province, p => p.ID == province.ID);
                    break;
                case CollectionName.City:
                    var city = model as City;
                    UpdateModel<City>(city, p => p.ID == city.ID);
                    break;
                case CollectionName.County:
                    var county = model as County;
                    UpdateModel<County>(county, p => p.ID == county.ID);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
         */

        public static void UpdateModel<T>(T model, Expression<Func<T, bool>> func) where T:class 
        {
            CollectionName collectionName;
            var collect = GetCollectionName(model, out collectionName);

            var collectStore = new MongoDbStore<T>(collect);
            if (model != null)
            {
                var targetSession = collectStore.Single(func);
                if (targetSession == null)
                {
                    collectStore.Insert(model);
                }
                else
                {
                    //ResetModel<T>(ref targetSession, ref model);
                    collectStore.Update(model, func);
                }
            }
        }

		/// <summary>
		/// 移除一个对象
		/// </summary>
		/// <typeparam name="T">移除对象的类型</typeparam>
		/// <param name="func">移除条件</param>
		public static void RemoveModel<T>(Expression<Func<T, bool>> func) where T : class 
	    {
			CollectionName collectionName;
			var collect = GetCollectionName(typeof(T), out collectionName);
			var collectStore = new MongoDbStore<T>(collect);
			collectStore.Delete(func);
	    }

        /// <summary>
        /// The reset model.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// 指定的参数类型
        /// </typeparam>
        private static void ResetModel<T>(ref T target, ref T source)
        {
            foreach (var sourceProperty in source.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                foreach (var targetProperty in target.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    try
                    {
                        // 属性名一致，且源数据属性值不为Null。
                        if (sourceProperty.Name == targetProperty.Name)
                        {
                            targetProperty.SetValue(target, sourceProperty.GetValue(source));
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
        }

		/// <summary>
		/// 将数据批量更新进MongoDB数据库中
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="modelList"></param>
		/// <param name="func"></param>
		public static List<T> GetMongoDBModels<T>(Expression<Func<T, bool>> func) where T : class
		{
			CollectionName collectionName;
			var collectName = GetCollectionName(typeof(T), out collectionName);
			var dbStore = new MongoDbStore<T>(collectName);
			return dbStore.List(func);
		}

        private static string GetCollectionName(object model, out CollectionName collectionName)
        {
            var type = model.GetType();
            return GetCollectionName(type, out collectionName);
        }

        private static string GetCollectionName(Type type, out CollectionName collectionName)
        {
            switch (type.Name)
            {
                case "PageModel":
                    collectionName = CollectionName.Pages;
                    return ConvertCollectionName(collectionName);
                case "UserSession":
                    collectionName = CollectionName.UserSessions;
                    return ConvertCollectionName(collectionName);
                case "UserCartModel":
                    collectionName = CollectionName.UserCart;
                    return ConvertCollectionName(collectionName);
                case "Province":
                    collectionName = CollectionName.Province;
                    return ConvertCollectionName(collectionName);
                case "City":
                    collectionName = CollectionName.City;
                    return ConvertCollectionName(collectionName);
                case "County":
                    collectionName = CollectionName.County;
                    return ConvertCollectionName(collectionName);
                default:
                    collectionName = CollectionName.NotDefined;
		            return type.Name;
            }
        }

        /// <summary>
        /// The convert collection name.
        /// </summary>
        /// <param name="collectionName">
        /// The collection name.
        /// </param>
        /// <returns>
        /// The 
        /// </returns>
		public static string ConvertCollectionName(CollectionName collectionName, string typeName = null)
        {
            switch (collectionName)
            {
                case CollectionName.Pages:
                    return "Pages";
                case CollectionName.UserSessions:
                    return "UserSessions";
                case CollectionName.UserCart:
                    return "UserCarts";
                case CollectionName.Province:
                    return "Provinces";
                case CollectionName.City:
                    return "Cities";
                case CollectionName.County:
                    return "Counties";
                default:
		            return typeName;
            }
        }
    }

    public enum CollectionName
    {
        Pages, // 页面集合
        UserSessions, // 前台用户会话对象
        UserCart,    // 购物车
        Province,   //省、自治区
        City,       //城市
        County,     //县\区
		NotDefined  //未定义的集合对象
    }
}
