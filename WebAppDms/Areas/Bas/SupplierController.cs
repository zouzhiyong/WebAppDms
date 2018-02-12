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
    public class SupplierController : ApiBaseController
    {
        public HttpResponseMessage FindBasSupplierTable(dynamic obj)
        {
            DBHelper<view_supplier> dbhelp = new DBHelper<view_supplier>();

            string Name = obj.Name;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;
            long CorpID = (long)userInfo.CorpID;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.Name.Contains(Name) && x.CorpID == CorpID, s => s.SupplierID, true);

            return Json(list, currentPage, pageSize, total);
        }

        public HttpResponseMessage FindBasSupplierForm(t_supplier obj)
        {
            long SupplierID = obj.SupplierID;

            var SupplierCategoryIDList = db.view_datadict.Where(w => (w.CorpID == userInfo.CorpID || w.CorpID == 0) && w.Code == "SupplierCategory").Select(s => new
            {
                label = s.Name,
                value = s.DClassID
            });

            var EmployeeIDList = db.t_bas_user.Where(w => w.IsValid != 0 && w.CorpID == userInfo.CorpID && w.UserCategoryID == 4).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            if (SupplierID == 0)
            {
                var list = new
                {
                    CorpID= userInfo.CorpID,
                    Code ="",
                    SupplierID=0,
                    Address="",
                    Name="",
                    HelperCode="",
                    City="",
                    CloseTime = "",
                    CloseUserID="",
                    Contact = "",
                    CreateTime = "",
                    CreateUserID="",
                    SupplierCategoryID=0,
                    SupplierCategoryIDList = SupplierCategoryIDList,
                    EmployeeID=0,
                    EmployeeIDList = EmployeeIDList,
                    Fax = "",
                    IsValid = 1,
                    Phone = "",
                    PostCode = "",
                    ShortName = "",
                    Tel = "",
                    UpdateTime = "",
                    UpdateUserID=""
                };

                return Json(true, "", list);
            }
            else
            {
                var list = db.t_supplier.Where(w => w.SupplierID == SupplierID && w.CorpID == userInfo.CorpID).Select(s => new
                {
                    s.CorpID,
                    s.Code,
                    s.SupplierID,
                    s.Address,
                    s.Name,
                    s.HelperCode,
                    s.City,
                    s.CloseTime,
                    s.CloseUserID,
                    s.Contact,
                    s.CreateTime,
                    s.CreateUserID,
                    s.SupplierCategoryID,
                    SupplierCategoryIDList= SupplierCategoryIDList,
                    s.EmployeeID,
                    EmployeeIDList = EmployeeIDList,
                    s.Fax,
                    s.IsValid,
                    s.Phone,                    
                    s.PostCode,                    
                    s.ShortName,
                    s.Tel,
                    s.UpdateTime,
                    s.UpdateUserID
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteBasSupplierRow(t_supplier obj)
        {
            var result = new DBHelper<t_supplier>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage SaveBasSupplierForm(t_supplier obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_supplier> dbhelp = new DBHelper<t_supplier>();
                DateTime dt = DateTime.Now;

                //事务
                var result = 0;
                var Supplier = db.t_supplier.Where(w => w.Code == obj.Code && w.CorpID == userInfo.CorpID);
                try
                {
                    if (obj.SupplierID == 0)
                    {
                        string Code = "";
                        result = AutoIncrement.AutoIncrementResult("Supplier", out Code);
                        obj.CreateTime = dt;
                        obj.CreateUserID = (int)userInfo.UserID;
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)userInfo.UserID;
                        obj.CorpID = userInfo.CorpID;
                        obj.Code = Code;

                        if (Supplier.ToList().Count() > 0)
                        {
                            throw new Exception("编码重复！");
                        }
                        else
                        {
                            if (obj.IsValid == 0)//判断是否修改关闭状态，如果是需要写关闭人
                            {
                                obj.CloseTime = dt;
                                obj.CloseUserID = (int)userInfo.UserID;
                            }
                        }
                    }
                    else
                    {
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)userInfo.UserID;

                        if (Supplier.ToList().Count() > 1)
                        {
                            throw new Exception("编码重复！");
                        }
                        else
                        {
                            if (obj.IsValid == 0 && Supplier.Select(s => s.IsValid).FirstOrDefault() != 0)//判断是否修改关闭状态，如果是需要写关闭人
                            {
                                obj.CloseTime = dt;
                                obj.UpdateUserID = (int)userInfo.UserID;
                            }

                            if (obj.IsValid != 0 && Supplier.Select(s => s.IsValid).FirstOrDefault() == 0)//如果再次打开清空时间和操作员
                            {
                                obj.CloseTime = null;
                                obj.UpdateUserID = null;
                            }
                        }
                    }

                    result = result + (obj.SupplierID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj));

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
