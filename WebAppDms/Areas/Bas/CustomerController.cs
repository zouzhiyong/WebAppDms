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
                    Photo = "",
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
                    Photo=s.Photo,
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


        public async Task<HttpResponseMessage> uploadPost()
        {
            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            HttpResponseMessage response = null;
            List<string> files = new List<string>();
            try
            {
                // 设置上传目录
                var provider = new MultipartFormDataStreamProvider(HttpContext.Current.Server.MapPath("~/UpLoad"));
                // 接收数据，并保存文件
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }
                response = Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return response;
        }

        [HttpPost]
        public Task<Hashtable> ImgUpload()
        {
            // 检查是否是 multipart/form-data 
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            //文件保存目录路径 
            string SaveTempPath = "~/UpLoad";
            String dirTempPath = HttpContext.Current.Server.MapPath(SaveTempPath);
            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            //var queryp = Request.GetQueryNameValuePairs();//获得查询字符串的键值集合 
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<Hashtable>(o =>
                {
                    Hashtable hash = new Hashtable();
                    hash["error"] = 1;
                    hash["errmsg"] = "上传出错";
                    var file = provider.FileData[0];//provider.FormData 
            string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
            //最大文件大小 
            int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        hash["error"] = 1;
                        hash["errmsg"] = "请选择上传文件。";
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        hash["error"] = 1;
                        hash["errmsg"] = "上传文件大小超过限制。";
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                //定义允许上传的文件扩展名 
                String fileTypes = "gif,jpg,jpeg,png,bmp";
                        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            hash["error"] = 1;
                            hash["errmsg"] = "上传文件扩展名是不允许的扩展名。";
                        }
                        else
                        {
                            String ymd = DateTime.Now.ToString("yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
                            fileinfo.Delete();
                            hash["error"] = 0;
                            hash["errmsg"] = "上传成功";
                            hash["url"] = newFileName + fileExt;
                        }
                    }
                    return hash;
                });
            return task;
        }
    }
}
