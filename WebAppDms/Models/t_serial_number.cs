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
    
    public partial class t_serial_number
    {
        public System.DateTime TimeStamp { get; set; }
        public long SerialID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IsManual { get; set; }
        public string MaintainMethod { get; set; }
        public string Prefix { get; set; }
        public int StartingNumber { get; set; }
        public long EndingNumber { get; set; }
        public Nullable<int> WarningNumber { get; set; }
        public int IncrementByNumber { get; set; }
        public Nullable<int> YearLength { get; set; }
        public Nullable<long> IsValid { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> CreateUserID { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> UpdateUserID { get; set; }
    }
}
