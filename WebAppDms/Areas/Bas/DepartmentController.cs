using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class DepartmentController : ApiBaseController
    {
        public HttpResponseMessage FindBasDepartmentTable(dynamic obj)
        {
            DBHelper<view_department> dbhelp = new DBHelper<view_department>();

            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;
            long CorpID = (long)((t_bas_user)UserSession.Get("UserInfo")).CorpID;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.CorpID == CorpID, s => s.DeptID, true);

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeleteBasDepartmentRow(t_bas_department obj)
        {
            var result = new DBHelper<t_bas_department>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage FindBasDepartmentForm(t_bas_department obj)
        {
            long DeptID = obj.DeptID;

            var ParentCodeList = db.t_bas_department.Where(w1 => w1.ParentCode == "&" && w1.DeptID != DeptID).Select(s1 => new
            {
                label = s1.Name,
                value = s1.Code
            });

            if (DeptID == 0)
            {
                var list = new
                {
                    DeptID = "",
                    Code = "",
                    Name = "",
                    IsValid = 1,
                    ParentCode = "&",
                    ParentCodeList = ParentCodeList,
                    Sequence = "",
                    CorpID = "",
                    Remark = "",
                    Level = "",
                    CreateTime = "",
                    CreateUserID = "",
                    UpdateTime = "",
                    UpdateUserID = ""
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.t_bas_department.Where(w => w.DeptID == DeptID).Select(s => new
                {
                    DeptID = s.DeptID,
                    Code = s.Code,
                    Name = s.Name,
                    IsValid = s.IsValid,
                    ParentCode = s.ParentCode,
                    ParentCodeList = ParentCodeList,
                    Sequence = s.Sequence,
                    CorpID = s.CorpID,
                    Remark = s.Remark,
                    Level = s.Level,
                    CreateTime = s.CreateTime,
                    CreateUserID = s.CreateUserID,
                    UpdateTime = s.UpdateTime,
                    UpdateUserID = s.UpdateUserID
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage SaveBasDepartmentForm(t_bas_department obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_bas_department> dbhelp = new DBHelper<t_bas_department>();
                DateTime dt = DateTime.Now;

                //事务
                var result = 0;
                try
                {
                    if (obj.DeptID == 0)
                    {
                        string Code = "";
                        result = AutoIncrement.AutoIncrementResult("Department", out Code);
                        obj.CreateTime = dt;
                        obj.CreateUserID = (int)((t_bas_user)UserSession.Get("UserInfo")).UserID;
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)((t_bas_user)UserSession.Get("UserInfo")).UserID;
                        obj.CorpID = ((t_bas_user)UserSession.Get("UserInfo")).CorpID;
                        obj.Code = Code;
                    }
                    else
                    {
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)((t_bas_user)UserSession.Get("UserInfo")).UserID;
                    }

                    if(db.t_bas_department.Where(w=>w.Code== obj.Code).ToList().Count() > 0)
                    {
                        throw new Exception("编码重复！");
                    }

                    result = result + (obj.DeptID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj));

                    //提交事务
                    transaction.Complete();
                    return Json(true, "保存成功！");
                }
                catch (Exception ex)
                {
                    return Json(false, "保存失败！" + ex.Message);
                }
            }

        }
    }
}
