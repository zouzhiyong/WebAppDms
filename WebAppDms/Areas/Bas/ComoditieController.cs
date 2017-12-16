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

        [HttpPost]
        public HttpResponseMessage DeleteBasComoditieRow(bas_comodities obj)
        {
            var result = new DBHelper<bas_comodities>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage FindBasComoditieForm(dynamic obj)
        {
            int ComoditiesID = obj == null ? 0 : obj.ComoditiesID;


            if (ComoditiesID == 0)
            {
                var list = new
                {
                    ComoditiesID=0,
                    Code = "",
                    FullName = "",
                    ShorName = "",
                    RecPrice = 0,
                    SalPrice = 0,
                    Photo = "",
                    Remark = "",
                    SpecificationsID = 0,
                    BrandID = 0,
                    TypeID = 0,
                    TypeIDList = new object[] { new { label = "未对应", value = 0 } }.
                        Concat(db.bas_comoditiestype.Where(w1 => w1.ParentID != 0 && w1.ParentID != null).Select(s1 => new
                        {
                            label = s1.TypeName,
                            value = s1.TypeID
                        })),
                    IsValid = 1,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                    }.ToList()
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.bas_comodities.Where(w => w.ComoditiesID == ComoditiesID).Select(s => new
                {
                    ComoditiesID=s.ComoditiesID,
                    Code = s.Code,
                    FullName = s.FullName,
                    ShorName = s.ShorName,
                    RecPrice = s.RecPrice,
                    SalPrice = s.SalPrice,
                    Photo = s.Photo,
                    Remark = s.Remark,
                    SpecificationsID = s.SpecificationsID,
                    BrandID = s.BrandID,
                    TypeID = s.TypeID,
                    TypeIDList = new object[] { new { label = "未对应", value = 0 } }.
                        Concat(db.bas_comoditiestype.Where(w1 => w1.ParentID != 0 && w1.ParentID != null).Select(s1 => new
                        {
                            label = s1.TypeName,
                            value = s1.TypeID
                        })),
                    IsValid = s.IsValid == null ? 1 : s.IsValid,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                }.ToList()
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage SaveBasComoditieForm(bas_comodities obj)
        {
            DBHelper<bas_comodities> dbhelp = new DBHelper<bas_comodities>();
            var result = obj.ComoditiesID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

            return Json(true, result == 1 ? "保存成功！" : "保存失败");
        }
    }
}
