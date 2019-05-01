using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class VideoMenuModel
    {
        public int CurrentClassID { get; set; }
        public IEnumerable<NewsClass> VideoSubClass { get; set; }
         
    }
 
}