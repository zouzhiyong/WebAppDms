using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppDms.Models;

namespace WebAppDms.Controllers
{
    public static class AutoIncrement
    {
        
        public static int AutoIncrementResult(string Code,out string CodeResult)
        {
            webDmsEntities db = new webDmsEntities();

            DBHelper<t_bas_serial_number> dbhelp = new DBHelper<t_bas_serial_number>();

            var CorpID = UserSession.userInfo.CorpID;
            var list = db.t_bas_serial_number.Where(w => w.CorpID == CorpID && w.Code == Code).FirstOrDefault();
            int length = list.EndingNumber.ToString().Length;
            int LastNumberUsed = Convert.ToInt16(list.LastNumberUsed.Substring(list.LastNumberUsed.Length - length, length));
            CodeResult = list.Prefix + DateTime.Now.ToString("yyyyMMdd") + (LastNumberUsed + 1).ToString().PadLeft(length, '0');
            list.LastNumberUsed = CodeResult;

            var result = 0;

            List<string> strList = new List<string>();
            strList.Add("LastNumberUsed");
            result = dbhelp.UpdateEntityFields(list, strList);

            return result;
        }
    }
}