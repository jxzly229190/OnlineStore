namespace GY_ERP_API
{
	using System.Net;
	using System.Text;

	public class APIBase
	{
		private WebClient ApiClient;

		public APIBase(string apiName, string paraStr="",string urlStr="")
		{
			ApiClient =new WebClient();

			ApiClient.QueryString["method"] = "ReqApi";
			ApiClient.QueryString["issysStr"] = "true";
			ApiClient.QueryString["appkeyStr"] = "系统分配";
			ApiClient.QueryString["nameStr"] = apiName;
			ApiClient.QueryString["paraStr"] = paraStr;
			ApiClient.QueryString["urlStr"] = urlStr;
		}

		public string GetResponseString()
		{
			var bytes = ApiClient.UploadValues(
				"http://api.guanyisoft.com/Handler/tools.ashx",
				"post",
				this.ApiClient.QueryString);

			return Encoding.UTF8.GetString(bytes);
		}
	}
}