using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
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

            DateTime dt = DateTime.Now;

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

            var CustList = db.t_bas_user.Where(w => w.CorpID == userInfo.CorpID).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            var BillTypeList = new object[] {
                    new { label = "采购订单", value = 0 },
                    new { label = "采购退货单", value = 1 }
                }.ToList();

            var StatusList = new object[] {
                    new { label = "打开", value = 0 },
                    new { label = "保存", value = 1 },
                    new { label = "确定", value = 2 },
                    new { label = "完成", value = 3 }
                }.ToList();

            var IsStockFinishedList = new object[] {
                    new { label = "未完成", value = 0 },
                    new { label = "已完成", value = 1 }
                }.ToList();

            string NullValue = null;

            List<object> OrderDetailList = new List<object>();
            var OrderDetail = new
            {
                CorpID = NullValue,
                POID = NullValue,
                RowID = NullValue,
                ItemID = NullValue,
                UomID = NullValue,
                WarehouseID = NullValue,
                BinID = NullValue,
                BillQty = NullValue,
                OperQty = NullValue,
                BalanceQty = NullValue,
                UnitAmount = NullValue,
                UnitCost = NullValue,
                Amount = NullValue,
                ReturnReasonID = NullValue,
                IsFree = NullValue,
                IsGift = NullValue,
                Remark = NullValue,
                UpdateTime = NullValue
            };
            OrderDetailList.Add(OrderDetail);

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
                    StatusList = StatusList,
                    SupplierID = NullValue,
                    SupplierIDList = SupplierIDList,
                    TruckID = NullValue,
                    TruckIDList = TruckIDList,
                    UpdateTime = NullValue,
                    UpdateUserID = NullValue,
                    BillDate = dt.ToString("yyyy-MM-dd"),
                    BillType = 0,
                    BillTypeList = BillTypeList,
                    Code = NullValue,
                    ConfirmTime = NullValue,
                    ConfirmUserID = NullValue,
                    CorpID = userInfo.CorpID,
                    CreateTime = NullValue,
                    CreateUserID = userInfo.UserID,
                    CustList = CustList,
                    DriverID = NullValue,
                    DriverIDList = DriverIDList,
                    IsStockFinished = 0,
                    IsStockFinishedList = IsStockFinishedList,
                    OrderDetail = OrderDetailList
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
                    StatusList = StatusList,
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
                    CustList = CustList,
                    s.DriverID,
                    DriverIDList = DriverIDList,
                    s.IsStockFinished,
                    IsStockFinishedList = IsStockFinishedList,
                    OrderDetail = db.t_purchase_order_detail.Where(w => w.POID == obj.POID && w.CorpID == s.CorpID).ToList()
                }).FirstOrDefault();



                return Json(true, "", list);
            }
        }

        public HttpResponseMessage FindPurOrderItem(string str)
        {
            var WarehouseIDList = db.t_warehouse.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.WarehouseID
            });

            var list = db.t_item.Where<t_item>(p => p.Name.Contains(str) || p.ShortName.Contains(str) || p.Code.Contains(str)).Select(s => new
            {
                Barcode = s.Barcode,
                Name = s.Name,
                ItemID = s.ItemID,
                Code = s.Code,
                PurchasePrice = s.PurchasePrice,
                CodeTemplate = s.Code + " " + s.Name,
                ShortName = s.ShortName,
                UomID = s.BaseUOM,
                UomIDList = db.view_uom.Where(w => w.CorpID == userInfo.CorpID && w.ItemID == s.ItemID).OrderByDescending(o => o.UomType).Select(s1 => new { value = s1.UomID, label = s1.Name, UomType = s1.UomType, PurchasePrice = s1.PurchasePrice, RateQty = s1.RateQty, IsPurchaseUOM = s1.IsPurchaseUOM }).ToList(),
                WarehouseIDList = WarehouseIDList
            }).Take(10).ToList();

            return Json(true, "", list);
        }

        public HttpResponseMessage SavePurOrderForm(t_purchaseOrder obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DateTime dt = DateTime.Now;

                t_purchase_order objItem = new t_purchase_order()
                {
                    POID = obj.POID,
                    PostDate = obj.PostDate,
                    PurchaserID = obj.PurchaserID,
                    Remark = obj.Remark,
                    Status = obj.Status,
                    SupplierID = obj.SupplierID,
                    TruckID = obj.TruckID,
                    UpdateTime = obj.UpdateTime,
                    UpdateUserID = obj.UpdateUserID,
                    BillDate = obj.BillDate,
                    BillType = obj.BillType,
                    Code = obj.Code,
                    ConfirmTime = obj.ConfirmTime,
                    ConfirmUserID = obj.ConfirmUserID,
                    CorpID = obj.CorpID,
                    CreateTime = obj.CreateTime,
                    CreateUserID = obj.CreateUserID,
                    DriverID = obj.DriverID,
                    IsStockFinished = obj.IsStockFinished
                };

                DBHelper<t_purchase_order> dbhelp_purOrder = new DBHelper<t_purchase_order>();


                //事务
                var result = 0;
                var Item = db.t_purchase_order.Where(w => w.Code == objItem.Code && w.CorpID == userInfo.CorpID);
                try
                {
                    if (objItem.POID == 0)
                    {
                        string Code = objItem.Code;
                        if (Code == "" || Code == null)
                        {
                            result = AutoIncrement.AutoIncrementResult("PurchaseOrder", out Code);
                        }

                        objItem.CreateTime = dt;
                        objItem.BillDate = dt;
                        objItem.CreateUserID = (int)userInfo.UserID;
                        objItem.CorpID = userInfo.CorpID;
                        objItem.Code = Code;
                        objItem.Status = 1;
                        if (Item.ToList().Count() > 0)
                        {
                            throw new Exception("账号重复！");
                        }
                        result = result + dbhelp_purOrder.Add(objItem);
                    }
                    else
                    {
                        objItem.CorpID = userInfo.CorpID;
                        objItem.UpdateTime = dt;
                        objItem.UpdateUserID = (int)userInfo.UserID;
                        if (Item.ToList().Count() > 1)
                        {
                            throw new Exception("账号重复！");
                        }
                        result = result + dbhelp_purOrder.Update(objItem);
                    }

                    //删除并保存明细
                    DBHelper<t_purchase_order_detail> dbhelp_purOderDetail = new DBHelper<t_purchase_order_detail>();
                    foreach (var item in obj.OrderDetail)
                    {
                        item.CorpID = userInfo.CorpID;
                        item.POID = objItem.POID;
                        item.UpdateTime = dt;
                        item.UpdateUserID = (int)userInfo.UserID;
                    }
                    result = result + dbhelp_purOderDetail.RemoveList(w => w.POID == objItem.POID);
                    result = result + dbhelp_purOderDetail.AddList(obj.OrderDetail);

                    //提交事务
                    transaction.Complete();
                    return Json(true, "保存成功！", new { POID = objItem.POID, Status = objItem.Status, BillDate = objItem.BillDate, CreateTime=objItem.CreateTime, OrderDetail = obj.OrderDetail, Code=objItem.Code });
                }
                catch (Exception ex)
                {
                    return Json(false, "保存失败！" + ex.Message);
                }
            }
        }

        public class t_purchaseOrder : t_purchase_order
        {
            public t_purchase_order_detail[] OrderDetail { get; set; }
        }
    }
}
