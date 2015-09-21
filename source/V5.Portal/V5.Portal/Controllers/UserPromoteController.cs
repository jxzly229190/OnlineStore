namespace V5.Portal.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Web.Mvc;

	using V5.Library;
	using V5.Portal.Models;

	public class UserPromoteController : BaseController
	{
		/// <summary>
		/// 检查用户是否登录
		/// </summary>
		/// <returns></returns>
		public ActionResult CheckLogin()
		{
			if (this.UserSession != null && this.UserSession.UserID > 0)
			{
				return this.Json(new AjaxResponse(1, "ok"), JsonRequestBehavior.AllowGet);
			}

			return this.Json(new AjaxResponse(0, "no"), JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// 参与活动
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public ActionResult Participate(int Id = 1)
		{
			MongoDBHelper.UpdateModel<UserPromote>(
				new UserPromote
					{
						GiftID = 0,
						HasGetGift = false,
						OrderID = 0,
						ParticipateTime = DateTime.Now,
						UserID = this.UserSession.UserID
					},
				u => u.UserID == this.UserSession.UserID);

			var promote = MongoDBHelper.GetModel<SpecialPromote>(p => p.ID == Id) ?? this.CreateDefaultSpecialPromote();
			UserPromote userPromote = null;

			if (promote.StartTime > DateTime.Now)
			{
				return this.Json(new AjaxResponse(-2, "对不起，此活动尚未开始。"), JsonRequestBehavior.AllowGet);
			}

			if (promote.EndTime < DateTime.Now)
			{
				return this.Json(new AjaxResponse(-2, "对不起，此活动已经结束。"), JsonRequestBehavior.AllowGet);
			}

			if (this.UserSession == null || this.UserSession.UserID < 1)
			{
				return this.Json(new AjaxResponse(0, "没有登录"), JsonRequestBehavior.AllowGet);
			}
			
			if (promote.UserPromotes == null)
			{
				promote.UserPromotes = new List<UserPromote>();
			}

			userPromote = promote.UserPromotes.FirstOrDefault(p => p.UserID == this.UserSession.UserID);
			if (userPromote == null)
			{
				userPromote = new UserPromote
				{
					HasGetGift = false,
					UserID = this.UserSession.UserID,
					ParticipateTime = DateTime.Now
				};

				promote.UserPromotes.Add(userPromote);
			}
			
			if (userPromote.HasGetGift)
			{
				return this.Json(new AjaxResponse(-1, "已经参加并领取过赠品了！"));
			}

			MongoDBHelper.UpdateModel<SpecialPromote>(promote, p => p.ID == promote.ID);

			return this.Json(new AjaxResponse(1, promote.GiftProductID));
		}

		/// <summary>
		/// 添加默认活动
		/// </summary>
		/// <returns></returns>
		private SpecialPromote CreateDefaultSpecialPromote()
		{
			//第一个活动写死了
			return this.CreatePromote("答题领取景酒",4153, DateTime.Parse("2014-3-12 09:00:00"), DateTime.Parse("2014-3-16 23:59:59"));
		}

		/// <summary>
		/// 刷新促销信息
		/// </summary>
		/// <param name="ps"></param>
		/// <returns></returns>
		public ActionResult RefreshSP(int ps)
		{
			if (ps == -100)
			{
				MongoDBHelper.RemoveModel<SpecialPromote>(p => p.ID > 0);
				return this.Content("Done");
			}

			return this.Content("OK");
		}

		/// <summary>
		/// 创建一个活动
		/// </summary>
		/// <param name="name"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns> 
		private SpecialPromote CreatePromote(string name, int giftProductId, DateTime startTime, DateTime endTime)
		{
			int Id = 1;
			var promotes = MongoDBHelper.GetModels<SpecialPromote>(p => p.ID > 0);

			if (promotes != null && promotes.Count > 0)
			{
				Id = promotes.Count;
			}

			var promote = new SpecialPromote
				              {
					              ID = Id,
					              EndTime = endTime,
					              StartTime = startTime,
					              IsValid = true,
					              PromoteName = name,
								  GiftProductID= giftProductId,
								  UserPromotes = new List<UserPromote>()
				              };

			MongoDBHelper.UpdateModel<SpecialPromote>(promote, p => p.ID == Id);

			return promote;
		}
	}
}