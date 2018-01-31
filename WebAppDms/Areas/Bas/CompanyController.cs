using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
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
            int CorpID = userInfo.CorpID;
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
                TradeMark = s.TradeMark,
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
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_bas_company> dbhelp = new DBHelper<t_bas_company>();
                DateTime dt = DateTime.Now;

                //事务
                var result = 0;
                try
                {
                    string base64Data = obj.TradeMark;
                    obj.TradeMark = "";//先置空
                    obj.UpdateTime = dt;
                    obj.UpdateUserID = (int)userInfo.UserID;
                    result = dbhelp.Update(obj);

                    //保存图片并修改数据库图片名称 
                    try
                    {
                        //获取文件储存路径            
                        string suffix = base64Data.Split(new char[] { ';' })[0].Substring(base64Data.IndexOf('/') + 1);//获取后缀名
                        string newFileName = "COMPANY_" + userInfo.CorpID.ToString("000000000") + "." + suffix;
                        string strPath = HttpContext.Current.Server.MapPath("~/" + UploadImgPath + "/" + newFileName); //获取当前项目所在目录           

                        //获取图片并保存
                        BaseToImg.Base64ToImg(base64Data.Split(',')[1]).Save(strPath);
                        obj.TradeMark = newFileName;
                    }
                    catch
                    {
                        obj.TradeMark = base64Data;
                    }
                    List<string> fileds = new List<string>();
                    fileds.Add("TradeMark");
                    result = result + dbhelp.UpdateEntityFields(obj, fileds);

                    //提交事务
                    transaction.Complete();
                    return Json(true, "保存成功！");
                }
                catch (Exception ex)
                {
                    return Json(false, "保存失败!" + ex.Message);
                }
            }
        }
    }
}
