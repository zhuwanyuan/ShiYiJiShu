using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class NewsMenuModel
    {   
        public int CurrentClassID { get; set; }
        public string TopClassName { get; set; }
        public string RouteName { get; set; }
 
        public IEnumerable<NewsSubMenuModel> ClassList { get; set; }
    }

    public class NewsSubMenuModel
    {  
        public NewsClass NewsClass { get; set; }
        public IEnumerable<NewsClass> SubNewsClassList { get; set; }
    }
}