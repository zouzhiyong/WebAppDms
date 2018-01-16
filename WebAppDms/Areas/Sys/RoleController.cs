using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Sys
{
    public class RoleController : ApiBaseController
    {
        public HttpResponseMessage FindSysRoleTree()
        {
            var list = db.t_sys_rights.Where<t_sys_rights>(p => p.IsValid != 0 && p.CorpID==UserSession.userInfo.CorpID).Select(s => new
            {
                label = s.Name,
                RoleID = s.RightsID
            }).ToList();

            return Json(true, "", list);
        }

        //public HttpResponseMessage FindSysRoleMenuTable(sys_rolemenu obj)
        //{
        //    var list = db.sys_menu.Where(w => w.IsValid != 0 && w.MenuParentID == 0).Select(s => new
        //    {
        //        MenuID = s.MenuID,
        //        MenuName = s.MenuName,
        //        MenuIcon = s.MenuIcon,
        //        Comment = s.Comment,
        //        MenuRolesData = db.sys_menu.Where(w1 => w1.IsValid != 0 && w1.MenuParentID == s.MenuID && db.sys_rolemenu.Where(w0 => w0.RoleID == obj.RoleID && w0.MenuID == w1.MenuID).Select(s0 => s0.MenuID).ToList().Count > 0).Select(s1 => s1.MenuID).ToList(),
        //        chilDren = db.sys_menu.Where(w2 => w2.IsValid != 0 && w2.MenuParentID == s.MenuID).Select(s2 => new
        //        {
        //            MenuID = s2.MenuID,
        //            MenuName = s2.MenuName
        //        }).ToList()
        //    }).ToList();

        //    return Json(true, "", list);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public HttpResponseMessage SaveSysRoleMenuForm(dynamic obj)
        //{
        //    int RoleID = obj.RoleID;
        //    var arr = obj.MenuID;
        //    var temp = db.Set<sys_rolemenu>().Where(w => w.RoleID == RoleID);
        //    foreach (var item in temp)
        //    {
        //        db.Entry<sys_rolemenu>(item).State = EntityState.Deleted;
        //    }

        //    List<sys_rolemenu> list = new List<sys_rolemenu>();


        //    foreach (int item in arr)
        //    {
        //        var tempObj = new sys_rolemenu()
        //        {
        //            RoleID = RoleID,
        //            MenuID = item,
        //            RecTimeStamp = DateTime.Now,
        //            CreateUserID = 1,
        //            CreateDate = DateTime.Now,
        //            ModifyUserID = 1,
        //            ModifyDate = DateTime.Now
        //        };
        //        list.Add(tempObj);

        //    }

        //    int result = 0;
        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        if (list[i] == null)
        //            continue;
        //        db.Entry<sys_rolemenu>(list[i]).State = EntityState.Added;
        //    }

        //    result += db.SaveChanges();

        //    return Json(true, result > 0 ? "保存成功！" : "保存失败");
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage FindSysRoleTable(dynamic obj)
        {
            DBHelper<view_rights> dbhelp = new DBHelper<view_rights>();

            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;
            long CorpID = (long)UserSession.userInfo.CorpID;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x=>x.CorpID == CorpID, s => s.RightsID, true);

            return Json(list, currentPage, pageSize, total);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DeleteSysRoleRow(t_sys_rights obj)
        {
            var result = new DBHelper<t_sys_rights>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage FindSysRoleForm(dynamic obj)
        {
            int RightsID = obj == null ? 0 : obj.RightsID;


            if (RightsID == 0)
            {
                var list = new
                {
                    RightsID = 0,
                    Name = "",
                    IsValid = 1
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.t_sys_rights.Where(w => w.RightsID == RightsID).Select(s => new
                {
                    RightsID = s.RightsID,
                    Name = s.Name,
                    IsValid = s.IsValid,                  
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage SaveSysRoleForm(t_sys_rights obj)
        {
            DBHelper<t_sys_rights> dbhelp = new DBHelper<t_sys_rights>();

            if (obj.RightsID == 0)
            {
                obj.CreateTime = DateTime.Now;
                obj.CreateUserID = (int)UserSession.userInfo.UserID;
                obj.UpdateTime = DateTime.Now;
                obj.UpdateUserID = (int)UserSession.userInfo.UserID;
                obj.CorpID = UserSession.userInfo.CorpID;
                obj.Code = "";
            }
            else
            {
                obj.UpdateTime = DateTime.Now;
                obj.UpdateUserID = (int)UserSession.userInfo.UserID;
            }
            var result = obj.RightsID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

            return Json(true, result == 1 ? "保存成功！" : "保存失败");
        }
    }
}
