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
    
    public partial class t_stock_order
    {
        public System.DateTime TimeStamp { get; set; }
        public int CorpID { get; set; }
        public string Code { get; set; }
        public int RowID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int StockOrderTypeID { get; set; }
        public int WarehouseID { get; set; }
        public Nullable<int> BinID { get; set; }
        public int ItemID { get; set; }
        public string Batch { get; set; }
        public int UomID { get; set; }
        public float Qty { get; set; }
        public decimal UnitCost { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CreateUserID { get; set; }
        public long Status { get; set; }
        public string Remark { get; set; }
    }
}