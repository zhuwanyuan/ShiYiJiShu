using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class ShiYiJiShuMenuModel
    {
        public int CurrentClassID { get; set; }
        public IEnumerable<NewsClass> GongZuoJieShao { get; set; }
        public IEnumerable<NewsClass> RuXuanJiShu { get; set; }
    }
 
}