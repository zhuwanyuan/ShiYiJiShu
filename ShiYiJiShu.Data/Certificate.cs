//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShiYiJiShu.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Certificate
    {
        public int CertID { get; set; }
        public string Name { get; set; }
        public string SearchType { get; set; }
        public string Picture { get; set; }
        public string IDNumber { get; set; }
        public string CertNo { get; set; }
        public string JobTitle { get; set; }
        public string CertSample { get; set; }
        public Nullable<System.DateTime> AddDateTime { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ActiveFlag { get; set; }
    }
}
