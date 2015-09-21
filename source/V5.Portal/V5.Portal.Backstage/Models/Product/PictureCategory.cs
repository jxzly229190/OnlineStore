// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PictureCategory.cs" company="www.gjw.com">
//  (C) 2013 www.gjw.com. All rights reserved. 
// </copyright>
// <summary>
//   图片类别模型类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Product
{
    using global::System.Collections.Generic;

    using V5.Portal.Backstage.Controllers.Picture;

    /// <summary>
    /// 图片类别模型类
    /// </summary>
    public class PictureCategory
    {
        /// <summary>
        /// The sub picture categories.
        /// </summary>
        private List<PictureCategory> subPictureCategories;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the sub picture categories.
        /// </summary>
        public List<PictureCategory> SubPictureCategories
        {
            get
            {
                if (this.Level < 1)
                {
                    return this.subPictureCategories;
                }

                var pictureController = new PictureController();

                if (this.Level == 1)
                {
                    this.subPictureCategories = pictureController.GetPictureCategories(0, this);
                }

                if (this.Level >= 2)
                {
                    this.subPictureCategories = pictureController.GetPictureCategories(1, this);
                }

                return this.subPictureCategories;
            }

            set
            {
                this.subPictureCategories = value;
            }
        }
    }
}