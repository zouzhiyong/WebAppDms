using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class CustomerController : ApiBaseController
    {
        string VirtualPath = ConfigurationManager.AppSettings["VirtualPath"].ToString();
        string UploadImgPath = ConfigurationManager.AppSettings["UploadImgPath"].ToString();

        public class Region : t_bas_region
        {
            public List<Region> children { get; set; }
        }
        public void LoopToAppendChildren(List<Region> all, Region curItem)
        {
            var subItems = all.Where(ee => ee.ParentCode == curItem.Code && ee.CorpID == userInfo.CorpID && ee.IsValid != 0).ToList();
            if (subItems.Count > 0)
            {
                curItem.children = new List<Region>();
                curItem.children.AddRange(subItems);
                foreach (var subItem in subItems)
                {
                    LoopToAppendChildren(all, subItem);//新闻1.1
                }
            }
        }

        public Region getRegtion()
        {
            var Regionlist = new List<Region>();
            var tempList = db.t_bas_region.Where<t_bas_region>(p => p.CorpID == userInfo.CorpID && p.IsValid != 0).ToList();
            foreach (var item in tempList)
            {
                var temObj = new Region();
                temObj.Code = item.Code;
                temObj.RegionID = item.RegionID;
                temObj.CorpID = item.CorpID;
                temObj.IsValid = item.IsValid;
                temObj.Name = item.Name;
                temObj.ParentCode = item.ParentCode;
                temObj.Sequence = item.Sequence;
                Regionlist.Add(temObj);
            }

            Region rootRoot = new Region();
            rootRoot.Code = "&";
            rootRoot.ParentCode = "";
            rootRoot.RegionID = 0;
            rootRoot.Name = "全部";

            LoopToAppendChildren(Regionlist, rootRoot);

            return rootRoot;
        }

        /// <summary>
        /// 查询条件中销售区域获取
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindBasRegionList()
        {
            //var Regionlist = new List<Region>();
            //var tempList = db.t_bas_region.Where<t_bas_region>(p => p.CorpID == userInfo.CorpID && p.IsValid != 0).ToList();
            //foreach (var item in tempList)
            //{
            //    var temObj = new Region();
            //    temObj.Code = item.Code;
            //    temObj.RegionID = item.RegionID;
            //    temObj.CorpID = item.CorpID;
            //    temObj.IsValid = item.IsValid;
            //    temObj.Name = item.Name;
            //    temObj.ParentCode = item.ParentCode;
            //    temObj.Sequence = item.Sequence;
            //    Regionlist.Add(temObj);
            //}

            //Region rootRoot = new Region();
            //rootRoot.Code = "&";
            //rootRoot.ParentCode = "";
            //rootRoot.RegionID = 0;
            //rootRoot.Name = "全部";

            //LoopToAppendChildren(Regionlist, rootRoot);
            return Json(true, "", getRegtion());
        }

        public HttpResponseMessage FindBasCustomerTable(dynamic obj)
        {
            DBHelper<view_customer> dbhelp = new DBHelper<view_customer>();

            long Region = obj.Region;
            string Name = obj.Name;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => (x.Name.Contains(Name) && (Region == 0 || x.RegionID == Region) && x.CorpID == userInfo.CorpID), s => s.CustID, true);

            //foreach (var item in list)
            //{
            //    string ResultName = "";
            //    List<long> ResultID = new List<long>();
            //    List<long> regionIDList = new List<long>();
            //    ReginName(item.regionName, regionIDList, item.ParentCode, db.t_bas_region.Where(w => w.IsValid != 0 && w.CorpID == userInfo.CorpID), out ResultName, out ResultID);
            //    item.regionName = ResultName;
            //}

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeleteBasCustomerRow(t_customer obj)
        {
            var result = new DBHelper<t_customer>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage FindBasCustomerForm(t_customer obj)
        {
            long CustID = obj.CustID;

            var CustCategoryIDList = db.view_datadict.Where(w => (w.CorpID == userInfo.CorpID || w.CorpID == 0) && w.Code == "CustomerCategory").Select(s => new
            {
                label = s.Name,
                value = s.DClassID
            });

            var EmployeeIDList = db.t_bas_user.Where(w => w.IsValid != 0 && w.CorpID == userInfo.CorpID && w.UserCategoryID == 5).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            var RegionIDList = getRegtion().children;

            List<long> ResultID = new List<long>();

            if (CustID == 0)
            {
                var list = new
                {
                    CustID = 0,
                    CorpID = userInfo.CorpID,
                    Address = "",
                    Name = "",
                    HelperCode = "",
                    AreaID = "",
                    City = "",
                    CloseTime = "",
                    CloseUserID = "",
                    Code = "",
                    Contact = "",
                    CreateTime = "",
                    CreateUserID = "",
                    CustCategoryID = 0,
                    CustCategoryIDList = CustCategoryIDList,
                    EmployeeID = 0,
                    EmployeeIDList = EmployeeIDList,
                    Fax = "",
                    IsValid = 1,
                    Phone = "",
                    Photo = "",
                    PostCode = "",
                    RegionID = 0,
                    ShortName = "",
                    Tel = "",
                    UpdateTime = "",
                    UpdateUserID = "",
                };

                return Json(true, "", new { list = list, RegionIDList = RegionIDList, RegionIDArr = ResultID });
            }
            else
            {
                var list = db.t_customer.Where(w => w.CustID == CustID && w.CorpID == userInfo.CorpID).Select(s => new
                {
                    s.CorpID,
                    s.Code,
                    s.CustID,
                    s.Address,
                    s.Name,
                    s.HelperCode,
                    s.AreaID,
                    s.City,
                    s.CloseTime,
                    s.CloseUserID,
                    s.Contact,
                    s.CreateTime,
                    s.CreateUserID,
                    s.CustCategoryID,
                    CustCategoryIDList = CustCategoryIDList,
                    s.EmployeeID,
                    EmployeeIDList = EmployeeIDList,
                    s.Fax,
                    s.IsValid,
                    s.Phone,
                    s.Photo,
                    s.PostCode,
                    s.RegionID,
                    s.ShortName,
                    s.Tel,
                    s.UpdateTime,
                    s.UpdateUserID
                }).FirstOrDefault();

                var tBasRegion = db.t_bas_region.Where(w => w.IsValid != 0 && w.CorpID == userInfo.CorpID && w.RegionID == list.RegionID).FirstOrDefault();

                if (tBasRegion != null)
                {
                    foreach (var item in tBasRegion.RegionAllID.Split(','))
                    {
                        ResultID.Add(Convert.ToInt16(item));
                    }
                }                

                return Json(true, "", new { list = list, RegionIDList = RegionIDList, RegionIDArr = ResultID });
            }
        }

        public HttpResponseMessage SaveBasCustomerForm(t_customer obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_customer> dbhelp = new DBHelper<t_customer>();
                DateTime dt = DateTime.Now;

                //事务
                var result = 0;
                var Customer = db.t_customer.Where(w => w.Code == obj.Code && w.CorpID==userInfo.CorpID);
                try
                {
                    string base64Data = obj.Photo;
                    if (obj.CustID == 0)
                    {
                        string Code = obj.Code;
                        if(Code == "")
                        {
                            result = AutoIncrement.AutoIncrementResult("Customer", out Code);
                        }
                                                
                        obj.Photo = "";
                        obj.CreateTime = dt;
                        obj.CreateUserID = (int)userInfo.UserID;
                        obj.CorpID = userInfo.CorpID;
                        obj.Code = Code;
                        if (Customer.ToList().Count() > 0)
                        {
                            throw new Exception("账号重复！");
                        }
                        result = result + dbhelp.Add(obj);
                    }
                    else
                    {
                        obj.Photo = "";
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)userInfo.UserID;
                        if (Customer.ToList().Count() > 1)
                        {
                            throw new Exception("账号重复！");
                        }
                        result = result + dbhelp.Update(obj);
                    }

                    //保存图片并修改数据库图片名称 
                    try
                    {
                        //获取文件储存路径            
                        string suffix = base64Data.Split(new char[] { ';' })[0].Substring(base64Data.IndexOf('/') + 1);//获取后缀名
                        string newFileName = "CUST_" + obj.CustID.ToString("000000000") + "." + suffix;
                        string strPath = HttpContext.Current.Server.MapPath("~/" + UploadImgPath + "/" + newFileName); //获取当前项目所在目录 
                        //获取图片并保存
                        BaseToImg.Base64ToImg(base64Data.Split(',')[1]).Save(strPath);
                        obj.Photo = newFileName;
                    }
                    catch
                    {
                        obj.Photo = base64Data;
                    }
                    List<string> fileds = new List<string>();
                    fileds.Add("Photo");
                    result = result + dbhelp.UpdateEntityFields(obj, fileds);

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

        private void ReginName(string regionName, List<long> regionIDList, string ParentCode, IQueryable<t_bas_region> tBasRegion, out string ResultName, out List<long> ResultID)
        {
            var regionTable = tBasRegion.Where(w => w.Code == ParentCode);
            ResultName = regionName;
            ResultID = regionIDList;
            if (regionTable.Count() > 0)
            {
                regionName = regionTable.Select(s => s.Name).FirstOrDefault() + "/" + regionName;
                ResultName = regionName;

                regionIDList.Insert(0, regionTable.Select(s => s.RegionID).FirstOrDefault());
                ResultID = regionIDList;
                ReginName(regionName, regionIDList, regionTable.Select(s => s.ParentCode).FirstOrDefault(), tBasRegion, out ResultName, out ResultID);
            }
        }
    }
}
