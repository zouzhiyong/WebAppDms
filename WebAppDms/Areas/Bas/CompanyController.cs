using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class CompanyController : ApiBaseController
    {
        //public HttpResponseMessage FindBasCompanyTable(dynamic obj)
        //{
        //    //DBHelper<t_bas_company> dbhelp = new DBHelper<t_bas_company>();

        //    //int pageSize = obj.pageSize;
        //    //int currentPage = obj.currentPage;
        //    //int total = 0;
        //    //long CorpID = (long)UserSession.userInfo.CorpID;

        //    //var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.CorpID == CorpID, s => s.RightsID, true);

        //    //return Json(list, currentPage, pageSize, total);
        //}
    }
}
