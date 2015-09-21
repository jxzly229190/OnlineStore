// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemController.cs" company="www.gjw.com">
// (C) 2013 www.gjw.com. All rights reserved.
// </copyright>
// <summary>
//   The ChannelGroupBuyDA interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Mvc;
using System.Web.Routing;
using V5.Library;
using V5.Library.Storage.DB.NoSql;

namespace V5.Portal.Backstage.Controllers.System
{
    using V5.Library.Logger;
    using V5.Library.Security;
    using V5.Service.System;

    public partial class SystemController : BaseController
    {
        //
        // GET: /system.ModifyPasswd/

        public PartialViewResult ModifyPasswd()
        {
            return PartialView();
        }
        [HttpPost]
        public void UpdatePwd(string loginName, string userId, string password, string oldpwd)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginName) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(oldpwd))
                {
                    this.systemUserService = new SystemUserService();
                    var userModel = this.systemUserService.QueryByLoginName(loginName);
                    string str = Encrypt.HashBySHA1(loginName + oldpwd);
                    if (userModel.LoginPassword != str)
                    {
                        Response.Write(1);

                    }
                    else
                    {
                        var loginPassword = Encrypt.HashBySHA1(loginName + password);
                        var id = int.Parse(userId);
                        LogUtils.Log("用户“" + loginName + "”修改密码", "UpdatePwd", Category.Info, Session.SessionID);
                        var recevieid = this.systemUserService.UpdatePassWord(id, loginPassword);
                        if (recevieid > 0)
                        {
                            Response.Write(2);
                            var mongoDbStore = new MongoDbStore<SystemUserSession>("SystemUserSessions");
                            mongoDbStore.Delete(item => item.SessionID == this.Session.SessionID);
                        }
                    }

                }
            }
            catch (Exception exception)
            {

                throw new ArgumentNullException(exception.Message, exception);
            }
        }
    }
}
