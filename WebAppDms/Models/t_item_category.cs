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
    
    public partial class t_item_category
    {
        public System.DateTime Timestamp { get; set; }
        public int ItemCategoryID { get; set; }
        public int CorpID { get; set; }
        public int ItemGroupID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long IsValid { get; set; }
        public long IsSystem { get; set; }
        public int Sequence { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public Nullable<System.DateTime> UPdateTime { get; set; }
    }
}
