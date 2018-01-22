using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppDms.Models;

namespace WebAppDms.Controllers
{
    public static class AutoIncrement
    {

        public static int AutoIncrementResult(string Code, out string CodeResult)
        {
            DateTime dt = DateTime.Now;
            webDmsEntities db = new webDmsEntities();
            DBHelper<t_serial_number_detail> db_serial_detail = new DBHelper<t_serial_number_detail>();

            var CorpID = UserSession.userInfo.CorpID;
            var UserID = UserSession.userInfo.UserID;
            var serial_list = db.t_serial_number.Where(w => w.Code == Code).FirstOrDefault();
            int length = serial_list.EndingNumber.ToString().Length;
            long SerialID = serial_list.SerialID;

            var serial_list_detail = db.t_serial_number_detail.Where(w => w.CorpID == CorpID && w.SerialID == SerialID).FirstOrDefault();

            var result = 0;
            if (serial_list_detail == null)
            {
                serial_list_detail = new t_serial_number_detail()
                {
                    CorpID = CorpID,
                    SerialID = SerialID,
                    FirstNumber = serial_list.StartingNumber,
                    IncrementByNumber = serial_list.IncrementByNumber,
                    LastDateUsed = dt,
                    LastNumber = serial_list.EndingNumber,
                    NumberDate = dt,
                    WarningNumber = serial_list.WarningNumber,
                    LastNumberUsed = serial_list.IncrementByNumber,
                    NumberLength = serial_list.EndingNumber.ToString().Length
                };
                result = db_serial_detail.Add(serial_list_detail);
            }
            else
            {
                serial_list_detail.SDID = serial_list_detail.SDID;
                serial_list_detail.CorpID = serial_list_detail.CorpID;
                serial_list_detail.SerialID = serial_list_detail.SerialID;
                serial_list_detail.FirstNumber = serial_list.StartingNumber;
                serial_list_detail.IncrementByNumber = serial_list.IncrementByNumber;
                serial_list_detail.LastDateUsed = dt;
                serial_list_detail.LastNumber = serial_list.EndingNumber;
                serial_list_detail.NumberDate = dt;
                serial_list_detail.WarningNumber = serial_list.WarningNumber;
                serial_list_detail.LastNumberUsed = serial_list_detail.LastNumberUsed + serial_list.IncrementByNumber;
                serial_list_detail.NumberLength = serial_list.EndingNumber.ToString().Length;

                result = db_serial_detail.Update(serial_list_detail);
            }

            switch (serial_list.MaintainMethod)
            {
                case "0":
                    CodeResult = serial_list.Prefix + serial_list_detail.LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
                case "1":
                    CodeResult = serial_list.Prefix + dt.ToString("yyMM") + serial_list_detail.LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
                case "2":
                    CodeResult = serial_list.Prefix + dt.ToString("yyMMdd") + serial_list_detail.LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
                default:
                    CodeResult = serial_list.Prefix + serial_list_detail.LastNumberUsed.ToString().PadLeft(length, '0');
                    break;
            }

            return result;
        }
    }
}