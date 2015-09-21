using System.Web.Http;

namespace V5.Portal.Backstage.Controllers.Utility
{
    public class UtilityApiController : ApiController
    {
        [HttpGet]
        public string Get(string business, string codeFormat, string prefixName, int tLength, string transaction)
        {
            return "business";
        }
        public string Get(int id)
        {
            return "all";
        }
    }
}
