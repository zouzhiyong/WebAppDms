using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using WebAppDms.Common.ExceptionHandling;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.ware
{
    public class WareHouseReceiptController : ApiBaseController
    {
        public HttpResponseMessage FindWarehouseList()
        {
            var list = db.t_warehouse.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0).Join(db.t_user_warehouse.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0 && w.UserID == (int)userInfo.UserID),a=>a.WarehouseID,b=>b.WarehouseID,(a,b)=>new 
            {
                value = a.WarehouseID,
                label = a.Name
            });

            return Json(true, "", list);
        }

        public HttpResponseMessage FindWareOrderTable(dynamic obj)
        {
            DBHelper<view_warehouse_receipt> dbhelp = new DBHelper<view_warehouse_receipt>();

            string Code = (obj.Code == null ? "" : obj.Code);
            int BillType = obj.BillType;
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

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.Code.Contains(Code) && x.CorpID == userInfo.CorpID && x.BillType == BillType && (x.Status == Status || Status == null) && x.BillDate >= startDate && x.BillDate <= endDate, s => s.ReceiptID, true);

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeleteWareOrderRow(t_warehouse_receipt obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                try
                {
                    var result = new DBHelper<t_warehouse_receipt>().Remove(obj);
                    result = result + new DBHelper<t_purchase_order_detail>().RemoveList(x => x.POID == obj.ReceiptID);

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

        public HttpResponseMessage FindWareOrderForm(t_warehouse_receipt obj)
        {
            long ReceiptID = obj.ReceiptID;

            DateTime dt = DateTime.Now;

            int CorpID = userInfo.CorpID;

            var WarehouseIDList = db.t_warehouse.Where(w => w.CorpID == CorpID && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.WarehouseID
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



            if (ReceiptID == 0)
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
                    ReceiptQty = NullValue,
                    BalanceQty = NullValue,
                    UnitAmount = NullValue,
                    UnitCost = NullValue,
                    Amount = NullValue,
                    ReturnReasonID = NullValue,
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
                    Remark = NullValue,
                    Status = 0,
                    TruckID = NullValue,
                    TruckIDList = TruckIDList,
                    UpdateTime = NullValue,
                    UpdateUserID = NullValue,
                    BillDate = dt.ToString("yyyy-MM-dd"),
                    BillType = obj.BillType,
                    Code = NullValue,
                    IsReceipted= 0,
                    ConfirmTime = NullValue,
                    ConfirmUserID = NullValue,
                    CorpID = CorpID,
                    CreateTime = NullValue,
                    CreateUserID = userInfo.UserID,
                    CustList = CustList,
                    DriverID = NullValue,
                    DriverIDList = DriverIDList,
                    OrderDetail = OrderDetailList
                };

                return Json(true, "", list);
            }
            else
            {
                var OrderDetail = db.view_warehouse_receipt_detail.Where(w => w.ReceiptID == ReceiptID && w.CorpID == CorpID).Select(x => new
                {
                    CorpID = x.CorpID,
                    ReceiptID = x.ReceiptID,
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
                        UomType = v.UomType,
                        RateQty = v.RateQty,
                        IsPurchaseUOM = v.IsPurchaseUOM,
                        IsSalesUOM = v.IsSalesUOM
                    }),
                    WarehouseID = x.WarehouseID,
                    BinID = x.BinID,
                    BillQty = x.BillQty,
                    ReceiptQty = x.ReceiptQty,
                    BalanceQty = x.BalanceQty,
                    UnitAmount = x.UnitAmount,
                    UnitCost = x.UnitCost,
                    Amount = x.Amount,
                    ReturnReasonID = x.ReturnReasonID,
                    Remark = x.Remark,
                    UpdateTime = x.UpdateTime,
                    UpdateUserID = x.UpdateUserID,
                    WarehouseIDList = WarehouseIDList
                });

                var list = db.t_warehouse_receipt.Where(w => w.ReceiptID == ReceiptID && w.CorpID == CorpID).Select(s => new
                {
                    s.ReceiptID,
                    s.PostDate,
                    s.Remark,
                    s.Status,
                    s.TruckID,
                    TruckIDList = TruckIDList,
                    s.UpdateTime,
                    s.UpdateUserID,
                    s.BillDate,
                    s.BillType,
                    s.Code,
                    s.CorpID,
                    s.CreateTime,
                    s.CreateUserID,
                    CustList = CustList,
                    s.DriverID,
                    DriverIDList = DriverIDList,
                    s.IsReceipted,
                    OrderDetail = OrderDetail
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }
    }
}
