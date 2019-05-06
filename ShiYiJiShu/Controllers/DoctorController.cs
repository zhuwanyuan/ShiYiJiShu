using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Data;
using ShiYiJiShu.Models;
using Common;

namespace ShiYiJiShu.Controllers
{
    public class DoctorController : Controller
    {
        DataService _dateService = new DataService();
        BaseClass bc = new BaseClass();
        int classid = 25;

        public ActionResult Index(string provinceid, int? currentpage)
        {
            DoctorIndexModel model = new DoctorIndexModel();

            NewsClass newsClass = _dateService.GetNewsClassByClassID(25);
 
            if (provinceid == "0") //显示首席专家和特邀专家
            {
                model.ShouXiDoctorList = _dateService.GetDoctorsByType(1, 10);
                model.TeYaoDoctorList = _dateService.GetDoctorsByType(2, 10);
                model.ProvinceID = "0";
            }
            else if (provinceid == "1") //显示首席专家
            {
                model.DoctorList = _dateService.GetTypeDoctorsByPageNum(1, 20, currentpage);
                model.ProvinceID = "1";
                model.ProvinceName = "首席专家";

                int totalCount = _dateService.GetDoctorTotalCountByType(1);

                if (totalCount > 20)
                {
                    model.PageLink = bc.GetPageLink(20, totalCount, currentpage, "../Doctor/Index/" + provinceid);
                }
            }
            else if (provinceid == "2") //显示特邀专家
            {
                model.DoctorList = _dateService.GetTypeDoctorsByPageNum(2, 20, currentpage);
                model.ProvinceID = "2";
                model.ProvinceName = "特邀专家";

                int totalCount = _dateService.GetDoctorTotalCountByType(2);

                if (totalCount > 20)
                {
                    model.PageLink = bc.GetPageLink(20, totalCount, currentpage, "../Doctor/Index/" + provinceid);
                }
            }
            else //按省份显示专家
            {
                model.DoctorList = _dateService.GetDoctorsByProvinceID(provinceid, 20, currentpage);
                model.ProvinceID = provinceid;

                int totalCount = _dateService.GetDoctorTotalCountByProvinceID(provinceid);

                if (provinceid == "000000") //全部省份
                {
                    model.ProvinceName = "全部专家";
                }
                else
                {
                    Province province = _dateService.GetProvinceByID(provinceid);
                    model.ProvinceName = province.Name;
                }

                if (totalCount > 20)
                {
                    model.PageLink = bc.GetPageLink(20, totalCount, currentpage, "../Doctor/Index/" + provinceid);
                }
            }

            model.ProvinceList = _dateService.GetProvinceList();
            model.ClassName = newsClass.ClassName;
            model.ClassID = classid;

            if (bc.CheckMobile())
            {          
                return View("~/Views/Mobile/DoctorIndex.cshtml", model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult List(int classid, string provinceid, int? currentpage)
        {
            NewsClass newsClass = _dateService.GetNewsClassByClassID(classid);
            NewsClass parentClass = _dateService.GetParentClass(newsClass.ClassID);

            int totalCount = _dateService.GetDoctorTotalCountByClassID(classid);

            DoctorListModel model = new DoctorListModel();
            model.DoctorList = _dateService.GetDoctorsByPageNum(classid, 16, currentpage);
            model.ClassName = newsClass.ClassName;
            model.ParentClassName = parentClass.ClassName;
 
            if (totalCount > 16)
            {
                model.PageLink = bc.GetPageLink(16, totalCount, currentpage, "List/" + classid);
            }
            
            model.ClassID = classid;

            if (bc.CheckMobile())
            {
                return View("~/Views/Mobile/DoctorList.cshtml", model);
            }
            else
            {
                return View(model);
            }
        }
        
        public ActionResult Detail(int doctorid)
        {
            _dateService.AddDoctorHitCount(doctorid);

            Doctor doctor = _dateService.GetDoctorByDoctorID(doctorid);
            NewsClass newsClass = _dateService.GetNewsClassByClassID((int)doctor.SecondClassID);
            NewsClass parentClass = _dateService.GetParentClass(newsClass.ClassID);

            DoctorDetailModel model = new DoctorDetailModel();
            model.Doctor = doctor;
            model.ClassID = newsClass.ClassID;
            model.ClassName = newsClass.ClassName;
            model.ParentClassName = parentClass.ClassName; 
            model.ProvinceList = _dateService.GetProvinceList();
            model.ProvinceID = doctor.ProvinceID.Trim();

            model.PreDoctor = _dateService.GetPreDoctor((int)doctor.SecondClassID, doctorid);
            model.NextDoctor = _dateService.GetNextDoctor((int)doctor.SecondClassID, doctorid);

            if (bc.CheckMobile())
            {
                return View("~/Views/Mobile/DoctorDetail.cshtml", model);
            }
            else
            {
                return View(model);
            }
        }

        public PartialViewResult DoctorLeftMenu(int topClassID, int currentClassID, string controllerName)
        {
            DoctorClassListModel model = new DoctorClassListModel();

            List<DoctorClassModel> modellist = new List<DoctorClassModel>();
  
            List<NewsClass> bigClassList = _dateService.GetSubClasses(topClassID).ToList<NewsClass>();

            for (int i = 0; i < bigClassList.Count(); i++)
            {
                DoctorClassModel doctorClassModel = new DoctorClassModel();
                NewsClass bigClass = bigClassList[i];
                doctorClassModel.BigClass = bigClass;


                IEnumerable<NewsClass> smallclasses=_dateService.GetSubClasses(bigClass.ClassID);
                if (smallclasses != null)
                {
                    doctorClassModel.SmallClassList = smallclasses.ToList<NewsClass>();
                }


                modellist.Add(doctorClassModel);
            }

            model.DoctorClassList = modellist;
            model.CurrentClassID = currentClassID;
            model.ControllerName = controllerName;
 
            if (bc.CheckMobile())
            {
                return PartialView("~/Views/Mobile/_MobileDoctorMenuPartial.cshtml", model);
            }
            else
            {
                return PartialView("_DoctorLeftMenuPartial", model);
            }
        }
    }
}
