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
    
    public partial class view_item
    {
        public string Code { get; set; }
        public int CorpID { get; set; }
        public string Barcode { get; set; }
        public int BaseUOM { get; set; }
        public string BaseUOMName { get; set; }
        public Nullable<System.DateTime> CloseTime { get; set; }
        public Nullable<int> CloseUserID { get; set; }
        public string CloseUserName { get; set; }
        public string UpdateUserName { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CreateUserID { get; set; }
        public long IsBatch { get; set; }
        public long IsValid { get; set; }
        public int ItemGroupID { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCategoryName { get; set; }
        public Nullable<int> ItemCategoryID { get; set; }
        public string Name { get; set; }
        public long ItemID { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> UpdateUserID { get; set; }
    }
}
