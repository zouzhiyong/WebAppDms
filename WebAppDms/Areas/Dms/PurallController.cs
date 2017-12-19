using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Dms
{
    public class PurallController : ApiBaseController
    {
        public HttpResponseMessage FindDmsPurallComoditie(string str)
        {
            var list = db.bas_comodities.Where<bas_comodities>(p => p.FullName.Contains(str) || p.ShorName.Contains(str) || p.Code.Contains(str)).Select(s=>s).Take(10).ToList();

            return Json(true, "", list);
        }
    }
}
