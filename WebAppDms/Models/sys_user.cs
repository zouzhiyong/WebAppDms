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
    
    public partial class sys_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sys_user()
        {
            this.sys_userrole = new HashSet<sys_userrole>();
        }
    
        public int UserID { get; set; }
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public Nullable<int> DeptID { get; set; }
        public string RealName { get; set; }
        public string Phone { get; set; }
        public int UserTypeID { get; set; }
        public System.DateTime BirthDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public string Email { get; set; }
        public string IMEICode { get; set; }
        public Nullable<int> IsValid { get; set; }
        public string WorkingPlace { get; set; }
        public string Avatar { get; set; }
        public string Comment { get; set; }
        public string SessionId { get; set; }
        public Nullable<System.DateTime> RecTimeStamp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sys_userrole> sys_userrole { get; set; }
    }
}
