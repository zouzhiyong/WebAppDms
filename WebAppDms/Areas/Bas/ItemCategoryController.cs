using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class ItemCategoryController : ApiBaseController
    {
        public HttpResponseMessage FindBasItemCategoryTable(dynamic obj)
        {
            DBHelper<view_item_category> dbhelp = new DBHelper<view_item_category>();

            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;
            long CorpID = (long)userInfo.CorpID;
            //int ItemGroupID = obj.ItemGroupID;
            //int ItemCategoryID = obj.ItemCategoryID;
            string Name = obj.Name;
            int ItemType = obj.ItemType;

            //var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.Name.Contains(Name) && x.ItemType== ItemType && x.CorpID == CorpID && (ItemGroupID == 0 || x.ItemGroupID == ItemGroupID) && (ItemCategoryID == 0 || x.ItemCategoryID == ItemCategoryID), s => s.Sequence, true);
            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => x.Name.Contains(Name) && x.ItemType == ItemType && (x.CorpID == CorpID || x.CorpID==0), s => s.Sequence, true);

            return Json(list, currentPage, pageSize, total);
        }

        [HttpPost]
        public HttpResponseMessage DeleteBasItemCategoryRow(dynamic obj)
        {
            int ItemType = (int)obj.ItemType;

            if (ItemType == 2)
            {
                t_item_group itemGroup = new t_item_group()
                {
                    ItemGroupID = obj.ItemGroupID
                };

                var result = new DBHelper<t_item_group>().Remove(itemGroup);
                return Json(true, result == 1 ? "删除成功！" : "删除失败");
            }else
            {
                t_item_category itemCategory = new t_item_category()
                {
                    ItemCategoryID = obj.ItemCategoryID
                };

                var result = new DBHelper<t_item_category>().Remove(itemCategory);
                return Json(true, result == 1 ? "删除成功！" : "删除失败");
            }
            
        }

        public HttpResponseMessage FindBasItemGroupForm(dynamic obj)
        {
            long ItemGroupID = obj.ItemGroupID;

            if (ItemGroupID == 0)
            {
                var list = new
                {
                    Code = "",
                    CorpID = userInfo.CorpID,
                    CreateTime = "",
                    CreateUser = "",
                    IsSystem = 0,
                    IsValid = 1,
                    ItemGroupID = 0,
                    Name = "",
                    Remark = "",
                    Sequence = "",
                    UPdateTime = "",
                    UpdateUser = ""
                };

                return Json(true, "", list);
            }
            else
            {
                var list = db.t_item_group.Where(w => w.ItemGroupID == ItemGroupID && w.CorpID == userInfo.CorpID).Select(s => new
                {
                    s.Code,
                    s.CorpID,
                    s.CreateTime,
                    s.CreateUser,
                    s.IsSystem,
                    s.IsValid,
                    s.ItemGroupID,
                    s.Name,
                    s.Remark,
                    s.Sequence,
                    s.UPdateTime,
                    s.UpdateUser
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage FindBasItemCategoryForm(dynamic obj)
        {
            long ItemGroupID = obj.ItemGroupID;
            long ItemCategoryID = obj.ItemCategoryID;

            var ItemGroupIDList = db.t_item_group.Where(w => w.IsValid != 0 && (w.CorpID == userInfo.CorpID || w.CorpID==0) ).Select(s => new
            {
                label = s.Name,
                value = s.ItemGroupID
            });


            if (ItemCategoryID == 0)
            {
                var list = new
                {
                    Code = "",
                    CorpID = userInfo.CorpID,
                    CreateTime = "",
                    CreateUser = "",
                    IsSystem = 0,
                    IsValid = 1,
                    ItemCategoryID = 0,
                    ItemGroupID = 0,
                    ItemGroupIDList = ItemGroupIDList,
                    Name = "",
                    Remark = "",
                    Sequence = "",
                    UPdateTime = "",
                    UpdateUser = ""
                };

                return Json(true, "", list);
            }
            else
            {
                var list = db.t_item_category.Where(w => w.ItemCategoryID == ItemCategoryID && w.CorpID == userInfo.CorpID).Select(s => new
                {
                    s.Code,
                    s.CorpID,
                    s.CreateTime,
                    s.CreateUser,
                    s.IsSystem,
                    s.IsValid,
                    s.ItemCategoryID,
                    s.ItemGroupID,
                    ItemGroupIDList = ItemGroupIDList,
                    s.Name,
                    s.Remark,
                    s.Sequence,
                    s.UPdateTime,
                    s.UpdateUser
                }).FirstOrDefault();

                return Json(true, "", list);
            }
        }

        public HttpResponseMessage SaveBasItemGroupForm(t_item_group obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_item_group> dbhelp = new DBHelper<t_item_group>();
                DateTime dt = DateTime.Now;

                //事务
                var result = 0;
                var ItemGroupID = db.t_item_group.Where(w => w.Code == obj.Code && w.CorpID == userInfo.CorpID);
                try
                {
                    if (obj.ItemGroupID == 0)
                    {
                        string Code = "";
                        result = AutoIncrement.AutoIncrementResult("ItemGroup", out Code);
                        obj.CreateTime = dt;
                        obj.CreateUser = (int)userInfo.UserID;
                        obj.UPdateTime = dt;
                        obj.UpdateUser = (int)userInfo.UserID;
                        obj.CorpID = userInfo.CorpID;
                        obj.Code = Code;

                        if (ItemGroupID.ToList().Count() > 0)
                        {
                            throw new Exception("编码重复！");
                        }                        
                    }
                    else
                    {
                        obj.UPdateTime = dt;
                        obj.UpdateUser = (int)userInfo.UserID;

                        if (ItemGroupID.ToList().Count() > 1)
                        {
                            throw new Exception("编码重复！");
                        }                        
                    }

                    result = result + (obj.ItemGroupID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj));


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

        public HttpResponseMessage SaveBasItemCategoryForm(t_item_category obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DBHelper<t_item_category> dbhelp = new DBHelper<t_item_category>();
                DateTime dt = DateTime.Now;

                //事务
                var result = 0;
                var ItemCategory = db.t_item_category.Where(w => w.Code == obj.Code && w.CorpID == userInfo.CorpID);
                try
                {
                    if (obj.ItemCategoryID == 0)
                    {
                        string Code = "";
                        result = AutoIncrement.AutoIncrementResult("ItemCategory", out Code);
                        obj.CreateTime = dt;
                        obj.CreateUser = (int)userInfo.UserID;
                        obj.UPdateTime = dt;
                        obj.UpdateUser = (int)userInfo.UserID;
                        obj.CorpID = userInfo.CorpID;
                        obj.Code = Code;

                        if (ItemCategory.ToList().Count() > 0)
                        {
                            throw new Exception("编码重复！");
                        }
                    }
                    else
                    {
                        obj.UPdateTime = dt;
                        obj.UpdateUser = (int)userInfo.UserID;

                        if (ItemCategory.ToList().Count() > 1)
                        {
                            throw new Exception("编码重复！");
                        }
                    }

                    result = result + (obj.ItemCategoryID == 0 ? dbhelp.Add(obj) : dbhelp.Update(obj));


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
        

    }
}
