﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProductCommentSearchModel.cs" company="www.gjw.com">
//   (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   商品评论搜索Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace V5.Portal.Backstage.Models.Transact
{
    using global::System;

    public class ProductCommentSearchModel
    {
        public int StatusForSearch { get; set; }

        public string ProductName { get; set; }

        public string UserName { get; set; }

        public DateTime FromDateTime { get; set; }

        public DateTime ToDateTime { get; set; }
    }
}