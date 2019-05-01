using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShiYiJiShu.Model
{
    public partial class NewsModel
    {
        public int NewsID { get; set; }
        public string NewsTitle { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public Nullable<int> BigClassID { get; set; }
        public Nullable<int> SmallClassID { get; set; }
        public string NewsPic { get; set; }
        public string NewsContent { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string CreateBy { get; set; }
        public string Source { get; set; }
        public Nullable<int> HitCount { get; set; }
        public Nullable<int> ZhiDing { get; set; }
    }

    public class GroupNewsModel
    {
        public int id { get; set; }
        public IEnumerable<NewsModel> newsList { get; set; }
        public IEnumerable<NewsModel> picNews { get; set; }
    }
}
