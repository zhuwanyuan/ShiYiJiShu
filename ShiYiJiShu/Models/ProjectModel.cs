using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class ProjectListModel
    {
        public IEnumerable<Project> ProjectList { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }

        public int TopClassID { get; set; }
        public string TopClassName { get; set; }
        public string PageLink { get; set; }
    }

    public class ProjectDetailModel
    {
        public Project Project { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }

        public int TopClassID { get; set; }
        public string TopClassName { get; set; }
    }
}