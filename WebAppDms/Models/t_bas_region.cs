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
    
    public partial class t_bas_region
    {
        public System.DateTime TimeStamp { get; set; }
        public long RegionID { get; set; }
        public int CorpID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public int Sequence { get; set; }
        public long IsValid { get; set; }
        public string Remark { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public int UpdateUserID { get; set; }
        public string RegionAllID { get; set; }
        public string NameAll { get; set; }
    }
}
