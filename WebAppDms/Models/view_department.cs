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
    
    public partial class view_department
    {
        public System.DateTime TimeStamp { get; set; }
        public int DeptID { get; set; }
        public int CorpID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public Nullable<int> Level { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<int> IsValid { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CreateUserID { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public int UpdateUserID { get; set; }
        public Nullable<long> isRole { get; set; }
    }
}