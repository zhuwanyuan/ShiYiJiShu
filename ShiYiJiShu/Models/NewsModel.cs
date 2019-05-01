using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class NewsListModel
    {
        public IEnumerable<News> List { get; set; }
        public IEnumerable<News> PaiHang { get; set; }
        public string ClassName { get; set; }
        public int ClassID { get; set; }
        public int ParentClassID { get; set; }
        public int TopClassID { get; set; }
        public string PageLink { get; set; }
        public bool HasSubClass { get; set; }
    }

    public class NewsDetailModel
    {
        public News News { get; set; }
        public string ClassName { get; set; }
        public int ClassID { get; set; }
        public int ParentClassID { get; set; }
    }
}