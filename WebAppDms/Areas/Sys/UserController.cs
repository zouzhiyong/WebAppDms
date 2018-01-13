using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Sys
{
    ///// <summary>
    ///// 
    ///// </summary>
    //public class UserController : ApiBaseController
    //{
    //    /// <summary>
    //    /// 获取部门
    //    /// </summary>
    //    /// <returns></returns>
    //    public HttpResponseMessage FindSysDeptTree()
    //    {
    //        var list = db.sys_dept.Where<sys_dept>(p => p.IsValid != 0).Select(s => new
    //        {
    //            label = s.Name,
    //            DeptID = s.DeptID
    //        }).ToList();

    //        return Json(true, "", list);
    //    }
    //    /// <summary>
    //    /// 获取用户
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public HttpResponseMessage FindSysUserTable(dynamic obj)
    //    {
    //        DBHelper<view_user> dbhelp = new DBHelper<view_user>();

    //        int DeptID = obj.DeptID;
    //        int pageSize = obj.pageSize;
    //        int currentPage = obj.currentPage;
    //        int total = 0;

    //        Expression<Func<view_user, bool>> where = null;

    //        if (DeptID == 0)
    //        {
    //            where = a => true;
    //        }else
    //        {
    //            where = a => a.DeptID == DeptID;
    //        }

    //        var list = dbhelp.FindPagedList(currentPage, pageSize, out total, where, s => s.UserID, true);

    //        return Json(list, currentPage, pageSize, total);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public HttpResponseMessage FindSysUserForm(dynamic obj)
    //    {
    //        int UserID = obj == null ? 0 : obj.UserID;

    //        if (UserID == 0)
    //        {
    //            var list = new
    //            {
    //                UserID = 0,
    //                LoginName = "",
    //                DeptID = DBNull.Value,
    //                RealName = "",
    //                Phone = "",
    //                UserTypeID = DBNull.Value,
    //                BirthDate = "",
    //                CreateDate = "",
    //                LastLoginDate = "",
    //                Email = "",
    //                IMEICode = "",
    //                RoleID = "",
    //                WorkingPlace = "",
    //                Avatar = "",
    //                Comment = "",
    //                DeptIDList = db.sys_dept.Select(s1 => new
    //                {
    //                    value = s1.DeptID,
    //                    label = s1.Name
    //                }).ToList(),
    //                UserTypeIDList = db.sys_usertype.Select(s1 => new
    //                {
    //                    value = s1.UserTypeID,
    //                    label = s1.UserTypeName
    //                }).ToList(),
    //                RoleIDList = db.sys_role.Select(s1 => new
    //                {
    //                    value = s1.RoleID,
    //                    label = s1.RoleName
    //                }).ToList(),
    //                IsValid = 1,
    //                IsValidList = new object[] {
    //                new { label = "有效", value = 1 },
    //                new { label = "无效", value = 0 }
    //            }.ToList()
    //            };
    //            return Json(true, "", list);
    //        }
    //        else
    //        {
    //            var list = db.sys_user.Where(w => w.UserID == UserID).Select(s => new
    //            {
    //                UserID = s.UserID,
    //                LoginName = s.LoginName,
    //                DeptID = s.DeptID,
    //                RealName = s.RealName,
    //                Phone = s.Phone,
    //                UserTypeID = s.UserTypeID,
    //                BirthDate = s.BirthDate,
    //                CreateDate = s.CreateDate,
    //                LastLoginDate = s.LastLoginDate,
    //                Email = s.Email,
    //                IMEICode = s.IMEICode,
    //                RoleID = db.sys_userrole.Where(w1 => w1.UserID == s.UserID).Select(s1 => s1.RoleID).FirstOrDefault(),
    //                IsValid = s.IsValid == null ? 1 : s.IsValid,
    //                WorkingPlace = s.WorkingPlace,
    //                Avatar = s.Avatar,
    //                Comment = s.Comment,
    //                RoleIDList = db.sys_role.Select(s1 => new
    //                {
    //                    value = s1.RoleID,
    //                    label = s1.RoleName
    //                }).ToList(),
    //                UserTypeIDList = db.sys_usertype.Select(s1 => new
    //                {
    //                    value = s1.UserTypeID,
    //                    label = s1.UserTypeName
    //                }).ToList(),
    //                DeptIDList = db.sys_dept.Select(s1 => new
    //                {
    //                    value = s1.DeptID,
    //                    label = s1.Name
    //                }).ToList(),
    //                IsValidList = new object[] {
    //                new { label = "有效", value = 1 },
    //                new { label = "无效", value = 0 }
    //            }.ToList(),
    //            }).FirstOrDefault();

    //            return Json(true, "", list);
    //        }
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public HttpResponseMessage SaveSysUserForm(dynamic obj)
    //    {
    //        var destination = new sys_user()
    //        {
    //            UserID = obj.UserID,
    //            LoginName = obj.LoginName,
    //            LoginPassword = "",
    //            DeptID = obj.DeptID,
    //            RealName = obj.RealName,
    //            Phone = obj.Phone,
    //            UserTypeID = obj.UserTypeID,
    //            BirthDate = DateTime.Now,
    //            CreateDate = DateTime.Now,
    //            LastLoginDate = DateTime.Now,
    //            Email = obj.Email,
    //            IMEICode = obj.IMEICode,
    //            IsValid = obj.IsValid,
    //            WorkingPlace = obj.WorkingPlace,
    //            Avatar = obj.Avatar,
    //            Comment = obj.Comment,
    //            SessionId = Guid.NewGuid().ToString(),
    //            RecTimeStamp = DateTime.Now,
    //            sys_userrole = new List<sys_userrole>
    //                {
    //                    new sys_userrole {
    //                        RoleID =obj.RoleID,
    //                        UserID=obj.UserID,
    //                        CreateUserID=0,
    //                        CreateDate=DateTime.Now,
    //                        ModifyUserID=obj.UserID,
    //                        ModifyDate=DateTime.Now,
    //                        RecTimeStamp=DateTime.Now
    //                    }
    //                }
    //        };


    //        DBHelper<sys_user> dbhelp = new DBHelper<sys_user>();

    //        var result = obj.UserID == 0 ? dbhelp.Add(destination) : dbhelp.Update(destination);

    //        return Json(true, result > 0 ? "保存成功！" : "保存失败");
    //    }


    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    [HttpPost]
    //    public HttpResponseMessage DeleteSysUserRow(sys_user obj)
    //    {
    //        //var sysUser = new Sys_User();


    //        //var result = new DBHelper<Sys_User>().Remove(obj);

    //        var sysUser = (from t in db.sys_user
    //                       where t.UserID == obj.UserID
    //                       select t).Single();

    //        foreach (var sysUserRole in sysUser.sys_userrole.ToList())
    //        {
    //            db.sys_userrole.Remove(sysUserRole);   //先标记相关的从表数据为删除状态
    //        }
    //        db.sys_user.Remove(sysUser);    //再标记主表数据为删除装填
    //        var result = db.SaveChanges();   //执行上面的所有标记


    //        return Json(true, result > 0 ? "删除成功！" : "删除失败");
    //    }
    //}
}
