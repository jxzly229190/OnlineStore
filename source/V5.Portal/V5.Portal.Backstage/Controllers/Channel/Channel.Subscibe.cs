using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace V5.Portal.Backstage.Controllers.Channel
{
    public partial class ChannelController
    {
        //
        // GET: /Channel.Subscibe/
        [HttpGet]
        public PartialViewResult GroupSubscribe()
        {
            return PartialView();
        }

    }
}
