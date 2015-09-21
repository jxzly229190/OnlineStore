// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageEditService.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   生成不同尺寸的图片.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Service.Picture
{
    using System.Web;

    using V5.DataAccess.Picture;

    /// <summary>
    /// 生成不同尺寸的图片.
    /// </summary>
    public class ImageEditService
    {
        /// <summary>
        /// 生成商品图片
        /// </summary>
        /// <param name="original">
        /// 原始图片地址.
        /// </param>
        /// <param name="savePath">
        /// 保存路径.
        /// </param>
        public void CreateProductPic(string original, string savePath)
        {
            var imageName = System.IO.Path.GetFileNameWithoutExtension(original); // 文件名（扩展名）
            ImageEdit im = new ImageEdit();
            im.OriginalImagePath = original;
            im.Mode = HWMode.Fill;

            // 生成 50x50
            im.Width = 50;
            im.Height = 50;
            im.SavePath = savePath + imageName + "_0.jpg";
            im.CreateNew();

            // 生成 160x160
            im.Width = 160;
            im.Height = 160;
            im.SavePath = savePath + imageName + "_1.jpg";
            im.CreateNew();

            // 生成 230x230
            im.Width = 230;
            im.Height = 230;
            im.SavePath = savePath + imageName + "_2.jpg";
            im.CreateNew();

            // 生成 400x400
            im.MarkPath = HttpContext.Current.Server.MapPath("/Images/3.png");
            im.Position = PositionMode.MiddleCenter;
            im.Opacity = 50;
            im.Width = 400;
            im.Height = 400;
            im.SavePath = savePath + imageName + "_3.jpg";
            im.CreateNew();

            // 生成 650x650
            im.MarkPath = HttpContext.Current.Server.MapPath("/Images/4.png");
            im.Position = PositionMode.MiddleCenter;
            im.Opacity = 50;
            im.Width = 650;
            im.Height = 650;
            im.SavePath = savePath + imageName + "_4.jpg";
            im.CreateNew();
        }
    }
}
