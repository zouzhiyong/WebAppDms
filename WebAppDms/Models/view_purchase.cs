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
    
    public partial class view_purchase
    {
        public int POID { get; set; }
        public int CorpID { get; set; }
        public string Code { get; set; }
        public int BillType { get; set; }
        public Nullable<long> SupplierID { get; set; }
        public string UpdateUserName { get; set; }
        public string SupplierName { get; set; }
        public Nullable<long> Status { get; set; }
        public string StatusName { get; set; }
        public System.DateTime BillDate { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public int isRole { get; set; }
    }
}
