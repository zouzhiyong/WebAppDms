using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
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
        string UploadImgPath = ConfigurationManager.AppSettings["UploadImgPath"].ToString();
        string VirtualPath = ConfigurationManager.AppSettings["VirtualPath"].ToString();
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
            t_bas_user tBasUser = null;

            if (!ValidateUser(loginData.strUser, loginData.strPwd,out tBasUser))
            {
                return new { bRes = false, message = "账号或密码不正确！" };
            }
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, loginData.strUser, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", loginData.strUser, loginData.strPwd),
                            FormsAuthentication.FormsCookiePath);
            webDmsEntities db = new webDmsEntities();

            var homeOjb = new object[] { new { path = "/", iconCls = "fa fa-home", leaf = true, children = new object[] { new { path = "/index", MenuPath = "index", meta = new { name = "主页", button = new string[] { }.ToList() } } } } };

            var list = db.view_menu.Where<view_menu>(p => p.UserID.ToString() == tBasUser.UserID.ToString() && p.ParentCode == "&").Select(s => new
            {
                path = "/",
                name = "",
                meta = new { name = s.Name, button = new string[0] { }.ToList() },
                Xh = s.Sequence,
                MenuID = s.Code,
                iconCls = s.ICON,
                children = db.view_menu.Where<view_menu>(p1 => p1.ParentCode == s.Code).Select(s1 => new
                {
                    path = "/" + s1.URL,
                    name = s1.Name,
                    meta = new { name = s1.Name, button = new string[] { "save", "cancle", "new" }.ToList(), isButton = false },
                    MenuPath = s1.URL.Replace("_", "/"),
                    Xh = s1.Sequence,
                    MenuID = s1.Code
                }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList()
            }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList();

            var tempList = homeOjb.Concat(list).ToList();

            //返回登录结果、用户信息、用户验证票据信息
            string trademark = db.t_sys_company.Where(w => w.CorpID == tBasUser.CorpID).Join(db.t_bas_company,a=>a.CorpID,b=>b.CorpID,(a,b)=>b.TradeMark).FirstOrDefault();
            string TradeMark = "/"+VirtualPath + "/" + UploadImgPath + "/" + trademark; //获取当前项目所在目录       
            var oUser = new UserInfo { bRes = true, UserName = loginData.strUser, Password = loginData.strPwd, user = new { name = tBasUser.Name, avatar = tBasUser.Photo, TradeMark = TradeMark }, Ticket = FormsAuthentication.Encrypt(ticket), menu = tempList };
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[loginData.strUser] = oUser;
            return oUser;
        }

        //校验用户名密码（正式环境中应该是数据库校验）
        private bool ValidateUser(string strUser, string strPwd,out t_bas_user userinfo)
        {
            webDmsEntities db = new webDmsEntities();
            string password = Sha1Encrypt(strPwd);            

            var list = db.t_bas_user.FirstOrDefault(p => p.Code == strUser && p.Password == password);
            userinfo = list;

            if (list != null)
            {
                HttpContext.Current.Session["UserInfo"] = list;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 对字符串SHA1加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string Sha1Encrypt(string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            // 第一种方式
            byte[] byteArray = encoding.GetBytes(source);
            using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
            {
                byteArray = hashAlgorithm.ComputeHash(byteArray);
                StringBuilder stringBuilder = new StringBuilder(256);
                foreach (byte item in byteArray)
                {
                    stringBuilder.AppendFormat("{0:x2}", item);
                }
                hashAlgorithm.Clear();
                return stringBuilder.ToString();
            }

            //// 第二种方式
            //using (SHA1 sha1 = SHA1.Create())
            //{
            //    byte[] hash = sha1.ComputeHash(encoding.GetBytes(source));
            //    StringBuilder stringBuilder = new StringBuilder();
            //    for (int index = 0; index < hash.Length; ++index)
            //        stringBuilder.Append(hash[index].ToString("x2"));
            //    sha1.Clear();
            //    return stringBuilder.ToString();
            //}
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
