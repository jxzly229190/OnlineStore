namespace GY_ERP_API
{
	using System.Collections.Generic;
	using System.Text;

	public class OrderAPI
	{
		private APIBase api;

		public string Get(List<string> fieldList, string condition = "", int pageIndex = 1, int pageSize = 10)
		{
			string fields = string.Empty;

			if (fieldList != null && fieldList.Count >0)
			{
				foreach (var field in fieldList)
				{
					fields += field + ",";
				}
			}

			if (string.IsNullOrEmpty(fields))
			{
				if (fields.EndsWith(","))
				{
					fields = fields.Substring(fields.Length - 1, 1);
				}
			}

			if (pageIndex < 1)
			{
				pageIndex = 1;
			}

			var paraStr =
				new StringBuilder().Append("&fields=")
					.Append("&")
					.Append("page_no=")
					.Append(pageIndex)
					.Append("&")
					.Append("page_size=")
					.Append(pageSize)
					.Append("&")
					.Append("condition=")
					.Append(condition);


			api = new APIBase("ecerp.trade.get", paraStr.ToString());
			return api.GetResponseString();
		}
	}
}