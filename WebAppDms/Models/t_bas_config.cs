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
    
    public partial class t_bas_config
    {
        public System.DateTime TimeStamp { get; set; }
        public int CorpID { get; set; }
        public string Code { get; set; }
        public string Descript { get; set; }
        public string ParaType { get; set; }
        public string Value { get; set; }
        public int Sequence { get; set; }
        public long OperationType { get; set; }
        public string Remark { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
