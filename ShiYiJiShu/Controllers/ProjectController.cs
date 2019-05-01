using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Data;
using ShiYiJiShu.Models;
using Common;
using System.Web.Http;

namespace ShiYiJiShu.Controllers
{
    public class ProjectController : Controller
    {
        BaseClass bc = new BaseClass();
       
        DataService _dataService = new DataService();

        public ActionResult Index(int classid, int? currentpage)
        {
            ProjectListModel model = new ProjectListModel();
            int pageCount = 16;

            NewsClass topClass = _dataService.GetNewsClassByClassID(19);
            model.TopClassID = topClass.ClassID;
            model.TopClassName = topClass.ClassName;

            if (classid != 0)
            {
                NewsClass currentClass = _dataService.GetNewsClassByClassID(classid);
                model.ClassID = currentClass.ClassID;
                model.ClassName = currentClass.ClassName;
            }
 
            model.ProjectList = _dataService.GetProjectsByPageNum(classid, pageCount, currentpage);

            int totalCount = _dataService.GetProjectTotalCount(classid);
 
            if (bc.CheckMobile())
            {
                if (totalCount > pageCount)
                {
                    model.PageLink = bc.GetMobilePageLink(classid, pageCount, totalCount, currentpage, "../Project/List");
                }
 
                return View("~/Views/Mobile/ProjectList.cshtml", model);
            }
            else
            {
                if (totalCount > pageCount)
                {
                    model.PageLink = bc.GetPageLink(pageCount, totalCount, currentpage, "../Project/List/" + classid);
                }

                return View(model);
            }
        }

        public ActionResult Detail(int projectid)
        {
            _dataService.AddProjectHitCount(projectid);
            Project project = _dataService.GetProjectByProjectID(projectid);
 
            ProjectDetailModel model = new ProjectDetailModel();
            model.Project = project;

            NewsClass topClass = _dataService.GetNewsClassByClassID(19);
            model.TopClassID = topClass.ClassID;
            model.TopClassName = topClass.ClassName;

            int classid = 19;
            NewsClass newsClass = _dataService.GetNewsClassByClassID(classid);
            model.ClassID = classid;
            model.ClassName = newsClass.ClassName;

            if (bc.CheckMobile())
            {
                return View("~/Views/Mobile/ProjectDetail.cshtml", model);
            }
            else
            {
                return View("Detail", model);
            }
            
        }

        public ActionResult Register(int projectid)
        {
            Project project = _dataService.GetProjectByProjectID(projectid);
            return View(project);
        }

        public JsonResult AddProjectRegister([FromBody]string username, string mobile, string company, string email, int projectid)
        {
 
            int returnCode = -1;
            object data = null;
            string message = "";

            try
            {
                returnCode = 0;

                ProjectRegister model = new ProjectRegister();
                model.UserName = username;
                model.Mobile = mobile;
                model.ProjectID = projectid;
                model.UserEmail = email;
                model.UserCompany = company;
                model.RegDateTime = DateTime.Now;

                _dataService.AddProjectRegister(model);
            }
            catch (Exception ex)
            {
                returnCode = -1;
                message = ex.Message;
            }

            var result = new
            {
                Data = data,
                ReturnCode = returnCode,
                Message = message
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
