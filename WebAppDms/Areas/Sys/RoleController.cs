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

            var IsSystem = UserSession.IsSystem;
            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.CorpID == CorpID && x.IsSystem== IsSystem, s => s.RightsID, true);

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
        public HttpResponseMessage FindSysRoleForm(t_sys_rights obj)
        {
            long RightsID = obj.RightsID;


            if (RightsID == 0)
            {
                var list = new
                {
                    RightsID = 0,
                    Name = "",
                    IsValid = 1,
                    IsSystem=0,
                    Code = "",
                    CorpID = 0,
                    CreateTime = "",
                    CreateUserID = 0,
                    UpdateTime = "",
                    UpdateUserID = 0
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
                    IsSystem=s.IsSystem,
                    Code = s.Code,
                    CorpID = s.CorpID,
                    CreateTime = s.CreateTime,
                    CreateUserID = s.CreateUserID,
                    UpdateTime = s.UpdateTime,
                    UpdateUserID = s.UpdateUserID
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

            DateTime dt = DateTime.Now;
            if (obj.RightsID == 0)
            {
                obj.CreateTime = dt;
                obj.CreateUserID = (int)UserSession.userInfo.UserID;
                obj.UpdateTime = dt;
                obj.UpdateUserID = (int)UserSession.userInfo.UserID;
                obj.CorpID = UserSession.userInfo.CorpID;
                obj.Code = "";
            }
            else
            {
                obj.UpdateTime = dt;
                obj.UpdateUserID = (int)UserSession.userInfo.UserID;
            }
            var result = obj.RightsID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

            return Json(true, result == 1 ? "保存成功！" : "保存失败");
        }

       
    }
}
