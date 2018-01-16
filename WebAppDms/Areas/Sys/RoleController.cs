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
            var list = db.t_sys_rights.Where<t_sys_rights>(p => p.IsValid != 0 && p.CorpID == UserSession.userInfo.CorpID).OrderBy(o => o.RightsID).Select(s => new
            {
                label = s.Name,
                RightsID = s.RightsID
            }).ToList();

            return Json(true, "", list);
        }

        public HttpResponseMessage FindSysRoleMenuTable(t_sys_rights_detail obj)
        {
            var list = db.t_sys_menumodule.Where(w => w.IsValid != 0 && w.ParentCode == "&").Select(s => new
            {
                FID = s.FID,
                Code = s.Code,
                Name = s.Name,
                ICON = s.ICON,
                Descript = s.Descript,
                MenuRolesData = db.t_sys_menumodule.Where(w1 => w1.IsValid != 0 && w1.ParentCode == s.Code && db.t_sys_rights_detail.Where(w0 => w0.RightsID == obj.RightsID && w0.ModuleID == w1.FID).Select(s0 => s0.RightsID).ToList().Count > 0).Select(s1 => s1.Code).ToList(),
                chilDren = db.t_sys_menumodule.Where(w2 => w2.IsValid != 0 && w2.ParentCode == s.Code).Select(s2 => new
                {
                    Code = s2.Code,
                    Name = s2.Name
                }).ToList()
            }).ToList();

            return Json(true, "", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage SaveSysRoleMenuForm(getRightsCode obj)
        {
            int RightsID = obj.RightsID;
            string[] arr = obj.arr;

            var temp = db.Set<t_sys_rights_detail>().Where(w => w.RightsID == RightsID && w.CorpID == UserSession.userInfo.CorpID);
            foreach (var item in temp)
            {
                db.Entry<t_sys_rights_detail>(item).State = EntityState.Deleted;
            }

            var fids = db.t_sys_menumodule.Where(w => arr.Contains(w.Code)).Select(s => s.FID).ToList();

            List<t_sys_rights_detail> list = new List<t_sys_rights_detail>();

            foreach (int item in fids)
            {
                var tempObj = new t_sys_rights_detail()
                {
                    RightsID = RightsID,
                    ModuleID = item,
                    CorpID = UserSession.userInfo.CorpID,
                    CreateTime = DateTime.Now,
                    CreateUserID = (int)UserSession.userInfo.UserID,
                    UpdateTime = DateTime.Now,
                    UpdateUserID = (int)UserSession.userInfo.UserID
                };
                list.Add(tempObj);
            }

            int result = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i] == null)
                    continue;
                db.Entry<t_sys_rights_detail>(list[i]).State = EntityState.Added;
            }

            result += db.SaveChanges();

            return Json(true, result > 0 ? "保存成功！" : "保存失败");
        }

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

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.CorpID == CorpID, s => s.RightsID, true);

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

        public class getRightsCode
        {
            public string[] arr { get; set; }

            public int RightsID { get; set; }
        }
    }
}
