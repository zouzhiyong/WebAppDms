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
    
    public partial class t_bas_user
    {
        public System.DateTime TimeStamp { get; set; }
        public long UserID { get; set; }
        public int CorpID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int UserCategoryID { get; set; }
        public Nullable<int> PositionID { get; set; }
        public Nullable<int> RightsID { get; set; }
        public int DeptID { get; set; }
        public Nullable<int> ParentEmpID { get; set; }
        public Nullable<int> CertificateID { get; set; }
        public string CertificateNumber { get; set; }
        public string Password { get; set; }
        public int FailedPasswordCount { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string WeiXin { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public long IsUseSystem { get; set; }
        public long IsAppUser { get; set; }
        public long IsValid { get; set; }
        public long IsLockedout { get; set; }
        public Nullable<System.DateTime> LockedoutTime { get; set; }
        public Nullable<int> LockedoutUserID { get; set; }
        public Nullable<System.DateTime> LastActiveTIme { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastChangePwdDate { get; set; }
        public string SessionID { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CreateUserID { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> UpdateUserID { get; set; }
        public string IMEICode { get; set; }
    }
}
