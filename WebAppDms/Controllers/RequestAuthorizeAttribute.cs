using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using WebAppDms.Models;

namespace WebAppDms.Controllers
{
    public class RequestAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                if (!ValidateController(actionContext))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "您没有访问权限", direct = "" });
                }
                else
                {
                    //解密用户ticket,并校验用户名密码是否匹配
                    var encryptTicket = authorization.Parameter;
                    if (ValidateTicket(encryptTicket))
                    {
                        base.IsAuthorized(actionContext);
                    }
                    else
                    {
                        HandleUnauthorizedRequest(actionContext);
                    }
                }                
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { result = false, message = "该操作需要用户登录", direct = "login.html" });
            }
        }

        //校验用户名密码（正式环境中应该是数据库校验）
        private bool ValidateTicket(string encryptTicket)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;

            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string strUser = strTicket.Substring(0, index);
            string strPwd = strTicket.Substring(index + 1);
            if (HttpContext.Current.Session[strUser] == null)
            {
                return false;
            }
            //string _sessionUser = HttpContext.Current.Session[strUser].ToString();

            Areas.Login.LoginController.UserInfo sessionUser = (Areas.Login.LoginController.UserInfo)(HttpContext.Current.Session[strUser]);
            if (strUser == sessionUser.UserName && strPwd == sessionUser.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateController(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var actionName = actionContext.ActionDescriptor.ActionName;
            var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            webDmsEntities db = new webDmsEntities();
            var count = db.view_menu.Where(w => w.ControllerName.ToString().ToLower() == controllerName.ToLower() && w.UserID == UserSession.userInfo.UserID).Count();

            return count > 0 ? true : false;
        }       
    }
}