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

        partial class sysMenuForm : sys_menu
        {
            public IQueryable<object> ApplicationNoList { get; set; }
            public IEnumerable<object> MenuParentIDList { get; set; }
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindSysMenu()
        {
            string userId = "3";// HttpContext.Current.Session["userId"].ToString();

            var list = db.view_menu.Where<view_menu>(p => p.UserID.ToString() == userId).Select(s => new
            {
                MenuID = s.MenuID,
                MenuParentID = s.MenuParentID,
                MenuName = s.MenuName,
                MenuUrl = s.MenuUrl,
                MenuPath = s.MenuPath,
                MenuIcon = s.MenuIcon,
                IsValid = s.IsValid,
                ApplicationNo = s.ApplicationNo,
                Xh = s.Xh,
                Controls = (from t in db.sys_templates
                            where t.TemplateName == s.MenuUrl
                            select t.Controls).FirstOrDefault()
            }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList();

            //var _list = (from t in list
            //             select new
            //             {
            //                 MenuID = t.MenuID,
            //                 MenuParentID = t.MenuParentID,
            //                 MenuName = t.MenuName,
            //                 MenuUrl = t.MenuUrl,
            //                 MenuIcon = t.MenuIcon,
            //                 IsValid = t.IsValid,
            //                 ApplicationNo = t.ApplicationNo,
            //                 Controls= JsonConvert.SerializeObject((from t2 in db.Sys_Templates
            //                           where t2.TemplateName==t.MenuUrl select t2).ToList())
            //                 //Controls = (from t2 in db.Sys_Templates
            //                 //            join t3 in db.Sys_TemplateControl
            //                 //            on t2.TemplateID equals t3.TemplateID
            //                 //            join t4 in db.Sys_Controls
            //                 //            on t3.ControlID equals t4.ControlID
            //                 //            where t2.TemplateName == t.MenuUrl
            //                 //            select new
            //                 //            {
            //                 //                ControlName = t4.ControlName,
            //                 //                ControlID = t3.ControlID,
            //                 //                FindUrl = t3.FindUrl,
            //                 //                DeleteUrl = t3.DeleteUrl,
            //                 //                FormUrl = t3.FormUrl,
            //                 //                SaveUrl = t3.SaveUrl,
            //                 //                SubControls = (from t5 in db.Sys_SubControls
            //                 //                               where (t5.ControlID == t3.ControlID && t5.TemplateID == t3.TemplateID)
            //                 //                               select t5).OrderBy(x=>x.Xh).ToList()
            //                 //            }).ToList()
            //             }).ToList();

            return Json(true, "", list);
        }

        /// <summary>
        /// 菜单编辑窗口左边树
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindSysMoudle()
        {
            var list = db.sys_menu.ToList();


            // List<Bas_ComoditiesType> treeList = new List<Bas_ComoditiesType>();
            list.Add(new sys_menu
            {
                MenuID = 0,
                MenuName = "所有模块",
                MenuParentID = -1
            });

            //List<object> treeList = new List<object>();

            //var tempOjb = new
            //{
            //    MenuID = 0,
            //    label = "所有模块",
            //    MenuName = "所有模块",
            //    MenuParentID = -1,
            //    children = ListToTree(list, 0)
            //};

            //treeList.Add(tempOjb);

            return Json(true, "", new { rows = list, ID = "MenuID", ParentID = "MenuParentID", Label = "MenuName" });
        }

        /// <summary>
        /// 获取菜单编辑窗口右边表格
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindSysMoudleTable(dynamic obj)
        {
            DBHelper<sys_menu> dbhelp = new DBHelper<sys_menu>();

            int MenuID = obj.MenuID;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.MenuParentID == MenuID, s => s.MenuID, true);

            return Json(list, currentPage, pageSize, total);
        }
       
        /// <summary>
        /// 表单数据保存
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage SaveSysMoudleForm(sys_menu obj)
        {
            DBHelper<sys_menu> dbhelp = new DBHelper<sys_menu>();
            var result = obj.MenuID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj);

            return Json(true, result == 1 ? "保存成功！" : "保存失败");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage FindMenu()
        {
            string userId = "3";// HttpContext.Current.Session["userId"].ToString();

            var list = db.view_menu.Where<view_menu>(p => p.UserID.ToString() == userId && p.MenuParentID == 0).Select(s => new
            {
                path = "/",
                name = s.MenuName,
                component = "",
                Xh = s.Xh,
                MenuID = s.MenuID,
                iconCls = s.MenuIcon,
                children = db.view_menu.Where<view_menu>(p1 => p1.MenuParentID == s.MenuID).Select(s1 => new
                {
                    path = "/" + s1.MenuPath,
                    name = s1.MenuName,
                    component = s1.MenuPath,
                    Xh = s1.Xh,
                    MenuID = s1.MenuID
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
            var list = db.view_menu.Where<view_menu>(p => p.MenuParentID == 0).Select(s => new
            {
                label = s.MenuName,
                Xh = s.Xh,
                MenuID = s.MenuID,
                children = db.view_menu.Where<view_menu>(p1 => p1.MenuParentID == s.MenuID).Select(s1 => new
                {
                    label = s1.MenuName,
                    Xh = s1.Xh,
                    MenuID = s1.MenuID,
                }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList()
            }).OrderBy(o => o.Xh).ThenBy(o => o.MenuID).ToList();


            return Json(true, "", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public HttpResponseMessage FindSysMoudleForm(dynamic obj)
        {
            int MenuID = obj == null ? 0 : obj.MenuID;


            if (MenuID == 0)
            {
                var list = new
                {
                    MenuID = 0,
                    MenuParentID = 0,
                    MenuParentIDList = new object[] { new { label = "未对应上级", value = 0 } }.
                        Concat(db.sys_menu.Where(w1 => w1.MenuParentID == 0).Select(s1 => new
                        {
                            label = s1.MenuName,
                            value = s1.MenuID
                        })),
                    MenuName = "",
                    MenuPath = "",
                    MenuIcon = "",
                    IsValid = 1,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                }.ToList(),
                    ApplicationNo = "",
                    ApplicationNoList = db.sys_application.Select(s2 => new
                    {
                        label = s2.ApplicationName,
                        value = s2.ApplicationNo
                    }).ToList(),
                    Xh = 0

                };
                return Json(true, "", list);
            }
            else
            {
                var list = db.sys_menu.Where(w => w.MenuID == MenuID).Select(s => new
                {
                    MenuID = s.MenuID,
                    MenuParentID = s.MenuParentID,
                    MenuParentIDList = new object[] { new { label = "未对应上级", value = 0 } }.
                        Concat(db.sys_menu.Where(w1 => w1.MenuParentID == 0).Select(s1 => new
                        {
                            label = s1.MenuName,
                            value = s1.MenuID
                        })),
                    MenuName = s.MenuName,
                    MenuPath = s.MenuPath,
                    MenuIcon = s.MenuIcon,
                    IsValid = s.IsValid == null ? 1 : s.IsValid,
                    IsValidList = new object[] {
                    new { label = "有效", value = 1 },
                    new { label = "无效", value = 0 }
                }.ToList(),
                    ApplicationNo = s.ApplicationNo,
                    ApplicationNoList = db.sys_application.Select(s2 => new
                    {
                        label = s2.ApplicationName,
                        value = s2.ApplicationNo
                    }).ToList(),
                    Xh = s.Xh

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
        public HttpResponseMessage DeleteSysMoudleRow(sys_menu obj)
        {
            var result = new DBHelper<sys_menu>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }
    }
}
