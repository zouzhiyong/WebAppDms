using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class CompanyController : ApiBaseController
    {
        string VirtualPath = ConfigurationManager.AppSettings["VirtualPath"].ToString();
        string UploadImgPath = ConfigurationManager.AppSettings["UploadImgPath"].ToString();

        public HttpResponseMessage FindBasCompanyForm()
        {
            int CorpID = UserSession.userInfo.CorpID;
            var list = db.t_bas_company.Where(w => w.CorpID == CorpID).Select(s => new
            {
                CorpID = s.CorpID,
                Code = s.Code,
                Name = s.Name,
                //IsValid = s.IsValid,
                Address = s.Address,
                Address2 = s.Address2,
                Contact = s.Contact,
                Tel = s.Tel,
                Phone = s.Phone,
                Fax = s.Fax,
                TradeMark = "/" + VirtualPath + "/" + UploadImgPath + "/" + s.TradeMark,
                InvAccountPeriod = s.InvAccountPeriod,
                Remark = s.Remark,
                CreateTime = s.CreateTime,
                CreateUserID = s.CreateUserID,
                UpdateTime = s.UpdateTime,
                UpdateUserID = s.UpdateUserID
            }).FirstOrDefault();

            return Json(true, "", list);
        }

        public HttpResponseMessage SaveBasCompanyForm(t_bas_company obj)
        {
            try
            {
                DBHelper<t_bas_company> dbhelp = new DBHelper<t_bas_company>();

                DateTime dt = DateTime.Now;

                string base64Data = obj.TradeMark;
                //获取文件储存路径            
                string suffix = base64Data.Split(new char[] { ';' })[0].Substring(base64Data.IndexOf('/') + 1);//获取后缀名
                                                                                                               //string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo)+ "."+suffix;
                string newFileName = "COMPANY_" + UserSession.userInfo.CorpID.ToString("000000000") + "." + suffix;
                string strPath = HttpContext.Current.Server.MapPath("~/" + UploadImgPath + "/" + newFileName); //获取当前项目所在目录           

                //获取图片并保存
                Base64ToImg(base64Data.Split(',')[1]).Save(strPath);

                obj.UpdateTime = dt;
                obj.UpdateUserID = (int)UserSession.userInfo.UserID;
                obj.TradeMark = newFileName;

                var result = dbhelp.Update(obj);

                return Json(true, "保存成功！");
            }
            catch (Exception ex)
            {
                return Json(false, "保存失败!" + ex.Message);
            }
        }

        //解析base64编码获取图片
        private Bitmap Base64ToImg(string base64Code)
        {
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64Code));
            return new Bitmap(stream);
        }
    }
}
