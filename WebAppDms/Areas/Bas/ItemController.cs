using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;
using WebAppDms.Controllers;
using WebAppDms.Models;

namespace WebAppDms.Areas.Bas
{
    public class ItemController : ApiBaseController
    {
        string VirtualPath = ConfigurationManager.AppSettings["VirtualPath"].ToString();
        string UploadImgPath = ConfigurationManager.AppSettings["UploadImgPath"].ToString();

        public HttpResponseMessage FindBasItemTree()
        {
            var list = db.t_item_group.Where(w => w.IsValid != 0 && (w.CorpID == userInfo.CorpID || w.CorpID == 0)).Select(s => new
            {
                label = s.Name,
                value = s.ItemGroupID,
                ItemGroupID = s.ItemGroupID,
                ItemCategoryID = 0,
                children = db.t_item_category.Where(w => w.IsValid != 0 && (w.CorpID == userInfo.CorpID || w.CorpID == 0) && w.ItemGroupID == s.ItemGroupID).Select(s1 => new
                {
                    label = s1.Name,
                    value = s1.ItemCategoryID,
                    ItemGroupID = s1.ItemGroupID,
                    ItemCategoryID = s1.ItemCategoryID
                }).ToList()
            }).ToList();

            return Json(true, "", list);
        }

        public HttpResponseMessage FindBasItemTable(dynamic obj)
        {
            DBHelper<view_item> dbhelp = new DBHelper<view_item>();

            //string Name = obj.Name;
            int pageSize = obj.pageSize;
            int currentPage = obj.currentPage;
            int total = 0;
            int ItemGroupID = obj.ItemGroupID;
            int ItemCategoryID = obj.ItemCategoryID;

            //var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => (x.Name.Contains(Name) && x.ItemGroupID== ItemGroupID && x.ItemCategoryID== ItemCategoryID && x.CorpID == userInfo.CorpID), s => s.ItemID, true);

            var list = dbhelp.FindPagedList(currentPage, pageSize, out total, x => (ItemGroupID == 0 || x.ItemGroupID == ItemGroupID) && (ItemCategoryID == 0 || x.ItemCategoryID == ItemCategoryID) && x.CorpID == userInfo.CorpID, s => s.ItemID, true);


            return Json(list, currentPage, pageSize, total);
        }

        public HttpResponseMessage DeleteBasItemRow(t_item obj)
        {
            var result = new DBHelper<t_item>().Remove(obj);

            return Json(true, result == 1 ? "删除成功！" : "删除失败");
        }

