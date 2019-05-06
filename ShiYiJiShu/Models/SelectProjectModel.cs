using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class SelectProjectListModel
    {
        public IEnumerable<SelectProject> SelectProjectList { get; set; }
 
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int TopClassID { get; set; }
        public string TopClassName { get; set; }
        public string PageLink { get; set; }

        public string ProvinceID { get; set; }
    }

    public class SelectProjectDetailModel
    {
        public SelectProject SelectProject { get; set; }
        
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int TopClassID { get; set; }
        public string TopClassName { get; set; }
    }
}