using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class AboutLeftMenuModel   
    {   
        public int CurrentClassID { get; set; }
        public IEnumerable<NewsClass> SecondClasses { get; set; }
        public IEnumerable<NewsClass> ThirdClasses { get; set; }
    }
}