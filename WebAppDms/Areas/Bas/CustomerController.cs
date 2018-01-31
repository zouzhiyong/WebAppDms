using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class CustomerController : ApiBaseController
    {
        public class Region: t_bas_region
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
        
        /// <summary>
        /// 查询条件中销售区域获取
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindBasRegionList()
        {
            var Regionlist = new List<Region>();
            var tempList= db.t_bas_region.Where<t_bas_region>(p => p.CorpID == userInfo.CorpID && p.IsValid != 0).ToList();
            foreach(var item in tempList)
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


            //string Code = "&";

            //var tempList = db.t_bas_region.Where<t_bas_region>(p => p.CorpID == userInfo.CorpID && p.IsValid != 0);
            //var list = tempList.Where(p=> p.ParentCode == Code).Select(s => new
            //{
            //    s.RegionID,
            //    Code = s.Code,
            //    s.ParentCode,
            //    s.Name,
            //    children = tempList.Where(p => p.ParentCode == s.Code).Select(s1 => new
            //    {
            //        s1.RegionID,
            //        Code = s1.Code,
            //        s1.ParentCode,
            //        s1.Name,
            //        children = tempList.Where(p => p.ParentCode == s1.Code).Select(s2 => new
            //        {
            //            s2.RegionID,
            //            Code = s2.Code,
            //            s2.ParentCode,
            //            s2.Name,
            //            children = tempList.Where(p => p.ParentCode == s2.Code).Select(s3 => new
            //            {
            //                s3.RegionID,
            //                Code = s3.Code,
            //                s3.ParentCode,
            //                s3.Name,
            //                children = tempList.Where(p => p.ParentCode == s3.Code).ToList()
            //            }).ToList()
            //        }).ToList()
            //    }).ToList()
            //}).ToList();

            return Json(true, "", rootRoot);
        }
        //public HttpResponseMessage FindBasCustomerTable(dynamic obj)
        //{
        //    DBHelper<view_customer> dbhelp = new DBHelper<view_customer>();

        //    string Region = obj.Region;
        //    string CustomerName = obj.CustomerName;
        //    int pageSize = obj.pageSize;
        //    int currentPage = obj.currentPage;
        //    int total = 0;

        //    var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => (x.CustomerName.Contains(CustomerName) && x.Region.Contains(Region)), s => s.CustomerID, true);

        //    return Json(list, currentPage, pageSize, total);
        //}

        //[HttpPost]
        //public HttpResponseMessage DeleteBasCustomerRow(bas_customer obj)
        //{
        //    var result = new DBHelper<bas_customer>().Remove(obj);

        //    return Json(true, result == 1 ? "删除成功！" : "删除失败");
        //}

        //public HttpResponseMessage FindBasCustomerForm(dynamic obj)
        //{
        //    int CustomerID = obj == null ? 0 : obj.CustomerID;


        //    if (CustomerID == 0)
        //    {
        //        var list = new
        //        {
        //            CustomerID = 0,
        //            CustomerName = "",
        //            Code = "",
        //            LinkMan = "",
        //            LinkManPhone = "",
        //            Region = "",
        //            QQ = "",
        //            Wechat = "",
        //            FAX = "",
        //            Phone1 = "",
        //            Phone2 = "",
        //            Address1 = "",
        //            Address2 = "",
        //            DepositAmount = "",
        //            Photo = "",
        //            ModifyDate = DateTime.Now,
        //            IsValid = 1,
        //            IsValidList = new object[] {
        //            new { label = "正常", value = 1 },
        //            new { label = "关门", value = 0 }
        //        }.ToList(),
        //            Remark = ""
        //        };
        //        return Json(true, "", list);
        //    }
        //    else
        //    {
        //        var list = db.bas_customer.Where(w => w.CustomerID == CustomerID).Select(s => new
        //        {
        //            CustomerID = s.CustomerID,
        //            CustomerName = s.CustomerName,
        //            Code = s.Code,
        //            LinkMan = s.LinkMan,
        //            LinkManPhone = s.LinkManPhone,
        //            Region = s.Region,
        //            QQ = s.QQ,
        //            Wechat = s.Wechat,
        //            FAX = s.FAX,
        //            Phone1 = s.Phone1,
        //            Phone2 = s.Phone2,
        //            Address1 = s.Address1,
        //            Address2 = s.Address2,
        //            DepositAmount = s.DepositAmount,
        //            Photo=s.Photo,
        //            ModifyDate = DateTime.Now,
        //            IsValid = s.IsValid == null ? 1 : s.IsValid,
        //            IsValidList = new object[] {
        //            new { label = "正常", value = 1 },
        //            new { label = "关门", value = 0 }
        //        }.ToList(),
        //            Remark = s.Remark
        //        }).FirstOrDefault();

        //        return Json(true, "", list);
        //    }
        //}

        //public HttpResponseMessage SaveBasCustomerForm(bas_customer obj)
        //{
        //    DBHelper<bas_customer> dbhelp = new DBHelper<bas_customer>();
        //    var result = obj.CustomerID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

        //    return Json(true, result == 1 ? "保存成功！" : "保存失败");
        //}


        //public async Task<HttpResponseMessage> uploadPost()
        //{
        //    // 检查是否是 multipart/form-data
        //    if (!Request.Content.IsMimeMultipartContent("form-data"))
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    HttpResponseMessage response = null;
        //    List<string> files = new List<string>();
        //    try
        //    {
        //        // 设置上传目录
        //        var provider = new MultipartFormDataStreamProvider(HttpContext.Current.Server.MapPath("~/UpLoad"));
        //        // 接收数据，并保存文件
        //        await Request.Content.ReadAsMultipartAsync(provider);
        //        foreach (MultipartFileData file in provider.FileData)
        //        {
        //            files.Add(Path.GetFileName(file.LocalFileName));
        //        }
        //        response = Request.CreateResponse(HttpStatusCode.OK, files);
        //    }
        //    catch
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }
        //    return response;
        //}

        //[HttpPost]
        //public Task<Hashtable> ImgUpload()
        //{
        //    // 检查是否是 multipart/form-data 
        //    if (!Request.Content.IsMimeMultipartContent("form-data"))
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    //文件保存目录路径 
        //    string SaveTempPath = "~/UpLoad";
        //    String dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);
        //    // 设置上传目录 
        //    var provider = new MultipartFormDataStreamProvider(dirTempPath);
        //    //var queryp = Request.GetQueryNameValuePairs();//获得查询字符串的键值集合 
        //    var task = Request.Content.ReadAsMultipartAsync(provider).
        //        ContinueWith<Hashtable>(o =>
        //        {
        //            Hashtable hash = new Hashtable();
        //            hash["error"] = 1;
        //            hash["errmsg"] = "上传出错";
        //            var file = provider.FileData[0];//provider.FormData 
        //    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
        //            FileInfo fileinfo = new FileInfo(file.LocalFileName);
        //    //最大文件大小 
        //    int maxSize = 500000;
        //            if (fileinfo.Length <= 0)
        //            {
        //                hash["error"] = 1;
        //                hash["errmsg"] = "请选择上传文件。";
        //            }
        //            else if (fileinfo.Length > maxSize)
        //            {
        //                hash["error"] = 1;
        //                hash["errmsg"] = "上传文件大小超过限制。";
        //            }
        //            else
        //            {
        //                string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
        //        //定义允许上传的文件扩展名 
        //        String fileTypes = "gif,jpg,jpeg,png,bmp";
        //                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
        //                {
        //                    hash["error"] = 1;
        //                    hash["errmsg"] = "上传文件扩展名是不允许的扩展名。";
        //                }
        //                else
        //                {
        //                    String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        //                    String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        //                    fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
        //                    fileinfo.Delete();
        //                    hash["error"] = 0;
        //                    hash["errmsg"] = "上传成功";
        //                    hash["url"] = newFileName + fileExt;
        //                }
        //            }
        //            return hash;
        //        });
        //    return task;
        //}
    }
}
