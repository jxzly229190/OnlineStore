namespace V5.Portal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Web;

    using V5.DataContract.Product;
    using V5.DataContract.System;
    using V5.DataContract.Transact;
    using V5.DataContract.Transact.ShoppingCart;
    using V5.Library;
    using V5.Library.Logger;
    using V5.Service.Product;
    using V5.Service.System;
    using V5.Service.Transact;
    using System.Web.Caching;

    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 产品
        /// </summary>
        Product,

        /// <summary>
        /// 品牌
        /// </summary>
        ProductBrand,

        /// <summary>
        /// 产品搜索
        /// </summary>
        ProductSearch,

        /// <summary>
        /// 评论
        /// </summary>
        Comment,
        
        /// <summary>
        /// 评论回复
        /// </summary>
        CommentReply,

        /// <summary>
        /// 咨询
        /// </summary>
        Consults,

        /// <summary>
        /// 省份
        /// </summary>
        Province,

        /// <summary>
        /// 城市
        /// </summary>
        City,

        /// <summary>
        /// 县市
        /// </summary>
        County
    }

    /// <summary>
    /// 刷新
    /// </summary>
    public static class Refresh
    {
        public static object objRefresh = new object();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            //List<Province> provinces = ReadCache<Province>();
            //if (provinces == null || provinces.Count == 0)
            //{
            //    Refresh.Area();
            //}
            //else
            //{
            //    HttpRuntime.Cache["Provinces"] = provinces;

            //    List<City> cities = ReadCache<City>();
            //    HttpRuntime.Cache["Cities"] = cities;

            //    List<County> counties = ReadCache<County>();
            //    HttpRuntime.Cache["Counties"] = counties;
            //}

            List<Product_Cache> cartProduct = ReadCache<Product_Cache>();
            InsertCache("Product_Cache", cartProduct);

            List<Product_Category_Brand> productBrand = ReadCache<Product_Category_Brand>();
            InsertCache("Product_Category_Brand", productBrand);

            List<Product_Comment> comments = ReadCache<Product_Comment>();
            InsertCache("ProductComment", comments);

            List<Product_Comment_Reply> commentReplys = ReadCache<Product_Comment_Reply>();
            InsertCache("CommentReply", commentReplys);

            List<Product_Consult> consults = ReadCache<Product_Consult>();
            InsertCache("ProductConsult", consults);

            int i = 0;
            while (true)
            {
                // 休眠时间
                int refreshTime = Utils.ToInteger(System.Configuration.ConfigurationManager.AppSettings["RefreshTime"], 10);

                LogUtils.Log(Thread.CurrentThread.Name + ", 01.刷新服务开始运行：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Init", Category.Fatal);

                Product(); // 更新产品

                if (i == 0)
                {
                    ProductSearch(); //更新产品搜索
                    ProductBrand(); //商品品牌
                    Comment(); // 更新商品评论
                    CommentReply(); // 商品评论回复
                    Consults(); // 商品咨询
                }

                if (i > 20)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }

                LogUtils.Log(Thread.CurrentThread.Name + ", 11. 刷新服务任务完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ", 线程准备进入休眠, 休眠时间" + Utils.ToString(refreshTime) + " 分钟", "Refresh.Init", Category.Fatal);

                // 休眠时间
                Thread.Sleep(refreshTime * 60000);
            }
        }

        /// <summary>
        /// 更新全部商品信息.
        /// </summary>
        public static void Product()
        {
            LogUtils.Log(Thread.CurrentThread.Name + ", 02.读取产品数据开始：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Product", Category.Fatal);
            var orderBillService = new OrderBillServices();
            var productCache = orderBillService.QueryCartProductFromDB();
            LogUtils.Log(Thread.CurrentThread.Name + ", 03.读取产品数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Product", Category.Fatal);
            InsertCache("Product_Cache", productCache);
            WriteCache(productCache);
        }

        /// <summary>
        /// 更新产品搜索
        /// </summary>
        public static void ProductSearch()
        {
            //更新搜索
            new ProductService().UpdateProductSearch();
            var productSearch = new ProductService().QueryAllProductSearch();
            InsertCache("Product_Search", productSearch);
            WriteCache(productSearch);
        }
        
        /// <summary>
        /// 产品品牌
        /// </summary>
        public static void ProductBrand()
        {
            List<Product_Category_Brand> list = new ProductBrandService().SelectProductCategoryBrandAll();
            LogUtils.Log(Thread.CurrentThread.Name + ", 04. 读取产品品牌数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.ProductBrand", Category.Fatal);
            InsertCache("Product_Category_Brand", list);
            WriteCache(list);
        }
        
        /// <summary>
        /// 更新全部商品评论.
        /// </summary>
        public static void Comment()
        {
            var productComments = new ProductCommentService().QueryAll();
            LogUtils.Log(Thread.CurrentThread.Name + ", 05. 读取产品评论数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Comment", Category.Fatal);
            InsertCache("ProductComment", productComments);
            WriteCache(productComments);
        }

        /// <summary>
        /// 更新全部商品评论回复.
        /// </summary>
        public static void CommentReply()
        {
            var commentReplys = new ProductCommentReplyService().QueryAll();
            LogUtils.Log(Thread.CurrentThread.Name + ", 06. 读取产品评论回复数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.CommentReply", Category.Fatal);
            InsertCache("CommentReply", commentReplys);
            WriteCache(commentReplys);
        }

        /// <summary>
        /// 更新全部商品咨询
        /// </summary>
        public static void Consults()
        {
            var consults = new ProductConsultService().QueryAll();
            LogUtils.Log(Thread.CurrentThread.Name + ", 07. 读取产品咨询数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Consults", Category.Fatal);
            InsertCache("ProductConsult", consults);
            WriteCache(consults);
        }
        
        /// <summary>
        /// 载入区域
        /// </summary>
        public static void Area()
        {
            var systemHomeService = new SystemHomeService();

            //省份
            var provinces = systemHomeService.QueryProvinces();
            LogUtils.Log(Thread.CurrentThread.Name + ", 08. 读取省份数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Area.Province", Category.Fatal);
            InsertCache("Provinces", provinces);
            WriteCache(provinces);

            //城市
            var cities = systemHomeService.QueryCities();
            LogUtils.Log(Thread.CurrentThread.Name + ", 09. 读取城市数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Area.City", Category.Fatal);
            InsertCache("Cities", cities);
            WriteCache(cities);

            //县市
            var counties = systemHomeService.QueryCounties();
            LogUtils.Log(Thread.CurrentThread.Name + ", 10. 读取县市数据完毕：" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Refresh.Area.County", Category.Fatal);
            InsertCache("Counties", counties);
            WriteCache(counties);           
        }
        
        /// <summary>
        /// 清除静态文件
        /// </summary>
        public static void RefreshHtml(HttpResponseBase response)
        {
            lock (objRefresh)
            {
                //清除首页
                Utils.SendMessage(response, "开始清理【首页】静态页...");
                ClearHtml("index.htm", response);
                Utils.SendMessage(response, "清理完毕...");

                //清除产品
                Utils.SendMessage(response, "开始清理【产品】静态页...");
                ClearHtml("product", response);
                Utils.SendMessage(response, "清理完毕...");

                //清除品牌
                Utils.SendMessage(response, "开始清理【品牌】静态页...");
                ClearBrandHtml(response);
                Utils.SendMessage(response, "清理完毕...");

                //清除文章
                Utils.SendMessage(response, "开始清理【文章】静态页...");
                ClearHtml("home\\article", response);
                Utils.SendMessage(response, "清理完毕...");

                //清除帮助
                Utils.SendMessage(response, "开始清理【帮助】静态页...");
                ClearHtml("home\\help", response);
                Utils.SendMessage(response, "清理完毕...");

                //清除LP
                Utils.SendMessage(response, "开始清理【LP】静态页...");
                ClearHtml("home\\landingpage", response);
                Utils.SendMessage(response, "清理完毕...");

                //清除团购
                Utils.SendMessage(response, "开始清理【团购】静态页...");
                ClearHtml("home\\tuanitem", response);
                Utils.SendMessage(response, "清理完毕...");
                
                //获取产品数据
                Utils.SendMessage(response, "开始生成【商品信息】静态页...");
                List<Product_Cache> list = GetCache<Product_Cache>(CacheType.Product);
                if (list != null && list.Count > 0)
                {
                    int[] ids = (from p in list select p.ProductID).ToArray();
                    StaticHtmlHelper.Refresh<int>(ids, QueryType.Product, response);
                }
                Utils.SendMessage(response, "生成完毕...");

                //获取品牌数据
                Utils.SendMessage(response, "开始生成【商品品牌】静态页...");
                List<Product_Category_Brand> list2 = GetCache<Product_Category_Brand>(CacheType.ProductBrand, true);
                if (list2 != null && list2.Count > 0)
                {
                    //大类
                    Utils.SendMessage(response, "开始生成【商品大类】静态页...");
                    string[] ids1 = (from p in list2 select p.ProductCategory_NameSpell).Distinct().ToArray();
                    StaticHtmlHelper.Refresh<string>(ids1, QueryType.Brand, response);
                    Utils.SendMessage(response, "生成完毕...");

                    //品牌
                    Utils.SendMessage(response, "开始生成【商品品牌】静态页...");
                    string[] ids2 = (from p in list2 where p.ProductBrand_ParentID == 0 select p.ProductCategory_NameSpell + "-" + p.ProductBrand_NameSpell).Distinct().ToArray();
                    StaticHtmlHelper.Refresh<string>(ids2, QueryType.Brand, response);
                    Utils.SendMessage(response, "生成完毕...");

                    //子品牌
                    Utils.SendMessage(response, "开始生成【商品子品牌】静态页...");
                    List<Product_Category_Brand> list3 = (from p in list2 where p.ProductBrand_ParentID == 0 select p).Distinct().ToList();
                    foreach (var parent in list3)
                    {
                        string[] ids3 = (from p in list2 where p.ProductBrand_ParentID == parent.ProductBrand_ID select p.ProductCategory_NameSpell + "-" + parent.ProductBrand_NameSpell + "-" + p.ProductBrand_NameSpell).ToArray();
                        StaticHtmlHelper.Refresh<string>(ids3, QueryType.Brand, response);
                    }
                }

                //刷新首页
                Utils.SendMessage(response, "开始生成【首页】静态页...");
                StaticHtmlHelper.Refresh<string>(new string[] { "index" }, QueryType.Home, response);
                Utils.SendMessage(response, "生成完毕...");
            }
        }

        /// <summary>
        /// 刷新首页
        /// </summary>
        /// <param name="response"></param>
        public static void RefreshIndexHtml(HttpResponseBase response)
        {
            //清除首页
            Utils.SendMessage(response, "开始清理【首页】静态页...");
            ClearHtml("index.htm", response);
            Utils.SendMessage(response, "清理完毕...");

            //刷新首页
            Utils.SendMessage(response, "开始生成【首页】静态页...");
            StaticHtmlHelper.Refresh<string>(new string[] { "index" }, QueryType.Home, response);
            Utils.SendMessage(response, "生成完毕...");
        }

        /// <summary>
        /// 刷新产品数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="path"></param>
        public static void RefreshProductHtml(HttpResponseBase response, string path)
        {
            List<Product_Cache> list = GetCache<Product_Cache>(CacheType.Product);
            if (list != null && list.Count > 0)
            {
                int[] ids = (from p in list select p.ProductID).ToArray();
                RefreshProductHtml(response, path, ids);
            }
        }

        /// <summary>
        /// 刷新产品数据
        /// </summary>
        /// <param name="response"></param>
        /// <param name="path"></param>
        public static void RefreshProductHtml(HttpResponseBase response, string path, int[] ids)
        {
            //清除产品数据
            Utils.SendMessage(response, "开始清理【产品】静态页...");
            ClearHtml(path, response);
            Utils.SendMessage(response, "清理完毕...");

            //获取产品数据
            Utils.SendMessage(response, "开始生成【商品信息】静态页...");
            StaticHtmlHelper.Refresh<int>(ids, QueryType.Product, response);
            Utils.SendMessage(response, "生成完毕...");
        }

        /// <summary>
        /// 清除品牌数据(此方法会清除根目录下所有的以.htm结尾的文件,慎用)
        /// </summary>
        public static void ClearBrandHtml(HttpResponseBase response)
        {
            //获取根目录
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                var files = Directory.GetFiles(basePath);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].EndsWith(".htm"))
                    {
                        DeleteFiles(files[i], response);
                    }
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        /// <summary>
        /// 清理静态HTML
        /// </summary>
        /// <param name="path"></param>
        public static void ClearHtml(string path, HttpResponseBase response)
        {
            //获取根目录
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(basePath + path);
                    if (fileInfo.Attributes == FileAttributes.Directory)
                    {
                        var files = Directory.GetFiles(basePath + path);
                        DeleteFiles(files, response);
                    }
                    else
                    {
                        DeleteFiles(new string[] { basePath + path }, response);
                    }
                }
                catch (Exception ex)
                {
                    Utils.SendMessage(response, "清除【" + path + "】异常...,异常信息：" + ex.Message + "...");
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public static void DeleteFiles(string file)
        {
            DeleteFiles(new string[] { file });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="files"></param>
        public static void DeleteFiles(string[] files)
        {
            if (files == null || files.Length == 0) return;

            foreach (var file in files)
            {
                try
                {
                    if (!File.Exists(file)) continue;

                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    LogUtils.Log("删除文件失败:" + ex.Message + ", 文件：" + file + ", 时间" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DeleteFiles", Category.Fatal);
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="file"></param>
        public static void DeleteFiles(string file, HttpResponseBase response)
        {
            DeleteFiles(new string[] { file }, response);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="files"></param>
        public static void DeleteFiles(string[] files, HttpResponseBase response)
        {
            if (files == null || files.Length == 0) return;

            foreach (var file in files)
            {
                try
                {
                    if (!File.Exists(file))
                    {
                        Utils.SendMessage(response, "文件【" + file + "】不存在...");
                        continue;
                    }

                    File.Delete(file);

                    Utils.SendMessage(response, "清除文件【" + file + "】完毕...");
                }
                catch (Exception ex)
                {
                    Utils.SendMessage(response, "清除文件【" + file + "】异常...,异常信息：" + ex.Message + "...");
                    LogUtils.Log("删除文件失败:" + ex.Message + ", 文件：" + file + ", 时间" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DeleteFiles", Category.Fatal);
                }
            }
        }
        
        /// <summary>
        /// 从本地文件读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> ReadCache<T>() where T : new()
        {
            List<T> list = new List<T>();
            try
            {
                var type = typeof(T);
                string fileName = AppDomain.CurrentDomain.BaseDirectory + type.Name + ".txt";
                if (File.Exists(fileName))
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    list = (List<T>)bf.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtils.Log("读取本地缓存信息异常：" + ex.Message, "Refresh.ReadCache", Category.Fatal);
            }
            return list;
        }

        /// <summary>
        /// 将缓存写入本地文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        public static void WriteCache<T>(List<T> p) where T : new()
        {
            try
            {
                var type = typeof(T);
                string fileName = AppDomain.CurrentDomain.BaseDirectory + type.Name + ".txt";
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, p);
                fs.Close();
            }
            catch (Exception ex)
            {
                LogUtils.Log("写入本地缓存信息异常：" + ex.Message, "Refresh.WriteCache", Category.Fatal);
            }
        }

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetCache<T>(CacheType cacheType)
        {
            return GetCache<T>(cacheType, false);
        }

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheType"></param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public static List<T> GetCache<T>(CacheType cacheType, bool forceRefresh)
        {
            string key = string.Empty;
            switch (cacheType)
            {
                case CacheType.Product:
                    key = "Product_Cache";
                    break;
                case CacheType.ProductBrand:
                    key = "Product_Category_Brand";
                    break;
                case CacheType.ProductSearch:
                    key = "Product_Search";
                    break;
                case CacheType.Comment:
                    key = "ProductComment";
                    break;
                case CacheType.CommentReply:
                    key = "CommentReply";
                    break;
                case CacheType.Consults:
                    key = "Consults";
                    break;
                case CacheType.Province:
                    key = "Provinces";
                    break;
                case CacheType.City:
                    key = "Cities";
                    break;
                case CacheType.County:
                    key = "Counties";
                    break;
            }

            if (string.IsNullOrEmpty(key)) return null;

            List<T> list = HttpRuntime.Cache[key] as List<T>;
            if (forceRefresh)
            {
                if (list == null || list.Count == 0)
                {
                    switch (cacheType)
                    {
                        case CacheType.Product:
                            Product();
                            break;
                        case CacheType.ProductSearch:
                            ProductSearch();
                            break;
                        case CacheType.ProductBrand:
                            ProductBrand();
                            break;
                        case CacheType.Comment:
                            Comment();
                            break;
                        case CacheType.CommentReply:
                            CommentReply();
                            break;
                        case CacheType.Consults:
                            Consults();
                            break;
                        case CacheType.Province:
                        case CacheType.City:
                        case CacheType.County:
                            Area();
                            break;
                    }
                    list = HttpRuntime.Cache[key] as List<T>;
                }
            }

            return list;
        }

        /// <summary>
        /// 写入Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void InsertCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        }
    }
}