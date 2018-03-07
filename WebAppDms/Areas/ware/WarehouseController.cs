using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppDms.Controllers;

namespace WebAppDms.Areas.ware
{
    public class WareHouseReceiptController : ApiBaseController
    {
        public HttpResponseMessage FindWarehouseList()
        {
            var list = db.t_warehouse.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0).Join(db.t_user_warehouse.Where(w => w.CorpID == userInfo.CorpID && w.IsValid != 0 && w.UserID == (int)userInfo.UserID),a=>a.WarehouseID,b=>b.WarehouseID,(a,b)=>new 
            {
                value = a.WarehouseID,
                label = a.Name
            });

            return Json(true, "", list);
        }
    }
}
