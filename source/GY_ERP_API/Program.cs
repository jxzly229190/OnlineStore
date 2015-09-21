using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GY_ERP_API
{
	using System.Net;
	using System.Xml;

	class Program
	{
		static void Main(string[] args)
		{
			OrderAPI api=new OrderAPI();
			var res = api.Get(null, null, 1, 10);

			Console.Write(res);

			XmlDocument xml =new XmlDocument();
			xml.LoadXml(res);

			var nodes=xml.SelectNodes("trade_get_response/trades/trade/djbh");
			if (nodes != null)
			{
				Console.WriteLine(nodes.Count);
				foreach (XmlElement node in nodes)
				{
					Console.WriteLine("第一条订单号：" + node.InnerText);
				}
			}

			Console.WriteLine("结束");
			Console.Read();
		}

	}
}
