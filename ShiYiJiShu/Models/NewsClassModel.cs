using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class NewsClassModel
    {
        public int CurrentClassID { get; set; }
        public IEnumerable<NewsClass> NewsClasses { get; set; }
    }

    public class DoctorClassListModel
    {
        public int CurrentClassID { get; set; }
        public string ControllerName { get; set; }
        public List<DoctorClassModel> DoctorClassList { get; set; }
    }

    public class DoctorClassModel
    {
        public NewsClass BigClass { get; set; }
        public List<NewsClass> SmallClassList { get; set; }
    }
}