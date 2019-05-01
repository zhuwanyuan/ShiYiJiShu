using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class VideoListModel
    {
        public IEnumerable<Video> VideoList { get; set; }
        
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int TopClassID { get; set; }
        public string TopClassName { get; set; }
        public string PageLink { get; set; }
    }

    public class VideoDetailModel
    {
        public Video Video { get; set; }
        
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int TopClassID { get; set; }
        public string TopClassName { get; set; }
    }
}