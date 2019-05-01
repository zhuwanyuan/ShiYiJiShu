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
    public class InfoController : Controller
    {
        DataService _dateService = new DataService();
        BaseClass bc = new BaseClass();

        public ActionResult Index(int classid)
        {
            InfoModel model = new InfoModel();
            var info = _dateService.GetInfoByInfoID(classid);
            model.Info = info;
 
            NewsClass newsClass = _dateService.GetNewsClassByClassID(classid);
            model.ClassName = newsClass.ClassName;
            model.ClassID = newsClass.ClassID;
            model.ParentClassID = _dateService.GetParentClassID(classid);

            if (model.ParentClassID == 139)
            {
                model.HasSubClass = true;
            }
 
            if (bc.CheckMobile())
            {
                return View("~/Views/Mobile/Info.cshtml", model);
            }
            else
            {
                return View(model);
            } 
        }

        public PartialViewResult GetAboutLeftMenu(int parentClassID, int currentClassID)
        {
            AboutLeftMenuModel model = new AboutLeftMenuModel();
            model.SecondClasses = _dateService.GetSubClasses(parentClassID, 2);
            model.ThirdClasses = _dateService.GetSubClasses(28, 3);
            model.CurrentClassID = currentClassID;

            return PartialView("_AboutLeftMenuPartial", model);
        }
    }
}
