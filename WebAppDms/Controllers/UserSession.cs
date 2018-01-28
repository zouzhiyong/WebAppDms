using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using WebAppDms.Models;

namespace WebAppDms.Controllers
{
    public class UserSession
    {
        public static t_bas_user userInfo = (t_bas_user)(HttpContext.Current.Session["UserInfo"]);

        public static int CompanyRightsID = (int)(HttpContext.Current.Session["CompanyRightsID"]);

        public static long IsSystem = new webDmsEntities().t_sys_rights.Where(w => w.RightsID == userInfo.RightsID).Select(s => s.IsSystem).FirstOrDefault();       
    }

    public class BaseToImg
    {
        public static Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }
    }
}