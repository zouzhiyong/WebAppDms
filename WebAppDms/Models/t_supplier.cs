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
    
    public partial class t_supplier
    {
        public System.DateTime TimeStamp { get; set; }
        public long SupplierID { get; set; }
        public int CorpID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string HelperCode { get; set; }
        public int SupplierCategoryID { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public long IsValid { get; set; }
        public System.DateTime CreateTime { get; set; }
        public int CreateUserID { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Nullable<int> UpdateUserID { get; set; }
        public Nullable<System.DateTime> CloseTime { get; set; }
        public Nullable<int> CloseUserID { get; set; }
    }
}