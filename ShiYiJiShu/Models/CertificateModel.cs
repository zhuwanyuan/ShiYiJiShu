using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiYiJiShu.Models
{
    public class CertificateModel
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
        public int? ClassID { get; set; }
        public int? ParentClassID { get; set; }
    }
}