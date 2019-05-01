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
    public class TechController : Controller
    {
        DataService _dataService = new DataService();
        BaseClass bc = new BaseClass();

        public ActionResult Info(int classid)
        {
            TechInfoModel model = new TechInfoModel();
            model.Info = _dataService.GetInfoByInfoID(classid);
            model.ClassName = _dataService.GetNewsClassByClassID(classid).ClassName;
       
            return View(model);
        }

        public ActionResult List(int classid, int? currentpage)
        {
            TechListModel model = new TechListModel();
            model.ClassName = _dataService.GetNewsClassByClassID(classid).ClassName;
            model.techs = _dataService.GetTechnologiesByPageNum(classid, 30, currentpage);
 
            int totalCount = _dataService.GetTechnologyCountByClassid(classid);
            if (totalCount > 30)
            {
                model.PageLink = bc.GetPageLink(classid, 30, totalCount, currentpage, "../Tech/List");
            }

            return View(model);
        }

        public PartialViewResult techMenuPartial()
        {
            TechMenuModel model = new TechMenuModel();
            model.GongZuoJieShao = _dataService.GetSubClasses(57);
            model.RuXuanJiShu = _dataService.GetSubClasses(58);

            return PartialView("~/Views/Shared/_TechMenuPartial.cshtml", model);
        }

    }
}
