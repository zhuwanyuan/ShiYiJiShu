using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShiYiJiShu.Data;

namespace ShiYiJiShu.Models
{
    public class DoctorIndexModel
    {
        public IEnumerable<Doctor> DoctorList { get; set; }
        public IEnumerable<Doctor> ShouXiDoctorList { get; set; }
        public IEnumerable<Doctor> TeYaoDoctorList { get; set; }
        public IEnumerable<Province> ProvinceList { get; set; }
        public string ClassName { get; set; }
        public int ClassID { get; set; }
        public string ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public string PageLink { get; set; }
 
    }

    public class DoctorListModel
    {

        public IEnumerable<Doctor> DoctorList { get; set; }

        public string ClassName { get; set; }

        public string ParentClassName { get; set; }

        public int ClassID { get; set; }
 

        public string PageLink { get; set; }
    }

    public class DoctorDetailModel
    {
        public Doctor Doctor { get; set; }
        public string ClassName { get; set; }
        public string ParentClassName { get; set; }
        public int ClassID { get; set; }
        public IEnumerable<Province> ProvinceList { get; set; }
        public string ProvinceID { get; set; }
        public string ProvinceName { get; set; }

        public Doctor PreDoctor { get; set; }
        public Doctor NextDoctor { get; set; }
    }
}