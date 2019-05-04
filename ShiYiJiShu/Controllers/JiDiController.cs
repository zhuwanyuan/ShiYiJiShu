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
    public class JiDiController : Controller
    {
        BaseClass bc = new BaseClass();
        DataService _dateService = new DataService();

        public ActionResult List(int classid, string provinceid, int? currentPage)
        {
            JiDiListModel model = new JiDiListModel();

            int pageCount = 16;

            IEnumerable<JiDiJianShe> jiDiList = _dateService.GetJiDisByPageNum(classid, provinceid, pageCount, currentPage);
            model.JiDiList = jiDiList;

            model.ProvinceList = _dateService.GetProvinceList();
            model.ProvinceID = provinceid;

            int totalCount = _dateService.GetJiDiTotalCount(classid);

            NewsClass newsClass = _dateService.GetNewsClassByClassID(5);
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
                    model.PageLink = bc.GetMobilePageLink(classid, pageCount, totalCount, currentPage, "../Video/List");
                }

                return View("~/Views/Mobile/VideoList.cshtml", model);
            }
            else
            {
                if (totalCount > pageCount)
                {
                    model.PageLink = bc.GetPageLink(pageCount, totalCount, currentPage, "../Video/List/" + classid);
                }

                return View(model);
            }
        }

        public ActionResult Detail(int jidiid)
        {
            _dateService.AddJiDiHitCount(jidiid);

            JiDiDetailModel model = new JiDiDetailModel();
            model.JiDi = _dateService.GetJiDiByID(jidiid);

            NewsClass newsClass = _dateService.GetNewsClassByClassID(5);
            model.TopClassID = newsClass.ClassID;
            model.TopClassName = newsClass.ClassName;
 
            if (model.JiDi.SecondClassID != null)
            {
                model.ClassID = (int)model.JiDi.SecondClassID;
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

        public PartialViewResult GetVideoMenuPartial(int currentClassID)
        {
            VideoMenuModel model = new VideoMenuModel();
            model.VideoSubClass = _dateService.GetSubClasses(11, 2);
            model.CurrentClassID = currentClassID;

            return PartialView("_VideoMenuPartial", model);
        }
    }
}
