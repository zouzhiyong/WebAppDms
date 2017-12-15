using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebAppDms.Models;

namespace WebAppDms.Areas.Login
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : ApiController
    {
        ////[Authorize]
        //public HttpResponseMessage Login(getLogin gl)
        //{
        //    return Json(gl);
        //}
        public class getLogin
        {
            public string strUser { get; set; }
            public string strPwd { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        public object Login(getLogin loginData)
        {
            if (!ValidateUser(loginData.strUser, loginData.strPwd))
            {
                return new { bRes = false, message = "账号或密码不正确！" };
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, loginData.strUser, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", loginData.strUser, loginData.strPwd),
                            FormsAuthentication.FormsCookiePath);
            webDmsEntities db = new webDmsEntities();
            var UserInfo = db.sys_user.Where(w => w.LoginName == loginData.strUser).FirstOrDefault();

            var homeOjb = new object[] { new { path = "/", iconCls = "fa fa-home", leaf = true, children = new object[] { new { path = "/index", MenuPath = "index", meta = new { name = "主页", button = new string[] { }.ToList() } } } } };

            var list = db.view_menu.Where<view_menu>(p => p.UserID.ToString() == UserInfo.UserID.ToString() && p.MenuParentID == 0).Select(s => new
            {
                path = "/",
                name = "",
                meta = new { name = s.MenuName, button = new string[0] { }.ToList() },
                Xh = s.Xh,
                MenuID = s.MenuID,
                iconCls = s.MenuIcon,
                children = db.view_menu.Where<view_menu>(p1 => p1.MenuParentID == s.MenuID).Select(s1 => new
                {
                    path = "/" + s1.MenuPath,
                    name = s1.MenuName,
                    meta = new { name = s1.MenuName, button = new string[] { "save", "cancle", "new" }.ToList(), isButton = false },
                    MenuPath = s1.MenuPath.Replace("_", "/"),
                    Xh = s1.Xh,
                    MenuID = s1.MenuID
                }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList()
            }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList();

            var tempList = homeOjb.Concat(list).ToList();

            //返回登录结果、用户信息、用户验证票据信息
            var oUser = new UserInfo { bRes = true, UserName = loginData.strUser, Password = loginData.strPwd, user = new { name = UserInfo.RealName, avatar = UserInfo.Avatar }, Ticket = FormsAuthentication.Encrypt(ticket), menu = tempList };
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[loginData.strUser] = oUser;
            return oUser;
        }

        //校验用户名密码（正式环境中应该是数据库校验）
        private bool ValidateUser(string strUser, string strPwd)
        {
            webDmsEntities db = new webDmsEntities();
            var list = db.sys_user.FirstOrDefault(p => p.LoginName == strUser && p.LoginPassword == strPwd);

            if (list != null)
            {
                HttpContext.Current.Session["userId"] = list.UserID;
                return true;
            }
            else
            {
                return false;
            }
        }

        public class UserInfo
        {
            public bool bRes { get; set; }

            public string UserName { get; set; }

            public string Password { get; set; }

            public string Ticket { get; set; }

            public object user { get; set; }
            public object menu { get; set; }
        }
    }
}
