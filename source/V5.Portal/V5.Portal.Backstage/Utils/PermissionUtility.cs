namespace V5.Portal.Backstage.Utils
{
    using System.Collections.Generic;

    using Kendo.Mvc.Infrastructure.Implementation;

    using V5.Portal.Backstage.Controllers.System;

    public class PermissionUtility
    {
        private readonly string _sessionId;

        private bool _hidden;

        public bool Hidden
        {
            get
            {
                return !this._hidden;
            }
            set
            {
                this._hidden = value;
            }
        }

        public PermissionUtility(string sessionID)
        {
            this._sessionId = sessionID;
            this._hidden = false;
        }

        /// <summary>
        /// 获取显示Attribute字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetDisplayAttributeInfo(string key)
        {
            return GetDisplayAttributeInfo(key, true);
        }

        /// <summary>
        /// 获取显示Attribute字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="columnHiddenFlag"></param>
        /// <returns></returns>
        public string GetDisplayAttributeInfo(string key, bool columnHiddenFlag)
        {
            var obj = GetDisplayAttribute(key, columnHiddenFlag);
            if (obj == null) return "";
            return obj["style"] == null ? "" : obj["style"].ToString();
        }

        /// <summary>
        /// 获取显示Attribute字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="columnHiddenFlag"></param>
        /// <returns></returns>
        public string GetStyleAttributeInfo(string key, bool columnHiddenFlag, string  extCss = "")
        {
            var obj = GetDisplayAttribute(key, columnHiddenFlag);
            if (obj == null || obj["style"] == null) return "style=" + extCss;
            return "style=" + extCss + obj["style"].ToString();
        }

        /// <summary>
        /// 获取显示Attribute
        /// </summary>
        /// <param name="key">
        /// 资源标识符
        /// </param>
        /// <returns>
        /// 返回字典
        /// </returns>
        public IDictionary<string, object> GetDisplayAttribute(string key)
        {
            return GetDisplayAttribute(key, true);
        }

        /// <summary>
        /// 获取显示Attribute
        /// </summary>
        /// <param name="key">
        /// 资源标识符
        /// </param>
        /// <param name="columnHiddenFlag">
        /// 隐藏操作列标志：true隐藏，false显示
        /// </param>
        /// <returns>
        /// 返回字典
        /// </returns>
        public IDictionary<string, object> GetDisplayAttribute(string key, bool columnHiddenFlag)
        {
            string[] keys = key.Split('.');
            if (keys.Length != 3) return null;

            string controller = keys[0];
            string action = keys[1];
            string requestMethod = keys[2];
            return GetDisplayAttribute(action, controller, requestMethod, columnHiddenFlag);
        }

        /// <summary>
        /// 获取显示Attribute
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="requestMethod">
        /// The request method.
        /// </param>
        /// <returns>
        /// 返回字典
        /// </returns>
        public IDictionary<string, object> GetDisplayAttribute(string action, string controller, string requestMethod)
        {
            return GetDisplayAttribute(action, controller, requestMethod, true);
        }

        /// <summary>
        /// 获取显示Attribute
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="requestMethod">
        /// The request method.
        /// </param>
        /// <param name="columnHiddenFlag">
        /// 隐藏操作列标志：true隐藏，false显示
        /// </param>
        /// <returns>
        /// 返回字典
        /// </returns>
        public IDictionary<string, object> GetDisplayAttribute(string action, string controller, string requestMethod, bool columnHiddenFlag)
        {
            var rightCtl = new SystemController();
            var obj = new Dictionary<string, object>();
            var isPermission = rightCtl.CheckRightInfo(action, controller, requestMethod, this._sessionId);
            if (columnHiddenFlag)
            {
                obj["style"] = isPermission ? "visibility:visible;" : "display:none;";
                this._hidden = this._hidden || isPermission;
            }
            else
            {
                obj["style"] = "visibility:" + (isPermission ? "visible" : "hidden") + ";";
            }
            return obj;
        }
    }
}