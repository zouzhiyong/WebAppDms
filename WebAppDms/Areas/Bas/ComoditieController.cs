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
    public class ComoditieController : ApiBaseController
    {
        public HttpResponseMessage FindBasComoditieTree()
        {
            var list = db.bas_comoditiestype.Where<bas_comoditiestype>(p => p.ParentID == 0).Select(s => new
            {
                label = s.TypeName,
                xh = s.xh,
                TypeID = s.TypeID,
                children = db.bas_comoditiestype.Where<bas_comoditiestype>(p1 => p1.ParentID == s.TypeID).Select(s1 => new
                {
                    label = s1.TypeName,
                    xh = s1.xh,
                    TypeID = s1.TypeID,
                }).OrderBy(o => o.xh).ThenBy(o => o.TypeID).ToList()
            }).OrderBy(o => o.xh).ThenBy(o => o.TypeID).ToList();

            return Json(true, "", list);
        }

        public HttpResponseMessage FindBasComoditieTable(dynamic obj)
        {
            DBHelper<bas_comodities> dbhelp = new DBHelper<bas_comodities>();

            int TypeID = obj.TypeID;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.TypeID == TypeID, s => s.TypeID, true);

            return Json(list, currentPage, pageSize, total);
        }
    }
}
