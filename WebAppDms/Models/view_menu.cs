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
    
    public partial class view_menu
    {
        public long UserID { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public long FID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public long IsMenu { get; set; }
        public int PlatformType { get; set; }
        public int Level { get; set; }
        public int Sequence { get; set; }
        public string URL { get; set; }
        public string ICON { get; set; }
        public long IsValid { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> UpdateUserID { get; set; }
        public string ControllerName { get; set; }
    }
}