        public HttpResponseMessage FindBasItemForm(t_item obj)
        {
            long ItemID = obj.ItemID;

            var UOMList = db.t_bas_unitofmeasure.Where(w => (w.CorpID == userInfo.CorpID || w.CorpID == 0) && w.IsValid != 0).Select(s => new
            {
                label = s.Name,
                value = s.UnitID
            });

            var ItemGroupIDList = db.t_item_group.Where(w => w.IsValid != 0 && (w.CorpID == userInfo.CorpID || w.CorpID == 0)).Select(s => new
            {
                label = s.Name,
                value = s.ItemGroupID
            });

            var ItemCategoryIDList = db.t_item_category.Where(w => w.IsValid != 0 && (w.CorpID == userInfo.CorpID || w.CorpID == 0)).Select(s => new
            {
                label = s.Name,
                value = s.ItemCategoryID
            });

            var WarehouseIDList = db.t_warehouse.Where(w => w.IsValid != 0 && w.CorpID == userInfo.CorpID).
                Join(db.t_user_warehouse.Where(w => w.IsValid != 0 && w.CorpID == userInfo.CorpID && w.UserID == userInfo.UserID), a => a.WarehouseID, b => b.WarehouseID, (a, b) => new
                {
                    label = a.Name,
                    value = a.WarehouseID
                });

            string NullValue = null;

            if (ItemID == 0)
            {
                var list = new
                {
                    CorpID = userInfo.CorpID,
                    Code = NullValue,
                    ItemID = 0,
                    ItemGroupID = NullValue,
                    ItemGroupIDList = ItemGroupIDList,
                    ItemCategoryID = NullValue,
                    ItemCategoryIDList = ItemCategoryIDList,
                    Photo = new
                    {
                        IsTitle = NullValue,
                        PicID = NullValue,
                        Type = NullValue,
                        IsValid = 1,
                        Picture = NullValue
                    },
                    Name = NullValue,
                    HelperCode = NullValue,
                    Barcode = NullValue,
                    BaseUOM = NullValue,
                    UOMList = UOMList,
                    CrossWeigth = NullValue,
                    Height = NullValue,
                    IsBatch = 0,
                    IsForSale = 1,
                    IsZeroValue = 0,
                    LastCost = NullValue,
                    Length = NullValue,
                    Period = NullValue,
                    PurchasePrice = NullValue,
                    PurchaseUOM = NullValue,
                    SalesPrice = NullValue,
                    SalesUOM = NullValue,
                    SaveInventory = NullValue,
                    ShortName = NullValue,
                    Size = NullValue,
                    IsValid = 1,
                    WarehouseID = NullValue,
                    WarehouseIDList = WarehouseIDList,
                    Width = NullValue,
                    CloseTime = NullValue,
                    CloseUserID = NullValue,
                    CreateTime = NullValue,
                    CreateUserID = NullValue,
                    UpdateTime = NullValue,
                    UpdateUserID = NullValue
                };

                return Json(true, "", list);
            }
            else
            {
                var list = db.t_item.Where(w => w.ItemID == ItemID && w.CorpID == userInfo.CorpID).Select(s => new
                {
                    s.CorpID,
                    s.Code,
                    s.ItemID,
                    s.ItemGroupID,
                    ItemGroupIDList = ItemGroupIDList,
                    s.ItemCategoryID,
                    ItemCategoryIDList = ItemCategoryIDList,
                    Photo = db.t_item_picture.Where(w => w.CorpID == userInfo.CorpID && w.ItemID == s.ItemID).Select(s1 => new
                    {
                        s1.IsTitle,
                        s1.PicID,
                        s1.Type,
                        s1.IsValid,
                        s1.Picture
                    }).FirstOrDefault(),
                    s.Name,
                    s.HelperCode,
                    s.Barcode,
                    s.BaseUOM,
                    UOMList = UOMList,
                    s.CrossWeigth,
                    s.Height,
                    s.IsBatch,
                    s.IsForSale,
                    s.IsZeroValue,
                    s.LastCost,
                    s.Length,
                    s.Period,
                    s.PurchasePrice,
                    s.PurchaseUOM,
                    s.SalesPrice,
                    s.SalesUOM,
                    s.SaveInventory,
                    s.ShortName,
                    s.Size,
                    s.IsValid,
                    s.WarehouseID,
                    WarehouseIDList = WarehouseIDList,
                    s.Width,
                    s.CloseTime,
                    s.CloseUserID,
                    s.CreateTime,
                    s.CreateUserID,
                    s.UpdateTime,
                    s.UpdateUserID
                }).FirstOrDefault();



                return Json(true, "", list);
            }
        }

        public HttpResponseMessage FindBasItemGroupCategoryForm(t_item_category obj)
        {
            var list = db.t_item_category.Where(w => w.IsValid != 0 && w.CorpID == userInfo.CorpID && w.ItemGroupID == obj.ItemGroupID).Select(s => new
            {
                label = s.Name,
                value = s.ItemCategoryID
            }).ToList();

            return Json(true, "", list);
        }

        public HttpResponseMessage SaveBasItemForm(t_item_photo obj)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DateTime dt = DateTime.Now;

