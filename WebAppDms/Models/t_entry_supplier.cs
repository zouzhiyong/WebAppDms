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
    
    public partial class t_entry_supplier
    {
        public System.DateTime TimeStamp { get; set; }
        public int SupplierEntryID { get; set; }
        public int CorpID { get; set; }
        public string CustEntryCode { get; set; }
        public int BillID { get; set; }
        public System.DateTime BillDate { get; set; }
        public System.DateTime PostDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public int BillTypeID { get; set; }
        public int SupplierID { get; set; }
        public int WarehouseID { get; set; }
        public Nullable<int> BinID { get; set; }
        public int ItemID { get; set; }
        public string Batch { get; set; }
        public int UomID { get; set; }
        public float BillQty { get; set; }
        public float OperQty { get; set; }
        public float BalanceQty { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal OperAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal UnitCost { get; set; }
        public long Status { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> CreateUserID { get; set; }
    }
}