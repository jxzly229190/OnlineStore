// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PicturePreviewModel.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.  
// </copyright>
// <summary>
//   The picture preview model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Product
{
    /// <summary>
    /// 图片预览Model.
    /// </summary>
    public class PicturePreviewModel
    {
        /// <summary>
        /// 大图.
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// 右边小图.
        /// </summary>
        public string ListImg { get; set; }
    }
}