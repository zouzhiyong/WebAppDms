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
    public class CustomerController : ApiBaseController
    {
        /// <summary>
        /// 查询条件中销售区域获取
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindBasRegionList()
        {
            var list = db.sys_region.Where<sys_region>(p => p.RegionParentNo == "0").Select(v => new
            {
                RegionNo = v.RegionNo,
                RegionName = v.RegionName,
                value = v.RegionNo,
                label = v.RegionName,
                RegionParentNo = v.RegionParentNo,
                children = db.sys_region.Where<sys_region>(p => p.RegionParentNo == v.RegionNo).Select(v1 => new
                {
                    RegionNo = v1.RegionNo,
                    RegionName = v1.RegionName,
                    value = v1.RegionNo,
                    label = v1.RegionName,
                    RegionParentNo = v1.RegionParentNo,
                    children = db.sys_region.Where<sys_region>(p => p.RegionParentNo == v1.RegionNo).Select(v2 => new
                    {
                        RegionNo = v2.RegionNo,
                        RegionName = v2.RegionName,
                        value = v2.RegionNo,
                        label = v2.RegionName,
                        RegionParentNo = v2.RegionParentNo,
                    }).ToList()
                }).ToList()
            }).ToList();

            return Json(true, "", list);
        }
        public HttpResponseMessage FindBasCustomerTable(dynamic obj)
        {
            DBHelper<view_customer> dbhelp = new DBHelper<view_customer>();

            string Region = obj.Region;
            string CustomerName = obj.CustomerName;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => (x.CustomerName.Contains(CustomerName) && x.Region.Contains(Region)), s => s.CustomerID, true);

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeleteBasCustomerRow(bas_customer obj)
        {
            var result = new DBHelper<bas_customer>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage FindBasCustomerForm(dynamic obj)
        {
            int CustomerID = obj == null ? 0 : obj.CustomerID;


            if (CustomerID == 0)
            {
                var list = new
                {
                    CustomerID = 0,
                    CustomerName = "",
                    Code = "",
                    LinkMan = "",
                    LinkManPhone = "",
                    Region = "",
                    QQ = "",
                    Wechat = "",
                    FAX = "",
                    Phone1 = "",
                    Phone2 = "",
                    Address1 = "",
                    Address2 = "",
                    DepositAmount = "",
                    ModifyDate = DateTime.Now,
                    IsValid = 1,
                    IsValidList = new object[] {
                    new { label = "正常", value = 1 },
                    new { label = "关门", value = 0 }
                }.ToList(),
                    Remark = ""
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.bas_customer.Where(w => w.CustomerID == CustomerID).Select(s => new
                {
                    CustomerID = s.CustomerID,
                    CustomerName = s.CustomerName,
                    Code = s.Code,
                    LinkMan = s.LinkMan,
                    LinkManPhone = s.LinkManPhone,
                    Region = s.Region,
                    QQ = s.QQ,
                    Wechat = s.Wechat,
                    FAX = s.FAX,
                    Phone1 = s.Phone1,
                    Phone2 = s.Phone2,
                    Address1 = s.Address1,
                    Address2 = s.Address2,
                    DepositAmount = s.DepositAmount,
                    ModifyDate = DateTime.Now,
                    IsValid = s.IsValid == null ? 1 : s.IsValid,
                    IsValidList = new object[] {
                    new { label = "正常", value = 1 },
                    new { label = "关门", value = 0 }
                }.ToList(),
                    Remark = s.Remark
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage SaveBasCustomerForm(bas_customer obj)
        {
            DBHelper<bas_customer> dbhelp = new DBHelper<bas_customer>();
            var result = obj.CustomerID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

            return Json(true, result == 1 ? "保存成功！" : "保存失败");
        }
    }
}
