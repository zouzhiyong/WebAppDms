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
    
    public partial class view_user
    {
        public long UserID { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public Nullable<int> DeptID { get; set; }
        public string DeptName { get; set; }
        public string RightsName { get; set; }
        public string Phone { get; set; }
        public long IsValid { get; set; }
        public long IsLockedout { get; set; }
        public string EmpCategoryName { get; set; }
        public Nullable<System.DateTime> LastActiveTIme { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public int CorpID { get; set; }
        public int isRole { get; set; }
        public string UpdateUser { get; set; }
    }
}
