using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class TechListModel
    {
        public IEnumerable<Technology> techs { get; set; }
        public string ClassName { get; set; }
 
        public string PageLink { get; set; }
    }

    public class TechInfoModel
    {
        public Info Info { get; set; }
        public string ClassName { get; set; }
   
    }

    public class TechMenuModel
    {
        public IEnumerable<NewsClass> GongZuoJieShao { get; set; }
        public IEnumerable<NewsClass> RuXuanJiShu { get; set; }
    }
}