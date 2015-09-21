// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Channel.AddGroupBuy.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   Defines the ChannelController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.UI.WebControls.WebParts;
using V5.Library.Logger;

namespace V5.Portal.Backstage.Controllers.Channel
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Drawing;
    using global::System.IO;
    using global::System.Text;
    using global::System.Web;
    using global::System.Web.Mvc;

    using Kendo.Mvc.UI;

    using V5.DataContract.Channel;
    using V5.DataContract.Product;
    using V5.Library;
    using V5.Library.Storage.DB;
    using V5.Portal.Backstage.Models.Channel;
    using V5.Portal.Backstage.Models.Product;
    using V5.Service.Channel;

    /// <summary>
    /// The channel controller.
    /// </summary>
    public partial class ChannelController
    {
        // GET: /Channel.Addlist/

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public PartialViewResult AddgroupBuyList()
        {
            return this.PartialView("AddgroupBuyList");
        }

        /// <summary>
        /// 分页查询商品
        /// </summary>
        /// <param name="request">数据对象</param>
        /// <param name="productBarCode">商品条形码</param>
        /// <param name="productName">商品名称</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryProduct([DataSourceRequest] DataSourceRequest request, string productBarCode, string productName)
        {
            if (request.Page <= 0)
            {
                request.Page = 1;
            }

            if (request.PageSize == 0)
            {
                request.PageSize = 5;
            }

            var condition = new StringBuilder();

            if (!string.IsNullOrEmpty(productBarCode))
            {
                CheckCondition(condition);
                condition.Append("[Barcode] like '%" + productBarCode + "%'");
            }

            if (!string.IsNullOrEmpty(productName))
            {
                CheckCondition(condition);
                condition.Append("[Name] like '%" + productName + "%'");
            }
            try
            {
                int pageCount;
                int totalCount;
                var paging = new Paging("[Product]", null, "ID", condition.ToString(), request.Page, request.PageSize);
                this.channelGroupBuyService = new ChannelGroupBuyService();
                var list = this.channelGroupBuyService.QueryProduct(paging, out pageCount, out totalCount);
                var modelList = new List<ProductModel>();
                if (list != null)
                {
                    foreach (var product in list)
                    {
                        modelList.Add(DataTransfer.Transfer<ProductModel>(product, typeof(Product)));
                    }

                    var data = new DataSource
                    {
                        Data = modelList,
                        Total = totalCount
                    };
                    return this.Json(data);
                }
            }
            catch (Exception exception)
            {
                TextLogger.Instance.Log("查询出错", Category.Error, exception);
            }
            return this.Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据商品ID查视图分页
        /// </summary>
        /// <param name="request"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QueryGroupProduct([DataSourceRequest] DataSourceRequest request, string setting)
        {
            var queryWhere = string.Empty;
            if (!string.IsNullOrEmpty(setting))
            {
                var strsetting = setting.Split(',');

                foreach (var s in strsetting)
                {
                    queryWhere += " [ID]=" + s + " OR";
                }

                queryWhere = queryWhere.TrimEnd(new[] { 'O', 'R' });


                if (request.Page <= 0)
                {
                    request.Page = 1;
                }

                if (request.PageSize == 0)
                {
                    request.PageSize = 5;
                }


                try
                {
                    int totalCount;
                    this.channelGroupBuyService = new ChannelGroupBuyService();
                    List<Product> list;
                    var paging = new Paging("[Product]", null, "ID", queryWhere, request.Page, request.PageSize);
                    int pageCount;
                    list = channelGroupBuyService.QueryProduct(paging, out pageCount, out totalCount);
                    if (list != null)
                    {
                        var modelList = new List<ProductModel>();

                        foreach (var groupBuyProduct in list)
                        {
                            modelList.Add(DataTransfer.Transfer<ProductModel>(groupBuyProduct, typeof(Product)));
                        }

                        var data = new DataSource
                        {
                            Data = modelList,
                            Total = totalCount
                        };
                        return this.Json(data);
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
            return this.Json(string.Empty);
        }
        #region 添加团购方法
        /// <summary>
        /// 添加团购方法
        /// </summary>
        [HttpPost]
        public void AddGroupBuy()
        {
            var channelGroupBuyModel = new ChannelGroupBuyModel();
            if (!string.IsNullOrEmpty(Request.Form["productId"]))
            {
                channelGroupBuyModel.ProductID = int.Parse(Request.Form["productId"]);
            }

            if (!string.IsNullOrEmpty(Request.Form["GroupBuyName"]))
            {
                channelGroupBuyModel.Name = Request.Form["GroupBuyName"];
            }
            if (!string.IsNullOrEmpty(Request.Form["startTime"]))
            {
                channelGroupBuyModel.StartTime = DateTime.Parse(Request.Form["startTime"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["endTime"]))
            {
                channelGroupBuyModel.EndTime = DateTime.Parse(Request.Form["endTime"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["levelID"]))
            {
                channelGroupBuyModel.UserLevelID = int.Parse(Request.Form["levelID"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["Level"]))
            {
                channelGroupBuyModel.ShowLevel = int.Parse(Request.Form["Level"]);
            }

            if (!string.IsNullOrEmpty(Request.Form["isShowTime"]))
            {
                channelGroupBuyModel.IsShowTime = bool.Parse(Request.Form["isShowTime"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["IsOnlinePayment"]))
            {
                channelGroupBuyModel.IsOnlinePayment = bool.Parse(Request.Form["IsOnlinePayment"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["totalNumber"]))
            {
                channelGroupBuyModel.TotalNumber = int.Parse(Request.Form["totalNumber"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["gbPrice"]))
            {
                channelGroupBuyModel.GBPrice = double.Parse(Request.Form["gbPrice"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["soldOfReality"]))
            {
                channelGroupBuyModel.SoldOfReality = int.Parse(Request.Form["soldOfReality"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["soldOfVirtual"]))
            {
                channelGroupBuyModel.SoldOfVirtual = int.Parse(Request.Form["soldOfVirtual"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["pageView"]))
            {
                channelGroupBuyModel.PageView = int.Parse(Request.Form["pageView"]);
            }
            if (!string.IsNullOrEmpty(Request.Form["beizhu"]))
            {
                channelGroupBuyModel.Introduce = Request.Form["beizhu"];
            }
            else
            {
                channelGroupBuyModel.Introduce = " ";
            }
            channelGroupBuyModel.CreateTime = DateTime.Now;
            channelGroupBuyModel.Status = 2;
            this.channelGroupBuyService = new ChannelGroupBuyService();
            var channelGroupBuy = DataTransfer.Transfer<Channel_GroupBuy>(channelGroupBuyModel, typeof(ChannelGroupBuyModel));

            //根据商品的ID但询是否在团购列表里面商品已经参加团购，此时修改团购数据
            try
            {
                List<Channel_GroupBuy> listProduct = channelGroupBuyService.QueryGroupBuyByProductId(channelGroupBuyModel.ProductID);
                if (listProduct.Count > 0)
                {
                    this.channelGroupBuyService.UpdateGroupBuyProductId(channelGroupBuy);
                }
                else
                {
                    this.channelGroupBuyService.Insert(channelGroupBuy);
                }
            }
            catch (Exception exception)
            {
                Response.Write("添加失败");
                throw new Exception(exception.Message, exception);
            }


            Response.Write("添加成功");
        }
        #endregion

        /// <summary>
        /// 查看商品详细信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public PartialViewResult ProductByIdDetail(int productId)
        {
            try
            {
                this.channelGroupBuyService = new ChannelGroupBuyService();

                var product = channelGroupBuyService.QueryProductById(productId);
                var productModel = DataTransfer.Transfer<ProductModel>(product, typeof(Product));
                return this.PartialView("ProductByIdDetail", productModel);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="pictureUpload">上传的图片</param>
        /// <param name="productId">对应商品的ID</param>
        /// <returns></returns>
        public ActionResult UploadPicture(IEnumerable<HttpPostedFileBase> pictureUpload, string productId)
        {
            if (pictureUpload == null)
            {
                return Json(string.Empty);
            }

            try
            {
                foreach (var file in pictureUpload)
                {
                    var displayName = Path.GetFileName(file.FileName);
                    var fileName = Guid.NewGuid().ToString();

                    var rootPath = this.Server.MapPath("~/Images/Channel/");
                    var path = rootPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + DateTime.Now.Day + "\\";
                    var thumbnailPath = rootPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + DateTime.Now.Day + "\\Thumbnail\\";
                    if (displayName == null)
                    {
                        continue;
                    }

                    var filePath = "/Images/Channel/" + DateTime.Now.Year + "/" + DateTime.Now.Month + DateTime.Now.Day + "/" + fileName + "." + displayName.Split('.')[1];

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (!Directory.Exists(thumbnailPath))
                    {
                        Directory.CreateDirectory(thumbnailPath);
                    }

                    file.SaveAs(path + fileName + "." + fileName);
                    var groupbuy = new ChannelGroupBuyModel
                    {
                        ProductID = int.Parse(productId),
                        Name = " ",
                        UserLevelID = 1,
                        CreateTime = DateTime.Now,
                        GBPrice = 1,
                        TotalNumber = 1,
                        ShowLevel = 1,
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        SoldOfReality = 1,
                        SoldOfVirtual = 1,
                        PageView = 1,
                        Status = 0,
                        ImageUrl = "///",
                        Introduce = "///"
                    };
                    channelGroupBuyService = new ChannelGroupBuyService();
                    if (channelGroupBuyService.QueryGroupBuyByProductId(groupbuy.ProductID).Count > 0)
                    {
                        channelGroupBuyService.UpdateImg(groupbuy.ProductID, filePath);
                    }
                    else
                    {
                        var groupBuyModel = DataTransfer.Transfer<Channel_GroupBuy>(groupbuy, typeof(ChannelGroupBuyModel));
                        channelGroupBuyService.Insert(groupBuyModel);
                        channelGroupBuyService.UpdateImg(groupBuyModel.ProductID, filePath);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }

            return this.Json(string.Empty);
        }

        public ActionResult RemovePicture(string[] fileNames)
        {
            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (global::System.IO.File.Exists(physicalPath))
                    {

                    }
                }
            }

            return Content(string.Empty);
        }


    }
}
