namespace V5.Portal.Backstage.Utils
{
	using System.Web.Mvc;

	public class CustomHandleErrorAttribute:HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			//todo：在此添加自定义的公共异常处理方式
			base.OnException(filterContext);
		}
	}
}