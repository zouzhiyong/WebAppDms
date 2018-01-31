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
    public class RoleMenuController : ApiBaseController
    {        
        public HttpResponseMessage FindSysRoleMenuTree()
        {
            //var IsSystem = UserSession.IsSystem;
            var list = db.t_sys_rights.Where<t_sys_rights>(p => p.IsValid != 0 && p.CorpID == userInfo.CorpID).OrderBy(o => o.RightsID).Select(s => new
            {
                label = s.Name,
                RightsID = s.RightsID
            }).ToList();

            return Json(true, "", list);
        }

        public HttpResponseMessage FindSysRoleMenuTable(t_sys_rights_detail obj)
        {
            var viewMenu = db.t_sys_menumodule.Where(w => w.IsValid != 0).Join(db.t_sys_rights_detail, a => a.FID, b => b.ModuleID, (a, b) => new
            {
                Code = a.Code,
                FID = a.FID,
                ICON = a.ICON,
                Name = a.Name,
                ParentCode = a.ParentCode,
                Sequence = a.Sequence,
                Descript = a.Descript,
                RightsID = b.RightsID,
            }).Join(db.t_sys_company.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0), a => a.RightsID, b => b.RightsID, (a, b) => a);

            var list = viewMenu.Where(w => w.ParentCode != "&").OrderBy(o => o.Sequence).Select(s => new
            {
                ICON1 = viewMenu.Where(w => w.Code == s.ParentCode).Select(s2 => s2.ICON).FirstOrDefault(),
                FID1 = viewMenu.Where(w => w.Code == s.ParentCode).Select(s2 => s2.FID).FirstOrDefault(),
                Code1 = viewMenu.Where(w => w.Code == s.ParentCode).Select(s2 => s2.Code).FirstOrDefault(),
                Name1 = viewMenu.Where(w => w.Code == s.ParentCode).Select(s2 => s2.Name).FirstOrDefault(),
                FID2 = s.FID,
                Code2 = s.Code,
                Name2 = s.Name,
                ParentCode=s.ParentCode,
                Descript2 = s.Descript,
                isMenuRole = db.t_sys_rights_detail.Where(w => w.RightsID == obj.RightsID && w.ModuleID == s.FID).Count() > 0 ? true : false,
                ButtonRoles = db.t_sys_rights_detail.Where(w => w.RightsID == obj.RightsID && w.ModuleID == s.FID).Select(s3 => s3.ButtonID).ToList(),
                chilDren = db.t_sys_modulebutton.Where(w => w.ModuleID == s.FID && w.IsValid != 0 && w.IsVisible != 0).Join(db.t_sys_button.OrderBy(o => o.ButtonID).Where(w => w.IsValid != 0), a => a.ButtonID, b => b.ButtonID, (a, b) => new
                {
                    ModButtonID = a.ModButtonID,
                    ButtonID = b.ButtonID,
                    Name = b.Name
                }).ToList()
            }).Where(w=>w.FID1!=0 && w.FID2!=0).ToList();
     

            return Json(true, "", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public HttpResponseMessage SaveSysRoleMenuForm(getRightsCode obj)
        //{
        //    try
        //    {
        //        int RightsID = obj.RightsID;
        //        string[] arr = obj.arr;
        //        DateTime dt = DateTime.Now;
        //        t_sys_modulebutton[] buttonArr = obj.buttonArr;

        //        var temp = db.Set<t_sys_rights_detail>().Where(w => w.RightsID == RightsID && w.CorpID == UserSession.userInfo.CorpID);
        //        foreach (var item in temp)
        //        {
        //            db.Entry<t_sys_rights_detail>(item).State = EntityState.Deleted;
        //        }

        //        var fids = db.t_sys_menumodule.Where(w => arr.Contains(w.Code)).Select(s => s.FID).ToList();

        //        List<t_sys_rights_detail> list = new List<t_sys_rights_detail>();

        //        foreach (int item in fids)
        //        {
        //            var tempObj = new t_sys_rights_detail()
        //            {
        //                RightsID = RightsID,
        //                ModuleID = item,
        //                CorpID = UserSession.userInfo.CorpID,
        //                CreateTime = dt,
        //                CreateUserID = (int)UserSession.userInfo.UserID,
        //                UpdateTime = dt,
        //                UpdateUserID = (int)UserSession.userInfo.UserID
        //            };
        //            list.Add(tempObj);
        //        }

        //        int result = 0;
        //        for (int i = 0; i < list.Count(); i++)
        //        {
        //            if (list[i] == null)
        //                continue;
        //            db.Entry<t_sys_rights_detail>(list[i]).State = EntityState.Added;
        //        }

        //        //获取菜单对应按钮的ID数组
        //        List<long> listModuleButtonID = new List<long>();
        //        foreach (t_sys_modulebutton item in buttonArr)
        //        {
        //            listModuleButtonID.Add(item.ModButtonID);
        //        }

        //        //查出所有满足要求的数据
        //        var listModuleButton = db.t_sys_modulebutton.Where(w => listModuleButtonID.Contains(w.ModButtonID)).Select(s => s).ToList();

        //        foreach (t_sys_modulebutton item in listModuleButton)
        //        {
        //            item.UpdateTime = dt;
        //            item.UpdateUserID = (int)UserSession.userInfo.UserID;
        //            foreach (var _item in buttonArr)
        //            {
        //                if (_item.ModButtonID == item.ModButtonID)
        //                {
        //                    item.IsVisible = _item.IsVisible;
        //                }
        //            }
        //            db.Entry<t_sys_modulebutton>(item).State = EntityState.Modified;
        //        }

        //        result += db.SaveChanges();

        //        return Json(true, "保存成功！");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(false, "保存失败!" + ex.Message);
        //    }
        //}

        //public class getRightsCode
        //{
        //    public string[] arr { get; set; }
        //    public t_sys_modulebutton[] buttonArr { get; set; }
        //    public int RightsID { get; set; }
        //}
    }
}
