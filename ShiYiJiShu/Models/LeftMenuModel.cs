using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class ShiYiJiShuLeftMenuModel
    {   
        public int CurrentClassID { get; set; }
        public IEnumerable<SecondClassListModel> ClassList { get; set; }
    }

    public class SecondClassListModel
    {
        public IEnumerable<NewsClass> SecondClassList { get; set; }
    }
}