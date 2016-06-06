using System;
using System.Web.Mvc;
namespace MyHomeWork
{
    /// <summary>
    /// 表示需要用戶登錄才可以使用的特性
    /// 如果不需要處理用戶登錄，則請指定AllowAnonymousAttribute屬性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class MyAuthAttribute : FilterAttribute, IAuthorizationFilter
    {

        /// <summary>
        /// 默認構造函數
        /// </summary>
        public MyAuthAttribute()
        {
            var authUrl = System.Configuration.ConfigurationManager.AppSettings["AuthUrl"];
            var saveKey = System.Configuration.ConfigurationManager.AppSettings["AuthSaveKey"];
            var saveType = System.Configuration.ConfigurationManager.AppSettings["AuthSaveType"];
            this._authUrl = string.IsNullOrEmpty(authUrl) ? "~/Home/Login" : authUrl;
            this._authSaveKey = string.IsNullOrEmpty(saveKey) ? "LoginedUser" : saveKey;
            this._authSaveType = string.IsNullOrEmpty(saveType) ? "Session" : saveType;
        }
        /// <summary>
        /// 獲取或者設置一個值，該值表示登錄地址
        /// 如果web.config中末定義AuthUrl的值，則默認為：/Test/Login
        /// </summary>
        private string _authUrl;
        public string AuthUrl
        {
            get { return _authUrl.Trim(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用於驗證用戶登錄信息的登錄地址不能為空！");
                }
                else
                {
                    _authUrl = value.Trim();
                }
            }
        }
        /// <summary>
        /// 獲取或者設置一個值，該值表示登錄用來保存登陸信息的鍵名
        /// 如果web.config中末定義AuthSaveKey的值，則默認為LoginedUser
        /// </summary>
        private string _authSaveKey;
        public string AuthSaveKey
        {
            get { return _authSaveKey.Trim(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("用於保存登陸信息的鍵名不能為空！");
                }
                else
                {
                    this._authSaveKey = value.Trim();
                }
            }
        }
        /// <summary>
        /// 獲取或者設置一個值，該值用來保存登錄信息的方式
        /// 如果web.config中末定義AuthSaveType的值，則默認為Session保存
        /// </summary>
        private string _authSaveType;
        public string AuthSaveType
        {
            get { return _authSaveType.Trim().ToUpper(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("保存登陸信息的方式不能為空，只能為【Cookie】或者【Session】！");
                }
                else
                {
                    _authSaveType = value.Trim();
                }
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new Exception("請正常進入網站！");
            }
            switch (AuthSaveType)
            {
                case "SESSION":
                    if (filterContext.HttpContext.Session == null)
                    {
                        throw new Exception("Session不可用！");
                    }
                    if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                    {
                        if (filterContext.HttpContext.Session[_authSaveKey] == null)
                        {
                            filterContext.Result = new RedirectResult(_authUrl);
                        }
                    }
                    break;
                case "COOKIE":
                    if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                    {
                        if (filterContext.HttpContext.Request.Cookies[_authSaveKey] == null)
                        {
                            filterContext.Result = new RedirectResult(_authUrl);
                        }
                    }
                    break;
                default:
                    throw new ArgumentNullException("保存登陸信息的方式不能為空，只能為【Cookie】或者【Session】！");
            }
        }
    }
}