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
    
    public partial class Technology
    {
        public int TechID { get; set; }
        public string TechName { get; set; }
        public string TechNameLink { get; set; }
        public string TechCompany { get; set; }
        public string TechCompanyLink { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> ZhiDing { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
