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
    
    public partial class Video
    {
        public int VideoID { get; set; }
        public string VideoName { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string VideoIntro { get; set; }
        public string VideoFilePath { get; set; }
        public string VideoUrl { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<int> ZhiDing { get; set; }
        public Nullable<int> HitCount { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ActiveFlag { get; set; }
        public Nullable<int> FirstClassID { get; set; }
        public Nullable<int> SecondClassID { get; set; }
        public string VideoUploadUrl { get; set; }
    }
}
