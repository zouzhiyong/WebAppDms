using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppDms.Models;

namespace WebAppDms.Controllers
{
    public class UserSession
    {
        public static t_bas_user userInfo = (t_bas_user)(HttpContext.Current.Session["UserInfo"]);

        public static int CompanyRightsID = (int)(HttpContext.Current.Session["CompanyRightsID"]);
        
    }
}