using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiYiJiShu.Models;
using ShiYiJiShu.Data;
using Common;

namespace ShiYiJiShu.Controllers
{
    public class SelectProjectController : Controller
    {
        BaseClass bc = new BaseClass();
        DataService _dateService = new DataService();

        public ActionResult List(int classid, int? currentPage)
        {
            SelectProjectListModel model = new SelectProjectListModel();

            int pageCount = 16;

            IEnumerable<SelectProject> selectProject = _dateService.GetSelectProjectsByPageNum(classid, pageCount, currentPage);
            model.SelectProjectList = selectProject;
 
            int totalCount = _dateService.GetSelectProjectTotalCount(classid);

            NewsClass newsClass = _dateService.GetNewsClassByClassID(7);
            model.TopClassID = newsClass.ClassID;
            model.TopClassName = newsClass.ClassName;

            if (classid != 0)
            {
                NewsClass currentClass = _dateService.GetNewsClassByClassID(classid);
                model.ClassID = currentClass.ClassID;
                model.ClassName = currentClass.ClassName;
            }

            if (bc.CheckMobile())
            {
                if (totalCount > pageCount)
                {
                    model.PageLink = bc.GetMobilePageLink(classid, pageCount, totalCount, currentPage, "../SelectProject/List");
                }

                return View("~/Views/Mobile/VideoList.cshtml", model);
            }
            else
            {
                if (totalCount > pageCount)
                {
                    model.PageLink = bc.GetPageLink(pageCount, totalCount, currentPage, "../SelectProject/List/" + classid);
                }

                return View(model);
            }
        }

        public ActionResult Detail(int projectid)
        {
            _dateService.AddSelectProjectHitCount(projectid);

            SelectProjectDetailModel model = new SelectProjectDetailModel();
            model.SelectProject = _dateService.GetSelectProjectByID(projectid);

            NewsClass newsClass = _dateService.GetNewsClassByClassID(7);
            model.TopClassID = newsClass.ClassID;
            model.TopClassName = newsClass.ClassName;
 
            if (model.SelectProject.SecondClassID != null)
            {
                model.ClassID = (int)model.SelectProject.SecondClassID;
            }

            if (bc.CheckMobile())
            {
                return View("~/Views/Mobile/VideoDetail.cshtml", model);
            }
            else
            {
                return View(model);
            }
        }
 
    }
}
