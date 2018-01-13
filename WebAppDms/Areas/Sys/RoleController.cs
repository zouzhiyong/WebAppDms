using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Sys
{
    public class RoleController : ApiBaseController
    {
        //public HttpResponseMessage FindSysRoleTree()
        //{
        //    var list = db.sys_role.Where<sys_role>(p => p.IsValid != 0).Select(s => new
        //    {
        //        label = s.RoleName,
        //        RoleID = s.RoleID
        //    }).ToList();

        //    return Json(true, "", list);
        //}

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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public HttpResponseMessage FindSysRoleTable(dynamic obj)
        //{
        //    DBHelper<view_role> dbhelp = new DBHelper<view_role>();

        //    int pageSize = obj.pageSize;
        //    int currentPage = obj.currentPage;
        //    int total = 0;

        //    var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => 1 == 1, s => s.RoleID, true);

        //    return Json(list, currentPage, pageSize, total);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public HttpResponseMessage DeleteSysRoleRow(sys_role obj)
        //{
        //    var result = new DBHelper<sys_role>().Remove(obj);

        //    return Json(true, result == 1 ? "删除成功！" : "删除失败");
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public HttpResponseMessage FindSysRoleForm(dynamic obj)
        //{
        //    int RoleID = obj == null ? 0 : obj.RoleID;


        //    if (RoleID == 0)
        //    {
        //        var list = new
        //        {
        //            RoleID = 0,
        //            MenuName = "",
        //            IsValid = 1,
        //            IsValidList = new object[] {
        //            new { label = "有效", value = 1 },
        //            new { label = "无效", value = 0 }
        //        }.ToList(),
        //            ModifyUserID = "",
        //            ModifyDate = DateTime.Now,
        //            CreateUserID = "",
        //            CreateDate = DateTime.Now,
        //            RoleDesc = ""
        //        };
        //        return Json(true, "", list);
        //    }
        //    else
        //    {
        //        var list = db.sys_role.Where(w => w.RoleID == RoleID).Select(s => new
        //        {
        //            RoleID = s.RoleID,
        //            RoleName = s.RoleName,
        //            IsValid = s.IsValid == null ? 1 : s.IsValid,
        //            IsValidList = new object[] {
        //            new { label = "有效", value = 1 },
        //            new { label = "无效", value = 0 }
        //        }.ToList(),
        //            ModifyUserID = "",
        //            ModifyDate = DateTime.Now,
        //            CreateUserID = "",
        //            CreateDate = DateTime.Now,
        //            RoleDesc = s.RoleDesc
        //        }).FirstOrDefault();

        //        return Json(true, "", list);
        //    }
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public HttpResponseMessage SaveSysRoleForm(sys_role obj)
        //{
        //    DBHelper<sys_role> dbhelp = new DBHelper<sys_role>();
        //    var result = obj.RoleID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

        //    return Json(true, result == 1 ? "保存成功！" : "保存失败");
        //}
    }
}
