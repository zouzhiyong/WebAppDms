//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppDms.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_entry_item
    {
        public System.DateTime TimeStamp { get; set; }
        public long InvEntryID { get; set; }
        public int CorpID { get; set; }
        public string InvEntryCode { get; set; }
        public Nullable<int> BillID { get; set; }
        public System.DateTime BillDate { get; set; }
        public System.DateTime PostDate { get; set; }
        public int BillTypeID { get; set; }
        public int WarehouseID { get; set; }
        public Nullable<int> BinID { get; set; }
        public int ItemID { get; set; }
        public string Batch { get; set; }
        public int UomID { get; set; }
        public int BillQty { get; set; }
        public int OperQty { get; set; }
        public Nullable<int> BalanceQty { get; set; }
        public string UnitAmount { get; set; }
        public string Amount { get; set; }
        public decimal UnitCost { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public long Status { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> ProduceDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
    }
}
