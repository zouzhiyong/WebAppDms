using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        /// <summary>
        /// 获取菜单编辑窗口右边表格
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindSysMoudleTable(dynamic obj)
        {
            DBHelper<t_sys_menumodule> dbhelp = new DBHelper<t_sys_menumodule>();

            string Code = obj.Code;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.ParentCode == Code, s => s.FID, true);

            return Json(list, currentPage, pageSize, total);
        }

        /// <summary>
        /// 表单数据保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage SaveSysMoudleForm(t_sys_menumodule obj)
        {
            DBHelper<t_sys_menumodule> dbhelp = new DBHelper<t_sys_menumodule>();
            var result = obj.Code == "00" ? dbhelp.Add(obj) : dbhelp.Update(obj);

            return Json(true, result == 1 ? "保存成功！" : "保存失败");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindMenu()
        {
            string userId = "3";// HttpContext.Current.Session["userId"].ToString();

            var list = db.view_menu.Where<view_menu>(p => p.UserID.ToString() == userId && p.ParentCode == "00").Select(s => new
            {
                path = "/",
                name = s.Name,
                component = "",
                Xh = s.Sequence,
                MenuID = s.Code,
                iconCls = s.ICON,
                children = db.view_menu.Where<view_menu>(p1 => p1.ParentCode == s.Code).Select(s1 => new
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindSysModuleTree()
        {
            var list = db.view_menu.Where<view_menu>(p => p.ParentCode == "00").Select(s => new
            {
                label = s.Name,
                Sequence = s.Sequence,
                FID = s.FID,
                Code= s.Code,
                children = db.view_menu.Where<view_menu>(p1 => p1.ParentCode == s.Code).Select(s1 => new
                {
                    label = s1.Name,
                    Sequence = s1.Sequence,
                    FID = s1.FID,
                    Code = s.Code,
                }).OrderBy(o => o.Sequence).ThenBy(o => o.FID).ToList()
            }).OrderBy(o => o.Sequence).ThenBy(o => o.FID).ToList();


            return Json(true, "", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage FindSysMoudleForm(dynamic obj)
        {
            int FID = obj == null ? 0 : obj.FID;


            if (FID == 0)
            {
                var list = new
                {
                    Code = "",
                    ParentCode = obj.ParentCode,
                    ParentCodeList = new object[] { new { label = "未对应上级", value = "00" } }.
                        Concat(db.t_sys_menumodule.Where(w1 => w1.ParentCode == "00").Select(s1 => new
                        {
                            label = s1.Name,
                            value = s1.Code
                        })),
                    Name = "",
                    URL = "",
                    ICON = "",
                    IsValid = 1,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                }.ToList(),
                    PlatformType = 1,
                    PlatformTypeList = new object[] {
                    new { label = "WEB", value = 1 },
                    new { label = "APP", value = 2 },
                    new { label = "Wechat", value = 3 }
                }.ToList(),
                    Sequence = 0
                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.t_sys_menumodule.Where(w => w.FID == FID).Select(s => new
                {
                    Code = s.Code,
                    ParentCode = s.ParentCode,
                    ParentCodeList = new object[] { new { label = "未对应上级", value = "00" } }.
                        Concat(db.t_sys_menumodule.Where(w1 => w1.ParentCode == "00").Select(s1 => new
                        {
                            label = s1.Name,
                            value = s1.Code
                        })),
                    Name = s.Name,
                    URL = s.URL,
                    ICON = s.ICON,
                    IsValid = s.IsValid,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                }.ToList(),
                    PlatformType = 1,
                    PlatformTypeList = new object[] {
                    new { label = "WEB", value = 1 },
                    new { label = "APP", value = 2 },
                    new { label = "Wechat", value = 3 }
                }.ToList(),
                    Sequence = s.Sequence

                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage DeleteSysMoudleRow(t_sys_menumodule obj)
        {
            var result = new DBHelper<t_sys_menumodule>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }
    }
}
