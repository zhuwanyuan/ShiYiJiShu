using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class VoteListModel
    {
        public IEnumerable<VoteStaff> StaffList { get; set; }
        public string PageLink { get; set; }
        public int ClassID { get; set; }
        public int ParentClassID { get; set; }
        public string ClassName { get; set; }

        public int TopClassID { get; set; }
        public string TopClassName { get; set; }
    }

    public class VoteDetailModel
    {
        public VoteStaff VoteStaff { get; set; }
        public int ClassID { get; set; }
        public int ParentClassID { get; set; }
        public string ClassName { get; set; }
    }
}