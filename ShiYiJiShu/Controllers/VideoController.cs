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
    public class VideoController : Controller
    {
        BaseClass bc = new BaseClass();
        DataService _dateService = new DataService();

        public ActionResult List(int classid, int? currentPage)
        {
            VideoListModel model = new VideoListModel();

            int pageCount = 16;

            IEnumerable<Video> videoList = _dateService.GetVideosByPageNum(classid, pageCount, currentPage);
            model.VideoList = videoList;

            int totalCount = _dateService.GetVideoTotalCount(classid);
 
            NewsClass newsClass = _dateService.GetNewsClassByClassID(11);
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

        public ActionResult Detail(int videoid)
        {
            _dateService.AddVideoHitCount(videoid);

            VideoDetailModel model = new VideoDetailModel();
            model.Video = _dateService.GetVideoByVideoID(videoid);

            NewsClass newsClass = _dateService.GetNewsClassByClassID(11);
            model.TopClassID = newsClass.ClassID;
            model.TopClassName = newsClass.ClassName;
 
            if (model.Video.SecondClassID != null)
            {
                model.ClassID = (int)model.Video.SecondClassID;
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
