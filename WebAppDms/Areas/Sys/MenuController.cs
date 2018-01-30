using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Sys
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuController : ApiBaseController
    {
        t_bas_user userInfo = (t_bas_user)UserSession.Get("UserInfo");
        public HttpResponseMessage FindSysMoudleTable(dynamic obj)
        {
            DBHelper<t_sys_menumodule> dbhelp = new DBHelper<t_sys_menumodule>();

            string Code = obj.Code;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.ParentCode == Code, s => s.Sequence, true);

            return Json(list, currentPage, pageSize, total);
        }


        public HttpResponseMessage SaveSysMoudleForm(dynamic obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_sys_menumodule> dbhelp = new DBHelper<t_sys_menumodule>();
                DateTime dt = DateTime.Now;
                int FID = obj.FID;
                var tSysButton = db.t_sys_button.Where(w => w.IsValid != 0);

                //事务
                var result = 0;
                try
                {
                    var menumodule = new t_sys_menumodule()
                    {
                        FID = obj.FID,
                        CreateTime = dt,
                        CreateUserID = (int)userInfo.UserID,
                        UpdateTime = dt,
                        UpdateUserID = (int)userInfo.UserID,
                        Code = obj.Code,
                        Name = obj.Name,
                        ParentCode = obj.ParentCode,
                        IsMenu = obj.IsMenu,
                        Level = obj.ParentCode == "&" ? 1 : 2,
                        Sequence = obj.Sequence,
                        URL = obj.URL,
                        ICON = obj.ICON,
                        IsValid = obj.IsValid,
                        ControllerName = obj.ControllerName,
                        Descript = obj.Descript
                    };

                    if (FID != 0)
                    {
                        //批量删除
                        var tempList=db.t_sys_modulebutton.Where(w => w.ModuleID == FID);
                        foreach (var item in tempList)
                        {
                            db.t_sys_modulebutton.Remove(item);
                        }
                        result = db.SaveChanges();
                    }

                    result = result + (obj.FID == 0 ? dbhelp.Add(menumodule) : dbhelp.Update(menumodule));

                    //批量插入
                    foreach (int item in obj.CheckedButtons)
                    {
                        var button = new t_sys_modulebutton();
                        button.ButtonID = item;
                        button.CorpID = userInfo.CorpID;
                        button.CreateTime = dt;
                        button.CreateUserID = (int)userInfo.UserID;
                        button.UpdateTime = dt;
                        button.UpdateUserID = (int)userInfo.UserID;
                        button.IsValid = 1;
                        button.IsVisible = 1;
                        button.ModuleID = (int)menumodule.FID;
                        button.Name = tSysButton.Where(w => w.ButtonID == item).Select(s => s.Name).FirstOrDefault();
                        db.t_sys_modulebutton.Add(button);
                        result = result + db.SaveChanges();
                    }

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

        public HttpResponseMessage FindMenu()
        {
            //string userId = "3";// HttpContext.Current.Session["userId"].ToString();


            var list = db.view_menu.Where<view_menu>(p => p.UserID == userInfo.UserID && p.ParentCode == "&" && p.PlatformType == 9).Select(s => new
            {
                path = "/",
                name = s.Name,
                component = "",
                Xh = s.Sequence,
                MenuID = s.Code,
                iconCls = s.ICON,
                children = db.view_menu.Where<view_menu>(p1 => p1.UserID == userInfo.UserID && p1.ParentCode == s.Code && p1.PlatformType == 9).Select(s1 => new
                {
                    path = "/" + s1.URL,
                    name = s1.Name,
                    component = s1.URL,
                    Xh = s1.Sequence,
                    MenuID = s1.Code
                }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList()
            }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList();


            return Json(true, "", list);
        }

        public HttpResponseMessage FindSysModuleTree()
        {
            var list = db.view_menu.Where<view_menu>(p => p.UserID == userInfo.UserID && p.ParentCode == "&").Select(s => new
            {
                label = s.Name,
                Sequence = s.Sequence,
                FID = s.FID,
                Code = s.Code,
                children = db.view_menu.Where<view_menu>(p1 => p1.UserID == userInfo.UserID && p1.ParentCode == s.Code).Select(s1 => new
                {
                    label = s1.Name,
                    Sequence = s1.Sequence,
                    FID = s1.FID,
                    Code = s1.Code,
                }).OrderBy(o => o.Sequence).ThenBy(o => o.FID).ToList()
            }).OrderBy(o => o.Sequence).ThenBy(o => o.FID).ToList();


            return Json(true, "", list);
        }

        public HttpResponseMessage FindSysMoudleForm(t_sys_menumodule obj)
        {
            long FID = obj.FID;
            string ParentCode = obj.ParentCode;

            var maxCode = db.t_sys_menumodule.Where(w0 => w0.ParentCode == ParentCode).OrderByDescending(o => o.Code).Select(s0 => new { Code = s0.Code }).FirstOrDefault();

            var PlatFormTypeList = db.t_datadict_class.Where(w => w.IsValid != 0 && w.Code == "ModuleType").Join(db.t_datadict_class_detail.Where(w => w.IsValid != 0), a => a.ClassID, b => b.ClassID, (a, b) => new
            {
                label = b.Name,
                value = b.DClassID
            }).OrderBy(o => o.value);

            //判断是否系统用户
            bool IsSystem = db.t_sys_rights.Where(w => w.RightsID == userInfo.RightsID).Select(s => s.IsSystem).FirstOrDefault()== 1;
            var MenuModule = IsSystem ? db.t_sys_menumodule.ToList() : db.t_sys_menumodule.Join(db.t_sys_rights_detail.Where(w => w.CorpID == userInfo.CorpID && w.RightsID == (int)UserSession.Get("CompanyRightsID")), a => a.FID, b => b.ModuleID, (a, b) => a).Distinct().ToList();
            var Buttons = IsSystem ? db.t_sys_button.Where(w => w.IsValid != 0).ToList() : db.t_sys_button.Where(w => w.IsValid != 0).Join(db.t_sys_rights_detail.Where(w => w.CorpID == userInfo.CorpID && w.RightsID == (int)UserSession.Get("CompanyRightsID")), a => a.ButtonID, b => b.ButtonID, (a, b) => a).Distinct().ToList();
            var CheckedButtons = IsSystem ? db.t_sys_modulebutton.Where(w => w.ModuleID == FID && w.IsValid != 0 && w.IsVisible != 0).Select(s0 => s0.ButtonID).ToList() : db.t_sys_modulebutton.Where(w => w.ModuleID == FID && w.IsValid != 0 && w.IsVisible != 0 && w.CorpID == userInfo.CorpID).Join(db.t_sys_rights_detail.Where(w => w.CorpID == userInfo.CorpID && w.RightsID == (int)UserSession.Get("CompanyRightsID")), a => a.ButtonID, b => b.ButtonID, (a, b) =>a.ButtonID).Distinct().ToList();

            var ParentCodeList = MenuModule.Where(w1 => w1.ParentCode == "&" && w1.FID != FID).OrderBy(o => o.FID).Select(s1 => new
            {
                label = s1.Name,
                value = s1.Code
            });

            if (FID == 0)
            {
                var list = new
                {
                    FID = 0,
                    CreateTime = "",
                    CreateUserID = "",
                    UpdateTime = "",
                    UpdateUserID = "",
                    TimeStamp = "",
                    IsMenu = 1,
                    Code = string.Format("{0:d" + maxCode.Code.ToString().Length + "}", (int.Parse(maxCode.Code) + 1)),
                    ParentCode = ParentCode,
                    ParentCodeList = ParentCodeList,
                    Name = "",
                    ControllerName = "",
                    URL = "",
                    ICON = "",
                    IsValid = 1,
                    PlatformType = PlatFormTypeList.FirstOrDefault().value,
                    PlatformTypeList = PlatFormTypeList,
                    Sequence = 0,
                    Buttons = Buttons,
                    CheckedButtons = CheckedButtons
                };
                return Json(true, "", list);
            }
            else
            {
                var list = MenuModule.Where(w => w.FID == FID).Select(s => new
                {
                    FID = s.FID,
                    CreateTime = s.CreateTime,
                    CreateUserID = s.CreateUserID,
                    UpdateTime = s.UpdateTime,
                    UpdateUserID = s.UpdateUserID,
                    TimeStamp = s.TimeStamp,
                    IsMenu = s.IsMenu,
                    Code = s.Code,
                    ParentCode = s.ParentCode,
                    ParentCodeList = ParentCodeList,
                    Name = s.Name,
                    ControllerName = s.ControllerName,
                    URL = s.URL,
                    ICON = s.ICON,
                    IsValid = s.IsValid,
                    PlatformType = s.PlatformType,
                    PlatformTypeList = PlatFormTypeList,
                    Sequence = s.Sequence,
                    Buttons = Buttons,
                    CheckedButtons = CheckedButtons
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteSysMoudleRow(t_sys_menumodule obj)
        {
            var result = new DBHelper<t_sys_menumodule>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }
    }
}
