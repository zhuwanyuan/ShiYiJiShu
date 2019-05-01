using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class InfoModel
    {
        public Info Info { get; set; }
        public int ClassID { get; set; }
        public int ParentClassID { get; set; }
        public string ClassName { get; set; }
        public bool HasSubClass { get; set; }
    }
}