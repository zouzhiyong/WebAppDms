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
    
    public partial class t_serial_number_detail
    {
        public System.DateTime TimeStamp { get; set; }
        public long SDID { get; set; }
        public int CorpID { get; set; }
        public long SerialID { get; set; }
        public System.DateTime NumberDate { get; set; }
        public int NumberLength { get; set; }
        public int FirstNumber { get; set; }
        public long LastNumber { get; set; }
        public Nullable<int> WarningNumber { get; set; }
        public int IncrementByNumber { get; set; }
        public Nullable<System.DateTime> LastDateUsed { get; set; }
        public Nullable<int> LastNumberUsed { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> UpdateUserID { get; set; }
    }
}
