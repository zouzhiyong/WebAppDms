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
    
    public partial class t_purchase_invoice_detail
    {
        public System.DateTime TimeStamp { get; set; }
        public int CorpID { get; set; }
        public int InvoiceID { get; set; }
        public int RowID { get; set; }
        public int ItemID { get; set; }
        public int UomID { get; set; }
        public int WarehouseID { get; set; }
        public Nullable<int> BinID { get; set; }
        public float InvoiceQty { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal UnitCost { get; set; }
        public string Remark { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public int UpdateUserID { get; set; }
    }
}
