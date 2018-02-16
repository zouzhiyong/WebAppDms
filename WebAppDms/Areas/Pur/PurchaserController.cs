using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Pur
{
    public class PurchaserController : ApiBaseController
    {
        public HttpResponseMessage FindPurOrderForm(t_purchase_order obj)
        {
            long POID = obj.POID;

            var PurchaserIDList = db.t_bas_user.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0 && w.UserCategoryID == 4).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            var SupplierIDList = db.t_supplier.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.SupplierID
            });

            var TruckIDList = db.t_bas_truck.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.TruckID
            });

            var DriverIDList = db.t_bas_user.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0 && w.UserCategoryID == 7).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            var BillTypeList = new object[] {
                    new { label = "采购订单", value = 0 },
                    new { label = "采购退货单", value = 1 }
                }.ToList();

            var StatusList= new object[] {
                    new { label = "打开", value = 0 },
                    new { label = "保存", value = 1 },
                    new { label = "确定", value = 2 },
                    new { label = "完成", value = 3 }
                }.ToList();

            var IsStockFinishedList= new object[] {
                    new { label = "未完成", value = 0 },
                    new { label = "已完成", value = 1 }
                }.ToList();

            string NullValue = null;

            if (POID == 0)
            {
                var list = new
                {
                    POID = 0,
                    PostDate = NullValue,
                    PurchaserID = NullValue,
                    PurchaserIDList = PurchaserIDList,
                    Remark = NullValue,
                    Status = 0,
                    StatusList= StatusList,
                    SupplierID = NullValue,
                    SupplierIDList = SupplierIDList,
                    TruckID = NullValue,
                    TruckIDList = TruckIDList,
                    UpdateTime = NullValue,
                    UpdateUserID = NullValue,
                    BillDate = NullValue,
                    BillType = 0,
                    BillTypeList = BillTypeList,
                    Code = NullValue,
                    ConfirmTime = NullValue,
                    ConfirmUserID = NullValue,
                    CorpID = NullValue,
                    CreateTime = NullValue,
                    CreateUserID = NullValue,
                    DriverID = NullValue,
                    DriverIDList = DriverIDList,
                    IsStockFinished = 0,
                    IsStockFinishedList= IsStockFinishedList
                };

                return Json(true, "", list);
            }
            else
            {
                var list = db.t_purchase_order.Where(w => w.POID == POID && w.CorpID == userInfo.CorpID).Select(s => new
                {
                    s.POID,
                    s.PostDate,
                    s.PurchaserID,
                    PurchaserIDList = PurchaserIDList,
                    s.Remark,
                    s.Status,
                    StatusList= StatusList,
                    s.SupplierID,
                    SupplierIDList = SupplierIDList,
                    s.TruckID,
                    TruckIDList = TruckIDList,
                    s.UpdateTime,
                    s.UpdateUserID,
                    s.BillDate,
                    s.BillType,
                    BillTypeList = BillTypeList,
                    s.Code,
                    s.ConfirmTime,
                    s.ConfirmUserID,
                    s.CorpID,
                    s.CreateTime,
                    s.CreateUserID,
                    s.DriverID,
                    DriverIDList = DriverIDList,
                    s.IsStockFinished,
                    IsStockFinishedList = IsStockFinishedList
                }).FirstOrDefault();



                return Json(true, "", list);
            }
        }
    }
}
