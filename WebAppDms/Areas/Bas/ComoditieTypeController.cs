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
    public class ComoditieTypeController : ApiBaseController
    {
        public HttpResponseMessage FindBasComoditieTypeTree()
        {
            var list = db.bas_comoditiestype.Where<bas_comoditiestype>(p => p.ParentID==0).Select(s => new
            {
                label = s.TypeName,
                TypeID = s.TypeID
            }).ToList();

            return Json(true, "", list);
        }
        public HttpResponseMessage FindBasComoditieTypeTable(dynamic obj)
        {
            DBHelper<view_comoditiestype> dbhelp = new DBHelper<view_comoditiestype>();

            int TypeID = obj.TypeID;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.ParentID == TypeID, s => s.TypeID, true);

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeleteBasComoditieTypeRow(bas_comoditiestype obj)
        {
            var result = new DBHelper<bas_comoditiestype>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage FindBasComoditieTypeForm(dynamic obj)
        {
            int TypeID = obj == null ? 0 : obj.TypeID;

            if (TypeID == 0)
            {
                var list = new
                {
                    TypeID = 0,
                    ParentID = 0,
                    ParentIDList = new object[] { new { label = "未对应上级", value = 0 } }.
                        Concat(db.bas_comoditiestype.Where(w1 => w1.Type != 0 && w1.IsValid != 0).Select(s1 => new
                        {
                            label = s1.TypeName,
                            value = s1.TypeID
                        })),
                    TypeName = "",
                    xh = 0,
                    IsValid = 1,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                }.ToList(),
                    Remark = ""
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.bas_comoditiestype.Where(w => w.TypeID == TypeID).Select(s => new
                {
                    TypeID = s.TypeID,
                    ParentID = s.ParentID,
                    ParentIDList = new object[] { new { label = "未对应上级", value = 0 } }.
                        Concat(db.bas_comoditiestype.Where(w1 => w1.Type != 0 && w1.IsValid!=0).Select(s1 => new
                        {
                            label = s1.TypeName,
                            value = s1.TypeID
                        })),                  
                    TypeName = s.TypeName,
                    xh = s.xh,
                    IsValid = s.IsValid == null ? 1 : s.IsValid,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                }.ToList(),
                    Remark = s.Remark
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage SaveBasComoditieTypeForm(bas_comoditiestype obj)
        {
            DBHelper<bas_comoditiestype> dbhelp = new DBHelper<bas_comoditiestype>();
            var result = obj.TypeID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

            return Json(true, result == 1 ? "保存成功！" : "保存失败");
        }
    }
}