                t_item objItem = new t_item()
                {
                    CorpID = userInfo.CorpID,
                    Code = obj.Code,
                    ItemID = obj.ItemID,
                    ItemGroupID = obj.ItemGroupID,
                    ItemCategoryID = obj.ItemCategoryID,
                    Name = obj.Name,
                    HelperCode = obj.HelperCode,
                    Barcode = obj.Barcode,
                    BaseUOM = obj.BaseUOM,
                    CrossWeigth = obj.CrossWeigth,
                    Height = obj.Height,
                    IsBatch = obj.IsBatch,
                    IsForSale = obj.IsForSale,
                    IsZeroValue = obj.IsZeroValue,
                    LastCost = obj.LastCost,
                    Length = obj.Length,
                    Period = obj.Period,
                    PurchasePrice = obj.PurchasePrice,
                    PurchaseUOM = obj.PurchaseUOM,
                    SalesPrice = obj.SalesPrice,
                    SalesUOM = obj.SalesUOM,
                    SaveInventory = obj.SaveInventory,
                    ShortName = obj.ShortName,
                    Size = obj.Size,
                    IsValid = obj.IsValid,
                    WarehouseID = obj.WarehouseID,
                    Width = obj.Width,
                    CloseTime = obj.CloseTime,
                    CloseUserID = obj.CloseUserID,
                    CreateTime = obj.CreateTime,
                    CreateUserID = obj.CreateUserID,
                    UpdateTime = obj.UpdateTime,
                    UpdateUserID = obj.UpdateUserID
                };                

                DBHelper<t_item> dbhelp_item = new DBHelper<t_item>();                

                //事务
                var result = 0;
                var Item = db.t_item.Where(w => w.Code == objItem.Code && w.CorpID == userInfo.CorpID);
                try
                {
                    if (objItem.ItemID == 0)
                    {
                        string Code = objItem.Code;
                        if (Code == "" || Code==null)
                        {
                            result = AutoIncrement.AutoIncrementResult("Item", out Code);
                        }

                        objItem.CreateTime = dt;
                        objItem.CreateUserID = (int)userInfo.UserID;
                        objItem.CorpID = userInfo.CorpID;
                        objItem.Code = Code;
                        if (Item.ToList().Count() > 0)
                        {
                            throw new Exception("账号重复！");
                        }
                        result = result + dbhelp_item.Add(objItem);
                    }
                    else
                    {
                        objItem.UpdateTime = dt;
                        objItem.UpdateUserID = (int)userInfo.UserID;
                        if (Item.ToList().Count() > 1)
                        {
                            throw new Exception("账号重复！");
                        }
                        result = result + dbhelp_item.Update(objItem);
                    }


                    //保存图片并修改数据库图片名称 
                    t_item_picture objItemPicture = new t_item_picture()
                    {
                        PicID = obj.Photo.PicID,
                        CorpID = userInfo.CorpID,
                        ItemID = (int)objItem.ItemID,
                        Picture = obj.Photo.Picture,
                        Type = obj.Photo.Type,
                        IsTitle = obj.Photo.IsTitle,
                        IsValid = obj.Photo.IsValid,
                        CreateTime = dt,
                        CreateUserID = (int)userInfo.UserID,
                    };
                    DBHelper<t_item_picture> dbhelp_picture = new DBHelper<t_item_picture>();
                    string base64Data = obj.Photo.Picture;
                    try
                    {
                        //获取文件储存路径            
                        string suffix = base64Data.Split(new char[] { ';' })[0].Substring(base64Data.IndexOf('/') + 1);//获取后缀名
                        string newFileName = "M_" + obj.ItemID.ToString("000000000") + "." + suffix;
                        string strPath = HttpContext.Current.Server.MapPath("~/" + UploadImgPath + "/" + newFileName); //获取当前项目所在目录 
                        //获取图片并保存
                        BaseToImg.Base64ToImg(base64Data.Split(',')[1]).Save(strPath);
                        objItemPicture.Picture = newFileName;
                    }
                    catch
                    {
                        objItemPicture.Picture = base64Data;
                    }

                    result = result + dbhelp_picture.RemoveList(w => w.ItemID == objItem.ItemID && w.Type == 0);
                    result = result + dbhelp_picture.Add(objItemPicture);

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

        public class t_item_photo : t_item
        {
            public t_item_picture Photo { get; set; }
        }
    }
}
