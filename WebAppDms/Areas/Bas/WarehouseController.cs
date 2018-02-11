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
    public class WarehouseController : ApiBaseController
    {
        public HttpResponseMessage FindBasWarehouseTable(dynamic obj)
        {
            DBHelper<view_warehouse> dbhelp = new DBHelper<view_warehouse>();

            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;
            long CorpID = (long)userInfo.CorpID;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.CorpID == CorpID, s => s.WarehouseID, true);

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeleteBasWarehouseRow(t_warehouse obj)
        {
            var result = new DBHelper<t_warehouse>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage FindBasWarehouseForm(t_warehouse obj)
        {
            long WarehouseID = obj.WarehouseID;

            var ParentCodeList = db.t_warehouse.Where(w1 => w1.WarehouseID != WarehouseID && w1.CorpID == userInfo.CorpID).Select(s1 => new
            {
                label = s1.Name,
                value = s1.Code
            });

            if (WarehouseID == 0)
            {
                var list = new
                {
                    WarehouseID = 0,
                    CorpID = userInfo.CorpID,
                    Code = "",
                    Name = "",
                    IsValid = 1,
                    Phone = "",
                    Tel = "",
                    IsBin = 0,
                    IsRequireReceive = 0,
                    IsRequireShipment = 0,
                    Contact = "",
                    Address="",
                    CreateTime = "",
                    CreateUserID = "",
                    UpdateTime = "",
                    UpdateUserID = "",
                    CloseTime = "",
                    CloseUser = "",
                    userView = db.view_user.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0).Select(s1 => new
                    {
                        s1.IsValid,
                        s1.UserID,
                        WarehouseID = 0,
                        s1.UserName,
                        s1.RightsName,
                        s1.CorpID,
                        s1.DeptID,
                        s1.DeptName,
                        userFlag = false
                    })
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.t_warehouse.Where(w => w.WarehouseID == WarehouseID && w.CorpID == userInfo.CorpID).Select(s => new
                {
                    WarehouseID = s.WarehouseID,
                    Code = s.Code,
                    CorpID = s.CorpID,
                    Name = s.Name,
                    IsValid = s.IsValid,
                    Phone = s.Phone,
                    Tel = s.Tel,
                    IsBin = s.IsBin,
                    IsRequireReceive = s.IsRequireReceive,
                    IsRequireShipment = s.IsRequireShipment,
                    Contact = s.Contact,
                    Address=s.Address,
                    CreateTime = s.CreateTime,
                    CreateUserID = s.CreateUserID,
                    UpdateTime = s.UpdateTime,
                    UpdateUserID = s.UpdateUserID,
                    CloseTime = s.CloseTime,
                    CloseUser = s.CloseUser,
                    userView = db.view_user.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0).Select(s1 => new
                    {
                        s1.IsValid,
                        s.WarehouseID,
                        s1.UserID,
                        s1.UserName,
                        s1.RightsName,
                        s1.CorpID,
                        s1.DeptID,
                        s1.DeptName,
                        userFlag = db.t_user_warehouse.Where(w => w.CorpID == userInfo.CorpID && w.WarehouseID == s.WarehouseID && w.UserID == s1.UserID && w.IsValid != 0).Count() > 0
                    })
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage SaveBasWarehouseForm(t_warehouse_user obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_warehouse> dbhelp = new DBHelper<t_warehouse>();
                DateTime dt = DateTime.Now;
                
                //事务
                var result = 0;
                var WareHouse = db.t_warehouse.Where(w => w.Code == obj.Code && w.CorpID == userInfo.CorpID);
                try
                {
                    if (obj.WarehouseID == 0)
                    {
                        string Code = "";
                        result = AutoIncrement.AutoIncrementResult("Warehouse", out Code);
                        obj.CreateTime = dt;
                        obj.CreateUserID = (int)userInfo.UserID;
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)userInfo.UserID;
                        obj.CorpID = userInfo.CorpID;
                        obj.Code = Code;

                        if (WareHouse.ToList().Count() > 0)
                        {
                            throw new Exception("编码重复！");
                        }
                        else
                        {
                            if (obj.IsValid == 0)//判断是否修改关闭状态，如果是需要写关闭人
                            {
                                obj.CloseTime = dt;
                                obj.CloseUser = (int)userInfo.UserID;
                            }
                        }
                    }
                    else
                    {
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)userInfo.UserID;

                        if (WareHouse.ToList().Count() > 1)
                        {
                            throw new Exception("编码重复！");
                        }
                        else
                        {
                            if (obj.IsValid == 0 && WareHouse.Select(s => s.IsValid).FirstOrDefault() != 0)//判断是否修改关闭状态，如果是需要写关闭人
                            {
                                obj.CloseTime = dt;
                                obj.CloseUser = (int)userInfo.UserID;
                            }

                            if (obj.IsValid == 1 && WareHouse.Select(s => s.IsValid).FirstOrDefault() == 0)//如果再次打开清空时间和操作员
                            {
                                obj.CloseTime = null;
                                obj.CloseUser = null;
                            }
                        }
                    }

                    t_warehouse tw = new t_warehouse()
                    {
                        WarehouseID = obj.WarehouseID,
                        Code = obj.Code,
                        Address = obj.Address,
                        CorpID = obj.CorpID,
                        Name = obj.Name,
                        IsValid = obj.IsValid,
                        Phone = obj.Phone,
                        Tel = obj.Tel,
                        IsBin = obj.IsBin,
                        IsRequireReceive = obj.IsRequireReceive,
                        IsRequireShipment = obj.IsRequireShipment,
                        Contact = obj.Contact,
                        CreateTime = obj.CreateTime,
                        CreateUserID = obj.CreateUserID,
                        UpdateTime = obj.UpdateTime,
                        UpdateUserID = obj.UpdateUserID,
                        CloseTime = obj.CloseTime,
                        CloseUser = obj.CloseUser
                    };
                    result = result + (obj.WarehouseID == 0 ? dbhelp.Add(tw) : dbhelp.Update(tw));

                    //删除并新增仓库对应用户
                    foreach (var item in obj.userData)
                    {
                        item.WarehouseID = (int)tw.WarehouseID;
                    }
                    result = result + new DBHelper<t_user_warehouse>().RemoveList(w => w.WarehouseID == tw.WarehouseID);
                    result = result + new DBHelper<t_user_warehouse>().AddList(obj.userData);

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

        public class t_warehouse_user : t_warehouse
        {
            public t_user_warehouse[] userData { get; set; }
        }
    }
}
