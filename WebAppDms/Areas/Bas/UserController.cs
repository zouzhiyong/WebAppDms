using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class UserController : ApiBaseController
    {
        string VirtualPath = ConfigurationManager.AppSettings["VirtualPath"].ToString();
        string UploadImgPath = ConfigurationManager.AppSettings["UploadImgPath"].ToString();

        public HttpResponseMessage FindBasDeptTree()
        {
            var list = db.t_bas_department.Where<t_bas_department>(p => p.IsValid != 0 && p.CorpID == UserSession.userInfo.CorpID).OrderBy(o => o.DeptID).Select(s => new
            {
                label = s.Name,
                DeptID = s.DeptID
            }).ToList();

            return Json(true, "", list);
        }

        public HttpResponseMessage FindBasUserTable(dynamic obj)
        {
            DBHelper<view_user> dbhelp = new DBHelper<view_user>();

            int DeptID = obj.DeptID;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            Expression<Func<view_user, bool>> where = null;

            if (DeptID == 0)
            {
                where = a => true;
            }
            else
            {
                where = a => a.DeptID == DeptID;
            }

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, where, s => s.UserID, true);

            return Json(list, currentPage, pageSize, total);
        }

        public HttpResponseMessage FindBasUserForm(t_bas_user obj)
        {
            long DeptID = obj.DeptID;
            long UserID = obj.UserID;

            var DeptIDList = db.t_bas_department.Where(w => w.IsValid != 0 && w.CorpID == UserSession.userInfo.CorpID).OrderBy(o=>o.DeptID).Select(s => new
            {
                label = s.Name,
                value = s.DeptID
            });

            var UserCategoryIDList = db.t_datadict_class.Where(w => w.Code == "EmpCategory" && w.IsValid != 0 && w.IsVisible != 0)
                .Join(db.t_datadict_class_detail.Where(w => w.IsValid != 0 && w.IsVisible != 0), a => a.ClassID, b => b.ClassID, (a, b) => new
                {
                    label = b.Name,
                    value = b.DClassID
                });

            var PositionIDList = db.t_bas_position.Where(w => w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.PositionID
            });

            var RightsIDList = db.t_sys_rights.Where(w => w.CorpID == UserSession.userInfo.CorpID && w.IsValid != 0 && w.IsSystem == UserSession.IsSystem).Select(s => new
            {
                label = s.Name,
                value = s.RightsID
            });

            var ParentEmpIDList = db.t_bas_user.Where(w => w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.UserID
            });

            var CertificateIDList = db.t_datadict_class.Where(w => w.IsValid != 0 && w.IsVisible != 0 && w.Code == "Certificate").Join(db.t_datadict_class_detail.Where(w => w.IsVisible != 0 && w.IsValid != 0), a => a.ClassID, b => b.ClassID, (a, b) => new
            {
                label = b.Name,
                value = b.DClassID
            });

            if (UserID == 0)
            {
                var list = new
                {
                    UserID = 0,
                    CorpID = UserSession.userInfo.CorpID,
                    Code = "",
                    Name = "",
                    UserCategoryID = 0,
                    UserCategoryIDList = UserCategoryIDList,
                    PositionID = 0,
                    PositionIDList = PositionIDList,
                    RightsID = 0,
                    RightsIDList = RightsIDList,
                    DeptID = 0,
                    DeptIDList = DeptIDList,
                    ParentEmpID = 0,
                    ParentEmpIDList = ParentEmpIDList,
                    CertificateID =0,
                    CertificateIDList = CertificateIDList,
                    CertificateNumber = "",
                    Password = "",
                    FailedPasswordCount = "",
                    PasswordQuestion = "",
                    PasswordAnswer = "",
                    Tel = "",
                    Phone = "",
                    QQ = "",
                    WeiXin = "",
                    Email = "",
                    Photo = "",
                    IsUseSystem = 1,
                    IsAppUser = "",
                    IsValid = 1,
                    IsLockedout = 0,
                    LockedoutTime = "",
                    LockedoutUserID = "",
                    LastActiveTIme = "",
                    LastLoginDate = "",
                    LastChangePwdDate = "",
                    SessionID = "",
                    Remark = "",
                    CreateTime = "",
                    CreateUserID = "",
                    UpdateTime = "",
                    UpdateUserID = "",
                    IMEICode = ""
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.t_bas_user.Where(w => w.DeptID == DeptID && w.UserID==obj.UserID).Select(s => new
                {
                    UserID = s.UserID,
                    CorpID = UserSession.userInfo.CorpID,
                    Code = s.Code,
                    Name = s.Name,
                    UserCategoryID = s.UserCategoryID,
                    UserCategoryIDList = UserCategoryIDList,
                    PositionID = s.PositionID,
                    PositionIDList = PositionIDList,
                    RightsID = s.RightsID,
                    RightsIDList = RightsIDList,
                    DeptID = s.DeptID,
                    DeptIDList = DeptIDList,
                    ParentEmpID = s.ParentEmpID,
                    ParentEmpIDList = ParentEmpIDList,
                    CertificateID = s.CertificateID,
                    CertificateIDList = CertificateIDList,
                    CertificateNumber = s.CertificateNumber,
                    Password = s.Password,
                    FailedPasswordCount = s.FailedPasswordCount,
                    PasswordQuestion = s.PasswordQuestion,
                    PasswordAnswer = s.PasswordAnswer,
                    Tel = s.Tel,
                    Phone = s.Phone,
                    QQ = s.QQ,
                    WeiXin = s.WeiXin,
                    Email = s.Email,
                    Photo = s.Photo,
                    IsUseSystem = s.IsUseSystem,
                    IsAppUser = s.IsAppUser,
                    IsValid = s.IsValid,
                    IsLockedout = s.IsLockedout,
                    LockedoutTime = s.LockedoutTime,
                    LockedoutUserID = s.LockedoutUserID,
                    LastActiveTIme = s.LastActiveTIme,
                    LastLoginDate = s.LastLoginDate,
                    LastChangePwdDate = s.LastChangePwdDate,
                    SessionID = s.SessionID,
                    Remark = s.Remark,
                    CreateTime = s.CreateTime,
                    CreateUserID = s.CreateUserID,
                    UpdateTime = s.UpdateTime,
                    UpdateUserID = s.UpdateUserID,
                    IMEICode = s.IMEICode
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage SaveBasUserForm(t_bas_user obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_bas_user> dbhelp = new DBHelper<t_bas_user>();
                DateTime dt = DateTime.Now;

                //事务
                var result = 0;
                try
                {
                    string base64Data = obj.Photo;
                    if (obj.UserID == 0)
                    {
                        obj.Photo = "";
                        obj.CreateTime = dt;
                        obj.CreateUserID = (int)UserSession.userInfo.UserID;                      
                        obj.CorpID = UserSession.userInfo.CorpID;
                        if (db.t_bas_user.Where(w => w.Code == obj.Code).ToList().Count() > 0)
                        {
                            throw new Exception("账号重复！");
                        }
                        result = result + dbhelp.Add(obj);                       
                    }
                    else
                    {
                        obj.Photo = "";
                        obj.UpdateTime = dt;
                        obj.UpdateUserID = (int)UserSession.userInfo.UserID;
                        if (db.t_bas_user.Where(w => w.Code == obj.Code).ToList().Count() > 1)
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
                        string newFileName = "USER_" + obj.UserID.ToString("000000000") + "." + suffix;
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

        [HttpPost]
        public HttpResponseMessage DeleteBasUserRow(t_bas_user obj)
        {
            var result = new DBHelper<t_bas_user>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }
    }
}
