using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebAppDms.Common.ExceptionHandling;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Pur
{
    public class PurchaserController : ApiBaseController
    {
        public HttpResponseMessage FindPurOrderTable(dynamic obj)
        {
            DBHelper<view_purchase> dbhelp = new DBHelper<view_purchase>();

            string Code = (obj.Code == null ? "" : obj.Code);
            long? Status = obj.Status;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            string[] mJObj = obj.Date.ToObject<string[]>();
            if (mJObj == null)
            {
                throw new Exception("日期不能为空!");
            }

            List<DateTime> Date = new List<DateTime>();
            foreach (var item in mJObj)
            {
                Date.Add(DateTime.ParseExact(item, "yyyy-MM-dd", System.Globalization.CultureInfo.CurrentCulture));
            }
            DateTime startDate = Date[0];
            DateTime endDate = Date[1];
            endDate = DateTime.Parse(endDate.AddDays(1).ToString("yyyy-MM-dd"));
            TimeSpan d3 = endDate.Subtract(startDate);
            if (d3.Days > 31)
            {
                throw new DomainException("只能查询30天数据!");
            }

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.Code.Contains(Code) && x.CorpID == userInfo.CorpID && (x.Status == Status || Status == null) && x.BillDate >= startDate && x.BillDate <= endDate, s => s.POID, true);

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeletePurOrderRow(t_purchase_order obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    var result = new DBHelper<t_purchase_order>().Remove(obj);
                    result = result + new DBHelper<t_purchase_order_detail>().RemoveList(x => x.POID == obj.POID);

                    //提交事务
                    transaction.Complete();
                    return Json(true, "删除成功！");
                }
                catch (Exception er)
                {
                    return Json(false, "删除失败！", er.Message);
                }

            }
        }

        public HttpResponseMessage FindPurOrderForm(t_purchase_order obj)
        {
            long POID = obj.POID;

            DateTime dt = DateTime.Now;

            int CorpID = userInfo.CorpID;

            var WarehouseIDList = db.t_warehouse.Where(w => w.CorpID == CorpID && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.WarehouseID
            });

            var PurchaserIDList = db.t_bas_user.Where(w => w.CorpID == CorpID && w.IsValid != 0 && w.UserCategoryID == 4).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            var SupplierIDList = db.t_supplier.Where(w => w.CorpID == CorpID && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.SupplierID
            });

            var TruckIDList = db.t_bas_truck.Where(w => w.CorpID == CorpID && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.TruckID
            });

            var DriverIDList = db.t_bas_user.Where(w => w.CorpID == CorpID && w.IsValid != 0 && w.UserCategoryID == 7).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            var CustList = db.t_bas_user.Where(w => w.CorpID == CorpID).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });



            if (POID == 0)
            {
                string NullValue = null;

                var OrderDetail = new
                {
                    Code = NullValue,
                    Name = NullValue,
                    CorpID = NullValue,
                    POID = NullValue,
                    RowID = NullValue,
                    ItemID = NullValue,
                    UomID = NullValue,
                    UomIDList = NullValue,
                    WarehouseID = NullValue,
                    WarehouseIDList = NullValue,
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
                    UpdateTime = NullValue,
                    UpdateUserID = NullValue
                };

                List<object> OrderDetailList = new List<object>();
                OrderDetailList.Add(OrderDetail);

                var list = new
                {
                    POID = 0,
                    PostDate = NullValue,
                    PurchaserID = NullValue,
                    PurchaserIDList = PurchaserIDList,
                    Remark = NullValue,
                    Status = 0,
                    SupplierID = NullValue,
                    SupplierIDList = SupplierIDList,
                    TruckID = NullValue,
                    TruckIDList = TruckIDList,
                    UpdateTime = NullValue,
                    UpdateUserID = NullValue,
                    BillDate = dt.ToString("yyyy-MM-dd"),
                    BillType = obj.BillType,
                    Code = NullValue,
                    ConfirmTime = NullValue,
                    ConfirmUserID = NullValue,
                    CorpID = CorpID,
                    CreateTime = NullValue,
                    CreateUserID = userInfo.UserID,
                    CustList = CustList,
                    DriverID = NullValue,
                    DriverIDList = DriverIDList,
                    IsStockFinished = 0,
                    OrderDetail = OrderDetailList
                };

                return Json(true, "", list);
            }
            else
            {
                var OrderDetail = db.view_purchase_detail.Where(w => w.POID == POID && w.CorpID == CorpID).Select(x => new
                {
                    CorpID = x.CorpID,
                    POID = x.POID,
                    RowID = x.RowID,
                    ItemID = x.ItemID,
                    Code = x.Code,
                    Name = x.Name,
                    UomID = x.UomID,
                    UomIDList = db.view_uom.Where(w => w.CorpID == x.CorpID && w.ItemID == x.ItemID).OrderByDescending(o => o.UomType).Select(v => new
                    {
                        value = v.UomID,
                        label = v.Name,
                        UomID = v.UomID,
                        ItemID = v.ItemID,
                        CorpID = v.CorpID,
                        Name = v.Name,
                        PurchasePrice = v.PurchasePrice,
                        SalesPrice = v.SalesPrice,
                        UomType = v.UomType,
                        RateQty = v.RateQty,
                        IsPurchaseUOM = v.IsPurchaseUOM,
                        IsSalesUOM = v.IsSalesUOM
                    }),
                    WarehouseID = x.WarehouseID,
                    BinID = x.BinID,
                    BillQty = x.BillQty,
                    OperQty = x.OperQty,
                    BalanceQty = x.BalanceQty,
                    UnitAmount = x.UnitAmount,
                    UnitCost = x.UnitCost,
                    Amount = x.Amount,
                    ReturnReasonID = x.ReturnReasonID,
                    IsFree = x.IsFree,
                    IsGift = x.IsGift,
                    Remark = x.Remark,
                    UpdateTime = x.UpdateTime,
                    UpdateUserID = x.UpdateUserID,
                    WarehouseIDList = WarehouseIDList
                });

                var list = db.t_purchase_order.Where(w => w.POID == POID && w.CorpID == CorpID).Select(s => new
                {
                    s.POID,
                    s.PostDate,
                    s.PurchaserID,
                    PurchaserIDList = PurchaserIDList,
                    s.Remark,
                    s.Status,
                    s.SupplierID,
                    SupplierIDList = SupplierIDList,
                    s.TruckID,
                    TruckIDList = TruckIDList,
                    s.UpdateTime,
                    s.UpdateUserID,
                    s.BillDate,
                    s.BillType,
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
                    OrderDetail = OrderDetail
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
                UomIDList = db.view_uom.Where(w => w.CorpID == userInfo.CorpID && w.ItemID == s.ItemID).OrderByDescending(o => o.UomType).Select(s1 => new
                {
                    value = s1.UomID,
                    label = s1.Name,
                    UomID = s1.UomID,
                    ItemID = s1.ItemID,
                    CorpID = s1.CorpID,
                    Name = s1.Name,
                    PurchasePrice = s1.PurchasePrice,
                    SalesPrice = s1.SalesPrice,
                    UomType = s1.UomType,
                    RateQty = s1.RateQty,
                    IsPurchaseUOM = s1.IsPurchaseUOM,
                    IsSalesUOM = s1.IsSalesUOM
                }).ToList(),
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
                    return Json(true, "保存成功！", new { POID = objItem.POID });
                }
                catch (Exception ex)
                {
                    return Json(false, "保存失败！" + ex.Message);
                }
            }
        }

        public HttpResponseMessage AuditPurOrderForm(t_purchase_order obj)
        {
            obj.Status = 2;
            DBHelper<t_purchase_order> dbhelp = new DBHelper<t_purchase_order>();
            List<string> fileds = new List<string>();
            fileds.Add("Status");
            var result = dbhelp.UpdateEntityFields(obj, fileds);

            if (result > 0)
            {
                return Json(true, "审核成功！", new { POID = obj.POID });
            }
            else
            {
                throw new DomainException("审核失败！");
            }
        }

        public class t_purchaseOrder : t_purchase_order
        {
            public t_purchase_order_detail[] OrderDetail { get; set; }
        }

    }
}
